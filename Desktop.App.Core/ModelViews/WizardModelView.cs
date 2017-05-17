using Desktop.App.Core.Ui.Windows;
using Desktop.Shared;
using Desktop.Shared.Core;
using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.Dtos;
using Desktop.Shared.Core.Services;
using Desktop.App.Core.Commands;
using Desktop.App.Core.Handlers;
using Desktop.App.Core.Jobs;
using Desktop.App.Core.Persisters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Desktop.App.Core.ModelViews
{
    public class WizardModelView<T> : BaseModelView, IWizardModelView where T : BaseDto
    {
        public T Dto { get; protected set; }        
        public string Title { get; private set; }
        public string Description { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand FinishCommand { get; private set; }
        public string ValidationMessage { get; private set; }

        protected ICRUDService<T> _crudService;
        protected BasePersister<T> _persister;
        protected Wizard _wizard;

        public WizardModelView(T dto) 
            : base(typeof(WizardModelView<>).Name)
        {
            Dto = dto;
            FinishCommand = new RelayCommand(OnFinishCommand);
            CancelCommand = new RelayCommand(OnCancelCommand);

            _crudService = (ICRUDService<T>)ServiceActivator.Get(HandlerUtils.DTO_TO_SERVICE[typeof(T)]);
            _persister = new BasePersister<T>(_crudService, Dto);

            CreateTitle();
        }

        public BaseDto GetDto()
        {
            return Dto;
        }

        public ICRUDService<T> GetService()
        {
            return _crudService;
        }

        public void SetPersister(BasePersister<T> persister)
        {
            _persister = persister;
        }

        protected virtual void OnCancelCommand(object obj)
        {
            Wizard wizard = (Wizard)obj;
            wizard.DialogResult = false;
            wizard.Close();
        }

        protected virtual void OnFinishCommand(object obj)
        {
            _wizard = (Wizard)obj;
            try
            {
                LongRunningJob<T> projectCreationJob = new LongRunningJob<T>(DoFinish, Title);
                projectCreationJob.AddAction(delegate 
                {
                    _wizard.Dispatcher.Invoke(DispatcherPriority.Normal, new ThreadStart(CloseWizard));
                });
                projectCreationJob.Execute(Dto);
            }
            catch(Exception ex)
            {

            }
        }

        protected virtual void CloseWizard()
        {
            _wizard.DialogResult = true;
            _wizard.Close();
        }

        protected virtual void DoFinish(T dto)
        {
            _persister.Persist();
        }

        private void CreateTitle()
        {
            Title = ((LocalizedDisplayNameAttribute)Dto.GetType().GetCustomAttributes(typeof(LocalizedDisplayNameAttribute), false).SingleOrDefault()).DisplayName;
        }
    }
}
