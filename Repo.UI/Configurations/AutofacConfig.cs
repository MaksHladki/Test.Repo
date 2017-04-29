using System;
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
                .AsClosedTypesOf(typeof(Validator<>))
                .InstancePerLifetimeScope();

            builder.Register<Func<Type, IValidator>>(c =>
            {
                var cc = c.Resolve<IComponentContext>();
                return type =>
                {
                    var valType = typeof(Validator<>).MakeGenericType(type);
                    return (IValidator)cc.Resolve(valType);
                };
            });

            return builder.Build();
        }
    }
}
