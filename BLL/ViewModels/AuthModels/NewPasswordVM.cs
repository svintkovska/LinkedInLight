using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.AuthModels
{
    public class NewPasswordVM
    {
		public string UserId { get; set; }
		public string Token { get; set; }
		public string NewPassword { get; set; }
	}
}
