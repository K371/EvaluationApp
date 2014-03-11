using System.Collections.Generic;
using System.Web.Http;
using API.Models.DTO.Courses;
using API.Models.DTO.Evaluations;
using API.Services.Repositories;
using API.Services.Services;

namespace API.Controllers
{
	/// <summary>
	/// MyController represents the base URL for a logged-in user.
	/// It will handle requests such as /api/v1/my/schedule, /api/v1/my/courses
	/// etc.
	/// </summary>
	[Authorize]
	[RoutePrefix("api/v1/my")]
	public class MyController : ApiController
	{
		#region Member variables
		private readonly CoursesServiceProvider _courseService;
		private readonly EvaluationsServiceProvider _evaluationService;
		#endregion

		#region Constructor
		/// <summary>
		/// C'tor
		/// </summary>
		/// <param name="uow"></param>
		public MyController(IUnitOfWork uow)
		{
			_courseService = new CoursesServiceProvider(uow);
			_evaluationService = new EvaluationsServiceProvider(uow);
		}
		#endregion

		#region Public functions

		/// <summary>
		/// Returns all courses which the logged in user is registered in.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("courses")]
		public IEnumerable<CourseInstanceDTO> Courses()
		{
			// var strUserName = User.Identity.Name;
			var strUserName = "arnars12"; // Pick a random user until User.Identity.Name is sorted out
			return _courseService.GetCoursesForStudent(strUserName);
		}

		/// <summary>
		/// Returns all evaluations the given student has
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("evaluations")]
		public IEnumerable<StudentEvaluationListDTO> Evaluations()
		{
			// var strUserName = User.Identity.Name;
			var strUserName = "arnars12"; // Pick a random user until User.Identity.Name is sorted out
			return _evaluationService.GetEvaluationsForUser(strUserName);
		}
		#endregion
	}
}
