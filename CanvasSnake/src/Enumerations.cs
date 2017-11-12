using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasSnake
{
    static class Enumerations
    {
        public enum States
        {
            State_MainMenu,
            State_Game,
            State_GameOver,

            State_Count
        }

        public enum Layouts
        {
            Layout_Field,
            Layout_GameLow,
            Layout_GameHigh,
            Layout_Hud,

            Layout_Count
        }

        public enum Direction
        {
            Direction_Up,
            Direction_Down,
            Direction_Right,
            Direction_Left,

            Direction_Count
        }
    }
}
