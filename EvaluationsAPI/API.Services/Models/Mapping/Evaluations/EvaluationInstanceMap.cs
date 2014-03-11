using System.Data.Entity.ModelConfiguration;
using API.Services.Models.Entities.Evaluations;

namespace API.Services.Models.Mapping.Evaluations
{
	public class EvaluationInstanceMap : EntityTypeConfiguration<EvaluationInstance>
	{
		public EvaluationInstanceMap()
		{
			ToTable("Eva_History");
			HasKey(t => t.ID);
			Property(t => t.ID).HasColumnName("ID_History");
			Property(t => t.TemplateID).HasColumnName("ID_Eva");
			Property(t => t.CourseInstanceID).HasColumnName("ID_CourseInstance");
			Property(t => t.TeacherSSN).HasColumnName("ID_Teacher");
			Property(t => t.StartDate).HasColumnName("dtBegins");
			Property(t => t.EndDate).HasColumnName("dtEnds");
			Property(t => t.Semester).HasColumnName("ID_Semester");
			Property(t => t.DepartmentID).HasColumnName("ID_Dep");
			Property(t => t.Published).HasColumnName("bPublished");
			Property(t => t.GroupID).HasColumnName("ID_Group");
			Property(t => t.InsertDate).HasColumnName("dtInsert");
			Property(t => t.UpdateDate).HasColumnName("dtUpdate");
			Property(t => t.DeleteDate).HasColumnName("dtDelete");
		}
	}
}
