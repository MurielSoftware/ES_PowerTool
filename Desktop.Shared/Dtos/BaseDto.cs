using Desktop.Shared.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Desktop.Shared.Core.Dtos
{
    public abstract class BaseDto : IDataErrorInfo
    {
        [Browsable(false)]
        [CSVAttribute("ID", 0)]
        public Guid Id { get; set; }

        [Browsable(false)]
        string IDataErrorInfo.Error { get { return string.Empty; } }

        [Browsable(false)]
        public bool IsValid
        {
            get
            {
                foreach (PropertyInfo propertyInfo in GetType().GetProperties().Where(x => x.Name != "IsValid").ToList())
                {
                    if (GetValidationError(propertyInfo.Name) != null)
                    {
                        return false;
                    }
                }
                return true;
            }
        }



        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                return GetValidationError(columnName);
            }
        }

        public string GetValidationResult()
        {
            //List<string> validationResults = new List<string>();
            StringBuilder sb = new StringBuilder();
            foreach (PropertyInfo propertyInfo in GetType().GetProperties().Where(x => x.Name != "IsValid").ToList())
            {
                string validationResult = GetValidationError(propertyInfo.Name);
                if (!string.IsNullOrEmpty(validationResult))
                {
                    sb.AppendLine(validationResult);
                }
            }
            return sb.ToString();
        }

        private string GetValidationError(string columnName)
        {
            PropertyInfo propertyInfo = GetType().GetProperty(columnName);
            object value = propertyInfo.GetValue(this, null);

            var context = new ValidationContext(this, null, null) { MemberName = columnName };
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateProperty(value, context, validationResults))
            {
                var sb = new StringBuilder();
                foreach (var vr in validationResults)
                {
                    sb.AppendLine(vr.ErrorMessage);
                }
                return sb.ToString().Trim();
            }
            return null;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is BaseDto))
            {
                return false;
            }

            BaseDto rsd = obj as BaseDto;
            return Id.Equals(rsd.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
