using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Game.Actions;

namespace Game.Menu
{
    public static class MiniMap
    {
        public static void ShowMiniMap()
        {
            HideMiniMap();
            string BaseName = "HUD_MiniMap";
            Vector2 MapSize = new Vector2(100, 100);
            //Vector2 InitPos = new Vector2(655, 405);
            Vector2 InitPos = new Vector2(0, 375);

            Objects.Sprite2d S = new Objects.Sprite2d(null, BaseName + "_Base", true, InitPos, MapSize, 0, Objects.Base.ControlType.None);
            S.orderNum = 10000;
            S.Viewtype = Objects.Base.ViewType.HUD;
            S.boxColor = Color.Black;
            S.text = "";
            Util.Global.Sprites.Add(S);


            Util.Global.Sprites.RemoveAll(x => x.name == BaseName + "Close");
            Util.Global.Sprites.Add(new Objects.Sprite2d(null, BaseName + "Close", true, Vector2.Add(InitPos, new Vector2(MapSize.X - 15, 0)), new Vector2(10, 10), 0, Objects.Base.ControlType.None));
            Util.Global.Sprites.Where(x => x.name == BaseName + "Close").FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
            Util.Global.Sprites.Where(x => x.name == BaseName + "Close").FirstOrDefault().text = "X";
            Util.Global.Sprites.Where(x => x.name == BaseName + "Close").FirstOrDefault().textSize = 1f;
            Util.Global.Sprites.Where(x => x.name == BaseName + "Close").FirstOrDefault().color = Color.Azure;
            Util.Global.Sprites.Where(x => x.name == BaseName + "Close").FirstOrDefault().boxColor = Color.Black;
            Util.Global.Sprites.Where(x => x.name == BaseName + "Close").FirstOrDefault().orderNum = 10980;
            List<Object> obj3 = new List<object>();
            ActionCall call3 = new ActionCall(ActionType.Mouse, typeof(MiniMap), "HideMiniMap", obj3);
            Util.Global.Sprites.Where(x => x.name == BaseName + "Close").FirstOrDefault().actionCall.Add(call3);

            InitPos = Vector2.Add(InitPos, Vector2.Divide(MapSize, 2));
            foreach (Maps.Map MM in Util.Global.MainMap)
            {
                if (MM != null && MM.MapVector != null && MM.MapVector.Z == 0)
                {
                    Vector3 Distance = Vector3.Subtract(new Vector3(50, 50, 0), MM.MapVector);
                    int DistanceLevel = (int)Math.Abs(Distance.X) + (int)Math.Abs(Distance.Y);
                    Vector3 DistanceMulti = Vector3.Multiply(Distance, 10F);
                    Vector2 Adder = Vector2.Add(InitPos, new Vector2(DistanceMulti.Y*-1, DistanceMulti.X));
                    S = new Objects.Sprite2d(null, BaseName + "_MapPart", true, Adder, new Vector2(8, 8), 0, Objects.Base.ControlType.None);
                    S.orderNum = 10000;
                    S.Viewtype = Objects.Base.ViewType.HUD;
                    if (MM.MapVector == new Vector3(50, 50, 0))
                        S.boxColor = Color.Yellow;
                    else if (MM.MapVector == Util.Global.CurrentMap)
                        S.boxColor = Color.Purple;
                    else
                        S.boxColor = Color.Blue * DistanceLevel * 10;
                    S.text = "";
                    Util.Global.Sprites.Add(S);
                }
            }


        }

        public static void HideMiniMap()
        {
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("HUD_MiniMap"));
        }

    }
}
