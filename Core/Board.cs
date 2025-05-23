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

        public void Draw(Tetromino? tetromino = null)
        {
            Console.Clear();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bool isTetrominoCell = false;

                    if (tetromino != null)
                    {
                        int[,] shape = tetromino.GetCurrentShape();
                        int shapeHeight = shape.GetLength(0);
                        int shapeWidth = shape.GetLength(1);

                        for (int sy = 0; sy < shapeHeight; sy++)
                        {
                            for (int sx = 0; sx < shapeWidth; sx++)
                            {
                                int boardX = tetromino.X + sx;
                                int boardY = tetromino.Y + sy;

                                if (boardX == x && boardY == y && shape[sy, sx] == 1)
                                {
                                    isTetrominoCell = true;
                                }
                            }
                        }
                    }

                    if (isTetrominoCell)
                        Console.Write(" # ");
                    else
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

        public bool IsCollision(Tetromino tetromino, int offsetX = 0, int offsetY = 0, int? rotation = null)
        {
            int[,] shape = rotation.HasValue
                ? tetromino.GetShape(rotation.Value)
                : tetromino.GetCurrentShape();

            int shapeHeight = shape.GetLength(0);
            int shapeWidth = shape.GetLength(1);

            for (int y = 0; y < shapeHeight; y++)
            {
                for (int x = 0; x < shapeWidth; x++)
                {
                    if (shape[y, x] == 0)
                        continue;

                    int boardX = tetromino.X + x + offsetX;
                    int boardY = tetromino.Y + y + offsetY;

                    if (boardX < 0 || boardX >= width || boardY < 0 || boardY >= height)
                        return true; // 벽 또는 바닥과 충돌

                    if (grid[boardY, boardX] != 0)
                        return true; // 기존 블록과 충돌
                }
            }

            return false;
        }
    }
}
