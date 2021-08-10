using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Items;

namespace Game.Items.Types
{
    public class Shovel
    {
        public Game.Objects.Sprite2d Get(Items.Item.ItemType IT, Vector2 Position)
        {
            Objects.Sprite2d ReturnSprite = new Objects.Sprite2d();
            List<Object> ActionObjects = new List<object>();
            List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>> DropReplace = new List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>>();
            Dictionary<Item.ItemType, Item.ItemType> ItemList = new Dictionary<Item.ItemType, Item.ItemType>();

            ReturnSprite = Items.Item.GetItemSprite("shovel", IT, Position);
            ReturnSprite.Item.Description = "Dig Dig";
            ReturnSprite.Item.Stack = false;
            ReturnSprite.collisionSound = "Sounds/shovel";
            ActionObjects.Add(ReturnSprite);
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
            ActionObjects = new List<object>();
            ItemList.Add(Item.ItemType.Sand, Item.ItemType.Grass);
            ItemList.Add(Item.ItemType.Flower, Item.ItemType.None);
            ItemList.Add(Item.ItemType.Flower2, Item.ItemType.None);
            ItemList.Add(Item.ItemType.Bush, Item.ItemType.None);
            ItemList.Add(Item.ItemType.Bush2, Item.ItemType.None);
            ItemList.Add(Item.ItemType.RockSmall, Item.ItemType.None);
            ActionObjects.Add(ItemList);
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Item, typeof(Items.ItemActions), "PickupTile", ActionObjects));

            return ReturnSprite;
        }
    }
}
