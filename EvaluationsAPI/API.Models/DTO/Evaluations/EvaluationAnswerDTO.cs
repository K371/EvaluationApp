namespace API.Models.DTO.Evaluations
{
	/// <summary>
	/// This DTO represents an answer to a question,
	/// from a student. The "Value" property is either
	/// a text or a number, if the question is a multiple
	/// options question.
	/// </summary>
	public class EvaluationAnswerDTO
	{
		/// <summary>
		/// The ID of the question this answer is for
		/// </summary>
		public int    QuestionID { get; set; }

		/// <summary>
		/// The SSN of the teacher this answer is meant for,
		/// if applicable.
		/// </summary>
		public string TeacherSSN { get; set; }

		/// <summary>
		/// The value of the answer. Can either be some text
		/// (if the question is a text question), or some value,
		/// such as a integer between 1 and 5.
		/// </summary>
		public string Value      { get; set; }
	}
}
