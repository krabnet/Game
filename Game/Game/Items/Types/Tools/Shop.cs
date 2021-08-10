using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Items;

namespace Game.Items.Types
{
    public class Shop
    {
        public Game.Objects.Sprite2d Get(Items.Item.ItemType IT, Vector2 Position)
        {
            Objects.Sprite2d ReturnSprite = new Objects.Sprite2d();
            List<Object> ActionObjects = new List<object>();
            List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>> DropReplace = new List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>>();
            Dictionary<Item.ItemType, Item.ItemType> ItemList = new Dictionary<Item.ItemType, Item.ItemType>();

            ReturnSprite = Items.Item.GetItemSprite("treehouse", IT, Position);
            ReturnSprite.Item.Description = "Shop";
            ReturnSprite.Item.BackGroundType = Item.ItemType.Grass;
            ReturnSprite.Size = new Vector2(509, 640) / 5;
            ReturnSprite.Item.OriSize = ReturnSprite.Size;
            ReturnSprite.SpriteType = Objects.Base.Type.Tile;

            ReturnSprite.Inventory = new List<Objects.Item>();
            ReturnSprite.Inventory.AddRange(Actions.Shop.GetRandomItemList(3, 10));

            ActionObjects = new List<object>();
            ActionObjects.Add(ReturnSprite);
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Actions.Shop), "EnterShop", ActionObjects));

            return ReturnSprite;
        }
    }
}
