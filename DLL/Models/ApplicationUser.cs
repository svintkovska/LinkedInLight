﻿using DLL.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? AdditionalName { get; set; }
        public string? Headline { get; set; }
        public string? CurrentPosition { get; set; }
        public string? Industry { get; set; }
        [Required]
        public string Country { get; set; }

        public string City { get; set; }
        [Required]
        public string About { get; set; }
		public virtual ICollection<Connection> Connections { get; set; }
        public virtual ICollection<ConnectionRequest> SentConnectionRequests { get; set; }
        public virtual ICollection<ConnectionRequest> ReceivedConnectionRequests { get; set; }
		public virtual ICollection<Post> Posts { get; set; }
		public virtual ICollection<Comment> Comments { get; set; }
		public virtual ICollection<Like> Likes { get; set; }
		public virtual ICollection<Notification> Notifications { get; set; }
		public bool IsRecruiter { get; set; }
		public virtual ICollection<JobPosting> JobPostings { get; set; }
	}
}
