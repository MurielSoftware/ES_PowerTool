using ES_PowerTool.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ES_PowerTool.Shared.Services
{
    public class IDataXmlService
    {
        public void Serialize(ProjectDto projectDto)
        {
            using (XmlWriter xmlWriter = XmlWriter.Create("a.xml"))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectDto));
                xmlSerializer.Serialize(xmlWriter, projectDto);
            }
        }

        public void Deserialize()
        {
            using (XmlReader xmlReader = XmlReader.Create("a.xml"))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectDto));
                xmlSerializer.Deserialize(xmlReader);
            }
        }
    }
}
