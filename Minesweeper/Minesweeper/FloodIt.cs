using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public static class FloodIt
    {
        #region Fields

        public static int Breite = 0;

        //wieviele Felder
        public static int Hoehe = 0;

        public static int[] Spielfeld;
        private static PictureBox[] Bilder;

        // wieviele Felder
        private static int Farbe = 1;

        // die Bilder des Spielfelds
        private static PictureBox[] Farben;

        // 0 = aufgedeckt, 1-6 Farben
        private static bool[] fertsch;

        private static ImageList Pictures;

        // Die Bilderliste für die Felder (0-8, Minenbilder, Felderbilder)
        private static Label Schrittanzeige;

        private static int Schritte;

        #endregion Fields

        #region Methods

        public static int ErhoeheSchritte()
        {
            // um einen Schritt erhöhen
            Schritte++;
            Schrittanzeige.Text = Schritte.ToString();
            return Schritte;
        }

        public static void InitSpielfeld(int _Breite, int _Hoehe, Form1 frm, PictureBox Zeichenflaeche,
            PictureBox Farbenflaeche, ImageList _Pictures, Label _Schrittanzeige, List<int> data)
        {
            // lösche das alte zeug
            if (Bilder != null)
            {
                for (int i = 0; i < Breite * Hoehe; i++)
                {
                    Bilder[i].Dispose();
                }
            }

            if (Farben != null)
            {
                for (int i = 0; i < 6; i++)
                {
                    Farben[i].Dispose();
                }
            }

            if (data != null)
            {
                _Breite = data[0];
                _Hoehe = data[1];
            }

            Pictures = _Pictures;
            Breite = _Breite;
            Hoehe = _Hoehe;
            Schrittanzeige = _Schrittanzeige;
            ResetSchritte();
            Spielfeld = new int[Breite * Hoehe];

            Bilder = new PictureBox[Breite * Hoehe];
            fertsch = new bool[Breite * Hoehe];
            Farben = new PictureBox[6];

            if (data == null)
            {
                var rnd = new Random();
                for (int i = 0; i < Hoehe * Breite; i++)
                {
                    Spielfeld[i] = rnd.Next(1, 7); // 1-6
                }
            }
            else
            {
                for (int i = 2; i < Hoehe * Breite + 2; i++)
                {
                    Spielfeld[i - 2] = data[i]; // 1-6
                }
            }

            Spielfeld[0] = 0;

            do
            {
                Spielfeld[0]++;
                if (Spielfeld[0] >= 7) Spielfeld[0] = 1;
            } while (Spielfeld[0] == Spielfeld[1] || Spielfeld[0] == Spielfeld[Breite]);
            Farbe = Spielfeld[0];
            Spielfeld[0] = 0;

            // die Felder, die man anklicken kann initialisieren
            for (int i = 0; i < Breite * Hoehe; i++)
            {
                Bilder[i] = new PictureBox();
                Bilder[i].Parent = frm;
                Bilder[i].Height = 32;
                Bilder[i].Width = 32;
                Bilder[i].Image = GetBild(i);
                Bilder[i].Top = (i / Breite) * 32 + Zeichenflaeche.Top;
                Bilder[i].Left = (i % Breite) * 32 + Zeichenflaeche.Left;
                Bilder[i].Hide();
            }
            // Fenster an Spielfeld anpassen
            int disty = frm.Height - Zeichenflaeche.Height;
            int distx = frm.Width - Zeichenflaeche.Width;
            frm.Height = (Hoehe * Breite / Breite) * 32 + disty;
            frm.Width = (Breite * 32) + distx;
            if (frm.Width < 491) frm.Width = 491;

            for (int i = 0; i < 6; i++)
            {
                Farben[i] = new PictureBox();
                Farben[i].Parent = frm;
                Farben[i].Height = 32;
                Farben[i].Width = 32;
                Farben[i].Image = Pictures.Images[i];
                Farben[i].Top = Farbenflaeche.Top + 10;
                Farben[i].Left = (i % Breite) * 32 + Bilder[0].Left;
                Farben[i].MouseClick += Bilder_Click;
                Farben[i].Hide();
            }

            for (int i = 0; i < Breite * Hoehe; i++)
            {
                Bilder[i].Show();
            }

            for (int i = 0; i < 6; i++)
            {
                Farben[i].Show();
            }
        }

        public static void KillSpiel()
        {
            if (Bilder == null) return;

            // alle Felder zerstören
            for (int i = 0; i < Breite * Hoehe; i++)
            {
                if (Bilder[i] == null) continue;
                Bilder[i].Dispose();
            }

            for (int i = 0; i < 6; i++)
            {
                if (Farben[i] == null) continue;
                Farben[i].Dispose();
            }
        }

        public static void ResetSchritte()
        {
            // Schritte zurücksetzen
            Schritte = 0;
            Schrittanzeige.Text = Schritte.ToString();
        }

        // die Bilder des Spielfelds
        public static void StopSpiel()
        {
            if (Bilder == null) return;
        }

        private static void Berechnen(int x, int y)
        {
            int pos = x + y * Breite;
            if (Spielfeld[pos] == 0)
            {
                Bilder[pos].Image = GetBild(pos);
                if (x > 0 && !fertsch[(x - 1) + y * Breite] &&
                    (Spielfeld[(x - 1) + y * Breite] == Farbe || Spielfeld[(x - 1) + (y) * Breite] == 0))
                {
                    int temp = (x - 1) + (y) * Breite;
                    fertsch[temp] = true;
                    Spielfeld[temp] = 0;
                    Berechnen(x - 1, y);
                }
                if (x < Breite - 1 && !fertsch[(x + 1) + y * Breite] &&
                    (Spielfeld[(x + 1) + y * Breite] == Farbe || Spielfeld[(x + 1) + (y) * Breite] == 0))
                {
                    int temp = (x + 1) + (y) * Breite;
                    fertsch[temp] = true;
                    Spielfeld[temp] = 0;
                    Berechnen(x + 1, y);
                }

                if (y > 0 && !fertsch[(x) + (y - 1) * Breite] &&
                    (Spielfeld[(x) + (y - 1) * Breite] == Farbe || Spielfeld[(x) + (y - 1) * Breite] == 0))
                {
                    int temp = (x) + (y - 1) * Breite;
                    fertsch[temp] = true;
                    Spielfeld[temp] = 0;
                    Berechnen(x, y - 1);
                }
                if (y < Hoehe - 1 && !fertsch[(x) + (y + 1) * Breite] &&
                    (Spielfeld[(x) + (y + 1) * Breite] == Farbe || Spielfeld[(x) + (y + 1) * Breite] == 0))
                {
                    int temp = (x) + (y + 1) * Breite;
                    fertsch[temp] = true;
                    Spielfeld[temp] = 0;
                    Berechnen(x, y + 1);
                }
            }
        }

        private static void Bilder_Click(object sender, MouseEventArgs e)
        {
            ErhoeheSchritte();
            for (int i = 0; i < 6; i++)
            {
                if (sender == Farben[i])
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        int oldFarbe = Farbe;
                        Farbe = i + 1;
                        Berechnen(0, 0);
                        for (int a = 0; a < Hoehe * Breite; a++) fertsch[a] = false;
                    }

                    PrüfeSieg();
                    break;
                }
            }
        }

        private static Image GetBild(int position)
        {
            if (Spielfeld[position] == 0)
            {
                return Pictures.Images[Farbe - 1];
            }
            return Pictures.Images[Spielfeld[position] - 1];
        }

        private static void PrüfeSieg()
        {
            // Prüfe ob es noch ein Feld gibt, das man noch anklicken könnte (ohne zu verlieren)
            for (int i = 0; i < Breite * Hoehe; i++)
            {
                if (Spielfeld[i] != 0) return;
            }

            // Es wurde gewonnen, also beende das Spiel
            StopSpiel();

            // Nun hochladen
            /*   if (HTTP.HTTP.Map != "")
               {
                   HTTP.HTTP.Result = Schritte.ToString();
                   HTTP.HTTP.Eingeben();
               }*/

            KillSpiel();
        }

        #endregion Methods

        // wieviel Zeit ist bereits verstrichen
        // Das Label für die Zeitanzeige
    }
}