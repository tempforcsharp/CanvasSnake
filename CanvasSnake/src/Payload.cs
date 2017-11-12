using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CanvasSnake
{
    class Payload : Entity
    {
        public Rectangle mPayloadRectangle { get; }

        public Payload()
        {
            mPayloadRectangle = new Rectangle();
            mPayloadRectangle.Width = Constants.CellWidth;
            mPayloadRectangle.Height = Constants.CellHeight;

            mPayloadRectangle.Stroke = new SolidColorBrush(Colors.Black);
            mPayloadRectangle.Fill = new SolidColorBrush(Colors.Gainsboro);
        }

        public override void update()
        {
            
        }
    }
}
