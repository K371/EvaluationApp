using System;

namespace API.Models.DTO.Evaluations
{
	/// <summary>
	/// This is a lightweight DTO, meant to represent an evaluation
	/// object to be used in a list of evaluation instances, where
	/// we only need the most basic information.
	/// </summary>
	public class EvaluationListDTO
	{
		/// <summary>
		/// Unique identifier.
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// The title of the evaluation (which is originally specified 
		/// in the template) in Icelandic/English
		/// </summary>
		public string TemplateTitleIS { get; set; }
		public string TemplateTitleEN { get; set; }

		/// <summary>
		/// The date when the evaluation opens.
		/// </summary>
		public DateTime StartDate { get; set; }

		/// <summary>
		/// The date when the evaluation closes.
		/// </summary>
		public DateTime EndDate { get; set; }

		/// <summary>
		/// The status of the evaluation. The status can
		/// actually be calculated based on the Start and End
		/// dates, and is specified as follows:
		/// - "new" - evaluation has not been opened yet
		/// - "open" - evaluation is open and active, students
		///   can reply to it.
		/// - "closed" - evaluation has been completed.
		/// </summary>
		public string Status { get; set; }
	}
}
