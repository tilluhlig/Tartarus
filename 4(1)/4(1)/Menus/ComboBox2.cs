using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _4_1_
{
    public class ComboBox2
    {
        public bool visible = false;
        public Minimenu Titel;
        public Minimenu Optionen;
        private Vector2 Pos;

        public ComboBox2(String Titelbezeichnung, String[] Optionenbezeichnungen, int Breite, GraphicsDevice graphicsDevice, Vector2 Position, Color Hintergrundfarbe, Color Schriftfarbe, Color SchiftfarbeAusgewählt, Color Hintergrundfarbe2, Color Schriftfarbe2, Color SchiftfarbeAusgewählt2)
        {
            Pos = Position;

            List<String> list = new List<String>();
            list.AddRange(Optionenbezeichnungen);

            Optionen = new Minimenu(list, Texturen.font2, graphicsDevice, Breite, Hintergrundfarbe2, Schriftfarbe2, SchiftfarbeAusgewählt2, Color.White);

            List<String> list2 = new List<String>();
            list2.Add(Titelbezeichnung);
            Titel = new Minimenu(list2, Texturen.font2, graphicsDevice, Breite, Hintergrundfarbe, Schriftfarbe, SchiftfarbeAusgewählt, Color.Black);
        }

        public void show()
        {
            visible = true;
            Titel.show(Pos, 0);
            Optionen.hide();
        }

        public void hide()
        {
            visible = false;
            Optionen.hide();
            Titel.hide();
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Vector2 Fenster)
        {
            if (!visible) return;

            Titel.Draw(spriteBatch, graphicsDevice, Fenster, true);
            Optionen.Draw(spriteBatch, graphicsDevice, Fenster, false);

            return;
        }

        public void MouseKeys(GraphicsDevice graphicsDevice, Vector2 Fenster, MouseState oldmouseState)
        {
            if (!visible) return;

            if (Help.GetMouseState().LeftButton != oldmouseState.LeftButton)
                switch (Titel.Interact(Fenster, true, oldmouseState))
                {
                    case 0:
                        {
                            if (Optionen.sichtbar)
                            {
                                Optionen.hide();
                            }
                            else
                                Optionen.show(Pos + new Vector2(0, Texturen.font2.MeasureString("A").Y + 7), 0);

                            return;
                        }

                    default: Optionen.hide(); break;
                }
        }
    }
}