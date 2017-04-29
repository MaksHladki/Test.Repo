using System;
using System.Linq;
using System.Linq.Expressions;
using Repo.Model.Models.Common;

namespace Repo.DAL.Infrastructure
{
    public interface IRepository<T> where T : Entity
    {
        T Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteById(object id);
        T GetById(object id);
        IQueryable<T> GetAll();
        IQueryable<T> GetByQuery(
            Expression<Func<T, bool>> query = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
    }
}
