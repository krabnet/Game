using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Game.Actions;

namespace Game.Menu
{
    public class Craft
    {
        public static void DisplayCraft(Crafting.Type CraftType)
        {
            Vector2 BasePos = Vector2.Subtract(Util.Global.Hero.Position, new Vector2(-75, 225));
            string Name = "Craft";
            Util.Global.Sprites.Add(new Objects.Sprite2d(Util.Global.AllTexture2D.Where(x => x.Name == "Craft").FirstOrDefault(), Name, true, BasePos, new Vector2(400, 400), 0, Objects.Base.ControlType.None));
            Util.Global.Sprites.Where(x => x.name == "Craft").FirstOrDefault().orderNum = 99;
            Util.Global.Sprites.Where(x => x.name == "Craft").FirstOrDefault().LightIgnor = true;

            Util.Global.Sprites.Add(new Objects.Sprite2d(Util.Global.AllTexture2D.Where(x => x.Name == "blank").FirstOrDefault(), "CloseCraft", true, Vector2.Add(BasePos,new Vector2(275,0)), new Vector2(75, 75), 0, Objects.Base.ControlType.None));
            Util.Global.Sprites.Where(x => x.name == "CloseCraft").FirstOrDefault().LightIgnor = true;
            Util.Global.Sprites.Where(x => x.name == "CloseCraft").FirstOrDefault().orderNum = 1001;
            ActionCall CloseCraftAC = new ActionCall(ActionType.Mouse, typeof(Craft), "HideCraft", null);
            Util.Global.Sprites.Where(x => x.name == "CloseCraft").FirstOrDefault().actionCall.Add(CloseCraftAC);

                
            BasePos = Vector2.Add(BasePos, new Vector2(7, 7));
            Vector2 InitBasePos = BasePos;
            int i = 1;

            foreach (Actions.Crafting.Recipie R in Actions.Crafting.GetRecipiesByType(CraftType))
            {
                Objects.Sprite2d Sprite = Items.Item.GetItemByType(R.Result, BasePos);
                Sprite.name = "Craft" + Sprite.name;
                Sprite.LightIgnor = true;
                Sprite.LightSourceDistance = 0f;
                Sprite.orderNum = 1000;
                List<Object> Objs3 = new List<object>();
                Objs3.Add(Sprite);
                Objs3.Add(R);
                Objs3.Add(InitBasePos);
                Sprite.actionCall = new List<Actions.ActionCall>();
                Sprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Menu.Craft), "SelectItem", Objs3));

                Util.Global.Sprites.Add(Sprite);

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

        public static void HideCraft()
        {
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("Craft"));
        }

        public static void CraftItem(Objects.Sprite2d Item, Actions.Crafting.Recipie Rec)
        {
            foreach (KeyValuePair<Items.Item.ItemType, int> R in Rec.ItemCost)
            {
                Util.Global.Sprites.Where(x => x.Item.State == Objects.Item.ItemState.Inventory && x.Item.Type == R.Key).FirstOrDefault().Item.Number -= R.Value;
            }
            new Objects.Item().AddItemToInventory(Items.Item.GetItemByType(Rec.Result, new Vector2(0, 0)), 1, 1);
            Util.Global.Sprites.RemoveAll(x => x.name == "ActiveCraft");
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("CraftRec"));
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("CraftItem"));
        }

        public static void SelectItem(Objects.Sprite2d Item, Actions.Crafting.Recipie Rec, Vector2 BasePos)
        {
            Util.Global.Sprites.RemoveAll(x => x.name == "ActiveCraft");
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("CraftRec"));
            Objects.Sprite2d Sprite = new Objects.Sprite2d(Util.Global.AllTexture2D.Where(x => x.Name == "drop").FirstOrDefault(), "ActiveCraft", true, Item.Position, Item.Size, 0, Objects.Base.ControlType.None);
            Sprite.orderNum = 1001;
            Sprite.color = Color.Yellow * 0.5f;
            Sprite.LightIgnor = true;
            Util.Global.Sprites.Add(Sprite);

            BasePos = Vector2.Add(BasePos, new Vector2(0, 44*4));
            Vector2 InitBasePos = BasePos;
            int i = 1;
            bool cancraft = true;
            foreach (KeyValuePair<Items.Item.ItemType, int> R in Rec.ItemCost)
            {
               

                Objects.Sprite2d RecSprite = Items.Item.GetItemByType(R.Key, BasePos);
                RecSprite.name = "CraftRec" + Sprite.name;
                RecSprite.active = true;
                RecSprite.orderNum = 1000;
                RecSprite.actionCall = new List<ActionCall>();
                Util.Global.Sprites.Add(RecSprite);

                Util.Global.Sprites.Add(new Objects.Sprite2d(null, "CraftRecText" + RecSprite.ID, true, Vector2.Add(BasePos, new Vector2(17, 23)), new Vector2(0, 0), 0, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == "CraftRecText" + RecSprite.ID).FirstOrDefault().text = R.Value.ToString();
                Util.Global.Sprites.Where(x => x.name == "CraftRecText" + RecSprite.ID).FirstOrDefault().textSize = .7f;
                Util.Global.Sprites.Where(x => x.name == "CraftRecText" + RecSprite.ID).FirstOrDefault().color = Color.White;
                Util.Global.Sprites.Where(x => x.name == "CraftRecText" + RecSprite.ID).FirstOrDefault().orderNum = 1001;
                if (Util.Global.Sprites.Where(x => x.Item.State == Objects.Item.ItemState.Inventory && x.Item.Type == R.Key).FirstOrDefault() != null && Util.Global.Sprites.Where(x => x.Item.State == Objects.Item.ItemState.Inventory && x.Item.Type == R.Key).FirstOrDefault().Item.Number >= R.Value)
                {
                    Util.Global.Sprites.Where(x => x.name == "CraftRecText" + RecSprite.ID).FirstOrDefault().boxColor = Color.Green;
                }
                else
                {
                    Util.Global.Sprites.Where(x => x.name == "CraftRecText" + RecSprite.ID).FirstOrDefault().boxColor = Color.Red;
                    cancraft = false;
                }



                BasePos = Vector2.Add(BasePos, new Vector2(44, 0));
                i++;
                if (i == 7)
                {
                    i = 1;
                    BasePos = Vector2.Add(BasePos, new Vector2(0, 44));
                }
            }
            if (cancraft)
            {
                Util.Global.Sprites.RemoveAll(x => x.name == "CraftItem");
                InitBasePos = Vector2.Add(InitBasePos, new Vector2(0, 44 * 2));
                Util.Global.Sprites.Add(new Objects.Sprite2d(null, "CraftItem", true, Vector2.Subtract(InitBasePos, new Vector2(-30, 0)), new Vector2(0, 0), 0, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == "CraftItem").FirstOrDefault().text = "CraftItem";
                Util.Global.Sprites.Where(x => x.name == "CraftItem").FirstOrDefault().textSize = 1f;
                Util.Global.Sprites.Where(x => x.name == "CraftItem").FirstOrDefault().color = Color.Red;
                Util.Global.Sprites.Where(x => x.name == "CraftItem").FirstOrDefault().boxColor = Color.Blue;
                Util.Global.Sprites.Where(x => x.name == "CraftItem").FirstOrDefault().orderNum = 980;
                List<Object> obj2 = new List<object>();
                obj2.Add(Item);
                obj2.Add(Rec);
                ActionCall call2 = new ActionCall(ActionType.Mouse, typeof(Craft), "CraftItem", obj2);
                Util.Global.Sprites.Where(x => x.name == "CraftItem").FirstOrDefault().actionCall.Add(call2);
            }
        }

    }
}
