using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Items;

namespace Game.Items.Types
{
    public class CaveEntrance
    {
        public Game.Objects.Sprite2d Get(Items.Item.ItemType IT, Vector2 Position)
        {
            Objects.Sprite2d ReturnSprite = new Objects.Sprite2d();
            List<Object> ActionObjects = new List<object>();
            List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>> DropReplace = new List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>>();
            Dictionary<Item.ItemType, Item.ItemType> ItemList = new Dictionary<Item.ItemType, Item.ItemType>();

            ReturnSprite = Item.GetItemSprite("CaveEntrance", IT, Position);
            ReturnSprite.Item.Description = "Going Down?";
            ReturnSprite.Size = new Vector2(600, 402) / 6;
            ReturnSprite.Item.BackGroundType = Items.Item.ItemType.Grass;
            ReturnSprite.orderNum = 5;
            ActionObjects = new List<object>();
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Actions.Cave), "EnterCave", ActionObjects));

            return ReturnSprite;
        }
    }
}
