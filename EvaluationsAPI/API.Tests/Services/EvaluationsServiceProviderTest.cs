using System;
using System.Linq;
using System.Collections.Generic;
using API.Models.DTO.Evaluations;
using API.Services.Exceptions;
using API.Services.Models.Entities.Courses;
using API.Services.Models.Entities.Customers;
using API.Services.Models.Entities.Evaluations;
using API.Services.Models.Entities.General;
using API.Services.Services;
using API.Tests.MockObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace API.Tests.Services
{
	[TestClass]
	public class EvaluationsServiceProviderTest
	{
		#region Member variables
		private EvaluationsServiceProvider _service;
		private List<EvaluationTemplate> _templates;
		private List<EvaluationQuestion> _questions;
		private List<EvaluationQuestionValue> _questionValues;
		private List<EvaluationInstance> _instances;
		private List<EvaluationReply> _replies;
		private List<EvaluationAnswer> _answers;
		private MockFactory _mockFactory;
		#endregion

		#region Setup
		[TestInitialize]
		public void Setup()
		{
			var mockUnitOfWork = new MockUnitOfWork<MockDataContext>();
			_mockFactory = new MockFactory();

			#region Mock data - evaluation templates:
			// We create three templates, one for the midterm (with few 
			// questions), one for the end of the semester (with more 
			// questions), and one which has no questions and no instance
			// created from it. Technically, we should not allow users to
			// create templates without any questions...
			_templates = new List<EvaluationTemplate>
			{
				new EvaluationTemplate
				{
					ID = 1,
					NameIS = "Miðannarmat",
					NameEN = "Midterm evaluation",
					IntroTextEN = "Some intro text",
					IntroTextIS = "Upphafstexti"
				},
				new EvaluationTemplate
				{
					ID = 2,
					NameIS = "Kennslumat í lok annar",
					NameEN = "Final evaluation",
					IntroTextEN = "Some other intro text",
					IntroTextIS = "Annar upphafstexti"
				},
				new EvaluationTemplate
				{
					ID = 3,
					NameIS = "Ónotað kennslumat",
					NameEN = "Unused evaluation",
					IntroTextEN = "Yet other intro text",
					IntroTextIS = "Enn annar upphafstexti"
				}
			};
			mockUnitOfWork.SetRepositoryData(_templates);
			#endregion

			#region Mock data - evaluation questions:
			// There will be three questions in the first
			// template, 4 questions in the second one,
			// and the third template will have no questions.

			_questions = new List<EvaluationQuestion>
			{
				#region Questions belonging to the first template:
				new EvaluationQuestion
				{
					ID = 1,
					Order = 1,
					InsertDate = DateTime.Now,
					Target = EvaluationQuestion.TARGET_COURSE,
					TypeID = EvaluationQuestion.TYPE_OPTIONS_SINGLE,
					TemplateID = 1,
					TextIS = "Ertu ánægð(ur) með námskeiðið?",
					TextEN = "Are you happy with the course so far?"
				},
				new EvaluationQuestion
				{
					ID = 2,
					Order = 2,
					InsertDate = DateTime.Now,
					Target = EvaluationQuestion.TARGET_COURSE,
					TypeID = EvaluationQuestion.TYPE_TEXT,
					TemplateID = 1,
					TextIS = "Hvað finnst þér vera vel gert?",
					TextEN = "What is positive about the course?"
				},
				new EvaluationQuestion
				{
					ID = 3,
					Order = 3,
					InsertDate = DateTime.Now,
					Target = EvaluationQuestion.TARGET_COURSE,
					TypeID = EvaluationQuestion.TYPE_TEXT,
					TemplateID = 1,
					TextIS = "Hvað mætti betur fara?",
					TextEN = "What is negative about the course?"
				},
				#endregion

				#region Questions belonging to the second template:
				// Define questions in different order from what they should
				// appear, to test that we get them in the correct order...
				new EvaluationQuestion
				{
					ID         = 4,
					Order      = 2,  // First here, should be second
					InsertDate = DateTime.Now,
					Target     = EvaluationQuestion.TARGET_COURSE,
					TypeID     = EvaluationQuestion.TYPE_OPTIONS_SINGLE,
					TemplateID = 2,
					TextIS     = "Hversu ánægð(ur) ertu með námsefnið?",
					TextEN     = "How happy are you with the material?"
				},
				new EvaluationQuestion
				{
					ID         = 5,
					Order      = 1, // Second here, should be first...
					InsertDate = DateTime.Now,
					Target     = EvaluationQuestion.TARGET_COURSE,
					TypeID     = EvaluationQuestion.TYPE_OPTIONS_MULTIPLE,
					TemplateID = 2,
					TextIS     = "Þessi spurning meikar ekki sens, er bara að prófa multiple möguleikann",
					TextEN     = "This question is just here to test the 'multiple' option"
				},
				new EvaluationQuestion
				{
					ID         = 6,
					Order      = 3,
					InsertDate = DateTime.Now,
					Target     = EvaluationQuestion.TARGET_TEACHERS,
					TypeID     = EvaluationQuestion.TYPE_OPTIONS_SINGLE,
					TemplateID = 2,
					TextIS     = "Hversu ánægð(ur) ertu með kennarann?",
					TextEN     = "How happy are you with the teacher?"
				},
				new EvaluationQuestion
				{
					ID         = 7,
					Order      = 4,  // Define questions in different order to test that...
					InsertDate = DateTime.Now,
					Target     = EvaluationQuestion.TARGET_TEACHERS,
					TypeID     = EvaluationQuestion.TYPE_TEXT,
					TemplateID = 2,
					TextIS     = "Hefurðu athugasemdir um kennarann?",
					TextEN     = "Have you got any comments on the teacher?"
				},
				#endregion
			};
			mockUnitOfWork.SetRepositoryData(_questions);
			#endregion

			#region Mock data - evaluation question values:
			// Template 1 q1 will have one multiple choice
			// question, and two text questions (which don't 
			// have any values associated with them).
			// Template 2 q1-q3 will all be multiple choice
			// questions, while template 2 q4 is a text question.
			_questionValues = new List<EvaluationQuestionValue>
			{
				#region Template 1, question 1:
				new EvaluationQuestionValue
				{
					ID = 1,
					QuestionID = 1,
					TextIS = "Mjög óánægð(ur)",
					TextEN = "Very unhappy",
					Value = 1,
					ImageURL = "http://i.imgur.com/E9JjWNH.png"
				},
				new EvaluationQuestionValue
				{
					ID = 2,
					QuestionID = 1,
					TextIS = "Frekar óánægð(ur)",
					TextEN = "Somewhat unhappy",
					Value = 2,
				},
				new EvaluationQuestionValue
				{
					ID = 3,
					QuestionID = 1,
					TextIS = "Hvorki né",
					TextEN = "Neither happy nor unhappy",
					Value = 3,
				},
				new EvaluationQuestionValue
				{
					ID = 4,
					QuestionID = 1,
					TextIS = "Frekar ánægð(ur)",
					TextEN = "Rather happy",
					Value = 4,
				},
				new EvaluationQuestionValue
				{
					ID = 5,
					QuestionID = 1,
					TextIS = "Mjög ánægð(ur)",
					TextEN = "Very happy",
					Value = 5,
					ImageURL = "http://i.imgur.com/BnkRQJD.gif"
				},
				#endregion

				#region Template 2, question 1 (ID=4):
				new EvaluationQuestionValue
				{
					ID = 6,
					QuestionID = 4,
					TextIS = "Mjög óánægð(ur)",
					TextEN = "Very unhappy",
					Value = 1,
					ImageURL = "http://i.imgur.com/E9JjWNH.png"
				},
				new EvaluationQuestionValue
				{
					ID = 7,
					QuestionID = 4,
					TextIS = "Frekar óánægð(ur)",
					TextEN = "Somewhat unhappy",
					Value = 2,
				},
				new EvaluationQuestionValue
				{
					ID = 8,
					QuestionID = 4,
					TextIS = "Hvorki né",
					TextEN = "Neither happy nor unhappy",
					Value = 3,
				},
				new EvaluationQuestionValue
				{
					ID = 9,
					QuestionID = 4,
					TextIS = "Frekar ánægð(ur)",
					TextEN = "Rather happy",
					Value = 4,
				},
				new EvaluationQuestionValue
				{
					ID = 10,
					QuestionID = 4,
					TextIS = "Mjög ánægð(ur)",
					TextEN = "Very happy",
					Value = 5,
					ImageURL = "http://i.imgur.com/BnkRQJD.gif"
				},
				#endregion

				#region Template 2, question 2 (ID=5):
				new EvaluationQuestionValue
				{
					ID = 11,
					QuestionID = 5,
					TextIS = "Mjög óánægð(ur)",
					TextEN = "Very unhappy",
					Value = 1,
					ImageURL = "http://i.imgur.com/E9JjWNH.png"
				},
				new EvaluationQuestionValue
				{
					ID = 12,
					QuestionID = 5,
					TextIS = "Frekar óánægð(ur)",
					TextEN = "Somewhat unhappy",
					Value = 2,
				},
				new EvaluationQuestionValue
				{
					ID = 13,
					QuestionID = 5,
					TextIS = "Hvorki né",
					TextEN = "Neither happy nor unhappy",
					Value = 3,
				},
				new EvaluationQuestionValue
				{
					ID = 14,
					QuestionID = 5,
					TextIS = "Frekar ánægð(ur)",
					TextEN = "Rather happy",
					Value = 4,
				},
				new EvaluationQuestionValue
				{
					ID = 15,
					QuestionID = 5,
					TextIS = "Mjög ánægð(ur)",
					TextEN = "Very happy",
					Value = 5,
					ImageURL = "http://i.imgur.com/BnkRQJD.gif"
				},
				#endregion

				#region Template 2, question 3 (ID=6):
				new EvaluationQuestionValue
				{
					ID = 16,
					QuestionID = 6,
					TextIS = "Mjög óánægð(ur)",
					TextEN = "Very unhappy",
					Value = 1,
					ImageURL = "http://i.imgur.com/E9JjWNH.png"
				},
				new EvaluationQuestionValue
				{
					ID = 17,
					QuestionID = 6,
					TextIS = "Frekar óánægð(ur)",
					TextEN = "Somewhat unhappy",
					Value = 2,
				},
				new EvaluationQuestionValue
				{
					ID = 18,
					QuestionID = 6,
					TextIS = "Hvorki né",
					TextEN = "Neither happy nor unhappy",
					Value = 3,
				},
				new EvaluationQuestionValue
				{
					ID = 19,
					QuestionID = 6,
					TextIS = "Frekar ánægð(ur)",
					TextEN = "Rather happy",
					Value = 4,
				},
				new EvaluationQuestionValue
				{
					ID = 20,
					QuestionID = 6,
					TextIS = "Mjög ánægð(ur)",
					TextEN = "Very happy",
					Value = 5,
					ImageURL = "http://i.imgur.com/BnkRQJD.gif"
				},
				#endregion
			};
			mockUnitOfWork.SetRepositoryData(_questionValues);

			#endregion

			#region Mock data - evaluation instances:
			// There will be three instances, two from a single
			// template (the midterm template), and one from the
			// end-of-semester template. The third template will have
			// no instances. One instance will be closed, another
			// will be open, and the third one created (i.e. not
			// yet opened.
			_instances = new List<EvaluationInstance>
			{
				new EvaluationInstance
				{
					ID = 1,
					TemplateID = 1,
					// Instance has been closed:
					StartDate = DateTime.Now.AddDays(-10),
					EndDate = DateTime.Now.AddDays(-3)
				},

				new EvaluationInstance
				{
					ID = 2,
					TemplateID = 2,
					// Instance is still open:
					StartDate = DateTime.Now.AddDays(-6),
					EndDate = DateTime.Now.AddDays(3),
				},

				// Kind of doesn't make sense that the midterm
				// hasn't been opened, while the end-of-term
				// is open, but whatever...
				new EvaluationInstance
				{
					ID = 3,
					TemplateID = 1,
					// Instance has yet to be opened:
					StartDate = DateTime.Now.AddDays(6),
					EndDate = DateTime.Now.AddDays(12),
				}
			};
			mockUnitOfWork.SetRepositoryData(_instances);
			#endregion

			#region Mock data - evaluation answers:
			// Midterm instance answers:
			// - There will be 5 answers: 3 for question 1
			//   (with values 5+4+4)
			//   and 2 text answers for question 2. 
			//   No answers for question 3.
			// End-of-term answers:
			// - 3 answers: one for the first question,
			//   and 1 for each teacher question.
			_answers = new List<EvaluationAnswer>
			{
				#region Evaluation instance 1 - answers:
				new EvaluationAnswer
				{
					ID = 1,
					QuestionID = 1,
					EvaluationInstanceID = 1,
					CourseInstanceID = 20435,
					Text = "",
					TeacherSSN = null,
					Value = 5
				},
				new EvaluationAnswer
				{
					ID = 2,
					QuestionID = 1,
					CourseInstanceID = 20435,
					EvaluationInstanceID = 1,
					Text = "",
					TeacherSSN = null,
					Value = 4
				},
				new EvaluationAnswer
				{
					ID = 3,
					QuestionID = 1,
					CourseInstanceID = 20435,
					EvaluationInstanceID = 1,
					Text = "",
					TeacherSSN = null,
					Value = 4
				},
				new EvaluationAnswer
				{
					ID = 4,
					QuestionID = 2,
					CourseInstanceID = 20435,
					EvaluationInstanceID = 1,
					Text = "Gott námskeið",
					TeacherSSN = null,
					Value = 0
				},
				new EvaluationAnswer
				{
					ID = 5,
					QuestionID = 2,
					CourseInstanceID = 20435,
					EvaluationInstanceID = 1,
					Text = "Bara nokkuð gott",
					TeacherSSN = null,
					Value = 0
				},
				#endregion

				#region Evaluation instance 2 - answers:
				new EvaluationAnswer
				{
					ID = 6,
					QuestionID = 6,
					CourseInstanceID = 20435,
					EvaluationInstanceID = 2,
					Text = "",
					TeacherSSN = "1203735289",
					Value = 4
				},
				new EvaluationAnswer
				{
					ID = 7,
					QuestionID = 7,
					CourseInstanceID = 20435,
					EvaluationInstanceID = 2,
					Text = "smu",
					TeacherSSN = "1203735289",
					Value = 0
				},
				#endregion
			};
			mockUnitOfWork.SetRepositoryData(_answers);
			#endregion

			#region Mock data - evaluation replies
			// Three students have replied to two
			// evaluation instances; i.e. one student
			// has answered them both.
			_replies = new List<EvaluationReply>
			{
				new EvaluationReply
				{
					ID = 1,
					EvaluationInstanceID = 1,
					DateAdded = DateTime.Now.AddDays(-7),
					StudentSSN = "1234567890"
				},
				new EvaluationReply
				{
					ID = 2,
					EvaluationInstanceID = 1,
					DateAdded = DateTime.Now.AddDays(-6),
					StudentSSN = "2234567890"
				},
				new EvaluationReply
				{
					ID = 3,
					EvaluationInstanceID = 1,
					DateAdded = DateTime.Now.AddDays(-5),
					StudentSSN = "3234567890"
				},
				new EvaluationReply
				{
					ID = 4,
					EvaluationInstanceID = 2,
					DateAdded = DateTime.Now.AddDays(-2),
					StudentSSN = "3234567890"
				},
			};
			mockUnitOfWork.SetRepositoryData(_replies);
			#endregion

			#region Mock data from the factory
			mockUnitOfWork.SetRepositoryData(_mockFactory.GetMockData<User>());
			mockUnitOfWork.SetRepositoryData(_mockFactory.GetMockData<CourseInstance>());
			mockUnitOfWork.SetRepositoryData(_mockFactory.GetMockData<CourseInstanceStudent>());
			mockUnitOfWork.SetRepositoryData(_mockFactory.GetMockData<TeachersRegistration>());
			#endregion

			_service = new EvaluationsServiceProvider(mockUnitOfWork);
		}
		#endregion

		#region Test methods

		[TestMethod]
		public void EvaluationsTestGetTemplates()
		{
			// Arrange:

			// Act:
			var result = _service.GetEvaluationTemplates();

			// Assert:
			Assert.AreEqual(3, result.Count(), "Result doesn't contain the correct number of evaluation templates");
			// Not really much more we can assert, since this method only 
			// returns a list of simple objects.
		}

		[TestMethod]
		public void EvaluationsTestGetByIdWithNoTeacherQuestions()
		{
			// Arrange:
			const int templateId = 1;

			// Act:
			var result = _service.GetEvaluationTemplateById(templateId);

			// Assert:
			Assert.IsNotNull(result);
			Assert.AreEqual(templateId, result.ID);
			Assert.AreEqual(3, result.CourseQuestions.Count());
			Assert.AreEqual(0, result.TeacherQuestions.Count());
			Assert.AreEqual(5, result.CourseQuestions[0].Answers.Count(), "The answers to an options question are missing");
			Assert.AreEqual("single", result.CourseQuestions[0].Type, "type of question not correctly set");
			Assert.AreEqual("text", result.CourseQuestions[1].Type, "type of question not correctly set");
			Assert.AreEqual("text", result.CourseQuestions[2].Type, "type of question not correctly set");
		}

		[TestMethod]
		public void EvaluationsTestGetByIdWithBothCourseAndTeacherQuestions()
		{
			// Arrange:
			const int templateId = 2;

			// Act:
			var result = _service.GetEvaluationTemplateById(templateId);

			// Assert:
			Assert.IsNotNull(result);
			Assert.AreEqual(templateId, result.ID);
			Assert.AreEqual(2, result.CourseQuestions.Count());
			Assert.AreEqual(2, result.TeacherQuestions.Count());
			Assert.AreEqual(5, result.CourseQuestions[0].Answers.Count());
			Assert.AreEqual(5, result.TeacherQuestions[0].Answers.Count());

			// In the test data for this template, the questions are defined 
			// in a different order from which they should appear. We
			// test this of course:
			// TODO: the DTO classes don't define the order, maybe we should change that?
		}

		[TestMethod]
		public void EvaluationsTestGetEmptyTemplate()
		{
			// Arrange:
			const int templateId = 3;

			// Act:
			var result = _service.GetEvaluationTemplateById(templateId);

			// Assert:
			Assert.IsNotNull(result);
			Assert.AreEqual(templateId, result.ID);
			Assert.AreEqual(0, result.CourseQuestions.Count());
			Assert.AreEqual(0, result.TeacherQuestions.Count());
		}

		[TestMethod]
		public void EvaluationsTestGetTemplateByInvalidID()
		{
			// Arrange:
			const int templateId = 7;

			// Act:
			var result = _service.GetEvaluationTemplateById(templateId);

			// Assert:
			Assert.IsNull(result);
		}

		[TestMethod]
		public void EvaluationsAddTemplate()
		{
			// The arrange section is quite large here...
			#region Arrange:
			// Note: since we're adding data, we create a separate
			// mockUOW and a separate service object.
			var mockUow = new MockUnitOfWork<MockDataContext>();
			var templates = new List<EvaluationTemplate>();
			var questions = new List<EvaluationQuestion>();
			var questionValues = new List<EvaluationQuestionValue>();
			mockUow.SetRepositoryData(templates);
			mockUow.SetRepositoryData(questions);
			mockUow.SetRepositoryData(questionValues);
			var service = new EvaluationsServiceProvider(mockUow);

			var template = new EvaluationTemplateDTO
			{
				ID = 20,
				IntroTextIS = "Nýr upphafstexti",
				IntroTextEN = "New intro text",
				TitleIS = "Nýtt miðannarmat",
				TitleEN = "New midterm evaluation",
				CourseQuestions = new List<EvaluationQuestionDTO>
				{
					new EvaluationQuestionDTO
					{
						ImageURL = "http://smu.org",
						TextEN = "What do you say about this?",
						TextIS = "Hvað hefurðu um þetta að segja?",
						Type = "text"
					}
				},
				TeacherQuestions = new List<EvaluationQuestionDTO>
				{
					new EvaluationQuestionDTO
					{
						ImageURL = "http://smu.edu",
						TextEN = "And now for something completely different",
						TextIS = "Og nú að einhverju allt öðru.",
						Type = "single",
						Answers = new List<EvaluationQuestionAnswerDTO>
						{
							new EvaluationQuestionAnswerDTO
							{
								TextEN = "Yes",
								TextIS = "Já",
								Weight = 1
							},
							new EvaluationQuestionAnswerDTO
							{
								TextEN = "No",
								TextIS = "Nei",
								Weight = 2
							}
						}
					},
					new EvaluationQuestionDTO
					{
						ImageURL = "http://smu.edu",
						TextEN = "Yet something completely different",
						TextIS = "Ennþá meira öðruvísi",
						Type = "multiple",
						Answers = new List<EvaluationQuestionAnswerDTO>
						{
							new EvaluationQuestionAnswerDTO
							{
								TextEN = "Something",
								TextIS = "Eitthvað",
								Weight = 1
							},
							new EvaluationQuestionAnswerDTO
							{
								TextEN = "Different",
								TextIS = "Öðruvísi",
								Weight = 2
							}
						}
					}
				}
			};
			#endregion

			// Act:
			service.AddEvaluationTemplate(template);

			// Assert:
			Assert.AreEqual(1, templates.Count());
			Assert.AreEqual(templates[0].NameIS, template.TitleIS);
			Assert.AreEqual(templates[0].NameEN, template.TitleEN);
			Assert.AreEqual(templates[0].IntroTextEN, template.IntroTextEN);
			Assert.AreEqual(templates[0].IntroTextIS, template.IntroTextIS);

			// There are three questions in this new template:
			Assert.AreEqual(3, questions.Count());
			// The first one should be of type text:
			Assert.AreEqual(EvaluationQuestion.TYPE_TEXT, questions[0].TypeID);
			// ... the second one should be a single-choice option question:
			Assert.AreEqual(EvaluationQuestion.TYPE_OPTIONS_SINGLE, questions[1].TypeID);
			// ... and the third one should be a multiple-choice option question:
			Assert.AreEqual(EvaluationQuestion.TYPE_OPTIONS_MULTIPLE, questions[2].TypeID);

			//... and total of 4 question values:
			Assert.AreEqual(4, questionValues.Count());
		}

		[TestMethod]
		[ExpectedException(typeof(ServiceValidationException))]
		public void EvaluationsAddInvalidTemplateNoTitleEN()
		{
			// Arrange:
			var template = new EvaluationTemplateDTO
			{
				TitleEN = "",
				TitleIS = "Nú er gaman",
				IntroTextEN = "Gaman",
				IntroTextIS = "meira gaman"
			};

			// Act:
			_service.AddEvaluationTemplate(template);

			// Since we expect an exception to be thrown, there is no assert here...
			//... although we would like to assert that nothing has been added
			// to our mock collection of templates...
		}

		[TestMethod]
		[ExpectedException(typeof(ServiceValidationException))]
		public void EvaluationsAddInvalidTemplateNoTitleIS()
		{
			// Arrange:
			var template = new EvaluationTemplateDTO
			{
				TitleEN = "And now for something completely different",
				TitleIS = "",
				IntroTextEN = "Gaman",
				IntroTextIS = "meira gaman"
			};

			// Act:
			_service.AddEvaluationTemplate(template);

			// Since we expect an exception to be thrown, there is no assert here...
			//... although we would like to assert that nothing has been added
			// to our mock collection of templates...
		}

		[TestMethod]
		[ExpectedException(typeof(ServiceValidationException))]
		public void EvaluationsAddInvalidTemplateNoIntroIS()
		{
			// Arrange:
			var template = new EvaluationTemplateDTO
			{
				TitleEN = "And now for something completely different",
				TitleIS = "Titill",
				IntroTextEN = "Gaman",
				IntroTextIS = ""
			};

			// Act:
			_service.AddEvaluationTemplate(template);

			// Since we expect an exception to be thrown, there is no assert here...
			//... although we would like to assert that nothing has been added
			// to our mock collection of templates...
		}

		[TestMethod]
		[ExpectedException(typeof(ServiceValidationException))]
		public void EvaluationsAddInvalidTemplateNoIntroEN()
		{
			// Arrange:
			var template = new EvaluationTemplateDTO
			{
				TitleEN = "And now for something completely different",
				TitleIS = "Titill",
				IntroTextEN = "",
				IntroTextIS = "Gaman"
			};

			// Act:
			_service.AddEvaluationTemplate(template);

			// Since we expect an exception to be thrown, there is no assert here...
			//... although we would like to assert that nothing has been added
			// to our mock collection of templates...
		}

		[TestMethod]
		public void EvaluationsAddInstance()
		{
			// Arrange:
			var mockUnitOfWork = new MockUnitOfWork<MockDataContext>();
			var templates = new List<EvaluationTemplate>
			{
				// A simple mock template with no questions...
				new EvaluationTemplate
				{
					ID = 1
				}

			};
			var instances = new List<EvaluationInstance>();
			mockUnitOfWork.SetRepositoryData(instances);
			mockUnitOfWork.SetRepositoryData(templates);
			var service = new EvaluationsServiceProvider(mockUnitOfWork);

			var inst = new NewEvaluationDTO
			{
				TemplateID = 1,
				StartDate = DateTime.Now.AddDays(4),
				EndDate = DateTime.Now.AddDays(7)
			};

			// Act:
			service.AddEvaluation(inst);

			// Assert:
			Assert.AreEqual(1, instances.Count());
		}

		[TestMethod]
		[ExpectedException(typeof(ServiceValidationException))]
		public void EvaluationsAddInstanceInvalidDates()
		{
			// Arrange:
			var inst = new NewEvaluationDTO
			{
				TemplateID = 1,
				StartDate = DateTime.Now.AddDays(7), // Invalid dates,
				EndDate = DateTime.Now.AddDays(4)    // start should precede end
			};

			// Act:
			_service.AddEvaluation(inst);

			// Assert:
		}

		[TestMethod]
		[ExpectedException(typeof(ServiceValidationException))]
		public void EvaluationsAddInstanceInvalidTemplateID()
		{
			// Arrange:
			var inst = new NewEvaluationDTO
			{
				TemplateID = 7, // Not present in test data
				StartDate = DateTime.Now.AddDays(4), // Valid dates
				EndDate = DateTime.Now.AddDays(7)
			};

			// Act:
			_service.AddEvaluation(inst);

			// Assert:
		}

		[TestMethod]
		public void EvaluationsGetEvaluationsForUser()
		{
			// Arrange:
			const string userName = "loa";

			// Act:
			var result = _service.GetEvaluationsForUser(userName);

			// Assert:
			// This user has one course, and there is only one open evaluation,
			// so there should be only one result:

			Assert.AreEqual(1, result.Count());
		}

		[TestMethod]
		public void EvaluationsGetEvaluationsInstances()
		{
			// Arrange:

			// Act:
			var list = _service.GetEvaluations().ToList();

			// Assert:
			Assert.AreEqual(3, list.Count());
			Assert.AreEqual("closed", list[0].Status );
			Assert.AreEqual("open", list[1].Status);
			Assert.AreEqual("new", list[2].Status);
		}

		[TestMethod]
		public void EvaluationsGetCourseResultsWithNoTeacherQuestions()
		{
			// Arrange:
			const int evalID = 1;

			// Act:
			var result = _service.GetEvaluationResultsByCourse(evalID);

			// Assert:
			Assert.IsNotNull(result);

			// There have been given answers in a single course:
			Assert.AreEqual(1, result.Courses.Count());
			var courseResult = result.Courses[0];
			// There are 3 questions in this template:
			Assert.AreEqual(3, courseResult.Questions.Count());
			// The results of the first question is of type "single",
			// and therefore should contain no text results:
			Assert.IsNull(courseResult.Questions[0].TextResults);
			Assert.IsNotNull(courseResult.Questions[0].OptionsResults, "OptionsResult should not be null, but is.");
			// Assert.IsNotNull(courseResult.Questions[0].OptionsResults);
		}

		[TestMethod]
		public void EvaluationsGetCourseResultsWithTeacherQuestionsAndCourseQuestions()
		{
			// Arrange:
			const int evalID = 2;

			// Act:
			var result = _service.GetEvaluationResultsByCourse(evalID);

			// Assert:
			Assert.IsNotNull(result);

			// There have been given answers in a single course:
			Assert.AreEqual(1, result.Courses.Count());
			var courseResults = result.Courses[0];

			// The first question is a course question of type multiple:
			Assert.AreEqual("multiple", courseResults.Questions[0].Type, "Question 1 is not of the correct type");
			// There are no answers for this question in the test data.

			// The second question is a course question of type single:
			Assert.AreEqual("single", courseResults.Questions[1].Type, "Question 2 is not of the correct type");
			// There are no answers for this question in the test data.

			// The third question is a teacher question of type single:
			Assert.AreEqual("single", courseResults.Questions[2].Type, "Question 3 is not of the correct type");
			Assert.IsNotNull(courseResults.Questions[2].OptionsResults);

			// The fourth question is a teacher question of type text:
			Assert.IsNotNull(courseResults.Questions[3].TextResults);
			Assert.AreEqual(1, courseResults.Questions[3].TextResults.Count());
			Assert.AreEqual("text", courseResults.Questions[3].Type, "Question 4 is not of the correct type");

			// We could benefit from more asserts on the results here...
		}

		[TestMethod]
		[ExpectedException(typeof(ServiceValidationException))]
		public void EvaluationsGetCourseResultsInvalidEvaluationID()
		{
			// Arrange:
			const int evalID = 7;

			// Act:
			_service.GetEvaluationResultsByCourse(evalID);

			// Assert:
			// No assert, since an exception should be thrown...
		}

		[TestMethod]
		public void EvaluationsGetEvaluationInCourse()
		{
			// Arrange:
			const string courseID = "T-109-INTO";
			const string semester = "20141";
			const int evalID = 1;

			// Act:
			var result = _service.GetEvaluationInCourse(courseID, semester, evalID);

			// Assert:
			// This instance is based on the first template,
			// which has 3 questions, all targeted towards the course:
			Assert.AreEqual(evalID, result.ID);
			Assert.AreEqual(3, result.CourseQuestions.Count());
			Assert.AreEqual(0, result.TeacherQuestions.Count());
		}

		[TestMethod]
		public void EvaluationsTestAddAnswersFromStudentToEvaluation()
		{
			// Arrange:
			// Create our local repositories which will get
			// modified during the Act:
			var mockUow = new MockUnitOfWork<MockDataContext>();
			var answers = new List<EvaluationAnswer>();
			var replies = new List<EvaluationReply>();
			mockUow.SetRepositoryData(answers);
			mockUow.SetRepositoryData(replies);
			// Then, supply the rest of the (necessary) data:
			mockUow.SetRepositoryData(_templates);
			mockUow.SetRepositoryData(_questions);
			mockUow.SetRepositoryData(_questionValues);
			mockUow.SetRepositoryData(_instances);
			mockUow.SetRepositoryData(_mockFactory.GetMockData<User>());
			mockUow.SetRepositoryData(_mockFactory.GetMockData<CourseInstance>());
			var service = new EvaluationsServiceProvider(mockUow);

			const string courseID = "T-109-INTO";
			const string semester = "20141";
			const int evalID = 1;
			const string strUserName = "loa";
			var answersDTO = new List<EvaluationAnswerDTO>
			{
				new EvaluationAnswerDTO
				{
					QuestionID = 1,
					TeacherSSN = "",
					Value = "5"
				},
				new EvaluationAnswerDTO
				{
					QuestionID = 1,
					TeacherSSN = "",
					Value = "5"
				}
			};

			// Act:
			service.AddAnswersFromStudentToEvaluation(courseID, semester, evalID, answersDTO, strUserName);

			// Assert:
			Assert.AreEqual(1, replies.Count());
			Assert.AreEqual(replies[0].DateAdded.Date, DateTime.Now.Date);
			// Assert.AreEqual(replies[0].StudentSSN, ); - TODO: verify SSN is correct...
			Assert.AreEqual(replies[0].EvaluationInstanceID, evalID);

			Assert.AreEqual(2, answers.Count());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void EvaluationsTestAddAnswersFromStudentInvalidUsername()
		{
			// Arrange:
			const string courseID = "T-109-INTO";
			const string semester = "20141";
			const int evalID = 1;
			const string strUserName = "smu";
			var answersDTO = new List<EvaluationAnswerDTO>
			{
				new EvaluationAnswerDTO()
			};

			// Act:
			_service.AddAnswersFromStudentToEvaluation(courseID, semester, evalID, answersDTO, strUserName);
		}

		[TestMethod]
		[ExpectedException(typeof(ServiceValidationException))]
		public void EvaluationsTestAddAnswersFromStudentInvalidCourseID()
		{
			// Arrange:
			const string courseID = "T-427-WEPO"; // Not in our test data
			const string semester = "20141";
			const int evalID = 1;
			const string strUserName = "loa";
			var answersDTO = new List<EvaluationAnswerDTO>
			{
				new EvaluationAnswerDTO()
			};

			// Act:
			_service.AddAnswersFromStudentToEvaluation(courseID, semester, evalID, answersDTO, strUserName);
		}
		#endregion
	}
}
