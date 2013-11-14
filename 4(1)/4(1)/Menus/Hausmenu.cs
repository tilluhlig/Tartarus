using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;

namespace _4_1_
{
    public class HausMenu
    {
        Vector2[] stringpos = { new Vector2(120, 0), new Vector2(120, 20) };

        private Button[] items = {new Button(Texturen.hausbutton, new Vector2(200, 100), "ok", Texturen.font),
                                  new Button(Texturen.hausbutton, new Vector2(300, 100), "rep", Texturen.font),
                                  new Button(Texturen.hausbutton, new Vector2(400, 100), "upgr", Texturen.font)};


        public int mouseKeys(MouseState mouseState)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].MouseKeys(mouseState))
                {
                    switch (i)
                    {
                        case 0: { return 0; }
                        case 1: return 1;
                        case 2: { return 2; }
                    }
                }

            }
            return -1;
        }

        public void Draw(SpriteBatch spriteBatch, int hauslevel, int haushp)
        {
            spriteBatch.Draw(Texturen.hausmenu, new Vector2(120, 0), Color.White);
            for (int i = 0; i < items.Length; i++)
            {
                items[i].Draw(spriteBatch, Color.White, Color.Gold);
            }
            spriteBatch.DrawString(Texturen.font, "Hauslevel: " + hauslevel, stringpos[0], Color.Red);
            spriteBatch.DrawString(Texturen.font, "Haushp: " + haushp, stringpos[1], Color.Red);

        }

    }
}
