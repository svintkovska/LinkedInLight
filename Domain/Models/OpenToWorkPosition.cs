using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class OpenToWorkPosition
	{
		public int OpenToWorkId { get; set; }
		public OpenToWork OpenToWork { get; set; }

		public int PositionId { get; set; }
		public Position Position { get; set; }
	}
}
