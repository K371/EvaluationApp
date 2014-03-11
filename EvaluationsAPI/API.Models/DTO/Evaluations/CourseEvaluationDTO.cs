using System.Collections.Generic;

namespace API.Models.DTO.Evaluations
{
	/// <summary>
	/// This DTO is used when a student requests to view the evaluation
	/// for a given course.
	/// </summary>
	public class CourseEvaluationDTO
	{
		/// <summary>
		/// The ID of the evaluation instance.
		/// </summary>
		public int    ID          { get; set; }

		/// <summary>
		/// The ID of the evaluation template.
		/// </summary>
		public int    TemplateID  { get; set; }

		/// <summary>
		/// The title of the evaluation (template), in Icelandic
		/// </summary>
		public string TitleIS     { get; set; }

		/// <summary>
		/// The title of the evaluation in English.
		/// </summary>
		public string TitleEN     { get; set; }

		/// <summary>
		/// The introduction text
		/// </summary>
		public string IntoTextIS  { get; set; }
		public string IntroTextEN { get; set; }

		/// <summary>
		/// A list of questions, i.e. there are separate lists for 
		/// questions targeted towards the course in general, and 
		/// for those targeted towards teachers in the course.
		/// The student should be given the option to reply to the
		/// teacher questions for each teacher in the course.
		/// </summary>
		public List<EvaluationQuestionDTO> CourseQuestions { get; set; }
		public List<EvaluationQuestionDTO> TeacherQuestions { get; set; }
	}
}
