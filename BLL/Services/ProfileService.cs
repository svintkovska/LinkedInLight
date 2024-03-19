using AutoMapper;
using BLL.Interfaces;
using BLL.ViewModels;
using BLL.ViewModels.AuthModels;
using DLL.Data;
using DLL.Repositories;
using DLL.Repositories.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class ProfileService: IProfileService
	{

		private readonly IUploadService _uploadService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ProfileService(IUploadService uploadService, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_uploadService = uploadService;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

	
		public async Task<UserProfileVM> EditImage(string userId, string newImage, bool background = false)
		{
			var user =  await _unitOfWork.UserRepo.Get(u=>u.Id== userId);
			if (!background)
			{

				if (!string.IsNullOrEmpty(newImage) && newImage.Split(',').Length == 2)
				{
					if (user.Image != null)
					{
						_uploadService.RemoveImage(user.Image);
					}
					user.Image = _uploadService.SaveImageFromBase64(newImage);
				}

				if (string.IsNullOrEmpty(newImage) && user.Image != null)
				{
					_uploadService.RemoveImage(user.Image);
					user.Image = null;
				}


			}
			else
			{
				if (!string.IsNullOrEmpty(newImage) && newImage.Split(',').Length == 2)
				{
					if (user.Background != null)
					{
						_uploadService.RemoveImage(user.Background);
					}
					user.Background = _uploadService.SaveImageFromBase64(newImage);
				}

				if (string.IsNullOrEmpty(newImage) && user.Background != null)
				{
					_uploadService.RemoveImage(user.Background);
					user.Background = null;
                }
            }

            _unitOfWork.UserRepo.Update(user);

            await _unitOfWork.SaveAsync();
			var userProfile = await GetUserProfile(user.Id);
			return userProfile;
		}

		public async Task<UserProfileVM> GetUserProfile(string id)
		{
			var user = await _unitOfWork.UserRepo.GetUserProfileProps(id);

			var userProfile = _mapper.Map<ApplicationUser, UserProfileVM>(user);

			return userProfile;
		}

		public async Task<bool> EditAbout(string id, string about)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == id);
			if(user != null)
			{
				user.About = about;
				_unitOfWork.UserRepo.Update(user);
				await _unitOfWork.SaveAsync();
				return true;
			}
			return false;
		}

		public async Task<List<ExperienceVM>> GetUserExperiences(string userid)
		{
			var experienceList = await _unitOfWork.ExperienceRepo.GetUserExperiencesWithIndustry(userid);
			var list = _mapper.Map<List<ExperienceVM>>(experienceList);
			return list;
		}
		public async Task<ExperienceVM> GetExperience(int id)
		{
			var exp = await _unitOfWork.ExperienceRepo.Get(e => e.Id == id, includeProperties: "Industry");
			var experience = _mapper.Map<ExperienceVM>(exp);
			return experience;
		}
		public async Task<bool> AddExperience(ExperienceVM experience)
		{
			var mappedExperience = _mapper.Map<Experience>(experience);

			await _unitOfWork.ExperienceRepo.Add(mappedExperience);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> RemoveExperience(int experienceId)
		{
			var experience = await _unitOfWork.ExperienceRepo.Get(e => e.Id == experienceId);
			_unitOfWork.ExperienceRepo.Remove(experience);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> UpdateExperience(ExperienceVM experience)
		{
			var existingExperience = await _unitOfWork.ExperienceRepo.Get(e => e.Id == experience.Id);
			if (existingExperience == null)
			{
				return false;
			}

			existingExperience.Title = experience.Title;
			existingExperience.CompanyName = experience.CompanyName;
			existingExperience.Description = experience.Description;
			existingExperience.StartDate = experience.StartDate;
			existingExperience.EndDate = experience.EndDate;
			existingExperience.CurrentlyWorking = experience.CurrentlyWorking;
			existingExperience.ProfileHeadline = experience.ProfileHeadline;
			existingExperience.IndustryId = experience.Industry.Id;

			_unitOfWork.ExperienceRepo.Update(existingExperience);
			await _unitOfWork.SaveAsync();
			return true;
		}

		public async Task<List<EducationVM>> GetUserEducations(string userid)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "Educations");
			var educationList = user.Educations.ToList();
			var list = _mapper.Map<List<EducationVM>>(educationList);
			return list;
		}
		public async Task<EducationVM> GetEducation(int id)
		{
			var edu = await _unitOfWork.EducationRepo.Get(e => e.Id == id);
			var education = _mapper.Map<EducationVM>(edu);
			return education;
		}
		public async Task<bool> AddEducation(EducationVM education)
		{
			var mappedEducation = _mapper.Map<Education>(education);

			await _unitOfWork.EducationRepo.Add(mappedEducation);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> RemoveEducation(int educationId)
		{
			var education = await _unitOfWork.EducationRepo.Get(e => e.Id == educationId);
			_unitOfWork.EducationRepo.Remove(education);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> UpdateEducation(EducationVM education)
		{
			var existingEducation = await _unitOfWork.EducationRepo.Get(e => e.Id == education.Id);
			if (existingEducation == null)
			{
				return false;
			}

			existingEducation.Description = education.Description;
			existingEducation.StartDate = education.StartDate;
			existingEducation.EndDate = education.EndDate;
			existingEducation.CurrentlyStudying= education.CurrentlyStudying;
			existingEducation.Degree = education.Degree;
			existingEducation.School = education.School;
			existingEducation.Grade = education.Grade;
			existingEducation.FieldOfStudy = education.FieldOfStudy;

			_unitOfWork.EducationRepo.Update(existingEducation);
			await _unitOfWork.SaveAsync();
			return true;
		}

		public async Task<List<PostVM>> GetUserPosts(string userid)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "Posts");
			var postList = user.Posts.ToList();
			var list = _mapper.Map<List<PostVM>>(postList);

			return list;
		}

		public async Task<List<SkillVM>> GetUserSkills(string userid)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "Skills");
			var skillList = user.Skills.ToList();
			var list  = _mapper.Map<List<SkillVM>>(skillList);

			return list;
		}
		public async Task<bool> AddSkill(SkillVM skill)
		{
			var mappedSkill = _mapper.Map<Skill>(skill);

			await _unitOfWork.SkillRepo.Add(mappedSkill);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> RemoveSkill(int skillId)
		{
			var skill = await _unitOfWork.SkillRepo.Get(s => s.Id == skillId);
			_unitOfWork.SkillRepo.Remove(skill);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> UpdateSkill (SkillVM skill)
		{
			var existingSkill = await _unitOfWork.SkillRepo.Get(e => e.Id == skill.Id);
			if (existingSkill == null)
			{
				return false;
			}

			existingSkill.Name = skill.Name;
			existingSkill.IsMainSkill = skill.IsMainSkill;
			

			_unitOfWork.SkillRepo.Update(existingSkill);
			await _unitOfWork.SaveAsync();
			return true;
		}

		public async Task<List<LanguageVM>> GetUserLanguages(string userid)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "Languages");
			var languageList = user.Languages.ToList();
			var list = _mapper.Map<List<LanguageVM>>(languageList);

			return list;
		}
		public async Task<bool> AddLanguage(LanguageVM language)
		{
			var mappedLanguage = _mapper.Map<Language>(language);

			await _unitOfWork.LanguageRepo.Add(mappedLanguage);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> RemoveLanguage(int languageId)
		{
			var language = await _unitOfWork.LanguageRepo.Get(s => s.Id == languageId);
			_unitOfWork.LanguageRepo.Remove(language);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> UpdateLanguage(LanguageVM language)
		{
			var existingLanguage = await _unitOfWork.LanguageRepo.Get(e => e.Id == language.Id);
			if (existingLanguage == null)
			{
				return false;
			}

			existingLanguage.Name = language.Name;
			existingLanguage.Proficiency = language.Proficiency;


			_unitOfWork.LanguageRepo.Update(existingLanguage);
			await _unitOfWork.SaveAsync();
			return true;
		}


		public async Task<List<CertificationVM>> GetUserCertifications(string userid)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "Certifications");
			var certificationList = user.Certifications.ToList();
			var list = _mapper.Map<List<CertificationVM>>(certificationList);
			return list;
		}
		public async Task<CertificationVM> GetCertification(int id)
		{
			var crt = await _unitOfWork.CertificationRepo.Get(e => e.Id == id);
			var certification = _mapper.Map<CertificationVM>(crt);
			return certification;
		}
		public async Task<bool> AddCertification(CertificationVM certification)
		{
			var mappedCertification = _mapper.Map<Certification>(certification);

			await _unitOfWork.CertificationRepo.Add(mappedCertification);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> RemoveCertification(int certificationId)
		{
			var certification = await _unitOfWork.CertificationRepo.Get(e => e.Id == certificationId);
			_unitOfWork.CertificationRepo.Remove(certification);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> UpdateCertification(CertificationVM certification)
		{
			var existingCertification = await _unitOfWork.CertificationRepo.Get(e => e.Id == certification.Id);
			if (existingCertification == null)
			{
				return false; 
			}

			existingCertification.Name = certification.Name;
			existingCertification.IssuingOrganization = certification.IssuingOrganization;
			existingCertification.IssueDate = certification.IssueDate;
			existingCertification.ExpirationDate = certification.ExpirationDate;
			existingCertification.CredentialURL = certification.CredentialURL;
			existingCertification.CredentialId = certification.CredentialId;

			_unitOfWork.CertificationRepo.Update(existingCertification);
			await _unitOfWork.SaveAsync();
			return true;
		}



		public async Task<List<CourseVM>> GetUserCourses(string userid)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "Courses");
			var coursesList = user.Courses.ToList();
			var list = _mapper.Map<List<CourseVM>>(coursesList);
			return list;
		}
		public async Task<CourseVM> GetCourse(int id)
		{
			var crs = await _unitOfWork.CourseRepo.Get(e => e.Id == id);
			var course = _mapper.Map<CourseVM>(crs);
			return course;
		}
		public async Task<bool> AddCourse(CourseVM course)
		{
			var mappedCourse = _mapper.Map<Course>(course);

			await _unitOfWork.CourseRepo.Add(mappedCourse);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> RemoveCourse(int courseId)
		{
			var course = await _unitOfWork.CourseRepo.Get(e => e.Id == courseId);
			_unitOfWork.CourseRepo.Remove(course);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> UpdateCourse(CourseVM course)
		{
			var existingCourse = await _unitOfWork.CourseRepo.Get(e => e.Id == course.Id);
			if (existingCourse == null)
			{
				return false;
			}

			existingCourse.Name = course.Name;
			existingCourse.Number = course.Number;
			existingCourse.AssociatedWith = course.AssociatedWith;
		

			_unitOfWork.CourseRepo.Update(existingCourse);
			await _unitOfWork.SaveAsync();
			return true;
		}


		public async Task<List<PhoneNumberVM>> GetPhoneNumbers(string userid)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "PhoneNumbers");
			var phoneNumberlist = user.PhoneNumbers.ToList();
			var list = _mapper.Map<List<PhoneNumberVM>>(phoneNumberlist);
			return list;
		}
		public async Task<bool> AddPhoneNumber(PhoneNumberVM phoneNumber)
		{
			var mappedPhoneNumber = _mapper.Map<PhoneNumber>(phoneNumber);

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


		public async Task<List<WebsiteVM>> GetWebsites(string userid)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "Websites");
			var websiteList = user.Websites.ToList();
			var list = _mapper.Map<List<WebsiteVM>>(websiteList);
			return list;
		}
		public async Task<bool> AddWebsite(WebsiteVM website)
		{
			var mappedWebsite = _mapper.Map<Website>(website);

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
