using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Items;

namespace Game.Items.Types
{
    public class Stove
    {
        public Game.Objects.Sprite2d Get(Items.Item.ItemType IT, Vector2 Position)
        {
            Objects.Sprite2d ReturnSprite = new Objects.Sprite2d();
            List<Object> ActionObjects = new List<object>();
            List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>> DropReplace = new List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>>();
            Dictionary<Item.ItemType, Item.ItemType> ItemList = new Dictionary<Item.ItemType, Item.ItemType>();

            ReturnSprite = Items.Item.GetItemSprite("stove", IT, Position);
            ReturnSprite.Item.Description = "Cooking";
            ReturnSprite.Item.BackGroundType = Item.ItemType.Floor;
            ReturnSprite.Item.Stack = false;
            ReturnSprite.Item.Cost = 25;
            ReturnSprite.orderNum = 75;
            ReturnSprite.Size = new Vector2(75, 75);
            ReturnSprite.Item.OriSize = new Vector2(75, 75);
            ActionObjects = new List<object>();
            ActionObjects.Add(Actions.Crafting.Type.Stove);
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Menu.Craft), "DisplayCraft", ActionObjects));
            ActionObjects = new List<object>();
            ActionObjects.Add(ReturnSprite);
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));

            return ReturnSprite;
        }
    }
}
