using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
	public class ServicePosition
	{
		public int ServiceId { get; set; }
		public Service Service { get; set; }

		public int PositionId { get; set; }
		public Position Position { get; set; }
	}
}
