using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Minesweeper
{
    internal class Feld
    {
        #region Fields

        public int Art = 0; // 0==nichts,  1-7 Bausteine mit entsprechender Farbe
        public bool Bewegung = false;

        #endregion Fields

        // false==fest, keine Bewegung    true==in Bewegung, fällt noch
    }

    internal class Tetris
    {
        #region Fields

        private static Button A;

        // unwichtig
        private static ComboBox B;

        private static PictureBox[] Bilder;
        private static int Breite;
        private static bool DownVerändert = true;
        private static int Geschw = 500;

        //wieviele Felder
        private static int Hoehe;

        // die Bilder des Spielfelds
        private static bool modus;

        private static int nextStein;
        private static ImageList Pictures;
        private static readonly Random rnd = new Random();
        private static Feld[] Spielfeld;

        // wieviele Felder
        // Geschw in millisekunden
        private static int Warte;

        private static int Zeilen; // wieviel Zeit ist bereits verstrichen

        // Steine bis geschw erhöht wird
        // Die Bilderliste für die Felder (0-8, Minenbilder, Felderbilder)
        private static Label Zeilenanzeige; // Das Label für die Zeitanzeige

        // unwichtig
        private static Timer Zeitgeber;

        #endregion Fields

        #region Methods

        public static void ErhoeheZeilen()
        {
            Zeilen++;
            Zeilenanzeige.Text = Zeilen.ToString().PadLeft(3, '0');
        }

        public static void InitSpielfeld(int _Breite, int _Hoehe, Form1 frm, PictureBox Zeichenflaeche,
            ImageList _Pictures, Label _Zeilenanzeige, Button _A, ComboBox _B)
        {
            Pictures = _Pictures;
            Zeilenanzeige = _Zeilenanzeige;
            A = _A;
            B = _B;

            // lösche das alte zeug
            ResetSchritte();
            if (Bilder != null)
            {
                for (int i = 0; i < Breite*Hoehe; i++)
                {
                    Bilder[i].Dispose();
                }
            }

            Breite = _Breite;
            Hoehe = _Hoehe;
            Spielfeld = new Feld[Breite*Hoehe];
            for (int i = 0; i < Spielfeld.Count(); i++) Spielfeld[i] = new Feld();
            Bilder = new PictureBox[Breite*Hoehe + 8];

            // die Felder, die man anklicken kann initialisieren
            for (int i = 0; i < Breite*Hoehe; i++)
            {
                Bilder[i] = new PictureBox();
                Bilder[i].Parent = frm;
                Bilder[i].Height = 32;
                Bilder[i].Width = 32;
                Bilder[i].Image = GetBild(i);
                Bilder[i].Top = (i/Breite - 4)*32 + Zeichenflaeche.Top;
                Bilder[i].Left = (i%Breite)*32 + Zeichenflaeche.Left;
                Bilder[i].Hide();
            }

            for (int i = 0; i < 4; i++)
            {
                Bilder[i + Breite*Hoehe] = new PictureBox();
                Bilder[i + Breite*Hoehe].Parent = frm;
                Bilder[i + Breite*Hoehe].Height = 32;
                Bilder[i + Breite*Hoehe].Width = 32;
                Bilder[i + Breite*Hoehe].Image = GetBild(i);
                Bilder[i + Breite*Hoehe].Top = 0;
                Bilder[i + Breite*Hoehe].Left = (Breite - 4)*32 + (i%Breite)*32 + Zeichenflaeche.Left;
                Bilder[i + Breite*Hoehe].Hide();
            }

            for (int i = 0; i < 4; i++)
            {
                Bilder[i + Breite*Hoehe + 4] = new PictureBox();
                Bilder[i + Breite*Hoehe + 4].Parent = frm;
                Bilder[i + Breite*Hoehe + 4].Height = 32;
                Bilder[i + Breite*Hoehe + 4].Width = 32;
                Bilder[i + Breite*Hoehe + 4].Image = GetBild(i);
                Bilder[i + Breite*Hoehe + 4].Top = 32;
                Bilder[i + Breite*Hoehe + 4].Left = (Breite - 4)*32 + (i%Breite)*32 + Zeichenflaeche.Left;
                Bilder[i + Breite*Hoehe + 4].Hide();
            }

            frm.KeyDown += KeyDown;
            frm.KeyUp += KeyUp;

            // Fenster an Spielfeld anpassen
            int disty = frm.Height - Zeichenflaeche.Height;
            int distx = frm.Width - Zeichenflaeche.Width;
            frm.Height = (Hoehe*Breite/Breite)*32 + disty;
            frm.Width = (Breite*32) + distx;
            if (frm.Width < 491) frm.Width = 491;

            // Den "Wieviel Sekunden wird Gespielt" Timer initialisieren
            if (Zeitgeber != null) Zeitgeber.Dispose();
            Zeitgeber = new Timer();
            Zeitgeber.Tick += sekunde_Tick;
            Zeitgeber.Interval = Geschw;
            Zeitgeber.Enabled = true;

            // initialisiere Startwerte
            Geschw = 500;
            Warte = 15;

            for (int i = Breite*4; i < Breite*Hoehe + 8; i++)
            {
                Bilder[i].Show();
            }

            nextStein = rnd.Next(0, 7);

            // unwichtig
            A.Enabled = false;
            B.Enabled = false;
            A.Hide();
            B.Hide();
        }

        public static void KillSpiel()
        {
            if (Bilder == null) return;

            // alle Felder zerstören
            for (int i = 0; i < Bilder.Count(); i++)
            {
                if (Bilder[i] == null) continue;
                Bilder[i].Dispose();
            }

            // Form wieder herstellen
            A.Enabled = true;
            B.Enabled = true;
            A.Show();
            B.Show();
        }

        public static void ResetSchritte()
        {
            // Zeilen zurücksetzen
            Zeilen = -1;
            ErhoeheZeilen();
        }

        public static void StopSpiel()
        {
            if (Bilder == null) return;
            KillSpiel();
        }

        private static void BewegeFelder(bool sorte)
        {
            // sorte false==normales fallen(aufrücken) ,  true==objekt fällt
            if (!sorte)
            {
                for (int i = Hoehe - 1; i >= 0; i--)
                {
                    for (int b = 0; b < Breite; b++)
                    {
                        int pos = i*Breite + b;
                        // jedes Element fällt einzeln
                        if (Spielfeld[pos].Bewegung)
                        {
                            if (Spielfeld[(i + 1)*Breite + b].Art > 0 || i == Hoehe - 1)
                            {
                                // unabhängiger stein stoppt
                                Spielfeld[pos].Bewegung = false;
                            }
                            else
                            {
                                // unabhängiger stein fällt weiter
                                Spielfeld[(i + 1)*Breite + b].Art = Spielfeld[pos].Art;
                                Spielfeld[pos].Art = 0;
                                Spielfeld[pos].Bewegung = false;
                                Spielfeld[(i + 1)*Breite + b].Bewegung = true;
                                Bilder[pos].Image = GetBild(pos);
                                Bilder[(i + 1)*Breite + b].Image = GetBild((i + 1)*Breite + b);
                            }
                        }
                    }
                }
            }
            else
            {
                // kollision eines Steins???
                bool found = false;
                for (int i = Hoehe - 1; i >= 0 && !found; i--)
                {
                    for (int b = 0; b < Breite && !found; b++)
                    {
                        int pos = i*Breite + b;
                        if (Spielfeld[pos].Art > 0 && Spielfeld[pos].Bewegung)
                        {
                            if (i == Hoehe - 1 ||
                                (Spielfeld[(i + 1)*Breite + b].Art > 0 && !Spielfeld[(i + 1)*Breite + b].Bewegung))
                            {
                                found = true; // es gibt eine Kollision
                            }
                        }
                    }
                }

                for (int i = Hoehe - 1; i >= 0; i--)
                {
                    for (int b = 0; b < Breite; b++)
                    {
                        int pos = i*Breite + b;
                        // jedes Element fällt einzeln
                        if (Spielfeld[pos].Bewegung)
                        {
                            if (found)
                            {
                                Spielfeld[pos].Bewegung = false; // wenn kollision, dann den Stein anhalten
                            }
                            else
                            {
                                if (i < Hoehe - 1)
                                {
                                    // ansonsten den Stein weiter fallen lassen
                                    Spielfeld[(i + 1)*Breite + b].Art = Spielfeld[pos].Art;
                                    Spielfeld[(i + 1)*Breite + b].Bewegung = true;
                                    Spielfeld[pos].Art = 0;
                                    Spielfeld[pos].Bewegung = false;
                                    Bilder[pos].Image = GetBild(pos);
                                    Bilder[(i + 1)*Breite + b].Image = GetBild((i + 1)*Breite + b);
                                }
                            }
                        }
                    }
                }

                if (found)
                {
                    // wenn ein Stein angehalten wurde, könnte er ja eine Zeile voll gemacht haben
                    PrüfeZeilen();
                }
            }
        }

        private static void Drehe()
        {
            // Dreht das auf dem Spielfeld befindliche (in bewegung) Objekt

            // bestimme erstmal das Objekt, was da aufm Feld herumschwirrt
            var Objekt = new List<Point>();
            var ObjektNeu = new List<Point>();
            int Farbe = 1;
            for (int i = Hoehe - 2; i >= 0; i--)
            {
                for (int b = Breite - 1; b >= 0; b--)
                {
                    int pos = i*Breite + b;
                    if (Spielfeld[pos].Art > 0 && Spielfeld[pos].Bewegung)
                    {
                        Objekt.Add(new Point(b, i));
                        Farbe = Spielfeld[pos].Art;
                    }
                }
            }

            if (Objekt.Count == 0) return;

            // Mittelpunkt des Objektes
            int x = 0;
            int y = 0;
            for (int i = 0; i < Objekt.Count; i++)
            {
                x += Objekt[i].X;
                y += Objekt[i].Y;
            }
            var Mitte = new Point((int) Math.Round((decimal) x/Objekt.Count, 0),
                (int) Math.Round((decimal) y/Objekt.Count));

            // Drehe Objekt
            for (int i = 0; i < Objekt.Count; i++)
            {
                var Dist = new Point(Objekt[i].X - Mitte.X, Objekt[i].Y - Mitte.Y);
                ObjektNeu.Add(new Point(Mitte.X - Dist.Y, Mitte.Y + Dist.X));
            }

            // kann ich das neue Objekt setzen?
            for (int i = 0; i < ObjektNeu.Count; i++)
            {
                if (ObjektNeu[i].Y < 0 || ObjektNeu[i].Y >= Hoehe) return;
                if (ObjektNeu[i].X < 0 || ObjektNeu[i].X >= Breite) return;
                int pos = ObjektNeu[i].Y*Breite + ObjektNeu[i].X;
                if (Spielfeld[pos].Art > 0 && !Spielfeld[pos].Bewegung) return;
            }

            // lösche altes Objekt
            for (int i = 0; i < Objekt.Count; i++)
            {
                int pos = Objekt[i].Y*Breite + Objekt[i].X;
                Spielfeld[pos].Art = 0;
                Spielfeld[pos].Bewegung = false;
                Bilder[pos].Image = GetBild(pos);
            }

            // setze neues Objekt
            for (int i = 0; i < ObjektNeu.Count; i++)
            {
                int pos = ObjektNeu[i].Y*Breite + ObjektNeu[i].X;
                Spielfeld[pos].Art = Farbe;
                Spielfeld[pos].Bewegung = true;
                Bilder[pos].Image = GetBild(pos);
            }
        }

        private static Image GetBild(int position)
        {
            // Wähle das richtige Bild für ein Feld
            if (Spielfeld[position].Art > 0)
            {
                return Pictures.Images[Spielfeld[position].Art - 1]; // auf dem Feld ist ein Baustein
            }

            return Pictures.Images[7]; // auf dem Feld ist kein Baustein
        }

        private static bool GibtEsBewegung()
        {
            // Prüfe, ob es auf dem Spielfeld noch einen Baustein gibt, der nich "fest" ist
            for (int i = 0; i < Hoehe; i++)
            {
                for (int b = 0; b < Breite; b++)
                {
                    int pos = i*Breite + b;
                    if (Spielfeld[pos].Art > 0 && Spielfeld[pos].Bewegung)
                    {
                        // ja, den gibt es!!!
                        return true;
                    }
                }
            }
            return false;
        }

        private static void KeyDown(object sender, KeyEventArgs e)
        {
            if (modus)
            {
                if (e.KeyCode == Keys.Left)
                {
                    Verschiebe_Links();
                }
                else if (e.KeyCode == Keys.Right)
                {
                    Verschiebe_Rechts();
                }
                else if (e.KeyCode == Keys.Down && DownVerändert)
                {
                    if (Geschw > 25)
                    {
                        Zeitgeber.Interval = 25;
                    }
                    DownVerändert = false; // warte, bis er die taste ma wieder los lässt
                }
            }
        }

        // damit man erst neu drücken muss, um die geschwindigkeit der steine zu erhöhen (Keys.Down)
        private static void KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                Zeitgeber.Interval = Geschw;
                DownVerändert = true;
            }

            if (modus)
            {
                if (e.KeyCode == Keys.Up)
                {
                    // lässt den Stein runterfallen
                    Zeitgeber.Enabled = false;
                    do
                    {
                        BewegeFelder(true);
                    } while (GibtEsBewegung());
                    Zeitgeber.Enabled = true;
                }
                else if (e.KeyCode == Keys.Space)
                {
                    // drehe den Stein
                    Drehe();
                }
            }
        }

        private static void NeuerStein()
        {
            // hier sind die Steine definiert (0==nichts, 1==Baustein)
            int[,] data =
            {
                {0, 1, 1, 0, 0, 1, 1, 0}, // O
                {1, 0, 0, 0, 1, 1, 1, 0}, // J
                {0, 0, 0, 1, 0, 1, 1, 1}, // L
                {0, 0, 0, 0, 1, 1, 1, 1}, // I
                {0, 0, 1, 1, 0, 1, 1, 0}, // S
                {0, 0, 1, 0, 0, 1, 1, 1}, // T
                {1, 1, 0, 0, 0, 1, 1, 0} // Z
            };

            // Wähle einen Stein zufällig und setzen ihn entsprechend aufs Feld
            int temp = nextStein;
            int begin = Breite/2 - 2 + Breite*2;

            int pp = 0;
            for (int i = begin; i < begin + 4; i++, pp++)
            {
                if (data[temp, pp] == 1)
                {
                    Spielfeld[i].Art = temp + 1;
                    Spielfeld[i].Bewegung = true;
                }
            }

            pp = 4;
            begin += Breite;
            for (int i = begin; i < begin + 4; i++, pp++)
            {
                if (data[temp, pp] == 1)
                {
                    Spielfeld[i].Art = temp + 1;
                    Spielfeld[i].Bewegung = true;
                }
            }

            nextStein = rnd.Next(0, 7);
            // fertig gesetzt

            temp = nextStein;
            begin = Breite*Hoehe;

            pp = 0;
            for (int i = begin; i < begin + 4; i++, pp++)
            {
                if (data[temp, pp] == 1)
                {
                    Bilder[i].Image = Pictures.Images[nextStein];
                    Bilder[i].Show();
                }
                else
                    Bilder[i].Hide();
            }

            pp = 4;
            begin += 4;
            for (int i = begin; i < begin + 4; i++, pp++)
            {
                if (data[temp, pp] == 1)
                {
                    Bilder[i].Image = Pictures.Images[nextStein];
                    Bilder[i].Show();
                }
                else
                    Bilder[i].Hide();
            }
        }

        private static void PrüfeNiederlage()
        {
            // Prüfe ob sich ein "fester" Baustein im oberen "unsichtbaren" Bereich findet, also das Spielfeld bis oben reicht
            for (int i = Breite*3; i < Breite*4; i++)
            {
                if (Spielfeld[i].Art > 0 && !Spielfeld[i].Bewegung)
                {
                    StopSpiel();
                    return;
                } // wenn ja, dann zerstöre das Spiel
            }

            // Alles ok, niemand hat hier verloren
        }

        private static void PrüfeZeilen()
        {
            // hier soll ermittelt werden, ob es Zeilen gibt, "voll" sind, also gelöscht werden können

            for (int i = 0; i < Hoehe; i++)
            {
                // gehe alle Zeilen von oben nach unten durch

                bool found = false;
                for (int b = 0; b < Breite; b++)
                {
                    int pos = i*Breite + b;
                    if (Spielfeld[pos].Art == 0 || Spielfeld[pos].Bewegung)
                    {
                        found = true; // nein, Zeile ist nicht voll
                        break;
                    }
                }

                // ist Zeile voll???
                if (!found)
                {
                    // ja, lösche Zeile
                    for (int b = 0; b < Breite; b++)
                    {
                        int pos = i*Breite + b;
                        Spielfeld[pos].Art = 0;
                        Spielfeld[pos].Bewegung = false;
                        Bilder[pos].Image = GetBild(pos);
                    }

                    ErhoeheZeilen(); // erhöht score

                    // damit sind alle Zeile darüber in Bewegung (aufrücken)
                    for (int b = 0; b < i*Breite; b++)
                    {
                        Spielfeld[b].Bewegung = true;
                        Bilder[b].Image = GetBild(b);
                    }
                }
            }
        }

        // speichert das Spielfeld
        private static void sekunde_Tick(object sender, EventArgs e)
        {
            // Es ist mal wieder Zeit vergangen
            // modus :  false == Feld aufrücken,  true = Stein fällt

            if (!modus)
            {
                // Wenn eine Zeile entfernt wurde, wird das Feld wieder aufgerückt
                BewegeFelder(false);
                PrüfeZeilen();
                if (!GibtEsBewegung())
                {
                    NeuerStein();
                    modus = true;
                    Warte--;
                    if (Warte == 0)
                    {
                        Geschw = (int) ((double) Geschw*0.9f);
                        Warte = 15;
                    }
                }
            }
            else
            {
                // Befindet sich ein fallender Stein auf dem Feld, wird der hier bewegt
                BewegeFelder(true);
                PrüfeNiederlage();
                if (!GibtEsBewegung())
                {
                    Zeitgeber.Interval = Geschw;
                    modus = false;
                }
            }
        }

        private static void Verschiebe_Links()
        {
            // verschiebt den Stein nach Links

            // kann verschoben werden?
            for (int i = Hoehe - 2; i >= 0; i--)
            {
                for (int b = 0; b < Breite; b++)
                {
                    int pos = i*Breite + b;
                    if (Spielfeld[pos].Art > 0 && Spielfeld[pos].Bewegung &&
                        (b == 0 || Spielfeld[i*Breite + b - 1].Art > 0 && !Spielfeld[i*Breite + b - 1].Bewegung))
                    {
                        return;
                    }
                }
            }

            // ja, dann verschiebe
            for (int i = Hoehe - 1; i >= 0; i--)
            {
                for (int b = 1; b < Breite; b++)
                {
                    int pos = i*Breite + b;
                    if (Spielfeld[pos].Art > 0 && Spielfeld[pos].Bewegung && Spielfeld[i*Breite + b - 1].Art == 0)
                    {
                        Spielfeld[i*Breite + b - 1].Art = Spielfeld[pos].Art;
                        Spielfeld[pos].Art = 0;
                        Spielfeld[i*Breite + b - 1].Bewegung = true;
                        Spielfeld[pos].Bewegung = false;
                        Bilder[pos].Image = GetBild(pos);
                        Bilder[(i)*Breite + b - 1].Image = GetBild((i)*Breite + b - 1);
                    }
                }
            }
        }

        private static void Verschiebe_Rechts()
        {
            // verschiebt den Stein nach rechts

            // kann verschoben werden?
            for (int i = Hoehe - 2; i >= 0; i--)
            {
                for (int b = Breite - 1; b >= 0; b--)
                {
                    int pos = i*Breite + b;
                    if (Spielfeld[pos].Art > 0 && Spielfeld[pos].Bewegung &&
                        (b == Breite - 1 || Spielfeld[i*Breite + b + 1].Art > 0 && !Spielfeld[i*Breite + b + 1].Bewegung))
                    {
                        return;
                    }
                }
            }

            // ja, dann verschiebe
            for (int i = Hoehe - 1; i >= 0; i--)
            {
                for (int b = Breite - 2; b >= 0; b--)
                {
                    int pos = i*Breite + b;
                    if (Spielfeld[pos].Art > 0 && Spielfeld[pos].Bewegung)
                    {
                        Spielfeld[i*Breite + b + 1].Art = Spielfeld[pos].Art;
                        Spielfeld[pos].Art = 0;
                        Spielfeld[i*Breite + b + 1].Bewegung = true;
                        Spielfeld[pos].Bewegung = false;
                        ;
                        Bilder[pos].Image = GetBild(pos);
                        Bilder[(i)*Breite + b + 1].Image = GetBild((i)*Breite + b + 1);
                    }
                }
            }
        }

        #endregion Methods

        // der Zeitgeber für das Sekundenzählen

        // Stein = zusammenhängendes Objekt L,I,O,J,T.....
        // Baustein = einzelnes Feld des Spielfeldes oder Steins
    }
}