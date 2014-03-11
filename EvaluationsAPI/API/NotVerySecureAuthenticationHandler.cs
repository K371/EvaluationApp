using System;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace API
{
	/// <summary>
	/// A simple POCO class which represents user credentials,
	/// i.e. a username and a password.
	/// </summary>
	public class Credentials
	{
		/// <summary>
		/// The username.
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// The password.
		/// </summary>
		public string Password { get; set; }
	}

	/// <summary>
	/// A simple authentication handler which assumes that 
	/// the authorization header of a request is
	/// </summary>
	public class NotVerySecureAuthenticationHandler : DelegatingHandler
	{
		private const string Basic = "Basic";

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var credentials = GetCredentialsFromAuthorizationHeader(request);
			if (credentials != null)
			{
				// Here we should validate that the credentials
				// are valid, but since our implementation is
				// not really meant to be very secure, we cheat
				// by NOT looking up the given user/pass combo here.
				var identity = new GenericIdentity(credentials.Username, Basic);
				var genericPrincipal = new GenericPrincipal(identity, null);

				HttpContext.Current.User = genericPrincipal;
				Thread.CurrentPrincipal  = genericPrincipal;
			}

			return base.SendAsync(request, cancellationToken);
		}

		private static Credentials GetCredentialsFromAuthorizationHeader(HttpRequestMessage request)
		{
			var authenticationHeaderValue = request.Headers.Authorization;

			if (authenticationHeaderValue != null && !String.IsNullOrEmpty(authenticationHeaderValue.Scheme)
				&& !String.IsNullOrEmpty(authenticationHeaderValue.Parameter)
				&& authenticationHeaderValue.Scheme.Equals(Basic))
			{
				var authValue =
					System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authenticationHeaderValue.Parameter));
				var authParts = authValue.Split(':');

				if (authParts.Length == 2)
				{
					return new Credentials { Username = authParts[0], Password = authParts[1] };
				}
			}
			return null;
		}
	}
}