using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Game.Objects
{
    public class Base
    {
        public enum ControlType {None,Mouse,Keyboard,AI};
        public enum Type { Tile, Pet, Hero, Item };
        public enum EffectType { None, Ripple };

        public Guid ID { get; set; }
        public Texture2D model { get; set; }
        public string name { get; set; }
        public string text { get; set; }
        public float textSize { get; set; }
        public Color boxColor { get; set; }
        public int orderNum { get; set; }
        public bool active { get; set; }
        public ControlType? controlType { get; set; }
        public Vector2 Position { get; set; }
        private Vector2 previousPosition { get; set; }
        public Vector2 Size { get; set; }
        public int speed { get; set; }
        public float Orientation { get; set; }
        public bool allowScroll { get; set; }
        public bool clipping { get; set; }
        public List<Actions.ActionCall> actionCall { get; set; }
        public bool LightIgnor { get; set; }
        public float LightSourceDistance { get; set; }
        public Color color { get; set; }
        public SoundEffect collisionSound { get; set; }
        public AnimSprite AnimSprite { get; set; }
        public Maneuver Maneuver { get; set; }
        public Actor Actor { get; set; }
        public Type SpriteType { get; set; }
        public Item Item { get; set; }
        public EffectType effectType { get; set; }

        public void KeyInput(KeyboardState keystate)
        {
            this.previousPosition = new Vector2((float)Position.X, (float)Position.Y);
            int changex = 0;
            int changey = 0;
            if (keystate.IsKeyDown(Keys.A))
            {
                left();
                Util.Global.Sprites.Where(x => x.name == "Hero").FirstOrDefault().model = Util.Global.HeroLeft;
                changex = 1;
            }
            if (keystate.IsKeyDown(Keys.D))
            {
                right();
                Util.Global.Sprites.Where(x => x.name == "Hero").FirstOrDefault().model = Util.Global.HeroRight;
                changex = -1;
            }
            if (keystate.IsKeyDown(Keys.W))
            {
                up();
                Util.Global.Sprites.Where(x => x.name == "Hero").FirstOrDefault().model = Util.Global.HeroUp;
                changey = 1;
            }
            if (keystate.IsKeyDown(Keys.S))
            {
                down();
                Util.Global.Sprites.Where(x => x.name == "Hero").FirstOrDefault().model = Util.Global.HeroDown;
                changey = -1;
            }
            if (ClipCheck())
            {
                this.Position = this.previousPosition;

                while (ClipCheck())
                {
                    this.Position = new Vector2(this.Position.X + changex, this.Position.Y + changey);
                }

            }
        }

        public void MouseInput(MouseState mousestate)
        {
            Matrix inverseViewMatrix = Matrix.Invert(Util.Global.Cam.Transform);
            Vector2 worldMousePosition = Vector2.Transform(new Vector2(mousestate.X, mousestate.Y), inverseViewMatrix);

            Position = new Vector2(worldMousePosition.X, worldMousePosition.Y);

        }

        public void Update(KeyboardState state, MouseState ms, GameTime gameTime)
        {
            if (gameTime.TotalGameTime.Milliseconds % 10 == 0 && Actor != null)
            {
                //Util.Global.counter++;
                //Util.Global.Texts.Where(x => x.name == "Counter").FirstOrDefault().text = "Count:" + Util.Global.counter.ToString();
                //Util.Global.Texts.Where(x => x.name == "Counter").FirstOrDefault().Position = Util.Global.Hero.Position;

                Sprite2d S = Util.Global.Sprites.Where(x => x.ID == ID).FirstOrDefault();
                if (S != null)
                {
                    Actor.Update(S);
                }
            }

            if (gameTime.TotalGameTime.Milliseconds % 100 == 0 && Maneuver != null && Maneuver.Movement.Count>0)
            {
                Position = new Vector2(Maneuver.Movement[Maneuver.MoveNum].X, Maneuver.Movement[Maneuver.MoveNum].Y);
                if (Maneuver.MovementSize != null)
                {
                    Size = new Vector2(Maneuver.MovementSize[Maneuver.MoveNum].X, Maneuver.MovementSize[Maneuver.MoveNum].Y);
                }
                if (Maneuver.ColorType != Util.ColorType.None)
                { color = Util.Colors.GetColor(Maneuver.ColorType); }

                Maneuver.MoveNum++;
                if (Maneuver.MoveNum >= Maneuver.Movement.Count)
                {
                    if (Maneuver.FinalActionCall != null)
                    {
                        Util.Base.CallMethod(Maneuver.FinalActionCall);
                    }
                    if (!Maneuver.Repeat)
                    {
                        Position = Maneuver.OriginalVector;
                        Size = Maneuver.OriginalSize;
                        Maneuver = null;
                        if (Actor == null)
                        {
                            active = false;
                            Util.Global.Sprites.RemoveAll(x => x.ID == ID);
                        }
                    }
                    else
                    {
                        Maneuver.MoveNum = 0;
                    }
                }
            }
            
            if (AnimSprite != null && !Util.Global.Fighting && name !="Hero")
            {
                if (gameTime.TotalGameTime.Milliseconds % 100 == 0)
                {
                    AnimSprite.Update();
                }
            }

            if (AnimSprite != null && gameTime.TotalGameTime.Milliseconds % 100 == 0 && (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.D)))
            { AnimSprite.Update(); }

            if ((ms.LeftButton == ButtonState.Pressed && Util.Global.PreviousMouseState.LeftButton == ButtonState.Released) || (ms.RightButton == ButtonState.Pressed && Util.Global.PreviousMouseState.RightButton == ButtonState.Released))
            {
                Vector2 MP = Util.Global.GetTrueMousePosition();
                Rectangle rect1 = new Rectangle((int)MP.X, (int)MP.Y, 5, 5);
                Rectangle rect2 = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
                if (Util.Base.collision(rect1,rect2))
                {
                    if (ms.LeftButton == ButtonState.Pressed)
                    {
                        foreach (Actions.ActionCall AC in actionCall.Where(x => x.ActionType == Actions.ActionType.Mouse).ToList())
                        {
                            Util.Base.CallMethodByString(AC.Type, AC.actionMethodName, AC.parameters);
                        }
                        if (AnimSprite != null)
                        {
                            AnimSprite.action = true;
                        }
                    }
                    if (ms.RightButton == ButtonState.Pressed)
                    {
                        foreach (Actions.ActionCall AC in actionCall.Where(x => x.ActionType == Actions.ActionType.MouseRight).ToList())
                        {
                            Util.Base.CallMethodByString(AC.Type, AC.actionMethodName, AC.parameters);
                        }
                    }
                }
            }

            if (!Util.Global.Fighting)
            {
                if (controlType == Objects.Base.ControlType.Keyboard)
                { KeyInput(state); }
                if (controlType == Objects.Base.ControlType.Mouse)
                { MouseInput(ms); }
            }

            if (speed > 0 && gameTime.TotalGameTime.Milliseconds % speed == 0)
            {
                foreach (Actions.ActionCall AC in actionCall.Where(x => x.ActionType == Actions.ActionType.Update).ToList())
                {
                    Util.Base.CallMethodByString(AC.Type, AC.actionMethodName, AC.parameters);
                }
            }

            if (ID != Util.Global.Hero.ID && !Util.Global.Fighting)
            {
                    foreach (Actions.ActionCall AC in actionCall.Where(x => x.ActionType == Actions.ActionType.Collision).ToList())
                    {
                        if (Util.Base.collision(Util.Global.Hero, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y)))
                        {

                        Util.Base.CallMethodByString(AC.Type, AC.actionMethodName, AC.parameters);
                        if (collisionSound != null)
                        { collisionSound.Play(); }
                        }

                    }
            }
        }

        public void Draw(GraphicsDevice graphics, SpriteBatch spriteBatch, Color color)
        {
            if(model != null)
            {
                if (AnimSprite != null)
                {
                    int width = model.Width / AnimSprite.Rows;
                    int height = model.Height / AnimSprite.Columns;
                    int row = (int)((float)AnimSprite.currentFrame / (float)AnimSprite.Rows);
                    int column = AnimSprite.currentFrame % AnimSprite.Rows;

                    Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
                    Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
                    spriteBatch.Draw(model, destinationRectangle, sourceRectangle, color);
                }
                else
                {
                    if (Orientation > 0 && Actor == null)
                    {
                        spriteBatch.Draw(model, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), null, color, Orientation, Size, SpriteEffects.None, 0f); 
                    }
                    else
                    {
                        spriteBatch.Draw(model, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), color);
                    }
                }
            }
            if (text != null)
            {
                if (boxColor != null)
                {
                    Texture2D Box;
                    Vector2 TextSize = Util.Global.font.MeasureString(text);
                    TextSize = Vector2.Multiply(TextSize, textSize);
                    TextSize = Vector2.Add(TextSize, new Vector2(6,6));
                    Size = TextSize;
                    Box = new Texture2D(graphics, 1, 1);
                    Box.SetData(new[] { Color.White });
                    spriteBatch.Draw(Box, new Rectangle((int)Position.X-3, (int)Position.Y-3, (int)TextSize.X, (int)TextSize.Y), boxColor);
                }
                spriteBatch.DrawString(Util.Global.font, text, Position, color, 0f, new Vector2(0, 0), textSize, new SpriteEffects(), 1.0f);
            }
        }

        public void left()
        {
            Position = new Vector2(Position.X - speed, Position.Y);
            
        }
        public void right()
        {
            Position = new Vector2(Position.X + speed, Position.Y);
        }
        public void up()
        {
            Position = new Vector2(Position.X, Position.Y - speed);
        }
        public void down()
        {
            Position = new Vector2(Position.X, Position.Y + speed);
        }

        public void moveRandom()
        {
            int dir = Util.Global.GetRandomInt(1, 13);
            switch (dir)
            {
                case 1:
                    up();
                    break;
                case 2:
                    down();
                    break;
                case 3:
                    left();
                    break;
                case 4:
                    right();
                    break;
            }
        }

        public bool ClipCheck()
        {
            bool collideflag = false;
            if (clipping == true)
            {
                foreach (Objects.Sprite2d S in Util.Global.Sprites.Where(G => G.clipping == true && G.ID != ID && G.name != "Hero").ToList())
                {
                    Rectangle rect1 = new Rectangle((int)S.Position.X, (int)S.Position.Y, (int)S.Size.X, (int)S.Size.Y);
                    Rectangle rect2 = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
                    if (rect1.Intersects(rect2))
                    {
                        collideflag = true;
                    }
                }
            }
            return collideflag;
        }

        public void FlipClip(Sprite2d S)
        {
            if (S.AnimSprite.action == false)
            { S.clipping = !S.clipping; }
        }
    }
}
