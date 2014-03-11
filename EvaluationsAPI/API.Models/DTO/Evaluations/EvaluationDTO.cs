using System.Collections.Generic;

namespace API.Models.DTO.Evaluations
{
	/// <summary>
	/// This DTO represents the results of a given evaluation.
	/// It contains all results for all courses in this evaluation.
	/// </summary>
	public class EvaluationDTO
	{
		/// <summary>
		/// Unique identifier
		/// </summary>
		public int    ID              { get; set; }

		/// <summary>
		/// The ID of the template
		/// </summary>
		public int    TemplateID      { get; set; }

		/// <summary>
		/// The title of the evaluation, in Icelandic
		/// </summary>
		public string TemplateTitleIS { get; set; }

		/// <summary>
		/// The title of the evaluation, in English
		/// </summary>
		public string TemplateTitleEN { get; set; }

		/// <summary>
		/// A list of the results in each course.
		/// </summary>
		public List<CourseEvaluationResultDTO> Courses { get; set; } 
	}
}
