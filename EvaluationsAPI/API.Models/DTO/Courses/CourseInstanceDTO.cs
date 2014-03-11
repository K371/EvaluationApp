using System;

namespace API.Models.DTO.Courses
{
	/// <summary>
	/// This is a lightweight DTO class for the CourseInstance table.
	/// It should only contain the most used fields of that table.
	/// </summary>
	public class CourseInstanceDTO
	{
		public int       ID                 { get; set; }
		public string    CourseID           { get; set; }
		public string    NameIS             { get; set; }
		public string    NameEN             { get; set; }
		public DateTime? DateBegin          { get; set; }
		public DateTime? DateEnd            { get; set; }
	}
}