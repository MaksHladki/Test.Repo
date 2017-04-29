using System.Reflection;
using Autofac;
using Repo.DAL.Context;
using Repo.DAL.Infrastructure;
using Repo.Helpers.Validation;

namespace Repo.UI.Configurations
{
    class AutofacConfig
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            //common
            builder.RegisterType(typeof(EntityContext)).As(typeof(IEntityContext)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerLifetimeScope();

            //Services
            builder.RegisterAssemblyTypes(Assembly.Load("Repo.BAL"))
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            //Validators
            builder.RegisterType(typeof(ValidationProvider)).As(typeof(IValidationProvider)).InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.Load("Repo.BAL"))
                .Where(t => t.Name.EndsWith("Validator"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            return builder.Build();
        }
    }
}
