using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core.Validations
{
    public class ValidationResult
    {
        public List<ValidationMessage> ValidationMessages { get; private set; }

        public ValidationResult()
        {
            ValidationMessages = new List<ValidationMessage>();
        }

        public void Add(ValidationMessage validationMessage)
        {
            ValidationMessages.Add(validationMessage);
        }

        public void AddRange(List<ValidationMessage> validationMessages)
        {
            ValidationMessages.AddRange(validationMessages);
        }

        public bool IsEmpty()
        {
            return ValidationMessages.Count == 0;
        }

        //public List<ValidationMessage> GetValidationMessages()
        //{
        //    return _validationMessages;
        //}
    }
}
