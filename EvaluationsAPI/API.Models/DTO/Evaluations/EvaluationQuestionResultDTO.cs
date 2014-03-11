namespace API.Models.DTO.Evaluations
{
	/// <summary>
	/// This class represents the results of a given question
	/// in a single evaluation in a particular course.
	/// </summary>
	public class EvaluationQuestionResultDTO
	{
		/// <summary>
		/// The ID of the question
		/// </summary>
		public int      QuestionID     { get; set; }

		/// <summary>
		/// The title of the question, in Icelandic/English.
		/// </summary>
		public string   TextIS         { get; set; }
		public string   TextEN         { get; set; }

		/// <summary>
		/// The SSN of the teacher this answer is targeted
		/// towards. Should be null if the question is
		/// targeted towards the course in general.
		/// </summary>
		public string   TeacherSSN     { get; set; }

		/// <summary>
		/// The type of the question: "text", "single" or "multiple".
		/// </summary>
		public string   Type           { get; set; }

		/// <summary>
		/// Note that these two properties are mutually
		/// exclusive. One of those will always be null,
		/// depending on the type of the question.
		/// </summary>
		public string[] TextResults    { get; set; }
		public object   OptionsResults { get; set; }
	}
}
