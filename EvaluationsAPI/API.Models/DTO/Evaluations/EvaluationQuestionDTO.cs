using System.Collections.Generic;

namespace API.Models.DTO.Evaluations
{
	/// <summary>
	/// This DTO represents a question in an evaluation template.
	/// </summary>
	public class EvaluationQuestionDTO
	{
		/// <summary>
		/// Unique identifier
		/// </summary>
		public int    ID       { get; set; }

		/// <summary>
		/// The text of the question in Icelandic/English
		/// </summary>
		public string TextIS   { get; set; }
		public string TextEN   { get; set; }

		/// <summary>
		/// A URL to an image to be displayed with the question.
		/// Optional, can be null.
		/// </summary>
		public string ImageURL { get; set; }

		/// <summary>
		/// The type of the question: "text", "single" or "multiple".
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// A list of question options. Not used if the question
		/// is of type "text".
		/// </summary>
		public List<EvaluationQuestionAnswerDTO> Answers { get; set; }
	}
}