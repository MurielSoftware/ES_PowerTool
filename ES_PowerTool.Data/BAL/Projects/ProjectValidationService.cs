using Desktop.Shared.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using Desktop.Data.Core.DAL;
using Desktop.Shared.Core.Validations;
using ES_PowerTool.Shared.Dtos;
using Desktop.Data.Core.Model;
using Desktop.Ui.I18n;

namespace ES_PowerTool.Data.BAL.Projects
{
    public class ProjectValidationService : BaseService
    {
        private GenericRepository _genericRepository;

        public ProjectValidationService(Connection connection)
            : base(connection)
        {
            _genericRepository = new GenericRepository(connection);
        }

        public ValidationResult CollectValidationResultBeforePersist(ProjectDto projectDto)
        {
            ValidationResult validationResult = new ValidationResult();
            validationResult.AddRange(CollectIsNameUniqueValidationMessage(projectDto));
            return validationResult;
        }

        private List<ValidationMessage> CollectIsNameUniqueValidationMessage(ProjectDto projectDto)
        {
            List<ValidationMessage> validationMessages = new List<ValidationMessage>();
            if (_genericRepository.Exists<Project>(x => x.Name == projectDto.Name && x.Id != projectDto.Id))
            {
                validationMessages.Add(new ValidationMessage(ValidationType.ERROR, MessageKeyConstants.VALIDATION_MESSAGE_NAME_IS_NOT_UNIQUE, projectDto.Name));
            }
            return validationMessages;
        }
    }
}
