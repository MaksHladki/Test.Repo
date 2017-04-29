using System;
using System.Collections.Generic;
using Repo.Helpers.Validation;
using Repo.Model.Models;

namespace Repo.BAL.Validation
{
    public class UserValidator : Validator<User>
    {
        protected override IEnumerable<ValidationResult> Validate(User entity)
        {
            throw new NotImplementedException("UserValidator has not been implemented yet.");
        }
    }
}
