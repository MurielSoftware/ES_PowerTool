using Desktop.Shared.Core.Services;
using ES_PowerTool.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using ES_PowerTool.Data.DAL;
using Desktop.Data.Core.Model;
using ES_PowerTool.Shared.CSV;
using ES_PowerTool.Shared.Dtos;

namespace ES_PowerTool.Data.BAL
{
    public class GenerateService : BaseService, IGenerateService
    {
        private FolderRepository _folderRepository;

        public GenerateService(Connection connection) 
            : base(connection)
        {
            _folderRepository = new FolderRepository(connection);
        }

        public GenerateDto Generate()
        {
            GenerateDto generateDto = new GenerateDto();
            List<Folder> folders = _folderRepository.GetFoldersToExport();
            generateDto.GeneratedCSVFolder = CSVWriter.Write<FolderDto>(folders);
            return generateDto;
        }
    }
}
