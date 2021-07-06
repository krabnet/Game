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
                //ActorStats.RemoveAllStats();
                HideCraft();
                //Vector2 BasePos = Vector2.Subtract(Util.Global.Hero.Position, new Vector2(-25, 225));
                //Vector2 BasePos = new Vector2(150, Util.Global.screenSizeHeight - 130);
                Vector2 BasePos = new Vector2(150, 0);
                string Name = "Craft";
                Util.Global.Sprites.Add(new Objects.Sprite2d("Craft", Name, true, BasePos, new Vector2(420,130), 0, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == "Craft").FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
                Util.Global.Sprites.Where(x => x.name == "Craft").FirstOrDefault().orderNum = 10500;
                Util.Global.Sprites.Where(x => x.name == "Craft").FirstOrDefault().LightIgnor = true;

                Util.Global.Sprites.RemoveAll(x => x.name == "CloseCraft");
                Util.Global.Sprites.Add(new Objects.Sprite2d(null, "CloseCraft", true, Vector2.Add(BasePos, new Vector2(405, 5)), new Vector2(10, 10), 0, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == "CloseCraft").FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
                Util.Global.Sprites.Where(x => x.name == "CloseCraft").FirstOrDefault().text = "X";
                Util.Global.Sprites.Where(x => x.name == "CloseCraft").FirstOrDefault().textSize = 1f;
                Util.Global.Sprites.Where(x => x.name == "CloseCraft").FirstOrDefault().color = Color.Azure;
                Util.Global.Sprites.Where(x => x.name == "CloseCraft").FirstOrDefault().boxColor = Color.Black;
                Util.Global.Sprites.Where(x => x.name == "CloseCraft").FirstOrDefault().orderNum = 10980;
                List<Object> obj3 = new List<object>();
                ActionCall call3 = new ActionCall(ActionType.Mouse, typeof(Craft), "HideCraft", obj3);
                Util.Global.Sprites.Where(x => x.name == "CloseCraft").FirstOrDefault().actionCall.Add(call3);


                BasePos = Vector2.Add(BasePos, new Vector2(7, 9));
                Vector2 InitBasePos = BasePos;
                int i = 1;

                foreach (Actions.Crafting.Recipie R in Actions.Crafting.GetRecipiesByType(CraftType))
                {
                    Objects.Sprite2d Sprite = Items.Item.GetItemByType(R.Result, BasePos);
                    Sprite.Viewtype = Objects.Base.ViewType.HUD;
                    Sprite.Size = new Vector2(30, 30);
                    Sprite.name = "Craft" + Sprite.name;
                    Sprite.active = true;
                    Sprite.LightIgnor = true;
                    Sprite.LightSourceDistance = 0f;
                    Sprite.orderNum = 101000;
                    List<Object> Objs3 = new List<object>();
                    Objs3.Add(Sprite);
                    Objs3.Add(R);
                    Objs3.Add(InitBasePos);
                    Sprite.actionCall = new List<Actions.ActionCall>();
                    Sprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Menu.Craft), "SelectItem", Objs3));
                    Util.Global.Sprites.Add(Sprite);

                    if (R.ResultNum > 1)
                    {
                        Util.Global.Sprites.Add(new Objects.Sprite2d(null, "CraftRetText" + Sprite.ID, true, Vector2.Add(BasePos, new Vector2(17, 23)), Util.Global.DefaultPosition, 0, Objects.Base.ControlType.None));
                        Util.Global.Sprites.Where(x => x.name == "CraftRetText" + Sprite.ID).FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
                        Util.Global.Sprites.Where(x => x.name == "CraftRetText" + Sprite.ID).FirstOrDefault().text = R.ResultNum.ToString();
                        Util.Global.Sprites.Where(x => x.name == "CraftRetText" + Sprite.ID).FirstOrDefault().textSize = .7f;
                        Util.Global.Sprites.Where(x => x.name == "CraftRetText" + Sprite.ID).FirstOrDefault().color = Color.White;
                        Util.Global.Sprites.Where(x => x.name == "CraftRetText" + Sprite.ID).FirstOrDefault().orderNum = 101001;
                        Util.Global.Sprites.Where(x => x.name == "CraftRetText" + Sprite.ID).FirstOrDefault().boxColor = Color.Black;
                    }


                    BasePos = Vector2.Add(BasePos, new Vector2(35, 0));
                    i++;
                    if (i == 9)
                    {
                        i = 1;
                        BasePos = InitBasePos;
                        BasePos = Vector2.Add(BasePos, new Vector2(0, 35));
                    }
                }
        }

        public static void HideCraft()
        {
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("Craft"));
        }

        public static void CraftItem(Objects.Sprite2d Item, Actions.Crafting.Recipie Rec, int Count)
        {
            foreach (KeyValuePair<Items.Item.ItemType, int> R in Rec.ItemCost)
            {
                Util.Global.Sprites.Where(x => x.Item.State == Items.Item.ItemState.Inventory && x.Item.Type == R.Key).FirstOrDefault().Item.Number -= R.Value * Count;
            }
            Items.ItemActions.AddItemToInventory(Items.Item.GetItemByType(Rec.Result, Util.Global.DefaultPosition), Rec.ResultNum*Count, 1);
            Util.Global.Sprites.RemoveAll(x => x.name == "ActiveCraft");
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("CraftRec"));
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("CraftItem"));
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("CraftResultText"));
            if (Menu.Inventory.IsInventoryOpen())
            { Menu.Inventory.DisplayInventory(); }
        }

        public static void SelectItem(Objects.Sprite2d Item, Actions.Crafting.Recipie Rec, Vector2 BasePos)
        {
            Util.Global.Sprites.RemoveAll(x => x.name == "ActiveCraft");
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("CraftRec"));
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("CraftResultText"));
            Objects.Sprite2d Sprite = new Objects.Sprite2d("drop", "ActiveCraft", true, Item.Position, Item.Size, 0, Objects.Base.ControlType.None);
            Sprite.Viewtype = Objects.Base.ViewType.HUD;
            Sprite.orderNum = 101001;
            Sprite.color = Color.Yellow * 0.5f;
            Sprite.LightIgnor = true;
            Util.Global.Sprites.Add(Sprite);
            Vector2 InitBasePos = BasePos;
            BasePos = Vector2.Add(BasePos, new Vector2(0, 80));

            Util.Global.Sprites.Add(new Objects.Sprite2d(null, "CraftResultText" + Sprite.ID, true, Vector2.Add(InitBasePos, new Vector2(280, 90)), Util.Global.DefaultPosition, 0, Objects.Base.ControlType.None));
            Util.Global.Sprites.Where(x => x.name == "CraftResultText" + Sprite.ID).FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
            Util.Global.Sprites.Where(x => x.name == "CraftResultText" + Sprite.ID).FirstOrDefault().text = Item.Item.Description;
            Util.Global.Sprites.Where(x => x.name == "CraftResultText" + Sprite.ID).FirstOrDefault().textSize = 1.3f;
            Util.Global.Sprites.Where(x => x.name == "CraftResultText" + Sprite.ID).FirstOrDefault().color = Color.White;
            Util.Global.Sprites.Where(x => x.name == "CraftResultText" + Sprite.ID).FirstOrDefault().orderNum = 101001;
            Util.Global.Sprites.Where(x => x.name == "CraftResultText" + Sprite.ID).FirstOrDefault().boxColor = Color.Black;

            int i = 1;
            bool cancraft = true;
            foreach (KeyValuePair<Items.Item.ItemType, int> R in Rec.ItemCost)
            {
               

                Objects.Sprite2d RecSprite = Items.Item.GetItemByType(R.Key, BasePos);
                RecSprite.Viewtype = Objects.Base.ViewType.HUD;
                RecSprite.name = "CraftRec" + Sprite.name;
                RecSprite.Size = new Vector2(30, 30);
                RecSprite.active = true;
                RecSprite.orderNum = 101000;
                RecSprite.LightIgnor = true;
                RecSprite.actionCall = new List<ActionCall>();
                Util.Global.Sprites.Add(RecSprite);

                Util.Global.Sprites.Add(new Objects.Sprite2d(null, "CraftRecText" + RecSprite.ID, true, Vector2.Add(BasePos, new Vector2(17, 23)), Util.Global.DefaultPosition, 0, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == "CraftRecText" + RecSprite.ID).FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
                Util.Global.Sprites.Where(x => x.name == "CraftRecText" + RecSprite.ID).FirstOrDefault().text = R.Value.ToString();
                Util.Global.Sprites.Where(x => x.name == "CraftRecText" + RecSprite.ID).FirstOrDefault().textSize = .7f;
                Util.Global.Sprites.Where(x => x.name == "CraftRecText" + RecSprite.ID).FirstOrDefault().color = Color.White;
                Util.Global.Sprites.Where(x => x.name == "CraftRecText" + RecSprite.ID).FirstOrDefault().orderNum = 101001;
                if (Util.Global.Sprites.Where(x => x.Item.State == Items.Item.ItemState.Inventory && x.Item.Type == R.Key).FirstOrDefault() != null && Util.Global.Sprites.Where(x => x.Item.State == Items.Item.ItemState.Inventory && x.Item.Type == R.Key).FirstOrDefault().Item.Number >= R.Value)
                {
                    Util.Global.Sprites.Where(x => x.name == "CraftRecText" + RecSprite.ID).FirstOrDefault().boxColor = Color.Green;
                }
                else
                {
                    Util.Global.Sprites.Where(x => x.name == "CraftRecText" + RecSprite.ID).FirstOrDefault().boxColor = Color.Red;
                    cancraft = false;
                }



                BasePos = Vector2.Add(BasePos, new Vector2(34, 0));
                i++;
                if (i == 9)
                {
                    i = 1;
                    BasePos = Vector2.Add(BasePos, new Vector2(0, 34));
                }
            }
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("CraftItem-"));
            if (cancraft)
            {
                InitBasePos = Vector2.Add(InitBasePos, new Vector2(235, 10));
                int[] options = new int[5] {1,5,10,25,50};
                bool cancraftamount = true;
                foreach (int I in options)
                {
                    foreach (KeyValuePair<Items.Item.ItemType, int> R in Rec.ItemCost)
                    {
                        if (Util.Global.Sprites.Where(x => x.Item.State == Items.Item.ItemState.Inventory && x.Item.Type == R.Key).FirstOrDefault() != null && Util.Global.Sprites.Where(x => x.Item.State == Items.Item.ItemState.Inventory && x.Item.Type == R.Key).FirstOrDefault().Item.Number < R.Value*I)
                        {
                            cancraftamount = false;
                        }
                    }
                    if (cancraftamount)
                    {
                        string name = "CraftItem-" + I.ToString();
                        InitBasePos = Vector2.Add(InitBasePos, new Vector2(25, 0));
                        Util.Global.Sprites.Add(new Objects.Sprite2d(null, name, true, Vector2.Subtract(InitBasePos, new Vector2(-30, 0)), Util.Global.DefaultPosition, 0, Objects.Base.ControlType.None));
                        Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
                        Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().text = I.ToString();
                        Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().textSize = 1f;
                        Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().color = Color.Red;
                        Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().boxColor = Color.Blue;
                        Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().orderNum = 10980;
                        List<Object> obj2 = new List<object>();
                        obj2.Add(Item);
                        obj2.Add(Rec);
                        obj2.Add(I);
                        ActionCall call2 = new ActionCall(ActionType.Mouse, typeof(Craft), "CraftItem", obj2);
                        Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().actionCall.Add(call2);
                    }
                }
            }
        }

    }
}
