using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class OpenToWorkCity
	{
		public int OpenToWorkId { get; set; }
		public virtual OpenToWork OpenToWork { get; set; }

		public int CityId { get; set; }
		public virtual City City { get; set; }
	}
}
