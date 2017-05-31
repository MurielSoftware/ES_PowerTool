using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Templates.Structures
{
    public class ClassStructure
    {
        public Modifier Modifier { get; private set; }
        public string Name { get; private set; }
        public string Namespace { get; set; }

        public List<Annotation> Annotations { get; private set; }
        public List<FieldStructure> Fields { get; private set; }

        public ClassStructure(Modifier modifier, string name)
        {
            Modifier = modifier;
            Name = name;
            Annotations = new List<Annotation>();
            Fields = new List<FieldStructure>();
        }

        public void AddAnnotation(Annotation annotation)
        {
            Annotations.Add(annotation);
        }

        public void AddField(FieldStructure field)
        {
            Fields.Add(field);
        }

        //public List<Annotation> GetAnnotations()
        //{
        //    return _annotations;
        //}
    }
}
