using System.Data.Entity.ModelConfiguration;
using API.Services.Models.Entities.Courses;

namespace API.Services.Models.Mapping.Courses
{
	class CourseInstanceStudentMap : EntityTypeConfiguration<CourseInstanceStudent>
	{
		public CourseInstanceStudentMap()
		{
			ToTable("CourseInstanceStudents");

			HasKey(t => t.ID);

			Property(t => t.ID).HasColumnName("ID");
			Property(t => t.CourseInstanceID).HasColumnName("CourseInstanceID");
			Property(t => t.SSN).HasColumnName("SSN");
		}
	}
}
