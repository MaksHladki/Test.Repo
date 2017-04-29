using System;
using System.Collections.Generic;
using System.Linq;
using DataAnnotation = System.ComponentModel.DataAnnotations;

namespace Repo.Helpers.Validation
{
    public abstract class Validator<T> : IValidator
    {
        IEnumerable<ValidationResult> IValidator.Validate(Object entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity can not be null");

            return Validate((T)entity);
        }

        protected virtual IEnumerable<ValidationResult> Validate(T entity)
        {
            var daResults = new List<DataAnnotation.ValidationResult>();

            var context = new DataAnnotation.ValidationContext(entity);
            if (!DataAnnotation.Validator.TryValidateObject(entity, context, daResults, true))
            {
                return daResults.Select(er => new ValidationResult(
                    er.MemberNames.DefaultIfEmpty(string.Empty).First(),
                    er.ErrorMessage)
                );
            }

            return Enumerable.Empty<ValidationResult>();
        }
    }
}
