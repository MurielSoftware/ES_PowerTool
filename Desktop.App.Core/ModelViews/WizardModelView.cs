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
using Desktop.Shared.Core.Validations;
using Desktop.Ui.I18n;
using Log4N.Logger;
using System.Windows;

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
        public Visibility InfoVisibility { get; private set; }
        public Visibility ValidationMessageVisibility { get; private set; }

        protected ICRUDService<T> _crudService;
        protected BasePersister<T> _persister;
        protected Wizard _wizard;

        private bool _successFinish;

        public WizardModelView(T dto) 
            : base(typeof(WizardModelView<>).Name)
        {
            Dto = dto;
            FinishCommand = new RelayCommand(OnFinishCommand);
            CancelCommand = new RelayCommand(OnCancelCommand);
            InfoVisibility = Visibility.Visible;
            ValidationMessageVisibility = Visibility.Hidden;

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
            LongRunningJob<T> projectCreationJob = new LongRunningJob<T>(DoFinish, Title);
            projectCreationJob.AddAction(delegate 
            {
                _wizard.Dispatcher.Invoke(DispatcherPriority.Normal, new ThreadStart(CloseWizardOnFinish));
            });
            projectCreationJob.Execute(Dto);
        }

        protected virtual void CloseWizardOnFinish()
        {
            if (_successFinish)
            {
                _wizard.DialogResult = true;
                _wizard.Close();
            }
        }

        protected virtual void DoFinish(T dto)
        {
            if (!dto.IsValid)
            {
                Log.Warning(string.Format("The object type({0}) client side validation exception", typeof(T)));
                OnClientSideFailed(dto.GetValidationResult());
                return;
            }

            try
            {
                DoPersist(dto);
                Log.Info(string.Format("The object type({0}) was created/updated", typeof(T)));
                _successFinish = true;
            }
            catch (ValidationException ex)
            {
                Log.Warning(string.Format("The object type({0}) server side validation exception", typeof(T)));
                OnServerSideFailed(dto, ex.GetValidationResult());
            }
        }

        protected virtual void DoPersist(T dto)
        {
            _persister.Persist();
        }

        protected virtual void OnClientSideFailed(string validationResult)
        {
            ValidationMessage = validationResult;
            InfoVisibility = Visibility.Collapsed;
            ValidationMessageVisibility = Visibility.Visible;
            OnPropertyChanged(() => ValidationMessage);
            OnPropertyChanged(() => InfoVisibility);
            OnPropertyChanged(() => ValidationMessageVisibility);
        }

        protected virtual void OnServerSideFailed(T dto, ValidationResult validationResult)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ValidationMessage validationMessage in validationResult.ValidationMessages)
            {
                sb.AppendLine(ResourceUtils.GetMessage(validationMessage.ResourceKey, validationMessage.Parameters));
            }
            ValidationMessage = sb.ToString();
            InfoVisibility = Visibility.Collapsed;
            ValidationMessageVisibility = Visibility.Visible;
            OnPropertyChanged(() => ValidationMessage);
            OnPropertyChanged(() => InfoVisibility);
            OnPropertyChanged(() => ValidationMessageVisibility);
        }

        private void CreateTitle()
        {
            Title = ((LocalizedDisplayNameAttribute)Dto.GetType().GetCustomAttributes(typeof(LocalizedDisplayNameAttribute), false).SingleOrDefault()).DisplayName;
        }
    }
}
