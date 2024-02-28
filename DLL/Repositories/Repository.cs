using DLL.Data;
using DLL.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		internal DbSet<T> dbSet;
		public Repository(ApplicationDbContext db)
		{
			_db = db;
			this.dbSet = _db.Set<T>();
		}

		public async Task Add(T entity)
		{
			await dbSet.AddAsync(entity);
		}

		public async Task<T> Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
		{
			IQueryable<T> query;

			if (tracked)
			{
				query = dbSet;
			}
			else
			{
				query = dbSet.AsNoTracking();
			}
			query = query.Where(filter);
			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var includeProp in includeProperties
					.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
			return await query.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			if (filter != null)
			{
				query = query.Where(filter);

			}
			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var includeProp in includeProperties
					.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}

			return await query.ToListAsync();

		}

		public void Remove(T entity)
		{
			 dbSet.Remove(entity);

		}

		public void RemoveRange(IEnumerable<T> entities)
		{
			dbSet.RemoveRange(entities);
		}

		public void Update(T entity)
		{
			dbSet.Update(entity);
		}

		public async Task Save()
		{
			await _db.SaveChangesAsync();
		}
	}
}
