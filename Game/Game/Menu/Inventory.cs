using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Game.Actions;

namespace Game.Menu
{
    public class Inventory
    {
        public static void DisplayInventory()
        {
            HideInventory();
            Vector2 BasePos = new Vector2(Util.Global.screenSizeWidth-150, 0);
            string Name = "Inventory";
            Util.Global.Sprites.Add(new Objects.Sprite2d("Inventory", Name, true, BasePos, new Vector2(150, 400), 0, Objects.Base.ControlType.None));
            Util.Global.Sprites.Where(x => x.name == "Inventory").FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
            Util.Global.Sprites.Where(x => x.name == "Inventory").FirstOrDefault().boxColor = Color.White;
            Util.Global.Sprites.Where(x => x.name == "Inventory").FirstOrDefault().orderNum = 10500;
            Util.Global.Sprites.Where(x => x.name == "Inventory").FirstOrDefault().LightIgnor = true;

            Util.Global.Sprites.RemoveAll(x => x.name == "CloseInventory");
            Util.Global.Sprites.Add(new Objects.Sprite2d(null, "CloseInventory", true, Vector2.Add(BasePos, new Vector2(10, 375)), new Vector2(10, 10), 0, Objects.Base.ControlType.None));
            Util.Global.Sprites.Where(x => x.name == "CloseInventory").FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
            Util.Global.Sprites.Where(x => x.name == "CloseInventory").FirstOrDefault().text = "X";
            Util.Global.Sprites.Where(x => x.name == "CloseInventory").FirstOrDefault().textSize = 1f;
            Util.Global.Sprites.Where(x => x.name == "CloseInventory").FirstOrDefault().color = Color.Azure;
            Util.Global.Sprites.Where(x => x.name == "CloseInventory").FirstOrDefault().boxColor = Color.Black;
            Util.Global.Sprites.Where(x => x.name == "CloseInventory").FirstOrDefault().orderNum = 10980;
            List<Object> obj3 = new List<object>();
            ActionCall call3 = new ActionCall(ActionType.Mouse, typeof(Inventory), "HideInventory", obj3);
            Util.Global.Sprites.Where(x => x.name == "CloseInventory").FirstOrDefault().actionCall.Add(call3);

            BasePos = Vector2.Add(BasePos, new Vector2(12, 12));
            Vector2 InitBasePos = BasePos;
            int i = 1;
            int q = 1;

            Util.Global.Sprites.RemoveAll(x => x.Item != null && x.Item.State == Items.Item.ItemState.Inventory && x.Item.Number == 0);
            foreach (Objects.Sprite2d S in Util.Global.Sprites.Where(x => x.Item!=null && x.Item.State == Items.Item.ItemState.Inventory).OrderBy(y => y.Item.Type).ToList())
            {
                Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
                Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().active = true;
                Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Size = new Vector2(30, 30);
                Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().orderNum = 10980;
                Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Position = BasePos;
                Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().LightIgnor = true;
                Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().boxColor = Color.White;
                Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().LightSourceDistance = 0f;

                Util.Global.Sprites.Add(new Objects.Sprite2d(null, "InventoryText" + S.ID, true, Vector2.Add(BasePos, new Vector2(2, 17)), Util.Global.DefaultPosition, 0, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == "InventoryText" + S.ID).FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
                Util.Global.Sprites.Where(x => x.name == "InventoryText" + S.ID).FirstOrDefault().text = S.Item.Number.ToString();
                Util.Global.Sprites.Where(x => x.name == "InventoryText" + S.ID).FirstOrDefault().textSize = .7f;
                Util.Global.Sprites.Where(x => x.name == "InventoryText" + S.ID).FirstOrDefault().boxColor = Color.Black;
                Util.Global.Sprites.Where(x => x.name == "InventoryText" + S.ID).FirstOrDefault().color = Color.White;
                Util.Global.Sprites.Where(x => x.name == "InventoryText" + S.ID).FirstOrDefault().orderNum = 10980;
                Util.Global.Sprites.Where(x => x.name == "InventoryText" + S.ID).FirstOrDefault().LightIgnor = true;
                    
                BasePos = Vector2.Add(BasePos, new Vector2(34, 0));
                i++;
                if (i == 5)
                {
                    i = 1;
                    BasePos = InitBasePos;
                    BasePos = Vector2.Add(BasePos, new Vector2(0, 34*q));
                    q++;
                }
            }

        }

        public static void HideInventory()
        {
            Util.Global.Sprites.RemoveAll(x => x.name == "Inventory");
            foreach (Objects.Sprite2d S in Util.Global.Sprites.Where(x => x.Item!=null && x.Item.State == Items.Item.ItemState.Inventory))
            {
                Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().active = false;
            }

            Util.Global.Sprites.RemoveAll(x => x.name.Contains("InventoryText"));
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("CloseInventory"));
        }

        public static void UpdateInventoryCounts()
        {
            if (IsInventoryOpen())
            {
                DisplayInventory();
            }
        }

        public static bool IsInventoryOpen()
        {
            if (Util.Global.Sprites.Where(x => x.name == "Inventory").Count() > 0)
            {
                return true;
            }
            return false;
        }

    }
}
