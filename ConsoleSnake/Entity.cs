using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSnake
{
    class Entity
    {
        public Point Position { get; set; }
        public char Image { get; set; }

        public virtual void Update(double dt)
        {

        }
        public virtual void Draw()
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.WriteLine(Image);
        }
    }
}
