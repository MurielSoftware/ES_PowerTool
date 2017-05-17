using ES_PowerTool.Shared.Dtos;
using ES_PowerTool.Shared.Services;
using Desktop.Shared.Core.Context;
using Desktop.Data.Core.BAL;
using Desktop.Data.Core.Model;

namespace ES_PowerTool.Data.BAL
{
    public class CompositeTypeCRUDService : GenericCRUDService<CompositeTypeDto, CompositeType>, ICompositeTypeCRUDService
    {
        public CompositeTypeCRUDService(Connection connection) 
            : base(connection)
        {
        }
    }
}
