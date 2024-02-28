using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories.IRepository
{
	public interface IRepository<T> where T : class
	{
		public Task <IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
		public Task <T> Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
		public Task Add(T entity);
		public void Remove(T entity);
		public void RemoveRange(IEnumerable<T> entities);
		public void Update(T entity);
		public Task Save();

	}
}
