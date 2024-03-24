using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
	public class VolunteerExperienceVM
	{
		public int Id { get; set; }
		public string Organization { get; set; }
		public string Role { get; set; }
		public string Cause { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public bool CurrentlyVolunteering { get; set; }
		public string Description { get; set; }
		public string ApplicationUserId { get; set; }
	}
}
