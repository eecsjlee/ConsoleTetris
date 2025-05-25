using System;
using System.Threading;
using ConsoleTetris.Core;
using ConsoleTetris.Input;
using ConsoleTetris.Utils; // 랜덤 블록 생성

namespace ConsoleTetris
{
    class Program
    {
        static void Main(string[] args)
        {
            // 게임 보드 및 초기 블록 생성
            Board board = new Board();
            Tetromino tetromino = new Tetromino(RandomProvider.GetRandomTetrominoType());

            // 중력 타이머 설정
            DateTime lastFallTime = DateTime.Now;
            TimeSpan fallInterval = TimeSpan.FromMilliseconds(500); // 0.5초마다 자동 낙하

            // 점수
            int score = 0;

            Console.Clear();
            Console.WriteLine("← → ↓ 이동 / 스페이스바 회전 / ESC 종료");

            while (true)
            {
                // 1. 사용자 입력 처리
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
                        // ↓ 키 누르면 수동 낙하
                        TryMoveOrFix(board, ref tetromino, ref score);
                        break;

                    case InputAction.Rotate:
                        int newRotation = (tetromino.Rotation + 1) % 4;
                        if (!board.IsCollision(tetromino, rotation: newRotation))
                            tetromino.Rotate();
                        break;

                    case InputAction.None:
                        break;
                }

                // 2. 자동 낙하 처리 (중력)
                if ((DateTime.Now - lastFallTime) >= fallInterval)
                {
                    TryMoveOrFix(board, ref tetromino, ref score);
                    lastFallTime = DateTime.Now;
                }

                // 3. 화면 출력
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"점수: {score}");
                board.Draw(tetromino);

                Thread.Sleep(50); // CPU 과점유 방지
            }
        }

        /// <summary>
        /// 블록을 아래로 한 칸 이동하거나, 이동 불가 시 보드에 고정하고 새 블록 생성
        /// </summary>
        private static void TryMoveOrFix(Board board, ref Tetromino tetromino, ref int score)
        {
            if (!board.IsCollision(tetromino, offsetY: 1))
            {
                tetromino.Y += 1; // 아래로 이동
            }
            else
            {
                board.FixTetromino(tetromino); // 고정

                // 줄 제거 및 점수 처리
                int cleared = board.ClearLines();
                if (cleared > 0)
                {
                    score += cleared * 100; // 한 줄당 100점
                }

                // 새 블록 생성
                tetromino = new Tetromino(RandomProvider.GetRandomTetrominoType());

                // 생성 직후 충돌 → 게임 오버
                if (board.IsCollision(tetromino))
                {
                    Console.Clear();
                    Console.WriteLine($"게임 오버! 최종 점수: {score}");
                    Environment.Exit(0); // 강제 종료
                }
            }
        }
    }
}
