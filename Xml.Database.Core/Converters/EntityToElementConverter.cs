using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Xml.Database.Converters
{
    public class EntityToElementConverter
    {
        public XElement Convert(object entity)
        {
            XElement element = new XElement(entity.GetType().Name);
            List<PropertyInfo> attributePropertyInfos = entity.GetType().GetProperties().Where(x => Attribute.IsDefined(x, typeof(XmlAttributeAttribute))).ToList();

            foreach(PropertyInfo attributePropertyInfo in attributePropertyInfos)
            {
                XmlAttributeAttribute xmlAttribute = attributePropertyInfo.GetCustomAttribute<XmlAttributeAttribute>();
                element.Add(new XAttribute(xmlAttribute.AttributeName, attributePropertyInfo.GetValue(entity)));
            }

            return element;
        }
    }
}
