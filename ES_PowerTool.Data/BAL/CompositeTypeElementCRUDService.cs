using ES_PowerTool.Shared.Dtos;
using ES_PowerTool.Shared.Services;
using Desktop.Shared.Core.Context;
using Desktop.Data.Core.Model;
using Desktop.Data.Core.BAL;

namespace ES_PowerTool.Data.BAL
{
    public class CompositeTypeElementCRUDService : GenericCRUDService<CompositeTypeElementDto, CompositeTypeElement>, ICompositeTypeElementCRUDService
    {
        public CompositeTypeElementCRUDService(Connection connection) 
            : base(connection)
        {
        }
    }
}
