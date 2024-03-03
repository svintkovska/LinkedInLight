using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class Skill
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string ApplicationUserId { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }


	}
}
