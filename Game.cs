using System;
using System.Threading;
using ConsoleTetris.Core;
using ConsoleTetris.Input;
using ConsoleTetris.Utils;

namespace ConsoleTetris
{
    public class Game
    {
        private Board board;
        private Tetromino tetromino;
        private int score;
        private DateTime lastFallTime;
        private TimeSpan fallInterval;

        public Game()
        {
            board = new Board();
            tetromino = new Tetromino(RandomProvider.GetRandomTetrominoType());
            score = 0;
            fallInterval = TimeSpan.FromMilliseconds(500);
            lastFallTime = DateTime.Now;
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine("← → ↓ 이동 / 스페이스바 회전 / ESC 종료");

            while (true)
            {
                HandleInput();
                HandleAutoFall();

                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"점수: {score}");
                board.Draw(tetromino);

                Thread.Sleep(50);
            }
        }

        private void HandleInput()
        {
            var action = InputHandler.GetInput();

            switch (action)
            {
                case InputAction.Exit:
                    Environment.Exit(0);
                    break;
                case InputAction.MoveLeft:
                    if (!board.IsCollision(tetromino, offsetX: -1))
                        tetromino.X -= 1;
                    break;
                case InputAction.MoveRight:
                    if (!board.IsCollision(tetromino, offsetX: 1))
                        tetromino.X += 1;
                    break;
                case InputAction.MoveDown:
                    TryMoveOrFix();
                    break;
                case InputAction.Rotate:
                    int newRotation = (tetromino.Rotation + 1) % 4;
                    if (!board.IsCollision(tetromino, rotation: newRotation))
                        tetromino.Rotate();
                    break;
            }
        }

        private void HandleAutoFall()
        {
            if ((DateTime.Now - lastFallTime) >= fallInterval)
            {
                TryMoveOrFix();
                lastFallTime = DateTime.Now;
            }
        }

        private void TryMoveOrFix()
        {
            if (!board.IsCollision(tetromino, offsetY: 1))
            {
                tetromino.Y += 1;
            }
            else
            {
                board.FixTetromino(tetromino);
                int cleared = board.ClearLines();
                if (cleared > 0)
                    score += cleared * 100;

                tetromino = new Tetromino(RandomProvider.GetRandomTetrominoType());

                if (board.IsCollision(tetromino))
                {
                    Console.Clear();
                    Console.WriteLine($"게임 오버! 최종 점수: {score}");
                    Environment.Exit(0);
                }
            }
        }
    }
}
