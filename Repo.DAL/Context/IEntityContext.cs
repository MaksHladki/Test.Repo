using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Repo.Model.Models;

namespace Repo.DAL.Context
{
    public interface IEntityContext: IDisposable
    {
        DbChangeTracker ChangeTracker { get; }
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Message> Messages { get; set; }
    
        int SaveChanges();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
