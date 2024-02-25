using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
	public class ConnectionDTO
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public virtual UserDTO User { get; set; }

		public string ConnectedUserId { get; set; }
		public virtual UserDTO ConnectedUser { get; set; }
	}
}
