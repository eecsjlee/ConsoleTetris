using ConsoleTetris.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris.Constants
{
    public static class GameConstants
    {
        public static readonly Dictionary<TetrominoType, ConsoleColor> ColorMap = new()
        {
            { TetrominoType.I, ConsoleColor.Cyan },
            { TetrominoType.O, ConsoleColor.Yellow },
            { TetrominoType.T, ConsoleColor.Magenta },
            { TetrominoType.S, ConsoleColor.Green },
            { TetrominoType.Z, ConsoleColor.Red },
            { TetrominoType.J, ConsoleColor.Blue },
            { TetrominoType.L, ConsoleColor.DarkYellow }
        };
    }
}
