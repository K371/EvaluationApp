using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models.DTO.Evaluations;
using API.Models.DTO.Users;
using API.Services.Exceptions;
using API.Services.Repositories;
using API.Services.Services;
using API.Services.Helpers;

namespace API.Controllers
{
	/// <summary>
	/// API Controller for Courses
	/// </summary>
	[RoutePrefix("api/v1/courses")]
	[Authorize]
	public class CoursesController : ApiController
	{
		#region Member variables
		private readonly CoursesServiceProvider _service;
		private readonly EvaluationsServiceProvider _evaluationsService;
		#endregion

		#region Constructor
		/// <summary>
		/// C'tor
		/// </summary>
		/// <param name="uow"></param>
		public CoursesController(IUnitOfWork uow)
		{
			_service = new CoursesServiceProvider(uow);
			_evaluationsService = new EvaluationsServiceProvider(uow);
		}

		#endregion

		#region Public functions
		/// <summary>
		/// Returns a list of all teachers registered in a given course.
		/// Each entry in the list contains the teacher object, plus what role each
		/// teacher plays in the given course.
		/// </summary>
		/// <example>/api/v1/courses/{course}/{semester}/teachers</example>
		/// <param name="course">ID of a course</param>
		/// <param name="semester">Semester</param>
		/// <returns>List of teachers</returns>
		/// <method>GET</method>
		/// <group>Courses</group>
		[HttpGet]
		[Route("{course}/{semester}/teachers")]
		public IEnumerable<UserDTO> GetTeachersInCourseOnSemester(string course, string semester)
		{
			course = Helper.ParseCourseInstance(course);
			//Get all teachers for course instance:
			return _service.GetTeachersForCourse(course);
		}

		/// <summary>
		/// Returns a given evaluation in the given course.
		/// </summary>
		/// <param name="course"></param>
		/// <param name="semester"></param>
		/// <param name="evalID"></param>
		[HttpGet]
		[Route("{course}/{semester}/evaluations/{evalID}")]
		public CourseEvaluationDTO GetEvaluationInCourse(string course, string semester, int evalID)
		{
			course = Helper.ParseCourseInstance(course);
			var item = _evaluationsService.GetEvaluationInCourse(course, semester, evalID);
			if (item == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}
			return item;
		}

		/// <summary>
		/// Saves answers from the currently logged-in user in an evaluation
		/// in a given course.
		/// </summary>
		/// <param name="course"></param>
		/// <param name="semester"></param>
		/// <param name="evalID"></param>
		/// <param name="answers"></param>
		[HttpPost]
		[Route("{course}/{semester}/evaluations/{evalID}")]
		public void AddAnswersFromStudentToEvaluation(string course, string semester, int evalID,
													  [FromBody] List<EvaluationAnswerDTO> answers)
		{
			try
			{
				var strUserName = "arnars12"; // Pick a random user until User.Identity.Name is sorted out
				_evaluationsService.AddAnswersFromStudentToEvaluation(course, semester, evalID, answers, strUserName);
			}
			catch (ServiceValidationException ex)
			{
				// A validation error occurred:
				throw new HttpResponseException(new HttpResponseMessage
				{
					ReasonPhrase = ex.Message,
					StatusCode = HttpStatusCode.BadRequest
				});
			}
			catch (Exception ex)
			{
				// Some error, we can't really tell what went wrong...
				// .. a proper implementation would of course log this!!!
				throw new HttpResponseException(new HttpResponseMessage
				{
					ReasonPhrase = ex.Message,
					StatusCode = HttpStatusCode.InternalServerError
				});
			}
		}

		#endregion
	}
}
