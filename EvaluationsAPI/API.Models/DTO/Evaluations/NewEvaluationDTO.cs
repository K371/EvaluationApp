using System;

namespace API.Models.DTO.Evaluations
{
	/// <summary>
	/// This DTO represents a new instance of an evaluation.
	/// </summary>
	public class NewEvaluationDTO
	{
		/// <summary>
		/// The ID of the template this evaluation instance
		/// is based upon.
		/// </summary>
		public int      TemplateID { get; set; }

		/// <summary>
		/// The date when the evaluation should open.
		/// </summary>
		public DateTime StartDate  { get; set; }

		/// <summary>
		/// The date when the evaluation should end.
		/// </summary>
		public DateTime EndDate    { get; set; }
	}
}
