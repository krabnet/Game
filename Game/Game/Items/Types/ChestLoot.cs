using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Items;

namespace Game.Items.Types
{
    public class ChestLoot
    {
        public Game.Objects.Sprite2d Get(Items.Item.ItemType IT, Vector2 Position)
        {
            Objects.Sprite2d ReturnSprite = new Objects.Sprite2d();
            List<Object> ActionObjects = new List<object>();
            List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>> DropReplace = new List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>>();
            Dictionary<Item.ItemType, Item.ItemType> ItemList = new Dictionary<Item.ItemType, Item.ItemType>();

            ReturnSprite = Items.Item.GetItemSprite("chest", IT, Position);
            ReturnSprite.Item.Stack = false;
            ReturnSprite.Item.BackGroundType = Item.ItemType.Floor;
            ReturnSprite.Inventory = new List<Objects.Item>();
            ReturnSprite.Inventory.AddRange(Actions.Shop.GetRandomItemList(0, 5));
            for (int i = 0; i < Util.Global.GetRandomInt(0, 7); i++)
            {
                ReturnSprite.Inventory.Add(Item.GetItemByType(Item.ItemType.Coin, Util.Global.DefaultPosition).Item);
            }
            ReturnSprite.Size = new Vector2(32, 32);
            ActionObjects.Add(ReturnSprite);
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Menu.Chest), "DisplayChest", ActionObjects));
            ActionObjects = new List<object>();
            ActionObjects.Add(ReturnSprite);
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));

            return ReturnSprite;
        }
    }
}
