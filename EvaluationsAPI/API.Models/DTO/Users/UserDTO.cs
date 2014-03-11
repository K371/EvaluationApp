namespace API.Models.DTO.Users
{
	/// <summary>
	/// Basic information about a given user.
	/// </summary>
	public class UserDTO
	{
		public string Username { get; set; }
		public string FullName { get; set; }
		public string SSN      { get; set; }
		public string Email    { get; set; }
		public string Role     { get; set; }
		public string ImageURL { get; set; }
	}
}
