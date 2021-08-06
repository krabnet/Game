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
    [Serializable]
    public class Base
    {
        public enum ControlType {None,Mouse,Keyboard,AI};
        public enum Type { Tile, Pet, Hero, Item };
        public enum EffectType { None, Ripple };
        public enum ViewType { Default, HUD };

        public Guid ID { get; set; }
        public string modelname { get; set; }
        public string name { get; set; }
        public string text { get; set; }
        public float textSize { get; set; }
        public Color boxColor { get; set; }
        public int orderNum { get; set; }
        public bool active { get; set; }
        public ViewType Viewtype { get; set; }
        public ControlType? controlType { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 previousPosition { get; set; }
        public Vector2 Size { get; set; }
        public int SizeScale { get; set; }
        public int speed { get; set; }
        public float Orientation { get; set; }
        public bool allowScroll { get; set; }
        public bool clipping { get; set; }
        public List<Actions.ActionCall> actionCall { get; set; }
        public bool LightIgnor { get; set; }
        public float LightSourceDistance { get; set; }
        public Color color { get; set; }
        public string collisionSound { get; set; }
        public AnimSprite AnimSprite { get; set; }
        public Maneuver Maneuver { get; set; }
        public Actor Actor { get; set; }
        public Type SpriteType { get; set; }
        public Item Item { get; set; }
        public List<Item> Inventory { get; set; }
        public EffectType effectType { get; set; }

        public void KeyInput(KeyboardState keystate)
        {
            this.previousPosition = new Vector2((float)Position.X, (float)Position.Y);
            int changex = 0;
            int changey = 0;
            if (keystate.IsKeyDown(Keys.A))
            {
                left();
                Util.Global.Sprites.Where(x => x.name == "Hero").FirstOrDefault().modelname = "HeroLeft";
                changex = 1;
            }
            if (keystate.IsKeyDown(Keys.D))
            {
                right();
                Util.Global.Sprites.Where(x => x.name == "Hero").FirstOrDefault().modelname = "HeroRight";
                changex = -1;
            }
            if (keystate.IsKeyDown(Keys.W))
            {
                up();
                Util.Global.Sprites.Where(x => x.name == "Hero").FirstOrDefault().modelname = "HeroUp";
                changey = 1;
            }
            if (keystate.IsKeyDown(Keys.S))
            {
                down();
                Util.Global.Sprites.Where(x => x.name == "Hero").FirstOrDefault().modelname = "HeroDown";
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
            //Matrix inverseViewMatrix = Matrix.Invert(Util.Global.Cam.Transform);
            //Vector2 worldMousePosition = Vector2.Transform(new Vector2(mousestate.X, mousestate.Y), inverseViewMatrix);

            ////Position = new Vector2(worldMousePosition.X, worldMousePosition.Y);
            //Position = Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.Default);
            //if (Viewtype == ViewType.HUD)
            //{
            //    Position = new Vector2(mousestate.X, mousestate.Y);
            //}
            
            Position = Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.Default);
            if (Viewtype == ViewType.HUD)
            {
                Position = new Vector2(mousestate.X, mousestate.Y);
            }

        }

        public void Update(KeyboardState state, GameTime gameTime)
        {
            try
            {

                if (Util.Global.UpdateClockPreviousMili != gameTime.TotalGameTime.Milliseconds)
                {
                    Util.Global.UpdateClockPreviousMili = gameTime.TotalGameTime.Milliseconds;
                    Util.Global.UpdateClock++;
                    if (Util.Global.UpdateClock >= 1000)
                    {
                        Util.Global.UpdateClock = 0;
                    }
                }

                if (Util.Global.UpdateClock % 4 == 0 && Actor != null)
                {
                    Sprite2d S = Util.Global.Sprites.Where(x => x.ID == ID).FirstOrDefault();
                    if (S != null)
                    {
                        Actor.Update(S);
                    }

                    if (AnimSprite != null && (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.D)))
                    { AnimSprite.Update(); }

                }

                if (AnimSprite != null && Actor == null && Util.Global.UpdateClock % speed == 0)
                {
                    AnimSprite.Update();
                }

                if (speed > 0 && Util.Global.UpdateClock % speed == 0)
                {
                    foreach (Actions.ActionCall AC in actionCall.Where(x => x.ActionType == Actions.ActionType.Update).ToList())
                    {
                        Util.Base.CallMethodByString(AC.Type, AC.actionMethodName, AC.parameters);
                    }
                }

                if (speed > 0 && Util.Global.UpdateClock % speed == 0 && Maneuver != null && Maneuver.Movement.Count > 0 && Maneuver.Draw)
                {
                    Vector2 NewPosition = new Vector2(Maneuver.Movement[Maneuver.MoveNum].X, Maneuver.Movement[Maneuver.MoveNum].Y);
                    Objects.Sprite2d AddDraw = new Sprite2d(modelname, name, active, NewPosition, Size, 0, (ControlType)controlType);
                    AddDraw.color = color;
                    Util.Global.Sprites.Add(AddDraw);


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
                            if (Maneuver.ReturnToOriginalPosition)
                            {
                                Position = Maneuver.OriginalVector;
                                Size = Maneuver.OriginalSize;
                            }
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

                if (Util.Global.UpdateClock % 4 == 0 && Maneuver != null && Maneuver.Movement.Count > 0 && !Maneuver.Draw)
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
                            if (Maneuver.ReturnToOriginalPosition)
                            {
                                Position = Maneuver.OriginalVector;
                                Size = Maneuver.OriginalSize;
                            }
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
                if (!Util.Global.Fighting)
                {
                    if (controlType == Objects.Base.ControlType.Keyboard)
                    { KeyInput(state); }
                }

                if (ID != Util.Global.Hero.ID && !Util.Global.Fighting && active == true)
                {
                    foreach (Actions.ActionCall AC in actionCall.Where(x => x.ActionType == Actions.ActionType.Collision).ToList())
                    {
                        if (Util.Base.collision(Util.Global.Hero, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y)))
                        {
                            Util.Base.CallMethodByString(AC.Type, AC.actionMethodName, AC.parameters);
                        }
                    }
                }

                if (name == "fish")
                {
                    List<Sprite2d> fishlines = Util.Global.Sprites.Where(x => x.name == "FishLine").ToList();

                    foreach (Actions.ActionCall AC in actionCall.Where(x => x.ActionType == Actions.ActionType.FishCollision).ToList())
                    {
                        foreach (Sprite2d S in fishlines)
                        {
                            if (Util.Base.collision(S, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y)))
                            {
                                Util.Base.CallMethodByString(AC.Type, AC.actionMethodName, AC.parameters);
                            }
                        }
                    }
                }


                foreach (Actions.ActionCall AC in actionCall.Where(x => x.ActionType == Actions.ActionType.MouseCollision).ToList())
                {
                    Vector2 MousePos = Util.GameMouse.GetTrueMousePosition(ViewType.HUD);
                    if (Util.Base.collision(new Rectangle((int)MousePos.X, (int)MousePos.Y, 10, 10), new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y)))
                    {
                        Util.Base.CallMethodByString(AC.Type, AC.actionMethodName, AC.parameters);
                    }
                }
            }
            catch (Exception ex)
            {
                Util.Base.Log("Update Failure: " + ex.Message);
            }
        }

        public void Draw(GraphicsDevice graphics, SpriteBatch spriteBatch, Color DrawColor)
        {
            try
            {
                if (modelname != null)
                {
                    Texture2D model = Util.Global.AllTexture2D.Where(x => x.Name == modelname.ToLower()).FirstOrDefault();

                    if (AnimSprite != null)
                    {
                        int width = model.Width / AnimSprite.Rows;
                        int height = model.Height / AnimSprite.Columns;
                        int row = (int)((float)AnimSprite.currentFrame / (float)AnimSprite.Rows);
                        int column = AnimSprite.currentFrame % AnimSprite.Rows;

                        Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
                        Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
                        if (Orientation != 0 && Actor == null)
                        {
                            spriteBatch.Draw(model, destinationRectangle, sourceRectangle, DrawColor, Orientation, new Vector2(Size.X / 2, Size.Y / 2), SpriteEffects.None, 0f);
                        }
                        else
                        {
                            spriteBatch.Draw(model, destinationRectangle, sourceRectangle, DrawColor);
                        }
                    }
                    else
                    {
                        if (Orientation != 0 && Actor == null)
                        {
                            spriteBatch.Draw(model, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), null, DrawColor, Orientation, new Vector2(Size.X / 2, Size.Y / 2), SpriteEffects.None, 0f);
                        }
                        else
                        {
                            spriteBatch.Draw(model, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), DrawColor);
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
                        TextSize = Vector2.Add(TextSize, new Vector2(6, 6));
                        if (text == "")
                            TextSize = Size;
                        else
                            Size = TextSize;

                        Box = new Texture2D(graphics, 1, 1);
                        Box.SetData(new[] { Color.White });
                        spriteBatch.Draw(Box, new Rectangle((int)Position.X - 3, (int)Position.Y - 3, (int)TextSize.X, (int)TextSize.Y), boxColor);
                    }
                    spriteBatch.DrawString(Util.Global.font, text, Position, color, 0f, new Vector2(0, 0), textSize, new SpriteEffects(), 1.0f);
                }
            }
            catch (Exception ex)
            {
                Util.Base.Log("BaseDrawFail: " + ex.Message);
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
                    if (name == "Hero")
                    {
                        rect2 = new Rectangle((int)Position.X, (int)Position.Y + (int)(Size.Y / 1.5F), (int)Size.X, (int)Size.Y / 3);
                    }
                    if (rect1.Intersects(rect2))
                    {
                        collideflag = true;
                    }
                }
                

            }
            //return false;
            return collideflag;
        }
    }
}
