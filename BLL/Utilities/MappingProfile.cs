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
				.ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills))
				.ForMember(dest => dest.Languages, opt => opt.MapFrom(src => src.Languages))
				.ForMember(dest => dest.Posts, opt => opt.MapFrom(src => src.Posts))
				.ForMember(dest => dest.Certifications, opt => opt.MapFrom(src => src.Certifications))
				.ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.Projects))
				.ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.Courses))
				.ForMember(dest => dest.ReceivedRecommendations, opt => opt.MapFrom(src => src.ReceivedRecommendations))
				.ForMember(dest => dest.GivenRecommendations, opt => opt.MapFrom(src => src.GivenRecommendations))
				.ForMember(dest => dest.VolunteerExperiences, opt => opt.MapFrom(src => src.VolunteerExperiences))
				.ForMember(dest => dest.PhoneNumbers, opt => opt.MapFrom(src => src.PhoneNumbers))
				.ForMember(dest => dest.Websites, opt => opt.MapFrom(src => src.Websites));

			CreateMap<Education, EducationVM>();
			CreateMap<Certification, CertificationVM>();
			CreateMap<Course, CourseVM>();
			CreateMap<Recommendation, RecommendationVM>();
			CreateMap<VolunteerExperience, VolunteerExperienceVM>();
			CreateMap<PhoneNumber, PhoneNumberVM>();
			CreateMap<Website, WebsiteVM>();
			CreateMap<Skill, SkillVM>()
				.ReverseMap();
			CreateMap<Language, LanguageVM>()
				.ReverseMap();
			CreateMap<Comment, CommentVM>();
			CreateMap<Like, LikeVM>();
			CreateMap<Post, PostVM>()
				.ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
				.ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes));

			CreateMap<Experience, ExperienceVM>()
				.ForMember(dest => dest.Industry, opt => opt.MapFrom(src => new IndustryVM { Id = src.Industry.Id, Name = src.Industry.Name }));

			CreateMap<Industry, IndustryVM>();


			CreateMap<Project, ProjectVM>()
				.ForMember(dest => dest.ProjectContributors, opt => opt.MapFrom(src => src.ProjectContributors.Select(pc => new ProjectContributorVM
				{
					ProjectId = pc.ProjectId,
					ApplicationUserId = pc.ApplicationUserId
				})));


			CreateMap<ProjectContributor, ProjectContributorVM>();
			CreateMap<Country, CountryVM>();
			CreateMap<City, CityVM>();

		}
	}
}
