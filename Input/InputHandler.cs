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

    public static class InputHandler
    {
        public static InputAction GetInput()
        {
            if (!Console.KeyAvailable)
                return InputAction.None;

            var key = Console.ReadKey(true).Key;

            return key switch
            {
                ConsoleKey.LeftArrow => InputAction.MoveLeft,
                ConsoleKey.RightArrow => InputAction.MoveRight,
                ConsoleKey.DownArrow => InputAction.MoveDown,
                ConsoleKey.Spacebar => InputAction.Rotate,
                ConsoleKey.P => InputAction.Pause,
                ConsoleKey.R => InputAction.Restart,
                ConsoleKey.Escape => InputAction.Exit,
                _ => InputAction.None
            };
        }
    }
}
