using BLL.DTOs;
using BLL.ViewModels.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface IAccountService
	{
		public Task<bool> ChangePassword(ChangePasswordVM model);
		public Task<UserDTO> EditImage(UserDTO userDTO, string username, bool background = false);
	}
}
