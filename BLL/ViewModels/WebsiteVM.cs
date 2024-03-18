using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
	public class WebsiteVM
	{
		public int Id { get; set; }
		public string Url { get; set; }
		public string Type { get; set; } 
		public string UserId { get; set; }
	}
}
