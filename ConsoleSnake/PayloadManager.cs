using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSnake
{
    class PayloadManager
    {
        private Payload mPayload;

        public Point LeftTopLimit { get; set; }
        public Point RightButtomLimit { get; set; }

        public PayloadManager()
        {
        }

        public void GeneratePayload()
        {
            if (mPayload == null)
            {
                mPayload = new Payload();
                mPayload.Position = new Point();
                mPayload.Image = '&';
            }           

            Random rand = new Random();

            mPayload.Position.X = rand.Next(this.LeftTopLimit.X + 1, this.RightButtomLimit.X - 1);
            mPayload.Position.Y = rand.Next(this.LeftTopLimit.Y + 1, this.RightButtomLimit.Y - 1);
        }
        public Point GetPayloadPosition()
        {
            return mPayload.Position;
        }

        public void DestroyPayload()
        {
            mPayload = null;
        }

        public void Draw()
        {           
            if (mPayload != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                mPayload.Draw();
                Console.ForegroundColor = ConsoleColor.White;
            }           
        }
        public void Update()
        {
            if (mPayload == null)
            {
                GeneratePayload();
            }
        }
    }
}
