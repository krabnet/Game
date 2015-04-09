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
        private int previousScrollValue;
        private SpriteFont font;
        private Model model;
        private Util.Camera2D Cam;
        Actions.RollDice RD = new Actions.RollDice();
        Util.Base Utils = new Util.Base();
        SoundEffect soundEffect;
        protected Song song;
        private bool RunFullScreenToggle = false;
        private bool GamePaused = false;
        private Objects.Spite2d Background = new Objects.Spite2d();
        private Color DayColor;
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
        }

        protected override void Initialize()
        {
            //video = Content.Load<Video>("Wildlife");
            //player = new VideoPlayer();

            song = Content.Load<Song>("Music/town");  // Put the name of your song here instead of "song_title"
            MediaPlayer.Play(song);
            soundEffect = Content.Load<SoundEffect>("Sounds/EscapeMenu");
            previousScrollValue = 0;
            Cam = new Util.Camera2D(GraphicsDevice.Viewport);
            //SpeechSynthesizer synth = new SpeechSynthesizer();
            //synth.SetOutputToDefaultAudioDevice();
            //synth.Speak("Welcome to the game!");
            new Maps.Asset().PopulateContent(Content);
            new Objects.Menu().DefaultMenu();
            new Actions.Fight().init();
            Util.Global.MainMap = new Maps.Map[100, 100, 100];

            Maps.Map Smap = new Maps.Map();
            Smap.GenerateBaseMap(new Vector3(5,5,0));
            Smap.AddMapPart(Maps.MapPart.courtyard, 10, 10);
            Smap.AddMapPart(Maps.MapPart.room, 2, 2);
            //Smap.AddMapPart(Maps.MapPart.stream, 20, 10);
            Maps.Map Smap2 = new Maps.Map();
            Smap2.GenerateBaseMap(new Vector3(6, 5, 0));
            Smap2.AddMapPart(Maps.MapPart.stream, 10, 10);

            new Maps.Map().LoadMapByInt(5, 5, 0);

            DayColor = Color.White;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Font/Font1");
            model = Content.Load<Model>("3dModel/cube");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            MouseState ms = Mouse.GetState();

            //if (player.State == MediaState.Stopped)
            //{
            //    player.IsLooped = true;
            //    player.Play(video);
            //}

            //new Util.Camera2D(GraphicsDevice.Viewport).Update();

            if(state.IsKeyDown(Keys.Escape))
            {
                System.Threading.Thread.Sleep(200);
                if (Util.Global.Menus.Where(x => x.name == "Exit").FirstOrDefault().active == true)
                {
                    GamePaused = false;
                    Util.Global.Menus.Where(x => x.name == "Exit").FirstOrDefault().active = false;
                    Util.Global.Menus.Where(x => x.name == "FullScreen").FirstOrDefault().active = false;
                    soundEffect.Play();
                    MediaPlayer.Resume();
                    new Actions.Inventory().HideInventory();
                }
                else
                {
                    GamePaused = true;
                    Util.Global.Menus.Where(x => x.name == "Exit").FirstOrDefault().active = true;
                    Util.Global.Menus.Where(x => x.name == "FullScreen").FirstOrDefault().active = true;
                    soundEffect.Play();
                    MediaPlayer.Pause();
                    new Actions.Inventory().ShowInventory();
                }
            }

            Objects.Menu returnMenu = new Objects.Menu();
            Objects.AnimSprite returnSprite = new Objects.AnimSprite();
            if (ms.LeftButton == ButtonState.Pressed || ms.RightButton == ButtonState.Pressed)
            {
                returnMenu = Utils.GetMenuClick(Util.Global.GameMouse);
               if (returnMenu.name == "Exit")
               { this.Exit(); }
               if (returnMenu.name == "FullScreen")
               {
                   RunFullScreenToggle = true;
               }
               if (returnMenu.actionCall != null)
               {
                   returnMenu.boxColor = Color.Red;
                   Utils.CallMethodByString(returnMenu.actionCall.actionType, returnMenu.actionCall.actionMethodName, returnMenu.actionCall.parameters);
               }

               returnSprite = Utils.GetAnimatedSpriteClick(Util.Global.GameMouse);
               if (returnSprite.name != null && !GamePaused && returnSprite.actionType == Objects.Base.ActionType.Mouse)
               {
                   if (returnSprite.actionCall != null)
                   {
                       Utils.CallMethodByString(returnSprite.actionCall.actionType, returnSprite.actionCall.actionMethodName, returnSprite.actionCall.parameters);
                   }
                   returnSprite.action = true;
               }

               Objects.Spite2d return2dSprite = Utils.GetSpriteClick(Util.Global.GameMouse);
               if (return2dSprite.name != null && !GamePaused && return2dSprite.actionType == Objects.Base.ActionType.Mouse)
               {
                   if (return2dSprite.actionCall != null)
                   {
                       Utils.CallMethodByString(return2dSprite.actionCall.actionType, return2dSprite.actionCall.actionMethodName, return2dSprite.actionCall.parameters);
                   }
                   if (return2dSprite.actionCallB != null && ms.RightButton == ButtonState.Pressed)
                   {
                       Utils.CallMethodByString(return2dSprite.actionCallB.actionType, return2dSprite.actionCallB.actionMethodName, return2dSprite.actionCallB.parameters);
                   }
                   //return2dSprite.action = true;
               }

            }
            Util.Global.GameMouse.MouseInput(ms);
            if (!GamePaused)
            {
                foreach (Actions.Anim a in Util.Global.Anim.Where(x => x.active == true).ToList())
                {
                    if (gameTime.TotalGameTime.Milliseconds % 100 == 0)
                    {
                        a.update();
                    }
                }
                
                foreach (Objects.Spite2d m in Util.Global.Sprites.Where(x => x.active == true).ToList())
                {
                    if (m.controlType == Objects.Base.ControlType.Keyboard)
                    { m.KeyInput(state); }
                    if (m.controlType == Objects.Base.ControlType.Mouse)
                    { m.MouseInput(ms); }
                    if (m.actionCall != null)
                    {
                        if (m.actionType == Objects.Base.ActionType.Collision)
                        {
                            if (Utils.collision(m, Util.Global.Hero.HeroSprite))
                            {
                                Utils.CallMethodByString(m.actionCall.actionType, m.actionCall.actionMethodName, m.actionCall.parameters);
                                if (m.collisionSound != null)
                                { m.collisionSound.Play(); }
                            }
                        }
                    }
                }
                foreach (Objects.Text m in Util.Global.Texts.Where(x => x.active == true).ToList())
                {
                    if (m.controlType == Objects.Base.ControlType.Keyboard)
                    { m.KeyInput(state); }
                    if (m.controlType == Objects.Base.ControlType.Mouse)
                    { m.MouseInput(ms); }
                }

                foreach (Objects.AnimSprite m in Util.Global.SpritesAnim.Where(x => x.action == true && x.name != "Hero").ToList())
                {
                    if (gameTime.TotalGameTime.Milliseconds % m.speed == 0)
                    {
                        m.Update();
                    }
                }

                foreach (Objects.Spite2d m in Util.Global.Sprites.Where(x => x.actionType == Objects.Base.ActionType.Update && x.active == true).ToList())
                {
                    if (gameTime.TotalGameTime.Milliseconds % m.speed == 0 && m.actionCall != null)
                    {
                        Utils.CallMethodByString(m.actionCall.actionType, m.actionCall.actionMethodName, m.actionCall.parameters);
                    }
                }
                foreach (Objects.AnimSprite m in Util.Global.SpritesAnim.Where(x => x.actionType == Objects.Base.ActionType.Update && x.active == true).ToList())
                {
                    if (gameTime.TotalGameTime.Milliseconds % m.speed == 0 && m.actionCall != null)
                    {
                        Utils.CallMethodByString(m.actionCall.actionType, m.actionCall.actionMethodName, m.actionCall.parameters);
                    }
                }


                if (!Util.Global.Fighting && (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.D)))
                {
                    if (gameTime.TotalGameTime.Milliseconds % 100 == 0)
                    { Util.Global.Hero.HeroSprite.Update(); }
                    Util.Global.Hero.HeroSprite.KeyInput(state);
                }

                if (ms.ScrollWheelValue < previousScrollValue)
                {
                    //new Util.Base().SizeTiles(-5);
                    Cam.Zoom -= 0.1f;
                    //Util.Global.WindowBoarder += 50;

                }
                if (ms.ScrollWheelValue > previousScrollValue)
                {
                    //new Util.Base().SizeTiles(5);
                    Cam.Zoom += 0.1f;
                    //Util.Global.WindowBoarder -= 50;
                }
                previousScrollValue = ms.ScrollWheelValue;
                //Cam.Pos = new Vector2(GameHero.x*-1,GameHero.y*-1);
                Cam.Update();

                 //int mousex =   Util.Global.Sprites.Where(a => a.name == "mouse").FirstOrDefault().x;
                 //int mousey =   Util.Global.Sprites.Where(a => a.name == "mouse").FirstOrDefault().y;
                 //Util.Global.Texts.Where(x => x.name == "mouseCord").FirstOrDefault().text = string.Format("{0}:{1}", mousex.ToString(), mousey.ToString());
                 //Util.Global.Texts.Where(x => x.name == "mouseCord").FirstOrDefault().x = mousex;
                 //Util.Global.Texts.Where(x => x.name == "mouseCord").FirstOrDefault().y = mousey;
            }
           
            //SpritesAnim.FirstOrDefault().KeyInput(state);
            //if (gameTime.TotalGameTime.Milliseconds % 10 == 0)
            //{ SpritesAnim.FirstOrDefault().Update(); }
            //int Tilex = Sprites.Where(x => x.name == "Tile").FirstOrDefault().x;
            //int Tiley = Sprites.Where(x => x.name == "Tile").FirstOrDefault().y;
            //Texts.Where(x => x.name == "TileText").FirstOrDefault().vector = new Vector2(Tilex, Tiley);
            //Texts.Where(x => x.name == "TileText").FirstOrDefault().text = string.Format("{0}/{1}", Tilex.ToString(), Tiley.ToString());
            //Texts.Where(x => x.name == "GameTime").FirstOrDefault().text = string.Format("{0}",gameTime.TotalGameTime.Seconds);

            //if(ms.LeftButton == ButtonState.Pressed)
            //{
            //    RD.Roll(gameTime.TotalGameTime.Seconds);
            //}

            //RD.update(gameTime);


            //explosion.Update();
            //position += new Vector3(0, 0.01f, 0);
            //angle += 0.03f;
            //world = Matrix.CreateRotationY(angle) * Matrix.CreateTranslation(position);
            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            //    this.Exit();
            Util.Global.GameClock = gameTime.TotalGameTime;
            float F = Util.Global.GameClock.Seconds / 20.0f ;
            DayColor = LerpColor(Color.White, Color.Gray,F);
            if (F >= .8)
            {
                Util.Global.GameClock = new TimeSpan(0);
            }

            base.Update(gameTime);
        }

        private Color LerpColor(Color a, Color b, float percentage)
        {
            return new Color(
                (byte)MathHelper.Lerp(a.R, b.R, percentage),
                (byte)MathHelper.Lerp(a.G, b.G, percentage),
                (byte)MathHelper.Lerp(a.B, b.B, percentage),
                (byte)MathHelper.Lerp(a.A, b.A, percentage));
        }

        protected override void Draw(GameTime gameTime)
        {
            if (RunFullScreenToggle)
            {
                graphics.ToggleFullScreen();
                RunFullScreenToggle = false;
            }

            GraphicsDevice.Clear(Color.Black);
            List<object> AllObjects = new List<object>();
            AllObjects.AddRange(Util.Global.Sprites);
            AllObjects.AddRange(Util.Global.Texts);
            AllObjects.AddRange(Util.Global.Menus);
            AllObjects.AddRange(Util.Global.SpritesAnim);

            int drawDistance = 1000;

            AllObjects = AllObjects.Where(x => (int)x.GetType().GetProperty("x").GetValue(x, null) < Util.Global.Hero.HeroSprite.x + drawDistance && (int)x.GetType().GetProperty("y").GetValue(x, null) < Util.Global.Hero.HeroSprite.y + drawDistance).ToList();
            AllObjects = AllObjects.Where(x => (int)x.GetType().GetProperty("x").GetValue(x, null) > Util.Global.Hero.HeroSprite.x - drawDistance && (int)x.GetType().GetProperty("y").GetValue(x, null) > Util.Global.Hero.HeroSprite.y - drawDistance).ToList();

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Cam.Transform);
            //spriteBatch.Begin();
            spriteBatch.Draw(Util.Global.GameBackground.model, new Rectangle(Util.Global.GameBackground.x, Util.Global.GameBackground.y, Util.Global.GameBackground.sizex, Util.Global.GameBackground.sizey), Color.White);
            foreach (object item in AllObjects.Where(y => (bool)y.GetType().GetProperty("active").GetValue(y,null) == true).OrderBy(x => x.GetType().GetProperty("orderNum").GetValue(x,null)))
            {
                if (item.GetType() == typeof(Objects.Menu))
                {
                    Objects.Menu M = (Objects.Menu)item;
                    Texture2D MenuItem;
                    MenuItem = new Texture2D(GraphicsDevice, 1, 1);
                    MenuItem.SetData(new[] { Color.White });
                    spriteBatch.Draw(MenuItem, new Rectangle(M.x, M.y, M.boxWidth, M.boxHeight), M.boxColor);
                    spriteBatch.DrawString(font, M.text, new Vector2(M.x + 15, M.y), M.color);
                }
                if (item.GetType() == typeof(Objects.Spite2d))
                {
                    Objects.Spite2d S = (Objects.Spite2d)item;
                    //spriteBatch.Draw(S.model, new Rectangle(S.x, S.y, S.sizex, S.sizey), DayColor);
                    spriteBatch.Draw(S.model, new Rectangle(S.x, S.y, S.sizex, S.sizey), Color.White);
                }
                if (item.GetType() == typeof(Objects.Text))
                {
                    Objects.Text T = (Objects.Text)item;
                    spriteBatch.DrawString(font, T.text, new Vector2(T.x, T.y), T.color, 0f, new Vector2(0, 0), 0.5f, new SpriteEffects(), 1.0f);
                }
                if (item.GetType() == typeof(Objects.AnimSprite))
                {
                    Objects.AnimSprite A = (Objects.AnimSprite)item;
                    A.Draw(spriteBatch);
               }
            }
            
            

            //Texture2D videoTexture = null;
            //if (player.State != MediaState.Stopped)
            //    videoTexture = player.GetTexture();
            //if (videoTexture != null)
            //{
            //    spriteBatch.Draw(videoTexture, new Rectangle(0, 0, 250, 150), Color.White);
            //}


            //DrawModel(model, world, view, projection);
            spriteBatch.End();
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
    }
}
