using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class ServiceCity
	{
		public int ServiceId { get; set; }
		public Service Service { get; set; }

		public int CityId { get; set; }
		public City City { get; set; }
	}

}
