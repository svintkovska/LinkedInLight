using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using System.Net;
using BLL.Utilities;
using BLL.Interfaces;

namespace BLL.Services
{
	public class SendGridService: ISendGridService
	{
		private readonly SendGridClient _sendGridClient;

		public SendGridService(IOptions<SendGridOptions> options)
		{
			_sendGridClient = new SendGridClient(options.Value.ApiKey);
		}

		public async Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			var msg = new SendGridMessage()
			{
				From = new EmailAddress("noreply.jobforyou@ukr.net", "NoReply"),
				Subject = subject,
				PlainTextContent = htmlMessage,
				HtmlContent = htmlMessage
			};
			msg.AddTo(new EmailAddress(email));

			var response = await _sendGridClient.SendEmailAsync(msg);
			if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Accepted)
			{
				throw new Exception($"Failed to send email. Status code: {response.StatusCode}");
			}
		}
	}
}
