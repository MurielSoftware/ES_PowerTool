using Desktop.Shared.Core.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.App.Core.Editors
{
    public interface IReferenceEditor
    {
        Task<List<TreeNavigationItem>> GetProposals();
    }
}
