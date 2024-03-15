using BLL.ViewModels.AuthModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAuthService
	{
		public Task<IEnumerable<string>> GetAllCountries();
		public Task<IEnumerable<string>> GetCitiesByCountry(string country);
		public Task<bool> Register(RegisterVM model);
		public Task<LoginResultVM> Login(string email, string password);
		public Task<LoginResultVM> GoogleRegistration(GoogleVM registrationModel);
		public Task<LoginResultVM> GoogleLogin(GoogleVM model);
		public  Task<bool> ForgotPassword(string email);
		public Task<bool> SetNewPassword(NewPasswordVM model);
		public Task<bool> IsValidEmail(string email);
		public Task<string> SendConfirmationCode(string email);
	}
}
