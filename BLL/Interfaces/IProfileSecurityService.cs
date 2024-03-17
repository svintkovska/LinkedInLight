using BLL.ViewModels.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface IProfileSecurityService
	{
		public Task<bool> ChangePassword(ChangePasswordVM model);

	}
}
