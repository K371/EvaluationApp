namespace API.Models.DTO.Evaluations
{
	/// <summary>
	/// This DTO represents an object that should be used when
	/// the student is shown a list of evaluations (s)he can
	/// take part in.
	/// </summary>
	public class StudentEvaluationListDTO
	{
		/// <summary>
		/// The Unique identifier of the evaluation (i.e. a number)
		/// </summary>
		public int    ID           { get; set; }

		/// <summary>
		/// The ID of the course, for instance: "T-427-WEPO".
		/// </summary>
		public string CourseID     { get; set; }

		/// <summary>
		/// The name of the course in Icelandic/English.
		/// </summary>
		public string CourseNameIS { get; set; }
		public string CourseNameEN { get; set; }

		/// <summary>
		/// The semester in which this course is taught.
		/// </summary>
		public string Semester     { get; set; }
	}
}
