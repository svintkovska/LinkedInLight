using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
	public class UserLanguageVM
	{
		public int Id { get; set; }
		public int LanguageId { get; set; }
		public LanguageVM Language { get; set; }
		public string Proficiency { get; set; }
	}
}
