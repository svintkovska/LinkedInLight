using DLL.Data;
using DLL.Models;
using DLL.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
	public class ScreeningAnswerRepository : Repository<ScreeningAnswer>, IScreeningAnswer
	{
		private readonly ApplicationDbContext _db;
		public ScreeningAnswerRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}


	}
}
