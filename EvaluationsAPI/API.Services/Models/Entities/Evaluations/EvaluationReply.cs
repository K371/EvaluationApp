using System;

namespace API.Services.Models.Entities.Evaluations
{
	/// <summary>
	/// Whenever a student has completed an evaluation, 
	/// a new instance of this entity will be created (and
	/// added to the corresponding table). Note that a
	/// single entry here simply means that the given student
	/// has answered a given evaluation, but does not state
	/// anything about how many answers were included, or
	/// what those answers were.
	/// </summary>
	public class EvaluationReply
	{
		/// <summary>
		/// Unique identifier
		/// </summary>
		public int      ID                   { get; set; }

		/// <summary>
		/// The ID of the evaluation instance.
		/// </summary>
		public int      EvaluationInstanceID { get; set; }

		/// <summary>
		/// The SSN of the student in question.
		/// </summary>
		public string   StudentSSN           { get; set; }

		/// <summary>
		/// The date when the submission was completed.
		/// </summary>
		public DateTime DateAdded            { get; set; }
	}
}
