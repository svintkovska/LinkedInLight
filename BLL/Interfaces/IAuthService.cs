using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface IAuthService
	{
		public Task<bool> Register(string email, string password);
		public Task<LoginResult> Login(string email, string password);
		public Task<LoginResult> GoogleRegistration(GoogleModel registrationModel);
		public Task<LoginResult> GoogleLogin(GoogleModel model);
		public  Task<bool> ForgotPassword(string email);
	}
}
