using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Items;

namespace Game.Items.Types
{
    public class Tent
    {
        public Game.Objects.Sprite2d Get(Items.Item.ItemType IT, Vector2 Position)
        {
            Objects.Sprite2d ReturnSprite = new Objects.Sprite2d();
            List<Object> ActionObjects = new List<object>();
            List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>> DropReplace = new List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>>();
            Dictionary<Item.ItemType, Item.ItemType> ItemList = new Dictionary<Item.ItemType, Item.ItemType>();

            ReturnSprite = Items.Item.GetItemSprite("tent", IT, Position);
            ReturnSprite.Item.Description = "Zoo";
            ReturnSprite.Item.Stack = false;
            ReturnSprite.Item.BackGroundType = Item.ItemType.Grass;
            ReturnSprite.Item.Cost = 25;
            ReturnSprite.Size = new Vector2(640, 508) / 6;
            ReturnSprite.Item.OriSize = ReturnSprite.Size;
            ReturnSprite.SpriteType = Objects.Base.Type.Tile;
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Actions.Stable), "EnterStable", null));
            ActionObjects = new List<object>();
            ActionObjects.Add(ReturnSprite);
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));

            return ReturnSprite;
        }
    }
}
