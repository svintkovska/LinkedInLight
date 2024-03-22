using DLL.Data;
using DLL.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _db;

		public IApplicationUserRepository UserRepo{ get; private set; }

		public IComment CommentRepo { get; private set; }

		public ICompany CompanyRepo { get; private set; }

		public IConnection ConnectionRepo { get; private set; }

		public IConnectionRequest ConnectionRequestRepo { get; private set; }

		public IEducation EducationRepo { get; private set; }

		public IExperience ExperienceRepo { get; private set; }

		public IIndustry IndustryRepo { get; private set; }

		public IJobApplication JobApplicationRepo { get; private set; }

		public IJobPosting JobPostingRepo { get; private set; }

		public ILike LikeRepo { get; private set; }

		public IMessage MessageRepo { get; private set; }
		public IChat ChatRepo { get; private set; }

		public INotification NotificationRepo { get; private set; }

		public IPost PostRepo { get; private set; }

		public IScreeningAnswer ScreeningAnswerRepo { get; private set; }

		public IScreeningQuestion ScreeningQuestionRepo { get; private set; }

		public ISkill SkillRepo { get; private set; }
		public ILanguage LanguageRepo { get; private set; }
		public IProfileVisit ProfileVisitRepo { get; private set; }
		public IUserPrivacySettings UserPrivacySettingsRepo { get; private set; }
		public IBlockedUser BlockedUserRepo { get; private set; }
		public ICertification CertificationRepo { get; private set; }
		public IProject ProjectRepo { get; private set; }
		public IProjectContributor ProjectContributorRepo { get; private set; }
		public IRecommendation RecommendationRepo { get; private set; }
		public IVolunteerExperience VolunteerExperienceRepo { get; private set; }
		public IPhoneNumber PhoneNumberRepo { get; private set; }
		public IWebsite WebsiteRepo { get; private set; }
		public ICourse CourseRepo { get; private set; }
		public ICountry CountryRepo { get; private set; }
		public ICity CityRepo { get; private set; }
		public IOpenToWorkCity OpenToWorkCityRepo { get; private set; }
		public IOpenToWorkCountry OpenToWorkCountryRepo { get; private set; }
		public IOpenToWorkPosition OpenToWorkPositionRepo { get; private set; }
		public IOpenToWork OpenToWorkRepo { get; private set; }
		public IPosition PositionRepo { get; private set; }
		public UnitOfWork(ApplicationDbContext db)
		{
			_db = db;
			UserRepo = new ApplicationUserRepository(_db);
			CommentRepo = new CommentRepository(_db);
			CompanyRepo = new CompanyRepository(_db);
			ConnectionRepo = new ConnectionRepository(_db);
			ConnectionRequestRepo = new ConnectionRequestRepository(_db);
			EducationRepo = new EducationRepository(_db);
			ExperienceRepo = new ExperienceRepository(_db);
			IndustryRepo = new IndustryRepository(_db);
			JobApplicationRepo = new JobApplicationRepository(_db);
			JobPostingRepo = new JobPostingRepository(_db);
			LikeRepo = new LikeRepository(_db);
			MessageRepo = new MessageRepository(_db);
			ChatRepo = new ChatRepository(_db);
			NotificationRepo = new NotificationRepository(_db);
			PostRepo = new PostRepository(_db);
			ScreeningAnswerRepo = new ScreeningAnswerRepository(_db);
			ScreeningQuestionRepo = new ScreeningQuestionRepository(_db);
			SkillRepo = new SkillRepository(_db);
			ProfileVisitRepo= new ProfileVisitRepository(_db);
			LanguageRepo = new LanguageRepository(_db);
			UserPrivacySettingsRepo= new UserPrivacySettingsRepository(_db);
			BlockedUserRepo= new BlockedUserRepository(_db);
			CertificationRepo= new CertificationRepository(_db);
			ProjectRepo= new ProjectRepository(_db);
			ProjectContributorRepo = new ProjectContributorsRepository(_db);
			RecommendationRepo = new RecommendationRepository(_db) ;
			VolunteerExperienceRepo = new VolunteerExperienceRepository(_db);
			PhoneNumberRepo = new PhoneNumberRepository(_db);
			WebsiteRepo = new WebsiteRepository(_db);
			CourseRepo = new CourseRepository(_db);
			CountryRepo = new CountryRepository(_db);
			CityRepo =	new CityRepository(_db);
			OpenToWorkCityRepo= new OpenToWorkCityRepository(_db);
			OpenToWorkCountryRepo= new OpenToWorkCountryRepository(_db);
			OpenToWorkPositionRepo= new OpenToWorkPositionRepository(_db);
			OpenToWorkRepo = new OpenToWorkRepository(_db);
			PositionRepo = new PositionRepository(_db);
		}
		public async Task SaveAsync()
		{
			await _db.SaveChangesAsync();
		}
	}
}
