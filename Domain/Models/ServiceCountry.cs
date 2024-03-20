using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class ServiceCountry
	{
		public int ServiceId { get; set; }
		public Service Service { get; set; }

		public int CountryId { get; set; }
		public Country Country { get; set; }
	}

}
