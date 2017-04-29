using System;
using System.Collections.Generic;
using Repo.Helpers.Validation;
using Repo.Model.Models;

namespace Repo.BAL.Validation
{
    public class MessageValidator : Validator<Message>
    {
        protected override IEnumerable<ValidationResult> Validate(Message entity)
        {
            throw new NotImplementedException("MessageValidator has not been implemented yet.");
        }
    }
}
