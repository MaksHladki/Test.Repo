using System;
using System.Collections.Generic;
using Repo.BAL.Infrastructure;
using Repo.DAL.Infrastructure;
using Repo.Helpers.Validation;
using Repo.Model.Models;

namespace Repo.BAL.Services
{
    public interface IMessageService : IGenericService<Message>
    {

    }

    public class MessageService : Service, IMessageService
    {
        public MessageService(IUnitOfWork uow, IValidationProvider validationProvider) : base(uow, validationProvider)
        {

        }

        public Message Get(Guid id)
        {
            throw new NotImplementedException("MessageService.Get has not been implemented yet.");
        }

        public IEnumerable<Message> GetAll()
        {
            throw new NotImplementedException("MessageService.GetAll has not been implemented yet.");
        }

        public Message Create(Message entity)
        {
            throw new NotImplementedException("MessageService.Create has not been implemented yet.");
        }

        public void Delete(Message entity)
        {
            throw new NotImplementedException("MessageService.Delete has not been implemented yet.");
        }

        public void Update(Message entity)
        {
            throw new NotImplementedException("MessageService.Update has not been implemented yet.");
        }
    }
}
