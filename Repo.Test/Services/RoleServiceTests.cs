using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using NUnit.Framework;
using Repo.BAL.Services;
using Repo.BAL.Validation;
using Repo.DAL.Context;
using Repo.DAL.Infrastructure;
using Repo.Helpers.Validation;
using Repo.Model.Models;

namespace Repo.Test.Services
{
    [TestFixture()]
    public class RoleServiceTests
    {
        private IRoleService _service;
        private IUnitOfWork _uow;
        private IEntityContext _context;

        [OneTimeSetUp]
        public void Intialize()
        {
            DbConnection connection = Effort.DbConnectionFactory.CreateTransient();

            _context = new EntityContext(connection);
            _uow = new UnitOfWork(_context);

            var validationProvider = new ValidationProvider(type => new RoleValidator(_uow));
            _service = new RoleService(_uow, validationProvider);

            PrepareData(_context);
        }

        private void PrepareData(IEntityContext context)
        {
            var roles = new List<Role>
            {
                new Role { Name = "User"},
                new Role { Name = "Moderator"}
            };

            context.Roles.AddRange(roles);
            context.SaveChanges();
        }

        [Test()]
        public void GetAllTest()
        {
            var roles = _service.GetAll().ToList();
            Assert.AreEqual(roles.Count, 2);
        }

        [Test()]
        public void GetTest()
        {
            var roles = _service.GetAll().ToList();
            foreach (var role in roles)
            {
                var tmp = _service.Get(role.Id);
                Assert.AreEqual(tmp.Id, role.Id);
            }
        }

        [Test()]
        [TestCase("User")]
        [TestCase("Moderator")]
        public void GetByName_Positive(string name)
        {
            var role = _service.GetByName(name);
            Assert.IsNotNull(role);
        }

        [Test()]
        [TestCase("Administrator")]
        public void GetByName_Negative(string name)
        {
            var role = _service.GetByName(name);
            Assert.IsNull(role);
        }

        [Test()]
        public void CreateTest()
        {
            var role = new Role
            {
                Name = "Administrator"
            };

            var dbRole = _service.Create(role);

            Assert.True(dbRole.Id != Guid.Empty);
            Assert.AreEqual(dbRole.Name, role.Name);
        }

        [Test()]
        public void UpdateTest()
        {
            const string name = "Test";
            var role = _service.GetAll().FirstOrDefault(r => r.Name == "Administrator");
            Assert.NotNull(role);

            role.Name = name;
            _service.Update(role);
            Assert.NotNull(_service.GetAll().FirstOrDefault(r => r.Name == name));
        }

        [Test()]
        public void DeleteTest()
        {
            var role = _service.GetAll().FirstOrDefault(r => r.Name == "User");
            _service.Delete(role);

            Assert.True(_service.Get(role.Id) == null);
        }
    }
}