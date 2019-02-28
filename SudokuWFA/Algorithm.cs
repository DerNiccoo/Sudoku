using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SudokuWFA
{
    class Algorithm
    {
        private List<List<List<int>>> map;
        private RichTextBox log;
        private int[][] mapStart;
        public int iterations = 1;
        
        public Algorithm(int[][] numbers, RichTextBox log)
        {
            this.log = log;
            mapStart = numbers;
            UpdateMap(numbers);
        }

        public int[][] Solve(int[][] gamefield = null, int x = -1, int y = -1, int tryNumber = -1)
        {
            int[][] result;
            if (gamefield == null)
            {
                result = Toolbox.DeepCopy(mapStart);
                UpdateMap(mapStart);
            }
            else // Wir sind in einem Rekursionsaufruf
            {
                result = Toolbox.DeepCopy(gamefield);
                result[y][x] = tryNumber;  //Hier könnte es curshen 
                UpdateMap(result);
            }

            while (!SolveByLogic(ref result))
            {
                int[] field = LeastPossibleField();
                iterations++;
                if (field[2] != 0) 
                {
                    List<int> possibles = new List<int>(map[field[1]][field[0]]);
                    for (int i = 0; i < possibles.Count; i++)
                    {
                        int[][] ergebnis = Solve(result, field[0], field[1], possibles[i]);
                        if (ergebnis != null)
                            return ergebnis;
                    }
                }
                return null; //Deadend
            }

            //log.BeginInvoke(new Action(() => Toolbox.LogTextEvent(log, Color.Black, "Sudoku gelöst in " + iterations + " Iterationen.")));
            return result;
        }

        private int[] LeastPossibleField()
        {
            int[] field = new int[3];
            field[2] = 10;

            for (int y = 0; y < map.Count; y++)
            {
                for (int x = 0; x < map.Count; x++)
                {
                    int possibles = map[y][x].Count;
                    if (possibles == 0 || possibles == 2)// Kleinster möglichen Wert gefunden TAKE IT
                    {
                        field[0] = x;
                        field[1] = y;
                        field[2] = possibles;
                        return field;
                    } 
                    else if(possibles != 1 && possibles < field[2])
                    {
                        field[0] = x;
                        field[1] = y;
                        field[2] = possibles;
                    }
                }
            }
            return field;
        }

        private bool SolveByLogic(ref int[][] result)
        {
            bool mapChanged = false;
            while (!Sudoku.EveryFieldFilled(result))
            {
                mapChanged = false;
                UpdateMap(result);
                for (int y = 0; y < result.Length; y++)
                {
                    for (int x = 0; x < result[y].Length; x++)
                    {
                        if (result[y][x] == 0)
                        {
                            AddFieldPossibilities(x, y, result, false);
                            if (map[y][x].Count == 1) // Wir haben nur ein EINDEUTIGES Ergebnis, das muss es sein
                            {
                                result[y][x] = map[y][x][0];
                                mapChanged = true;
                            }
                        }
                    }
                }
                if (!mapChanged)
                {
                    //Ausschlussverfahren beginnen
                    if (ApplyHorizontalRule(ref result))
                        mapChanged = true;
                    if (ApplyVerticalRule(ref result))
                        mapChanged = true;
                    if (ApplyBlockRule(ref result))
                        mapChanged = true;
                    if (!mapChanged)
                        return false;
                }
            }
            return true;
        }

        private bool ApplyBlockRule(ref int[][] result)
        {
            List<List<int>> block;
            bool hit = false;

            for (int b = 0; b < map.Count; b++)
            {
                block = GetBlockLine(b);
                List<int> unique = FindUniqueNumber(block);
                for (int i = 0; i < unique.Count; i++)
                {
                    int xPos = b % 3 * 3;
                    int yPos = b / 3 * 3;

                    for (int y = yPos; y < yPos + 3; y++)
                    {
                        for (int x = xPos; x < xPos + 3; x++)
                        {
                            if (map[y][x].Contains(unique[i]))
                            {
                                hit = true;
                                result[y][x] = unique[i];
                            }
                        }
                    }
                }
            }

            return hit;
        }

        private bool ApplyVerticalRule(ref int[][] result)
        {
            List<List<int>> vertical;
            bool hit = false;

            for (int x = 0; x < map.Count; x++)
            {
                vertical = GetVerticalLine(x);
                List<int> unique = FindUniqueNumber(vertical);
                for (int i = 0; i < unique.Count; i++)
                {
                    for (int y = 0; y < vertical.Count; y++)
                    {
                        if (map[y][x].Contains(unique[i]))
                        {
                            hit = true;
                            result[y][x] = unique[i];
                        }
                    }
                }
            }

            return hit;
        }

        private bool ApplyHorizontalRule(ref int[][] result)
        {
            bool hit = false;
            for (int y = 0; y < map.Count; y++)
            {
                List<int> unique = FindUniqueNumber(map[y]);
                for (int i = 0; i < unique.Count; i++)
                {
                    for (int x = 0; x < map[y].Count; x++)
                    {
                        if (map[y][x].Contains(unique[i])) //Treffer!
                        {
                            hit = true;
                            result[y][x] = unique[i];
                        }
                    }
                }
            }
            
            return hit;
        }

        private List<List<int>> GetVerticalLine(int index)
        {
            List<List<int>> result = new List<List<int>>();

            for (int y = 0; y < map.Count; y++)
                result.Add(map[y][index]);

            return result;
        }

        private List<List<int>> GetBlockLine(int index)
        {
            List<List<int>> result = new List<List<int>>();

            int xPos = index % 3 * 3;
            int yPos = index / 3 * 3;

            for (int y = yPos; y < yPos + 3; y++)
            {
                for (int x = xPos; x < xPos + 3; x++)
                {
                    result.Add(map[y][x]);
                }
            }

            return result;
        }

        private List<int> FindUniqueNumber(List<List<int>> field)
        {
            List<int> result = new List<int>();
            int hits;

            for (int i = 1; i <= 9; i++)    //Geht alle mögl. Zahlen durch
            {
                hits = 0;
                for (int y = 0; y < field.Count; y++)
                {
                    if (field[y].Count <= 1)
                        continue;

                    for (int x = 0; x < field[y].Count; x++)
                    {
                        if (field[y][x] == i)
                        {
                            hits++;
                            break;
                        }
                        else if (field[y][x] > i) break;    //Reduce overhead
                    }
                    if (hits >= 2) break;   //Reduce overhead
                }
                if (hits == 1)
                    result.Add(i);
            }

            return result;
        }

        public List<int> AddFieldPossibilities(int x, int y, int[][] fields, bool logActiv = true, bool singleField = false)
        {
            if (singleField)
                UpdateMap(fields);

            for (int number = 1; number < 10; number++)
            {
                // Y Row ausschließen
                if (fields[y].Contains(number))
                    continue;
                // X Row ausschließen
                if (Toolbox.ColumnToRow(fields, x).Contains(number))
                    continue;
                // Block ausschließen
                if (Toolbox.BlockToRow(fields, x, y).Contains(number))
                    continue;

                if (!map[y][x].Contains(number))
                    map[y][x].Add(number);
            }
            if (logActiv)
            {
                string output = "Mögliche Zahlen für das Feld sind: ";
                foreach (int i in map[y][x])
                    output += i.ToString() + ", ";

                output = output.Substring(0, output.Length - 2);

                log.BeginInvoke(new Action(() => Toolbox.LogTextEvent(log, Color.Black, output)));
            }
            return map[y][x];
        }

        public void UpdateMap(int[][] numbers)
        {
            map = new List<List<List<int>>>();
            for (int y = 0; y < numbers.Length; y++)
            {
                map.Add(new List<List<int>>());
                for (int x = 0; x < numbers[y].Length; x++)
                {
                    map[y].Add(new List<int>());
                    if (numbers[y][x] != 0)
                        map[y][x].Add(numbers[y][x]);
                }
            }
        }
    }
}