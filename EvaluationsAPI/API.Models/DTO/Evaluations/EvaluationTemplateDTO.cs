using System.Collections.Generic;

namespace API.Models.DTO.Evaluations
{
	/// <summary>
	/// This DTO represents a single evaluation template, complete 
	/// with all the questions in it, as well as all the question options
	/// (for those questions which have options).
	/// </summary>
	public class EvaluationTemplateDTO
	{
		/// <summary>
		/// The unique identifier of the template.
		/// </summary>
		public int    ID          { get; set; }

		/// <summary>
		/// The title of the template, in Icelandic/English.
		/// </summary>
		public string TitleIS     { get; set; }
		public string TitleEN     { get; set; }

		/// <summary>
		/// The introduction text, in Icelandic/English
		/// </summary>
		public string IntroTextIS { get; set; }
		public string IntroTextEN { get; set; }

		/// <summary>
		/// A list of questions which should be targeted towards
		/// the course in general.
		/// </summary>
		public List<EvaluationQuestionDTO> CourseQuestions { get; set; }

		/// <summary>
		/// A list of questions which should be targeted towards
		/// each teacher in the course. I.e. these questions should
		/// be repeated for each teacher, however, the student should
		/// be given the option what teachers to evaluate.
		/// </summary>
		public List<EvaluationQuestionDTO> TeacherQuestions { get; set; }
	}
}
