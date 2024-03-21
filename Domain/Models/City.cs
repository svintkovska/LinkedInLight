using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class City
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int CountryId { get; set; }
		public virtual Country Country { get; set; }
		public virtual ICollection<ServiceCity> ServiceCities { get; set; }

	}
}
