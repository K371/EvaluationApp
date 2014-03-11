namespace API.Services.Models.Entities.Evaluations
{
	/// <summary>
	/// EvaluationTemplate represents a template for a 
	/// teaching evaluation. A template can then be used to create
	/// an actual evaluation.
	/// </summary>
	public class EvaluationTemplate
	{
		/// <summary>
		/// Unique identifier
		/// </summary>
		public int    ID             { get; set; }

		/// <summary>
		/// The name of the template (in Icelandic)
		/// </summary>
		public string NameIS         { get; set; }

		#region Fields added (not in current version of MySchool):
		public string NameEN         { get; set; }
		public string IntroTextIS    { get; set; }
		public string IntroTextEN    { get; set; }
		#endregion

		#region Unused fields
		public int?   DepartmentID   { get; set; }
		public int	  OrganizationID { get; set; }
		public int?   TypeID         { get; set; }
		public bool   Valid          { get; set; }
		#endregion

		public EvaluationTemplate()
		{
			// Default to "Reykjavík University", at least in this version.
			// This is necessary, because the field is marked as
			// "not null" in the DB.
			OrganizationID = 1;
		}
	}
}
