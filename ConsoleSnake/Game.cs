using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSnake
{
    class Game
    {
        private PayloadManager mPayloadManager;
        private Snake mSnake;
        private GameField mGameField;

        private int mDt;

        private bool mGameOverLoose;
        private bool mGameOverWin;

        public Game()
        {
            mPayloadManager = new PayloadManager();
            mGameField = new GameField();

            mDt = 200;

            Reset();
        }

        public void RunGameLoop()
        {
            while (true)
            {
                if (mGameOverLoose == false && mGameOverWin == false)
                {
                    this.Update(0);
                    this.HandleInput();
                }

                this.Draw();

                System.Threading.Thread.Sleep(mDt);
            }
        }

        private void Update(double dt)
        {           
            if (CheckIfPayloadWasEated())
            {
                mPayloadManager.DestroyPayload();
                mSnake.AddOnePart();
            }

            mSnake.Update(dt);
            mPayloadManager.Update();

            if (CheckRules() == false)
            {
                mGameOverLoose = true;
            }

            if (CheckWinConditions())
            {
                mGameOverWin = true;
            }

        }
        private void Draw()
        {
            Console.Clear();

            Console.SetCursorPosition(1, 1);
            Console.WriteLine(mSnake.Lenght);

            mGameField.Draw();
            mPayloadManager.Draw();

            if (mGameOverLoose || mGameOverWin)
            {
                Console.SetCursorPosition((mGameField.Width / 2) - 4, mGameField.Height / 2);

                if (mGameOverLoose)
                {
                    Console.WriteLine("GAME OVER");
                }
                else
                {
                    Console.WriteLine("YOU WIN");
                }
                
                Console.ReadKey();
                Reset();
                return;
            }

            mSnake.Draw();           
        }

        private void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                char key = Console.ReadKey().KeyChar;

                mSnake.Direction = key;
            }            
        }
        private bool CheckRules()
        {
            if (IsSnakeInField() == false)
            {
                return false;
            }
            if (mSnake.IsSakeValid() == false)
            {
                return false;
            }
            return true;
        }
        private bool CheckIfPayloadWasEated()
        {
            Point snakeHead = mSnake.GetHead();
            Point payloadPos = mPayloadManager.GetPayloadPosition();
            if (snakeHead == payloadPos)
            {
                return true;
            }

            return false;
        }
        private bool CheckWinConditions()
        {            
            if (mSnake.Lenght >= 10)
            {               
                return true;
            }

            return false;
        }
        private bool IsSnakeInField()
        {
            Point snakeHead = mSnake.GetHead();
            bool isInField = mGameField.IsPointInRect(snakeHead);

            if (isInField)
            {
                return true;
            }

            return false;
        }
        private void Reset()
        {
            Point leftTopPoint = new Point(3, 3);
            Point rightButtomPoint = new Point(33, 23);

            mSnake = new Snake(leftTopPoint);

            mGameField.SetRect(leftTopPoint, rightButtomPoint);

            mPayloadManager.LeftTopLimit = leftTopPoint;
            mPayloadManager.RightButtomLimit = rightButtomPoint;
            mPayloadManager.GeneratePayload();

            mGameOverLoose = false;
            mGameOverWin = false;
        }
    }
}
