using System;
using System.Collections;
using System.Linq;

namespace Repo.Helpers.Validation
{
    public sealed class ValidationProvider : IValidationProvider
    {
        private readonly Func<Type, IValidator> _validatorFactory;

        public ValidationProvider(Func<Type, IValidator> validatorFactory)
        {
            _validatorFactory = validatorFactory;
        }

        public void Validate(Object entity)
        {
            var results = _validatorFactory(entity.GetType()).Validate(entity).ToArray();

            if (results.Length > 0)
                throw new ValidationException(results);
        }

        public void ValidateAll(IEnumerable entities)
        {
            var results = (from entity in entities.Cast<Object>()
                           let validator = _validatorFactory(entity.GetType())
                           from result in validator.Validate(entity)
                           select result).ToArray();

            if (results.Length > 0)
                throw new ValidationException(results);
        }
    }
}
