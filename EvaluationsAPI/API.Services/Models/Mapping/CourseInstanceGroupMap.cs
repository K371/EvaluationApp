using System.Data.Entity.ModelConfiguration;
using API.Services.Models.Entities.Courses;

namespace API.Services.Models.Mapping
{
    public class CourseInstanceGroupMap : EntityTypeConfiguration<CourseInstanceGroup>
    {
        public CourseInstanceGroupMap()
        {
            this.ToTable("CourseInst_Groups");

            this.HasKey(t => t.ID);

            this.Property(t => t.ID).HasColumnName("ID_Group");
            this.Property(t => t.CourseInstId).HasColumnName("ID_CourseInst");
            this.Property(t => t.MaxStudentNumber).HasColumnName("nMax");
            this.Property(t => t.GroupName).HasColumnName("sName");
            this.Property(t => t.GroupNumber).HasColumnName("nGroupNumber");            
        }
    }
}