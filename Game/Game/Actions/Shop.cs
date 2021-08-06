using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game.Actions
{
    public static class Shop
    {
        public static void EnterShop(Objects.Sprite2d Shop)
        {
            Util.Global.FightPreviousMap = Util.Global.CurrentMap;
            Util.Global.FightPreviousHeroLocation = new Vector2(Util.Global.Hero.Position.X, Util.Global.Hero.Position.Y);

            Vector3 mapVector = new Vector3(98, 98, 98);

            new Maps.Map().GenerateMap(mapVector, Game.Maps.MapData.Biome.Shop, 10, 10);
            new Maps.Map().WarpMap(mapVector, Util.Global.CurrentMap);

            Objects.Sprite2d ReturnDoor = new Objects.Sprite2d("doorway", "ShopExit", true, new Vector2(300, 300), new Vector2(596, 640) / 7, 0, Objects.Base.ControlType.None);
            ReturnDoor.orderNum = 1000;
            ReturnDoor.Position = Vector2.Add(Util.Global.Sprites.Where(x => x.name == "0:4").FirstOrDefault().Position, new Vector2(6, -50));
            ReturnDoor.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseAny, typeof(Actions.Shop), "ExitShop", null));
            ReturnDoor.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Actions.Shop), "ExitShop", null));
            Util.Global.Sprites.Add(ReturnDoor);
            Util.Base.GetLightSources();

            Vector2 BasePos = new Vector2(50, 200);
            Vector2 InitBasePos = BasePos;

            int i = 1;
            foreach (Objects.Item IT in Shop.Inventory)
            {
                string Name = "ShopItem:";
                Objects.Sprite2d Sprite = Items.Item.GetItemByType(IT.Type, BasePos);
                Sprite.name = Name + Sprite.name;
                Sprite.LightIgnor = true;
                Sprite.LightSourceDistance = 0f;
                Sprite.orderNum = 1000;
                Sprite.Size = new Vector2(32, 32);
                Sprite.Item = IT;
                List<Object> Objs3 = new List<object>();
                Objs3.Add(Shop);
                Objs3.Add(Sprite);
                Objs3.Add(IT.ID);
                Sprite.actionCall = new List<Actions.ActionCall>();
                Sprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Actions.Shop), "SelectItem", Objs3));
                Util.Global.Sprites.Add(Sprite);

                ShowHeroCoin();

                Util.Global.Sprites.Add(new Objects.Sprite2d(null, "ShopText" + Sprite.ID.ToString(), true, Vector2.Add(BasePos, new Vector2(17, 23)), Util.Global.DefaultPosition, 0, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == "ShopText" + Sprite.ID.ToString()).FirstOrDefault().text = Sprite.Item.Cost.ToString();
                Util.Global.Sprites.Where(x => x.name == "ShopText" + Sprite.ID.ToString()).FirstOrDefault().textSize = .7f;
                Util.Global.Sprites.Where(x => x.name == "ShopText" + Sprite.ID.ToString()).FirstOrDefault().color = Color.White;
                Util.Global.Sprites.Where(x => x.name == "ShopText" + Sprite.ID.ToString()).FirstOrDefault().boxColor = Color.Black;
                Util.Global.Sprites.Where(x => x.name == "ShopText" + Sprite.ID.ToString()).FirstOrDefault().orderNum = 1001;
                Util.Global.Sprites.Where(x => x.name == "ShopText" + Sprite.ID.ToString()).FirstOrDefault().LightIgnor = true;



                BasePos = Vector2.Add(BasePos, new Vector2(55, 0));
                i++;
                if (i == 6)
                {
                    i = 1;
                    BasePos = InitBasePos;
                    BasePos = Vector2.Add(BasePos, new Vector2(0, 55));
                }
            }

            Util.Global.Sprites.Where(x => x.ID == Util.Global.Hero.ID).FirstOrDefault().Position = new Vector2(150, 100);

            Season.GetMusic();

        }

        public static void ExitShop()
        {
            new Maps.Map().WarpMap(Util.Global.FightPreviousMap, new Vector3(98, 98, 98));
            Util.Global.Sprites.Where(x => x.ID == Util.Global.Hero.ID).FirstOrDefault().Position = Util.Global.FightPreviousHeroLocation;
            foreach (Objects.Sprite2d S in Util.Global.Pets)
            {
                Util.Global.Sprites.Where(X => X.ID == S.ID).FirstOrDefault().Position = Vector2.Subtract(Util.Global.FightPreviousHeroLocation, new Vector2((float)Util.Global.GetRandomInt(1, 150), (float)Util.Global.GetRandomInt(1, 150)));
                Util.Global.Sprites.Where(X => X.ID == S.ID).FirstOrDefault().Actor.aiState = Objects.Actor.AiState.Chasing;
            }
            Season.GetMusic();
        }

        public static void SelectItem(Objects.Sprite2d Shop, Objects.Sprite2d SaleItem, Guid ShopItemID)
        {
            int HeroCoin = 0;
            if (Util.Global.Sprites.Where(x => x.Item.State == Items.Item.ItemState.Inventory && x.Item.Type == Items.Item.ItemType.Coin).Count() > 0)
            {
                HeroCoin = Util.Global.Sprites.Where(x => x.Item.State == Items.Item.ItemState.Inventory && x.Item.Type == Items.Item.ItemType.Coin).FirstOrDefault().Item.Number;
            }

           
            Util.Global.Sprites.RemoveAll(x => x.name == "ActiveSale");
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("SellItem"));
            Objects.Sprite2d Sprite = new Objects.Sprite2d("drop", "ActiveSale", true, SaleItem.Position, SaleItem.Size, 0, Objects.Base.ControlType.None);
            Sprite.orderNum = 1001;
            if (HeroCoin >= SaleItem.Item.Cost)
            {
                Sprite.color = Color.Green * 0.5f;
            }
            else
            {
                    Sprite.color = Color.Red * 0.5f;
            }
            Sprite.LightIgnor = true;
            Util.Global.Sprites.Add(Sprite);

            if (HeroCoin >= SaleItem.Item.Cost)
            {
                string name = "SellItem-" + SaleItem.ToString();
                Util.Global.Sprites.Add(new Objects.Sprite2d(null, name, true, new Vector2(-150, 50), new Vector2(), 0, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().text = "Buy " + SaleItem.Item.Type.ToString() + " for " + SaleItem.Item.Cost.ToString();
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().textSize = 1f;
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().color = Color.Red;
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().boxColor = Color.Blue;
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().orderNum = 980;
                List<Object> obj2 = new List<object>();
                obj2.Add(Shop);
                obj2.Add(SaleItem);
                obj2.Add(ShopItemID);
                ActionCall call2 = new ActionCall(ActionType.Mouse, typeof(Actions.Shop), "BuyItem", obj2);
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().actionCall.Add(call2);
            }
        }

        public static void BuyItem(Objects.Sprite2d Shop, Objects.Sprite2d SaleItem, Guid ShopItemID)
        {
            Util.Global.Sprites.RemoveAll(x => x.name == "SellItem-" + SaleItem.ToString());
            Util.Global.Sprites.RemoveAll(x => x.name == "ShopText" + SaleItem.ID.ToString());
            Util.Global.Sprites.RemoveAll(x => x.name == "ActiveSale");
            Util.Global.Sprites.RemoveAll(x => x.ID == SaleItem.ID);

            Util.Global.MainMap[(int)Util.Global.FightPreviousMap.X, (int)Util.Global.FightPreviousMap.Y, (int)Util.Global.FightPreviousMap.Z].Sprite2d.Where(x => x.ID == Shop.ID).FirstOrDefault().Inventory.RemoveAll(y => y.ID == ShopItemID);

            Util.Global.Sprites.Where(x => x.Item.State == Items.Item.ItemState.Inventory && x.Item.Type == Items.Item.ItemType.Coin).FirstOrDefault().Item.Number -= SaleItem.Item.Cost;
            Items.ItemActions.AddItemToInventory(Items.Item.GetItemByType(SaleItem.Item.Type, Util.Global.DefaultPosition), 1 , 1);
            if (Menu.Inventory.IsInventoryOpen())
            { Menu.Inventory.DisplayInventory(); }
            ShowHeroCoin();
        }

        public static void ShowHeroCoin()
        {
            Util.Global.Sprites.RemoveAll(x=>x.name == "HeroCoinText");
            
            int HeroCoin = 0;
            if (Util.Global.Sprites.Where(x => x.Item.State == Items.Item.ItemState.Inventory && x.Item.Type == Items.Item.ItemType.Coin).Count() > 0)
            {
                HeroCoin = Util.Global.Sprites.Where(x => x.Item.State == Items.Item.ItemState.Inventory && x.Item.Type == Items.Item.ItemType.Coin).FirstOrDefault().Item.Number;
            }

            Util.Global.Sprites.Add(new Objects.Sprite2d(null, "HeroCoinText", true, new Vector2(-150, 0), new Vector2(10,10), 0, Objects.Base.ControlType.None));
            Util.Global.Sprites.Where(x => x.name == "HeroCoinText").FirstOrDefault().text = "Coins: " + HeroCoin;
            Util.Global.Sprites.Where(x => x.name == "HeroCoinText").FirstOrDefault().textSize = 1F;
            Util.Global.Sprites.Where(x => x.name == "HeroCoinText").FirstOrDefault().color = Color.Black;
            Util.Global.Sprites.Where(x => x.name == "HeroCoinText").FirstOrDefault().boxColor = Color.Gold;
            Util.Global.Sprites.Where(x => x.name == "HeroCoinText").FirstOrDefault().orderNum = 500;
            Util.Global.Sprites.Where(x => x.name == "HeroCoinText").FirstOrDefault().LightIgnor = true;
        }

        public static void ResetAllShopsInventory()
        {
            foreach (Maps.Map M in Util.Global.MainMap)
            {
                if (M != null)
                {
                    foreach (Objects.Sprite2d Shops in M.Sprite2d.Where(x => x.Item != null && x.Item.Type == Items.Item.ItemType.Shop))
                    {
                        Util.Global.MainMap[(int)M.MapVector.X, (int)M.MapVector.Y, (int)M.MapVector.Z].Sprite2d.Where(x => x.ID == Shops.ID).FirstOrDefault().Inventory = GetRandomItemList(3, 10);
                    }
                }
            }

            foreach (Objects.Sprite2d Shops in Util.Global.Sprites.Where(x => x.Item != null && x.Item.Type == Items.Item.ItemType.Shop))
            {
                Util.Global.Sprites.Where(x => x.ID == Shops.ID).FirstOrDefault().Inventory = GetRandomItemList(3,10);
            }
        }

        public static List<Objects.Item> GetRandomItemList(int MinItems, int MaxItems)
        {
            List<Objects.Item> Inv = new List<Objects.Item>();
            List<Items.Item.ItemType> ValidShopItems = new List<Items.Item.ItemType>();
            ValidShopItems.Add(Items.Item.ItemType.Tent);
            ValidShopItems.Add(Items.Item.ItemType.WorkBench);
            ValidShopItems.Add(Items.Item.ItemType.Stove);
            ValidShopItems.Add(Items.Item.ItemType.Caldron);
            ValidShopItems.Add(Items.Item.ItemType.Torch);
            ValidShopItems.Add(Items.Item.ItemType.BerryHealth);
            ValidShopItems.Add(Items.Item.ItemType.PotionHealth);
            ValidShopItems.Add(Items.Item.ItemType.BerrySpeed);
            ValidShopItems.Add(Items.Item.ItemType.PotionSpeed);
            ValidShopItems.Add(Items.Item.ItemType.BerryStrength);
            ValidShopItems.Add(Items.Item.ItemType.PotionStrength);
            ValidShopItems.Add(Items.Item.ItemType.BerryDexterity);
            ValidShopItems.Add(Items.Item.ItemType.PotionDexterity);
            ValidShopItems.Add(Items.Item.ItemType.Oil);
            ValidShopItems.Add(Items.Item.ItemType.Log);
            ValidShopItems.Add(Items.Item.ItemType.IronBar);
            ValidShopItems.Add(Items.Item.ItemType.SilverBar);
            ValidShopItems.Add(Items.Item.ItemType.GoldBar);
            ValidShopItems.Add(Items.Item.ItemType.Grain);
            ValidShopItems.Add(Items.Item.ItemType.Bed);

            int numberOfItems = Util.Global.GetRandomInt(MinItems, MaxItems);
            for (int i = 1; i <= numberOfItems; i++)
            {
                Items.Item.ItemType ThisItem = ValidShopItems.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                Objects.Sprite2d Item = Items.Item.GetItemByType(ThisItem, Util.Global.DefaultPosition);
                Inv.Add(Item.Item);
            }
            
            return Inv;
        }

    }
}
