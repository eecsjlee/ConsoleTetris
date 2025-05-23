using System;
using System.Threading;
using ConsoleTetris.Core;
using ConsoleTetris.Input;

namespace ConsoleTetris
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            Tetromino tetromino = new Tetromino(TetrominoType.T); // 임시로 T 블록만 사용

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
                        {
                            tetromino.Y += 1;
                        }
                        else
                        {
                            // 블록 고정
                            board.FixTetromino(tetromino);

                            // ✅ 줄 제거
                            int cleared = board.ClearLines();
                            if (cleared > 0)
                            {
                                Console.WriteLine($"{cleared}줄 제거!");
                            }

                            // 새 블록 생성
                            tetromino = new Tetromino(TetrominoType.T); // 나중에 랜덤으로

                            // 생성 직후 충돌? → 게임 오버
                            if (board.IsCollision(tetromino))
                            {
                                Console.Clear();
                                Console.WriteLine("게임 오버!");
                                return;
                            }
                        }
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
                Thread.Sleep(50); // CPU 과점유 방지용
            }
        }
    }
}
