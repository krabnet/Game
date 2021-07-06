using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Game.Actions;

namespace Game.Menu
{
    public static class Warp
    {
        private const string BaseName = "HUD_Warp";

        public static void HideWarp()
        {
            Util.Global.Sprites.RemoveAll(x => x.name.Contains(BaseName));
        }

        public static void ShowWarp(Objects.Sprite2d CurrentWarp)
        {
            HideWarp();
            Vector2 MapSize = new Vector2(400, 400);
            Vector2 InitPos = new Vector2(200, 50);

            Objects.Sprite2d S = new Objects.Sprite2d(null, BaseName + "_Base", true, InitPos, MapSize, 0, Objects.Base.ControlType.None);
            S.orderNum = 10000;
            S.Viewtype = Objects.Base.ViewType.HUD;
            S.boxColor = Color.Black;
            S.text = "";
            Util.Global.Sprites.Add(S);

            Util.Global.Sprites.RemoveAll(x => x.name == BaseName + "Close");
            Util.Global.Sprites.Add(new Objects.Sprite2d(null, BaseName + "Close", true, Vector2.Add(InitPos, new Vector2(MapSize.X-15,0)), new Vector2(10, 10), 0, Objects.Base.ControlType.None));
            Util.Global.Sprites.Where(x => x.name == BaseName + "Close").FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
            Util.Global.Sprites.Where(x => x.name == BaseName + "Close").FirstOrDefault().text = "X";
            Util.Global.Sprites.Where(x => x.name == BaseName + "Close").FirstOrDefault().textSize = 1f;
            Util.Global.Sprites.Where(x => x.name == BaseName + "Close").FirstOrDefault().color = Color.Azure;
            Util.Global.Sprites.Where(x => x.name == BaseName + "Close").FirstOrDefault().boxColor = Color.Black;
            Util.Global.Sprites.Where(x => x.name == BaseName + "Close").FirstOrDefault().orderNum = 10980;
            List<Object> obj3 = new List<object>();
            ActionCall call3 = new ActionCall(ActionType.Mouse, typeof(Warp), "HideWarp", obj3);
            Util.Global.Sprites.Where(x => x.name == BaseName + "Close").FirstOrDefault().actionCall.Add(call3);


            InitPos = Vector2.Add(InitPos, new Vector2(25, 25));
            foreach (Items.Warp WW in Util.Global.Warp)
            {
                List<Object> ActionObjects = new List<object>();

                S = new Objects.Sprite2d(null, BaseName + "_Warp", true, InitPos, new Vector2(8, 8), 0, Objects.Base.ControlType.None);
                S.orderNum = 10001;
                S.Viewtype = Objects.Base.ViewType.HUD;
                S.boxColor = Color.Blue;
                S.text = WW.WarpName;
                S.textSize = 1f;

                if (CurrentWarp.ID == WW.WarpID)
                {
                    S.boxColor = Color.Red;

                    ActionObjects = new List<object>();
                    ActionObjects.Add(WW);
                    S.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseCollision, typeof(Menu.Warp), "UpdateWarpImage", ActionObjects));
                    S.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Menu.Warp), "UpdateWarpImage", ActionObjects));
                }
                else
                {
                    S.boxColor = Color.Blue;

                    ActionObjects = new List<object>();
                    ActionObjects.Add(WW.MapLocation);
                    ActionObjects.Add(WW.WarpLocation);
                    S.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Maps.Map), "WarpMapPosition", ActionObjects));

                    ActionObjects = new List<object>();
                    ActionObjects.Add(WW);
                    S.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseCollision, typeof(Menu.Warp), "UpdateWarpImage", ActionObjects));
                    S.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Menu.Warp), "UpdateWarpImage", ActionObjects));
                    //S.actionCall.Add
                }
                Util.Global.Sprites.Add(S);

                //InitPos = Vector2.Add(InitPos, new Vector2(0, (Util.Global.screenSizeHeight / 6)+5));
                InitPos = Vector2.Add(InitPos, new Vector2(0, 25));

            }

        }

        public static void UpdateWarpImage(Items.Warp WW)
        {
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("WarpShot:"));
            Vector2 InitPos = new Vector2(200, 50);
            string modelname = "WarpShot:" + WW.WarpName;
            Objects.Sprite2d S = new Objects.Sprite2d(modelname.ToLower(), BaseName + "_WarpShot", true, Vector2.Add(InitPos, new Vector2(250, 10)), new Vector2(Util.Global.screenSizeWidth / 6, Util.Global.screenSizeHeight / 6), 0, Objects.Base.ControlType.None);
            S.orderNum = 10001;
            S.Viewtype = Objects.Base.ViewType.HUD;
            Util.Global.Sprites.Add(S);
        }
        
    }
}
