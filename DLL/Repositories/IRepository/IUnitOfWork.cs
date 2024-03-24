using DLL.Repositories.IRepository;
using DLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories.IRepository
{
	public interface IUnitOfWork
	{
		IApplicationUserRepository UserRepo { get; }
		IComment CommentRepo { get; }
		ICompany CompanyRepo { get; }
		IConnection ConnectionRepo { get; }
		IConnectionRequest ConnectionRequestRepo { get; }
		IEducation EducationRepo { get; }
		IExperience ExperienceRepo { get; }
		IIndustry IndustryRepo { get; }
		IJobApplication JobApplicationRepo { get; }
		IJobPosting JobPostingRepo { get; }
		ILike LikeRepo { get; }
		IMessage MessageRepo { get; }
		IChat ChatRepo { get; }
		INotification NotificationRepo { get; }
		IPost PostRepo { get; }
		IScreeningAnswer ScreeningAnswerRepo { get; }
		IScreeningQuestion ScreeningQuestionRepo { get; }
		ISkill SkillRepo { get; }
		IUserSkill UserSkillRepo { get; }
		IUserLanguage UserLanguageRepo { get; }
		ILanguage LanguageRepo { get; }
		IProfileVisit ProfileVisitRepo { get; }
		IUserPrivacySettings UserPrivacySettingsRepo { get; }
		IBlockedUser BlockedUserRepo { get; }
		ICertification CertificationRepo { get; }
		IProject ProjectRepo { get; }
		IProjectContributor ProjectContributorRepo { get; }
		IRecommendation RecommendationRepo{ get; }
		IVolunteerExperience VolunteerExperienceRepo { get; }
		IPhoneNumber PhoneNumberRepo { get; }
		IWebsite WebsiteRepo { get; }
		ICourse CourseRepo { get; }
		ICountry CountryRepo { get; }
		ICity CityRepo { get; }
		IOpenToWork OpenToWorkRepo { get; }
		IOpenToWorkPosition OpenToWorkPositionRepo { get; }
		IOpenToWorkCity OpenToWorkCityRepo { get; }
		IOpenToWorkCountry OpenToWorkCountryRepo { get; }
		IPosition PositionRepo { get; }
		public Task SaveAsync();


	}
}
