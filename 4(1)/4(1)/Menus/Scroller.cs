using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _4_1_
{
    public class Scroller
    {
        private int breite;
        private int hoehe;
        private Vector2 pos;
        public int maxItems;
        public int items = 0;
        private float minY; private float maxY;
        private bool vertikal = false;
        public bool visible = false;

        private Texture2D oben;
        private Texture2D unten;
        private Texture2D balken;
        private Texture2D scroller;

        private BoundingBox obenBox;
        private BoundingBox untenBox;
        private BoundingBox scrollerBox;
        private BoundingBox Balkenbox;

        private GraphicsDevice graphicsDevice;

        private float obenscale;

        private int _oberstes = 0;

        public int oberstes
        {
            get
            {
                return _oberstes;
            }

            set
            {
                _oberstes = value;

                if (vertikal)
                {
                    scrollerpos.Y = (float)minY + ((float)((float)maxY - minY) / (items - maxItems) * value);
                }
                else
                {
                    scrollerpos.X = (float)minY + ((float)((float)maxY - minY) / (items - maxItems) * value);
                }

                UpdatePos(pos);
                updateBox();
            }
        }

        private Vector2 scrollerpos;
        private Vector2 untenpos;
        private Vector2 balkenpos;
        public float clickY;

        private bool holding = false;
        private bool holdingo = false;
        private bool holdingu = false;
        private int counter = 15;
        private int maxcounter = 15;

        private Color Hintergrund = Color.Green;
        private Color Balken = Color.Gray;

        public void show()
        {
            visible = true;
        }

        public void hide()
        {
            visible = false;
        }

        public bool InScroller(Vector2 Fenster)
        {
            BoundingBox a = new BoundingBox(new Vector3(pos, 0), new Vector3(pos + new Vector2(breite, hoehe), 0));
            if (a.Contains(new Vector3(Help.GetMouseState().X + Fenster.X, Help.GetMouseState().Y + Fenster.Y, 0)) == ContainmentType.Contains)
            {
                return true;
            }
            return false;
        }

        public Scroller(int breite, int hoehe, Vector2 pos, int maxItems, int Items, bool vertikal, GraphicsDevice graphicsDevice, ContentManager Content, Color Hintergrund, Color Balken)
        {
            this.breite = breite; this.hoehe = hoehe; this.pos = pos; this.maxItems = maxItems; this.vertikal = vertikal; this.items = Items;
            this.Hintergrund = Hintergrund;
            this.Balken = Balken;

            if (breite > hoehe) vertikal = false;

            if (vertikal)
            {
                oben = Content.Load<Texture2D>("Textures\\alt_oben"); unten = Content.Load<Texture2D>("Textures\\alt_unten");
                obenscale = (float)breite / oben.Width;

                balken = new Texture2D(graphicsDevice, breite, (int)(hoehe - 2 * oben.Height * obenscale));
                Color[] color = new Color[breite * (int)(hoehe - 2 * oben.Height * obenscale)]; //
                for (int i = 0; i < color.Length; i++) color[i] = Hintergrund;
                balken.SetData(color);

                minY = pos.Y + oben.Height * obenscale;
                untenpos = pos + new Vector2(0, oben.Height * obenscale + balken.Height);
                balkenpos = pos + new Vector2(0, oben.Height * obenscale);

                Balkenbox = new BoundingBox(new Vector3(pos + new Vector2(0, oben.Height) * obenscale, 0), new Vector3(untenpos + new Vector2(oben.Width, 0) * obenscale, 0));
            }
            else
            {
                oben = Content.Load<Texture2D>("Textures\\nachLinks"); unten = Content.Load<Texture2D>("Textures\\nachRechts");
                obenscale = (float)hoehe / oben.Height;

                balken = new Texture2D(graphicsDevice, (int)(breite - 2 * oben.Width * obenscale), hoehe);
                Color[] color = new Color[hoehe * (int)(breite - 2 * oben.Width * obenscale)];
                for (int i = 0; i < color.Length; i++) color[i] = Hintergrund;
                balken.SetData(color);

                minY = pos.X + oben.Width * obenscale;
                untenpos = pos + new Vector2(oben.Width * obenscale + balken.Width, 0);

                balkenpos = pos + new Vector2(oben.Width * obenscale, 0);

                Balkenbox = new BoundingBox(new Vector3(pos + new Vector2(oben.Width, 0) * obenscale, 0), new Vector3(untenpos + new Vector2(0, oben.Height) * obenscale, 0));
            }

            obenBox = new BoundingBox(new Vector3(pos, 0), new Vector3(pos + new Vector2(oben.Width, oben.Height) * obenscale, 0));
            untenBox = new BoundingBox(new Vector3(untenpos, 0), new Vector3(untenpos + new Vector2(oben.Width, oben.Height) * obenscale, 0));

            scrollerpos = balkenpos;
            updateScroller(items, graphicsDevice);
        }

        public void UpdatePos(Vector2 pos)
        {
            Vector2 move = pos - this.pos;
            this.pos = pos;

            if (vertikal)
            {
                minY = pos.Y + oben.Height * obenscale;
                untenpos = pos + new Vector2(0, oben.Height * obenscale + balken.Height);
                balkenpos = pos + new Vector2(0, oben.Height * obenscale);
                maxY = untenpos.Y - scroller.Height;

                Balkenbox = new BoundingBox(new Vector3(pos + new Vector2(0, oben.Height) * obenscale, 0), new Vector3(untenpos + new Vector2(oben.Width, 0) * obenscale, 0));
            }
            else
            {
                minY = pos.X + oben.Width * obenscale;
                untenpos = pos + new Vector2(oben.Width * obenscale + balken.Width, 0);
                balkenpos = pos + new Vector2(oben.Width * obenscale, 0);
                maxY = untenpos.X - scroller.Width;

                Balkenbox = new BoundingBox(new Vector3(pos + new Vector2(oben.Width, 0) * obenscale, 0), new Vector3(untenpos + new Vector2(0, oben.Height) * obenscale, 0));
            }

            obenBox = new BoundingBox(new Vector3(pos, 0), new Vector3(pos + new Vector2(oben.Width, oben.Height) * obenscale, 0));
            untenBox = new BoundingBox(new Vector3(untenpos, 0), new Vector3(untenpos + new Vector2(oben.Width, oben.Height) * obenscale, 0));

            scrollerpos += move;
            updateBox();
        }

        //rufe die Funktion auf, wenn items dazukommen
        public void updateScroller(int items, GraphicsDevice graphicsDevice)
        {
            this.items = items;
            this.graphicsDevice = graphicsDevice;

            if (items < maxItems + 1)
            //scroller nicht noetig
            {
                Color[] color;
                if (vertikal)
                {
                    scroller = new Texture2D(graphicsDevice, breite, (int)(balken.Height));
                    color = new Color[breite * (int)(hoehe - 2 * oben.Height * obenscale)];
                }
                else
                {
                    scroller = new Texture2D(graphicsDevice, (int)(balken.Width), hoehe);
                    color = new Color[(int)(breite - 2 * oben.Width * obenscale) * hoehe];
                }
                for (int i = 0; i < color.Length; i++) color[i] = Balken;
                scroller.SetData(color);
            }
            else
            {
                Color[] color;
                if (vertikal)
                {
                    int size = balken.Height / (items - maxItems);
                    //ist update notwendig?
                    if (size < 10) { size = 10; if (scroller != null) if (scroller.Height == 10) return; }
                    scroller = new Texture2D(graphicsDevice, breite, size);
                    color = new Color[breite * size];
                }
                else
                {
                    int size = balken.Width / (items - maxItems);
                    if (size < 10) { size = 10; if (scroller != null) if (scroller.Width == 10) return; }
                    scroller = new Texture2D(graphicsDevice, size, hoehe);
                    color = new Color[hoehe * size];
                }
                for (int i = 0; i < color.Length; i++) color[i] = Balken;
                scroller.SetData(color);
            }

            if (vertikal) maxY = untenpos.Y - scroller.Height;
            else maxY = untenpos.X - scroller.Width;

            if (scrollerpos.Y > maxY) scrollerpos.Y = maxY;
            updateBox();
        }

        private void updateBox()
        {
            scrollerBox = new BoundingBox(new Vector3(scrollerpos, 0), new Vector3(scrollerpos + new Vector2(scroller.Width, scroller.Height), 0));
        }

        public bool MouseKeys(Vector2 Fenster)
        {
            if (!visible) return false;
            if (items <= maxItems) return false;

            #region Scroller

            if (holding)
            {
                if (vertikal)
                {
                    float diff = Help.GetMouseState().Y - clickY - Fenster.Y;

                    if (clickY + diff > maxY)
                    { scrollerpos.Y = maxY; oberstes = items - maxItems; }
                    else
                    {
                        if (clickY + diff < minY) { scrollerpos.Y = minY; oberstes = 0; }
                        else
                        {
                            scrollerpos.Y = clickY + diff;
                            oberstes = (int)((scrollerpos.Y - minY) / (maxY - minY) * (items - maxItems));
                        }
                    }
                }
                else
                {
                    float diff = Help.GetMouseState().X - clickY - Fenster.Y;

                    if (clickY + diff > maxY)
                    { scrollerpos.X = maxY; oberstes = items - maxItems; }
                    else
                    {
                        if (clickY + diff < minY) { scrollerpos.X = minY; oberstes = 0; }
                        else
                        {
                            scrollerpos.X = clickY + diff;
                            oberstes = (int)((scrollerpos.X - minY) / (maxY - minY) * (items - maxItems));
                        }
                    }
                }
                if (Help.GetMouseState().LeftButton == ButtonState.Released)
                {
                    holding = false;
                    updateBox();
                    return true;
                }
                return false;
            }

            #endregion Scroller

            #region Halte Oben/Unten

            if (holdingo)
            {
                if (Help.GetMouseState().LeftButton == ButtonState.Released)
                {
                    holdingo = false; maxcounter = 15; updateBox();
                    return true;
                }
                else
                {
                    counter--;
                    if (counter == 0)
                    {
                        if (oberstes == 0)
                            return false;
                        else
                        {
                            oberstes--;
                            if (vertikal) scrollerpos.Y = minY + (maxY - minY) * oberstes / (items - maxItems);
                            else scrollerpos.X = minY + (maxY - minY) * oberstes / (items - maxItems);
                        }
                        counter = maxcounter;
                        if (maxcounter > 1) maxcounter--;
                    }
                }
                return false;
            }

            if (holdingu)
            {
                if (Help.GetMouseState().LeftButton == ButtonState.Released)
                {
                    holdingu = false; maxcounter = 15; updateBox();
                    return true;
                }
                else
                {
                    counter--;
                    if (counter == 0)
                    {
                        if (oberstes == items - maxItems)
                            return false;
                        else
                        {
                            oberstes++;
                            if (vertikal) scrollerpos.Y = minY + (maxY - minY) * oberstes / (items - maxItems);
                            else scrollerpos.X = minY + (maxY - minY) * oberstes / (items - maxItems);
                        }
                        counter = maxcounter;
                        if (maxcounter > 1) maxcounter--;
                    }
                }
                return false;
            }

            #endregion Halte Oben/Unten

            if (scrollerBox.Contains(new Vector3(Help.GetMouseState().X + Fenster.X, Help.GetMouseState().Y + Fenster.Y, 0)) == ContainmentType.Contains)
            {
                if (Help.GetMouseState().LeftButton == ButtonState.Pressed)
                {
                    holding = true;
                    if (vertikal) clickY = Help.GetMouseState().Y; else clickY = Help.GetMouseState().X;
                    return true;
                }
                return true;
            }

            #region Druecke Oben/Unten

            if (obenBox.Contains(new Vector3(Help.GetMouseState().X + Fenster.X, Help.GetMouseState().Y + Fenster.Y, 0)) == ContainmentType.Contains)
            {
                if (Help.GetMouseState().LeftButton == ButtonState.Pressed)
                {
                    holdingo = true;
                    if (oberstes == 0) return true;
                    else counter = 1;
                    return true;
                }
                return true;
            }

            if (untenBox.Contains(new Vector3(Help.GetMouseState().X + Fenster.X, Help.GetMouseState().Y + Fenster.Y, 0)) == ContainmentType.Contains)
            {
                if (Help.GetMouseState().LeftButton == ButtonState.Pressed)
                {
                    holdingu = true;
                    if (oberstes == items - maxItems) return true;
                    else counter = 1;
                    return true;
                }
                return true;
            }

            if (Balkenbox.Contains(new Vector3(Help.GetMouseState().X + Fenster.X, Help.GetMouseState().Y + Fenster.Y, 0)) == ContainmentType.Contains)
            {
                if (Help.GetMouseState().LeftButton == ButtonState.Pressed)
                {
                    if (vertikal)
                    {
                        oberstes = (int)((Help.GetMouseState().Y - minY) / (maxY - minY) * (items - maxItems));
                        scrollerpos.Y = minY + (float)balken.Height * oberstes / (items - maxItems + 2);
                    }
                    else
                    {
                        oberstes = (int)((Help.GetMouseState().X - minY) / (maxY - minY) * (items - maxItems));
                        scrollerpos.X = minY + (float)balken.Width * oberstes / (items - maxItems + 2);
                    }
                    holding = true;
                    return true;
                }
                return true;
            }

            #endregion Druecke Oben/Unten

            return false;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 Fenster, GraphicsDevice graphicsDevice)
        {
            if (!visible) return;
            if (items <= maxItems) return;

            updateScroller(items, graphicsDevice);

            spriteBatch.Draw(oben, pos - Fenster, null, Color.White, 0f, Vector2.Zero, obenscale, SpriteEffects.None, 0);
            spriteBatch.Draw(unten, untenpos - Fenster, null, Color.White, 0f, Vector2.Zero, obenscale, SpriteEffects.None, 0);
            spriteBatch.Draw(balken, balkenpos - Fenster, Color.White);
            spriteBatch.Draw(scroller, scrollerpos - Fenster, Color.White);
        }
    }
}