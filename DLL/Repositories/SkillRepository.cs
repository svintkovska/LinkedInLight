using DLL.Data;
using DLL.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
	public class SkillRepository : Repository<Skill>, ISkill
	{
		private readonly ApplicationDbContext _db;
		public SkillRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}


	}
}
