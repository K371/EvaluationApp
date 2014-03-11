using System.Linq;
using API.Services.Models.Entities.Customers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using API.Services.Services;
using API.Tests.MockObjects;
using API.Services.Models.Entities.General;
using API.Services.Models.Entities.Courses;

namespace API.Tests.Services
{
	[TestClass]
	public class CoursesServiceProviderTest
	{
		private CoursesServiceProvider _service;

		/// <summary>
		/// Setup the fake repository
		/// </summary>
		[TestInitialize]
		public void Setup()
		{
			var mockUnitOfWork = new MockUnitOfWork<MockDataContext>();
			var mockFactory = new MockFactory();

			// Arrange
			mockUnitOfWork.SetRepositoryData(mockFactory.GetMockData<CourseInstance>());
			mockUnitOfWork.SetRepositoryData(mockFactory.GetMockData<CourseInstanceStudent>());
			mockUnitOfWork.SetRepositoryData(mockFactory.GetMockData<TeachersRegistration>());
			mockUnitOfWork.SetRepositoryData(mockFactory.GetMockData<User>());

			_service = new CoursesServiceProvider(mockUnitOfWork);
		}

		/// <summary>
		/// Test to get teachers for a course on a semester
		/// </summary>
		[TestMethod]
		public void CoursesTestGetTeachersForCourseSemester()
		{
			// Arrange
			const string course = "T-109-INTO";

			// Act
			var teachers = _service.GetTeachersForCourse(course);

			// Assert
			Assert.AreEqual(teachers.Count(), 3, "Teacher count for the course is not right");
		}

		[TestMethod]
		public void CoursesTestGetCoursesForStudentSpecificSemester()
		{
			// Arrange:
			const string userName = "loa";

			// Act:
			var result = _service.GetCoursesForStudent(userName);

			// Assert:
			Assert.AreEqual(1, result.Count(), "Not correct number of courses for student");
		}
	}
}
