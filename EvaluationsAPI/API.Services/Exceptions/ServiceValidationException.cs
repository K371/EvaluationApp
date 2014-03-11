using System;

namespace API.Services.Exceptions
{
	public class ServiceValidationException : ApplicationException
	{
		public ServiceValidationException(string field):base(field)
		{
		}
	}
}
