using Desktop.Data.Core.DAL;
using Desktop.Shared.Core.Context;

namespace ES_PowerTool.Data.DAL
{
    public class CompositeTypeElementRepository : BaseRepository
    {
        public CompositeTypeElementRepository(Connection connection)
            : base(connection)
        {
        }
    }
}
