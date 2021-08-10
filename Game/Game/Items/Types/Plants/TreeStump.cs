using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Items;

namespace Game.Items.Types
{
    public class TreeStump
    {
        public Game.Objects.Sprite2d Get(Items.Item.ItemType IT, Vector2 Position)
        {
            Objects.Sprite2d ReturnSprite = new Objects.Sprite2d();
            List<Object> ActionObjects = new List<object>();
            List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>> DropReplace = new List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>>();
            Dictionary<Item.ItemType, Item.ItemType> ItemList = new Dictionary<Item.ItemType, Item.ItemType>();

            ReturnSprite = Items.Item.GetItemSprite("tree3stump", IT, Position);
            ReturnSprite.Size = new Vector2(592 / 6, 640 / 6);
            ReturnSprite.Position = Vector2.Subtract(ReturnSprite.Position, Vector2.Multiply(ReturnSprite.Size, .5F));
            ReturnSprite.Item.BackGroundType = Item.ItemType.Grass;

            return ReturnSprite;
        }
    }
}
