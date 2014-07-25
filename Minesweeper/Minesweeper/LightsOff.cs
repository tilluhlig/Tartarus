using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    internal class LightsOff
    {
        private static int Schritte = 0; // wieviel Zeit ist bereits verstrichen
        private static int Breite = 0; //wieviele Felder
        private static int Hoehe = 0; // wieviele Felder

        private static ImageList Pictures; // Die Bilderliste für die Felder (0-8, Minenbilder, Felderbilder)
        private static Label Schrittanzeige; // Das Label für die Zeitanzeige

        public static void ErhoeheSchritte()
        {
            Schritte++;
            Schrittanzeige.Text = Schritte.ToString().PadLeft(3, '0');
        }

        public static void ResetSchritte()
        {
            // Zeit zurücksetzen
            Schritte = -1;
            ErhoeheSchritte();
        }

        private static bool[] Spielfeld; // speichert wo Minen sind und wo nich  true==mine.... false==keine Mine
        private static PictureBox[] Bilder; // die Bilder des Spielfelds

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

        public static void StopSpiel()
        {
            if (Bilder == null) return;

            // alle Felder sichtbar machen
            KillSpiel();
        }

        private static void PrüfeSieg()
        {
            // Prüfe ob es noch ein Feld gibt, das man noch anklicken könnte (ohne zu verlieren)
            for (int i = 0; i < Breite * Hoehe; i++)
            {
                if (Spielfeld[i]) return;
            }

            // Es wurde gewonnen, also beende das Spiel
            StopSpiel();
        }

        private static Image GetBild(int position)
        {
            // Wähle das richtige Bild für ein Feld
            if (Spielfeld[position])
            {
                return Pictures.Images[0];
            }

            return Pictures.Images[2];
        }

        private static void ChangeBild(int position)
        {
            // Wähle das richtige Bild für ein Feld
            if (Spielfeld[position])
            {
                Spielfeld[position] = false;
            }
            else
            {
                Spielfeld[position] = true;
            }
            Bilder[position].Image = GetBild(position);
        }

        private static void Bilder_Click(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < Breite * Hoehe; i++)
            {
                if (sender == Bilder[i])
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        ErhoeheSchritte();
                        ChangeBild(i);

                        // Nun befreie 0er Felder
                        int x = i % Breite;
                        int y = i / Breite;

                        if (x > 0) ChangeBild((x - 1) + (y + 0) * Breite);
                        if (x < Breite - 1) ChangeBild((x + 1) + (y + 0) * Breite);

                        if (y > 0) ChangeBild((x - 0) + (y - 1) * Breite);
                        if (y < Hoehe - 1) ChangeBild((x + 0) + (y + 1) * Breite);
                    }
                    PrüfeSieg();
                    break;
                }
            }
        }

        public static void InitSpielfeld(int _Breite, int _Hoehe, Form1 frm, PictureBox Zeichenflaeche, ImageList _Pictures, Label _Schrittanzeige)
        {
            Pictures = _Pictures;
            Schrittanzeige = _Schrittanzeige;

            // lösche das alte zeug
            ResetSchritte();
            if (Bilder != null)
            {
                for (int i = 0; i < Breite * Hoehe; i++)
                {
                    Bilder[i].Dispose();
                }
            }

            Breite = _Breite;
            Hoehe = _Hoehe;
            Spielfeld = new bool[Breite * Hoehe];
            Bilder = new PictureBox[Breite * Hoehe];

            // Minen setzen
            Random rnd = new Random();
            int anz = (int)Math.Sqrt(Breite * Hoehe);
            for (int i = 0; i < anz; i++)
            {
                int x;
                do
                {
                    x = rnd.Next(0, Breite * Hoehe);
                } while (Spielfeld[x] == true);
                Spielfeld[x] = true;
            }

            // die Felder, die man anklicken kann initialisieren
            for (int i = 0; i < Breite * Hoehe; i++)
            {
                Bilder[i] = new PictureBox();
                Bilder[i].Parent = frm;
                Bilder[i].Height = 32;
                Bilder[i].Width = 32;
                Bilder[i].Image = GetBild(i);
                Bilder[i].Top = ((int)i / Breite) * 32 + Zeichenflaeche.Top;
                Bilder[i].Left = (i % Breite) * 32 + Zeichenflaeche.Left;
                Bilder[i].MouseClick += Bilder_Click;
                Bilder[i].Hide();
            }

            // Fenster an Spielfeld anpassen
            int disty = frm.Height - Zeichenflaeche.Height;
            int distx = frm.Width - Zeichenflaeche.Width;
            frm.Height = ((int)(Hoehe * Breite) / Breite) * 32 + disty;
            frm.Width = ((int)(Breite) * 32) + distx;
            if (frm.Width < 491) frm.Width = 491;

            for (int i = 0; i < Breite * Hoehe; i++)
            {
                Bilder[i].Show();
            }
        }
    }
}