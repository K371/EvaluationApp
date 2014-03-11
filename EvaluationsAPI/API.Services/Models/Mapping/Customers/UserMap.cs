using System.Data.Entity.ModelConfiguration;
using API.Services.Models.Entities.Customers;

namespace API.Services.Models.Mapping.Customers
{
	public class UserMap : EntityTypeConfiguration<User>
	{
		public UserMap()
		{
			ToTable("Users");

			HasKey(t => t.ID);

			Property(t => t.ID).HasColumnName("ID");
			Property(t => t.FullName).HasColumnName("FullName");
			Property(t => t.UserName).HasColumnName("UserName");
			Property(t => t.SSN).HasColumnName("SSN");
			Property(t => t.Email).HasColumnName("Email");
		}
	}
}
