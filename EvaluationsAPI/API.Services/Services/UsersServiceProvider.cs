using System.Linq;
using API.Services.Models.Entities.Customers;
using API.Services.Repositories;

namespace API.Services.Services
{
	/// <summary>
	/// UsersServiceProvider contains various services which are not 
	/// specific to students, i.e. they refer to all users: teachers,
	/// students, faculty, other people etc.
	/// 
	/// Note that there might be some overlap in functionality between
	/// this class and StudentsServiceProvider, we should strive to 
	/// minimize that.
	/// </summary>
	public class UsersServiceProvider
	{
		#region Member variables
		private readonly IRepository<User> _users;
		#endregion

		#region Constructor
		public UsersServiceProvider(IUnitOfWork uow)
		{
			_users = uow.GetRepository<User>();
		}
		#endregion

		#region Public functions
		public API.Models.DTO.Users.UserDTO Login(string user, string pass)
		{
			var result = (from u in _users.All()
			              where u.UserName == user
			              select new API.Models.DTO.Users.UserDTO
			              {
				              FullName = u.FullName,
				              SSN = u.SSN,
				              Username = user,
				              Role = (user == "admin") ? "admin" : "student",
							  ImageURL = ""
			              }).SingleOrDefault();

			if (result != null)
			{
				result.ImageURL = "http://www.ru.is/kennarar/dabs/img/" + result.SSN.Substring(0, 2) + "/" + result.SSN + ".jpg";
			}
			return result;
		}
		#endregion
	}
}
