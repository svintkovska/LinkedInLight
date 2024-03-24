using AutoMapper;
using BLL.Interfaces;
using BLL.ViewModels;
using DLL.Repositories.IRepository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class AdditionalProfileService: ProfileService, IAdditionalProfileService
	{
		public AdditionalProfileService(IUploadService uploadService, IUnitOfWork unitOfWork, IMapper mapper)
			: base(uploadService, unitOfWork, mapper)
		{
		}

		public async Task<List<UserLanguageVM>> GetUserLanguages(string userid)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "UserLanguages.Language");
			var languageList = user.UserLanguages.ToList();
			var list = _mapper.Map<List<UserLanguageVM>>(languageList);

			return list;
		} 

		public async Task<List<LanguageVM>> GetAllLanguages()
		{
			var languages = await _unitOfWork.LanguageRepo.GetAll(); 
			var languageVMs = _mapper.Map<List<LanguageVM>>(languages);

	     	return languageVMs;
		}
		public async Task<bool> AddLanguage(UserLanguageVM language, string userId)
		{
			var selectedLanguage = await _unitOfWork.LanguageRepo.Get(l => l.Id == language.LanguageId);
			if (selectedLanguage == null)
			{
				return false;
			}

			var userLanguage = new UserLanguage
			{
				LanguageId = selectedLanguage.Id,
				Proficiency = language.Proficiency,
				ApplicationUserId = userId
			};
			await _unitOfWork.UserLanguageRepo.Add(userLanguage);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> RemoveLanguage(int languageId, string userId)
		{
			var language = await _unitOfWork.UserLanguageRepo.Get(s => s.Id == languageId && s.ApplicationUserId == userId);
			if (language == null)
			{
				return false;
			}

			_unitOfWork.UserLanguageRepo.Remove(language);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> UpdateLanguage(UserLanguageVM language)
		{
			var existingLanguage = await _unitOfWork.UserLanguageRepo.Get(e => e.Id == language.Id);
			if (existingLanguage == null)
			{
				return false; 
			}

			var updatedLanguage = await _unitOfWork.LanguageRepo.Get(l => l.Id == language.LanguageId);
			if (updatedLanguage == null)
			{
				return false; 
			}

			existingLanguage.LanguageId = language.LanguageId;
			existingLanguage.Proficiency = language.Proficiency;

			_unitOfWork.UserLanguageRepo.Update(existingLanguage);
			await _unitOfWork.SaveAsync();
			return true;
		}


		public async Task<List<VolunteerExperienceVM>> GetUserVolunteerExperiences(string userid)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "VolunteerExperiences");
			var volunteerExperienceList = user.VolunteerExperiences.ToList();
			var list = _mapper.Map<List<VolunteerExperienceVM>>(volunteerExperienceList);
			return list;
		}
		public async Task<VolunteerExperienceVM> GetVolunteerExperience(int id)
		{
			var experience = await _unitOfWork.VolunteerExperienceRepo.Get(e => e.Id == id);
			var volunteerExperience = _mapper.Map<VolunteerExperienceVM>(experience);
			return volunteerExperience;
		}
		public async Task<bool> AddVolunteerExperience(VolunteerExperienceVM volunteerExperience, string userId)
		{
			var mappedVolunteerExperience = _mapper.Map<VolunteerExperience>(volunteerExperience);
			mappedVolunteerExperience.ApplicationUserId= userId;
			await _unitOfWork.VolunteerExperienceRepo.Add(mappedVolunteerExperience);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> RemoveVolunteerExperience(int volunteerExperienceId)
		{
			var volunteerExperience = await _unitOfWork.VolunteerExperienceRepo.Get(e => e.Id == volunteerExperienceId);
			_unitOfWork.VolunteerExperienceRepo.Remove(volunteerExperience);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> UpdateVolunteerExperience(VolunteerExperienceVM volunteerExperience)
		{
			var existingVolunteerExperience = await _unitOfWork.VolunteerExperienceRepo.Get(e => e.Id == volunteerExperience.Id);
			if (existingVolunteerExperience == null)
			{
				return false;
			}

			existingVolunteerExperience.Organization = volunteerExperience.Organization;
			existingVolunteerExperience.Description = volunteerExperience.Description;
			existingVolunteerExperience.StartDate = volunteerExperience.StartDate;
			existingVolunteerExperience.EndDate = volunteerExperience.EndDate;
			existingVolunteerExperience.Role = volunteerExperience.Role;
			existingVolunteerExperience.Cause = volunteerExperience.Cause;
			existingVolunteerExperience.CurrentlyVolunteering = volunteerExperience.CurrentlyVolunteering;

			_unitOfWork.VolunteerExperienceRepo.Update(existingVolunteerExperience);
			await _unitOfWork.SaveAsync();
			return true;
		}


		public async Task<List<PhoneNumberVM>> GetUserPhoneNumbers(string userid)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "PhoneNumbers");
			var phoneNumberlist = user.PhoneNumbers.ToList();
			var list = _mapper.Map<List<PhoneNumberVM>>(phoneNumberlist);
			return list;
		}
		public async Task<bool> AddPhoneNumber(PhoneNumberVM phoneNumber, string userId)
		{
			var mappedPhoneNumber = _mapper.Map<PhoneNumber>(phoneNumber);
			mappedPhoneNumber.UserId = userId;
			await _unitOfWork.PhoneNumberRepo.Add(mappedPhoneNumber);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> RemovePhoneNumber(int phoneNumberId)
		{
			var phoneNumber = await _unitOfWork.PhoneNumberRepo.Get(e => e.Id == phoneNumberId);
			_unitOfWork.PhoneNumberRepo.Remove(phoneNumber);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> UpdatePhoneNumber(PhoneNumberVM phoneNumber)
		{
			var existingPhoneNumber = await _unitOfWork.PhoneNumberRepo.Get(e => e.Id == phoneNumber.Id);
			if (existingPhoneNumber == null)
			{
				return false;
			}

			existingPhoneNumber.Number = phoneNumber.Number;
			existingPhoneNumber.Type = phoneNumber.Type;


			_unitOfWork.PhoneNumberRepo.Update(existingPhoneNumber);
			await _unitOfWork.SaveAsync();
			return true;
		}


		public async Task<List<WebsiteVM>> GetUserWebsites(string userid)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "Websites");
			var websiteList = user.Websites.ToList();
			var list = _mapper.Map<List<WebsiteVM>>(websiteList);
			return list;
		}
		public async Task<bool> AddWebsite(WebsiteVM website, string userId)
		{
			var mappedWebsite = _mapper.Map<Website>(website);
			mappedWebsite.UserId = userId;
			await _unitOfWork.WebsiteRepo.Add(mappedWebsite);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> RemoveWebsite(int websiteId)
		{
			var website = await _unitOfWork.WebsiteRepo.Get(e => e.Id == websiteId);
			_unitOfWork.WebsiteRepo.Remove(website);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> UpdateWebsite(WebsiteVM website)
		{
			var existingWebsite = await _unitOfWork.WebsiteRepo.Get(e => e.Id == website.Id);
			if (existingWebsite == null)
			{
				return false;
			}

			existingWebsite.Url = website.Url;
			existingWebsite.Type = website.Type;


			_unitOfWork.WebsiteRepo.Update(existingWebsite);
			await _unitOfWork.SaveAsync();
			return true;
		}
	}
}
