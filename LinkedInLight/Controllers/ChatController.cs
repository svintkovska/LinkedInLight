using BLL.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace LinkedInLight.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ChatController : ControllerBase
	{
		private readonly IChatService _chatService;

		public ChatController(IChatService chatService)
		{
			_chatService = chatService;
		}

		[HttpGet("{userId}")]
		public async Task<ActionResult<List<Chat>>> GetChatsForUser(string userId)
		{
			var chats = await _chatService.GetChatsForUser(userId);
			return Ok(chats);
		}

		[HttpGet("{chatId}/{userId}")]
		public async Task<ActionResult<List<Message>>> GetMessagesFromChat(int chatId, string userId)
		{
			var messages = await _chatService.GetMessagesFromChat(chatId, userId);
			return Ok(messages);
		}

		[HttpPost]
		public async Task<ActionResult> SendMessage(Message message)
		{
			try
			{
				await _chatService.SendMessage(message.SenderId, message.ChatId.Value, message);
				return Ok("Message sent.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost("{chatId}/{userId}")]
		public async Task<ActionResult> MarkMessagesAsRead(int chatId, string userId)
		{
			try
			{
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

		[HttpDelete("{userId}/{messageId}/{chatId}")]
		public async Task<ActionResult> DeleteMyMessageForAll(string userId, int messageId, int chatId)
		{
			try
			{
				var result = await _chatService.DeleteMyMessageForAll(userId, messageId, chatId);
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

		[HttpDelete("{userId}/{messageId}/{chatId}")]
		public async Task<ActionResult> DeleteMessageForMe(string userId, int messageId, int chatId)
		{
			try
			{
				await _chatService.DeleteMessageForMe(userId, messageId, chatId);
				return Ok("Message deleted.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{chatId}/{userId}")]
		public async Task<ActionResult> DeleteChatForAll(int chatId, string userId)
		{
			try
			{
				await _chatService.DeleteChatForAll(chatId, userId);
				return Ok("Chat deleted.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{chatId}/{userId}")]
		public async Task<ActionResult> DeleteChatForMe(int chatId, string userId)
		{
			try
			{
				await _chatService.DeleteChatForMe(chatId, userId);
				return Ok("Chat deleted.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
