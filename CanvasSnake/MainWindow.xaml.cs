using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CanvasSnake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer mUpdateTimer = new DispatcherTimer();

        private Snake mSnake;
        private PayloadManager mPayloadManager;

        private bool mNeedRestart;
        TextBlock mTextBlock;

        public MainWindow()
        {
            InitializeComponent();

            mNeedRestart = false;

            canvas.Width = Constants.CellWidth * Constants.CellCountX;
            canvas.Height = Constants.CellHeight * Constants.CellCountY;

            canvasBorder.Width = Constants.CellWidth * 20;
            canvasBorder.Height = Constants.CellHeight * 20;

            this.Width = canvas.Width + canvas.Margin.Left * 4;
            this.Height = canvas.Height + canvas.Margin.Top * 8;

            InitializeGameEntities();
            InitializeGameTimer();

            int textCoordX = (int)canvas.Width / 2;
            int textCoordY = (int)canvas.Height / 2;

            DrawText("PRESS SPACE TO START", Colors.Red, textCoordX, textCoordY);
        }

        private void InitializeGameEntities()
        {
            mSnake = new Snake(Constants.MaxSnakeLength);
            mSnake.InitializeSnake(canvas);
            mPayloadManager = new PayloadManager(canvas);
        }
        private void InitializeGameTimer()
        {
            mUpdateTimer.Tick += Update;
            mUpdateTimer.Interval = TimeSpan.FromMilliseconds(100);
            //mUpdateTimer.Start();
        }
        private void ResetGameEntities()
        {
            canvas.Children.Clear();
            InitializeGameEntities();
        }
        private void Update(object sender, object e)
        {
            mSnake.update();

            if (PayloadWasEated())
            {
                mSnake.AddOnePart();
                mPayloadManager.DestroyPayload();
            }

            if (CheckWinConditions() == true)
            {
                int textCoordX = (int)canvas.Width / 2;
                int textCoordY = (int)canvas.Height / 2;

                DrawText("YOU WIN", Colors.Green, textCoordX, textCoordY);

                mUpdateTimer.Stop();
                return;
            }
            
            if (CheckRules() == false)
            {
                int textCoordX = (int)canvas.Width / 2;
                int textCoordY = (int)canvas.Height / 2;

                DrawText("GAME OVER", Colors.Red, textCoordX, textCoordY);

                mNeedRestart = true;

                mUpdateTimer.Stop();
                return;
            }

            mPayloadManager.update();
        }

        private bool PayloadWasEated()
        {
            Rect payload = mPayloadManager.GetPayloadRect();

            if (payload.Width < Constants.CellWidth)
            {
                return false;
            }

            Rect snake = mSnake.GetSnakeHeadRect();

            snake.Intersect(payload);

            if (snake.Width > 0 && snake.Height > 0)
            {
                return true;
            }

            return false;
        }
        private bool CheckRules()
        {
            bool isSnakeLoose = mSnake.CheckRules();
            return isSnakeLoose;
        }
        private bool CheckWinConditions()
        {
            int snakeLenght = mSnake.GetLength();

            if (snakeLenght >= Constants.MaxSnakeLength)
            {
                mNeedRestart = true;
                return true;
            }

            return false;
        }
        private void DrawText(string message, Color color, int x, int y)
        {
            mTextBlock = new TextBlock();

            mTextBlock.Text = message;
            mTextBlock.Foreground = new SolidColorBrush(color);

            Canvas.SetLeft(mTextBlock, x);
            Canvas.SetTop(mTextBlock, y);

            canvas.Children.Add(mTextBlock);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space:
                    {
                        if (mNeedRestart == true)
                        {
                            ResetGameEntities();
                            mNeedRestart = false;
                        }

                        canvas.Children.Remove(mTextBlock);
                        mUpdateTimer.Start();

                        break;
                    }
                case Key.S:
                    {
                        mSnake.SetDirection(Enumerations.Direction.Direction_Down);
                        break;
                    }
                case Key.W:
                    {
                        mSnake.SetDirection(Enumerations.Direction.Direction_Up);
                        break;
                    }
                case Key.D:
                    {
                        mSnake.SetDirection(Enumerations.Direction.Direction_Right);
                        break;
                    }
                case Key.A:
                    {
                        mSnake.SetDirection(Enumerations.Direction.Direction_Left);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}
