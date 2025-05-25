using ConsoleTetris.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris.Utils
{
    public static class RandomProvider
    {
        private static readonly Random random = new Random();

        public static int Next(int min, int max)
        {
            return random.Next(min, max);
        }

        public static TetrominoType GetRandomTetrominoType()
        {
            Array values = Enum.GetValues(typeof(TetrominoType));
            return (TetrominoType)values.GetValue(random.Next(values.Length))!;
        }
    }
}
