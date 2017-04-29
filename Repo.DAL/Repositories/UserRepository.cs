using System.Linq;
using Repo.DAL.Context;
using Repo.DAL.Infrastructure;
using Repo.Model.Models;

namespace Repo.DAL.Repositories
{
    public interface IUserRepository: IRepository<User>
    {
        User GetByLogin(string login);
    }

    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        public UserRepository(IEntityContext dbContext) : base(dbContext)
        {
        }

        public User GetByLogin(string login)
        {
            return DbContext.Users.FirstOrDefault(u => u.Login == login);
        }
    }
}
