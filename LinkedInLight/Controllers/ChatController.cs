using BLL.Interfaces;
using BLL.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace LinkedInLight.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ChatController : ControllerBase
	{
		private readonly IChatService _chatService;
		private readonly UserManager<ApplicationUser> _userManager;

		public ChatController(IChatService chatService, UserManager<ApplicationUser> userManager)
		{
			_chatService = chatService;
			_userManager = userManager;
		}

		[HttpGet]
		public async Task<ActionResult<List<Chat>>> GetChatsForUser()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (userId == null)
			{
				throw new InvalidOperationException("User not found in claims.");
			}
			var chats = await _chatService.GetChatsForUser(userId);
			return Ok(chats);
		}

		[HttpGet("{chatId}")]
		public async Task<ActionResult<List<Message>>> GetMessagesFromChat(int chatId)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var messages = await _chatService.GetMessagesFromChat(chatId, userId);
			return Ok(messages);
		}

		[HttpPost]
		public async Task<ActionResult> SendMessage(MessageVM message)
		{
			

			try
			{
				await _chatService.SendMessage(message);
				return Ok("Message sent.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost("{chatId}")]
		public async Task<ActionResult> MarkMessagesAsRead(int chatId)
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				await _chatService.MarkMessagesAsRead(chatId, userId);
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		[HttpPut]
		public async Task<ActionResult> EditMessage(Message message)
		{
			try
			{
				await _chatService.UpdateMessage(message);
				return Ok("Message Edited");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{messageId}/{chatId}")]
		public async Task<ActionResult> DeleteMessage(int messageId, int chatId, bool deleteForMeOnly = true)
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				bool result;
				if (deleteForMeOnly)
				{
					result = await _chatService.DeleteMessageForMe(userId, messageId, chatId);

				}
				else
				{
					 result = await _chatService.DeleteMyMessageForAll(userId, messageId, chatId);

				}
				if (result)
				{
					return Ok("Message deleted.");
				}
				else
				{
					return NotFound("Message not found.");
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{chatId}")]
		public async Task<ActionResult> DeleteChat(int chatId, bool deleteForMeOnly = true)
		{
			try
			{
				bool result;
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				if (deleteForMeOnly)
				{
					result = await _chatService.DeleteChatForMe(chatId, userId);
				}
				else
				{
					result = await _chatService.DeleteChatForAll(chatId, userId);
				}
				if (result)
				{
					return Ok("Chat deleted.");
				}
				else
				{
					return NotFound("Chat not found.");
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

	}
}
