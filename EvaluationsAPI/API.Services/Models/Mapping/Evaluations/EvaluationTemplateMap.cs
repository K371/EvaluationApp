using System.Data.Entity.ModelConfiguration;
using API.Services.Models.Entities.Evaluations;

namespace API.Services.Models.Mapping.Evaluations
{
	public class EvaluationTemplateMap : EntityTypeConfiguration<EvaluationTemplate>
	{
		public EvaluationTemplateMap()
		{
			ToTable("Evaluations");
			HasKey(t => t.ID);
			Property(t => t.ID).HasColumnName("ID_Evaluation");
			Property(t => t.NameIS).HasColumnName("sName");
			Property(t => t.NameEN).HasColumnName("sNameEN");

			Property(t => t.DepartmentID).HasColumnName("ID_Department");
			Property(t => t.OrganizationID).HasColumnName("ID_Organization");
			Property(t => t.TypeID).HasColumnName("ID_Type");
			Property(t => t.Valid).HasColumnName("bValid");
		}
	}
}
