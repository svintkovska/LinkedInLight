using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	
	public class Country
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<City> Cities { get; set; }
		public virtual ICollection<ServiceCountry> ServiceCountries { get; set; }

	}
}
