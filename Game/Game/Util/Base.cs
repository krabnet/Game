using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Util
{
    public static class Base
    {

        public static bool collision(Objects.Sprite2d sprite1, Objects.Sprite2d sprite2)
        {
            Rectangle rect1 = Util.Base.GetRectangleFromVectors(sprite1.Position, sprite1.Size);
            Rectangle rect2 = Util.Base.GetRectangleFromVectors(sprite2.Position, sprite2.Size);
            return Detect(rect1, rect2);
        }

        public static bool collision(Objects.Sprite2d sprite1, Rectangle rect2)
        {
            Rectangle rect1 = Util.Base.GetRectangleFromVectors(sprite1.Position, sprite1.Size);
            return Detect(rect1, rect2);
        }

        public static bool collision(Rectangle rect1, Rectangle rect2)
        {
          return Detect(rect1,rect2);
        }

        public static bool collision(Vector2 circle, float radius, Rectangle rectangle)
        {
            var rectangleCenter = new Vector2((rectangle.X + rectangle.Width / 2),
                                             (rectangle.Y + rectangle.Height / 2));

            var w = rectangle.Width / 2;
            var h = rectangle.Height / 2;

            var dx = Math.Abs(circle.X - rectangleCenter.X);
            var dy = Math.Abs(circle.Y - rectangleCenter.Y);

            if (dx > (radius + w) || dy > (radius + h)) return false;

            var circleDistance = new Vector2
            {
                X = Math.Abs(circle.X - rectangle.X - w),
                Y = Math.Abs(circle.Y - rectangle.Y - h)
            };

            if (circleDistance.X <= (w))
            {
                return true;
            }

            if (circleDistance.Y <= (h))
            {
                return true;
            }

            var cornerDistanceSq = Math.Pow(circleDistance.X - w, 2) +
                                            Math.Pow(circleDistance.Y - h, 2);

            return (cornerDistanceSq <= (Math.Pow(radius, 2)));
        }

        public static bool collision(Rectangle rect1, int radius, Rectangle rect2)
        {
            Point circleDistance = new Point(Math.Abs(rect1.X - rect2.X), Math.Abs(rect1.Y - rect2.Y));

            if (circleDistance.X > (rect2.Width / 2 + radius)) { return false; }
            if (circleDistance.Y > (rect2.Height / 2 + radius)) { return false; }

            if (circleDistance.X <= (rect2.Width / 2)) { return true; }
            if (circleDistance.Y <= (rect2.Height / 2)) { return true; }

            float cornerDistance_sq = (circleDistance.X - rect2.Width / 2) ^ 2 + (circleDistance.Y - rect2.Height / 2) ^ 2;
            return (cornerDistance_sq <= (radius ^ 2));
        }

        public static bool collision(Vector2 Point1, Vector2 Point2, int Range)
        {
            float distance = Vector2.Distance(Point1, Point2);
            if (Math.Floor(distance) <= Range)
                return true;
            else
                return false;
        }

        private static bool Detect(Rectangle rect1, Rectangle rect2)
        {
            if (rect1.Intersects(rect2))
            {
                return true;
            }
            return false;
        }

        public static void SetColorInRange(Vector2 V, int Distance, Color C)
        {
            List<Objects.Sprite2d> AllObjects = new List<Objects.Sprite2d>();
            AllObjects.AddRange(Util.Global.Sprites);
            AllObjects = AllObjects.Where(x => x.Position.X < V.X + Distance && x.Position.Y < V.Y + Distance).ToList();
            AllObjects = AllObjects.Where(x => x.Position.X < V.X - Distance && x.Position.Y < V.Y - Distance).ToList();
            foreach (Objects.Sprite2d O in AllObjects)
            {
                O.color = C;
            }
        }

        public static void GetLightSources()
        {
            List<Tuple<int, int, float>> Lights = new List<Tuple<int, int, float>>();
            foreach (Objects.Sprite2d O in Util.Global.Sprites.Where(x => x.LightSourceDistance > 0 && x.Item.State != Items.Item.ItemState.Hotbar))
            {
                float X = O.Position.X-20;
                float Y = O.Position.Y-20;
                float Distance = O.LightSourceDistance;
                Lights.Add(new Tuple<int, int, float>((int)X, (int)Y, Distance));
            }

            Util.Global.Lights = Lights;
        }

        //public static Objects.Menu GetMenuClick()
        //{
        //    Vector2 MP = Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.Default);

        //    Objects.Menu returnMenu = new Objects.Menu();
        //    Rectangle rect1 = new Rectangle((int)MP.X, (int)MP.Y, 15, 15);
        //    Rectangle rect2 = new Rectangle();

        //    foreach (Objects.Menu M in Util.Global.Menus.Where(x => x.active==true).ToList())
        //    {
        //        rect2 = new Rectangle((int)M.Position.X,(int)M.Position.Y,M.boxWidth,M.boxHeight);
        //        if (rect1.Intersects(rect2))
        //        {
        //            returnMenu = M;
        //            break;
        //        }
        //    }
        //    return returnMenu;
        //}

        public static Objects.Sprite2d GetSpriteClick()
        {
            Vector2 MP = Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.Default);
            
            Objects.Sprite2d returnObject = new Objects.Sprite2d();
            Rectangle rect1 = new Rectangle((int)MP.X, (int)MP.Y, 15, 15);
            Rectangle rect2 = new Rectangle();

            foreach (Objects.Sprite2d M in Util.Global.Sprites.Where(x => x.active == true).ToList())
            {
                rect2 = Util.Base.GetRectangleFromVectors(M.Position, M.Size);
                if (rect1.Intersects(rect2))
                {
                    returnObject = M;
                }
            }
            return returnObject;
        }

        public static void CallMethod(Actions.ActionCall AC)
        {
            CallMethodByString(AC.Type, AC.actionMethodName, AC.parameters);
        }

        public static void CallMethodByString(Type className, string methodName, List<Object> parameters)
        {
            MethodInfo mi = className.GetMethod(methodName);
            if (className.IsAbstract && className.IsSealed)
            {
                if (parameters == null)
                {
                    mi.Invoke(null, null);
                }
                else
                {
                    mi.Invoke(null, parameters.ToArray());
                }
            }
            else
            {
                object classInstance = Activator.CreateInstance(className);
                if (parameters == null)
                {
                    List<Object> p = new List<object>();
                    mi.Invoke(classInstance, p.ToArray());
                }
                else
                {
                    mi.Invoke(classInstance, parameters.ToArray());
                }
            }
        }

        public static void SizeTiles(int SizeChange)
        {
            foreach (Objects.Sprite2d S in Util.Global.Sprites.Where(x => x.controlType == Objects.Base.ControlType.None))
            {
                S.Size = new Vector2(S.Size.X + SizeChange, S.Size.Y + SizeChange);
            }

            //foreach (Objects.Text S in Util.Global.Texts.Where(x => x.controlType == Objects.Base.ControlType.None))
            //{
            //    S.Size = new Vector2(S.Size.X + SizeChange, S.Size.Y + SizeChange);
            //}
        }

        public static double GetBellCurvePoint(float Percent, float Max)
        {
            double ret = 0;
            if (Percent > .5)
            {
                ret = ((1 - Percent) * Max * 2);
            }
            else
            {
                ret = (Percent * Max * 2);
            }
            return ret;
        }

        public static Rectangle GetRectangleFromVectors(Vector2 Position, Vector2 Size)
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
        }

        public static void Log(string Message)
        {
            Message = string.Format("{0} | {1} | {2}", DateTime.Now.ToString(), Util.Global.GameClock.ToString(), Message);

            System.Diagnostics.Debug.WriteLine(Message);
            Sys.WritetoLog(Message);
        }

        public static string GetRandomName()
        {
            string Retstr = "";
            List<string> Syl = new List<string>();
            Syl.Add("mon");
            Syl.Add("shi");
            Syl.Add("izl");
            Syl.Add("ag");
            Syl.Add("rol");
            Syl.Add("wren");
            Syl.Add("pla");
            Syl.Add("gron");
            Syl.Add("flo");
            Syl.Add("pod");
            Syl.Add("gen");
            Syl.Add("gler");
            Syl.Add("que");
            Syl.Add("yal");
            Syl.Add("dof");
            Syl.Add("kel");
            Syl.Add("une");
            Syl.Add("alp");
            Syl.Add("bree");
            Syl.Add("xly");
            Syl.Add("yuv");
            Syl.Add("nu");
            Syl.Add("ina");
            Syl.Add("koy");
            Syl.Add("mig");
            Syl.Add("xly");
            Syl.Add("lofo");
            Syl.Add("ohay");
            Syl.Add("itba");
            Syl.Add("bari");
            Syl.Add("kapi");
            Syl.Add("rire");
            Syl.Add("ega");
            Syl.Add("lok");
            Syl.Add("umit");
            Syl.Add("boga");
            Syl.Add("enga");
            Syl.Add("tho");
            Syl.Add("xly");
            Syl.Add("tasu");
            Syl.Add("kude");
            Syl.Add("nuna");
            Syl.Add("doyo");
            Syl.Add("nomu");
            Syl.Add("gi");
            Syl.Add("cial");
            Syl.Add("tien");
            Syl.Add("ion");
            Syl.Add("ige");
            Syl.Add("anz");

            for(int i=1; i<=Util.Global.GetRandomInt(2,4); i++)
            {
                Retstr = Retstr + Syl.OrderBy(s => Guid.NewGuid()).First();
            }

            string letter = Retstr.Substring(0, 1);
            Retstr = Retstr.Remove(0, 1);
            Retstr = letter.ToUpper() + Retstr;
            return Retstr;
        }
    }
}
