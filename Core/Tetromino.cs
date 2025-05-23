using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris.Core
{
    public class Tetromino
    {
        public TetrominoType Type { get; }
        public int Rotation { get; private set; } = 0;
        public int X { get; set; } = 3; // 기본 시작 위치
        public int Y { get; set; } = 0;

        private readonly int[][,] shapes;

        public Tetromino(TetrominoType type)
        {
            Type = type;
            shapes = TetrominoShapes.GetShapes(type);
        }

        public int[,] GetCurrentShape()
        {
            return shapes[Rotation];
        }

        public void Rotate()
        {
            Rotation = (Rotation + 1) % shapes.Length;
        }

        public void RotateBack()
        {
            Rotation = (Rotation - 1 + shapes.Length) % shapes.Length;
        }
        public int[,] GetShape(int rotation)
        {
            return shapes[rotation % shapes.Length];
        }
    }
}
