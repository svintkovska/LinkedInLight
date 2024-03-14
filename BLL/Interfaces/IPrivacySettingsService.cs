using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface IPrivacySettingsService
	{
		public Task<bool> UpdateProfileViewinValue(string userId, int profileViewingValue);
		public Task<ProfileViewingOptions> GetProfileViewing(string userId);
		public Task<EmailVisibilityOptions> GetEmailVisibility(string userId);
		public Task<bool> UpdateEmailVisibilityValue(string userId, int emailVisibilityValue);
		public Task<DiscoverByEmailOptions> GetDiscoverByEmail(string userId);
		public Task<bool> UpdateDiscoverByEmailValue(string userId, int discoverByEmailValue);
		public Task<DiscoverByPhoneOptions> GetDiscoverByPhone(string userId);
		public Task<bool> UpdateDiscoverByPhoneValue(string userId, int discoverByPhoneValue);
		public Task<ActiveStatusVisibility> GetActiveStatusVisibility(string userId);
		public  Task<bool> UpdateActiveStatusVisibilityValue(string userId, int activeStatusVisibilityValue);
		public Task<bool> GetConnectionVisibility(string userId);
		public Task<bool> UpdateConnectionVisibility(string userId, bool connectionVisibility);
		public Task<bool> GetShowLastName(string userId);
		public Task<bool> UpdateShowLastName(string userId, bool showLastName);
		public Task<bool> GetShareProfileUpdates(string userId);
		public Task<bool> UpdateShareProfileUpdates(string userId, bool shareProfileUpdates);
		public Dictionary<string, IEnumerable<string>> GetAllEnumValues();
		public Task<IEnumerable<BlockedUser>> GetBlockedUsers(string userId);
		public Task<bool> RemoveBlockedUser(string userId, string blockedUserId);
	}
}
