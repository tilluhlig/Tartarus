using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public static class Minesweeper
    {
        private static int Zeit = 0; // wieviel Zeit ist bereits verstrichen
        private static int Breite = 0; //wieviele Felder
        private static int Hoehe = 0; // wieviele Felder
        public static int Minen = 0; // Wieviele Minen hat das Spiel?

        private static Timer Zeitgeber; // der Zeitgeber für das Sekundenzählen
        private static ImageList Pictures; // Die Bilderliste für die Felder (0-8, Minenbilder, Felderbilder)
        private static Label Minenanzeige; // Das Label für die anzeige der noch nicht markierten Minen
        private static Label Zeitanzeige; // Das Label für die Zeitanzeige
        private static CheckBox Schwer;

        public static int ErhoeheZeit()
        {
            // um eine Sekunde erhöhen
            return ++Zeit;
        }

        public static void ResetZeit()
        {
            // Zeit zurücksetzen
            Zeit = 0;
            if (Zeitanzeige != null) Zeitanzeige.Text = Zeit.ToString();
        }

        public static void ResetMinen()
        {
            // Zeit zurücksetzen
            Minen = 0;
            if (Minenanzeige != null) Minenanzeige.Text = Minen.ToString();
        }

        public static void AddMinenanzeige(int wert)
        {
            // erhöht den Zähler für markierte Minen um einen "wert"
            Minen += wert;
            if (Minen > 0)
            {
                Minenanzeige.Text = (Minesweeper.Minen).ToString().PadLeft(3, '0');
            }
            else
                Minenanzeige.Text = (0).ToString().PadLeft(3, '0');
        }

        private static bool[] Spielfeld; // speichert wo Minen sind und wo nich  true==mine.... false==keine Mine
        private static PictureBox[] Bilder; // die Bilder des Spielfelds
        private static int[] GesetzteBilder; // speichert.. 0==normal, 1==Feld wurde gedrückt, 2==markierung für vermutete Mine gesetzt
        private static int[] Zahlen; // speichert die Zahlen ("wieviele Minen im Umkreis")
        private static String[] Text;

        public static void KillSpiel()
        {
            if (Bilder == null) return;

            // alle Felder zerstören
            for (int i = 0; i < Breite * Hoehe; i++)
            {
                if (Bilder[i] == null) continue;
                Bilder[i].Dispose();
            }
        }

        public static void StopSpiel(bool click)
        {
            if (Zeitgeber != null) Zeitgeber.Enabled = false; // Timer abschalten
            if (Bilder == null) return;

            // alle Felder sichtbar machen
            for (int i = 0; i < Breite * Hoehe; i++)
            {
                if (Bilder[i] == null) continue;
                if (GesetzteBilder[i] == 1) continue; // wenn bereits sichtbar, aufdecken nicht nötig

                GesetzteBilder[i] = 1; // als sichtbar markieren

                // das richtige Bild setzen
                Bilder[i].Image = GetBild(i, click);
            }
        }

        private static void PrüfeSieg()
        {
            // Prüfe ob es noch ein Feld gibt, das man noch anklicken könnte (ohne zu verlieren)
            for (int i = 0; i < Breite * Hoehe; i++)
            {
                if (GesetzteBilder[i] == 0 && !Spielfeld[i]) return;
            }

            // Es wurde gewonnen, also beende das Spiel
            StopSpiel(false);
        }

        private static int over = -1; // selektiertes Feld

        private static void Bilder_MouseMove(object sender, MouseEventArgs e)
        {
            // Die Maus wird übers Spielfeld bewegt
            for (int i = 0; i < Breite * Hoehe; i++)
            {
                if (sender == Bilder[i])
                {
                    if (GesetzteBilder[i] == 0)
                    {
                        // Feld selektiert malen
                        Bilder[i].Image = Pictures.Images[1];

                        // altes selektiertes Feld wieder normale machen
                        if (over >= 0 && over != i && GesetzteBilder[over] != 2)
                        {
                            Bilder[over].Image = Pictures.Images[0];
                        }

                        // Feld selektieren
                        over = i;
                    }
                    break;
                }
            }
        }

        private static void sekunde_Tick(object sender, EventArgs e)
        {
            // Es ist mal wieder eine Sekunde vorbei
            if (Zeit > 999) Zeit = 999;
            Zeitanzeige.Text = ErhoeheZeit().ToString().PadLeft(3, '0');
        }

        private static Image GetBild(int position, bool click)
        {
            // Wähle das richtige Bild für ein Feld
            if (click && Spielfeld[position])
            {
                // du hast auf eine Mine gedrückt
                StopSpiel(click); // verloren
                return Pictures.Images[12];
            }
            else
                if (!click && Spielfeld[position])
                {
                    return Pictures.Images[13];
                }

            System.Drawing.Font fntFont = new Font("Courier New", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 178, false);
            Bitmap temp = new Bitmap(Pictures.Images[2]);
            Graphics g = Graphics.FromImage(temp);
            if (Zahlen[position] > 0)
            {
                String q = Zahlen[position].ToString();
                Color[] Farbe = { Color.Black, Color.Blue, Color.Red, Color.Green, Color.Indigo, Color.Orange, Color.Gold, Color.Coral };
                SolidBrush blackBrush;
                if (Schwer.Checked)
                {
                    q = Text[position];
                    if (q.Length > 1)
                    {
                        blackBrush = new SolidBrush(Color.Purple);
                    }
                    else
                        blackBrush = new SolidBrush(Farbe[Zahlen[position] - 1]);
                }
                else
                    blackBrush = new SolidBrush(Farbe[Zahlen[position] - 1]);

                g.DrawString(q, fntFont, blackBrush, new PointF(16 - q.Length * 5, 16 - 7));
                blackBrush.Dispose();
            }
            return temp;
        }

        private static void Bilder_Click(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < Breite * Hoehe; i++)
            {
                if (sender == Bilder[i])
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        if (GesetzteBilder[i] == 0)
                        {
                            // setzte die markierung aufs Feld (ich glaube da ist eine Mine)
                            GesetzteBilder[i] = 2;
                            Bilder[i].Image = Pictures.Images[11];
                            AddMinenanzeige(-1);
                        }
                        else
                            if (GesetzteBilder[i] == 2)
                            {
                                // entferne die markierung vom Feld (ich glaube da ist doch keine Mine)
                                GesetzteBilder[i] = 0;
                                Bilder[i].Image = Pictures.Images[0];
                                AddMinenanzeige(+1);
                            }
                    }
                    else
                        if (e.Button == MouseButtons.Left)
                        {
                            // wurde dieses Feld bereits angeklickt?
                            if (GesetzteBilder[i] == 0 || GesetzteBilder[i] == 2)
                            {
                                // setze das neue Bild
                                GesetzteBilder[i] = 1;
                                Bilder[i].Image = GetBild(i, true);
                                if (i == over) over = -1;

                                // Nun befreie 0er Felder
                                int x = i % Breite;
                                int y = i / Breite;
                                if (Zahlen[(x) + (y) * Breite] == 0)
                                {
                                    if (x > 0 && Zahlen[(x - 1) + (y + 0) * Breite] != -1) Bilder_Click(Bilder[(x - 1) + (y + 0) * Breite], e);
                                    if (x < Breite - 1 && Zahlen[(x + 1) + (y + 0) * Breite] != -1) Bilder_Click(Bilder[(x + 1) + (y + 0) * Breite], e);

                                    if (y > 0 && Zahlen[(x) + (y - 1) * Breite] != -1) Bilder_Click(Bilder[(x - 0) + (y - 1) * Breite], e);
                                    if (y < Hoehe - 1 && Zahlen[(x) + (y + 1) * Breite] != -1) Bilder_Click(Bilder[(x + 0) + (y + 1) * Breite], e);

                                    // sollen auch diagonale frei gemacht werden???
                                    if (x > 0 && y > 0 && Zahlen[(x - 1) + (y - 1) * Breite] >= 0) Bilder_Click(Bilder[(x - 1) + (y - 1) * Breite], e);
                                    if (x > 0 && y < Hoehe - 1 && Zahlen[(x - 1) + (y + 1) * Breite] >= 0) Bilder_Click(Bilder[(x - 1) + (y + 1) * Breite], e);

                                    if (x < Breite - 1 && y > 0 && Zahlen[(x + 1) + (y - 1) * Breite] >= 0) Bilder_Click(Bilder[(x + 1) + (y - 1) * Breite], e);
                                    if (x < Breite - 1 && y < Hoehe - 1 && Zahlen[(x + 1) + (y + 1) * Breite] >= 0) Bilder_Click(Bilder[(x + 1) + (y + 1) * Breite], e);
                                }
                            }
                        }
                    PrüfeSieg();
                    break;
                }
            }
        }

        public static void InitSpielfeld(int _Minen, int _Breite, int _Hoehe, Form1 frm, PictureBox Zeichenflaeche, ImageList _Pictures, Label _Minenanzeige, Label _Zeitanzeige, CheckBox _Schwer)
        {
            // lösche das alte zeug
            Minesweeper.ResetZeit();
            if (Bilder != null)
            {
                for (int i = 0; i < Breite * Hoehe; i++)
                {
                    Bilder[i].Dispose();
                }
            }

            Schwer = _Schwer;
            Pictures = _Pictures;
            Breite = _Breite;
            Hoehe = _Hoehe;
            Minen = _Minen;
            Minenanzeige = _Minenanzeige;
            AddMinenanzeige(0);
            Zeitanzeige = _Zeitanzeige;
            Spielfeld = new bool[Breite * Hoehe];

            Bilder = new PictureBox[Breite * Hoehe];
            GesetzteBilder = new int[Breite * Hoehe];
            Zahlen = new int[Breite * Hoehe];
            Text = new String[Breite * Hoehe];

            // die Felder, die man anklicken kann initialisieren
            for (int i = 0; i < Breite * Hoehe; i++)
            {
                GesetzteBilder[i] = 0;
                Bilder[i] = new PictureBox();
                Bilder[i].Parent = frm;
                Bilder[i].Height = 32;
                Bilder[i].Width = 32;
                Bilder[i].Image = Pictures.Images[0];
                Bilder[i].Top = ((int)i / Breite) * 32 + Zeichenflaeche.Top;
                Bilder[i].Left = (i % Breite) * 32 + Zeichenflaeche.Left;
                Bilder[i].MouseMove += Bilder_MouseMove;
                Bilder[i].MouseClick += Bilder_Click;
                Bilder[i].Hide();
            }

            // Minen setzen
            Random rnd = new Random();
            for (int i = 0; i < Minen; i++)
            {
                int x;
                do
                {
                    x = rnd.Next(0, Breite * Hoehe);
                } while (x / Breite == 0 || x / Breite == Hoehe - 1 || x % Breite == 0 || x % Breite == Breite - 1 || Spielfeld[x] == true);
                Spielfeld[x] = true;
            }

            // Zahlen bestimmen
            for (int i = 0; i < Breite; i++)
                for (int b = 0; b < Hoehe; b++)
                {
                    int pos = i + b * Breite;
                    if (Spielfeld[pos])
                    {
                        Zahlen[pos] = -1;
                    }
                    else
                    {
                        int anz = 0;
                        if (i > 0 && Spielfeld[(i - 1) + b * Breite]) anz++;
                        if (i < Breite - 1 && Spielfeld[(i + 1) + b * Breite]) anz++;

                        if (b > 0 && Spielfeld[(i) + (b - 1) * Breite]) anz++;
                        if (b < Hoehe - 1 && Spielfeld[(i) + (b + 1) * Breite]) anz++;

                        if (i > 0 && b > 0 && Spielfeld[(i - 1) + (b - 1) * Breite]) anz++;
                        if (i > 0 && b < Hoehe - 1 && Spielfeld[(i - 1) + (b + 1) * Breite]) anz++;

                        if (i < Breite - 1 && b > 0 && Spielfeld[(i + 1) + (b - 1) * Breite]) anz++;
                        if (i < Breite - 1 && b < Hoehe - 1 && Spielfeld[(i + 1) + (b + 1) * Breite]) anz++;

                        Zahlen[pos] = anz;
                        if (Schwer.Checked)
                        {
                            if (anz == 0)
                            {
                                Text[pos] = "";
                            }
                            else
                            {
                                int temp = rnd.Next(1, anz + 1);
                                int set = rnd.Next(0, 3);
                                if (set > 0)
                                {
                                    Text[pos] = anz.ToString();
                                }
                                else
                                {
                                    int begin = anz;
                                    if (temp == anz) begin = anz + 1;
                                    int end = anz + 1 + 2;
                                    if (end > 9) end = 9;
                                    int temp2 = rnd.Next(begin, end);
                                    Text[pos] = temp.ToString() + "-" + temp2.ToString();
                                }
                            }
                        }
                    }
                }

            // äußeren Ring sichtbar machen
            for (int i = 0; i < Breite * Hoehe; i++)
            {
                if (i / Breite == 0 || i / Breite == Hoehe - 1 || i % Breite == 0 || i % Breite == Breite - 1)
                {
                    // GesetzteBilder[i] = 1; // als sichtbar markieren

                    // das richtige Bild setzen
                    MouseEventArgs temp = new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0);
                    Bilder_Click(Bilder[i], temp);
                }
            }

            // Fenster an Spielfeld anpassen
            int disty = frm.Height - Zeichenflaeche.Height;
            int distx = frm.Width - Zeichenflaeche.Width;
            frm.Height = ((int)(Hoehe * Breite) / Breite) * 32 + disty;
            frm.Width = ((int)(Breite) * 32) + distx;

            // Den "Wieviel Sekunden wird Gespielt" Timer initialisieren
            if (Zeitgeber != null) Zeitgeber.Dispose();
            Zeitgeber = new Timer();
            Zeitgeber.Tick += sekunde_Tick;
            Zeitgeber.Interval = 1000;
            Zeitgeber.Enabled = true;

            for (int i = 0; i < Breite * Hoehe; i++)
            {
                Bilder[i].Show();
            }
        }
    }
}