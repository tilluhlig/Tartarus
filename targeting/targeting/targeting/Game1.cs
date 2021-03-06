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
        Texture2D dummy;
        Texture2D rohr;
        Texture2D mouse;
        SpriteFont font;
        bool overreach = false;
        int screenheight = 480, screenwidth = 800;

        // Wind
        private float a = 0.0f;

        float dx = 0, dy = 0, g = 0.1f;
        double v0 = 0, t = 0, angle = MathHelper.ToRadians(45);
        bool shoot = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            screenwidth = graphics.PreferredBackBufferWidth;
            screenheight = graphics.PreferredBackBufferHeight;
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
            LoadContent();
            ownPos = new Vector2(1, 470);
            targetPos = new Vector2(screenwidth / 2 - dummy.Width / 2, screenheight / 2 - dummy.Height / 2);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            dummy = Content.Load<Texture2D>("Dummy");
            rohr = Content.Load<Texture2D>("Rohr");
            mouse = Content.Load<Texture2D>("mauszeiger");
            font=Content.Load<SpriteFont>("font");
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

            // berechne Schusskraft
            float deg = MathHelper.ToDegrees((float) angle);
            if (deg > 90) { overreach = true; } else overreach = false;

            shoot = true;
            dx = -(targetPos.X - ownPos.X);
            dy = -(targetPos.Y - ownPos.Y);
            Target.GetPower(angle, dx, dy, a, g, overreach, ref t, ref v0);
            if (t == 0) v0 = 100;
            if (v0 < 0) shoot = false;
            //else v0 *= Math.Sqrt(10);

            mouseKeys();
            KeyboardKeys();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        /// <summary>
        /// Z�ndet den Schuss
        /// </summary>
        void KeyboardKeys()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                    angle -= 0.01f;
                    if (angle < MathHelper.ToRadians(0))
                    {
                        angle = MathHelper.ToRadians(0);
                    }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                    angle += 0.01f;
                    if (angle > MathHelper.ToRadians(180))
                    {
                        angle = MathHelper.ToRadians(180);
                    }
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
        /// Ratet den Winkel f�r die restliche Ki, unter dem gefeuert wird
        /// Soweit nicht vollst�ndig!!! (wird auch nirgends genutzt)
        /// </summary>
        /// <param name="relTargetPos">Position des Ziels (relativ zur eigener Position)</param>
        /// <param name="lastCollision">Enth�lt, wo die letzte Rakete m�ndete (nicht notwendig?)</param>
        /// <param name="lastAngle">Enth�lt den Winkel, unter dem zuerst gefeuert wurde</param>
        /// <param name="successfullHit">Enth�lt die Trefferkategorie des letzten Schusses
        /// 0: daneben, 1: starke Gel�nde�nderung beim letzten Schuss, 2: Splash (gro�e Rakete), 3: Splash (kleine Rakete), 4: direkter Treffer</param>
        /// <param name="firstShot">Gibt an, ob es der erste Schus ist, der auf dieses Ziel erfolgte</param>
        /// <param name="lastX">Letzte bekannte X-Koordinate des Gegners(letzter Zug)</param>
        /// <returns>Ungef�irer Schusswinkel</returns>
        float rate_Winkel(Vector2 relTargetPos, Vector2 lastCollision, float lastAngle, byte successfullHit, bool firstShot, float lastX)
        {
            if (lastCollision == Vector2.Zero)
                //wenn erster schuss auf gegebenen Gegner
                //verwende schwache munition
                return 45;
                if (successfullHit>0) 
                    //letzter treffer w�re erfolgreich, wenn der mit bestimmter munition ausgef�hrt w�re:
                            //4:bleimunition(freie munitionswahl); 3:kleine rakete; 2:gro�e rakete; 
                            //1:war zuletzt erfolgreich, aber nun gel�nde�nderung durch explosion; 0:keine munition w�rde treffen
                    //verwende m�glichst starke munition, die treffen w�rde
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
            //eigentliche Sch�tzung

            /*
             * was kann passieren?
             * gegner ist definitiv nicht treffbar (bestimmte entfernung zum ziel �berschritten)
             * -Schuss hat zu wenig x beschleunigung, dh treffer f�r beliebige power nicht m�glich
             *     dann schusswinkel verringern
             * -schuss hat zu wenig y beschleunigung, dh projektil bleibt am gel�nde h�ngen(mit treffpunkt.x<zielpunkt.x)
             *     dann schusswinkel erh�hen
             * -schuss w�rde richtig landen, es war aber eine landschicht/br�cke dr�ber (treffpunkt.x=zielpunkt.y)
             *     entscheidung treffen, ob man diese zerst�rt; n�her ranfahren
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
            float x = Mouse.GetState().X;
            float y = Mouse.GetState().Y;

            if (x < 0)x=0;
            if (x >= screenwidth) x = screenwidth-1;
            if (y < 0) y = 0;
            if (y >=screenheight) y = screenheight-1;

                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    targetPos = new Vector2(x, y);
                }
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    ownPos = new Vector2(x, y);
                }
        }

        /// <summary>
        ///     Rotiert einen Vektor
        /// </summary>
        /// <param name="Winkel">der Rotationswinkel</param>
        /// <param name="u">die Rotationsachse</param>
        /// <param name="BB">der Vektor, der gedreht werden soll</param>
        /// <returns>Vector3.</returns>
        public static Vector3 Rotiere(double Winkel, Vector3 u, Vector3 BB)
        {
            double c = Math.Cos(Winkel);
            double s = Math.Sin(Winkel);
            double[] A =
            {
                (float) c + (1 - c)*u.X*u.X, (float) -s*u.Z + (1 - c)*u.X*u.Y, (float) s*u.Y + (1 - c)*u.X*u.Z
            };
            double[] B =
            {
                (float) s*u.Z + (1 - c)*u.X*u.Y, (float) c + (1 - c)*u.Y*u.Y, (float) -s*u.X + (1 - c)*u.Y*u.Z
            };
            double[] C =
            {
                (float) -s*u.Y + (1 - c)*u.X*u.Z, (float) s*u.X + (1 - c)*u.Y*u.Z, (float) c + (1 - c)*u.Z*u.Z
            };
            return new Vector3((float)((float)A[0] * BB.X + A[1] * BB.Y + A[2] * BB.Z),
                (float)((float)B[0] * BB.X + B[1] * BB.Y + B[2] * BB.Z), (float)((float)C[0] * BB.X + C[1] * BB.Y + C[2] * BB.Z));
        }

        /// <summary>
        ///     Dreht einen Vektor
        /// </summary>
        /// <param name="Winkel">der Drehwinkel</param>
        /// <param name="u">die Rotationsachse</param>
        /// <param name="B">der Vektor, welcher gedreht werden soll</param>
        /// <returns>Vector2.</returns>
        public static Vector2 Rotiere(double Winkel, Vector3 u, Vector2 B)
        {
            Vector3 B2 = Rotiere(Winkel, u, new Vector3(B.X, B.Y, 1));
            return new Vector2(B2.X, B2.Y);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            float x = Mouse.GetState().X;
            float y = Mouse.GetState().Y;
            if (x < 0) x = 0;
            if (x >= screenwidth) x = screenwidth - 1;
            if (y < 0) y = 0;
            if (y >= screenheight) y = screenheight - 1;

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Maus X: " + x + " Y: " + y, Vector2.Zero, Color.Red);
            spriteBatch.DrawString(font, "Ziel X: " + targetPos.X + " Y: " + targetPos.Y, new Vector2(0, 15), Color.Red);
            spriteBatch.DrawString(font, "v0 " + v0, new Vector2(0, 30), Color.Red);
            /*CannonOrigin[0][0] = ;
            CannonOrigin[0][1] = new Vector2(-250, 25);*/

            spriteBatch.Draw(mouse, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);
            spriteBatch.Draw(dummy, targetPos - new Vector2(dummy.Width / 2, dummy.Height / 2),Color.White);
            spriteBatch.Draw(rohr, ownPos, null, Color.Red,
                            (float) (angle + 0), new Vector2(250, 25), 0.25f,
                            (overreach ? SpriteEffects.FlipVertically : SpriteEffects.None), 1); //

            if (shoot)
            {
                float x2 = (float)(Math.Cos(angle) * v0);
                float y2 = (float)(-Math.Sin(angle) * v0);
                //Vector2 schuss = v0 / (float)Math.Log(v0, Math.E);
                //Vector2 schuss = new Vector2(x2, y2);

                var up = new Vector2(0, -1);
                Matrix rotMatrix = Matrix.CreateRotationZ( (float) angle + 0 - MathHelper.PiOver2 );
                Vector2 schuss = Vector2.Transform(up, rotMatrix);
                schuss *= (float) v0 / (float)Math.Log(v0, Math.E);

                Vector2 c2 = Rotiere(angle, new Vector3(0, 0, 1), new Vector2(-250, 0) * 0.25f);

                getBahn(new Vector2(a, g), schuss, ownPos+c2, screenheight, -1, 1000, spriteBatch);
                //getBahn(new Vector2(a, g), schuss*0.8f, ownPos, screenheight, -1, 1000, spriteBatch);
            }
           // getBahn(new Vector2(2, 3), new Vector2(20, -30), new Vector2(1, 400),480, 0, 800, spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
