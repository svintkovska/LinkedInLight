using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
	public class LikeDTO
	{
		[Key]
		public int Id { get; set; }
		public int? PostId { get; set; }
		public virtual PostDTO Post { get; set; }
		public int? CommentId { get; set; }
		public virtual CommentDTO Comment { get; set; }
		public string ApplicationUserId { get; set; }
		public virtual UserDTO ApplicationUser { get; set; }
	}
}
