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

namespace targeting
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 ownPos, targetPos;
        BoundingBox box;
        Texture2D pic;
        Texture2D mouse;
        SpriteFont font;
        int screenheight = 480, screenwidth = 800;

        // Wind
        private float a = 0.5f;

        float dx = 0, dy = 0, g = 0.3f;
        double v0 = 0, t = 0, angle = MathHelper.ToRadians(45);
        bool shoot = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ownPos = new Vector2(1, 470);
            targetPos = new Vector2(400,400);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            pic = Content.Load<Texture2D>("this");
            mouse = Content.Load<Texture2D>("mauszeiger");
            font=Content.Load<SpriteFont>("font");
            box = new BoundingBox(new Vector3(targetPos, 0), new Vector3((targetPos
                        + new Vector2(pic.Width, pic.Height)), 0));
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            mouseKeys();
            KeyboardKeys();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        /// <summary>
        /// Zündet den Schuss
        /// </summary>
        void KeyboardKeys()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                shoot = true;
                dx = targetPos.X - ownPos.X;
                dy = -(targetPos.Y - ownPos.Y);
                Target.GetPower(angle, dx, dy, a, g, true, ref t, ref v0);
                if (t == 0) v0 = 100;
                if (v0 < 0) shoot = false;
                //else v0 *= Math.Sqrt(10);
            }

        }

        //zeichnet die Flugbahn
        void getBahn(Vector2 g, Vector2 schuss, Vector2 pos, int screenheight, int minx, int maxx, SpriteBatch spriteBatch)
        {
            Vector2 temp = pos;
            Texture2D line = new Texture2D(GraphicsDevice, 1, 1);
            line.SetData<Color>(new Color[] { Color.Green });
            bool draw = true;
            while (temp.Y < screenheight)
            {
                float angle =
                    (float)Math.Atan2(schuss.Y, schuss.X);
                if (draw)
                {
                    if (temp.X < maxx && temp.X > minx)
                        spriteBatch.Draw(line, new Rectangle((int)temp.X, (int)temp.Y, (int)schuss.Length(), 2), null, Color.White, angle, new Vector2(0, 0), SpriteEffects.None, 0);
                    else return;
                    draw = false;
                }
                else
                {
                    draw = true;
                    schuss += g;
                    temp += schuss;
                }
                schuss += g;
                temp += schuss;
            }
        }
        /// <summary>
        /// Ratet den Winkel für die restliche Ki, unter dem gefeuert wird
        /// Soweit nicht vollständig!!! (wird auch nirgends genutzt)
        /// </summary>
        /// <param name="relTargetPos">Position des Ziels (relativ zur eigener Position)</param>
        /// <param name="lastCollision">Enthält, wo die letzte Rakete mündete (nicht notwendig?)</param>
        /// <param name="lastAngle">Enthält den Winkel, unter dem zuerst gefeuert wurde</param>
        /// <param name="successfullHit">Enthält die Trefferkategorie des letzten Schusses
        /// 0: daneben, 1: starke Geländeänderung beim letzten Schuss, 2: Splash (große Rakete), 3: Splash (kleine Rakete), 4: direkter Treffer</param>
        /// <param name="firstShot">Gibt an, ob es der erste Schus ist, der auf dieses Ziel erfolgte</param>
        /// <param name="lastX">Letzte bekannte X-Koordinate des Gegners(letzter Zug)</param>
        /// <returns>Ungefäirer Schusswinkel</returns>
        float rate_Winkel(Vector2 relTargetPos, Vector2 lastCollision, float lastAngle, byte successfullHit, bool firstShot, float lastX)
        {
            if (lastCollision == Vector2.Zero)
                //wenn erster schuss auf gegebenen Gegner
                //verwende schwache munition
                return 45;
                if (successfullHit>0) 
                    //letzter treffer wäre erfolgreich, wenn der mit bestimmter munition ausgeführt wäre:
                            //4:bleimunition(freie munitionswahl); 3:kleine rakete; 2:große rakete; 
                            //1:war zuletzt erfolgreich, aber nun geländeänderung durch explosion; 0:keine munition würde treffen
                    //verwende möglichst starke munition, die treffen würde
                    return lastAngle;
                    //auf diesen Gegner wurde von diesen Panzer aus letzte Runde geschossen
                    //versuche letzten erfolgreichen Schuss noch einmal
                    //verwende schwache munition
                if (firstShot)
                {
                    if (relTargetPos.X - lastX < 50)
                    //gegner ist noch nicht weit gefahren
                    {
                        firstShot = false;
                        return lastAngle;
                    }
                    else
                        //gegner weit weggefahren
                        return 45;
                }
            //eigentliche Schätzung

            /*
             * was kann passieren?
             * gegner ist definitiv nicht treffbar (bestimmte entfernung zum ziel überschritten)
             * -Schuss hat zu wenig x beschleunigung, dh treffer für beliebige power nicht möglich
             *     dann schusswinkel verringern
             * -schuss hat zu wenig y beschleunigung, dh projektil bleibt am gelände hängen(mit treffpunkt.x<zielpunkt.x)
             *     dann schusswinkel erhöhen
             * -schuss würde richtig landen, es war aber eine landschicht/brücke drüber (treffpunkt.x=zielpunkt.y)
             *     entscheidung treffen, ob man diese zerstört; näher ranfahren
             * -schuss traf nicht, landete aber nicht weit entfernt von ziel
             *     aoe munition verwenden
            */
          
            return 0;
        }
        /// <summary>
        /// Setzt das Versuchsziel aufs Feld. mEhr nicht
        /// </summary>
        void mouseKeys()
        {
                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    targetPos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                    box = new BoundingBox(new Vector3(targetPos, 0), new Vector3((targetPos
                        + new Vector2(pic.Width, pic.Height)), 0));
                }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Maus X: " + Mouse.GetState().X + " Y: " + Mouse.GetState().Y, Vector2.Zero, Color.Red);
            spriteBatch.DrawString(font, "Ziel X: " + targetPos.X + " Y: " + targetPos.Y, new Vector2(0, 15), Color.Red);
            spriteBatch.DrawString(font, "v0 " + v0, new Vector2(0, 30), Color.Red);


            spriteBatch.Draw(mouse, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);
            spriteBatch.Draw(pic, targetPos, Color.Red);
            if (shoot)
            {
                float x = (float)(Math.Cos(angle) * v0);
                float y = (float)(-Math.Sin(angle) * v0);
                getBahn(new Vector2(a, g), new Vector2(x, y), ownPos, screenheight, -1, 1000, spriteBatch);
                getBahn(new Vector2(a, g), new Vector2(x*0.8f, y*0.8f), ownPos, screenheight, -1, 1000, spriteBatch);
            }
           // getBahn(new Vector2(2, 3), new Vector2(20, -30), new Vector2(1, 400),480, 0, 800, spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
