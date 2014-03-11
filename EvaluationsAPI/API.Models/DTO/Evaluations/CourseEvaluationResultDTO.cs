using System.Collections.Generic;

namespace API.Models.DTO.Evaluations
{
	/// <summary>
	/// This DTO represents the results of an evaluation in a given course.
	/// </summary>
	public class CourseEvaluationResultDTO
	{
		/// <summary>
		/// The Unique identifer of the course
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// The ID of the course, such as "T-427-WEPO"
		/// </summary>
		public string CourseID     { get; set; }
		
		/// <summary>
		/// The semester in question, such as "20141"
		/// </summary>
		public string Semester     { get; set; }

		/// <summary>
		/// The name of the course, in Icelandic.
		/// Example: "Vefforritun II"
		/// </summary>
		public string CourseNameIS { get; set; }

		/// <summary>
		/// The name of the course, in English.
		/// Example: "Web Programming II".
		/// </summary>
		public string CourseNameEN { get; set; }

		/// <summary>
		/// A list of the results for each question in the evaluation.
		/// </summary>
		public List<EvaluationQuestionResultDTO> Questions { get; set; }
	}
}
