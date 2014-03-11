using API.Services.Models.Entities.Courses;
using API.Services.Models.Entities.Customers;
using API.Services.Models.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Tests.MockObjects
{
	/// <summary>
	/// Class that returns mock data
	/// </summary>
	public class MockFactory
	{
		private readonly Dictionary<Type, object> _repositories;

		/// <summary>
		/// Constructor for mock factory
		/// </summary>
		public MockFactory()
		{
			_repositories = new Dictionary<Type, object>();

			#region Mock data - course instances:
			var courseInstancesList = new List<CourseInstance>
			{
				// Exchange Program course
				new CourseInstance
				{
					ID = 22971,
					NameEN = "Exchange program",
					NameIS = "Skiptinám",
					CourseID = "X-699-EXCH",
					DateBegin = new DateTime(2012, 1, 1),
					DateEnd = new DateTime(2012, 5, 31),
				},

				new CourseInstance
				{
					ID = 20435,
					NameEN = "Inngangur að tölvunarfræði",
					NameIS = "Introduction to computer science",
					CourseID = "T-109-INTO",
					DateBegin = new DateTime(2010, 9, 9),
				},

				new CourseInstance
				{
					ID = 20436,
					NameEN = "Verkefnalausnir",
					NameIS = "Problem solving",
					CourseID = "T-110-VERK",
					DateBegin = new DateTime(2010, 9, 9),
				},

				new CourseInstance
				{
					ID = 20437,
					CourseID = "T-111-PROG",
					NameEN = "Forritun",
					NameIS = "Programming",
					DateBegin = new DateTime(2010, 9, 9),
				},

				new CourseInstance
				{
					ID = 20438,
					NameEN = "Stærðfræði",
					NameIS = "Math",
					CourseID = "T-117-STR1",
					DateBegin = new DateTime(2010, 9, 9),
				},

				new CourseInstance
				{
					ID = 20434,
					NameEN = "Tölvuhögun",
					NameIS = "Computer architecture",
					CourseID = "T-107-TOLH",
					DateBegin = new DateTime(2010, 9, 9),
				},

				// Creating instance of our beloved cookie class:
				new CourseInstance
				{
					ID = 20439,
					NameEN = "Kaka",
					NameIS = "Cookie",
					CourseID = "T-666-COOKIE",
					DateBegin = new DateTime(2012, 6, 6),
				}
			};

			_repositories.Add(typeof(CourseInstance), courseInstancesList);
			#endregion

			#region Mock data - course instance students:
			var courseInstanceStudentList = new List<CourseInstanceStudent>();
			courseInstanceStudentList.Add(new CourseInstanceStudent
			{
				ID = 0,
				CourseInstanceID = 20437,
				SSN = "1708882519",
			});

			courseInstanceStudentList.Add(new CourseInstanceStudent
			{
				ID = 1,
				CourseInstanceID = 20437,
				SSN = "1903863419",
			});

			courseInstanceStudentList.Add(new CourseInstanceStudent
			{
				ID = 2,
				CourseInstanceID = 20434,
				SSN = "1903863419",
			});

			// Registering Bjarki to T-109-INTO
			courseInstanceStudentList.Add(new CourseInstanceStudent
			{
				ID = 3,
				CourseInstanceID = 20435, //T-109-INTO
				SSN = "0805903269", //Bjarki
			});
			// Registering Snorri to T-109-INTO
			courseInstanceStudentList.Add(new CourseInstanceStudent
			{
				ID = 4,
				CourseInstanceID = 20435, //T-109-INTO
				SSN = "2403902139", //Snorri
			});
			// Registering Bjarki to T-666-COOKIE
			courseInstanceStudentList.Add(new CourseInstanceStudent
			{
				ID = 5,
				CourseInstanceID = 20439, // T-666-COOKIE
				SSN = "0805903269", // Bjarki
			});
			// Registering Lóa to T-666-COOKIE
			courseInstanceStudentList.Add(new CourseInstanceStudent
			{
				ID = 6,
				CourseInstanceID = 20435, // T-109-INTO
				SSN = "1903863419", // Lóa
			});
			// Registering Bjarki to T-109-INTO 20113
			courseInstanceStudentList.Add(new CourseInstanceStudent
			{
				ID = 7,
				CourseInstanceID = 22363, //T-109-INTO
				SSN = "0805903269", //Bjarki
			});
			// Registering Jóhann to T-109-INTO 20113
			courseInstanceStudentList.Add(new CourseInstanceStudent
			{
				ID = 8,
				CourseInstanceID = 22363, //T-109-INTO
				SSN = "1804912289", //Jóhann
			});
			_repositories.Add(typeof(CourseInstanceStudent), courseInstanceStudentList);
			#endregion

			#region Mock data - users:
			// Note: the test data contains users both with @ru.is and @hir.is
			// email addresses.
			var users = new List<User>
			{
				new User
				{
					UserName = "dabs", 
					Email = "dabs@ru.is", 
					SSN = "1203735289",
					FullName = "Daníel Brandur Sigurgeirsson"
				},
				new User
				{
					UserName = "siggi", 
					Email = "siggi@hir.is", 
					SSN = "1903863419",
					FullName = "Sigurður Sigurðsson"
				},
				new User
				{
					UserName = "loa", 
					Email = "loa@hir.is", 
					SSN = "1708882519",
					FullName = "Lóa Jóhannesdóttir"
				}
			};
			_repositories.Add(typeof(User), users);
			#endregion

			#region Mock data - teachers registrations:
			var teacherRegistrationList = new List<TeachersRegistration>();

			teacherRegistrationList.Add(new TeachersRegistration
			{
				ID = 1,
				CourseID = 20435,
				SSN = "1203735289",
			});

			teacherRegistrationList.Add(new TeachersRegistration
			{
				ID = 2,
				CourseID = 20435,
				SSN = "1903863419",
			});

			teacherRegistrationList.Add(new TeachersRegistration
			{
				ID = 3,
				CourseID = 20435,
				SSN = "1708882519",
			});

			_repositories.Add(typeof(TeachersRegistration), teacherRegistrationList);
			#endregion
		}

		/// <summary>
		/// Returns mock data for a certain entity
		/// </summary>
		/// <typeparam name="T">Type of entity</typeparam>
		/// <returns>Mock data for a certain entity</returns>
		public List<T> GetMockData<T>() where T : class
		{
			if (_repositories.Keys.Contains(typeof(T)))
			{
				return _repositories[typeof(T)] as List<T>;
			}

			return null;
		}
	}
}
