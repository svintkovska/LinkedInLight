﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
	public class LikeVM
	{
		public int Id { get; set; }
		public int PostId { get; set; }
		public string ApplicationUserId { get; set; }

	}
}
