using System;

namespace API.Services.Exceptions
{
	public class CourseException : ApplicationException
	{
		public string Messages { get; set; }
		public Exception Ex { get; set; }

		public CourseException()
		{
			//Empty
		}

		public CourseException(string message)
		{
			Messages = message;
		}

		public CourseException(Exception ex)
		{
			Ex = ex;
		}

		public CourseException(string message, Exception ex)
		{
			Messages = message;
			Ex = ex;
		}
	}
}
