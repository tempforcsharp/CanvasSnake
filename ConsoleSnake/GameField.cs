using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSnake
{
    class GameField
    {
        private Point mLeftTop;
        private Point mRightButtom;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public void SetRect(Point leftTop, Point rightButtom)
        {
            mLeftTop = new Point(leftTop.X, leftTop.Y);
            mRightButtom = new Point(rightButtom.X, rightButtom.Y);

            this.Width = mRightButtom.X - mLeftTop.X;
            this.Height = mRightButtom.Y - mLeftTop.Y;
        }

        public bool IsPointInRect(Point point)
        {
            if (point.X <= mLeftTop.X || point.X >= mRightButtom.X - 1)
            {
                return false;
            }
            if (point.Y <= mLeftTop.Y || point.Y >= mRightButtom.Y - 1)
            {
                return false;
            }

            return true;
        }
        public void Draw()
        {            
            Console.ForegroundColor = ConsoleColor.Green;

            for (int i = 0; i < this.Height; ++i)
            {
                Console.SetCursorPosition(mLeftTop.X, mLeftTop.Y + i);

                for (int j = 0; j < this.Width; ++j)
                {
                    if (i == 0 || i == this.Height - 1)
                    {
                        Console.Write('#');
                        continue;
                    }

                    if (j == 0 || j == this.Width - 1)
                    {
                        Console.Write('#');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }

                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
