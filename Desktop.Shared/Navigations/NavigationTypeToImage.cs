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
            { NavigationType.PROJECT, new Uri("pack://application:,,,/Images/project.png") },
            { NavigationType.FOLDER, new Uri("pack://application:,,,/Images/folder.gif") },
            { NavigationType.PRIMITIVE_TYPE, new Uri("pack://application:,,,/Images/red_square.gif") },
            { NavigationType.COMPOSITE_TYPE, new Uri("pack://application:,,,/Images/type.png") },
            { NavigationType.TYPE_ELEMENT, new Uri("pack://application:,,,/Images/green_circle_filled.gif") },
            { NavigationType.PRESET, new Uri("pack://application:,,,/Images/preset.png") },
            { NavigationType.COMPOSITE_PRESET_ELEMENT, new Uri("pack://application:,,,/Images/green_circle_filled.gif") },
        };
    }
}
