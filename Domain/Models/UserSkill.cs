using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class UserSkill
	{
		[Key]
		public int Id { get; set; }
		public bool IsMainSkill { get; set; }
		public int SkillId { get; set; }
		public virtual Skill Skill { get; set; }
		public string ApplicationUserId { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }
	}
}
