using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form1 : Form
    {
        private String Spiel = "";

        public Form1()
        {
            //  HTTP.HTTP.SetLocal();

            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            /* if (File.Exists("name.txt"))
             {
                 StreamReader datei = new StreamReader("name.txt");
                 if (!datei.EndOfStream)
                 {
                     HTTP.HTTP.Spieler = datei.ReadLine();
                     if (HTTP.HTTP.Spieler.Length > 5) HTTP.HTTP.Spieler = HTTP.HTTP.Spieler.Substring(0, 5);
                     textBox1.Text = HTTP.HTTP.Spieler;
                 }
                 datei.Close();
             }*/

            //  comboBox2.SelectedIndex = 0;

            if (Program.Fehlertext != "")
            {
                label5.Text = Program.Fehlertext;
                label5.Left = this.Width / 2 - label5.Width / 2;
                label5.Show();

                button5.Left = this.Width / 2 - button5.Width / 2;
                button5.Show();
            }

            if (Program.Fehlertext2 != "")
            {
                label6.Text = Program.Fehlertext2;
                label6.Left = this.Width / 2 - label6.Width / 2;
                label6.Show();
            }
        }

        public void Insert(RichTextBox A, String Text, System.Drawing.Color Farbe)
        {
            Text = Text + "\n";
            A.AppendText(Text);
            A.Select(A.TextLength - (Text.Length), Text.Length);
            A.SelectionColor = Farbe;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int SWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            int SHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;

            // Initialisiere ein neues Spiel 9x9 mit 10 Minen
            if (Spiel == "Minesweeper")
            {
                Minesweeper.InitSpielfeld(80, 32, 16, this, pictureBox1, imageList1, label1, label2, checkBox1);
                label8.Left = 294; label8.Top = 4;
                label8.Text = "linke Maustaste = Feld aufdecken\n" +
                "rechte Maustaste = Feld als Mine vormerken\n";
                label8.Show();
            }
            else
                if (Spiel == "FloodIt")
                {
                    /*HTTP.HTTP.Map = comboBox1.Text;
                    String Datei = "Maps\\" + HTTP.HTTP.Map + ".map";
                    if (File.Exists(Datei))
                    {
                        StreamReader dat = new StreamReader(Datei);
                        List<int> list = new List<int>();
                        while (!dat.EndOfStream) list.Add(Convert.ToInt32(dat.ReadLine()));
                        dat.Close();
                        FloodIt.InitSpielfeld(16, 16, this, pictureBox1, pictureBox2, imageList2, label1, list);
                    }*/
                    FloodIt.InitSpielfeld(16, 16, this, pictureBox1, pictureBox2, imageList2, label1, null);

                    label8.Left = 294 - 75; label8.Top = 4;
                    label8.Text = "Erreichen Sie es mit möglichst wenig Zügen, das alle Felder die selbe Farbe besitzen,\n" +
                    "durch klicken auf die Farben unterhalb des Spielfeldes.\n" +
                    "Das Spiel beginnt mit der Berechnung oben Links. Es werden stets alle angrenzenden\n" +
                    "Felder, welche der Farbe entsprechen mit aufgenommen.";
                    label8.Show();
                }
                else
                    if (Spiel == "LightsOff")
                    {
                        LightsOff.InitSpielfeld(5, 5, this, pictureBox1, imageList1, label1);
                        label8.Left = button1.Left; label8.Top = button1.Top + button1.Height + 15;
                        label8.Text = "Versuchen Sie alle Felder durch klicken\n" +
                        "abzuschalten (grau). Wenn Sie auf ein Feld klicken,\n" +
                        "wechseln das Feld selbst und die 4\n" +
                        "umgebenden (Links, Rechts, Oben, Unten) ihre Farbe.\n";
                        label8.Show();
                    }
                    else
                        if (Spiel == "Tetris")
                        {
                            Tetris.InitSpielfeld(12, 22, this, pictureBox1, imageList2, label1, button1, comboBox2);

                            label8.Left = pictureBox1.Left;
                            label8.Top = ((int)(22) - 4) * 32 + pictureBox1.Top;
                            label8.Text = "Sie erhalten Punkte indem Sie eine Reihe\n" +
                            "des Spielfeldes komplett auffüllen.\n\n" +
                            "Tasten:\n" +
                            "Links,Rechts = Stein nach Links oder Rechts\n" +
                            "Hoch = Stein sofort auf den Boden legen\n" +
                            "Runter = Stein nach Unten bewegen\n" +
                            "Leertaste = Stein drehen";
                            label8.Show();

                            label9.Top = ((int)(22) - 4) * 32 + pictureBox1.Top + label8.Height / 2;
                            label9.Left = label8.Width + 50;
                            label9.Show();
                        }

            this.Left = SWidth / 2 - this.Width / 2;
            this.Top = SHeight / 2 - this.Height / 2;
        }

        private void ShowMinesweeper()
        {
            Spiel = "Minesweeper";
            this.Text = Spiel;
            label1.Show();
            label2.Show();
            label3.Hide();
            label4.Hide();
            button3.Hide();
            button2.Hide();
            button1.Show();
            textBox1.Hide();
            comboBox1.Hide();
            label9.Hide();
            richTextBox1.Hide();
            checkBox1.Show();
            pictureBox1.Left = richTextBox1.Left;
            pictureBox1.Width = this.Width - pictureBox1.Left - 20;
            pictureBox1.Height = this.Height - pictureBox1.Top - 40;
            this.Width = 491;
            this.Height = 348;
            Minesweeper.ResetZeit();
            Minesweeper.ResetMinen();
            FloodIt.StopSpiel();
            FloodIt.KillSpiel();
            LightsOff.KillSpiel();
        }

        private void ShowFloodIt()
        {
            Spiel = "FloodIt";
            this.Text = Spiel;
            label1.Show();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            button3.Hide();
            button2.Hide();
            button1.Show();
            textBox1.Hide();
            comboBox1.Hide();
            label9.Hide();
            richTextBox1.Hide();
            checkBox1.Hide();
            pictureBox1.Left = richTextBox1.Left + richTextBox1.Width + 10;
            pictureBox1.Width = this.Width - pictureBox1.Left - 20;
            pictureBox1.Height = this.Height - pictureBox1.Top - pictureBox2.Height - 40;
            this.Width = 491;
            this.Height = 348;
            Minesweeper.StopSpiel(false);
            Minesweeper.KillSpiel();
            Minesweeper.ResetMinen();
            Minesweeper.ResetZeit();
            FloodIt.StopSpiel();
            FloodIt.KillSpiel();
            LightsOff.KillSpiel();
        }

        private void ShowLightsOff()
        {
            Spiel = "LightsOff";
            this.Text = Spiel;
            label1.Show();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            button3.Hide();
            button2.Hide();
            button1.Show();
            textBox1.Hide();
            comboBox1.Hide();
            label9.Hide();
            richTextBox1.Hide();
            checkBox1.Hide();
            pictureBox1.Left = richTextBox1.Left;
            pictureBox1.Width = this.Width - pictureBox1.Left - 20;
            pictureBox1.Height = this.Height - pictureBox1.Top - 40;
            this.Width = 491;
            this.Height = 348;
            Minesweeper.StopSpiel(false);
            Minesweeper.KillSpiel();
            Minesweeper.ResetMinen();
            Minesweeper.ResetZeit();
            FloodIt.StopSpiel();
            FloodIt.KillSpiel();
            LightsOff.KillSpiel();
        }

        private void ShowTetris()
        {
            Spiel = "Tetris";
            this.Text = Spiel;
            label1.Show();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            button3.Hide();
            button2.Hide();
            button1.Show();
            textBox1.Hide();
            comboBox1.Hide();
            richTextBox1.Hide();
            checkBox1.Hide();
            pictureBox1.Left = richTextBox1.Left;
            pictureBox1.Width = this.Width - pictureBox1.Left - 20;
            pictureBox1.Height = this.Height - pictureBox1.Top - 40;
            this.Width = 491;
            this.Height = 348;
            Minesweeper.StopSpiel(false);
            Minesweeper.KillSpiel();
            Minesweeper.ResetMinen();
            Minesweeper.ResetZeit();
            FloodIt.StopSpiel();
            FloodIt.KillSpiel();
            LightsOff.KillSpiel();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            //  StreamWriter datei = new StreamWriter("name.txt");
            //HTTP.HTTP.Spieler = textBox1.Text;
            //  datei.Write(HTTP.HTTP.Spieler);
            //  datei.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /* richTextBox1.Clear();
             if (Spiel == "FloodIt")
             {
                 HTTP.HTTP.Map = comboBox1.Text;
                 List<String> list = HTTP.HTTP.Ausgeben();
                 if (!HTTP.HTTP.IsFailure(list))
                 {
                     // neues Spiel erstellen
                     for (int i = 3; i < list.Count; i++)
                     {
                         Insert(richTextBox1,  list[i].Trim(), System.Drawing.Color.Blue);
                     }
                 }
                 else
                     Insert(richTextBox1, HTTP.HTTP.Get_Meldung(list), System.Drawing.Color.Red);
             }*/
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //HTTP.HTTP.Map = comboBox1.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int SWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            int SHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;

            if (Spiel == "FloodIt")
            {
                //HTTP.HTTP.Map = "";
                FloodIt.InitSpielfeld(16, 16, this, pictureBox1, pictureBox2, imageList2, label1, null);

                label8.Left = 294 - 75; label8.Top = 4;
                label8.Text = "Erreichen Sie es mit möglichst wenig Zügen, das alle Felder die selbe Farbe besitzen,\n" +
                "durch klicken auf die Farben unterhalb des Spielfeldes.\n" +
                "Das Spiel beginnt mit der Berechnung oben Links. Es werden stets alle angrenzenden\n" +
                "Felder, welche der Farbe entsprechen mit aufgenommen.";
                label8.Show();
            }

            this.Left = SWidth / 2 - this.Width / 2;
            this.Top = SHeight / 2 - this.Height / 2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String[] Namen = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            for (int i = 0; i < 10; i++)
            {
                StreamWriter datei = new StreamWriter("Maps\\" + Namen[i] + ".map");
                FloodIt.InitSpielfeld(16, 16, this, pictureBox1, pictureBox2, imageList2, label1, null);
                datei.WriteLine(FloodIt.Breite.ToString());
                datei.WriteLine(FloodIt.Hoehe.ToString());
                for (int b = 0; b < FloodIt.Spielfeld.Count(); b++)
                {
                    datei.WriteLine(FloodIt.Spielfeld[b].ToString());
                }
                datei.Close();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label5.Hide();
            label6.Hide();
            label7.Hide();
            label8.Hide();
            button5.Hide();

            if (comboBox2.SelectedIndex == 0)
            {
                ShowMinesweeper();
                button1_Click(null, null);
            }
            else
                if (comboBox2.SelectedIndex == 1)
                {
                    ShowFloodIt();
                    button3_Click(null, null);
                }
                else
                    if (comboBox2.SelectedIndex == 2)
                    {
                        ShowLightsOff();
                        button1_Click(null, null);
                    }
                    else
                        if (comboBox2.SelectedIndex == 3)
                        {
                            ShowTetris();
                            button1_Click(null, null);
                        }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label5.Hide();
            label6.Hide();
            label7.Hide();
            label8.Hide();
            button5.Hide();
            Random rand = new Random();
            comboBox2.SelectedIndex = rand.Next(0, comboBox2.Items.Count);

            // Log einsenden
            if (File.Exists(Application.StartupPath + "\\log.txt"))
            {
                String Text = "";
                StreamReader Datei = new StreamReader(Application.StartupPath + "\\log.txt");
                while (!Datei.EndOfStream)
                {
                    Text = Text + Datei.ReadLine() + "\n";
                }
                Datei.Close();

                //    if (!HTTP.HTTP.IsFailure(HTTP.HTTP.Eingeben(Text)))
                File.Delete(Application.StartupPath + "\\log.txt");
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Tetris.KillSpiel();
            comboBox2.SelectedIndex = 0;
            label9.Hide();
        }
    }
}