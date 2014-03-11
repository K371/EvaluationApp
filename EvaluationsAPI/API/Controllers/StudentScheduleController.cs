using API.Services.Services;
using API.Services.Repositories;
using API.Services.ServiceModels.DTO;
using System;
using System.Linq;
using System.Web.Http;
using System.Collections.Generic;


namespace API.Controllers
{
    #region ClassHelper
    /// <summary>
    /// To filter the search results
    /// </summary>
    public class ScheduleFilter
    {
        /// <summary>
        /// String representing date
        /// Example: range=2013-05-12T08:30:00,2013-05-12T10:20:00
        /// </summary>
        public string range { get; set; }

    }
    #endregion

    /// <summary>
	/// StudentScheduleController handles tasks relating to the
	/// schedule of an individual student.
	/// </summary>
    public class StudentScheduleController : ApiController
	{
		#region Member variables
		private readonly StudentsServiceProvider _service;
		#endregion

		#region Constructor
		/// <summary>
		/// C'tor
		/// </summary>
		/// <param name="uow"></param>
		public StudentScheduleController(IUnitOfWork uow)
		{
			_service = new StudentsServiceProvider(uow);
		}
		#endregion

		#region HTTP Methods
        /// <summary>
		/// Returns the schedule for a given student.
		/// Like other schedules, this method can return the schedule
		/// in iCal format, if the request contains Content-Type = "text/calendar".
		/// </summary>
		/// <param name="ssn">Required</param>
        /// <param name="range">Optional</param>
		/// <example>/api/v1/students/{ssn}/schedule/</example>
		/// <group>Students</group>
		/// <method>GET</method>
		/// <returns></returns>
		public IEnumerable<BookingDTO> Get(string ssn, [FromUri]ScheduleFilter filter)
		{
            /*var range = DateTimeRange.Parse(filter.range);
            return _service.GetStudentSchedule(ssn, range);*/
            // not yet implemeneted
            return null;
		}

		#endregion
	}
}
