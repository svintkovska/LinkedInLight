using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class UserLanguage
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int LanguageId { get; set; }
		public virtual Language Language { get; set; }
		public string Proficiency { get; set; }
		public string ApplicationUserId { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }


	}
}
