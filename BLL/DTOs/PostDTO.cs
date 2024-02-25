using BLL.DTOs;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class PostDTO
	{
		[Key]
		public int Id { get; set; }
		public string Content { get; set; }
		public string? Image { get; set; }
		public DateTime DatePosted { get; set; }

		public string ApplicationUserId { get; set; }
		public virtual UserDTO ApplicationUser { get; set; }

		public virtual ICollection<CommentDTO> Comments { get; set; }
		public virtual ICollection<LikeDTO> Likes { get; set; }
	}
}
