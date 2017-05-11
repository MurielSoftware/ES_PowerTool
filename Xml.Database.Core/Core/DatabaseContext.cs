using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Xml.Database.Converters;

namespace Xml.Database.Core
{
    public class DatabaseContext
    {
        private Database _database;
        private XDocument _xDocument;

        private ElementToEntityConverter elementToEntityConverter = new ElementToEntityConverter();
        private EntityToElementConverter entityToElementConverter = new EntityToElementConverter();

        public DatabaseContext(string path, string name)
        {
            _database = new Database(string.Format(@"{0}\{1}", path, name));
            _xDocument = XDocument.Load(_database.GetPath());
        }

        public T Find<T>(object key)
        {
            XmlRootAttribute xmlRootAttribute = (XmlRootAttribute)typeof(T).GetCustomAttributes(typeof(XmlRootAttribute), false).SingleOrDefault();
            XElement xElement = _xDocument.Descendants(xmlRootAttribute.ElementName).Where(x => x.Attribute("id") == key).FirstOrDefault();
            return default(T);
        }

        public void Insert(object obj)
        {
            XElement element = entityToElementConverter.Convert(obj);
            XmlRootAttribute xmlRootAttribute = (XmlRootAttribute)obj.GetType().GetCustomAttributes(typeof(XmlRootAttribute), false).SingleOrDefault();
            _xDocument.Element(xmlRootAttribute.ElementName).Add(element);
        }

        public void Update(object obj)
        {
            XElement element = entityToElementConverter.Convert(obj);
            XmlRootAttribute xmlRootAttribute = (XmlRootAttribute)obj.GetType().GetCustomAttributes(typeof(XmlRootAttribute), false).SingleOrDefault();
            PropertyInfo keyPropertyInfo = obj.GetType().GetProperties().Where(x => Attribute.IsDefined(x, typeof(KeyAttribute))).SingleOrDefault();
            _xDocument.Descendants(xmlRootAttribute.ElementName).Where(x => x.Attribute("id") == keyPropertyInfo.GetValue(obj)).Remove();
            _xDocument.Element(xmlRootAttribute.ElementName).Add(element);
        }

        public void Delete<T>(object key)
        {
            XmlRootAttribute xmlRootAttribute = (XmlRootAttribute)typeof(T).GetCustomAttributes(typeof(XmlRootAttribute), false).SingleOrDefault();
            _xDocument.Descendants(xmlRootAttribute.ElementName).Where(x => x.Attribute("id") == key).Remove();
        }

        public List<T> Set<T>(Expression<Func<T, bool>> where = null)
        {
            Func<XElement, bool> whereFinalExpression = GetWhereExpression<T>(where);
            XmlRootAttribute xmlRootAttribute = (XmlRootAttribute)typeof(T).GetCustomAttributes(typeof(XmlRootAttribute), false).SingleOrDefault();
            List<XElement> elements = _xDocument.Descendants(xmlRootAttribute.ElementName).Where(whereFinalExpression).ToList();
            return null;
        }

        private Func<XElement, bool> GetWhereExpression<T>(Expression<Func<T, bool>> where)
        {
            return null;
        }

        public void ApplyChanges()
        {
            _xDocument.Save(_database.GetPath());
        }
    }
}
