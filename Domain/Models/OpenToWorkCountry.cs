using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class OpenToWorkCountry
	{
		public int OpenToWorkId { get; set; }
		public virtual OpenToWork OpenToWork { get; set; }

		public int CountryId { get; set; }
		public virtual Country Country { get; set; }
	}
}
