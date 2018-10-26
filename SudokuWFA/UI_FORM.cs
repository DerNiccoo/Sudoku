using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SudokuWFA
{
    public partial class UI_Form : Form
    {
        private Sudoku sudoku;
        private List<ToolStripItem> loadItems = new List<ToolStripItem>();
        private List<ToolStripItem> saveItems = new List<ToolStripItem>();

        public UI_Form()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //TODO: Schauen ob ein Level im Speicher liegt
            NewLevel("Leicht", false);
            loadItems.Add(spielstand1ToolStripMenuItem);
            loadItems.Add(spielstand2ToolStripMenuItem);
            loadItems.Add(spielstand3ToolStripMenuItem);
            saveItems.Add(spielstand1ToolStripMenuItem1);
            saveItems.Add(spielstand2ToolStripMenuItem1);
            saveItems.Add(spielstand3ToolStripMenuItem1);
            CheckSaveGames();
        }

        private void spielfeld_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            sudoku.Draw(e.Graphics);
        }

        private void spielfeld_Resize(object sender, EventArgs e)
        {
            // TODO: Berechnung der Nummerpositionen
            spielfeld.Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int numberPressed = Toolbox.NumberPressed(e.KeyValue);
            if (numberPressed != -1)
            {
                zurückToolStripMenuItem.Enabled = true;
                int result = sudoku.NumberPressed(numberPressed);
                spielfeld.Refresh();
                if (result == 1) //Spiel gewonnen
                    gameWon();
                else if (result == 0) //Fehler
                    MessageBox.Show("Irgendwo steckt da noch ein Fehler drinne :(", "Fehler!", MessageBoxButtons.OK);

            }
            else if (e.KeyValue == 8 || e.KeyValue == 46)
            {
                sudoku.RemoveNumber();
            }
            else if (e.KeyValue >= 37 && e.KeyValue <= 40 )
            {
                sudoku.MoveFocus(e.KeyValue);
            }
            else if (e.Control && e.KeyValue == 90)
            {
                sudoku.StepBack();
            }
            spielfeld.Refresh();
        }

        private void gameWon()
        {            
            MessageBox.Show("Du hast das Sudoku richtig gelöst!", "Gewonnen!", MessageBoxButtons.OK);
            tippToolStripMenuItem.Enabled = false;
            //TODO: Add stuff from winning here
        }

        private void spielfeld_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            sudoku.SetFocus(e.X, e.Y);
            spielfeld.Refresh();
        }

        private void NewLevel(string difficulty, bool logAcitv = true)
        {
            log.Clear();
            tippToolStripMenuItem.Enabled = true;
            int[][] level = Levelmanager.GetLevel(difficulty);
            sudoku = new Sudoku(spielfeld.Width, log, level, difficulty);
            sudoku.fieldNumberHints = möglicheEingabenToolStripMenuItem.Checked;
            sudoku.wrongNumberHints = fehlerhafteEingabeToolStripMenuItem.Checked;

            zurückToolStripMenuItem.Enabled = false;

            if (logAcitv)
                log.BeginInvoke(new Action(() => Toolbox.LogTextEvent(log, Color.Black, "Neues " + difficulty + " Sudoku geladen")));
            spielfeld.Refresh();
        }

        private void leichtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewLevel("Leicht");
        }

        private void schwerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewLevel("Schwer");
        }

        private void mittelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewLevel("Mittel");
        }

        private void schwerstenDerWeltToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewLevel("Insane");
        }

        private void lösenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sudoku.Solve();
            spielfeld.Refresh();
            MessageBox.Show("Das Sudoku wurde durch den Algorithmus richtig gelöst.", "Gelöst", MessageBoxButtons.OK);
            tippToolStripMenuItem.Enabled = false;
        }

        private void möglicheEingabenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sudoku.fieldNumberHints = !sudoku.fieldNumberHints;
            möglicheEingabenToolStripMenuItem.Checked = sudoku.fieldNumberHints;
            spielfeld.Refresh();
        }

        private void fehlerhafteEingabeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sudoku.wrongNumberHints = !sudoku.wrongNumberHints;
            fehlerhafteEingabeToolStripMenuItem.Checked = sudoku.wrongNumberHints;
            spielfeld.Refresh();
        }

        private void zurückToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!sudoku.StepBack())
                zurückToolStripMenuItem.Enabled = false;
            spielfeld.Refresh();
        }

        private void tippToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sudoku.GiveHintToPlayer();
            spielfeld.Refresh();
        }

        private void spielstand1ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            log.BeginInvoke(new Action(() => Toolbox.LogTextEvent(log, Color.Black, "Spielstand 1: " + sudoku.difficulty + " wurde gespeichert.")));
            Levelmanager.SaveGame(1, sudoku.map, sudoku.difficulty, sudoku.presetMap);
            CheckSaveGames();
        }

        private void spielstand2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            log.BeginInvoke(new Action(() => Toolbox.LogTextEvent(log, Color.Black, "Spielstand 2: " + sudoku.difficulty + " wurde gespeichert.")));
            Levelmanager.SaveGame(2, sudoku.map, sudoku.difficulty, sudoku.presetMap);
            CheckSaveGames();
        }

        private void spielstand3ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            log.BeginInvoke(new Action(() => Toolbox.LogTextEvent(log, Color.Black, "Spielstand 3: " + sudoku.difficulty + " wurde gespeichert.")));
            Levelmanager.SaveGame(3, sudoku.map, sudoku.difficulty, sudoku.presetMap);
            CheckSaveGames();
        }

        private void CheckSaveGames()
        {
            for (int i = 0; i < loadItems.Count; i++)
            {
                string name = Levelmanager.CheckSaveGame(i + 1);
                if (name != null)
                {
                    loadItems[i].Text = name;
                    loadItems[i].Enabled = true;
                    saveItems[i].Text = name;
                }
            }
        }

        private void spielstand1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSaveGame(1);
        }

        private void spielstand2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSaveGame(2);
        }

        private void spielstand3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSaveGame(3);
        }

        private void LoadSaveGame(int slot)
        {
            int[][] field = new int[9][];
            int[][] preset = new int[9][];
            string diff = "";

            Levelmanager.LoadSaveGame(slot, ref diff, ref field, ref preset);

            sudoku = new Sudoku(spielfeld.Width, log, preset, diff, field); //TODO: FIXEN
            sudoku.fieldNumberHints = möglicheEingabenToolStripMenuItem.Checked;
            sudoku.wrongNumberHints = fehlerhafteEingabeToolStripMenuItem.Checked;
            log.BeginInvoke(new Action(() => Toolbox.LogTextEvent(log, Color.Black, "Spielstand " + slot + ": " + diff + " wurde geladen.")));
            spielfeld.Refresh();
        }
    }
}
