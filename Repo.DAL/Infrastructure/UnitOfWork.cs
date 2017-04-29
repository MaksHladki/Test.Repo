using System;
using System.Linq;
using Repo.DAL.Repositories;
using System.Data.Entity;
using System.Data.Entity.Validation;
using Repo.DAL.Context;
using Repo.Model.Models.Common;

namespace Repo.DAL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields

        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private IMessageRepository _messageRepository;

        private readonly IEntityContext _dbContext;
        private bool _disposed;

        #endregion

        #region Constructors

        public UnitOfWork(IEntityContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Properties

        private string UserName => $"{Environment.MachineName}\\{Environment.UserDomainName}";

        public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(_dbContext));

        public IRoleRepository RoleRepository => _roleRepository ?? (_roleRepository = new RoleRepository(_dbContext));

        public IMessageRepository MessageRepository => _messageRepository ?? (_messageRepository = new MessageRepository(_dbContext));

        #endregion

        #region Methods

        public void Commit()
        {
            try
            {
                EntityState[] states = { EntityState.Added, EntityState.Modified };
                var entities = _dbContext.ChangeTracker.Entries().Where(x => x.Entity is Entity && states.Contains(x.State));

                foreach (var entity in entities)
                {
                    if (entity.State == EntityState.Added)
                    {
                        ((Entity)entity.Entity).CreatedAt = DateTime.UtcNow;
                        ((Entity)entity.Entity).CreatedBy = UserName;
                    }

                    ((Entity)entity.Entity).ModifiedAt = DateTime.UtcNow;
                    ((Entity)entity.Entity).ModifiedBy = UserName;
                }

                _dbContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = $"{validationErrors.Entry.Entity}:{validationError.ErrorMessage}";
                        raise = new InvalidOperationException(message, raise);
                    }
                }

                Rollback();
                throw raise;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Helper Methods

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        private void Rollback()
        {
            EntityState[] states = { EntityState.Added, EntityState.Modified, EntityState.Deleted };

            foreach (var entity in _dbContext.ChangeTracker.Entries().Where(e => states.Contains(e.State)))
            {
                _dbContext.Entry(entity.Entity).State = EntityState.Detached;
            }
        }

        #endregion
    }
}
