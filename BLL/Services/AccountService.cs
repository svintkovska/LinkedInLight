using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using BLL.ViewModels.AuthModels;
using DLL.Models;
using DLL.Repositories.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class AccountService: IAccountService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IUploadService _uploadService;
		private readonly IMapper _mapper;
		private readonly IApplicationUserRepository _userRepository;
		public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUploadService uploadService, IMapper mapper, IApplicationUserRepository userRepository)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_uploadService = uploadService;
			_mapper = mapper;
			_userRepository = userRepository;
		}

		public async Task<bool> ChangePassword( ChangePasswordVM model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				throw new Exception("User not found");
			}
			var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
			if (!result.Succeeded)
			{
				throw new Exception("Error when changing password");
			}
			return result.Succeeded;

		}
		public async Task<UserDTO> EditImage(UserDTO userDTO, string username, bool background = false)
		{
			var user =  await _userRepository.Get(u=>u.UserName== username);
			if (!background)
			{
				if (!string.IsNullOrEmpty(userDTO.Image) && userDTO.Image.Split(',').Length == 2)
				{
					if (user.Image != null)
					{
						_uploadService.RemoveImage(user.Image);
					}
					user.Image = _uploadService.SaveImageFromBase64(userDTO.Image);
				}

				if (string.IsNullOrEmpty(userDTO.Image) && user.Image != null)
				{
					_uploadService.RemoveImage(user.Image);
					user.Image = null;
				}
			}
			else
			{
				if (!string.IsNullOrEmpty(userDTO.Background) && userDTO.Background.Split(',').Length == 2)
				{
					if (user.Background != null)
					{
						_uploadService.RemoveImage(user.Background);
					}
					user.Background = _uploadService.SaveImageFromBase64(userDTO.Background);
				}

				if (string.IsNullOrEmpty(userDTO.Background) && user.Background != null)
				{
					_uploadService.RemoveImage(user.Background);
					user.Background = null;
				}
			}

			await _userRepository.Save();
			return _mapper.Map<UserDTO>(user);
		}
	}
}
