using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
	public class OpenToWorkVM
	{
		public int Id { get; set; }
		
		public bool CanStartImmediately { get; set; }
		public bool FullTime { get; set; }
		public bool PartTime { get; set; }
		public bool Internship { get; set; }
		public bool Contract { get; set; }
		public bool Temporary { get; set; }
		public bool VisibleForAll { get; set; }
		public string ApplicationUserId { get; set; }
		public ICollection<OpenToWorkPositionVM> OpenToWorkPositions { get; set; }
		public ICollection<OpenToWorkCityVM> OpenToWorkCities { get; set; }
		public ICollection<OpenToWorkCountryVM> OpenToWorkCountries { get; set; }


	}
}
