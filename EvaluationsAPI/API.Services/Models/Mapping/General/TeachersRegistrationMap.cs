using API.Services.Models.Entities.General;
using System.Data.Entity.ModelConfiguration;

namespace API.Services.Models.Mapping.General
{
	class TeachersRegistrationMap : EntityTypeConfiguration<TeachersRegistration>
	{
		public TeachersRegistrationMap()
		{
			ToTable("TeacherRegistrations");

			HasKey(t => t.ID);

			Property(t => t.ID).HasColumnName("ID");
			Property(t => t.CourseID).HasColumnName("CourseInstanceID");
			Property(t => t.SSN).HasColumnName("SSN");
		}
	}
}
