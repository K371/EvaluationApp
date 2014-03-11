using System;
using System.Collections.Generic;
using System.Linq;
using API.Services.Exceptions;
using API.Services.Models.Entities.Courses;
using API.Services.Models.Entities.Customers;
using API.Services.Models.Entities.Evaluations;
using API.Services.Models.Entities.General;
using API.Services.Repositories;
using API.Models.DTO.Evaluations;

namespace API.Services.Services
{
	/// <summary>
	/// EvaluationsServiceProvider handles the logic behind evaluations.
	/// </summary>
	public class EvaluationsServiceProvider
	{
		#region Member variables
		private readonly IUnitOfWork _uow;
		private readonly IRepository<EvaluationTemplate> _evaluationTemplates;
		private readonly IRepository<EvaluationQuestion> _evaluationQuestions;
		private readonly IRepository<EvaluationQuestionValue> _evaluationQuestionValues;
		private readonly IRepository<EvaluationInstance> _evaluationInstances;
		private readonly IRepository<EvaluationAnswer> _evaluationAnswers;
		private readonly IRepository<EvaluationReply> _evaluationReplies;
		private readonly IRepository<CourseInstanceStudent> _courseInstanceStudents;
		private readonly IRepository<CourseInstance> _courseInstances;
		private readonly IRepository<User> _users;
		private readonly IRepository<TeachersRegistration> _teachersRegistrations;
		#endregion

		#region Constructor
		public EvaluationsServiceProvider(IUnitOfWork uow)
		{
			_uow = uow;
			_evaluationTemplates      = uow.GetRepository<EvaluationTemplate>();
			_evaluationQuestions      = uow.GetRepository<EvaluationQuestion>();
			_evaluationQuestionValues = uow.GetRepository<EvaluationQuestionValue>();
			_evaluationInstances      = uow.GetRepository<EvaluationInstance>();
			_evaluationAnswers        = uow.GetRepository<EvaluationAnswer>();
			_evaluationReplies        = uow.GetRepository<EvaluationReply>();
			_courseInstanceStudents   = uow.GetRepository<CourseInstanceStudent>();
			_courseInstances          = uow.GetRepository<CourseInstance>();
			_users                    = uow.GetRepository<User>();
			_teachersRegistrations    = uow.GetRepository<TeachersRegistration>();
		}
		#endregion

		#region Public functions
		/// <summary>
		/// Returns all evaluation templates.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<EvaluationTemplateListDTO> GetEvaluationTemplates()
		{
			var result = _evaluationTemplates.All().Select(t => new EvaluationTemplateListDTO
			{
				ID = t.ID,
				TitleEN = t.NameEN,
				TitleIS = t.NameIS
			}).ToList();

			return result;
		}

		/// <summary>
		/// Returns a single evaluation template, including its questions
		/// and their possible choices.
		/// </summary>
		/// <param name="id">The ID of the evaluation template.</param>
		/// <returns></returns>
		public EvaluationTemplateDTO GetEvaluationTemplateById(int id)
		{
			var templ = _evaluationTemplates.All()
			                                .Where(t => t.ID == id)
			                                .Select(t => new EvaluationTemplateDTO
			                                {
												ID = t.ID,
												IntroTextEN = t.IntroTextEN,
												IntroTextIS = t.IntroTextIS,
												TitleEN = t.NameEN,
												TitleIS = t.NameIS
			                                }).SingleOrDefault();

			if (templ != null)
			{
				// Load the questions for this template:
				var questions = _evaluationQuestions.All()
				                                    .Where(q => q.TemplateID == id)
				                                    .ToList();

				// Then, filter the returned questions based on target:
				templ.CourseQuestions = LoadEvaluationQuestions(questions, EvaluationQuestion.TARGET_COURSE);
				templ.TeacherQuestions = LoadEvaluationQuestions(questions, EvaluationQuestion.TARGET_TEACHERS);

				return templ;
			}

			return null;
		}

		/// <summary>
		/// Returns all open evaluations for a user, regardless if
		/// the user has completed the given evaluation or not
		/// (we might want to change this later).
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		public IEnumerable<StudentEvaluationListDTO> GetEvaluationsForUser(string userName)
		{
			// First, we find the courses a student is registered in:
			var studentCourses = from ci in _courseInstances.All()
			             join cis in _courseInstanceStudents.All()
				             on ci.ID equals cis.CourseInstanceID
			             join u in _users.All()
				             on cis.SSN equals u.SSN
			             where u.UserName == userName
			             select ci;

			// Then, we find all open evaluations:
			var openEvaluations = from eval in _evaluationInstances.All()
			                      where eval.StartDate < DateTime.Now.Date
			                            && eval.EndDate > DateTime.Now.Date
			                      select eval;

			// Now, this is a simple matter of a cross-join:
			var result = (from c in studentCourses
			              from e in openEvaluations
			              select new StudentEvaluationListDTO
			              {
							  ID = e.ID,
							  CourseID = c.CourseID,
							  CourseNameEN = c.NameEN,
							  CourseNameIS = c.NameIS,
							  Semester = "20141" // Doesn't change in this version...
			              }).ToList();

			return result;
		}

