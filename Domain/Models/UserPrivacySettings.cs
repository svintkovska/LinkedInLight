using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class UserPrivacySettings
	{
		[Key]
		public string UserId { get; set; }

		[ForeignKey("UserId")]
		public virtual ApplicationUser User { get; set; }

		public int ProfileViewingValue { get; set; }

		[NotMapped]
		public ProfileViewingOptions ProfileViewing
		{
			get => (ProfileViewingOptions)ProfileViewingValue;
			set => ProfileViewingValue = (int)value;
		}

		public int EmailVisibilityValue { get; set; }

		[NotMapped]
		public EmailVisibilityOptions EmailVisibility
		{
			get => (EmailVisibilityOptions)EmailVisibilityValue;
			set => EmailVisibilityValue = (int)value;
		}

		public bool ConnectionVisibility { get; set; }
		//public bool ShowMembersYouFollow { get; set; }
		//public bool RepresentCompany { get; set; }
		//public bool ExportingData { get; set; }                   - не будемо цього робити
		//public bool VisibilityOutsideJ4U { get; set; }
		public bool ShowLastName { get; set; }
		public bool ShareProfileUpdates { get; set; }


		public int DiscoverByEmailValue { get; set; }

		[NotMapped]
		public DiscoverByEmailOptions DiscoverByEmail
		{
			get => (DiscoverByEmailOptions)DiscoverByEmailValue;
			set => DiscoverByEmailValue = (int)value;
		}
		public int DiscoverByPhoneValue { get; set; }

		[NotMapped]
		public DiscoverByPhoneOptions DiscoverByPhone
		{
			get => (DiscoverByPhoneOptions)DiscoverByPhoneValue;
			set => DiscoverByPhoneValue = (int)value;
		}
		public int ActiveStatusVisibilityValue { get; set; }

		[NotMapped]
		public ActiveStatusVisibility ActiveStatusVisibility
		{
			get => (ActiveStatusVisibility)ActiveStatusVisibilityValue;
			set => ActiveStatusVisibilityValue = (int)value;
		}
		public virtual ICollection<BlockedUser> BlockedUsers { get; set; }

	}
}
