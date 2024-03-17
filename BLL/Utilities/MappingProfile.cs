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
			.ForMember(dest => dest.Posts, opt => opt.MapFrom(src => src.Posts));

			CreateMap<Education, EducationVM>();
			CreateMap<Skill, SkillVM>();
			CreateMap<Language, LanguageVM>();
			CreateMap<Comment, CommentVM>();
			CreateMap<Like, LikeVM>();
			CreateMap<Post, PostVM>()
				.ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
				.ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes));

			CreateMap<Experience, ExperienceVM>()
				.ForMember(dest => dest.Industry, opt => opt.MapFrom(src => new IndustryVM { Id = src.Industry.Id, Name = src.Industry.Name }));
		}
	}
}
