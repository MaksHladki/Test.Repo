using System;

namespace Repo.Helpers.Validation
{
    public class ValidationResult
    {
        public ValidationResult(String key, String message)
        {
            Key = key;
            Message = message;
        }

        public String Key { get; }
        public String Message { get; }
    }
}
