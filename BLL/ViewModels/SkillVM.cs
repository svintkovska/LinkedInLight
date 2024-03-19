using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
	public class SkillVM
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsMainSkill { get; set; }
		public string ApplicationUserId { get; set; }
	}
}
