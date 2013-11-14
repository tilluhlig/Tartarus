using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _4_1_
{
    public static class KleinesMenu
    {
        public static Minimenu test;
        private static bool first = true;
        public static bool sichtbar = false;

        public static void init(GraphicsDevice graphicsDevice)
        {
            List<string> Inhalt = new List<string>();
            Inhalt.Add("Entferne Notiz");

            test = new Minimenu(Inhalt, Texturen.font4, graphicsDevice, -1, Color.SteelBlue, Color.Black, Color.Goldenrod, Color.White);
        }

        public static void show(Vector2 Pos, int target)
        {
            sichtbar = true;
            test.show(Pos, target);
        }

        public static void hide()
        {
            sichtbar = false;
        }

        public static void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Vector2 Fenster)
        {
            if (!sichtbar) return;
            test.Draw(spriteBatch, graphicsDevice, Fenster, false);
        }

        public static void MouseKeys(GraphicsDevice graphicsDevice, Notizen notiz, MouseState oldmouseState)
        {
            if (first)
            {
                init(graphicsDevice);
                first = false;
            }

            if (!sichtbar) return;

            switch (test.Interact(Game1.Spiel2.Fenster, true, oldmouseState))
            {
                case 0:
                    {
                        if (test.target > -1) { notiz.delNotiz(test.target); test.target = -1; test.sichtbar = false; }
                        return;
                    }
                case 1:
                    {
                        if (test.target > -1)
                        {
                            //schreibe mittels Kurznachricht die Fehlermeldung, dass hier schon eine Notiz vorhanden ist
                            return;
                        }
                        // Vector2 temp = new Vector2(minscreen + Help.GetMouseState().X, Help.GetMouseState().Y);
                        //notiz.AddNotiz(graphicsDevice, temp, "Neuer Eintrag");
                        return;
                    }
                default: break;
            }
        }
    }
}