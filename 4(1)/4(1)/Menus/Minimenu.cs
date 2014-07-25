using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _4_1_
{
    public class Minimenu
    {
        public List<string> Inhalt;
        private SpriteFont font;
        private Texture2D menu;
        private BoundingBox menuBereich;
        private int gewahlt = -1;
        private Vector2 pos = new Vector2(-1, -1);
        private bool tastendruck = false;
        public bool sichtbar = false;

        //private float teilmenu;
        private float fHohe;

        public int target = -1;
        public int over = -1;

        private Color Schriftfarbe = Color.Black;
        private Color SchriftfarbeAusgewählt = Color.Goldenrod;
        private Color Randfarbe = Color.Black;

        public void hide()
        {
            sichtbar = false;
        }

        public Minimenu(List<string> inhalt, SpriteFont font, GraphicsDevice graphicsDevice, int Breite, Color Hintergrundfarbe, Color _Schriftfarbe, Color _SchriftfarbeAusgewählt, Color _Randfarbe)
        {
            Schriftfarbe = _Schriftfarbe;
            SchriftfarbeAusgewählt = _SchriftfarbeAusgewählt;
            Randfarbe = _Randfarbe;

            Inhalt = inhalt;
            this.font = font;
            fHohe = font.MeasureString("A").Y;

            float longest = 0;

            if (Breite <= -1)
            {
                for (int i = 0; i < Inhalt.Count; i++)
                    if (longest < font.MeasureString(Inhalt[i]).X) longest = font.MeasureString(Inhalt[i]).X;
                longest += 4;
            }
            else
                longest = Breite;

            menu = new Texture2D(graphicsDevice, (int)longest, (int)(fHohe * Inhalt.Count)); // + Inhalt.Count * 4
            Color[] color = new Color[(int)longest * (int)(fHohe * Inhalt.Count)]; // + Inhalt.Count * 4
            for (int i = 0; i < color.Length; i++) color[i] = Hintergrundfarbe;
            menu.SetData(color);
            // teilmenu = 1.0f / 3;
        }

        public void show(Vector2 Position, int target)
        {
            if (tastendruck) return;
            pos = Position;

            menuBereich = new BoundingBox(new Vector3(pos, 0), new Vector3(pos.X + menu.Width, pos.Y + menu.Height, 0));
            sichtbar = true;
            this.target = target;
            tastendruck = true;
        }

        public int Interact(Vector2 Fenster, bool richtung, MouseState oldmouseState)
        {
            tastendruck = false;
            if (sichtbar)
            {
                if (menuBereich.Contains(new Vector3(Help.GetMouseState().X + Fenster.X, Help.GetMouseState().Y + Fenster.Y, 0)) == ContainmentType.Contains)
                {
                    float temp = Help.GetMouseState().Y + Fenster.Y;
                    temp = (temp - pos.Y);

                    //float abstand = (temp - pos.Y); abstand = menu.Height / Inhalt.Count;
                    float abstand = font.MeasureString("a").Y;

                    for (int i = 0; i < Inhalt.Count; i++)
                    {
                        if (temp < abstand * (i + 1) && temp >= abstand * i)
                        {
                            gewahlt = i; over = i; break;
                        }
                    }
                }
                else { gewahlt = -1; over = -1; }

                if (richtung)
                    if (Help.GetMouseState().LeftButton != oldmouseState.LeftButton)
                        if (Help.GetMouseState().LeftButton == ButtonState.Pressed) { return gewahlt; }

                if (!richtung)
                    if (Help.GetMouseState().LeftButton != oldmouseState.LeftButton)
                        if (Help.GetMouseState().LeftButton == ButtonState.Released) { return gewahlt; }
            }
            else
                over = -1;
            return -1;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Vector2 Fenster, bool rand)
        {
            if (sichtbar)
            {
                if (rand) Help.DrawRectangle(spriteBatch, graphicsDevice, new Rectangle((int)(pos.X - Fenster.X - 1), (int)(pos.Y - Fenster.Y - 1), (int)(menu.Width + 2), (int)(menu.Height + 2)), Randfarbe, 1.0f);

                spriteBatch.Draw(menu, pos - Fenster, Color.White);

                Vector2 textpos = pos + new Vector2(2, 2) - Fenster;

                for (int i = 0; i < Inhalt.Count; i++)
                {
                    if (i == over)
                        spriteBatch.DrawString(font, Inhalt[i], textpos + new Vector2(0, i * (font.MeasureString("A").Y)), SchriftfarbeAusgewählt);
                    else
                        spriteBatch.DrawString(font, Inhalt[i], textpos + new Vector2(0, i * (font.MeasureString("A").Y)), Schriftfarbe);
                }
            }
        }
    }
}