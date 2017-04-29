using System;
using System.Collections.Generic;
using Repo.Model.Models.Common;

namespace Repo.BAL.Infrastructure
{
    public interface IGenericService<T> : IService where T : Entity
    {
        T Get(Guid id);
        IEnumerable<T> GetAll();
        T Create(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
