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

namespace ES_PowerTool.ModelViews
{
    public class CompositeTypeTreeModelView : BaseTreeModelView
    {
        public ICommand NewFolderCommand { get; private set; }
        public ICommand NewTypeCommand { get; private set; }
        public ICommand NewTypeElementCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }

        public CompositeTypeTreeModelView() 
            : base("CompositeTypeTreeModelView")
        {
            NewFolderCommand = new RelayCommand(OnNewFolderCommand);
            NewTypeCommand = new RelayCommand(OnNewTypeCommand);
            NewTypeElementCommand = new RelayCommand(OnNewTypeElementCommand);
            DeleteCommand = new RelayCommand(OnDeleteCommand);
            UpdateCommand = new RelayCommand(OnUpdateCommand);
        }

        public override void SetService()
        {
            _service = ServiceActivator.Get<ICompositeTypeNavigationService>();
        }

        private void OnNewFolderCommand(object obj)
        {
            HandlerExecutor.Execute<NewFolderHandler>(ExecutionEvent.Create(obj as List<TreeNavigationItem>));
        }

        private void OnNewTypeCommand(object obj)
        {
            HandlerExecutor.Execute<NewTypeHandler>(ExecutionEvent.Create(obj as List<TreeNavigationItem>));
        }

        private void OnNewTypeElementCommand(object obj)
        {
            HandlerExecutor.Execute<NewTypeElementHandler>(ExecutionEvent.Create(obj as List<TreeNavigationItem>));
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
