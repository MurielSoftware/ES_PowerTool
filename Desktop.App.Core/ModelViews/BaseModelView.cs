using Desktop.App.Core.Events.Selection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.App.Core.ModelViews
{
    public abstract class BaseModelView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _modelViewId;
        protected MasterSelectionChangeService _masterSelectionChangeService = new MasterSelectionChangeService();


        public BaseModelView(string modelViewId)
        {
            _modelViewId = modelViewId;
        }

        public string GetModelViewId()
        {
            return _modelViewId;
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged<T>(Expression<Func<T>> expression)
        {
            var property = (MemberExpression)expression.Body;
            OnPropertyChanged(property.Member.Name);
        }

        protected bool SetProperty<T>(ref T backingField, T Value, Expression<Func<T>> propertyExpression)
        {
            var changed = !EqualityComparer<T>.Default.Equals(backingField, Value);
            if (changed)
            {
                backingField = Value;
                OnPropertyChanged(propertyExpression);
            }
            return changed;
        }
    }
}
