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
        //private int drawDistanceWidth = 100;
        //private int drawDistanceHeight = 300;
        private Model model;
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
            Util.Global.Cam = new Util.Camera2D(GraphicsDevice.Viewport);
            GraphicsDevice.Clear(Color.Black);
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
            try
            {

                if (Util.Global.QuitGameToggle)
                {
                    this.Exit();
                }

                KeyboardState state = Keyboard.GetState();
                MouseState ms = Mouse.GetState();
                Util.GameKey.Key(state);

                foreach (Objects.Sprite2d A in Util.Global.Sprites.Where(x => x.active == true && x.controlType == Objects.Base.ControlType.Mouse))
                {
                    A.MouseInput(ms);
                }

                if (Util.Global.PreviousMouseState == ms)
                {
                    Util.Global.GameMouseTime = Util.Global.GameMouseTime + 1;
                }
                else
                {
                    if ((ms.LeftButton == ButtonState.Pressed && Util.Global.PreviousMouseState.LeftButton == ButtonState.Released) || (ms.RightButton == ButtonState.Pressed && Util.Global.PreviousMouseState.RightButton == ButtonState.Released))
                    {
                        Util.GameMouse.MouseClick(ms);
                    }
                    Util.Global.PreviousMouseState = ms;
                    Util.Global.GameMouseTime = 0;
                }
                this.IsMouseVisible = true;
                if (this.IsActive && Util.Global.GameMouseTime > 200)
                {
                    this.IsMouseVisible = false;
                }
                if (Util.Global.Sprites.Where(x => x.Item != null && x.Item.State == Items.Item.ItemState.Hand).Count() > 0)
                {
                    this.IsMouseVisible = false;
                }


                if (Util.Global.Fighting == false)
                {
                    Util.Global.Cam.Pos = (Util.Global.Hero.Position * -1) + new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
                }
                Util.Global.Cam.Update();

                Util.Global.PreviousKeyboardState = state;

                if (!Util.Global.PauseGameToggle)
                {
                    Actions.Season.UpdateClock(gameTime);

                    Objects.Sprite2d[] SIRcheck = Util.Global.Sprites.Where(l => l.actionCall != null && l.actionCall.Any(y => y.ActionType == Actions.ActionType.MouseCollision)).ToArray();

                    int updateCount = 0;
                    //Objects.Sprite2d[] SIR = Util.Global.Sprites.Where(r => (r.Actor != null) || (r.actionCall.Count() > 0) || (r.Maneuver != null) || (r.Position.X < Util.Global.Hero.Position.X + drawDistanceWidth && r.Position.Y < Util.Global.Hero.Position.Y + drawDistanceHeight && r.Position.X > Util.Global.Hero.Position.X - drawDistanceWidth && r.Position.Y > Util.Global.Hero.Position.Y - drawDistanceHeight)).ToArray();
                    Objects.Sprite2d[] SIR = Util.Global.Sprites.Where(r => (r.Actor != null) || (r.actionCall.Count() > 0) || (r.Maneuver != null) || Vector2.Distance(r.Position, Util.Global.Hero.Position) < Util.Global.DrawDistance).ToArray();
                    Objects.Sprite2d[] SIR2 = SIR.Where(x => x.active == true && (x.AnimSprite != null && x.AnimSprite.action == true) || (x.actionCall.Count() > 0) || (x.Maneuver != null) || (x.Actor != null)).ToArray().OrderByDescending(y => y.orderNum).ToArray();
                    foreach (Objects.Sprite2d A in SIR2)
                    {
                        A.Update(state, gameTime);
                        updateCount++;
                        //Util.Base.Log("updateCount:" + updateCount.ToString());
                        //Util.Base.Log(A.name + " " + A.Position.X.ToString() + ":" + A.Position.Y.ToString());
                    }

                }
                base.Update(gameTime);
            }
            catch (Exception ex)
            {
                Util.Base.Log("Main Update Failure: "+ ex.Message);
            }
            
        }

        protected override void Draw(GameTime gameTime)
        {
            try
            {
                GraphicsDevice.Clear(Color.Black);
                if (Util.Global.ScreenShotFlag)
                {
                    GraphicsDevice.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;
                    GraphicsDevice.PrepareScreenShot();
                }

                Util.Global.screenSizeWidth = GraphicsDevice.Viewport.Width;
                Util.Global.screenSizeHeight = GraphicsDevice.Viewport.Height;
                //drawDistanceWidth = Util.Global.screenSizeWidth;
                //drawDistanceHeight = Util.Global.screenSizeHeight;
                if (Util.Global.RunFullScreenToggle)
                {
                    graphics.ToggleFullScreen();
                    Util.Global.RunFullScreenToggle = false;
                }
                if (Util.Global.Fighting)
                {
                    //drawDistanceWidth = drawDistanceWidth + 100;
                    //drawDistanceHeight = drawDistanceHeight + 100;
                }

                int DrawCount = 0;

                GraphicsDevice.Textures[1] = waterfallTexture;
                ripple.Parameters["DisplacementScroll"].SetValue(MoveInCircle(gameTime, 0.1f));
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, ripple, Util.Global.Cam.Transform);
                foreach (Objects.Sprite2d S in Util.Global.Sprites.Where(x => x.effectType == Objects.Base.EffectType.Ripple && x.active == true && x.Viewtype == Objects.Base.ViewType.Default && Vector2.Distance(x.Position, Util.Global.Hero.Position) < Util.Global.DrawDistance).OrderBy(O => O.orderNum))
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



                




                //GraphicsDevice.PrepareScreenShot();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Util.Global.Cam.Transform);
                //Util.Global.ScreenShotRender = new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
                //GraphicsDevice.SetRenderTarget(Util.Global.ScreenShotRender);

                //spriteBatch.Begin();
                //spriteBatch.Draw(Util.Global.GameBackground.model, new Rectangle(Util.Global.GameBackground.x, Util.Global.GameBackground.y, Util.Global.GameBackground.sizex, Util.Global.GameBackground.sizey), Color.White);
                //foreach (Objects.Sprite2d S in Util.Global.Sprites.Where(x => x.effectType == Objects.Base.EffectType.None && x.active == true && x.Viewtype == Objects.Base.ViewType.Default && x.Position.X < Util.Global.Hero.Position.X + drawDistanceWidth && x.Position.Y < Util.Global.Hero.Position.Y + drawDistanceHeight && x.Position.X > Util.Global.Hero.Position.X - drawDistanceWidth && x.Position.Y > Util.Global.Hero.Position.Y - drawDistanceHeight).OrderBy(O => O.orderNum).ToList())
                foreach (Objects.Sprite2d S in Util.Global.Sprites.Where(x => x.effectType == Objects.Base.EffectType.None && x.active == true && x.Viewtype == Objects.Base.ViewType.Default && Vector2.Distance(x.Position, Util.Global.Hero.Position) < Util.Global.DrawDistance).OrderBy(O => O.orderNum).ToList())
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
                    else if (S.color != Color.White && S.modelname != null)
                    {
                        DrawColor = S.color;
                        if (S.color.R > Util.Global.DayColor.R)
                        {
                            DrawColor.R = Util.Global.DayColor.R;
                        }
                        if (S.color.G > Util.Global.DayColor.G)
                        {
                            DrawColor.G = Util.Global.DayColor.G;
                        }
                        if (S.color.B > Util.Global.DayColor.B)
                        {
                            DrawColor.B = Util.Global.DayColor.B;
                        }
                    }
                    S.Draw(GraphicsDevice, spriteBatch, DrawColor);
                    DrawCount++;
                }
                spriteBatch.End();

                //------------------------------
                Viewport OriVP = GraphicsDevice.Viewport;
                GraphicsDevice.Viewport = new Viewport(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
                spriteBatch.Begin();
                foreach (Objects.Sprite2d S in Util.Global.Sprites.Where(x => x.active == true && x.Viewtype == Objects.Base.ViewType.HUD).OrderBy(O => O.orderNum).ToList())
                {
                    S.Draw(GraphicsDevice, spriteBatch, S.color);
                }
                spriteBatch.End();
                GraphicsDevice.Viewport = OriVP;
                //------------------------------

                //Texture2D videoTexture = null;
                //if (player.State != MediaState.Stopped)
                //    videoTexture = player.GetTexture();
                //if (videoTexture != null)
                //{
                //    spriteBatch.Draw(videoTexture, new Rectangle(0, 0, 250, 150), Color.White);
                //}
                //DrawModel(model, world, view, projection);
                //System.Diagnostics.Debug.WriteLine("DrawCount:" + DrawCount.ToString());

                base.Draw(gameTime);
                Util.Global.graphicsDevice = GraphicsDevice;

                if (Util.Global.ScreenShotFlag)
                {
                    Util.Global.ScreenShotFlag = false;
                    if (!string.IsNullOrEmpty(Util.Global.ScreenShotName))
                    {
                        GraphicsDevice.SaveScreenshot(Util.Global.ScreenShotName);
                        Util.Global.ScreenShotName = "";
                    }
                    else
                    {
                        GraphicsDevice.SaveScreenshot();
                    }

                    GraphicsDevice.SetRenderTarget(null);
                }
            }
            catch (Exception ex)
            {
                Util.Base.Log("Main Draw Fail: "+ex.Message);
            }
            
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
