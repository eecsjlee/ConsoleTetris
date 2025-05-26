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
        private int level;

        private DateTime lastFallTime;
        private TimeSpan fallInterval;

        private const int ScorePerLevel = 500;

        public Game()
        {
            board = new Board();
            tetromino = new Tetromino(RandomProvider.GetRandomTetrominoType());
            score = 0;
            level = 1;
            fallInterval = TimeSpan.FromMilliseconds(500); // 시작 낙하 속도
            lastFallTime = DateTime.Now;
        }

        /// <summary>
        /// 게임 루프를 실행합니다.
        /// </summary>
        public void Run()
        {
            Console.Clear();
            Console.WriteLine("← → ↓ 이동 / 스페이스바 회전 / ESC 종료");

            while (true)
            {
                HandleInput();     // 사용자 키 입력 처리
                HandleAutoFall();  // 일정 시간마다 자동 낙하

                // 상단 상태 출력
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"점수: {score}");
                Console.WriteLine($"레벨: {level}");

                board.Draw(tetromino);

                Thread.Sleep(50); // CPU 사용률 제한
            }
        }

        /// <summary>
        /// 방향키, 회전, 종료 등 사용자 입력 처리
        /// </summary>
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
                    TryMoveOrFix(); // 수동 낙하
                    break;

                case InputAction.Rotate:
                    int newRotation = (tetromino.Rotation + 1) % 4;
                    if (!board.IsCollision(tetromino, rotation: newRotation))
                        tetromino.Rotate();
                    break;
            }
        }

        /// <summary>
        /// 자동 낙하 타이밍 체크 및 실행
        /// </summary>
        private void HandleAutoFall()
        {
            if ((DateTime.Now - lastFallTime) >= fallInterval)
            {
                TryMoveOrFix(); // 자동 낙하
                lastFallTime = DateTime.Now;
            }
        }

        /// <summary>
        /// 한 칸 아래로 이동 또는 고정/새 블록 생성/게임 오버 처리
        /// </summary>
        private void TryMoveOrFix()
        {
            if (!board.IsCollision(tetromino, offsetY: 1))
            {
                tetromino.Y += 1;
            }
            else
            {
                board.FixTetromino(tetromino);

                // 줄 제거 및 점수 증가
                int cleared = board.ClearLines();
                if (cleared > 0)
                {
                    score += cleared * 100;

                    // 레벨업 체크
                    int newLevel = (score / ScorePerLevel) + 1;
                    if (newLevel > level)
                    {
                        level = newLevel;
                        int newIntervalMs = Math.Max(100, 500 - (level - 1) * 50); // 최소 100ms
                        fallInterval = TimeSpan.FromMilliseconds(newIntervalMs);

                        Console.WriteLine($"레벨 {level} 달성! 속도 ↑");
                    }
                }

                // 새 블록 생성
                tetromino = new Tetromino(RandomProvider.GetRandomTetrominoType());

                // 생성하자마자 충돌 → 게임 오버
                if (board.IsCollision(tetromino))
                {
                    Console.Clear();
                    Console.WriteLine($"게임 오버! 최종 점수: {score}, 레벨: {level}");
                    Environment.Exit(0);
                }
            }
        }
    }
}
