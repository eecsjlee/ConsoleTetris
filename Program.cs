using System;
using ConsoleTetris.Core;
using ConsoleTetris.Input;

namespace ConsoleTetris
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            Tetromino tetromino = new Tetromino(TetrominoType.T);

            Console.WriteLine("← → ↓ 이동 / 스페이스바 회전 / ESC 종료");

            while (true)
            {
                var action = InputHandler.GetInput();

                switch (action)
                {
                    case InputAction.Exit:
                        return;

                    case InputAction.MoveLeft:
                        if (!board.IsCollision(tetromino, offsetX: -1))
                            tetromino.X -= 1;
                        break;

                    case InputAction.MoveRight:
                        if (!board.IsCollision(tetromino, offsetX: 1))
                            tetromino.X += 1;
                        break;

                    case InputAction.MoveDown:
                        if (!board.IsCollision(tetromino, offsetY: 1))
                            tetromino.Y += 1;
                        break;

                    case InputAction.Rotate:
                        int newRotation = (tetromino.Rotation + 1) % 4;
                        if (!board.IsCollision(tetromino, rotation: newRotation))
                            tetromino.Rotate();
                        break;

                    case InputAction.None:
                        break;
                }

                board.Draw(tetromino);
                System.Threading.Thread.Sleep(50); // CPU 과점유 방지
            }
        }
    }
}