		/// <summary>
		/// Returns a list of evaluation instances.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<EvaluationListDTO> GetEvaluations()
		{
			var evaluations = (from inst in _evaluationInstances.All()
			                  join templ in _evaluationTemplates.All() on inst.TemplateID equals templ.ID
			                  select new EvaluationListDTO
			                  {
								  ID = inst.ID,
								  StartDate = inst.StartDate,
								  EndDate = inst.EndDate,
								  TemplateTitleEN = templ.NameEN,
								  TemplateTitleIS = templ.NameIS,
			                  }).ToList();

			foreach (var eval in evaluations)
			{
				if (eval.EndDate < DateTime.Now)
				{
					eval.Status = "closed";
				}
				else if (eval.StartDate > DateTime.Now)
				{
					eval.Status = "new";
				}
				else
				{
					eval.Status = "open";
				}
			}

			return evaluations;
		}

		/// <summary>
		/// Returns the results of a given evaluation (intended for the admin).
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public EvaluationDTO GetEvaluationResultsByCourse(int id)
		{
			// First, find out the evaluation instance
			// and at the same time we validate the ID:
			var evalInstance = (from inst in _evaluationInstances.All()
								join templ in _evaluationTemplates.All() on inst.TemplateID equals templ.ID
			                   where inst.ID == id
			                   select new EvaluationDTO
			                   {
								   ID = inst.ID,
								   TemplateID = inst.TemplateID,
								   TemplateTitleEN = templ.NameEN,
								   TemplateTitleIS = templ.NameIS,
			                   }).SingleOrDefault();
			if (evalInstance == null)
			{
				throw new ServiceValidationException("INVALID_EVALUATION_ID");
			}

			// Then, load all results for this evaluation
			// (from all courses):
			var allAnswers = (from ans in _evaluationAnswers.All()
			               where ans.EvaluationInstanceID == id
			               select ans).ToList();

			// Get a list of all course IDs which have answers
			// in this evaluation:
			var courseIDList = (from ans in allAnswers
			                    select ans.CourseInstanceID).Distinct();

			// Generate a list of distinct courses from that list:
			evalInstance.Courses = (from c in _courseInstances.All()
			                        join cid in courseIDList on c.ID equals cid
			                        select new CourseEvaluationResultDTO
			                        {
										ID = c.ID,
										CourseID = c.CourseID,
										CourseNameEN = c.NameEN,
										CourseNameIS = c.NameIS,
										Semester = "20141", // Not used in this version
										Questions = new List<EvaluationQuestionResultDTO>()
			                        }).ToList();

			// Oh, and one more thing: load all the questions
			// in this evaluation.
			var questions = (from q in _evaluationQuestions.All()
			                 where q.TemplateID == evalInstance.TemplateID
			                 select q).ToList();

			var courseQuestions = LoadEvaluationQuestions(questions, EvaluationQuestion.TARGET_COURSE);
			var teacherQuestions = LoadEvaluationQuestions(questions, EvaluationQuestion.TARGET_TEACHERS);

			// Generate the results for each course:
			foreach (var course in evalInstance.Courses)
			{
				// Load all answers belonging to this course (we will
				// filter them in more detail below):
				var courseAnswers = (from a in allAnswers
				                     where a.CourseInstanceID == course.ID
				                     select a).ToList();

				// Then, enumerate all questions in this evaluation.
				// General course questions are easier to handle:
				foreach (var q in courseQuestions)
				{
					var res = CreateEvaluationResult(q, courseAnswers, null);
					course.Questions.Add(res);
				}

				// Figure out what the teachers are in this course:
				var ssnList = (from tr in _teachersRegistrations.All()
				                   where tr.CourseID == course.ID
				                   select tr.SSN).ToList();

				// Yes, the complexity here is O(n^3). However,
				// the numbers will never get very large:
				// - number of teachers in a course: usually between 1-5,
				//   numbers as high as 20 can occur but are rare
				// - number of questions aimed at teachers: probably between 1 and 5
				// - number of courses: could be between 100 and 200
				// Therefore, we're probably talking about max number of 
				// iterations around 200x20x5 in extreme cases.
				foreach (var ssn in ssnList)
				{
					foreach (var q in teacherQuestions)
					{
						var result = CreateEvaluationResult(q, courseAnswers, ssn);
						course.Questions.Add(result);
					}
				}
			}

			return evalInstance;
		}

