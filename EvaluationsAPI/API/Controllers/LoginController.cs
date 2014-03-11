using System;
using System.Net;
using System.Text;
using System.Web.Http;
using API.Models.DTO.Users;
using API.Services.Repositories;
using API.Services.Services;

namespace API.Controllers
{
	/// <summary>
	/// LoginData is a simple wrapper for username/password
	/// </summary>
	public class LoginData
	{
// ReSharper disable InconsistentNaming
		/// <summary>
		/// The username of a user
		/// </summary>
		public string user { get; set; }

		/// <summary>
		/// The password of a user
		/// </summary>
		public string pass { get; set; }
// ReSharper restore InconsistentNaming
	}

	/// <summary>
	/// 
	/// </summary>
	[RoutePrefix("api/v1/login")]
	public class LoginController : ApiController
	{
		#region Member variables
		private readonly UsersServiceProvider _usersService;
		#endregion

		#region Constructor
		/// <summary>
		/// C'tor
		/// </summary>
		/// <param name="uow"></param>
		public LoginController(IUnitOfWork uow)
		{
			_usersService = new UsersServiceProvider(uow);
		}
		#endregion

		#region Methods
		/// <summary>
		/// Logs the current user in. If the login succeeds, this method
		/// returns HTTP 200, but if it fails it will return HTTP 401 (Unauthorized).
		/// </summary>
		/// <param name="loginData"></param>
		[HttpPost]
		[Route]
		public LoginResponseDTO Post(LoginData loginData)
//		public LoginResponseDTO Post(string user, string pass)
		{
			/*
			var loginData = new LoginData
			{
				user = user,
				pass = pass
			};
			*/

			var u = _usersService.Login(loginData.user, loginData.pass);
			if (u != null)
			{
				var unencoded = loginData.user + ":" + loginData.pass;
				var authBytes = Encoding.UTF8.GetBytes(unencoded.ToCharArray());

				return new LoginResponseDTO
				{
					User = u,
					Token = Convert.ToBase64String(authBytes)
				};
			}
			throw new HttpResponseException(HttpStatusCode.Unauthorized);
		}
		#endregion
	}
}
