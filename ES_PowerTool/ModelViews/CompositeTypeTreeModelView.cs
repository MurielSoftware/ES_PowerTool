using Desktop.Shared.Core;
using Desktop.Shared.Core.Navigations;
using Desktop.App.Core.Commands;
using Desktop.App.Core.Handlers;
using Desktop.App.Core.ModelViews;
using ES_PowerTool.Handlers;
using ES_PowerTool.Shared.Services;
using System.Collections.Generic;
using System.Windows.Input;
using System;
using Desktop.Shared.Core.Services;

namespace ES_PowerTool.ModelViews
{
    public class CompositeTypeTreeModelView : BaseTreeModelView
    {
        public ICommand NewFolderCommand { get; private set; }
        public ICommand NewCompositeTypeCommand { get; private set; }
        public ICommand NewCompmositeTypeElementCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }

        public CompositeTypeTreeModelView() 
            : base("CompositeTypeTreeModelView")
        {
            NewFolderCommand = new RelayCommand(OnNewFolderCommand, x => ModelViewsUtil.IsType(x, NavigationType.PROJECT, NavigationType.FOLDER));
            NewCompositeTypeCommand = new RelayCommand(OnNewCompositeTypeCommand, x => ModelViewsUtil.IsType(x, NavigationType.FOLDER));
            NewCompmositeTypeElementCommand = new RelayCommand(OnNewCompmositeTypeElementCommand, x => ModelViewsUtil.IsType(x, NavigationType.COMPOSITE_TYPE));
            DeleteCommand = new RelayCommand(OnDeleteCommand);
            UpdateCommand = new RelayCommand(OnUpdateCommand);
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
