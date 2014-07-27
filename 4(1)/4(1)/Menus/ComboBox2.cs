using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _4_1_
{
    public class ComboBox2
    {
        #region Fields

        private readonly Vector2 Pos;
        public Minimenu Optionen;
        public Minimenu Titel;
        public bool visible = false;

        #endregion Fields

        #region Constructors

        public ComboBox2(String Titelbezeichnung, String[] Optionenbezeichnungen, int Breite,
            GraphicsDevice graphicsDevice, Vector2 Position, Color Hintergrundfarbe, Color Schriftfarbe,
            Color SchiftfarbeAusgewählt, Color Hintergrundfarbe2, Color Schriftfarbe2, Color SchiftfarbeAusgewählt2)
        {
            Pos = Position;

            var list = new List<String>();
            list.AddRange(Optionenbezeichnungen);

            Optionen = new Minimenu(list, Texturen.font2, graphicsDevice, Breite, Hintergrundfarbe2, Schriftfarbe2,
                SchiftfarbeAusgewählt2, Color.White);

            var list2 = new List<String>();
            list2.Add(Titelbezeichnung);
            Titel = new Minimenu(list2, Texturen.font2, graphicsDevice, Breite, Hintergrundfarbe, Schriftfarbe,
                SchiftfarbeAusgewählt, Color.Black);
        }

        #endregion Constructors

        #region Methods

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Vector2 Fenster)
        {
            if (!visible) return;

            Titel.Draw(spriteBatch, graphicsDevice, Fenster, true);
            Optionen.Draw(spriteBatch, graphicsDevice, Fenster, false);
        }

        public void hide()
        {
            visible = false;
            Optionen.hide();
            Titel.hide();
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

                    default:
                        Optionen.hide();
                        break;
                }
        }

        public void show()
        {
            visible = true;
            Titel.show(Pos, 0);
            Optionen.hide();
        }

        #endregion Methods
    }
}