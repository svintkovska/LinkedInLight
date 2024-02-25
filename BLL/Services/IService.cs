using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{

	public interface IService<T> where T : class
	{
		IEnumerable<T> GetAll();
		T GetById(int id);
		void Add(T entity);
		void Update(T entity);
		void Delete(T entity);
	}
}
