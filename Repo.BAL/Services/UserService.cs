using System;
using System.Collections.Generic;
using Repo.BAL.Infrastructure;
using Repo.DAL.Infrastructure;
using Repo.Helpers.Validation;
using Repo.Model.Models;

namespace Repo.BAL.Services
{
    public interface IUserService : IGenericService<User>
    {
        User GetByLogin(string login);
        bool IsUserExist(string login);
    }

    public class UserService : Service, IUserService
    {
        public UserService(IUnitOfWork uow, IValidationProvider validationProvider) : base(uow, validationProvider)
        {

        }

        public User Get(Guid id)
        {
            throw new NotImplementedException("UserService.Get has not been implemented yet.");
        }

        public IEnumerable<User> GetAll()
        {
            return Uow.UserRepository.GetAll();
        }

        public User Create(User entity)
        {
            throw new NotImplementedException("UserService.Create has not been implemented yet.");
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException("UserService.Delete has not been implemented yet.");
        }

        public void Update(User entity)
        {
            throw new NotImplementedException("UserService.Update has not been implemented yet.");
        }

        public User GetByLogin(string login)
        {
            return Uow.UserRepository.GetByLogin(login);
        }

        public bool IsUserExist(string login)
        {
            return GetByLogin(login) != null;
        }
    }
}
