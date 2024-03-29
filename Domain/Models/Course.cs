﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class Course
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string Number { get; set; }
		public string AssociatedWith { get; set; }
		public string ApplicationUserId { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }
	}
}
