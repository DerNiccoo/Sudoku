namespace SudokuWFA
{
    partial class UI_Form
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.spielfeld = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.spielToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neuesSpielToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leichtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mittelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.schwerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.schwerstenDerWeltToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spielstandLadenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spielstand1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spielstand2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spielstand3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spielstandSpeichernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spielstand1ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.spielstand2ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.spielstand3ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lösungshilfeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.möglicheEingabenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fehlerhafteEingabeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zurückToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tippToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lösenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.anleitungToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.log = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.spielfeld)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // spielfeld
            // 
            this.spielfeld.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spielfeld.Location = new System.Drawing.Point(29, 34);
            this.spielfeld.Name = "spielfeld";
            this.spielfeld.Size = new System.Drawing.Size(500, 500);
            this.spielfeld.TabIndex = 0;
            this.spielfeld.TabStop = false;
            this.spielfeld.Paint += new System.Windows.Forms.PaintEventHandler(this.spielfeld_Paint);
            this.spielfeld.MouseClick += new System.Windows.Forms.MouseEventHandler(this.spielfeld_MouseClick);
            this.spielfeld.Resize += new System.EventHandler(this.spielfeld_Resize);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spielToolStripMenuItem,
            this.lösungshilfeToolStripMenuItem,
            this.zurückToolStripMenuItem,
            this.tippToolStripMenuItem,
            this.lösenToolStripMenuItem,
            this.anleitungToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(553, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // spielToolStripMenuItem
            // 
            this.spielToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.neuesSpielToolStripMenuItem,
            this.spielstandLadenToolStripMenuItem,
            this.spielstandSpeichernToolStripMenuItem});
            this.spielToolStripMenuItem.Name = "spielToolStripMenuItem";
            this.spielToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.spielToolStripMenuItem.Text = "Spiel";
            // 
            // neuesSpielToolStripMenuItem
            // 
            this.neuesSpielToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.leichtToolStripMenuItem,
            this.mittelToolStripMenuItem,
            this.schwerToolStripMenuItem,
            this.schwerstenDerWeltToolStripMenuItem});
            this.neuesSpielToolStripMenuItem.Name = "neuesSpielToolStripMenuItem";
            this.neuesSpielToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.neuesSpielToolStripMenuItem.Text = "Neues Spiel";
            // 
            // leichtToolStripMenuItem
            // 
            this.leichtToolStripMenuItem.Name = "leichtToolStripMenuItem";
            this.leichtToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.leichtToolStripMenuItem.Text = "Leicht";
            this.leichtToolStripMenuItem.Click += new System.EventHandler(this.leichtToolStripMenuItem_Click);
            // 
            // mittelToolStripMenuItem
            // 
            this.mittelToolStripMenuItem.Name = "mittelToolStripMenuItem";
            this.mittelToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.mittelToolStripMenuItem.Text = "Mittel";
            this.mittelToolStripMenuItem.Click += new System.EventHandler(this.mittelToolStripMenuItem_Click);
            // 
            // schwerToolStripMenuItem
            // 
            this.schwerToolStripMenuItem.Name = "schwerToolStripMenuItem";
            this.schwerToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.schwerToolStripMenuItem.Text = "Schwer";
            this.schwerToolStripMenuItem.Click += new System.EventHandler(this.schwerToolStripMenuItem_Click);
            // 
            // schwerstenDerWeltToolStripMenuItem
            // 
            this.schwerstenDerWeltToolStripMenuItem.Name = "schwerstenDerWeltToolStripMenuItem";
            this.schwerstenDerWeltToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.schwerstenDerWeltToolStripMenuItem.Text = "Insane";
            this.schwerstenDerWeltToolStripMenuItem.Click += new System.EventHandler(this.schwerstenDerWeltToolStripMenuItem_Click);
            // 
            // spielstandLadenToolStripMenuItem
            // 
            this.spielstandLadenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spielstand1ToolStripMenuItem,
            this.spielstand2ToolStripMenuItem,
            this.spielstand3ToolStripMenuItem});
            this.spielstandLadenToolStripMenuItem.Name = "spielstandLadenToolStripMenuItem";
            this.spielstandLadenToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.spielstandLadenToolStripMenuItem.Text = "Spielstand laden";
            // 
            // spielstand1ToolStripMenuItem
            // 
            this.spielstand1ToolStripMenuItem.Enabled = false;
            this.spielstand1ToolStripMenuItem.Name = "spielstand1ToolStripMenuItem";
            this.spielstand1ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.spielstand1ToolStripMenuItem.Text = "Spielstand 1";
            this.spielstand1ToolStripMenuItem.Click += new System.EventHandler(this.spielstand1ToolStripMenuItem_Click);
            // 
            // spielstand2ToolStripMenuItem
            // 
            this.spielstand2ToolStripMenuItem.Enabled = false;
            this.spielstand2ToolStripMenuItem.Name = "spielstand2ToolStripMenuItem";
            this.spielstand2ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.spielstand2ToolStripMenuItem.Text = "Spielstand 2";
            this.spielstand2ToolStripMenuItem.Click += new System.EventHandler(this.spielstand2ToolStripMenuItem_Click);
            // 
            // spielstand3ToolStripMenuItem
            // 
            this.spielstand3ToolStripMenuItem.Enabled = false;
            this.spielstand3ToolStripMenuItem.Name = "spielstand3ToolStripMenuItem";
            this.spielstand3ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.spielstand3ToolStripMenuItem.Text = "Spielstand 3";
            this.spielstand3ToolStripMenuItem.Click += new System.EventHandler(this.spielstand3ToolStripMenuItem_Click);
            // 
            // spielstandSpeichernToolStripMenuItem
            // 
            this.spielstandSpeichernToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spielstand1ToolStripMenuItem1,
            this.spielstand2ToolStripMenuItem1,
            this.spielstand3ToolStripMenuItem1});
            this.spielstandSpeichernToolStripMenuItem.Name = "spielstandSpeichernToolStripMenuItem";
            this.spielstandSpeichernToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.spielstandSpeichernToolStripMenuItem.Text = "Spielstand speichern";
            // 
            // spielstand1ToolStripMenuItem1
            // 
            this.spielstand1ToolStripMenuItem1.Name = "spielstand1ToolStripMenuItem1";
            this.spielstand1ToolStripMenuItem1.Size = new System.Drawing.Size(137, 22);
            this.spielstand1ToolStripMenuItem1.Text = "Spielstand 1";
            this.spielstand1ToolStripMenuItem1.Click += new System.EventHandler(this.spielstand1ToolStripMenuItem1_Click);
            // 
            // spielstand2ToolStripMenuItem1
            // 
            this.spielstand2ToolStripMenuItem1.Name = "spielstand2ToolStripMenuItem1";
            this.spielstand2ToolStripMenuItem1.Size = new System.Drawing.Size(137, 22);
            this.spielstand2ToolStripMenuItem1.Text = "Spielstand 2";
            this.spielstand2ToolStripMenuItem1.Click += new System.EventHandler(this.spielstand2ToolStripMenuItem1_Click);
            // 
            // spielstand3ToolStripMenuItem1
            // 
            this.spielstand3ToolStripMenuItem1.Name = "spielstand3ToolStripMenuItem1";
            this.spielstand3ToolStripMenuItem1.Size = new System.Drawing.Size(137, 22);
            this.spielstand3ToolStripMenuItem1.Text = "Spielstand 3";
            this.spielstand3ToolStripMenuItem1.Click += new System.EventHandler(this.spielstand3ToolStripMenuItem1_Click);
            // 
            // lösungshilfeToolStripMenuItem
            // 
            this.lösungshilfeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.möglicheEingabenToolStripMenuItem,
            this.fehlerhafteEingabeToolStripMenuItem});
            this.lösungshilfeToolStripMenuItem.Name = "lösungshilfeToolStripMenuItem";
            this.lösungshilfeToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.lösungshilfeToolStripMenuItem.Text = "Lösungshilfe";
            // 
            // möglicheEingabenToolStripMenuItem
            // 
            this.möglicheEingabenToolStripMenuItem.Name = "möglicheEingabenToolStripMenuItem";
            this.möglicheEingabenToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.möglicheEingabenToolStripMenuItem.Text = "Mögliche Eingaben";
            this.möglicheEingabenToolStripMenuItem.Click += new System.EventHandler(this.möglicheEingabenToolStripMenuItem_Click);
            // 
            // fehlerhafteEingabeToolStripMenuItem
            // 
            this.fehlerhafteEingabeToolStripMenuItem.Name = "fehlerhafteEingabeToolStripMenuItem";
            this.fehlerhafteEingabeToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.fehlerhafteEingabeToolStripMenuItem.Text = "Fehlerhafte Eingabe";
            this.fehlerhafteEingabeToolStripMenuItem.Click += new System.EventHandler(this.fehlerhafteEingabeToolStripMenuItem_Click);
            // 
            // zurückToolStripMenuItem
            // 
            this.zurückToolStripMenuItem.Enabled = false;
            this.zurückToolStripMenuItem.Name = "zurückToolStripMenuItem";
            this.zurückToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.zurückToolStripMenuItem.Text = "Zurück";
            this.zurückToolStripMenuItem.Click += new System.EventHandler(this.zurückToolStripMenuItem_Click);
            // 
            // tippToolStripMenuItem
            // 
            this.tippToolStripMenuItem.Name = "tippToolStripMenuItem";
            this.tippToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.tippToolStripMenuItem.Text = "Tipp";
            this.tippToolStripMenuItem.Click += new System.EventHandler(this.tippToolStripMenuItem_Click);
            // 
            // lösenToolStripMenuItem
            // 
            this.lösenToolStripMenuItem.Name = "lösenToolStripMenuItem";
            this.lösenToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.lösenToolStripMenuItem.Text = "Lösen";
            this.lösenToolStripMenuItem.Click += new System.EventHandler(this.lösenToolStripMenuItem_Click);
            // 
            // anleitungToolStripMenuItem
            // 
            this.anleitungToolStripMenuItem.Name = "anleitungToolStripMenuItem";
            this.anleitungToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.anleitungToolStripMenuItem.Text = "Anleitung";
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(0, 540);
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.Size = new System.Drawing.Size(553, 92);
            this.log.TabIndex = 2;
            this.log.Text = "";
            this.log.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // UI_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(553, 632);
            this.Controls.Add(this.log);
            this.Controls.Add(this.spielfeld);
            this.Controls.Add(this.menuStrip1);
            this.Name = "UI_Form";
            this.Text = "Sudoku";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.spielfeld)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox spielfeld;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem spielToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem neuesSpielToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leichtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mittelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem schwerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spielstandLadenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spielstandSpeichernToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lösungshilfeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem anleitungToolStripMenuItem;
        private System.Windows.Forms.RichTextBox log;
        private System.Windows.Forms.ToolStripMenuItem schwerstenDerWeltToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zurückToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tippToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lösenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem möglicheEingabenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fehlerhafteEingabeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spielstand1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spielstand2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spielstand3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spielstand1ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem spielstand2ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem spielstand3ToolStripMenuItem1;
    }
}

