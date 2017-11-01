using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSnake
{
    class Snake : Entity
    {
        private SnakePart[] mSnake;

        public int Lenght { get; private set; }
        public char Direction { get; set; }

        public Snake(Point topLeftPoint)
        {
            mSnake = new SnakePart[10];

            this.Direction = 'D';

            Point initialPosition = new Point();
            initialPosition.X = topLeftPoint.X + 3;
            initialPosition.Y = topLeftPoint.Y + 1;

            this.Lenght = 3;

            mSnake[0] = new SnakePart();
            mSnake[0].Position = initialPosition;

            for (int i = 1; i < this.Lenght; ++i)
            {
                mSnake[i] = new SnakePart();

                mSnake[i].Position.X = initialPosition.X - i;
                mSnake[i].Position.Y = initialPosition.Y;
            }
        }

        public Point GetHead()
        {
            return mSnake[0].Position;
        }
        public bool IsSakeValid()
        {
            Point headPosition = GetHead();

            for (int i = 1; i < mSnake.Length; ++i)
            {
                if (mSnake[i] == null)
                {
                    break;
                }

                if (mSnake[i].Position == headPosition)
                {
                    return false;
                }
            }

            return true;
        }
        public bool IsSnakeOnPoint(Point point)
        {
            foreach (SnakePart part in mSnake)
            {
                if (part.Position == point)
                {
                    return true;
                }
            }

            return false;
        }
        public Point[] GetSnakePositions()
        {
            Point[] snakePoints = new Point[this.Lenght];

            for (int i = 0; i < this.Lenght; ++i)
            {
                snakePoints[i] = mSnake[i].Position;
            }

            return snakePoints;
        }
        public void AddOnePart()
        {
            if (this.Lenght < mSnake.Length)
            {
                mSnake[this.Lenght] = new SnakePart();
                mSnake[this.Lenght].Position.X = 0;
                mSnake[this.Lenght].Position.X = 0;
                this.Lenght++;
            }
        }

        public override void Update(double dt)
        {
            for (int i = mSnake.Length - 1; i >= 0; --i)
            {
                if (mSnake[i] == null)
                {
                    continue;
                }

                mSnake[i].Position.X = mSnake[i - 1].Position.X;
                mSnake[i].Position.Y = mSnake[i - 1].Position.Y;

                if (i == 1)
                {
                    break;
                }
            }

            this.ApplyDirrection();
        }
        public override void Draw()
        {
            for (int i = 0; i < mSnake.Length; ++i)
            {
                if (mSnake[i] == null)
                {
                    continue;
                }

                mSnake[i].Draw();
            }
        }

        private void ApplyDirrection()
        {
            switch (Direction)
            {
                case 's':
                case 'S': mSnake[0].Position.Y++; break;
                case 'w':
                case 'W': mSnake[0].Position.Y--; break;
                case 'd':
                case 'D': mSnake[0].Position.X++; break;
                case 'a':
                case 'A': mSnake[0].Position.X--; break;
                default: mSnake[0].Position.Y++; break;
            }
        }
    }
}
