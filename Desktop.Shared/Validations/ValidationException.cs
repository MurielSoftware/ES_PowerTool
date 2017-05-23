using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core.Validations
{
    public class ValidationException : Exception
    {
        private ValidationResult _validationResult;

        public ValidationException(ValidationResult validationResult)
        {
            _validationResult = validationResult;
        }

        public ValidationResult GetValidationResult()
        {
            return _validationResult;
        }
    }
}
