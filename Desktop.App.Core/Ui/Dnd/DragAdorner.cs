using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Desktop.App.Core.Ui.Dnd
{
    public class DragAdorner : Adorner
    {
        private Brush _brush;
        private Point _location;
        private Point _offset;
        private Size _renderSize;

        public DragAdorner(UIElement adornedElement, Size renderSize, Point offset)
            : base(adornedElement)
        {
            _offset = new Point(offset.X, offset.Y);
            _brush = new VisualBrush(AdornedElement);
            _brush.Opacity = 0.5;
            _renderSize = renderSize;

            Focusable = false;
            IsHitTestVisible = false;
            IsEnabled = false;
            AdornerLayer.GetAdornerLayer(AdornedElement).Add(this);
        }

        public void UpdatePosition(Point location)
        {
            _location = location;
            InvalidateVisual();
        }

        public void Detach()
        {
            if(AdornerLayer.GetAdornerLayer(AdornedElement) == null)
            {
                return;
            }
            AdornerLayer.GetAdornerLayer(AdornedElement).Remove(this);
        }

        protected override void OnRender(DrawingContext dc)
        {
            Point currentLocation = _location;
            currentLocation.Offset(-_offset.X+20, -_offset.Y);
            dc.DrawRectangle(_brush, null, new Rect(currentLocation, _renderSize));
        } 
    }
}
