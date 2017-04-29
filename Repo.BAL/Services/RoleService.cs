using System;
using System.Collections.Generic;
using Repo.BAL.Infrastructure;
using Repo.DAL.Infrastructure;
using Repo.Model.Models;
using Repo.Helpers.Validation;

namespace Repo.BAL.Services
{
    public interface IRoleService : IGenericService<Role>
    {
        Role GetByName(string name);
        bool IsRoleExist(string name);
        void AssosiateWithUser(string roleName, string userName);
        void UnAssosiateWithUser(string roleName, string userName);
    }

    public class RoleService : Service, IRoleService
    {
        public RoleService(IUnitOfWork uow, IValidationProvider validationProvider) : base(uow, validationProvider)
        {

        }

        public Role Get(Guid id)
        {
            return Uow.RoleRepository.GetById(id);
        }

        public IEnumerable<Role> GetAll()
        {
            return Uow.RoleRepository.GetAll();
        }

        public Role Create(Role entity)
        {
            ValidationProvider.Validate(entity);

            Uow.RoleRepository.Insert(entity);
            Uow.Commit();

            return entity;
        }

        public void Delete(Role entity)
        {
            Uow.RoleRepository.Delete(entity);
            Uow.Commit();
        }

        public void Update(Role entity)
        {
            ValidationProvider.Validate(entity);

            Uow.RoleRepository.Update(entity);
            Uow.Commit();
        }

        public Role GetByName(string name)
        {
            return Uow.RoleRepository.GetByName(name);
        }

        public bool IsRoleExist(string name)
        {
            return GetByName(name) != null;
        }

        public void AssosiateWithUser(string roleName, string userName)
        {
            var role = GetByName(roleName);
            if (role == null)
                throw new ValidationException("Role", "Role not found");

            var user = Uow.UserRepository.GetByLogin(userName);
            if (user == null)
                throw new ValidationException("User", "User not found");

            if (role.Users.Contains(user))
                throw new ValidationException("User", "User has already associated with role");

            role.Users.Add(user);
            Uow.Commit();
        }

        public void UnAssosiateWithUser(string roleName, string userName)
        {
            var role = GetByName(roleName);
            if (role == null)
                throw new ValidationException("Role", "Role not found");

            var user = Uow.UserRepository.GetByLogin(userName);
            if (user == null)
                throw new ValidationException("User", "User not found");

            if (!role.Users.Contains(user))
                throw new ValidationException("User", "User haven't associated with role yet");

            role.Users.Remove(user);
            Uow.Commit();
        }
    }
}
