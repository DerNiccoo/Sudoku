using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SudokuWFA
{
    static class Levelmanager
    {
        private static Random random = new Random();

        public static int[][] GetLevel(string difficulty)
        {
            int[][] result = new int[9][];
            for (int i = 0; i < result.Length; i++)
                result[i] = new int[9];

            string[] lines = File.ReadAllLines(@"Sudokus\" + difficulty + ".txt");

            if (lines.Length % 9 != 0)
                return DefaultLevel();

            int partie = random.Next(0, lines.Length / 9) * 9;

            for (int y = 0; y < 9; y++)
            {
                string[] split = lines[partie + y].Split(';');

                if (split.Length != 9)
                    return DefaultLevel();

                for (int x = 0; x < split.Length; x++)
                {
                    result[y][x] = int.Parse(split[x]);
                }
            }

            return result;
        }

        public static void SaveGame(int slot, int[][] field, string diff, int[][] preset)
        {
            List<string> output = new List<string>();
            File.Delete(@"Sudokus\save" + slot + ".txt");
            output.Add(diff + " - " + DateTime.Today.ToShortDateString());

            SaveMapToList(field, ref output);
            SaveMapToList(preset, ref output);

            File.AppendAllLines(@"Sudokus\save" + slot + ".txt", output);
        }

        private static void SaveMapToList(int[][] field, ref List<string> output)
        {
            string line = "";

            for (int y = 0; y < field.Length; y++)
            {
                line = "";
                for (int x = 0; x < field[y].Length; x++)
                {
                    line += x < field[y].Length - 1 ? field[y][x].ToString() + ";" : field[y][x].ToString();
                }
                output.Add(line);
            }
        }

        public static string CheckSaveGame(int slot)
        {
            try
            {
                string[] lines = File.ReadAllLines(@"Sudokus\save" + slot + ".txt");

                if (lines.Length > 1)
                    return lines[0];
            } catch (Exception e)
            {
                Debug.WriteLine("Die Datei Sudokus\\save" + slot + ".txt könnte nicht gefunden werden. Error Msg: " + e.Message );
                return null;
            }
            return null;              
        }

        public static void LoadSaveGame(int slot, ref string diff, ref int[][] field, ref int[][] preset)
        {
            string[] lines;
            try
            {
                lines = File.ReadAllLines(@"Sudokus\save" + slot + ".txt");
            }
            catch (Exception e)
            {
                Debug.WriteLine("Die Datei Sudokus\\save" + slot + ".txt könnte nicht gefunden werden. Error Msg: " + e.Message);
                return;
            }
            diff = lines[0].Split(' ')[0];

            field = LoadMapToIntArray(lines, 1);
            preset = LoadMapToIntArray(lines, 10);
        }

        private static int[][] LoadMapToIntArray(string[] lines, int start)
        {
            int[][] field = new int[9][];
            for (int i = 0; i < field.Length; i++)
                field[i] = new int[9];

            for (int y = 0; y < 9; y++)
            {
                string[] split = lines[y + start].Split(';');

                for (int x = 0; x < split.Length; x++)
                {
                    field[y][x] = int.Parse(split[x]);
                }
            }

            return field;
        }

        private static int[][] DefaultLevel()
        {
            int[][] numbers =
            {
                new int[] { 8, 0, 0, 7, 0, 0, 0, 3, 0 },
                new int[] { 0, 6, 0, 0, 0, 0, 7, 1, 0 },
                new int[] { 1, 0, 0, 5, 2, 3, 0, 0, 6 },
                new int[] { 0, 8, 0, 2, 4, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0, 5, 9, 0, 2 },
                new int[] { 0, 9, 3, 0, 7, 0, 0, 0, 8 },
                new int[] { 0, 0, 8, 1, 0, 0, 6, 0, 9 },
                new int[] { 9, 3, 6, 0, 0, 0, 4, 0, 0 },
                new int[] { 0, 0, 5, 9, 6, 2, 0, 0, 0 },
            };
            return numbers;
        }
    }
}
