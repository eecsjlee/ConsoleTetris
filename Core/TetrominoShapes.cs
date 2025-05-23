using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris.Core
{
    public static class TetrominoShapes
    {
        public static int[][,] GetShapes(TetrominoType type)
        {
            return type switch
            {
                TetrominoType.I => new int[][,]
                {
                    new int[,] { {1, 1, 1, 1} },
                    new int[,] {
                        {1},
                        {1},
                        {1},
                        {1}
                    }
                },
                TetrominoType.O => new int[][,]
                {
                    new int[,] {
                        {1, 1},
                        {1, 1}
                    }
                },
                TetrominoType.T => new int[][,]
                {
                    new int[,] {
                        {0, 1, 0},
                        {1, 1, 1}
                    },
                    new int[,] {
                        {1, 0},
                        {1, 1},
                        {1, 0}
                    },
                    new int[,] {
                        {1, 1, 1},
                        {0, 1, 0}
                    },
                    new int[,] {
                        {0, 1},
                        {1, 1},
                        {0, 1}
                    }
                },
                // 나머지 S, Z, J, L 생략 가능 (나중에 추가)
                _ => new int[1][,] { new int[1, 1] { { 1 } } }
            };
        }
    }
}
