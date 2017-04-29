using System;
using System.Collections;

namespace Repo.Helpers.Validation
{
    public interface IValidationProvider
    {
        void Validate(Object entity);
        void ValidateAll(IEnumerable entities);
    }
}
