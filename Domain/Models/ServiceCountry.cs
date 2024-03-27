﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class ServiceCountry
	{
		public int ServiceId { get; set; }
		public virtual Service Service { get; set; }

		public int CountryId { get; set; }
		public virtual Country Country { get; set; }
	}

}
