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
using System.Reflection;
using System.Speech.Synthesis;

namespace Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private int drawDistanceWidth = 450;
        private int drawDistanceHeight = 300;
        private int previousScrollValue;
        private Model model;
        Actions.RollDice RD = new Actions.RollDice();
        SoundEffect soundEffect;
        protected Song song;
        private bool RunFullScreenToggle = false;
        private bool GamePaused = false;
        private Objects.Sprite2d Background = new Objects.Sprite2d();
        private Effect ripple;
        private Texture waterfallTexture;
        //Video video;
        //VideoPlayer player;

        //private Matrix world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
        //private Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 10), new Vector3(0, 0, 0), Vector3.UnitY);
        //private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.1f, 100f);
        //private Vector3 position = new Vector3(0, 0, 0);
        //private float angle = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true;
            Util.Global.screenSizeHeight = graphics.PreferredBackBufferHeight - 100;
            Util.Global.screenSizeWidth = graphics.PreferredBackBufferWidth - 100;
            Content.RootDirectory = "Content";
            IsFixedTimeStep = true;

        }

        protected override void Initialize()
        {
            Util.Global.ContentMan = Content;
            song = Content.Load<Song>("Music/town");
            MediaPlayer.Play(song);
            soundEffect = Content.Load<SoundEffect>("Sounds/EscapeMenu");
            Util.Global.Cam = new Util.Camera2D(GraphicsDevice.Viewport);
            previousScrollValue = 0;
            Util.Global.DayColor = Color.White;
            new Util.Main().Init();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Util.Global.font = Content.Load<SpriteFont>("Font/SpriteFont1");
            model = Content.Load<Model>("3dModel/cube");

            ripple = Content.Load<Effect>("Effect/ripple");
            waterfallTexture = Content.Load<Texture>("Texture/waterfall");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Play(song);
            }

            KeyboardState state = Keyboard.GetState();
            MouseState ms = Mouse.GetState();
            //if (player.State == MediaState.Stopped)
            //{
            //    player.IsLooped = true;
            //    player.Play(video);
            //}
            //new Util.Camera2D(GraphicsDevice.Viewport).Update();

            if (state.IsKeyDown(Keys.F1))
            {
                Actions.Anim.AnimTest1();
            }
            if (state.IsKeyDown(Keys.F2))
            {
                Actions.Anim.AnimTest2();
            }
            if (state.IsKeyDown(Keys.F3))
            {
                Actions.Anim.AnimTest3();
            }
            if (state.IsKeyDown(Keys.F4))
            {
                Actions.Anim.AnimTest4();
            }
            if (state.IsKeyDown(Keys.F5))
            {
                Actions.Anim.AnimTest5();
            }
            if (state.IsKeyDown(Keys.F6))
            {
                Actions.Anim.AnimTest6();
            }
            if (state.IsKeyDown(Keys.F7))
            {
               Actions.Season.StartEarlyWinter();
            }
            if (state.IsKeyDown(Keys.Add))
            {
                Actions.Season.AddTime();
            }
            //if (state.IsKeyDown(Keys.Q))
            //{
            //    Util.Global.Cam.Rotation += 0.05f;
            //}
            //if (state.IsKeyDown(Keys.R))
            //{
            //    Util.Global.Cam.Rotation -= 0.05f;
            //}
            if (state.IsKeyDown(Keys.F11))
            {
                if (RunFullScreenToggle == true)
                {
                    RunFullScreenToggle = false;
                }
                else
                {
                    RunFullScreenToggle = true;
                }
            }
            if (state.IsKeyDown(Keys.F12))
            {
                new Objects.Maneuver().RemoveAll();
            }

            if (state.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (state.IsKeyDown(Keys.Tab) && !Util.Global.PreviousKeyboardState.IsKeyDown(Keys.Tab))
            {
                if (Util.Global.Sprites.Where(x => x.name == "Inventory").FirstOrDefault() == null)
                { Menu.Inventory.DisplayInventory(); }
                else
                { Menu.Inventory.HideInventory(); }
            }

            if (state.IsKeyDown(Keys.C) && !Util.Global.PreviousKeyboardState.IsKeyDown(Keys.C))
            {
                if (Util.Global.Sprites.Where(x => x.name == "Craft").FirstOrDefault() == null)
                { Menu.Craft.DisplayCraft(Actions.Crafting.Type.Hand); }
                else
                { Menu.Craft.HideCraft(); }
            }

            if (!GamePaused)
            {
                int updateCount = 0;
                Objects.Sprite2d[] SIR = Util.Global.Sprites.Where(r => (r.Actor != null) || (r.Maneuver != null) || (r.Position.X < Util.Global.Hero.Position.X + drawDistanceWidth && r.Position.Y < Util.Global.Hero.Position.Y + drawDistanceHeight && r.Position.X > Util.Global.Hero.Position.X - drawDistanceWidth && r.Position.Y > Util.Global.Hero.Position.Y - drawDistanceHeight)).ToArray();
                //List<Objects.Sprite2d> SpritesInRange = Util.Global.Sprites.Where(r =>  (r.Actor != null) || (r.Position.X < Util.Global.Hero.Position.X + drawDistanceWidth && r.Position.Y < Util.Global.Hero.Position.Y + drawDistanceHeight && r.Position.X > Util.Global.Hero.Position.X - drawDistanceWidth && r.Position.Y > Util.Global.Hero.Position.Y - drawDistanceHeight)).ToList();
                //foreach (Objects.Sprite2d A in SpritesInRange.Where(x => (x.AnimSprite != null && x.AnimSprite.action == true) || (x.actionCall.Count() > 0 && x.active == true) || (x.Maneuver != null) || (x.Actor != null)).ToList())
                foreach (Objects.Sprite2d A in SIR.Where(x => x.active == true && (x.AnimSprite != null && x.AnimSprite.action == true) || (x.actionCall.Count() > 0) || (x.Maneuver != null) || (x.Actor != null)).ToArray())
                {
                    A.Update(state, ms, gameTime);
                    updateCount++;
                    //System.Diagnostics.Debug.WriteLine(A.name + " " + A.Position.X.ToString() + ":" + A.Position.Y.ToString());
                }
                System.Diagnostics.Debug.WriteLine("updateCount:" + updateCount.ToString());
                if (ms.ScrollWheelValue < previousScrollValue)
                {
                    Util.Global.Cam.Zoom -= 0.1f;
                }
                if (ms.ScrollWheelValue > previousScrollValue)
                {
                    Util.Global.Cam.Zoom += 0.1f;
                }
                previousScrollValue = ms.ScrollWheelValue;

                if (Util.Global.Fighting == false)
                {
                    Util.Global.Cam.Pos = new Vector2((Util.Global.Hero.Position.X * -1) + 350, (Util.Global.Hero.Position.Y * -1) + 250);
                }
                Util.Global.Cam.Update();

                if (Util.Global.PreviousMouseState == ms)
                {
                    Util.Global.GameMouseTime = Util.Global.GameMouseTime + 1;
                }
                else
                {
                    Util.Global.PreviousMouseState = ms;
                    Util.Global.GameMouseTime = 0;
                }
                if (Util.Global.GameMouseTime > 100 || Util.Global.Sprites.Where(x => x.Item !=null && x.Item.State == Objects.Item.ItemState.Hand).Count() > 0)
                {
                    this.IsMouseVisible = false;
                }
                else
                {
                    this.IsMouseVisible = true;
                }


                //int mousex =   Util.Global.Sprites.Where(a => a.name == "mouse").FirstOrDefault().x;
                //int mousey =   Util.Global.Sprites.Where(a => a.name == "mouse").FirstOrDefault().y;
                //Util.Global.Texts.Where(x => x.name == "mouseCord").FirstOrDefault().text = string.Format("{0}:{1}", mousex.ToString(), mousey.ToString());
                //Util.Global.Texts.Where(x => x.name == "mouseCord").FirstOrDefault().x = mousex;
                //Util.Global.Texts.Where(x => x.name == "mouseCord").FirstOrDefault().y = mousey;
                Util.Global.PreviousKeyboardState = state;

                Actions.Season.UpdateClock(gameTime);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (RunFullScreenToggle)
            {
                graphics.ToggleFullScreen();
                RunFullScreenToggle = false;
            }

            GraphicsDevice.Clear(Color.Black);

            int DrawCount = 0;

            GraphicsDevice.Textures[1] = waterfallTexture;
            ripple.Parameters["DisplacementScroll"].SetValue(MoveInCircle(gameTime, 0.1f));

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Util.Global.Cam.Transform);
            //spriteBatch.Begin();
            //spriteBatch.Draw(Util.Global.GameBackground.model, new Rectangle(Util.Global.GameBackground.x, Util.Global.GameBackground.y, Util.Global.GameBackground.sizex, Util.Global.GameBackground.sizey), Color.White);
            foreach (Objects.Sprite2d S in Util.Global.Sprites.Where(x => x.effectType == Objects.Base.EffectType.None && x.active == true && x.Position.X < Util.Global.Hero.Position.X + drawDistanceWidth && x.Position.Y < Util.Global.Hero.Position.Y + drawDistanceHeight && x.Position.X > Util.Global.Hero.Position.X - drawDistanceWidth && x.Position.Y > Util.Global.Hero.Position.Y - drawDistanceHeight).OrderBy(O => O.orderNum).ToArray())
            {
                Color DrawColor = Util.Global.DayColor;
                if (Util.Global.DayColor.G < 152)
                {
                    foreach (Tuple<int, int, float> V in Util.Global.Lights)
                    {
                        if (Util.Base.collision(S.Position, V.Item3,new Rectangle((int)V.Item1, (int)V.Item2, 70, 70)))
                        {
                            DrawColor = Color.LightGray;
                        }
                        if (Util.Base.collision(S.Position, V.Item3 / 2f, new Rectangle((int)V.Item1, (int)V.Item2, 70, 70)))
                        {
                            DrawColor = Color.White;
                        }
                    }
                }
                if (S.LightIgnor == true)
                {
                    DrawColor = S.color;
                }
                S.Draw(GraphicsDevice, spriteBatch, DrawColor);
                DrawCount++;
            }
            spriteBatch.End();
            

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, ripple, Util.Global.Cam.Transform);
            foreach (Objects.Sprite2d S in Util.Global.Sprites.Where(x => x.effectType == Objects.Base.EffectType.Ripple && x.active == true && x.Position.X < Util.Global.Hero.Position.X + drawDistanceWidth && x.Position.Y < Util.Global.Hero.Position.Y + drawDistanceHeight && x.Position.X > Util.Global.Hero.Position.X - drawDistanceWidth && x.Position.Y > Util.Global.Hero.Position.Y - drawDistanceHeight).OrderBy(O => O.orderNum))
            {
                Color DrawColor = Util.Global.DayColor;
                if (Util.Global.DayColor.G < 152)
                {
                    foreach (Tuple<int, int, float> V in Util.Global.Lights)
                    {
                        if (Util.Base.collision(S.Position, V.Item3, new Rectangle((int)V.Item1, (int)V.Item2, 70, 70)))
                        {
                            DrawColor = Color.LightGray;
                        }
                        if (Util.Base.collision(S.Position, V.Item3 / 2f, new Rectangle((int)V.Item1, (int)V.Item2, 70, 70)))
                        {
                            DrawColor = Color.White;
                        }
                    }
                }
                if (S.LightIgnor == true)
                {
                    DrawColor = S.color;
                }
                S.Draw(GraphicsDevice, spriteBatch, DrawColor);
                DrawCount++;
            }
            spriteBatch.End();
            //Texture2D videoTexture = null;
            //if (player.State != MediaState.Stopped)
            //    videoTexture = player.GetTexture();
            //if (videoTexture != null)
            //{
            //    spriteBatch.Draw(videoTexture, new Rectangle(0, 0, 250, 150), Color.White);
            //}
            //DrawModel(model, world, view, projection);
            System.Diagnostics.Debug.WriteLine("DrawCount:" + DrawCount.ToString());
            base.Draw(gameTime);
        }

        private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                }

                mesh.Draw();
            }
        }

        static Vector2 MoveInCircle(GameTime gameTime, float speed)
        {
            double time = gameTime.TotalGameTime.TotalSeconds * speed;

            float x = (float)Math.Cos(time);
            float y = (float)Math.Sin(time);

            return new Vector2(x, y);
        }
    }
}
