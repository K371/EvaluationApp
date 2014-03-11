using System;
using System.Collections.Generic;
using System.Linq;
using API.Services.Repositories;

namespace API.Tests.MockObjects
{
	public class MockRepository<T> : IRepository<T> where T : class
	{
		public List<T> _context;

		public MockRepository(List<T> ctx)
		{
			_context = ctx;
		}

		// OTHER FUNCS

		public virtual void Add(T entity)
		{
			_context.Add(entity);
		}

		public virtual void Delete(T entity)
		{
			_context.Remove(entity);
		}

		public virtual void Update(T entity)
		{
			var entry = _context.Where(s => s == entity).SingleOrDefault();
			entry = entity;
		}

		public virtual T GetById(long id)
		{
			throw new NotImplementedException();
		}

		public virtual IQueryable<T> All(string includeProperties = "")
		{
			//TODO: for testing include
			if (!string.IsNullOrEmpty(includeProperties))
			{
				throw new NotImplementedException();
			}

			return _context.AsQueryable();
		}
	}
}