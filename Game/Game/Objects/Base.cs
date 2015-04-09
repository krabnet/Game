using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game.Objects
{
    public class Base
    {
        public enum ControlType {None,Mouse,Keyboard};
        public enum ActionType { Collision, Mouse, Update };

        public Guid ID { get; set; }
        public string name { get; set; }
        public int orderNum { get; set; }
        public bool active { get; set; }
        public ControlType? controlType { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int sizex { get; set; }
        public int sizey { get; set; }
        public int speed { get; set; }
        public int WindowHeight { get { return Util.Global.screenSizeHeight; } }
        public int WindowWidth { get { return Util.Global.screenSizeWidth; } }
        public bool allowScroll { get; set; }
        public bool clipping { get; set; }
        private Vector2 previousPosition { get; set; }
        public ActionType actionType { get; set; }
        public Actions.ActionCall actionCall { get; set; }
        public Actions.ActionCall actionCallB { get; set; }

        public const int tilespeed = 5;

        public void left()
        {
            if (x > 0 + Util.Global.WindowBoarder || allowScroll)
            { x = x - speed; }
            else if (!ClipCheck())
            { Maps.Asset.MoveTiles(tilespeed, 0); }
        }
        public void right()
        {
            if (x < WindowWidth - Util.Global.WindowBoarder || allowScroll)
            { x = x + speed; }
            else if (!ClipCheck())
            { Maps.Asset.MoveTiles(-tilespeed, 0); }
        }
        public void up()
        {
            if (y > 0 + Util.Global.WindowBoarder || allowScroll)
            { y = y - speed; }
            else if (!ClipCheck())
            { Maps.Asset.MoveTiles(0, tilespeed); }
        }
        public void down()
        {
            if (y < WindowHeight - Util.Global.WindowBoarder || allowScroll)
            { y = y + speed; }
            else if (!ClipCheck())
            { Maps.Asset.MoveTiles(0, -tilespeed); }
        }

        public void moveRandom()
        {
            Random rnd = new Random();
            int dir = rnd.Next(1, 13);
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

        private bool ClipCheck()
        {
            bool collideflag = false;
            if (this.clipping == true)
            {

                foreach (Objects.Spite2d S in Util.Global.Sprites.Where(x => x.clipping == true).ToList())
                {
                    Rectangle rect1 = new Rectangle(S.x, S.y, S.sizex, S.sizey);
                    Rectangle rect2 = new Rectangle(this.x, this.y, this.sizex, this.sizey);
                    if (rect1.Intersects(rect2))
                    {
                        collideflag = true;
                    }
                }
                foreach (Objects.AnimSprite S in Util.Global.SpritesAnim.Where(x => x.clipping == true && x.name!="Hero").ToList())
                {
                    Rectangle rect1 = new Rectangle(S.x, S.y, S.sizex, S.sizey);
                    Rectangle rect2 = new Rectangle(this.x, this.y, this.sizex, this.sizey);
                    if (rect1.Intersects(rect2))
                    {
                        collideflag = true;
                    }
                }
            }
            return collideflag;
        }

        public void KeyInput(KeyboardState keystate)
        {
            this.previousPosition = new Vector2((float)this.x, (float)this.y);
            int changex = 0;
            int changey = 0;
            if (keystate.IsKeyDown(Keys.A))
            {
                left();
                Util.Global.SpritesAnim.Where(x => x.name == "Hero").FirstOrDefault().Texture = Util.Global.Hero.HeroLeft;
                changex = 1;
            }
            if (keystate.IsKeyDown(Keys.D))
            {
                right();
                Util.Global.SpritesAnim.Where(x => x.name == "Hero").FirstOrDefault().Texture = Util.Global.Hero.HeroRight;
                changex = -1;
            }
            if (keystate.IsKeyDown(Keys.W))
            {
                up();
                Util.Global.SpritesAnim.Where(x => x.name == "Hero").FirstOrDefault().Texture = Util.Global.Hero.HeroUp;
                changey = 1;
            }
            if (keystate.IsKeyDown(Keys.S))
            {
                down();
                Util.Global.SpritesAnim.Where(x => x.name == "Hero").FirstOrDefault().Texture = Util.Global.Hero.HeroDown;
                changey = -1;
            }
            if (ClipCheck())
            {
                this.x = Convert.ToInt32(this.previousPosition.X);
                this.y = Convert.ToInt32(this.previousPosition.Y);

                while (ClipCheck())
                {
                    this.x = this.x + changex;
                    this.y = this.y + changey;
                }

            }
        }

        public void MouseInput(MouseState mousestate)
        {
            x=mousestate.X;
            y=mousestate.Y;
        }

       
    }
}
