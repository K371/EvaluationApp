using System.Data.Entity.ModelConfiguration;
using API.Services.Models.Entities.Evaluations;

namespace API.Services.Models.Mapping.Evaluations
{
	public class EvaluationReplyMap : EntityTypeConfiguration<EvaluationReply>
	{
		public EvaluationReplyMap()
		{
			ToTable("Eva_Replies");
			HasKey(t => t.ID);
			Property(t => t.ID).HasColumnName("ID_Replies");
			Property(t => t.EvaluationInstanceID).HasColumnName("ID_History");
			Property(t => t.StudentSSN).HasColumnName("ID_Student");
			Property(t => t.DateAdded).HasColumnName("dtTimeStamp");
		}
	}
}
