using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Templates.Structures
{
    public class Annotation
    {
        public static string EntityAnnotation = "@Entity";
        public static string TableAnnotation = "@Table(name = \"{0}\")";
        public static string DiscriminatorValueAnnotation = "@DiscriminatorValue(\"{0}\")";
        public static string IdAnnotation = "@Id";
        public static string ColumnAnnotation = "@Column(name = \"{0}\")";
        public static string TransientAnnotation = "@Transient";

        private string _name;
        private object[] _attributes;

        private Annotation(string name, params object[] attributes)
        {
            _name = name;
            _attributes = attributes;
        }

        public static Annotation CreateAnnotation(string name, params object[] attributes)
        {
            return new Annotation(name, attributes);
        }

        public override string ToString()
        {
            return string.Format(_name, _attributes);
        }
    }
}
