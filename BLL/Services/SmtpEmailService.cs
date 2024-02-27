using BLL.Interfaces;
using BLL.ViewModels;
using DLL.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class SmtpEmailService : ISmtpEmailService
	{
		private readonly EmailConfiguration _configuration;
		private readonly IConfiguration _config;

		public SmtpEmailService(IConfiguration config)
		{
			_config = config;
			_configuration = new EmailConfiguration()
			{
				From = "tanyasv_97@ukr.net",
				SmtpServer = "smtp.ukr.net",
				Port = 2525,
				UserName = "tanyasv_97@ukr.net",
				Password = config.GetValue<string>("smtpPassword")
			};
		}

		public void Send(SmtpMessage message)
		{
			var body = new TextPart("html")
			{
				Text = message.Body,
			};

			var multipart = new Multipart("mixed");
			multipart.Add(body);


			var emailMessage = new MimeMessage();
			emailMessage.From.Add(new MailboxAddress(_configuration.From, _configuration.From));
			emailMessage.To.Add(new MailboxAddress(message.To, message.To));
			emailMessage.Subject = message.Subject;
			emailMessage.Body = multipart;

			using (var client = new SmtpClient())
			{
				try
				{
					client.Connect(_configuration.SmtpServer, _configuration.Port, true);
					client.Authenticate(_configuration.UserName, _configuration.Password);
					client.Send(emailMessage);
				}
				catch (Exception ex)
				{
					System.Console.WriteLine("Send message error {0}", ex.Message);
				}
				finally
				{
					client.Disconnect(true);
					client.Dispose();
				}
			}
		}
	}
}
