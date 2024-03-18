using DLL.Data;
using DLL.Repositories.IRepository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
	public class VolunteerExperienceRepository: Repository<VolunteerExperience>, IVolunteerExperience
	{
		private readonly ApplicationDbContext _db;
		public VolunteerExperienceRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
	}
}
