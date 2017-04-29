using System.Linq;
using Repo.DAL.Context;
using Repo.DAL.Infrastructure;
using Repo.Model.Models;

namespace Repo.DAL.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role GetByName(string name);
    }

    public class RoleRepository: GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(IEntityContext dbContext) : base(dbContext)
        {
        }

        public Role GetByName(string name)
        {
            return DbContext.Roles.FirstOrDefault(r => r.Name == name);
        }
    }
}
