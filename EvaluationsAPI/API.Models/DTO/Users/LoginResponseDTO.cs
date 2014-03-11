namespace API.Models.DTO.Users
{
	/// <summary>
	/// This class represents the reply a user gets when login succeeds.
	/// </summary>
	public class LoginResponseDTO
	{
		/// <summary>
		/// A token which should be sent as a part of the "Authorization"
		/// HTTP header of each request after login.
		/// </summary>
		public string Token { get; set; }

		/// <summary>
		/// Basic information about the logged-in user
		/// </summary>
		public UserDTO User { get; set; }
	}
}
