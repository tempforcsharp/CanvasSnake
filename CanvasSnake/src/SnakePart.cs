using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CanvasSnake
{
    class SnakePart
    {
        public Rectangle mSnakePart { get; }

        public SnakePart() : this(Constants.CellWidth, Constants.CellHeight)
        {
            
        }
        public SnakePart(int width, int height)
        {
            mSnakePart = new Rectangle();
            mSnakePart.Width = width;
            mSnakePart.Height = height;

            mSnakePart.Stroke = new SolidColorBrush(Colors.Black);
            mSnakePart.Fill = new SolidColorBrush(Colors.Firebrick);
        }
    }
}
