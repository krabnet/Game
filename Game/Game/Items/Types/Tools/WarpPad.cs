using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Items;

namespace Game.Items.Types
{
    public class WarpPad
    {
        public Game.Objects.Sprite2d Get(Items.Item.ItemType IT, Vector2 Position)
        {
            Objects.Sprite2d ReturnSprite = new Objects.Sprite2d();
            List<Object> ActionObjects = new List<object>();
            List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>> DropReplace = new List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>>();
            Dictionary<Item.ItemType, Item.ItemType> ItemList = new Dictionary<Item.ItemType, Item.ItemType>();

            ReturnSprite = Item.GetItemSprite("pad", IT, Position);
            ReturnSprite.Item.Description = "Travel";
            ReturnSprite.Size = new Vector2(32, 32);
            ReturnSprite.Item.BackGroundType = Items.Item.ItemType.Grass;
            ReturnSprite.orderNum = 5;
            ActionObjects.Add(ReturnSprite);
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "ActivateWarp", ActionObjects));

            return ReturnSprite;
        }
    }
}
