
namespace API.Services.Models.Entities.Customers
{
	/// <summary>
	/// This class represents a single user in the system.
	/// </summary>
	public class User
	{
		public int      ID              { get; set; }
		public string   UserName        { get; set; }
		public string   FullName        { get; set; }
		public string   SSN             { get; set; }
		public string   Email           { get; set; }
	}
}
