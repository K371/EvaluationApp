using API.Services.Models.Entities.Assignment;
using System;
using System.Linq;
using System.Collections.Generic;
using API.Services.Models.Entities.Courses;

namespace API.Tests.MockObjects
{
	public class AssignmentMockFactory
	{
		private readonly Dictionary<Type, object> _repositories;

		public AssignmentMockFactory()
		{
			var assignmentList = new List<Assignment>();
			var assignmentCourseInstanceList = new List<AssignmentCourseInstance>();
			var assignmentGroupList = new List<AssignmentGroup>();
			var assignmentDownloadList = new List<AssignmentDownload>();
			var assignmentSolutionList = new List<AssignmentSolution>();
			var assignmentSolutionGroupList = new List<AssignmentSolutionGroup>();
			var courseInstancesList = new List<CourseInstance>();

			#region Mock data - course instances (7 total)
			// Exchange Program course
			courseInstancesList.Add(new CourseInstance
			{
				ID = 22971,
				CourseID = "X-699-EXCH",
				DateBegin = new System.DateTime(2012, 1, 1),
				DateEnd = new System.DateTime(2012, 5, 31),
				Semester = "20121"
			});

			courseInstancesList.Add(new CourseInstance
			{
				ID = 20435,
				CourseID = "T-109-INTO",
				DateBegin = new System.DateTime(2010, 9, 9),
				Semester = "20103"
			});

			courseInstancesList.Add(new CourseInstance
			{
				ID = 20436,
				CourseID = "T-110-VERK",
				DateBegin = new System.DateTime(2010, 9, 9),
				Semester = "20103"
			});

			courseInstancesList.Add(new CourseInstance
			{
				ID = 20437,
				CourseID = "T-111-PROG",
				DateBegin = new System.DateTime(2010, 9, 9),
				Semester = "20103"
			});

			courseInstancesList.Add(new CourseInstance
			{
				ID = 20438,
				CourseID = "T-117-STR1",
				DateBegin = new System.DateTime(2010, 9, 9),
				Semester = "20103"
			});

			courseInstancesList.Add(new CourseInstance
			{
				ID = 20434,
				CourseID = "T-107-TOLH",
				DateBegin = new System.DateTime(2010, 9, 9),
				Semester = "20103"
			});
			// Creating instance of our beloved cookie class
			courseInstancesList.Add(new CourseInstance
			{
				ID = 20439,
				CourseID = "T-666-COOKIE",
				DateBegin = new System.DateTime(2012, 6, 6),
				Semester = "20123"
			});
			#endregion

			#region Mock data - assignments (2 total)
			assignmentList.Add(new Assignment
			{
				ID = 1,
				Title = "Mp3 spilari",
				Description = "Forrita mp3 spilara í c++",
				AllowedFileExtensions = "*.exe;*.doc;*.txt",
				AllowedNumberOfFiles = 1,
				Anonymity = 0,
				MaxStudentsInGroup = 1,
				Weight = 15,
				AverageGrade = null,
				DateInserted = new DateTime(2010, 9, 15, 10, 0, 0),
				DatePublished = new DateTime(2010, 9, 15, 10, 0, 0),
				DateClosed = new DateTime(2010, 9, 20, 23, 59, 0),
				DateUpdated = null,
				DateDeleted = null
			});
			assignmentList.Add(new Assignment
			{
				ID = 2,
				Title = "Myschool",
				Description = "Forrita nýtt Myschool",
				AllowedFileExtensions = "*.zip;*.doc;*.cs",
				AllowedNumberOfFiles = 5,
				Anonymity = 0,
				MaxStudentsInGroup = 1,
				Weight = 20,
				AverageGrade = null,
				DateInserted = new DateTime(2010, 9, 15, 12, 20, 0),
				DatePublished = new DateTime(2010, 9, 15, 13, 0, 0),
				DateClosed = new DateTime(2010, 9, 20, 22, 0, 0),
				DateUpdated = null,
				DateDeleted = null
			});
			#endregion

			#region Mock data - assignments course instance (2 total)
			assignmentCourseInstanceList.Add(new AssignmentCourseInstance
			{
				AssignmentID = 2,
				CourseInstanceID = 20435,
				DateDeleted = null,
				DateInserted = null,
				DateUpdated = null
			});
			assignmentCourseInstanceList.Add(new AssignmentCourseInstance
			{
				AssignmentID = 1,
				CourseInstanceID = 20435,
				DateDeleted = null,
				DateInserted = null,
				DateUpdated = null
			});
			#endregion

			#region Mock data - assignments download (1 total)
			assignmentDownloadList.Add(new AssignmentDownload
			{
				ID = 0,
				AssignmentID = 1,
				UserID = 1,
				SSN = "2403902139",
				DateInserted = DateTime.Now,
				DateUpdated = null,
				DateDeleted = null
			});
			#endregion

			#region Mock data - assignments Solutions (1 total)
			assignmentSolutionList.Add(new AssignmentSolution
			{
				GroupID = 1,
				AssignmentID = 1,
				Grade = null,
				StudentMemo = null,
				TeacherMemo = null,
				Published = 1,
				Closes = new DateTime(2013, 11, 28),
				Rank = null,
				Closed = 0,
				Graded = null,
				DateInserted = new DateTime(2013, 11, 28),
				DateUpdated = null,
				DateDeleted = null
			});
			#endregion

			#region Mock data - assignments Solution Groups (1 total)
			assignmentSolutionGroupList.Add(new AssignmentSolutionGroup
			{
				GroupID = 235,
				SSN = "2403902139",
				UserID = 1,
				DateInserted = new DateTime(2013, 11, 28),
				DateUpdated = null,
				DateDeleted = null
			});
			#endregion

			#region Mock data - assignments group (1 total)
			assignmentGroupList.Add(new AssignmentGroup
			{
				GroupID = 1,
				AssignmentID = 1,
				DateInserted = new DateTime(2013, 11, 28),
				DateUpdated = null,
				DateDeleted = null
			});
			#endregion

			#region Populate repo
			_repositories.Add(typeof(Assignment), assignmentList);
			_repositories.Add(typeof(AssignmentCourseInstance), assignmentCourseInstanceList);
			_repositories.Add(typeof(AssignmentGroup), assignmentGroupList);
			_repositories.Add(typeof(AssignmentSolution), assignmentSolutionList);
			_repositories.Add(typeof(AssignmentSolutionGroup), assignmentSolutionGroupList);
			_repositories.Add(typeof(AssignmentDownload), assignmentDownloadList);
			#endregion
		}

		/// <summary>
		/// TODO: put this in super class. Code duplication !
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
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
