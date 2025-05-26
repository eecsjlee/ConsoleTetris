using ConsoleTetris.Constants;
using System;

namespace ConsoleTetris.Core
{
    public class Board
    {
        private readonly int width;
        private readonly int height;
        private readonly TetrominoType?[,] grid;

        public Board(int width = 10, int height = 20)
        {
            this.width = width;
            this.height = height;
            grid = new TetrominoType?[height, width]; // null = 빈 셀
        }

        public void Draw(Tetromino? tetromino = null)
        {
            Console.SetCursorPosition(0, 3); // 점수/레벨 아래로 출력

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bool isTetrominoCell = false;
                    ConsoleColor cellColor = ConsoleColor.White;

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
                                    cellColor = GameConstants.ColorMap[tetromino.Type];
                                }
                            }
                        }
                    }

                    if (isTetrominoCell)
                    {
                        Console.ForegroundColor = cellColor;
                        Console.Write("■");
                        Console.ResetColor();
                    }
                    else if (grid[y, x] != null)
                    {
                        Console.ForegroundColor = GameConstants.ColorMap[grid[y, x].Value];
                        Console.Write("■");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }

                Console.WriteLine();
            }
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
                        return true;

                    if (grid[boardY, boardX] != null)
                        return true;
                }
            }

            return false;
        }

        public void FixTetromino(Tetromino tetromino)
        {
            int[,] shape = tetromino.GetCurrentShape();
            int shapeHeight = shape.GetLength(0);
            int shapeWidth = shape.GetLength(1);

            for (int y = 0; y < shapeHeight; y++)
            {
                for (int x = 0; x < shapeWidth; x++)
                {
                    if (shape[y, x] == 1)
                    {
                        int boardX = tetromino.X + x;
                        int boardY = tetromino.Y + y;

                        if (boardY >= 0 && boardY < height && boardX >= 0 && boardX < width)
                        {
                            grid[boardY, boardX] = tetromino.Type;
                        }
                    }
                }
            }
        }

        public int ClearLines()
        {
            int linesCleared = 0;

            for (int y = height - 1; y >= 0; y--)
            {
                bool isFullLine = true;

                for (int x = 0; x < width; x++)
                {
                    if (grid[y, x] == null)
                    {
                        isFullLine = false;
                        break;
                    }
                }

                if (isFullLine)
                {
                    for (int row = y; row > 0; row--)
                    {
                        for (int col = 0; col < width; col++)
                        {
                            grid[row, col] = grid[row - 1, col];
                        }
                    }

                    for (int col = 0; col < width; col++)
                    {
                        grid[0, col] = null;
                    }

                    linesCleared++;
                    y++; // 내려온 줄 다시 검사
                }
            }

            return linesCleared;
        }
    }
}
