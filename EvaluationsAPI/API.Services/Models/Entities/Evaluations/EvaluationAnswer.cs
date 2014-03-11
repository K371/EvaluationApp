
namespace API.Services.Models.Entities.Evaluations
{
	/// <summary>
	/// This class represents a single answer from a given
	/// student to a single question. Note that the identity
	/// of the student is not stored here, as the answers
	/// are supposed to be anonymous.
	/// </summary>
	public class EvaluationAnswer
	{
		/// <summary>
		/// Unique identifier
		/// </summary>
		public int    ID                   { get; set; }

		/// <summary>
		/// The ID of the question this answer is related to
		/// </summary>
		public int    QuestionID           { get; set; }

		/// <summary>
		/// If the question was targeted towards the teachers
		/// of the course, then this field represents what teacher
		/// this answer relates to. Could be NULL.
		/// </summary>
		public string TeacherSSN           { get; set; }

		/// <summary>
		/// The ID of the course which this answer relates to.
		/// </summary>
		public int?   CourseInstanceID     { get; set; }

		/// <summary>
		/// The ID of the department (not really sure why 
		/// this is needed...)
		/// </summary>
		public int?   DepartmentID         { get; set; }

		/// <summary>
		/// The link to the EvaluationInstance this answer
		/// is connected to.
		/// </summary>
		public int    EvaluationInstanceID { get; set; }

		/// <summary>
		/// The text of the answer (only used if the
		/// question is a text question).
		/// </summary>
		public string Text                 { get; set; }

		/// <summary>
		/// The value of the question (usually 1-5)
		/// </summary>
		public int    Value                { get; set; }

		/// <summary>
		/// Not sure what this is, or why it is here...
		/// </summary>
		public int?   MajorID              { get; set; }

		/// <summary>
		/// Not sure what this is, or why it is here...
		/// </summary>
		public int?   ClassID              { get; set; }

		/// <summary>
		/// Not sure what this is, or why it is here...
		/// </summary>
		public int?   GroupID              { get; set; }

		/// <summary>
		/// Not sure what this is, or why it is here...
		/// </summary>
		public int?   QuestionValueID      { get; set; }

		/// <summary>
		/// Not sure what this is, or why it is here...
		/// </summary>
		public string Extra                { get; set; }

		/// <summary>
		/// Not sure what this is, or why it is here...
		/// </summary>
		public int?   PageID               { get; set; }
	}
}
