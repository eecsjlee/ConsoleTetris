using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris.Core
{
    public class Board
    {
        private readonly int width;
        private readonly int height;
        private readonly int[,] grid;

        public Board(int width = 10, int height = 20)
        {
            this.width = width;
            this.height = height;
            grid = new int[height, width];
        }

        public void Draw()
        {
            Console.Clear();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write(grid[y, x] == 0 ? " . " : " # ");
                }
                Console.WriteLine();
            }
        }

        public void SetCell(int x, int y, int value)
        {
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                grid[y, x] = value;
            }
        }

        public int GetCell(int x, int y)
        {
            return (x >= 0 && x < width && y >= 0 && y < height) ? grid[y, x] : 1;
        }
    }
}
