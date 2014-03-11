using System.Collections.Generic;
using System.Linq;
using API.Models.DTO.Courses;
using API.Models.DTO.Users;
using API.Services.Models.Entities.Courses;
using API.Services.Models.Entities.Customers;
using API.Services.Models.Entities.General;
using API.Services.Repositories;

namespace API.Services.Services
{
	/// <summary>
	/// CoursesServiceProvider provides the business logic for courses in the application
	/// </summary>
	public class CoursesServiceProvider
	{
		#region Member variables
		private readonly IRepository<CourseInstance> _courseInstances;
		private readonly IRepository<CourseInstanceStudent> _courseInstanceStudents;
		private readonly IRepository<TeachersRegistration> _teacherRegistrations;
		private readonly IRepository<User> _users;
		#endregion

		#region Constructors
		public CoursesServiceProvider(IUnitOfWork uow)
		{
			_courseInstances        = uow.GetRepository<CourseInstance>();
			_courseInstanceStudents = uow.GetRepository<CourseInstanceStudent>();
			_teacherRegistrations   = uow.GetRepository<TeachersRegistration>();
			_users                  = uow.GetRepository<User>();
		}
		#endregion

		#region Public functions
		/// <summary>
		/// Gets a list of teachers in a course on a semester
		/// </summary>
		/// <param name="course">ID of a course</param>
		/// <returns>List of teachers in a course on a semester</returns>
		public List<UserDTO> GetTeachersForCourse(string course)
		{
			var courseID = ((from d in _courseInstances.All()
							 where d.CourseID == course
							 select d.ID).FirstOrDefault());

			var result = (from c in _teacherRegistrations.All()
			              join s in _users.All() on c.SSN equals s.SSN
			              where c.CourseID == courseID
			              select new UserDTO
			              {
				              SSN = s.SSN,
				              Email = s.Email,
				              FullName = s.FullName,
				              Role = "teacher",
				              Username = s.UserName
			              }).ToList();

			foreach (var teacher in result)
			{
				teacher.ImageURL = "http://www.ru.is/kennarar/dabs/img/" + teacher.SSN.Substring(0, 2) + "/" + teacher.SSN + ".jpg";
			}
			return result;
		}

		/// <summary>
		/// Returns all courses being taken by a student on a given semester.
		/// If no semester is specified, the current semester is assumed.
		/// Note: this will also return courses the student registered
		/// but then unregistered from! We need to take this into account.
		/// </summary>
		/// <param name="strUserName">The SSN of the student.</param>
		/// <returns></returns>
		public IEnumerable<CourseInstanceDTO> GetCoursesForStudent(string strUserName)
		{
			var result = (from c in _courseInstances.All()
						  join x in _courseInstanceStudents.All()
			                on c.ID equals x.CourseInstanceID
						  join u in _users.All() on x.SSN equals u.SSN
			              where u.UserName == strUserName
			              select new CourseInstanceDTO
			              {
				              CourseID = c.CourseID,
							  DateBegin = c.DateBegin,
							  DateEnd = c.DateEnd,
							  ID = c.ID,
							  NameEN = c.NameEN,
							  NameIS = c.NameIS
			              }).ToList();

			return result;
		}

		#endregion
	}
}
