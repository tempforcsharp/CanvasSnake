using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace CanvasSnake
{
    class PayloadManager
    {
        private Payload mPayload;
        private Canvas mCanvas;

        public PayloadManager(Canvas canvas)
        {
            mCanvas = canvas;
        }

        public void update()
        {
            if (mPayload == null)
            {
                mPayload = new Payload();

                Random rand = new Random();

                bool canCreate = false;

                do
                {
                    int coordX = rand.Next(0, Constants.CellCountX - 1);
                    int coordY = rand.Next(0, Constants.CellCountY - 1);

                    Canvas.SetLeft(mPayload.mPayloadRectangle, coordX * Constants.CellWidth);
                    Canvas.SetTop(mPayload.mPayloadRectangle, coordY * Constants.CellHeight);

                    canCreate = CheckPayloadCreationPos();

                } while (canCreate == false);

                mCanvas.Children.Add(mPayload.mPayloadRectangle);               
            }
        }
        public Rect GetPayloadRect()
        {
            if (mPayload == null)
            {
                return new Rect();
            }

            Rectangle payloadRectangle = mPayload.mPayloadRectangle;

            return new Rect(Canvas.GetLeft(payloadRectangle), Canvas.GetTop(payloadRectangle),
                                                payloadRectangle.Width, payloadRectangle.Height);
        }
        public void DestroyPayload()
        {
            mCanvas.Children.Remove(mPayload.mPayloadRectangle);
            mPayload = null;
        }

        private bool CheckPayloadCreationPos()
        {
            Rect payloadRect = this.GetPayloadRect();

            foreach (var item in mCanvas.Children)
            {
                Rectangle rectangle = item as Rectangle;

                if (rectangle == null)
                {
                    continue;
                }

                Rect itemRect = new Rect(Canvas.GetLeft(rectangle), Canvas.GetTop(rectangle),
                                        rectangle.Width, rectangle.Height);

                if (payloadRect.IntersectsWith(itemRect))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
