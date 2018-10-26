using System;
using System.Drawing;
using System.Windows.Forms;

namespace SudokuWFA
{
    static class Toolbox
    {
        private static string oldLog = "";

        public static int PreviousBoundary(int[] boundary, int count)
        {
            int result = 0;
            for (int i = 0; i < count; i++)
                result += i % 3 == 0 ? boundary[0] : boundary[1];

            return result;
        }

        public static int AllBoundary(int[] boundary)
        {
            return PreviousBoundary(boundary, 10);
        }

        public static int BoundaryLength(int[] boundary, int fieldWith)
        {
            int allBoundary = AllBoundary(boundary);

            return (fieldWith - allBoundary) / 9 * 9 + allBoundary;
        }

        public static int GetFocus(int[] boundary, int numberFieldSize, int coord)
        {
            for (int i = 0; i < 9; i++)
            {
                int border = PreviousBoundary(boundary, i + 1);

                if (coord > numberFieldSize * i + border &&
                    coord < numberFieldSize * (i + 1) + border)
                {
                    return i;
                }
            }
            return -1;
        }

        public static int[] ColumnToRow(int[][] numbers, int column)
        {
            int[] result = new int[numbers.Length];

            for (int i = 0; i < numbers.Length; i++)
            {
                result[i] = numbers[i][column];
            }            

            return result;
        }

        public static int[] BlockToRow(int[][] numbers, int xCoord, int yCoord)
        {
            int[] result = new int[numbers.Length];
            int xBlock = xCoord / 3 * 3;
            int yBlock = yCoord / 3 * 3;

            int i = 0;
            for (int y = yBlock; y < yBlock + 3; y++)
            {
                for (int x = xBlock; x < xBlock + 3; x++, i++)
                {
                    result[i] = numbers[y][x];
                }
            }

            return result;
        }

        public static int[][] DeepCopy(int[][] input)
        {
            int[][] result = new int[input.Length][];
            for (int i = 0; i < input.Length; i++)
                result[i] = (int[])input[i].Clone();
            return result;
        }

        public static int NumberPressed(int keyValue)
        {
            if (keyValue == 49)
                return 1;
            else if (keyValue == 49)
                return 1;
            else if (keyValue == 50)
                return 2;
            else if (keyValue == 51)
                return 3;
            else if (keyValue == 52)
                return 4;
            else if (keyValue == 53)
                return 5;
            else if (keyValue == 54)
                return 6;
            else if (keyValue == 55)
                return 7;
            else if (keyValue == 56)
                return 8;
            else if (keyValue == 57)
                return 9;
            else if (keyValue == 97)
                return 1;
            else if (keyValue == 98)
                return 2;
            else if (keyValue == 99)
                return 3;
            else if (keyValue == 100)
                return 4;
            else if (keyValue == 101)
                return 5;
            else if (keyValue == 102)
                return 6;
            else if (keyValue == 103)
                return 7;
            else if (keyValue == 104)
                return 8;
            else if (keyValue == 105)
                return 9;
            else
                return -1;
        }

        public static void LogTextEvent(RichTextBox TextEventLog, Color TextColor, string EventText)
        {
            //if (oldLog == EventText)
            //    return;

            if (TextEventLog.InvokeRequired)
            {
                TextEventLog.BeginInvoke(new Action(delegate {
                    LogTextEvent(TextEventLog, TextColor, EventText);
                }));
                return;
            }

            string nDateTime = DateTime.Now.ToString("hh:mm:ss tt") + " - ";

            // color text.
            TextEventLog.SelectionStart = TextEventLog.Text.Length;
            TextEventLog.SelectionColor = TextColor;
            
            // newline if first line, append if else.
            if (TextEventLog.Lines.Length == 0)
            {
                TextEventLog.AppendText(nDateTime + EventText);
                TextEventLog.ScrollToCaret();
                TextEventLog.AppendText(System.Environment.NewLine);
            }
            else
            {
                TextEventLog.AppendText(nDateTime + EventText + System.Environment.NewLine);
                TextEventLog.ScrollToCaret();
            }
            oldLog = EventText;
        }
    }
}
