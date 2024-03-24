using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
	public class UserSkillVM
	{
		public int Id { get; set; }
		public bool IsMainSkill { get; set; }
		public int SkillId { get; set; }
		public SkillVM Skill { get; set; }
		public string ApplicationUserId { get; set; }
	}
}
