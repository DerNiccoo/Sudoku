using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SudokuWFA
{
    class Sudoku
    {
        public bool fieldNumberHints = false;
        public bool wrongNumberHints = false;
        public bool uniqueNumberHints = false;

        public string difficulty { get; private set; }
        public int[][] map { get; private set; }
        public int[][] presetMap { get; private set; }

        private int[] boundary;
        private int fieldSize;
        private int[] focus;
        private int mapFieldSize;
        private Algorithm algorithm;
        private RichTextBox log;

        private long solveTime;
        private bool[][] preset;    //true an vorgegebenen Zahlen
        private int[][] solution;
        private List<int[][]> history = new List<int[][]>();
        private List<int[]> wrongNumbers = new List<int[]>();   //0 = X; 1 = y
        private List<int[]> uniqueNumbers = new List<int[]>();   //0 = X; 1 = y

        public Sudoku(int fieldSize, RichTextBox log, int[][] level = null, string diff = null, int[][] saveGame = null)
        {
            if (level != null)
                map = level;

            presetMap = Toolbox.DeepCopy(map);      //For load and save
            difficulty = diff;                       //Just for load and save
            focus = new int[] { 0, -1, -1 };    //Current Focus
            preset = loadPreset(map);            //True for every field that belongs to the game field
            algorithm = new Algorithm(map, log);    //To solve the Game
            this.log = log;                        //For some Log Msg

            Stopwatch s = new Stopwatch();
            s.Start();
            solution = algorithm.Solve();          //Solve for reference
            s.Stop();
            solveTime = s.ElapsedMilliseconds;
            
            CalculateSizes(fieldSize);

            if (saveGame != null)
                map = saveGame;

            UpdateNumberHintLists();
        }

        public void Draw(Graphics graphics)
        {
            DrawGamefield(graphics);

            if (uniqueNumberHints)
                DrawNumberHints(graphics, uniqueNumbers, Color.Orange);

            if (wrongNumberHints)
                DrawNumberHints(graphics, wrongNumbers, Color.Red);

            DrawFocus(graphics);
            DrawNumber(graphics);
        }

        public int NumberPressed(int number)
        {
            if (focus[0] == 0 || preset[focus[2]][focus[1]])
                return -1;

            history.Add(Toolbox.DeepCopy(map));
            map[focus[2]][focus[1]] = number;
            UpdateNumberHintLists();

            return checkWinCondition();
        }

        private void DrawGamefield(Graphics graphics)
        {
            for (int i = 0; i < 10; i++)
            {
                int thickness = i % 3 == 0 ? boundary[0] : boundary[1];

                graphics.FillRectangle(Brushes.Black, mapFieldSize * i + Toolbox.PreviousBoundary(boundary, i), 0, thickness, Toolbox.BoundaryLength(boundary, fieldSize));

                graphics.FillRectangle(Brushes.Black, 0, mapFieldSize * i + Toolbox.PreviousBoundary(boundary, i), Toolbox.BoundaryLength(boundary, fieldSize), thickness);
            }
        }

        private void DrawFocus(Graphics graphics)
        {
            if (focus[0] == 0)
                return;

            int x = mapFieldSize * focus[1] + Toolbox.PreviousBoundary(boundary, focus[1] + 1) + 4;
            int y = mapFieldSize * focus[2] + Toolbox.PreviousBoundary(boundary, focus[2] + 1) + 4;
            int height = mapFieldSize - 8;
            int width = mapFieldSize - 8;

            graphics.DrawRectangle(new Pen(Color.Green, 2), x, y, width, height);
        }

        private void DrawNumberHints(Graphics graphics, List<int[]> hintsList, Color color)
        {
            for (int i = 0; i < hintsList.Count; i++)
            {
                int x = mapFieldSize * hintsList[i][0] + Toolbox.PreviousBoundary(boundary, hintsList[i][0] + 1) + 4;
                int y = mapFieldSize * hintsList[i][1] + Toolbox.PreviousBoundary(boundary, hintsList[i][1] + 1) + 4;
                int height = mapFieldSize - 8;
                int width = mapFieldSize - 8;

                graphics.DrawRectangle(new Pen(color, 2), x, y, width, height);
            }
        }

        private void DrawNumber(Graphics graphics)
        {
            using (Font myFont = new Font("Arial", mapFieldSize / 2))
            {
                for (int y = 0; y < map.Length; y++)
                {
                    for (int x = 0; x < map[y].Length; x++)
                    {
                        int posX = fieldSize / 9 * x + (int)(mapFieldSize / 4.416666666);
                        int posY = fieldSize / 9 * y + (int)(mapFieldSize / 7.5714285714);
                        if (map[y][x] != 0)
                            graphics.DrawString(map[y][x].ToString(),
                                myFont,
                                preset[y][x] ? Brushes.Black : Brushes.Gray,
                                new Point(posX, posY));
                    }
                }
            }
        }

        public void SetFocus(int x, int y)
        {
            focus = new int[] { 0, 0, 0 };

            focus[1] = Toolbox.GetFocus(boundary, mapFieldSize, x);
            focus[2] = Toolbox.GetFocus(boundary, mapFieldSize, y);

            if (focus[1] != -1 && focus[2] != -1)
                focus[0] = 1;

            if (fieldNumberHints && focus[0] == 1)
                algorithm.AddFieldPossibilities(focus[1], focus[2], map, true, true);
        }

        public void MoveFocus(int keyValue)
        {
            if (focus[0] == 0)
            {
                focus = new int[] { 1, 4, 4 };
            } else
            {
                if (keyValue == 37) //Left
                    focus[1] = focus[1] - 1 < 0 ? 8 : focus[1] - 1;
                else if (keyValue == 39) //Right
                    focus[1] = focus[1] + 1 > 8 ? 0 : focus[1] + 1;
                else if (keyValue == 38) //Up
                    focus[2] = focus[2] - 1 < 0 ? 8 : focus[2] - 1;
                else if (keyValue == 40) //Down
                    focus[2] = focus[2] + 1 > 8 ? 0 : focus[2] + 1;
            }

            if (fieldNumberHints && focus[0] == 1)
                algorithm.AddFieldPossibilities(focus[1], focus[2], map, true, true);
        }

        public void RemoveNumber()
        {
            if (focus[0] == 0 || preset[focus[2]][focus[1]])
                return;

            history.Add(Toolbox.DeepCopy(map));
            map[focus[2]][focus[1]] = 0;
            UpdateNumberHintLists();
        }

        public bool StepBack()
        {
            if (history.Count < 1)
                return false;

            map = history[history.Count - 1];
            history.RemoveAt(history.Count - 1);
            UpdateNumberHintLists();

            if (history.Count < 1)
                return false;
            else
                return true;
        }

        private bool[][] loadPreset(int[][] numbers) //Festgebene Zahlen makieren
        {
            bool[][] preset = new bool[numbers.Length][];
            for (int y = 0; y < numbers.Length; y++)
            {
                preset[y] = new bool[numbers[y].Length];
                for (int x = 0; x < numbers[y].Length; x++)
                {
                    if (numbers[y][x] != 0)
                        preset[y][x] = true;
                }
            }
            return preset;
        }

        private int checkWinCondition() // Einführen von Enums für die einzelnen Zustände des Sudokus
        {
            if (!EveryFieldFilled(map))    //Can't win if one field is empthy
                return -1;

            if (!rowsCorrectFilled())
                return 0;

            if (!blocksCorrectFilled())
                return 0;

            return 1;
        }

        public static bool EveryFieldFilled(int[][] numbers)
        {
            for (int y = 0; y < numbers.Length; y++)
            {
                for (int x = 0; x < numbers[y].Length; x++)
                {
                    if (numbers[y][x] == 0)
                        return false;
                }
            }

            return true;
        }

        private bool rowsCorrectFilled()
        {
            for (int y = 0; y < map.Length; y++)
            {
                int[] check = map[y].Distinct().ToArray();
                if (map[y].Length != check.Length)
                    return false;

                check = Toolbox.ColumnToRow(map, y).Distinct().ToArray();
                if (map[y].Length != check.Length)
                    return false;
            }

            return true;
        }

        private bool blocksCorrectFilled()
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    int[] check = Toolbox.BlockToRow(map, x * 3, y * 3).Distinct().ToArray();
                    if (map.Length != check.Length) //kinda cheaty?
                        return false;
                }
            }
            return true;
        }

        public void Solve()
        {
            //algorithm.AlgorithmUpdate(map); //DEBUG ONLY
            map = solution;
            wrongNumbers = new List<int[]>();
            Log("Das Sudoku wurde in " + algorithm.iterations + " Iterationen und " + solveTime + " ms gelöst.");
        }

        private void UpdateNumberHintLists()
        {
            int[][] mapWithNoFails = FieldWithoutWrongNumbers();

            wrongNumbers = new List<int[]>();
            uniqueNumbers = new List<int[]>();
            
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (preset[y][x])
                        continue;

                    if (map[y][x] != 0 && map[y][x] != solution[y][x])
                        wrongNumbers.Add(new int[] { x, y });

                    if (map[y][x] != solution[y][x] && algorithm.AddFieldPossibilities(x, y, mapWithNoFails, false, true).Count == 1)
                        uniqueNumbers.Add(new int[] { x, y });
                }
            }
        }

        public void GiveHintToPlayer()
        {
            //Step One: Prüfen ob im aktuellen Feld ein Fehler ist 
            if (wrongNumbers.Count > 0) 
            {
                HintWithFails();
            }
            //Step Two: Wenn kein Fehler - SolveByLogic für die einzelnen Regeln
            else
            {
                List<int> leastCount = new List<int>();
                int x = 0, y = 0;
                if (!HintWithOnePossibility(ref leastCount, ref x, ref y)) //Step Three: Wenn SBL nix bringt Feld mit geringsten Möglichkeiten suchen und damit raten.
                {
                    string output = "";
                    foreach (int i in leastCount)
                        output += i + ", ";

                    output = output.Substring(0, output.Length - 2);
                    Log("Es gibt aktuell keine eindeutige Lösung. In dies Feld könnte " + output + ". Es wird zur " + solution[y][x] + " geraten.");
                    focus = new int[] { 1, x, y };
                }
            }            
        }

        private bool HintWithOnePossibility(ref List<int> leastCount, ref int xPos, ref int yPos)
        {
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x] == 0)
                    {
                        List<int> possi = algorithm.AddFieldPossibilities(x, y, map, false, true);

                        if (possi.Count == 1)
                        {
                            Log("Für dieses Feld gibt es nur eine eindeutige Lösung. Andere Zahlen würden die Regeln verletzten. Hier muss eine " + possi[0] + " stehen.");
                            focus = new int[] { 1, x, y };
                            return true;
                        }
                        else if (leastCount.Count == 0 || possi.Count < leastCount.Count)
                        {
                            leastCount = new List<int>(possi);
                            xPos = x;
                            yPos = y;
                        }
                    }
                }
            }
            return false;
        }

        private void HintWithFails()
        {
            string output = "";
            int[][] noFailsMap = FieldWithoutWrongNumbers();
            int x = wrongNumbers[0][0];
            int y = wrongNumbers[0][1];
            if (noFailsMap[y].Contains(map[y][x]))
            {
                output = " steht bereits in dieser Spalte.";
            }
            else if (Toolbox.ColumnToRow(noFailsMap, x).Contains(map[y][x]))
            {
                output = " steht bereits in dieser Zeile.";
            }
            else if (Toolbox.BlockToRow(noFailsMap, x, y).Contains(map[y][x]))
            {
                output = " steht bereits in diesem Block.";
            }
            else
            {
                output = " ist falsch. Richtig wäre " + solution[y][x] + ".";
            }
            if (!output.Equals(""))
            {
                Log("Hier ist ein Fehler. Die " + map[y][x] + output);
                focus = new int[] { 1, x, y };
            }
        }

        private int[][] FieldWithoutWrongNumbers()
        {
            int[][] result = Toolbox.DeepCopy(map);

            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (result[y][x] != 0 && solution[y][x] != result[y][x])
                        result[y][x] = 0;
                }
            }


            return result;
        }

        private void Log(string logText)
        {
            log.BeginInvoke(new Action(() => Toolbox.LogTextEvent(log, Color.Black, logText)));
        }

        public void CalculateSizes(int size)
        {
            boundary = new int[] { 3, 1 };         //Boarder Thickness for Resize
            fieldSize = size;                       //Same size
            mapFieldSize = (fieldSize - Toolbox.AllBoundary(boundary)) / 9;
        }
    }
}
