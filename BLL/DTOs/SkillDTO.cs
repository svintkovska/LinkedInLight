using DLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Data
{
	public class SkillDTO
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string ApplicationUserId { get; set; }
		public virtual UserDTO ApplicationUser { get; set; }


	}
}
