using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ConsoleTetris.Core;

namespace ConsoleTetris.Constants
{
    public static class TetrominoColors
    {
        public static readonly Dictionary<TetrominoType, ConsoleColor> ColorMap;

        static TetrominoColors()
        {
            string path = Path.Combine("Configs", "colors.json"); // json 파일 경로
            if (!File.Exists(path))
            {
                Console.WriteLine("colors.json 파일을 찾을 수 없습니다. 기본 색상으로 대체합니다.");
                ColorMap = GetDefaultColors();
                return;
            }

            try
            {
                string json = File.ReadAllText(path);
                Dictionary<string, string> raw = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

                ColorMap = new Dictionary<TetrominoType, ConsoleColor>();
                foreach (var kv in raw)
                {
                    if (Enum.TryParse<TetrominoType>(kv.Key, out var type) &&
                        Enum.TryParse<ConsoleColor>(kv.Value, out var color))
                    {
                        ColorMap[type] = color;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("colors.json 파싱 중 오류 발생: " + ex.Message);
                ColorMap = GetDefaultColors();
            }
        }

        private static Dictionary<TetrominoType, ConsoleColor> GetDefaultColors()
        {
            return new Dictionary<TetrominoType, ConsoleColor>
            {
                { TetrominoType.I, ConsoleColor.Cyan },
                { TetrominoType.O, ConsoleColor.Yellow },
                { TetrominoType.T, ConsoleColor.Magenta },
                { TetrominoType.S, ConsoleColor.Green },
                { TetrominoType.Z, ConsoleColor.Red },
                { TetrominoType.J, ConsoleColor.Blue },
                { TetrominoType.L, ConsoleColor.DarkYellow }
            };
        }
    }
}
