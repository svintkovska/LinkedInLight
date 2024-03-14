using BLL.Interfaces;
using BLL.Utilities.SignalR;
using DLL.Repositories.IRepository;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class PrivacySettingsService: IPrivacySettingsService
	{
		private readonly IUnitOfWork _unitOfWork;

		public PrivacySettingsService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public Dictionary<string, IEnumerable<string>> GetAllEnumValues()
		{
			var enumValues = new Dictionary<string, IEnumerable<string>>
			{
				{ "ActiveStatusVisibility", Enum.GetNames(typeof(ActiveStatusVisibilityOptions)) },
				{ "DiscoverByEmailOptions", Enum.GetNames(typeof(DiscoverByEmailOptions)) },
				{ "DiscoverByPhoneOptions", Enum.GetNames(typeof(DiscoverByPhoneOptions)) },
				{ "EmailVisibilityOptions", Enum.GetNames(typeof(EmailVisibilityOptions)) },
				{ "ProfileViewingOptions", Enum.GetNames(typeof(ProfileViewingOptions)) }
			};
			return enumValues;
		}
		public async Task<ProfileViewingOptions> GetProfileViewing(string userId)
		{
			var userPrivacySettings = await _unitOfWork.UserPrivacySettingsRepo.Get(u => u.UserId == userId);
			return userPrivacySettings != null ? userPrivacySettings.ProfileViewing : ProfileViewingOptions.YourNameAndHeadline;
		}
		public async Task<bool> UpdateProfileViewinValue(string userId, int profileViewingValue)
		{
			var userPrivacySettings = await _unitOfWork.UserPrivacySettingsRepo.Get(u=> u.UserId ==userId);
			if (userPrivacySettings == null)
			{
				return false;
			}

			userPrivacySettings.ProfileViewingValue = profileViewingValue;
			_unitOfWork.UserPrivacySettingsRepo.Update(userPrivacySettings);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<EmailVisibilityOptions> GetEmailVisibility(string userId)
		{
			var userPrivacySettings = await _unitOfWork.UserPrivacySettingsRepo.Get(u => u.UserId == userId);
			return userPrivacySettings != null ? userPrivacySettings.EmailVisibility : EmailVisibilityOptions.OnlyMe;
		}
		public async Task<bool> UpdateEmailVisibilityValue(string userId, int emailVisibilityValue)
		{
			var userPrivacySettings = await _unitOfWork.UserPrivacySettingsRepo.Get(u => u.UserId == userId);
			if (userPrivacySettings == null)
			{
				return false;
			}

			userPrivacySettings.EmailVisibilityValue = emailVisibilityValue;
			_unitOfWork.UserPrivacySettingsRepo.Update(userPrivacySettings);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<DiscoverByEmailOptions> GetDiscoverByEmail(string userId)
		{
			var userPrivacySettings = await _unitOfWork.UserPrivacySettingsRepo.Get(u => u.UserId == userId);
			return userPrivacySettings != null ? userPrivacySettings.DiscoverByEmail : DiscoverByEmailOptions.FirstDegreeConnections;
		}
		public async Task<bool> UpdateDiscoverByEmailValue(string userId, int discoverByEmailValue)
		{
			var userPrivacySettings = await _unitOfWork.UserPrivacySettingsRepo.Get(u => u.UserId == userId);
			if (userPrivacySettings == null)
			{
				return false;
			}

			userPrivacySettings.DiscoverByEmailValue = discoverByEmailValue;
			_unitOfWork.UserPrivacySettingsRepo.Update(userPrivacySettings);
			await _unitOfWork.SaveAsync();
			return true;
		}

		public async Task<DiscoverByPhoneOptions> GetDiscoverByPhone(string userId)
		{
			var userPrivacySettings = await _unitOfWork.UserPrivacySettingsRepo.Get(u => u.UserId == userId);
			return userPrivacySettings != null ? userPrivacySettings.DiscoverByPhone : DiscoverByPhoneOptions.Nobody;
		}
		public async Task<bool> UpdateDiscoverByPhoneValue(string userId, int discoverByPhoneValue)
		{
			var userPrivacySettings = await _unitOfWork.UserPrivacySettingsRepo.Get(u => u.UserId == userId);
			if (userPrivacySettings == null)
			{
				return false;
			}

			userPrivacySettings.DiscoverByPhoneValue = discoverByPhoneValue;
			_unitOfWork.UserPrivacySettingsRepo.Update(userPrivacySettings);
			await _unitOfWork.SaveAsync();
			return true;
		}

		public async Task<ActiveStatusVisibilityOptions> GetActiveStatusVisibility(string userId)
		{
			var userPrivacySettings = await _unitOfWork.UserPrivacySettingsRepo.Get(u => u.UserId == userId);
			return userPrivacySettings != null ? userPrivacySettings.ActiveStatusVisibility : ActiveStatusVisibilityOptions.NoOne;
		}
		public async Task<bool> UpdateActiveStatusVisibilityValue(string userId, int activeStatusVisibilityValue)
		{
			var userPrivacySettings = await _unitOfWork.UserPrivacySettingsRepo.Get(u => u.UserId == userId);
			if (userPrivacySettings == null)
			{
				return false;
			}

			userPrivacySettings.ActiveStatusVisibilityValue = activeStatusVisibilityValue;
			_unitOfWork.UserPrivacySettingsRepo.Update(userPrivacySettings);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> GetConnectionVisibility(string userId)
		{
			var userPrivacySettings = await _unitOfWork.UserPrivacySettingsRepo.Get(u => u.UserId == userId);
			return userPrivacySettings?.ConnectionVisibility ?? false;
		}

		public async Task<bool> UpdateConnectionVisibility(string userId, bool connectionVisibility)
		{
			var userPrivacySettings = await _unitOfWork.UserPrivacySettingsRepo.Get(u => u.UserId == userId);
			if (userPrivacySettings == null)
			{
				return false;
			}

			userPrivacySettings.ConnectionVisibility = connectionVisibility;
			_unitOfWork.UserPrivacySettingsRepo.Update(userPrivacySettings);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> GetShowLastName(string userId)
		{
			var userPrivacySettings = await _unitOfWork.UserPrivacySettingsRepo.Get(u => u.UserId == userId);
			return userPrivacySettings?.ShowLastName ?? false;
		}
		public async Task<bool> UpdateShowLastName(string userId, bool showLastName)
		{
			var userPrivacySettings = await _unitOfWork.UserPrivacySettingsRepo.Get(u => u.UserId == userId);
			if (userPrivacySettings == null)
			{
				return false;
			}

			userPrivacySettings.ShowLastName = showLastName;
			_unitOfWork.UserPrivacySettingsRepo.Update(userPrivacySettings);
			await _unitOfWork.SaveAsync();
			return true;
		}

		public async Task<bool> GetShareProfileUpdates(string userId)
		{
			var userPrivacySettings = await _unitOfWork.UserPrivacySettingsRepo.Get(u => u.UserId == userId);
			return userPrivacySettings?.ShareProfileUpdates ?? false;
		}
		public async Task<bool> UpdateShareProfileUpdates(string userId, bool shareProfileUpdates)
		{
			var userPrivacySettings = await _unitOfWork.UserPrivacySettingsRepo.Get(u => u.UserId == userId);
			if (userPrivacySettings == null)
			{
				return false;
			}

			userPrivacySettings.ShareProfileUpdates = shareProfileUpdates;
			_unitOfWork.UserPrivacySettingsRepo.Update(userPrivacySettings);
			await _unitOfWork.SaveAsync();
			return true;
		}

		public async Task<IEnumerable<BlockedUser>> GetBlockedUsers(string userId)
		{
			var userPrivacySettings = await _unitOfWork.UserPrivacySettingsRepo.Get(u => u.UserId == userId, includeProperties:"BlockedUser");
			return userPrivacySettings?.BlockedUsers;
		}
		public async Task<bool> RemoveBlockedUser(string userId, string blockedUserId)
		{
			var userPrivacySettings = await _unitOfWork.UserPrivacySettingsRepo.Get(u => u.UserId == userId, includeProperties: "BlockedUser");
			if (userPrivacySettings == null)
			{
				return false;
			}

			var blockedUser = userPrivacySettings.BlockedUsers.FirstOrDefault(bu => bu.BlockedUserId == blockedUserId);
			if (blockedUser == null)
			{
				return false;
			}

			userPrivacySettings.BlockedUsers.Remove(blockedUser);
			_unitOfWork.UserPrivacySettingsRepo.Update(userPrivacySettings);
			await _unitOfWork.SaveAsync();
			return true;
		}

		

	}
}
