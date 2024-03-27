﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class ServiceCity
	{
		public int ServiceId { get; set; }
		public virtual Service Service { get; set; }

		public int CityId { get; set; }
		public virtual City City { get; set; }
	}

}
