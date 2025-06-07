using System;
using System.Threading;
using ConsoleTetris.Core;
using ConsoleTetris.Input;
using ConsoleTetris.Utils;
using ConsoleTetris.Constants;

namespace ConsoleTetris
{
    public class Game
    {
        private Board board;
        private Tetromino tetromino;
        private int score;
        private int level;

        private bool isPaused = false;
        private bool isGameOver = false;
        private bool isSoftDropping = false;

        private DateTime lastFallTime;
        private TimeSpan fallInterval;

        private const int ScorePerLevel = 500;

        public Game()
        {
            Restart();
        }

        public void Run()
        {
            while (true)
            {
                HandleInput();

                if (!isPaused && (DateTime.Now - lastFallTime) >= fallInterval)
                {
                    TryMoveOrFix();
                    lastFallTime = DateTime.Now;
                }

                // 고정 위치 상태 출력
                Console.SetCursorPosition(0, 0);
                Console.Write($"점수: {score}     ");

                Console.SetCursorPosition(0, 1);
                Console.Write($"레벨: {level}     ");

                Console.SetCursorPosition(0, 2);
                Console.Write(isPaused ? "일시정지 중..." : "                   ");

                Console.SetCursorPosition(0, 4);
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
                    if (!isPaused && !board.IsCollision(tetromino, offsetX: -1))
                        tetromino.X -= 1;
                    break;
                case InputAction.MoveRight:
                    if (!isPaused && !board.IsCollision(tetromino, offsetX: 1))
                        tetromino.X += 1;
                    break;
                case InputAction.MoveDown:
                    if (!isPaused)
                        TryMoveOrFix();
                    break;
                case InputAction.Rotate:
                    if (!isPaused)
                    {
                        int newRotation = (tetromino.Rotation + 1) % 4;
                        if (!board.IsCollision(tetromino, rotation: newRotation))
                            tetromino.Rotate();
                    }
                    break;
                case InputAction.Pause:
                    isPaused = !isPaused;
                    break;
                case InputAction.Restart:
                    Restart();
                    break;
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
                {
                    score += cleared * 100;

                    int newLevel = (score / ScorePerLevel) + 1;
                    if (newLevel > level)
                    {
                        level = newLevel;
                        int newIntervalMs = Math.Max(100, 500 - (level - 1) * 50);
                        fallInterval = TimeSpan.FromMilliseconds(newIntervalMs);

                        Console.SetCursorPosition(0, 3);
                        Console.Write($"레벨 {level} 달성! 속도 ↑     ");
                    }
                }

                tetromino = new Tetromino(RandomProvider.GetRandomTetrominoType());

                if (board.IsCollision(tetromino))
                {
                    Console.Clear();
                    Console.WriteLine($"게임 오버! 최종 점수: {score}, 레벨: {level}");
                    Environment.Exit(0);
                }
            }
        }

        private void Restart()
        {
            board = new Board();
            tetromino = new Tetromino(RandomProvider.GetRandomTetrominoType());
            score = 0;
            level = 1;
            fallInterval = TimeSpan.FromMilliseconds(500);
            lastFallTime = DateTime.Now;
            isPaused = false;
            isGameOver = false;

            Console.Clear();
            Console.WriteLine("← → ↓ 이동 / 스페이스바 회전 / P: 일시정지 / R: 재시작 / ESC 종료");
        }
    }
}