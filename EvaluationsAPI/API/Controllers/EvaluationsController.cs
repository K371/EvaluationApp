using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models.DTO.Evaluations;
using API.Services.Exceptions;
using API.Services.Repositories;
using API.Services.Services;

namespace API.Controllers
{
	/// <summary>
	/// EvaluationsController handles requests relating to
	/// the evaluations themselves, i.e. those which the admin
	/// must have access to. Request used by teachers/students
	/// are located elsewhere (i.e. in the context of a given course).
	/// </summary>
	[Authorize]
	[RoutePrefix("api/v1/evaluations")]
	public class EvaluationsController : ApiController
	{
		#region Member variables
		private readonly EvaluationsServiceProvider _evaluationService;
		#endregion

		#region Constructor
		/// <summary>
		/// C'tor
		/// </summary>
		/// <param name="uow"></param>
		public EvaluationsController(IUnitOfWork uow)
		{
			_evaluationService = new EvaluationsServiceProvider(uow);
		}
		#endregion

		#region Methods
		/// <summary>
		/// Returns all evaluations (open, closed etc.).
		/// Can be filtered based on status.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("")]
		public IEnumerable<EvaluationListDTO> Get()
		{
			return _evaluationService.GetEvaluations();
		}

		/// <summary>
		/// Returns the results of a given evaluation.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("{id}")]
		public EvaluationDTO Get(int id)
		{
			var item = _evaluationService.GetEvaluationResultsByCourse(id);
			if (item == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}
			return item;
		}

		/// <summary>
		/// Adds a new evaluation.
		/// </summary>
		/// <param name="evaluation"></param>
		[HttpPost]
		[Route("")]
		public void Post(NewEvaluationDTO evaluation)
		{
			try
			{
				_evaluationService.AddEvaluation(evaluation);
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
			catch(Exception ex)
			{
				// Some error, we can't really tell what went wrong...
				// .. a proper implementation would of course log this!!!
				throw new HttpResponseException( new HttpResponseMessage
				{
					ReasonPhrase = ex.Message,
					StatusCode = HttpStatusCode.InternalServerError
				});
			}
		}
		#endregion
	}
}
