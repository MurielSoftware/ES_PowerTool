using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.Dtos;
using Desktop.Shared.Core.Services;
using Desktop.Ui.Core.Commands;
using Desktop.Ui.Core.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop.Ui.Core.ModelViews
{
    public class WizardModelView<T> : BaseModelView, IWizardModelView where T : BaseDto
    {
        public T Dto { get; protected set; }
        public ICRUDService<T> _crudService;
        public string Title { get; private set; }
        public string Description { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand FinishCommand { get; private set; }
        public string ValidationMessage { get; private set; }

        public WizardModelView() 
            : base(typeof(WizardModelView<>).Name)
        {
            Dto = Activator.CreateInstance<T>();
            FinishCommand = new RelayCommand(OnFinishCommand);
            CancelCommand = new RelayCommand(OnCancelCommand);

            CreateTitle();
        }

        public BaseDto GetDto()
        {
            return Dto;
        }

        protected virtual void OnCancelCommand(object obj)
        {

        }

        protected virtual void OnFinishCommand(object obj)
        {
            try
            {
                LongRunningJob<T> projectCreationJob = new LongRunningJob<T>(DoFinish, Title);
                projectCreationJob.Execute(Dto);
            }
            catch(Exception ex)
            {

            }
        }

        protected virtual void DoFinish(T dto)
        {
            try
            {
                Dto = _crudService.Persist(Dto);
            }
            catch(Exception ex)
            {

            }
        }

        private void CreateTitle()
        {
            Title = ((LocalizedDisplayNameAttribute)Dto.GetType().GetCustomAttributes(typeof(LocalizedDisplayNameAttribute), false).SingleOrDefault()).DisplayName;
        }
    }
}
