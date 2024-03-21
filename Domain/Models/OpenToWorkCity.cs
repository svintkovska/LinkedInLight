﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class OpenToWorkCity
	{
		public int OpenToWorkId { get; set; }
		public OpenToWork OpenToWork { get; set; }

		public int CityId { get; set; }
		public City City { get; set; }
	}
}
