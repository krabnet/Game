using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Reflection;

namespace Game.Util
{
    public class Base
    {
        public Base()
        {
        }

        public bool collision(Objects.Spite2d sprite1, Objects.Spite2d sprite2)
        {
            Rectangle rect1 = new Rectangle(sprite1.x, sprite1.y, sprite1.sizex, sprite1.sizey);
            Rectangle rect2 = new Rectangle(sprite2.x, sprite2.y, sprite2.sizex, sprite2.sizey);
            return Detect(rect1, rect2);
        }

        public bool collision(Objects.Spite2d sprite1, Objects.AnimSprite sprite2)
        {
            Rectangle rect1 = new Rectangle(sprite1.x, sprite1.y, sprite1.sizex, sprite1.sizey);
            Rectangle rect2 = new Rectangle(sprite2.x, sprite2.y, sprite2.sizex, sprite2.sizey);
            return Detect(rect1, rect2);
        }

        public bool collision(Objects.AnimSprite sprite2, Rectangle rect1)
        {
            Rectangle rect2 = new Rectangle(sprite2.x, sprite2.y, sprite2.sizex, sprite2.sizey);
            return Detect(rect1, rect2);
        }

        public bool collision(Objects.Spite2d sprite1, Rectangle rect2)
        {
            Rectangle rect1 = new Rectangle(sprite1.x, sprite1.y, sprite1.sizex, sprite1.sizey);
            return Detect(rect1, rect2);
        }
        public bool collision(Rectangle rect1, Rectangle rect2)
        {
          return Detect(rect1,rect2);
        }

        private bool Detect(Rectangle rect1, Rectangle rect2)
        {
            if (rect1.Intersects(rect2))
            {
                return true;
            }
            return false;
        }

        public Objects.Menu GetMenuClick(Objects.Spite2d mouseSprite)
        {
            Objects.Menu returnMenu = new Objects.Menu();
            Rectangle rect1 = new Rectangle(mouseSprite.x, mouseSprite.y, mouseSprite.x + 1, mouseSprite.y+1);
            Rectangle rect2 = new Rectangle();

            foreach (Objects.Menu M in Util.Global.Menus.Where(x => x.active==true).ToList())
            {
                rect2 = new Rectangle(M.x,M.y,M.boxWidth,M.boxHeight);
                if (rect1.Intersects(rect2))
                {
                    returnMenu = M;
                    break;
                }
            }
            return returnMenu;
        }

        public Objects.AnimSprite GetAnimatedSpriteClick(Objects.Spite2d mouseSprite)
        {
            Objects.AnimSprite returnObject = new Objects.AnimSprite();
            Rectangle rect1 = new Rectangle(mouseSprite.x, mouseSprite.y, mouseSprite.sizex, mouseSprite.sizey);
            Rectangle rect2 = new Rectangle();

            foreach (Objects.AnimSprite M in Util.Global.SpritesAnim.Where(x => x.active == true).ToList())
            {
                rect2 = new Rectangle(M.x, M.y,M.sizex, M.sizey);
                if (rect1.Intersects(rect2))
                {
                    returnObject = M;
                }
            }
            return returnObject;
        }

        public Objects.Spite2d GetSpriteClick(Objects.Spite2d mouseSprite)
        {
            Objects.Spite2d returnObject = new Objects.Spite2d();
            Rectangle rect1 = new Rectangle(mouseSprite.x, mouseSprite.y, mouseSprite.sizex, mouseSprite.sizey);
            Rectangle rect2 = new Rectangle();

            foreach (Objects.Spite2d M in Util.Global.Sprites.Where(x => x.active == true).ToList())
            {
                rect2 = new Rectangle(M.x, M.y, M.sizex, M.sizey);
                if (rect1.Intersects(rect2))
                {
                    returnObject = M;
                }
            }
            return returnObject;
        }

        public void CallMethodByString(Type className, string methodName, List<Object> parameters)
        {
            MethodInfo mi = className.GetMethod(methodName);
            object classInstance = Activator.CreateInstance(className);
            mi.Invoke(classInstance, parameters.ToArray());
        }

        public void SizeTiles(int SizeChange)
        {
            foreach (Objects.Spite2d S in Util.Global.Sprites.Where(x => x.controlType == Objects.Base.ControlType.None))
            {
                S.sizex = S.sizex + SizeChange;
                S.sizey = S.sizey + SizeChange;
            }

            foreach (Objects.Text S in Util.Global.Texts.Where(x => x.controlType == Objects.Base.ControlType.None))
            {
                S.sizex = S.sizex + SizeChange;
                S.sizey = S.sizey + SizeChange;
            }

            foreach (Objects.AnimSprite S in Util.Global.SpritesAnim.Where(x => x.controlType == Objects.Base.ControlType.None))
            {
                S.sizex = S.sizex + SizeChange;
                S.sizey = S.sizey + SizeChange;
            }
        }
    }
}
