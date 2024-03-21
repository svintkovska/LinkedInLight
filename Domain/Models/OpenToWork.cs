using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class OpenToWork
	{
		[Key]
		public int Id { get; set; }
		
		public bool CanStartImmediately { get; set; }
		public bool FullTime { get; set; }
		public bool PartTime { get; set; }
		public bool Internship { get; set; }
		public bool Contract { get; set; }
		public bool Temporary { get; set; }
		public bool VisibleForAll { get; set; }

		public string ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public virtual ICollection<OpenToWorkPosition> OpenToWorkPositions { get; set; }
		public virtual ICollection<OpenToWorkCity> OpenToWorkCities { get; set; }
		public virtual ICollection<OpenToWorkCountry> OpenToWorkCountries { get; set; }


	}
}
