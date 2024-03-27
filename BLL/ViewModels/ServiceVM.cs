using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class ServiceVM
	{
		public int Id { get; set; }
		public string GeneralInformation { get; set; }
		public string Currency { get; set; }
		public decimal Salary { get; set; }
		public bool IsRemoteOk { get; set; }

		public string ApplicationUserId { get; set; }
		public ICollection<ServicePositionVM> ServicePositions { get; set; }
		public ICollection<ServiceCityVM> ServiceCities { get; set; }
		public ICollection<ServiceCountryVM> ServiceCountries { get; set; }

	}
}
