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
                TetrominoType.S => new int[][,]
{
                    new int[,] {
                        {0, 1, 1},
                        {1, 1, 0}
                    },
                    new int[,] {
                        {1, 0},
                        {1, 1},
                        {0, 1}
                    }
},
                TetrominoType.Z => new int[][,]
                {
                    new int[,] {
                        {1, 1, 0},
                        {0, 1, 1}
                    },
                    new int[,] {
                        {0, 1},
                        {1, 1},
                        {1, 0}
                    }
                },
                TetrominoType.J => new int[][,]
                {
                    new int[,] {
                        {1, 0, 0},
                        {1, 1, 1}
                    },
                    new int[,] {
                        {1, 1},
                        {1, 0},
                        {1, 0}
                    },
                    new int[,] {
                        {1, 1, 1},
                        {0, 0, 1}
                    },
                    new int[,] {
                        {0, 1},
                        {0, 1},
                        {1, 1}
                    }
                },
                TetrominoType.L => new int[][,]
                {
                    new int[,] {
                        {0, 0, 1},
                        {1, 1, 1}
                    },
                    new int[,] {
                        {1, 0},
                        {1, 0},
                        {1, 1}
                    },
                    new int[,] {
                        {1, 1, 1},
                        {1, 0, 0}
                    },
                    new int[,] {
                        {1, 1},
                        {0, 1},
                        {0, 1}
                    }
                },
                _ => new int[1][,] { new int[1, 1] { { 1 } } } // 예외 처리용 기본 모양
            };
        }
    }
}
