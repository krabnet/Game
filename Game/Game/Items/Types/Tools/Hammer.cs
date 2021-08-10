using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Items;

namespace Game.Items.Types
{
    public class Hammer
    {
        public Game.Objects.Sprite2d Get(Items.Item.ItemType IT, Vector2 Position)
        {
            Objects.Sprite2d ReturnSprite = new Objects.Sprite2d();
            List<Object> ActionObjects = new List<object>();
            List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>> DropReplace = new List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>>();
            Dictionary<Item.ItemType, Item.ItemType> ItemList = new Dictionary<Item.ItemType, Item.ItemType>();

            ReturnSprite = Item.GetItemSprite("hammer", IT, Position);
            ReturnSprite.Item.Description = "Bang Bang";
            ReturnSprite.Item.Stack = false;
            ReturnSprite.collisionSound = "Sounds/shovel";
            ActionObjects.Add(ReturnSprite);
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
            ActionObjects = new List<object>();
            ItemList.Add(Item.ItemType.Wall, Item.ItemType.Grass);
            ItemList.Add(Item.ItemType.Plank, Item.ItemType.Grass);
            ItemList.Add(Item.ItemType.Glass, Item.ItemType.Grass);
            ItemList.Add(Item.ItemType.Fence, Item.ItemType.Grass);
            ActionObjects.Add(ItemList);
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Item, typeof(Items.ItemActions), "PickupTile", ActionObjects));

            return ReturnSprite;
        }
    }
}
