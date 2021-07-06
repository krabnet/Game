using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Game.Actions;

namespace Game.Menu
{
    public class Chest
    {

        public static void DisplayChest(Objects.Sprite2d ChestItem)
        {
            ActorStats.RemoveAllStats();
            if (Util.Global.Sprites.Where(x => x.Item != null && x.Item.State == Items.Item.ItemState.Hand).Count() == 0)
           {
               Say.speak(Util.Global.Hero.ID, "Looky Looky! \nSomething shiney!");

                HideChest();
                Util.Global.Chest = ChestItem;
                Vector2 BasePos = Vector2.Subtract(Util.Global.Hero.Position, new Vector2(325, -100));
                string Name = "ChestInv";
                Util.Global.Sprites.Add(new Objects.Sprite2d("ChestInv", Name, true, BasePos, new Vector2(400, 400), 0, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().orderNum = 500;
                Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().LightIgnor = true;

                Util.Global.Sprites.Add(new Objects.Sprite2d("blank", "Close" + Name, true, Vector2.Add(BasePos, new Vector2(275, 0)), new Vector2(75, 75), 0, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == "Close" + Name).FirstOrDefault().LightIgnor = true;
                Util.Global.Sprites.Where(x => x.name == "Close" + Name).FirstOrDefault().orderNum = 2001;
                ActionCall CloseAC = new ActionCall(ActionType.Mouse, typeof(Chest), "HideChest", null);
                Util.Global.Sprites.Where(x => x.name == "Close" + Name).FirstOrDefault().actionCall.Add(CloseAC);


                BasePos = Vector2.Add(BasePos, new Vector2(7, 7));
                Vector2 InitBasePos = BasePos;
                int i = 1;

                foreach (Objects.Item IT in ChestItem.Inventory)
                {
                    Objects.Sprite2d Sprite = Items.Item.GetItemByType(IT.Type, BasePos);
                    Sprite.name = Name + Sprite.name;
                    Sprite.LightIgnor = true;
                    Sprite.LightSourceDistance = 0f;
                    Sprite.orderNum = 1000;
                    Sprite.Size = new Vector2(32, 32);
                    Sprite.Item = IT;

                    List<Object> Objs3 = new List<object>();
                    Objs3.Add(Sprite);
                    Objs3.Add(ChestItem);
                    Sprite.actionCall = new List<Actions.ActionCall>();
                    Sprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Menu.Chest), "AddItemToInv", Objs3));

                    Util.Global.Sprites.Add(Sprite);


                    Util.Global.Sprites.Add(new Objects.Sprite2d(null, Name + "ChestText" + Sprite.ID.ToString(), true, Vector2.Add(BasePos, new Vector2(17, 23)), Util.Global.DefaultPosition, 0, Objects.Base.ControlType.None));
                    Util.Global.Sprites.Where(x => x.name == Name + "ChestText" + Sprite.ID.ToString()).FirstOrDefault().text = Sprite.Item.Number.ToString();
                    Util.Global.Sprites.Where(x => x.name == Name + "ChestText" + Sprite.ID.ToString()).FirstOrDefault().textSize = .7f;
                    Util.Global.Sprites.Where(x => x.name == Name + "ChestText" + Sprite.ID.ToString()).FirstOrDefault().color = Color.White;
                    Util.Global.Sprites.Where(x => x.name == Name + "ChestText" + Sprite.ID.ToString()).FirstOrDefault().boxColor = Color.Black;
                    Util.Global.Sprites.Where(x => x.name == Name + "ChestText" + Sprite.ID.ToString()).FirstOrDefault().orderNum = 1001;
                    Util.Global.Sprites.Where(x => x.name == Name + "ChestText" + Sprite.ID.ToString()).FirstOrDefault().LightIgnor = true;



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

        public static void HideChest()
        {
            Util.Global.Chest = null;
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("ChestInv"));
        }

        public static void AddItemToInv(Objects.Sprite2d Item, Objects.Sprite2d Chest)
        {
            string Name = "ChestInv";
            Util.Global.Sprites.RemoveAll(x => x.ID == Item.ID);
            Util.Global.Sprites.RemoveAll(x => x.name == Name + "ChestText" + Item.ID.ToString());
            Util.Global.Sprites.Where(x => x.ID == Chest.ID).FirstOrDefault().Inventory.RemoveAll(x => x.Type == Item.Item.Type);

            Objects.Sprite2d NewItem = Items.Item.GetItemByType(Item.Item.Type,Item.Position);
            NewItem.active = false;
            NewItem.orderNum = 1001;

            Items.ItemActions.AddItemToInventory(NewItem, Item.Item.Number, 1);
        }
    }
}
