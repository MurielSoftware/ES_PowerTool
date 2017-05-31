using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Templates.Structures
{
    public class FieldStructure
    {
        public Modifier Modifier { get; private set; }
        public string DataType { get; private set; }
        public string Name { get; private set; }
        public List<Annotation> Annotations { get; private set; }

        public FieldStructure(Modifier modifier, string dataType, string name)
        {
            Modifier = modifier;
            DataType = dataType;
            Name = name;
            Annotations = new List<Annotation>();
        }

        public void AddAnnotation(Annotation annotation)
        {
            Annotations.Add(annotation);
        }

        public List<Annotation> GetAnnotations()
        {
            return Annotations;
        }
    }
}
