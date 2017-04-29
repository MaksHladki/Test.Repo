using Repo.DAL.Context;
using Repo.DAL.Infrastructure;
using Repo.Model.Models;

namespace Repo.DAL.Repositories
{
    public interface IMessageRepository : IRepository<Message>
    {
        
    }

    public class MessageRepository: GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(IEntityContext dbContext) : base(dbContext)
        {
        }
    }
}
