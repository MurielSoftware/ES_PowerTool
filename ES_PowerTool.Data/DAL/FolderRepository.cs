using Desktop.Shared.Core.Context;
using Desktop.Data.Core.DAL;

namespace ES_PowerTool.Data.DAL
{
    public class FolderRepository : BaseRepository
    {
        public FolderRepository(Connection connection) 
            : base(connection)
        {
        }
    }
}
