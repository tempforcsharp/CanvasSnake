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
    class Snake : Entity
    {
        private SnakePart[] mSnake;
        private int mMaxSnakeSize;
        private int mLenght;
        private Enumerations.Direction mDirection;

        private Canvas mCanvas;

        public Snake(int maxSize)
        {
            mSnake = new SnakePart[maxSize];
        }

        public void InitializeSnake(Canvas canvas)
        {
            mCanvas = canvas;

            Point initialPosition = new Point();
            initialPosition.X = Constants.CellWidth * 2;
            initialPosition.Y = 0;

            this.mLenght = 3;

            mSnake[0] = new SnakePart();
            Canvas.SetLeft(mSnake[0].mSnakePart, initialPosition.X);
            Canvas.SetTop(mSnake[0].mSnakePart, initialPosition.Y);

            for (int i = 1; i < this.mLenght; ++i)
            {
                mSnake[i] = new SnakePart();

                Canvas.SetLeft(mSnake[i].mSnakePart, initialPosition.X - Constants.CellWidth * i);
                Canvas.SetTop(mSnake[i].mSnakePart, initialPosition.Y);
            }

            for (int i = 0; i < this.mLenght; ++i)
            {
                canvas.Children.Add(mSnake[i].mSnakePart);
            }

            mDirection = Enumerations.Direction.Direction_Right;
        }

        public override void update()
        {
            for (int i = mSnake.Length - 1; i >= 0; --i)
            {
                if (mSnake[i] == null)
                {
                    continue;
                }

                Canvas.SetLeft(mSnake[i].mSnakePart, Canvas.GetLeft(mSnake[i - 1].mSnakePart));
                Canvas.SetTop(mSnake[i].mSnakePart, Canvas.GetTop(mSnake[i - 1].mSnakePart));

                if (i == 1)
                {
                    break;
                }
            }

            this.ApplyDirrection();
        }
        private void ApplyDirrection()
        {
            switch (mDirection)
            {
                case Enumerations.Direction.Direction_Up:
                    {
                        int curY = (int)Canvas.GetTop(mSnake[0].mSnakePart);
                        Canvas.SetTop(mSnake[0].mSnakePart, curY - Constants.CellHeight);
                        break;
                    }
                case Enumerations.Direction.Direction_Down:
                    {
                        int curY = (int)Canvas.GetTop(mSnake[0].mSnakePart);
                        Canvas.SetTop(mSnake[0].mSnakePart, curY + Constants.CellHeight);
                        break;
                    }
                case Enumerations.Direction.Direction_Right:
                    {
                        int curX = (int)Canvas.GetLeft(mSnake[0].mSnakePart);
                        Canvas.SetLeft(mSnake[0].mSnakePart, curX + Constants.CellWidth);
                        break;
                    }
                case Enumerations.Direction.Direction_Left:
                    {
                        int curX = (int)Canvas.GetLeft(mSnake[0].mSnakePart);
                        Canvas.SetLeft(mSnake[0].mSnakePart, curX - Constants.CellWidth);
                        break;
                    }
            }           
        }

        public bool CheckRules()
        {
            SnakePart headSection = mSnake[0];
            Rectangle headSectionRectangle = headSection.mSnakePart;

            int curHeadPosX = (int)Canvas.GetLeft(headSectionRectangle);
            int curHeadPosY = (int)Canvas.GetTop(headSectionRectangle);

            if (curHeadPosX + Constants.CellWidth > mCanvas.Width || curHeadPosX < 0)
            {
                return false;
            }
            else if (curHeadPosY + Constants.CellHeight > mCanvas.Height || curHeadPosY < 0)
            {
                return false;
            }

            Rect headRect = new Rect(Canvas.GetLeft(headSectionRectangle), Canvas.GetTop(headSectionRectangle),
                                                    headSectionRectangle.Width, headSectionRectangle.Height);

            for (int i = 2; i < this.mLenght; ++i)
            {
                SnakePart tailSection = mSnake[i];
                
                if (tailSection == null)
                {
                    break;
                }

                Rectangle tailSectionRectangle = tailSection.mSnakePart;

                Rect tailRect = new Rect(Canvas.GetLeft(tailSectionRectangle), Canvas.GetTop(tailSectionRectangle),
                                        tailSectionRectangle.Width, tailSectionRectangle.Height);

                tailRect.Intersect(headRect);

                if (tailRect.Width > 0 && tailRect.Height > 0)
                {
                    return false;
                }
            }

            return true;
        }
        public void SetDirection(Enumerations.Direction direction)
        {
            mDirection = direction;
        }

        public void AddOnePart()
        {
            if (this.mLenght < mSnake.Length)
            {
                SnakePart newPart = new SnakePart();
                mSnake[this.mLenght] = newPart;

                Canvas.SetLeft(newPart.mSnakePart, 0 - Constants.CellWidth * 10);
                Canvas.SetTop(newPart.mSnakePart, 0);

                mCanvas.Children.Add(newPart.mSnakePart);

                this.mLenght++;
            }
        }

        public Rect GetSnakeHeadRect()
        {
            SnakePart headSection = mSnake[0];
            Rectangle headSectionRectangle = headSection.mSnakePart;

            return new Rect(Canvas.GetLeft(headSectionRectangle), Canvas.GetTop(headSectionRectangle),
                                                    headSectionRectangle.Width, headSectionRectangle.Height);
        }

        public int GetLength()
        {
            return mLenght;
        }
    }
}
