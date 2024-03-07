using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories.IRepository
{
	public interface IChat : IRepository<Chat>
	{
		public  Task<List<Chat>> GetChatsForUser(string userId);
	}
}
