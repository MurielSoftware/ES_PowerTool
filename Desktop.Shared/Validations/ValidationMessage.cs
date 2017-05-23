using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core.Validations
{
    public class ValidationMessage
    {
        public string ResourceKey { get; private set; }
        public ValidationType Type { get; private set; }

        public ValidationMessage(ValidationType type, string resourceKey)
        {
            ResourceKey = resourceKey;
            Type = type;
        }
    }
}
