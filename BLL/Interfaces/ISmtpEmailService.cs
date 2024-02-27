﻿using BLL.ViewModels;
using DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface ISmtpEmailService
	{
		public void Send(SmtpMessage message);
	}
}
