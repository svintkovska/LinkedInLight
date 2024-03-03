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
	public class EducationRepository : Repository<Education>, IEducation
	{
		private readonly ApplicationDbContext _db;
		public EducationRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}


	}
}
