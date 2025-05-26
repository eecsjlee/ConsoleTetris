using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris.Input
{
    public enum InputAction
    {
        None,
        MoveLeft,
        MoveRight,
        MoveDown,
        Rotate,
        Exit,
        Pause,
        Restart
    }
}
