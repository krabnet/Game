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
            Vector2 BasePos = Vector2.Subtract(Util.Global.Hero.Position, new Vector2(275, 225));
            string Name = "Inventory";
            Util.Global.Sprites.Add(new Objects.Sprite2d(Util.Global.AllTexture2D.Where(x => x.Name == "Inventory").FirstOrDefault(), Name, true, BasePos, new Vector2(400, 400), 0, Objects.Base.ControlType.None));
            Util.Global.Sprites.Where(x => x.name == "Inventory").FirstOrDefault().orderNum = 99;
            Util.Global.Sprites.Where(x => x.name == "Inventory").FirstOrDefault().LightIgnor = true;

            Util.Global.Sprites.Add(new Objects.Sprite2d(Util.Global.AllTexture2D.Where(x => x.Name == "blank").FirstOrDefault(), "CloseInv", true, Vector2.Add(BasePos, new Vector2(275, 0)), new Vector2(75, 75), 0, Objects.Base.ControlType.None));
            Util.Global.Sprites.Where(x => x.name == "CloseInv").FirstOrDefault().LightIgnor = true;
            Util.Global.Sprites.Where(x => x.name == "CloseInv").FirstOrDefault().orderNum = 1001;
            ActionCall CloseCraftAC = new ActionCall(ActionType.Mouse, typeof(Inventory), "HideInventory", null);
            Util.Global.Sprites.Where(x => x.name == "CloseInv").FirstOrDefault().actionCall.Add(CloseCraftAC);

            BasePos = Vector2.Add(BasePos, new Vector2(7, 7));
            Vector2 InitBasePos = BasePos;
            int i = 1;

            foreach (Objects.Sprite2d S in Util.Global.Sprites.Where(x => x.Item.State == Objects.Item.ItemState.Inventory).OrderBy(y => y.name).ToList())
            {
                Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().active = true;
                Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Position = BasePos;
                Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().LightIgnor = true;
                Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().LightSourceDistance = 0f;
                Util.Global.Sprites.Add(new Objects.Sprite2d(null, "InventoryText" + S.ID, true, Vector2.Add(BasePos, new Vector2(17, 23)), new Vector2(0, 0), 0, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == "InventoryText" + S.ID).FirstOrDefault().text = S.Item.Number.ToString();
                Util.Global.Sprites.Where(x => x.name == "InventoryText" + S.ID).FirstOrDefault().textSize = .7f;
                Util.Global.Sprites.Where(x => x.name == "InventoryText" + S.ID).FirstOrDefault().color = Color.White;
                Util.Global.Sprites.Where(x => x.name == "InventoryText" + S.ID).FirstOrDefault().boxColor = Color.Black;
                Util.Global.Sprites.Where(x => x.name == "InventoryText" + S.ID).FirstOrDefault().orderNum = 980;
                Util.Global.Sprites.Where(x => x.name == "InventoryText" + S.ID).FirstOrDefault().LightIgnor = true;
                
                BasePos = Vector2.Add(BasePos, new Vector2(44, 0));
                i++;
                if (i == 7)
                {
                    i = 1;
                    BasePos = InitBasePos;
                    BasePos = Vector2.Add(BasePos, new Vector2(0, 44));
                }
            }

        }

        public static void HideInventory()
        {
            Util.Global.Sprites.RemoveAll(x => x.name == "Inventory");
            foreach (Objects.Sprite2d S in Util.Global.Sprites.Where(x => x.Item.State == Objects.Item.ItemState.Inventory))
            {
                Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().active = false;
            }

            Util.Global.Sprites.RemoveAll(x => x.name.Contains("InventoryText"));
        }

        public static void UpdateInventoryCounts()
        {
            if (Util.Global.Sprites.Where(x => x.name == "Inventory").Count() > 0)
            {
                Util.Global.Sprites.RemoveAll(x => x.name.Contains("InventoryText"));
                //Vector2 BasePos = Vector2.Subtract(Util.Global.Hero.Position, new Vector2(275, 225));
                Vector2 BasePos = Util.Global.Sprites.Where(x => x.name == "Inventory").FirstOrDefault().Position;
                BasePos = Vector2.Add(BasePos, new Vector2(7, 7));
                Vector2 InitBasePos = BasePos;
                int i = 1;
                foreach (Objects.Sprite2d S in Util.Global.Sprites.Where(x => x.Item.State != Objects.Item.ItemState.Null).ToList())
                {
                    Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().active = true;
                    Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Position = BasePos;
                    Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().LightIgnor = true;
                    Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().LightSourceDistance = 0f;
                    Util.Global.Sprites.Add(new Objects.Sprite2d(null, "InventoryText" + S.ID, true, Vector2.Add(BasePos, new Vector2(17, 23)), new Vector2(0, 0), 0, Objects.Base.ControlType.None));
                    Util.Global.Sprites.Where(x => x.name == "InventoryText" + S.ID).FirstOrDefault().text = S.Item.Number.ToString();
                    Util.Global.Sprites.Where(x => x.name == "InventoryText" + S.ID).FirstOrDefault().textSize = .7f;
                    Util.Global.Sprites.Where(x => x.name == "InventoryText" + S.ID).FirstOrDefault().color = Color.White;
                    Util.Global.Sprites.Where(x => x.name == "InventoryText" + S.ID).FirstOrDefault().boxColor = Color.Black;
                    Util.Global.Sprites.Where(x => x.name == "InventoryText" + S.ID).FirstOrDefault().orderNum = 980;
                    Util.Global.Sprites.Where(x => x.name == "InventoryText" + S.ID).FirstOrDefault().LightIgnor = true;

                    BasePos = Vector2.Add(BasePos, new Vector2(44, 0));
                    i++;
                    if (i == 7)
                    {
                        i = 1;
                        BasePos = InitBasePos;
                        BasePos = Vector2.Add(BasePos, new Vector2(0, 44));
                    }
                }
            }
        }

    }
}
