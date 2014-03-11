
using System;

namespace API.Services.Models.Entities.Evaluations
{
	/// <summary>
	/// EvaluationInstance represents a given instance of an
	/// evaluation (based on a template). This class maps to 
	/// the table Eva_History (clearly obvious).
	/// </summary>
	public class EvaluationInstance
	{
		/// <summary>
		/// Unique identifier
		/// </summary>
		public int      ID               { get; set; }

		/// <summary>
		/// The ID of the template which this evaluation
		/// is based on.
		/// </summary>
		public int      TemplateID       { get; set; }

		/// <summary>
		/// If this is not null, the evaluation is targeted 
		/// towards a given course.
		/// </summary>
		public int?     CourseInstanceID { get; set; }

		/// <summary>
		/// Not used (in this version...)
		/// </summary>
		public string   TeacherSSN       { get; set; }

		/// <summary>
		/// The date when this evaluation should open.
		/// </summary>
		public DateTime StartDate        { get; set; }

		/// <summary>
		/// The date when the evaluation should close.
		/// </summary>
		public DateTime EndDate          { get; set; }

		/// <summary>
		/// The semester which this evaluation is related
		/// to (probably unneccessary)
		/// </summary>
		public string   Semester         { get; set; }

		/// <summary>
		/// The department ID (probably unneccessary as well...)
		/// </summary>
		public int?     DepartmentID     { get; set; }

		/// <summary>
		/// If true, then the evaluation will be visible to 
		/// teachers. 
		/// Not used in this version.
		/// </summary>
		public bool     Published        { get; set; }

		/// <summary>
		/// No idea what this is.
		/// </summary>
		public int?     GroupID          { get; set; }

		/// <summary>
		/// The usual date fields.
		/// </summary>
		public DateTime InsertDate       { get; set; }
		public DateTime UpdateDate       { get; set; }
		public DateTime DeleteDate       { get; set; }

	}
}
