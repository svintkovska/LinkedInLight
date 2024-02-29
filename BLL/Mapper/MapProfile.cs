using AutoMapper;
using BLL.DTOs;
using DLL.Data;
using DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapper
{
	public class MapProfile : Profile
	{
		public MapProfile()
		{
			CreateMap<UserDTO, ApplicationUser>();
			CreateMap<ApplicationUser, UserDTO>();
			CreateMap<Experience, ExperienceDTO>();
			CreateMap<ExperienceDTO, Experience>();
			CreateMap<Education, EducationDTO>();
			CreateMap<EducationDTO, Education>();

		}
	}
}

