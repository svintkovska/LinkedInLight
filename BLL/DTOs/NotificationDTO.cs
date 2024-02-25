using System.ComponentModel.DataAnnotations;



namespace BLL.DTOs
{
	public class NotificationDTO
	{
		[Key]
		public int Id { get; set; }
		public string Content { get; set; }
		public bool IsRead { get; set; }
		public DateTime CreatedAt { get; set; }

		public string ApplicationUserId { get; set; }
		public virtual UserDTO ApplicationUser { get; set; }
	}
}
