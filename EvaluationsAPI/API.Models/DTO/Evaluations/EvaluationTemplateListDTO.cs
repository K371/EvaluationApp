namespace API.Models.DTO.Evaluations
{
	/// <summary>
	/// EvaluationTemplateListDTO represents a "simple"
	/// version of an evaluation template, i.e. one which
	/// only contains basic information about a given
	/// evaluation template.
	/// </summary>
	public class EvaluationTemplateListDTO
	{
		/// <summary>
		/// Unique identifier
		/// </summary>
		public int    ID      { get; set; }

		/// <summary>
		/// The title, in Icelandic
		/// </summary>
		public string TitleIS { get; set; }

		/// <summary>
		/// The title, in English
		/// </summary>
		public string TitleEN { get; set; }
	}
}
