using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface IAdditionalProfileService : IProfileService
	{
		public Task<List<UserLanguageVM>> GetUserLanguages(string userid);
		public Task<List<LanguageVM>> GetAllLanguages();
		public Task<bool> AddLanguage(UserLanguageVM language, string userid);
		public Task<bool> RemoveLanguage(int languageId, string userId);
		public Task<bool> UpdateLanguage(UserLanguageVM language);


		public Task<List<VolunteerExperienceVM>> GetUserVolunteerExperiences(string userid);
		public Task<VolunteerExperienceVM> GetVolunteerExperience(int id);
		public Task<bool> AddVolunteerExperience(VolunteerExperienceVM volunteerExperience, string userId);
		public Task<bool> RemoveVolunteerExperience(int volunteerExperienceId);
		public Task<bool> UpdateVolunteerExperience(VolunteerExperienceVM volunteerExperience);


		public Task<List<PhoneNumberVM>> GetUserPhoneNumbers(string userid);
		public Task<bool> AddPhoneNumber(PhoneNumberVM phoneNumber, string userId);
		public Task<bool> RemovePhoneNumber(int phoneNumberId);
		public Task<bool> UpdatePhoneNumber(PhoneNumberVM phoneNumber);


		public Task<List<WebsiteVM>> GetUserWebsites(string userid);
		public Task<bool> AddWebsite(WebsiteVM website, string userId);
		public Task<bool> RemoveWebsite(int websiteId);
		public Task<bool> UpdateWebsite(WebsiteVM website);


	}
}
