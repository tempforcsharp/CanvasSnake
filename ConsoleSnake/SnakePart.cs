using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSnake
{
    class SnakePart
    {
        public Point Position { get; set; }

        public SnakePart()
        {
            Position = new Point();
        }
        public void Draw()
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.WriteLine('*');
        }
    }
}
