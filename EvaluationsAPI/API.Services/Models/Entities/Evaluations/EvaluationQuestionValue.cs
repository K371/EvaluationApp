namespace API.Services.Models.Entities.Evaluations
{
	/// <summary>
	/// EvaluationQuestionValue represents an option
	/// in a question with multiple answers
	/// (either single selection or multi selection).
	/// </summary>
	public class EvaluationQuestionValue
	{
		/// <summary>
		/// Unique identifier
		/// </summary>
		public int    ID         { get; set; }

		/// <summary>
		/// The ID of the question this answer
		/// is connected to
		/// </summary>
		public int    QuestionID { get; set; }

		/// <summary>
		/// The text of the question, in Icelandic
		/// </summary>
		public string TextIS     { get; set; }

		/// <summary>
		/// The text of the question, in English
		/// </summary>
		public string TextEN     { get; set; }

		/// <summary>
		/// The value of the question (usually 
		/// one of the numbers from 1-5)
		/// </summary>
		public int    Value      { get; set; }

		/// <summary>
		/// Not sure what this is... seems to be
		/// either NULL or an empty string in MySchool.
		/// </summary>
		public string Extra      { get; set; }

		/// <summary>
		/// Seems to be rarely used, only in 42 
		/// rows out of 2885 in MySchool
		/// </summary>
		public float  MultValue  { get; set; }

		#region Added fields
		/// <summary>
		/// A URL to an image which should be displayed
		/// with the option (thumbs up/down, smileys etc.)
		/// </summary>
		public string ImageURL   { get; set; }
		#endregion
	}
}
