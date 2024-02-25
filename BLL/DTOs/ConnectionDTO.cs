using System;


namespace BLL.DTOs
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
