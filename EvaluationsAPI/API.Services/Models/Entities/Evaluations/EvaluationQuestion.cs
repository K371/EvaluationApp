using System;

namespace API.Services.Models.Entities.Evaluations
{
	/// <summary>
	/// EvaluationQuestion represents a single question in an
	/// evaluation template.
	/// </summary>
	public class EvaluationQuestion
	{
		/// <summary>
		/// Unique identifier
		/// </summary>
		public int    ID          { get; set; }

		/// <summary>
		/// The type of the question. We will only use three types:
		/// 1 (Textaspurning)
		/// 4 (Many options, only one can be selected)
		/// 6 (Many options, user can select many)
		/// MySchool defines other options, but we will only use
		/// those three in this version.
		/// </summary>
		public int    TypeID      { get; set; }

		public const int TYPE_TEXT = 1;
		public const int TYPE_OPTIONS_SINGLE = 4;
		public const int TYPE_OPTIONS_MULTIPLE = 6;

		/// <summary>
		/// The text of the question, in Icelandic
		/// </summary>
		public string TextIS      { get; set; }

		/// <summary>
		/// The text of the question, in English
		/// </summary>
		public string TextEN      { get; set; }

		/// <summary>
		/// The ID of the template which this question
		/// belongs to
		/// </summary>
		public int    TemplateID  { get; set; }

		/// <summary>
		/// The order of the questions
		/// </summary>
		public int    Order       { get; set; }

		#region Added fields
		/// <summary>
		/// If an image should be displayed with the question,
		/// it can be linked to here.
		/// </summary>
		public string ImageURL    { get; set; }

		/// <summary>
		/// 0 if the question is targeted towards the course
		/// in general, 1 if targeted towards teachers in the course.
		/// </summary>
		public int Target { get; set; }

		public const int TARGET_COURSE = 0;
		public const int TARGET_TEACHERS = 1;
		#endregion

		#region Not used
		/// <summary>
		/// True if the question is "Are you happy/unhappy with..."
		/// We won't use this in the current version.
		/// </summary>
		public bool   HappyFactor { get; set; }
		#endregion

		#region Bookkeeping
		/// <summary>
		/// The usual date fields.
		/// </summary>
		public DateTime InsertDate { get; set; }
		public DateTime UpdateDate { get; set; }
		public DateTime DeleteDate { get; set; }
		#endregion
	}
}
