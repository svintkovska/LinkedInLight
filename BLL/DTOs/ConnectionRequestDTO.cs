using System;
using System.Collections.Generic;


namespace BLL.DTOs
{
	public class ConnectionRequestDTO
	{
		public int Id { get; set; }

		public string SenderId { get; set; }
		public virtual UserDTO Sender { get; set; }

		public string ReceiverId { get; set; }
		public virtual UserDTO Receiver { get; set; }

		public string Status { get; set; } 
	}
}
