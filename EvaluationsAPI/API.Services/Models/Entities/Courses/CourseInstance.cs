using System;

namespace API.Services.Models.Entities.Courses
{
	/// <summary>
	/// Missing documentation
	/// </summary>
	public class CourseInstance
	{
		public int       ID                  { get; set; }
		public string    NameIS              { get; set; }
		public string    NameEN              { get; set; }
		public string    CourseID            { get; set; }
		public DateTime  DateBegin           { get; set; }
		public DateTime? DateEnd             { get; set; }
	}
}