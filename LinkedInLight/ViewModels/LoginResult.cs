using BLL.DTOs;

namespace LinkedInLight.ViewModels
{
	public class LoginResult
	{
		public bool Success { get; set; }
		public UserDTO User { get; set; }
		public IList<string> Roles { get; set; }
	}
}
