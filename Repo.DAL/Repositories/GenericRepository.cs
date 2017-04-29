using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Repo.DAL.Context;
using Repo.DAL.Infrastructure;
using Repo.Model.Models.Common;

namespace Repo.DAL.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : Entity
    {
        protected IEntityContext DbContext;
        protected DbSet<T> DbSet;

        public GenericRepository(IEntityContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<T>();
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public void DeleteById(object id)
        {
            var entity = DbSet.Find(id);
            Delete(entity);
        }

        public IQueryable<T> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public T GetById(object id)
        {
            return DbSet.Find(id);
        }

        public IQueryable<T> GetByQuery(Expression<Func<T, bool>> query = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> queryResult = DbSet;

            if (query != null)
            {
                queryResult = queryResult.Where(query);
            }

            foreach (var property in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                queryResult = queryResult.Include(property);
            }

            if (orderBy != null)
            {
                return orderBy(queryResult);
            }

            return queryResult;
        }

        public T Insert(T entity)
        {
            return DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            DbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