		/// <summary>
		/// Adds a new evaluation instance.
		/// </summary>
		/// <param name="evaluation"></param>
		public void AddEvaluation(NewEvaluationDTO evaluation)
		{
			// Validate the input:
			if (evaluation.StartDate > evaluation.EndDate)
			{
				throw new ServiceValidationException("START_DATE_MUST_PRECEDE_END_DATE");
			}

			if (_evaluationTemplates.All().SingleOrDefault(t => t.ID == evaluation.TemplateID) == null)
			{
				throw new ServiceValidationException("INVALID_TEMPLATE_ID");
			}

			// Convert from DTO to entity and save:
			var inst = new EvaluationInstance
			{
				StartDate  = evaluation.StartDate,
				EndDate    = evaluation.EndDate,
				TemplateID = evaluation.TemplateID
			};

			_evaluationInstances.Add(inst);
			_uow.Save();
		}

		/// <summary>
		/// Adds a new evaluation template, including questions
		/// and their possible answers.
		/// </summary>
		/// <param name="evaluationTemplate"></param>
		public void AddEvaluationTemplate(EvaluationTemplateDTO evaluationTemplate)
		{
			// Input validation:
			if (string.IsNullOrEmpty(evaluationTemplate.IntroTextEN))
			{
				throw new ServiceValidationException("MISSING_INTRO_TEXT_EN");
			}
			if (string.IsNullOrEmpty(evaluationTemplate.IntroTextIS))
			{
				throw new ServiceValidationException("MISSING_INTRO_TEXT_IS");
			}
			if (string.IsNullOrEmpty(evaluationTemplate.TitleEN))
			{
				throw new ServiceValidationException("MISSING_TITLE_EN");
			}
			if (string.IsNullOrEmpty(evaluationTemplate.TitleIS))
			{
				throw new ServiceValidationException("MISSING_TITLE_IS");
			}

			// Then, we add the template itself, since
			// we need the ID of the template when adding
			// the questions.
			// NOTE: this might result in an orphan template if a given question
			// doesn't validate! This needs to be fixed.
			var templ = new EvaluationTemplate
			{
				IntroTextEN = evaluationTemplate.IntroTextEN,
				IntroTextIS = evaluationTemplate.IntroTextIS,
				NameEN = evaluationTemplate.TitleEN,
				NameIS = evaluationTemplate.TitleIS,
			};

			_evaluationTemplates.Add(templ);
			_uow.Save();

			// Finally, save the questions in the template:
			var order = 0;
			foreach (var q in evaluationTemplate.CourseQuestions)
			{
				SaveQuestion(templ.ID, q, order, EvaluationQuestion.TARGET_COURSE);
			}

			order = 0;
			foreach (var q in evaluationTemplate.TeacherQuestions)
			{
				SaveQuestion(templ.ID, q, order, EvaluationQuestion.TARGET_TEACHERS);
			}
		}

		/// <summary>
		/// Returns all evaluations in a given course 
		/// </summary>
		/// <param name="course"></param>
		/// <param name="semester"></param>
		/// <param name="evalID"></param>
		/// <returns></returns>
		public CourseEvaluationDTO GetEvaluationInCourse(string course, string semester, int evalID)
		{
			// We currently don't use the course, since all evaluations
			// apply to all courses, but that could change in later versions.

			var evaluation = (from eval in _evaluationInstances.All()
			                 join templ in _evaluationTemplates.All()
				                 on eval.TemplateID equals templ.ID
			                 where eval.ID == evalID
			                 select new CourseEvaluationDTO
			                 {
								ID = eval.ID,
								TemplateID = templ.ID,
								IntroTextEN = templ.IntroTextEN,
								IntoTextIS = templ.IntroTextIS,
								TitleEN = templ.NameEN,
								TitleIS = templ.NameIS,
			                 }).SingleOrDefault();

			if (evaluation != null)
			{
				var questions = _evaluationQuestions.All()
				                                    .Where(q => q.TemplateID == evaluation.TemplateID)
				                                    .ToList();

				evaluation.TeacherQuestions = LoadEvaluationQuestions(questions, EvaluationQuestion.TARGET_TEACHERS);
				evaluation.CourseQuestions = LoadEvaluationQuestions(questions, EvaluationQuestion.TARGET_COURSE);
			}

			return evaluation;
		}

