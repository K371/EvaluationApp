using System.Data.Entity.ModelConfiguration;
using API.Services.Models.Entities.Evaluations;

namespace API.Services.Models.Mapping.Evaluations
{
	public class EvaluationQuestionMap : EntityTypeConfiguration<EvaluationQuestion>
	{
		public EvaluationQuestionMap()
		{
			ToTable("Eva_Questions");
			HasKey(t => t.ID);
			Property(t => t.ID).HasColumnName("ID_Question");
			Property(t => t.TypeID).HasColumnName("ID_Type");
			Property(t => t.TextIS).HasColumnName("sText");
			Property(t => t.TextEN).HasColumnName("sAltText");
			Property(t => t.TemplateID).HasColumnName("ID_Eva");
			Property(t => t.Order).HasColumnName("nSequenceNr");
			Property(t => t.ImageURL).HasColumnName("ImageURL");
			Property(t => t.Target).HasColumnName("Target");
			Property(t => t.HappyFactor).HasColumnName("bHappyFactor");
			Property(t => t.InsertDate).HasColumnName("dtInsert");
			Property(t => t.UpdateDate).HasColumnName("dtUpdate");
			Property(t => t.DeleteDate).HasColumnName("dtDelete");
		}
	}
}
