using System;
using Autofac;
using Repo.BAL.Services;
using Repo.UI.Configurations;
using Repo.UI.OperationType;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Repo.Model.Models;
using Repo.UI.Extensions;
using Repo.UI.Helpers;

namespace Repo.UI
{
    static class  Program
    {
        private static IRoleService _roleService;
        private static IUserService _userService;
        private static IMessageService _messageService;

        static Program()
        {
            Initialize();
        }

        static void Main()
        {
            Menu();
        }

        static void Menu()
        {
            var operations = new Dictionary<OperationType.OperationType, Action>()
            {
                {OperationType.OperationType.Role, RoleMenu },
                {OperationType.OperationType.User, UserMenu },
                {OperationType.OperationType.Message, MessageMenu }
            };

            var menuBuilder = new MenuBuilder<OperationType.OperationType>("Menu", 0, operations);
            menuBuilder.Build();
        }

        #region Role

        static void RoleMenu()
        {
            var operations = new Dictionary<RoleOperationType, Action>
            {
                {RoleOperationType.GetAll, RoleGetList },
                {RoleOperationType.GetByName, RoleGetByName },
                {RoleOperationType.Create, RoleCreate },
                {RoleOperationType.Update, RoleUpdate },
                {RoleOperationType.Delete, RoleDelete },
                {RoleOperationType.AssociateWithUser, RoleAssociate },
                {RoleOperationType.UnAssociateWithUser, RoleUnAssociate }
            };

            var menuBuilder = new MenuBuilder<RoleOperationType>("Role Menu", 1, operations);
            menuBuilder.Build();
        }

        static void RoleGetList()
        {
            var roles = _roleService.GetAll();

            if (!roles.Any())
            {
                Console.WriteLine("Roles not found.");
                return;
            }

            int i = 1;
            foreach (var role in roles)
            {
                Console.WriteLine($"Role #{i++}. {role.Name}");
            }
        }

        static void RoleGetByName()
        {
            Console.WriteLine("Enter name");
            var name = Console.ReadLine();

            var role = _roleService.GetByName(name);
            if (role == null)
            {
                Console.WriteLine("Role not found.");
            }
            else
            {
                role.Print();
            }
        }

        static void RoleCreate()
        {
            Console.WriteLine("Enter name");

            var name = Console.ReadLine();
            _roleService.Create(new Role
            {
                Name = name
            });
        }

        static void RoleUpdate()
        {
            Console.WriteLine("Enter name");
            var name = Console.ReadLine();
            var role = _roleService.GetByName(name);

            Console.WriteLine("Enter new name");

            var nameNew = Console.ReadLine();
            role.Name = nameNew;
            _roleService.Update(role);
        }

        static void RoleDelete()
        {
            Console.WriteLine("Enter name");

            var name = Console.ReadLine();
            var role = _roleService.GetByName(name);

            if (role == null)
            {
                Console.WriteLine("Role not found.");
            }

            _roleService.Delete(role);
        }

        static void RoleAssociate()
        {
            Console.WriteLine("Enter name");
            var roleName = Console.ReadLine();
           
            Console.WriteLine("Enter user name");
            var userName = Console.ReadLine();
            _roleService.AssosiateWithUser(roleName, userName);
        }

        static void RoleUnAssociate()
        {
            Console.WriteLine("Enter name");
            var roleName = Console.ReadLine();

            Console.WriteLine("Enter user name");
            var userName = Console.ReadLine();
            _roleService.UnAssosiateWithUser(roleName, userName);
        }

        #endregion

        #region User

        static void UserMenu()
        {
            var operations = new Dictionary<UserOperationType, Action>()
            {
                //TODO
            };

            var menuBuilder = new MenuBuilder<UserOperationType>("User Menu", 1, operations);
            menuBuilder.Build();
        }

        #endregion

        #region Message

        static void MessageMenu()
        {
            var operations = new Dictionary<MessageOperationType, Action>()
            {
                //TODO
            };

            var menuBuilder = new MenuBuilder<MessageOperationType>("Message Menu", 1, operations);
            menuBuilder.Build();
        }

        #endregion

        #region Initialization logic

        private static void Initialize()
        {
            InitializeDomainSettings();
            InitializeSetviceSettings();
        }

        private static void InitializeDomainSettings()
        {
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string path = Path.Combine(dir, "Database");

            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }

        private static void InitializeSetviceSettings()
        {
            var container = AutofacConfig.ConfigureContainer();
            _roleService = container.Resolve<IRoleService>();
            _userService = container.Resolve<IUserService>();
            _messageService = container.Resolve<IMessageService>();
        }

        #endregion
    }
}
