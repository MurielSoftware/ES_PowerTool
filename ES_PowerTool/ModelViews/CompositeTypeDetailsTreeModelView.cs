using Desktop.App.Core.ModelViews;
using System.Collections.Generic;
using Desktop.Shared.Core.Services;
using Desktop.Shared.Core;
using System.Windows.Input;
using Desktop.App.Core.Commands;
using Desktop.App.Core.Handlers;
using ES_PowerTool.Handlers;
using Desktop.Shared.Core.Navigations;
using ES_PowerTool.Shared.Services.OOE.Types;

namespace ES_PowerTool.ModelViews
{
    public class CompositeTypeDetailsTreeModelView : BaseDetailsTreeModelView
    {
        public ICommand NewPresetCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand SetAsDefaultCommand { get; private set; }

        public CompositeTypeDetailsTreeModelView() 
            : base("CompositeTypeDetailsTreeModelView")
        {
            NewPresetCommand = new RelayCommand(OnNewPresetCommand, x => ModelViewsUtil.IsType(x, NavigationType.FOLDER));
            DeleteCommand = new RelayCommand(OnDeleteCommand, x=> ModelViewsUtil.IsType(x, NavigationType.PRESET) && !ModelViewsUtil.IsBuiltIn(x));
            UpdateCommand = new RelayCommand(OnUpdateCommand, x => ModelViewsUtil.IsType(x, NavigationType.PRESET) && !ModelViewsUtil.IsBuiltIn(x));
            SetAsDefaultCommand = new RelayCommand(OnSetAsDefaultCommand, x => ModelViewsUtil.IsType(x, NavigationType.PRESET));
        }

        protected override INavigationService CreateNavigationService()
        {
            return ServiceActivator.Get<ICompositeTypeDetailsNavigationService>();
        }

        private void OnNewPresetCommand(object obj)
        {
            HandlerExecutor.Execute<NewPresetHandler>(ExecutionEvent.Create(new List<TreeNavigationItem>() { GetMasterNavigationItem() }));
        }

        private void OnDeleteCommand(object obj)
        {
            HandlerExecutor.ExecuteEx(typeof(DeleteEntityHandler<>), obj);
        }

        private void OnUpdateCommand(object obj)
        {
            HandlerExecutor.ExecuteEx(typeof(UpdateEntityHandler<>), obj);
        }

        private void OnSetAsDefaultCommand(object obj)
        {
            HandlerExecutor.Execute<DefaultHandler>(ExecutionEvent.Create((List<TreeNavigationItem>) obj, GetMasterNavigationItem()));
        }
    }
}
