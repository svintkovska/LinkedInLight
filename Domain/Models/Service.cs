using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class Service
	{
		[Key]
		public int Id { get; set; }
		public string GeneralInformation { get; set; }
		public string Currency { get; set; }
		public decimal Salary { get; set; }
		public bool IsRemoteOk { get; set; }

		public string ApplicationUserId { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }
		public virtual ICollection<ServicePosition> ServicePositions { get; set; }
		public virtual ICollection<ServiceCity> ServiceCities { get; set; }
		public virtual ICollection<ServiceCountry> ServiceCountries { get; set; }

	}
}
