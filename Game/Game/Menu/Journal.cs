using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Game.Actions;

namespace Game.Menu
{
    public static class Journal
    {
        public static void Show()
        {
            Hide();
            Util.Global.PauseGameToggle = true;
            string BaseName = "HUD_Journal";
            Vector2 MapSize = new Vector2(600, 400);
            Vector2 InitPos = new Vector2(100, 50);

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
            ActionCall call3 = new ActionCall(ActionType.Mouse, typeof(Journal), "Hide", obj3);
            Util.Global.Sprites.Where(x => x.name == BaseName + "Close").FirstOrDefault().actionCall.Add(call3);


            InitPos = Vector2.Add(InitPos, new Vector2(5, 25));
            List<Object> ActionObjects = new List<object>();
            foreach (string message in Util.Global.Journal)
            {
                string txt = message.Replace("\n", "");
                S = new Objects.Sprite2d(null, BaseName + "_Warp", true, InitPos, new Vector2(8, 8), 0, Objects.Base.ControlType.None);
                S.orderNum = 10001;
                S.Viewtype = Objects.Base.ViewType.HUD;
                S.boxColor = Color.Blue;
                S.text = txt;
                S.textSize = 1f;
                S.boxColor = Color.Blue;
                Util.Global.Sprites.Add(S);

                InitPos = Vector2.Add(InitPos, new Vector2(0, 30));
            }
        }

        public static void Hide()
        {
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("HUD_Journal"));
            Util.Global.PauseGameToggle = false;
        }

    }
}
