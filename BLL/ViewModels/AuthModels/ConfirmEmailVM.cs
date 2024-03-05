using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.AuthModels
{
	public class ConfirmEmailVM
	{
		public string UserId { get; set; }
		public string Code { get; set; }
	}
}