		/// <summary>
		/// Adds all answers a student has given in a specific evaluation
		/// in a specific course.
		/// </summary>
		/// <param name="course"></param>
		/// <param name="semester"></param>
		/// <param name="evalID"></param>
		/// <param name="answers"></param>
		/// <param name="strUserName"></param>
		public void AddAnswersFromStudentToEvaluation(string course, string semester, int evalID, List<EvaluationAnswerDTO> answers, string strUserName)
		{
			var ssn = (from user in _users.All()
			           where user.UserName == strUserName
			           select user.SSN).SingleOrDefault();
			if (ssn == null)
			{
				throw new ArgumentException(strUserName);
			}

			var cID = (from c in _courseInstances.All()
			           where c.CourseID == course
					   // && c.Semester == semester - not used in this version
			           select c.ID).SingleOrDefault();
			if (cID == 0)
			{
				// Course couldn't be found, and was therefore invalid:
				throw new ServiceValidationException("INVALID_COURSE_ID");
			}

			// First, record the fact that this student has answered the 
			// evaluation for this course:
			var reply = new EvaluationReply
			{
				DateAdded = DateTime.Now,
				StudentSSN = ssn,
				EvaluationInstanceID = evalID
			};
			_evaluationReplies.Add(reply);
			// Don't save just yet, let's save everything
			// in one go...

			// Then, save the answers:
			foreach (var ansDTO in answers)
			{
				int value;
				Int32.TryParse(ansDTO.Value, out value);
				var evalAnswer = new EvaluationAnswer
				{
					CourseInstanceID     = cID,
					EvaluationInstanceID = evalID,
					QuestionID           = ansDTO.QuestionID,
					Value                = value,
					TeacherSSN           = ansDTO.TeacherSSN
				};
				_evaluationAnswers.Add(evalAnswer);
			}
			_uow.Save();
		}

		#endregion

		#region Private functions
		private List<EvaluationQuestionDTO> LoadEvaluationQuestions(IEnumerable<EvaluationQuestion> questions, int targetType)
		{
			var result = questions.Where(q => q.Target == targetType)
												.OrderBy(q => q.Order)
												.Select(q => new EvaluationQuestionDTO
												{
													ID = q.ID,
													ImageURL = q.ImageURL,
													TextEN = q.TextEN,
													TextIS = q.TextIS,
													Type = ( q.TypeID == EvaluationQuestion.TYPE_TEXT ? "text" : (q.TypeID == EvaluationQuestion.TYPE_OPTIONS_SINGLE ? "single" : "multiple")),
													Answers = _evaluationQuestionValues.All()
														.Where(qv => qv.QuestionID == q.ID)
														.Select(qv => new EvaluationQuestionAnswerDTO
														{
															ID = qv.ID,
															TextEN = qv.TextEN,
															TextIS = qv.TextIS,
															ImageURL = qv.ImageURL,
															Weight = qv.Value
														}).ToList()
												}).ToList();

			return result;
		}

		private void SaveQuestion(int templateID, EvaluationQuestionDTO q, int order, int target)
		{
			var typeID = EvaluationQuestion.TYPE_TEXT;
			if (q.Type == "single")
			{
				typeID = EvaluationQuestion.TYPE_OPTIONS_SINGLE;
			}
			else if (q.Type == "multiple")
			{
				typeID = EvaluationQuestion.TYPE_OPTIONS_MULTIPLE;
			}

			var question = new EvaluationQuestion
			{
				TextEN = q.TextEN,
				TextIS = q.TextIS,
				TemplateID = templateID,
				ImageURL = q.ImageURL,
				InsertDate = DateTime.Now,
				Order = order,
				Target = target,
				TypeID = typeID
			};
			_evaluationQuestions.Add(question);
			_uow.Save();

			// If there are answer options, we save them as well:
			if (q.Answers != null)
			{
				foreach (var qOption in q.Answers)
				{
					var evalQuestionValue = new EvaluationQuestionValue
					{
						ImageURL = qOption.ImageURL,
						QuestionID = question.ID,
						TextIS = qOption.TextIS,
						TextEN = qOption.TextEN,
						Value = qOption.Weight
					};
					_evaluationQuestionValues.Add(evalQuestionValue);
				}
			}
			_uow.Save();
		}

		private EvaluationQuestionResultDTO CreateEvaluationResult(EvaluationQuestionDTO q, IEnumerable<EvaluationAnswer> courseAnswers, string ssn)
		{
			var res = new EvaluationQuestionResultDTO
			{
				QuestionID = q.ID,
				TextEN = q.TextEN,
				TextIS = q.TextIS,
				Type = q.Type
			};
			if (q.Type == "text")
			{
				res.TextResults = (courseAnswers.Where(a => a.QuestionID == q.ID && a.TeacherSSN == ssn).Select(a => a.Text)).ToArray();
			}
			else if (q.Type == "single" || q.Type == "multiple")
			{
				res.OptionsResults = (from option in q.Answers
									  select new
									  {
										  Answer = option.ID,
										  AnswerTextIS = option.TextIS,
										  AnswerTextEN = option.TextEN,
										  option.Weight,
										  Count = (from a in courseAnswers
												   where a.QuestionID == q.ID
												   && a.Value == option.Weight
												   && a.TeacherSSN == ssn
												   select a).Count()
									  }).ToList();
			}
			return res;
		}

		#endregion
	}
}