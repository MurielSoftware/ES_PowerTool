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
        public static DependencyProperty LineNumberMarginWidthProperty = DependencyProperty.Register("LineNumberMarginWidth", typeof(double), typeof(SyntaxHighlightTextBox),
                                                new FrameworkPropertyMetadata(25.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double LineNumberMarginWidth
        {
            get { return (double)GetValue(LineNumberMarginWidthProperty); }
            set { SetValue(LineNumberMarginWidthProperty, value); }
        }

        //private Canvas _suggestionCanvas = null;
        //private ListBox _suggestionList = null;
        //private double _leftTextBorder = double.PositiveInfinity;
        //public double _lineNumberWidthOffset = 25.0;
        //private int _tabSize = 4;
        //private FormattedText _lastLineNumberFormat;
        private string _tab = new string(' ', 4);
        private double _leftTextBorder = double.PositiveInfinity;
        //private string _lineWidth = 25;

        public SyntaxHighlightTextBox()
        {
            Foreground = Brushes.Transparent;
            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            AcceptsTab = true;
            AcceptsReturn = true;

            Loaded += new RoutedEventHandler(CodeTextBox_Loaded);

            InitializeComponent();
        }

        //private string GetTab()
        //{
        //    return new string(' ', _tabSize);
        //}

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            Text = Text.Replace("\t", _tab);
            //if (SyntaxLexer != null)
            //    SyntaxLexer.Parse(this.Text, CaretIndex);
            InvalidateVisual();
        }


        //protected override void OnPreviewKeyDown(KeyEventArgs e)
        //{
        //    if (Key.Escape.Equals(e.Key))
        //    {
        //        HideSuggestionList();
        //        e.Handled = true;
        //    }

            //    if (Key.Space.Equals(e.Key) && ModifierKeys.Control.Equals(e.KeyboardDevice.Modifiers))
            //    {
            //        ShowSuggestionList();
            //        e.Handled = true;
            //    }

            //    if (Key.Tab.Equals(e.Key) && ModifierKeys.None.Equals(e.KeyboardDevice.Modifiers))
            //    {
            //        if (string.Empty.Equals(SelectedText))
            //        {
            //            int caretPosition = CaretIndex;
            //            Text = Text.Insert(caretPosition, GetTab());
            //            CaretIndex = caretPosition + _tabSize;
            //        }
            //        else
            //        {
            //            SelectedText = SelectedText.Replace(SelectedText, string.Empty);
            //        }
            //        e.Handled = true;
            //    }

            // enter respects indenting
            //if (Key.Return.Equals(e.Key))
            //{
            //    int index = CaretIndex;
            //    int lastLine = Text.LastIndexOf(Environment.NewLine, index);
            //    int spaces = 0;

            //    if (lastLine != -1)
            //    {
            //        string line = Text.Substring(lastLine, Text.Length - lastLine);
            //        int startLine = line.IndexOf(Environment.NewLine);

            //        if (startLine != -1)
            //        {
            //            line = line.Substring(startLine).TrimStart('\r', '\n');
            //        }

            //        foreach (char c in line)
            //        {
            //            if (c == ' ')
            //            {
            //                spaces++;
            //            }
            //            else
            //            {
            //                break;
            //            }
            //        }
            //    }
            //    Text = Text.Insert(index, Environment.NewLine + new string(' ', spaces));
            //    CaretIndex = index + Environment.NewLine.Length + spaces;
            //    e.Handled = true;
            //}
            //    base.OnPreviewKeyDown(e);
            //}

        protected override void OnRender(DrawingContext drawingContext)
        {
            // draw background 
            drawingContext.PushClip(new RectangleGeometry(new Rect(0, 0, ActualWidth, ActualHeight)));
            drawingContext.DrawRectangle(Brushes.White, new Pen(), new Rect(0, 0, ActualWidth, ActualHeight));

            // draw text
            if (!string.Empty.Equals(Text))
            {
                FormattedText ft = new FormattedText(Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                    new Typeface(FontFamily.Source), FontSize, new SolidColorBrush(Colors.Black));

                double leftMargin = 5 + BorderThickness.Left + LineNumberMarginWidth;
                double topMargin = 5 + BorderThickness.Top;

                // Background highlight
                //if (HighlightText != null && HighlightText.Count > 0)
                //{
                //    foreach (string text in HighlightText)
                //    {
                //        int txtEnd = this.Text.Length;
                //        int index = 0;
                //        int lastIndex = this.Text.LastIndexOf(text, StringComparison.OrdinalIgnoreCase);

                //        while (index <= lastIndex)
                //        {
                //            index = this.Text.IndexOf(text, index, StringComparison.OrdinalIgnoreCase);

                //            Geometry geom = ft.BuildHighlightGeometry(new Point(left_margin, top_margin - this.VerticalOffset), index, text.Length);
                //            if (geom != null)
                //            {
                //                drawingContext.DrawGeometry(HighlightBrush, null, geom);
                //            }
                //            index += 1;
                //        }
                //    }
                //}

                //if (SyntaxLexer != null)
                //{
                //    // set color of tokens by syntax rules
                //    foreach (var t in SyntaxLexer.Tokens)
                //    {
                //        SyntaxRuleItem rule = GetSyntaxRule(t.TokenType);
                //        if (rule != null)
                //        {
                //            ft.SetForegroundBrush(rule.Foreground, t.Start, t.Length);
                //        }
                //    }
                //}
                ft.Trimming = TextTrimming.None;

                // left from first char boundary

                double leftBorder = GetRectFromCharacterIndex(0).Left;
                if (!double.IsInfinity(leftBorder))
                {
                    _leftTextBorder = leftBorder;
                }
                drawingContext.DrawText(ft, new Point(_leftTextBorder - HorizontalOffset, topMargin - VerticalOffset));

                // draw lines
            //    if (GetLastVisibleLineIndex() != -1)
            //    {
            //        _lastLineNumberFormat = GetLineNumbers();
            //    }
            //    if (_lastLineNumberFormat != null)
            //    {
            //        drawingContext.DrawText(_lastLineNumberFormat, new Point(30, topMargin));
            //    }
            }
        }

        private void CodeTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (VisualTreeHelper.GetChildrenCount(this) > 0)
            {
                var bd = VisualTreeHelper.GetChild(this, 0);
                var gd = VisualTreeHelper.GetChild(bd, 0);
                var sv = VisualTreeHelper.GetChild(gd, 0) as ScrollViewer;
                sv.ScrollChanged += (s2, e2) => { InvalidateVisual(); };

                //_suggestionCanvas = VisualTreeHelper.GetChild(gd, 2) as Canvas;
                //_suggestionList = VisualTreeHelper.GetChild(_suggestionCanvas, 0) as ListBox;

                //HideSuggestionList();
                InvalidateVisual();
            }
        }

        //private void ShowSuggestionList()
        //{
        //    Point position = GetRectFromCharacterIndex(CaretIndex).BottomRight;
        //    _suggestionCanvas.IsHitTestVisible = true;
        //    double left = position.X - _lineNumberWidthOffset - LineNumberMarginWidth;
        //    double top = position.Y;

        //    if (left + _suggestionList.ActualWidth > _suggestionCanvas.ActualWidth)
        //    {
        //        left = _suggestionCanvas.ActualWidth - _suggestionList.ActualWidth;
        //    }

        //    if (top + _suggestionList.ActualHeight > _suggestionCanvas.ActualHeight)
        //    {
        //        top = _suggestionCanvas.ActualHeight - _suggestionList.ActualHeight;
        //    }

        //    Canvas.SetLeft(_suggestionList, left);
        //    Canvas.SetTop(_suggestionList, top);
        //    _suggestionList.Visibility = Visibility.Visible;
        //    _suggestionList.Focus();
        //}

        //private void HideSuggestionList()
        //{
        //    _suggestionCanvas.IsHitTestVisible = false;
        //    _suggestionList.Visibility = Visibility.Hidden;
        //}

        //private FormattedText GetLineNumbers()
        //{
        //    int firstLine = GetFirstVisibleLineIndex();
        //    int lastLine = GetLastVisibleLineIndex();
        //    StringBuilder sb = new StringBuilder();
        //    double maxSize = 15.0;

        //    for (int i = firstLine; i <= lastLine; i++)
        //    {
        //        string num = (i + 1) + Environment.NewLine;

        //        double size = MeasureString(num);
        //        if (size > maxSize)
        //        {
        //            maxSize = size;
        //        }
        //        sb.Append(num);
        //    }

        //    LineNumberMarginWidth = maxSize + _lineNumberWidthOffset;
        //    string lineNumberString = sb.ToString();
        //    FormattedText lineNumbers = new FormattedText(lineNumberString, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
        //        new Typeface(FontFamily.Source), FontSize, new SolidColorBrush(Colors.Teal));
        //    lineNumbers.TextAlignment = TextAlignment.Right;
        //    return lineNumbers;
        //}

        //private double MeasureString(string str)
        //{
        //    FormattedText formattedText = new FormattedText(str, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
        //        new Typeface(FontFamily.Source), FontSize, new SolidColorBrush(Colors.Black));
        //    if (string.Empty.Equals(str))
        //    {
        //        return formattedText.WidthIncludingTrailingWhitespace;
        //    }
        //    else if (str.Substring(0, 1) == "\t")
        //    {
        //        return formattedText.WidthIncludingTrailingWhitespace;
        //    }
        //    else
        //    {
        //        return formattedText.WidthIncludingTrailingWhitespace;
        //    }
        //}
    }
}
