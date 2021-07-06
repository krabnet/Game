using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Game.Actions;

namespace Game.Menu
{
    public static class Hotbar
    {
        public const string Name = "Hotbar";
        //public static readonly Vector2 BasePos = new Vector2(150, 0);
        public static readonly Vector2 BasePos = new Vector2(150, Util.Global.screenSizeHeight - 50);

        public static void Display()
        {
                Hide();
                Util.Global.Sprites.Add(new Objects.Sprite2d("Hotbar", Name, true, BasePos, new Vector2(420, 50), 0, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
                Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().orderNum = 10500;
                Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().LightIgnor = true;

                Util.Global.Sprites.RemoveAll(x => x.name == Name + "Close");
                Util.Global.Sprites.Add(new Objects.Sprite2d(null, Name + "Close", true, Vector2.Add(BasePos, new Vector2(405, 5)), new Vector2(10, 10), 0, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == Name + "Close").FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
                Util.Global.Sprites.Where(x => x.name == Name + "Close").FirstOrDefault().text = "X";
                Util.Global.Sprites.Where(x => x.name == Name + "Close").FirstOrDefault().textSize = 1f;
                Util.Global.Sprites.Where(x => x.name == Name + "Close").FirstOrDefault().color = Color.Azure;
                Util.Global.Sprites.Where(x => x.name == Name + "Close").FirstOrDefault().boxColor = Color.Black;
                Util.Global.Sprites.Where(x => x.name == Name + "Close").FirstOrDefault().orderNum = 10980;
                List<Object> obj3 = new List<object>();
                ActionCall call3 = new ActionCall(ActionType.Mouse, typeof(Hotbar), "Hide", obj3);
                Util.Global.Sprites.Where(x => x.name == Name + "Close").FirstOrDefault().actionCall.Add(call3);

                foreach (Objects.Sprite2d S in Util.Global.Sprites.Where(x => x.Item != null && x.Item.State == Items.Item.ItemState.Hotbar).OrderBy(y => y.Item.ItemSlot).ToList())
                {
                        Vector2 NewPos = Vector2.Add(Vector2.Add(BasePos, new Vector2(9, 9)), new Vector2(35 * (S.Item.ItemSlot - 1), 0));
                        Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
                        Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().active = true;
                        Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Size = new Vector2(30, 30);
                        Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().orderNum = 10980;
                        Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Position = NewPos;
                        Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().LightIgnor = true;
                        Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().boxColor = Color.White;
                        Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().LightSourceDistance = 0f;

                        Util.Global.Sprites.Add(new Objects.Sprite2d(null, Name + "Slot" + S.ID, true, Vector2.Add(NewPos, new Vector2(2, 17)), Util.Global.DefaultPosition, 0, Objects.Base.ControlType.None));
                        Util.Global.Sprites.Where(x => x.name == Name + "Slot" + S.ID).FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
                        Util.Global.Sprites.Where(x => x.name == Name + "Slot" + S.ID).FirstOrDefault().text = S.Item.ItemSlot.ToString();
                        Util.Global.Sprites.Where(x => x.name == Name + "Slot" + S.ID).FirstOrDefault().textSize = .7f;
                        Util.Global.Sprites.Where(x => x.name == Name + "Slot" + S.ID).FirstOrDefault().boxColor = Color.Black;
                        Util.Global.Sprites.Where(x => x.name == Name + "Slot" + S.ID).FirstOrDefault().color = Color.White;
                        Util.Global.Sprites.Where(x => x.name == Name + "Slot" + S.ID).FirstOrDefault().orderNum = 10980;
                        Util.Global.Sprites.Where(x => x.name == Name + "Slot" + S.ID).FirstOrDefault().LightIgnor = true;
                }
        }
        public static void Hide()
        {
            Util.Global.Sprites.RemoveAll(x => x.name.Contains(Name));
            foreach (Objects.Sprite2d S in Util.Global.Sprites.Where(x => x.Item != null && x.Item.State == Items.Item.ItemState.Hotbar))
            {
                Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().active = false;
            }
        }
        public static bool IsOpen()
        {
            if (Util.Global.Sprites.Where(x => x.name == Name).Count() > 0)
            {
                return true;
            }
            return false;
        }
        public static bool IsMouseCollision()
        {
            if (Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.HUD).X > BasePos.X &&
                Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.HUD).X < BasePos.X + 420 &&
                Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.HUD).Y > BasePos.Y &&
                Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.HUD).Y < BasePos.Y + 50)
            {
                return true;
            }
            return false;
        }

        public static int GetMouseSlot()
        {
                Vector2 MosPos = Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.HUD);
                Vector2 Pos = Vector2.Add(Menu.Hotbar.BasePos, new Vector2(9, 9));
                for (int i = 1; i <= 8; i++)
                {
                    Rectangle rec1 = new Rectangle((int)MosPos.X+5,(int)MosPos.Y+5,1,1);
                    Rectangle rec2 = new Rectangle((int)Pos.X, (int)Pos.Y, 32, 32);
                    if (Util.Base.collision(rec1, rec2))
                    {
                        if (Util.Global.Sprites.Where(x => x.Item != null && x.Item.State == Items.Item.ItemState.Hotbar && x.Item.ItemSlot == i).Count() > 0)
                        {
                            return 0;
                        }
                        else
                        {
                            return i;
                        }
                    }
                    Pos = Vector2.Add(Pos, new Vector2(35, 0));
                }
                 return 0;
        }
    }
}
