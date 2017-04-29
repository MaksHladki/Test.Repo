using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Repo.Helpers.Validation
{
    public class ValidationException : Exception
    {
        #region Constructors

        public ValidationException(string key, string message) : base(message)
        {
            var result = new ValidationResult(key, message);
            Errors = new ReadOnlyCollection<ValidationResult>(new List<ValidationResult> { result });
        }

        public ValidationException(ValidationResult result) : base(result.Message)
        {
            Errors = new ReadOnlyCollection<ValidationResult>(new List<ValidationResult> { result });
        }

        public ValidationException(IEnumerable<ValidationResult> results) : base(GetFirstErrorMessage(results))
        {
            Errors = new ReadOnlyCollection<ValidationResult>(results.ToArray());
        }

        #endregion

        #region Properties

        public ReadOnlyCollection<ValidationResult> Errors { get; }

        #endregion

        #region Methods

        private static String GetFirstErrorMessage(IEnumerable<ValidationResult> errors)
        {
            return errors.First().Message;
        }

        #endregion
    }
}
