using Repo.DAL.Repositories;
using System;

namespace Repo.DAL.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IMessageRepository MessageRepository { get; }
        void Commit();
    }
}
