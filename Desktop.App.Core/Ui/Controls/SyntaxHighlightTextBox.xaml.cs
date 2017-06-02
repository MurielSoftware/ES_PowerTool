using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desktop.App.Core.Ui.Controls
{
    /// <summary>
    /// Interaction logic for SyntaxHighlightTextBox.xaml
    /// </summary>
    public partial class SyntaxHighlightTextBox : TextBox
    {
        public SyntaxHighlightTextBox()
        {
           // InitializeComponent();
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            FormattedText formattedText = new FormattedText(Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, 
                new Typeface("Courier New"), 18, Brushes.Black);
            drawingContext.DrawText(formattedText, new Point(10, 10));
        }
    }
}
