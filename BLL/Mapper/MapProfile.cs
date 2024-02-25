using AutoMapper;
using BLL.DTOs;
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
		}
	}
}

