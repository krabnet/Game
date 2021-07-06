using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Game.Actions;
using Microsoft.Xna.Framework.Media;

namespace Game.Menu
{
    public static class Escape
    {
        public enum EscapeAction { Character, Inventory, Crafting, Hotbar, MiniMap, Journal, FullScreen, Save, Load, Quit, Restart, Music, Mute };

        public static void Show()
        {
            Hide();
            Util.Global.PauseGameToggle = true;
            string BaseName = "HUD_Escape";
            Vector2 MapSize = new Vector2(400, 400);
            Vector2 InitPos = new Vector2(200, 50);

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
            ActionCall call3 = new ActionCall(ActionType.Mouse, typeof(Escape), "Hide", obj3);
            Util.Global.Sprites.Where(x => x.name == BaseName + "Close").FirstOrDefault().actionCall.Add(call3);


            InitPos = Vector2.Add(InitPos, new Vector2(25, 25));
            List<Object> ActionObjects = new List<object>();

            foreach (EscapeAction EA in Enum.GetValues(typeof(EscapeAction)))
            {
                S = new Objects.Sprite2d(null, BaseName + "_Warp", true, InitPos, new Vector2(8, 8), 0, Objects.Base.ControlType.None);
                S.orderNum = 10001;
                S.Viewtype = Objects.Base.ViewType.HUD;
                S.boxColor = Color.Blue;
                S.text = EA.ToString();
                S.textSize = 1f;
                S.boxColor = Color.Blue;
                ActionObjects = new List<object>();
                ActionObjects.Add(EA);
                S.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Menu.Escape), "Action", ActionObjects));
                Util.Global.Sprites.Add(S);
                InitPos = Vector2.Add(InitPos, new Vector2(0, 25));
            }
        }

        public static void Hide()
        {
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("HUD_Escape"));
            Util.Global.PauseGameToggle = false;
        }

        public static void Action(EscapeAction EA)
        {
            switch (EA)
            {
                case EscapeAction.Character:
                    Hide();
                    Menu.ActorStats.DisplayStats(Util.Global.Hero);
                    break;
                case EscapeAction.Inventory:
                    Hide();
                    Menu.Inventory.DisplayInventory();
                    break;
                case EscapeAction.Hotbar:
                    Hide();
                    Menu.Hotbar.Display();
                    break;
                case EscapeAction.Crafting:
                    Hide();
                    Menu.Craft.DisplayCraft(Crafting.Type.Hand);
                    break;
                case EscapeAction.MiniMap:
                    Hide();
                    Menu.MiniMap.ShowMiniMap();
                    break;
                case EscapeAction.Journal:
                    Hide();
                    Menu.Journal.Show();
                    break;
                case EscapeAction.FullScreen:
                    Hide();
                    Util.Global.RunFullScreenToggle = true;
                    break;
                case EscapeAction.Save:
                    Hide();
                    Util.Sys.Save();
                    break;
                case EscapeAction.Load:
                    Hide();
                    Util.Sys.Load();
                    break;
                case EscapeAction.Quit:
                    Hide();
                    Util.Global.QuitGameToggle = true;
                    break;
                case EscapeAction.Mute:
                    Util.Global.SoundMute = true;
                    Season.CheckMusic();
                    break;
                case EscapeAction.Restart:
                    Hide();
                    Util.Global.Fighting = false;
                    new Util.Main().Init();
                    break;
                case EscapeAction.Music:
                    Season.GetMusic();
                    MediaPlayer.Play(Util.Global.Music);
                    Util.Global.MusicChange = false;
                    break;

            }
        }
    }
}
