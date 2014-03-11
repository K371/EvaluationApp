using System.Data.Entity.ModelConfiguration;
using API.Services.Models.Entities.Evaluations;

namespace API.Services.Models.Mapping.Evaluations
{
	public class EvaluationAnswerMap : EntityTypeConfiguration<EvaluationAnswer>
	{
		public EvaluationAnswerMap()
		{
			ToTable("Eva_Answers");
			HasKey(t => t.ID);
			Property(t => t.ID).HasColumnName("ID_Answer");
			Property(t => t.QuestionID).HasColumnName("ID_Question");
			Property(t => t.TeacherSSN).HasColumnName("ID_Teacher");
			Property(t => t.CourseInstanceID).HasColumnName("ID_CourseInstance");
			Property(t => t.DepartmentID).HasColumnName("ID_Department");
			Property(t => t.EvaluationInstanceID).HasColumnName("ID_History");
			Property(t => t.Text).HasColumnName("sText");
			Property(t => t.Value).HasColumnName("lValue");
			Property(t => t.MajorID).HasColumnName("id_major");
			Property(t => t.ClassID).HasColumnName("id_class");
			Property(t => t.GroupID).HasColumnName("id_group");
			Property(t => t.QuestionValueID).HasColumnName("ID_QuestionValue");
			Property(t => t.Extra).HasColumnName("sExtra");
			Property(t => t.PageID).HasColumnName("ID_Page");
		}
	}
}
