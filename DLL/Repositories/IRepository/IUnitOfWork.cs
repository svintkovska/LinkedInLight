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
		INotification NotificationRepo { get; }
		IPost PostRepo { get; }
		IScreeningAnswer ScreeningAnswerRepo { get; }
		IScreeningQuestion ScreeningQuestionRepo { get; }
		ISkill SkillRepo { get; }
		ILanguage LanguageRepo { get; }

	}
}
