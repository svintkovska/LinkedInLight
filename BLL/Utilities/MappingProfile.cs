using AutoMapper;
using BLL.ViewModels;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Utilities
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<ApplicationUser, UserProfileVM>()
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
				.ForMember(dest => dest.Experiences, opt => opt.MapFrom(src => src.Experiences))
				.ForMember(dest => dest.Educations, opt => opt.MapFrom(src => src.Educations))
				.ForMember(dest => dest.Posts, opt => opt.MapFrom(src => src.Posts))
				.ForMember(dest => dest.Certifications, opt => opt.MapFrom(src => src.Certifications))
				.ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.Projects))
				.ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.Courses))
				.ForMember(dest => dest.ReceivedRecommendations, opt => opt.MapFrom(src => src.ReceivedRecommendations))
				.ForMember(dest => dest.GivenRecommendations, opt => opt.MapFrom(src => src.GivenRecommendations))
				.ForMember(dest => dest.VolunteerExperiences, opt => opt.MapFrom(src => src.VolunteerExperiences))
				.ForMember(dest => dest.PhoneNumbers, opt => opt.MapFrom(src => src.PhoneNumbers))
				.ForMember(dest => dest.Websites, opt => opt.MapFrom(src => src.Websites));

			CreateMap<ApplicationUser, UserVM>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
				.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
				.ForMember(dest => dest.AdditionalName, opt => opt.MapFrom(src => src.AdditionalName));

			CreateMap<Recommendation, RecommendationVM>()
			.ForMember(dest => dest.Sender, opt => opt.MapFrom(src =>
				new UserVM
				{
					Id = src.Sender.Id,
					FirstName = src.Sender.FirstName,
					LastName = src.Sender.LastName,
					AdditionalName = src.Sender.AdditionalName
				}))
			.ForMember(dest => dest.Receiver, opt => opt.MapFrom(src =>
				new UserVM
				{
					Id = src.Receiver.Id,
					FirstName = src.Receiver.FirstName,
					LastName = src.Receiver.LastName,
					AdditionalName = src.Receiver.AdditionalName
				}));

			CreateMap<Education, EducationVM>()
                .ReverseMap();
            CreateMap<Certification, CertificationVM>()
				.ReverseMap();
			CreateMap<Course, CourseVM>()
				.ReverseMap();
			CreateMap<Recommendation, RecommendationVM>();
			CreateMap<VolunteerExperience, VolunteerExperienceVM>();
			CreateMap<PhoneNumber, PhoneNumberVM>();
			CreateMap<Website, WebsiteVM>();
			CreateMap<Skill, SkillVM>()
				.ReverseMap();
			CreateMap<UserSkill, UserSkillVM>()
			.ForMember(dest => dest.Skill, opt => opt.MapFrom(src =>
				new SkillVM
				{
					Id = src.Id,
					Name = src.Skill.Name,
				}));

			CreateMap<UserLanguage, UserLanguageVM>() ;
			CreateMap<Language, LanguageVM>();

			CreateMap<UserLanguage, UserLanguageVM>()
			.ForMember(dest => dest.Language, opt => opt.MapFrom(src =>
				new LanguageVM
				{
					Id = src.Id,
					Name = src.Language.Name,
				}));

			CreateMap<Comment, CommentVM>();
			CreateMap<Like, LikeVM>();
			CreateMap<Post, PostVM>()
				.ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
				.ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes));

			CreateMap<Experience, ExperienceVM>()
				.ForMember(dest => dest.Industry, opt => opt.MapFrom(src => new IndustryVM { Id = src.Industry.Id, Name = src.Industry.Name }))
				.ForMember(dest => dest.Company, opt => opt.MapFrom(src => new CompanyVM
				{
					Id = src.Company.Id,
					ComanyName = src.Company.ComanyName,
					LinkedinUrl = src.Company.LinkedinUrl,
					WebsiteUrl = src.Company.WebsiteUrl,
					OrganizationSize = src.Company.OrganizationSize,
					OrganizationType = src.Company.OrganizationType,
					LogoImg= src.Company.LogoImg,
					ApplicationUserId = src.Company.ApplicationUserId,
					IndustryId = src.Company.IndustryId
				}));

			CreateMap<ExperienceVM, Experience>()
				.ForMember(dest => dest.Industry, opt => opt.Ignore())
				.ForMember(dest => dest.Company, opt => opt.Ignore());

			CreateMap<Industry, IndustryVM>()
				.ReverseMap();
			CreateMap<Company, CompanyVM>()
				.ReverseMap();

			CreateMap<Project, ProjectVM>()
				.ForMember(dest => dest.ProjectContributors, opt => opt.MapFrom(src => src.ProjectContributors.Select(pc => new ProjectContributorVM
				{
					ProjectId = pc.ProjectId,
					ApplicationUserId = pc.ApplicationUserId
				})));


			CreateMap<ProjectContributor, ProjectContributorVM>();
			CreateMap<Country, CountryVM>();
			CreateMap<City, CityVM>();
			CreateMap<Position, PostVM>();
			CreateMap<OpenToWork, OpenToWorkVM>();
			CreateMap<OpenToWorkCity, OpenToWorkCityVM>();
			CreateMap<OpenToWorkCountry, OpenToWorkCountryVM>();
			CreateMap<OpenToWorkPosition, OpenToWorkPositionVM>();

		}
	}
}
