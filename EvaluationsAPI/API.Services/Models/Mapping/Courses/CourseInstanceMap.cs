using System.Data.Entity.ModelConfiguration;
using API.Services.Models.Entities.Courses;

namespace API.Services.Models.Mapping.Courses
{
	class CourseInstanceMap : EntityTypeConfiguration<CourseInstance>
	{
		public CourseInstanceMap()
		{
			ToTable("CourseInstances");

			HasKey(t => t.ID);

			Property(t => t.ID).HasColumnName("ID");
			Property(t => t.NameIS).HasColumnName("NameIS");
			Property(t => t.NameEN).HasColumnName("NameEN");
			Property(t => t.CourseID).HasColumnName("CourseID");
			Property(t => t.DateBegin).HasColumnName("DateBegin");
			Property(t => t.DateEnd).HasColumnName("DateEnd");
		}
	}
}