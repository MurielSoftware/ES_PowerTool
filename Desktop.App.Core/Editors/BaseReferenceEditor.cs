using Desktop.Shared.Core.DataTypes;
using Desktop.Shared.Core.Navigations;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Desktop.App.Core.Editors
{
    public abstract class BaseReferenceEditor
    {
        public async Task<List<TreeNavigationItem>> GetProposals()
        {
            return await Task.Run(() =>
            {
                return DoGetProposals();
            });
        }

        protected abstract List<TreeNavigationItem> DoGetProposals();
    }
    //public abstract class BaseReferenceEditor : ITypeEditor
    //{
    //    private PropertyItem _propertyItem;
    //    private Label _referenceLabel;

    //    public FrameworkElement ResolveEditor(PropertyItem propertyItem)
    //    {
    //        _propertyItem = propertyItem;
    //        _propertyItem.WillRefreshPropertyGrid = true;

    //        Grid grid = CreateGrid();

    //        Label referenceLabel = CreateReferenceLabel(propertyItem.Value);
    //        Button browseButton = CreateButton("pack://application:,,,/Images/dots_16.png", ReferenceButtonClick);
    //        Button removeReferenceButton = CreateButton("pack://application:,,,/Images/delete_16.png", RemoveReferenceButtonClick);

    //        grid.Children.Add(referenceLabel);
    //        grid.Children.Add(browseButton);
    //        grid.Children.Add(removeReferenceButton);

    //        Grid.SetColumn(referenceLabel, 0);
    //        Grid.SetColumn(browseButton, 1);
    //        Grid.SetColumn(removeReferenceButton, 2);

    //        return grid;
    //    }

    //    private Grid CreateGrid()
    //    {
    //        Grid grid = new Grid();
    //        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.5, GridUnitType.Star) });
    //        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(24, GridUnitType.Pixel) });
    //        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(24, GridUnitType.Pixel) });
    //        return grid;
    //    }

    //    private Label CreateReferenceLabel(object value)
    //    {
    //        _referenceLabel = new Label();
    //        _referenceLabel.MouseDoubleClick += LabelMouseDoubleClick;
    //        if (value == null)
    //        {
    //            _referenceLabel.Content = string.Empty;
    //        }
    //        else
    //        {
    //            _referenceLabel.Content = ((ReferenceString)value).GetValue();
    //        }
    //        return _referenceLabel;
    //    }

    //    private Button CreateButton(string imageUri, RoutedEventHandler click)
    //    {
    //        Button button = new Button();
    //        button.Click += click;
    //        button.Width = 22;
    //        button.Height = 22;
    //        Image image = new Image();
    //        image.Source = new BitmapImage(new Uri(imageUri));
    //        button.Content = image;
    //        return button;
    //    }

    //    private void ReferenceButtonClick(object sender, RoutedEventArgs e)
    //    {
    //        OpenReferenceWindow();
    //    }

    //    private void RemoveReferenceButtonClick(object sender, RoutedEventArgs e)
    //    {
    //        SetNewValue(null);
    //    }

    //    private void LabelMouseDoubleClick(object sender, RoutedEventArgs e)
    //    {
    //        OpenReferenceWindow();
    //    }

    //    private void OpenReferenceWindow()
    //    {
    //        //TreeNavigationItem selectedTreeNavigationItem = DialogUtils.OpenReferenceWindow(GetProposals);
    //        //if (selectedTreeNavigationItem != null)
    //        //{
    //        //    SetNewValue(new ReferenceString(selectedTreeNavigationItem.Id, selectedTreeNavigationItem.Name));
    //        //}
    //    }

    //    private void SetNewValue(ReferenceString referenceString)
    //    {
    //        PropertyInfo propertyInfo = _propertyItem.Instance.GetType().GetProperty(_propertyItem.PropertyName);
    //        propertyInfo.SetValue(_propertyItem.Instance, referenceString);
    //        if (referenceString == null)
    //        {
    //            _referenceLabel.Content = string.Empty;
    //        }
    //        else
    //        {
    //            _referenceLabel.Content = referenceString.GetValue();
    //        }
    //        _propertyItem.Value = referenceString;
    //    }

    //    //internal async Task<List<TreeNavigationItem>> GetProposals()
    //    //{
    //    //    return await Task.Run(() => {
    //    //        return DoGetProposals();
    //    //    });
    //    //}

    //    //protected abstract List<TreeNavigationItem> DoGetProposals();
    //}
}
