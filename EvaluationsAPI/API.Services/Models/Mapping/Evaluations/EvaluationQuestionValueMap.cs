using System.Data.Entity.ModelConfiguration;
using API.Services.Models.Entities.Evaluations;

namespace API.Services.Models.Mapping.Evaluations
{
	public class EvaluationQuestionValueMap : EntityTypeConfiguration<EvaluationQuestionValue>
	{
		public EvaluationQuestionValueMap()
		{
			ToTable("Eva_QuestionValues");
			HasKey(t => t.ID);
			Property(t => t.ID).HasColumnName("ID_Questionvalue");
			Property(t => t.QuestionID).HasColumnName("ID_Question");
			Property(t => t.TextIS).HasColumnName("sText");
			Property(t => t.TextEN).HasColumnName("sAltText");
			Property(t => t.Value).HasColumnName("iValue");
			Property(t => t.Extra).HasColumnName("sExtra");
			Property(t => t.MultValue).HasColumnName("fMultValue");
			Property(t => t.ImageURL).HasColumnName("ImageURL");
		}
	}
}
