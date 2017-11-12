using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasSnake
{
    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point()
        {

        }
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override bool Equals(object obj)
        {
            if (!ReferenceEquals(obj, null) && (obj is Point))
            {
                Point point = obj as Point;

                if (this.X == point.X && this.Y == point.Y)
                {
                    return true;
                }
            }

            return false;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;

                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();

                return hash;
            }
        }

        public static bool operator ==(Point point1, Point point2)
        {
            if (!ReferenceEquals(point1, null) && !ReferenceEquals(point2, null))
            {
                return point1.Equals(point2);
            }

            return false;
        }
        public static bool operator !=(Point point1, Point point2)
        {
            return !(point1 == point2);
        }       
    }
}
