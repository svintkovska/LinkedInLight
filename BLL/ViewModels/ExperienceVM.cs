using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
	public class ExperienceVM
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string CompanyName { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string Description { get; set; }
		public string ProfileHeadline { get; set; }
		public IndustryVM Industry { get; set; }
	}
}
