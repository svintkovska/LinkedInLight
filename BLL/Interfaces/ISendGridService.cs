﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface ISendGridService
	{
		public Task SendEmailAsync(string email, string subject, string htmlMessage);

	}
}
