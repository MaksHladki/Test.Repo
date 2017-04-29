using Repo.DAL.Infrastructure;
using Repo.Helpers.Validation;

namespace Repo.BAL.Infrastructure
{
    public class Service : IService
    {
        protected IUnitOfWork Uow;
        protected IValidationProvider ValidationProvider;

        public Service(IUnitOfWork uow, IValidationProvider validationProvider)
        {
            Uow = uow;
            ValidationProvider = validationProvider;
        }
    }
}
