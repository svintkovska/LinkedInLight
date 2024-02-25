using BLL.DTOs;

namespace BLL.ViewModels
{
	public class LoginResult
	{
		public bool Success { get; set; }
		public UserDTO User { get; set; }
		public IList<string> Roles { get; set; }
		public string Token { get; set; }
	}
}
