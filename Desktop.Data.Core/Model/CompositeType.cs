using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Data.Core.Model
{
    public class CompositeType : ModelObjectType
    {
        public static string DISC = "COM";

        [InverseProperty("OwningType")]
        public virtual ICollection<CompositeTypeElement> Children { get; set; }

        public virtual ICollection<CompositeType> SuperTypes { get; set; }
    }
}
