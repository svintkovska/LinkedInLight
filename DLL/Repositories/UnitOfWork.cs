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
			LanguageRepo = new LanguageRepository(_db);


			
		}
		public async Task SaveAsync()
		{
			await _db.SaveChangesAsync();
		}
	}
}
