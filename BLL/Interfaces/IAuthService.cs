using BLL.ViewModels;
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
		public Task<IEnumerable<CountryVM>> GetAllCountries();
		public Task<IEnumerable<CityVM>> GetCitiesByCountry(string countryName);
		public Task<bool> Register(RegisterVM model);
		public Task<LoginResultVM> Login(string email, string password);
		public Task<LoginResultVM> GoogleRegistration(GoogleVM registrationModel);
		public Task<LoginResultVM> GoogleLogin(GoogleVM model);
		public  Task<bool> ForgotPassword(string email);
		public Task<bool> SetNewPassword(NewPasswordVM model);
		public Task<bool> IsValidEmail(string email);
		public Task<bool> ConfirmEmail(string email, string code, string emailToken);
		public Task<bool> SendConfirmationCode(string email);
	}
}
