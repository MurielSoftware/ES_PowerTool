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
    public class CompositeTypeElementCRUDService : GenericCRUDService<CompositeTypeElementDto, CompositeTypeElement>, ICompositeTypeElementCRUDService
    {
        public CompositeTypeElementCRUDService(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
