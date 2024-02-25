using BLL.DTOs;


namespace BLL.DTOs
{
	public class CommentDTO
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; }
		public int PostId { get; set; }
		public virtual PostDTO Post { get; set; }
		public string ApplicationUserId { get; set; }
		public virtual UserDTO ApplicationUser { get; set; }
		public virtual ICollection<LikeDTO> Likes { get; set; }
	}
}
