using Repo.DAL.Infrastructure;
using Repo.Helpers.Validation;
using Repo.Model.Models;
using System.Collections.Generic;

namespace Repo.BAL.Validation
{
    public class RoleValidator : Validator<Role>
    {
        private readonly IUnitOfWork _uow;

        public RoleValidator(IUnitOfWork uow)
        {
            _uow = uow;
        }

        protected override IEnumerable<ValidationResult> Validate(Role entity)
        {
            foreach (var result in base.Validate(entity))
                yield return result;

            var dbRole = _uow.RoleRepository.GetByName(entity.Name);
            if (dbRole != null && dbRole.Id != entity.Id)
            {
                yield return new ValidationResult("Name", "Name must be unique.");
            }
        }
    }
}
