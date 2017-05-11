using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Xml.Database.Converters
{
    public class ElementToEntityConverter
    {
        public object Convert(object entity, XElement element)
        {
            foreach(XAttribute attribute in element.Attributes())
            {
                PropertyInfo propertyInfo = entity.GetType().GetProperty(attribute.Name.LocalName);
                if(propertyInfo != null)
                {
                    propertyInfo.SetValue(entity, attribute.Value);
                }
            }
            return entity;
        }
    }
}
