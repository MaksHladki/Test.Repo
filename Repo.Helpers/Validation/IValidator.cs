using System;
using System.Collections.Generic;

namespace Repo.Helpers.Validation
{
    public interface IValidator
    {
        IEnumerable<ValidationResult> Validate(Object entity);
    }
}
