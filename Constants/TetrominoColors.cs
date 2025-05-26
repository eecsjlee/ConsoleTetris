using System;
using System.Collections.Generic;
using ConsoleTetris.Core;

namespace ConsoleTetris.Constants
{
    public static class TetrominoColors
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
