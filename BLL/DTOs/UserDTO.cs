using BLL.DTOs;
using Microsoft.AspNetCore.Identity;

namespace BLL.DTOs
{
    public class UserDTO : IdentityUser
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? AdditionalName { get; set; }
        public string? Headline { get; set; }
        public string? CurrentPosition { get; set; }
        public string? Industry { get; set; }
        public string Country { get; set; }

        public string City { get; set; }
        public string About { get; set; }
		public string Image { get; set; }
		public string Background { get; set; }
		public ICollection<ConnectionDTO> Connections { get; set; } = new List<ConnectionDTO>();
		public ICollection<ConnectionRequestDTO> SentConnectionRequests { get; set; } = new List<ConnectionRequestDTO>();
		public ICollection<ConnectionRequestDTO> ReceivedConnectionRequests { get; set; } = new List<ConnectionRequestDTO>();
		public ICollection<PostDTO> Posts { get; set; } = new List<PostDTO>();
		public ICollection<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
		public ICollection<LikeDTO> Likes { get; set; } = new List<LikeDTO>();
		public ICollection<NotificationDTO> Notifications { get; set; } = new List<NotificationDTO>();
		public ICollection<JobPostingDTO> JobPostings { get; set; } = new List<JobPostingDTO>();
	}
}
