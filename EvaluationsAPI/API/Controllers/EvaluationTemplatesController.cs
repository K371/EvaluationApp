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
	/// EvaluationTemplatesController handles all evaluation templates
	/// and requests relating to them. 
	/// </summary>
	[RoutePrefix("api/v1/evaluationtemplates")]
	[Authorize]
	public class EvaluationTemplatesController : ApiController
	{
		#region Member variables
		private readonly EvaluationsServiceProvider _evaluationsService;
		#endregion

		#region Constructor
		/// <summary>
		/// C'tor
		/// </summary>
		/// <param name="uow"></param>
		public EvaluationTemplatesController(IUnitOfWork uow)
		{
			_evaluationsService = new EvaluationsServiceProvider(uow);
		}
		#endregion

		#region Methods
		/// <summary>
		/// Returns a list of all evaluation templates.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route]
		public IEnumerable<EvaluationTemplateListDTO> Get()
		{
			return _evaluationsService.GetEvaluationTemplates();
		}

		/// <summary>
		/// Returns an evaluation template by id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("{id}")]
		public EvaluationTemplateDTO Get(int id)
		{
			var item = _evaluationsService.GetEvaluationTemplateById(id);
			if (item == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}
			return item;
		}

		/// <summary>
		/// Creates a new evaluation template.
		/// </summary>
		/// <param name="evaluationTemplate"></param>
		[HttpPost]
		[Route]
		public void Post([FromBody]EvaluationTemplateDTO evaluationTemplate)
		{
			try
			{
				_evaluationsService.AddEvaluationTemplate(evaluationTemplate);
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
