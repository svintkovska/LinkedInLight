using DLL.Data;
using DLL.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
	public class ExperienceRepository : Repository<Experience>, IExperience
	{
		private readonly ApplicationDbContext _db;
		public ExperienceRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}


	}
}
