using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core.Navigations
{
    public class NavigationTypeToImage
    {
        public static Dictionary<NavigationType, Uri> NAVIGATION_TYPE_TO_IMAGE = new Dictionary<NavigationType, Uri>()
        {
            { NavigationType.FOLDER, new Uri("pack://application:,,,/Images/folder.png") },
            { NavigationType.TYPE, new Uri("pack://application:,,,/Images/type.png") },
            { NavigationType.TYPE_ELEMENT, new Uri("pack://application:,,,/Images/element.png") },
        };
    }
}
