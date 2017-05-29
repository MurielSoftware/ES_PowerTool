using Desktop.Shared.Core;
using Desktop.Shared.Core.Navigations;
using Desktop.App.Core.Commands;
using Desktop.App.Core.Handlers;
using Desktop.App.Core.ModelViews;
using ES_PowerTool.Handlers;
using System.Collections.Generic;
using System.Windows.Input;
using Desktop.Shared.Core.Services;
using ES_PowerTool.Shared.Services.OOE.Types;

namespace ES_PowerTool.ModelViews
{
    public class CompositeTypeTreeModelView : BaseTreeModelView
    {
        public ICommand NewFolderCommand { get; private set; }
        public ICommand NewCompositeTypeCommand { get; private set; }
        public ICommand NewCompmositeTypeElementCommand { get; private set; }
        public ICommand GenerateClassCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }

        public CompositeTypeTreeModelView() 
            : base("CompositeTypeTreeModelView")
        {
            NewFolderCommand = new RelayCommand(OnNewFolderCommand, x => ModelViewsUtil.IsType(x, NavigationType.PROJECT, NavigationType.FOLDER) && !ModelViewsUtil.IsBuiltIn(x));
            NewCompositeTypeCommand = new RelayCommand(OnNewCompositeTypeCommand, x => ModelViewsUtil.IsType(x, NavigationType.FOLDER) && !ModelViewsUtil.IsBuiltIn(x));
            NewCompmositeTypeElementCommand = new RelayCommand(OnNewCompmositeTypeElementCommand, x => ModelViewsUtil.IsType(x, NavigationType.COMPOSITE_TYPE) && !ModelViewsUtil.IsBuiltIn(x));
            GenerateClassCommand = new RelayCommand(OnGenerateClassCommand, x => ModelViewsUtil.IsType(x, NavigationType.COMPOSITE_TYPE));
            DeleteCommand = new RelayCommand(OnDeleteCommand, x => !ModelViewsUtil.IsBuiltIn(x) && ModelViewsUtil.IsType(x, NavigationType.FOLDER, NavigationType.COMPOSITE_TYPE, NavigationType.TYPE_ELEMENT));
            UpdateCommand = new RelayCommand(OnUpdateCommand, x => !ModelViewsUtil.IsBuiltIn(x) && ModelViewsUtil.IsType(x, NavigationType.FOLDER, NavigationType.COMPOSITE_TYPE, NavigationType.TYPE_ELEMENT));
        }

        protected override INavigationService CreateNavigationService()
        {
            return ServiceActivator.Get<ICompositeTypeNavigationService>();
        }

        private void OnNewFolderCommand(object obj)
        {
            HandlerExecutor.Execute<NewFolderHandler>(ExecutionEvent.Create(obj as List<TreeNavigationItem>));
        }

        private void OnNewCompositeTypeCommand(object obj)
        {
            HandlerExecutor.Execute<NewCompositeTypeHandler>(ExecutionEvent.Create(obj as List<TreeNavigationItem>));
        }

        private void OnNewCompmositeTypeElementCommand(object obj)
        {
            HandlerExecutor.Execute<NewCompositeTypeElementHandler>(ExecutionEvent.Create(obj as List<TreeNavigationItem>));
        }

        private void OnGenerateClassCommand(object obj)
        {
            HandlerExecutor.Execute<GenerateDtoHandler>(ExecutionEvent.Create(obj as List<TreeNavigationItem>));
        }

        private void OnDeleteCommand(object obj)
        {
            HandlerExecutor.ExecuteEx(typeof(DeleteEntityHandler<>), obj);
        }

        private void OnUpdateCommand(object obj)
        {
            HandlerExecutor.ExecuteEx(typeof(UpdateEntityHandler<>), obj);
        }
    }
}
