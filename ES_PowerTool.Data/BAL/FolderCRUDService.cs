using ES_PowerTool.Data.Model;
using ES_PowerTool.Shared.Dtos;
using ES_PowerTool.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;

namespace ES_PowerTool.Data.BAL
{
    public class FolderCRUDService : GenericCRUDService<FolderDto, Folder>, IFolderCRUDService
    {
        public FolderCRUDService(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
