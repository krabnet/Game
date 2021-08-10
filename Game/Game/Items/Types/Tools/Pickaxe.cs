using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Items;

namespace Game.Items.Types
{
    public class Pickaxe
    {
        public Game.Objects.Sprite2d Get(Items.Item.ItemType IT, Vector2 Position)
        {
            Objects.Sprite2d ReturnSprite = new Objects.Sprite2d();
            List<Object> ActionObjects = new List<object>();
            List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>> DropReplace = new List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>>();
            Dictionary<Item.ItemType, Item.ItemType> ItemList = new Dictionary<Item.ItemType, Item.ItemType>();

            ReturnSprite = Items.Item.GetItemSprite("pick", IT, Position);
            ReturnSprite.Item.Description = "Metal Collector";
            ReturnSprite.Item.Stack = false;
            ReturnSprite.collisionSound = "Sounds/shovel";
            DropReplace.Add(new Tuple<Item.ItemType, Item.ItemType, Item.ItemType>(Item.ItemType.IronOre, Item.ItemType.IronBar, Item.ItemType.Grass));
            DropReplace.Add(new Tuple<Item.ItemType, Item.ItemType, Item.ItemType>(Item.ItemType.SilverOre, Item.ItemType.SilverBar, Item.ItemType.Grass));
            DropReplace.Add(new Tuple<Item.ItemType, Item.ItemType, Item.ItemType>(Item.ItemType.GoldOre, Item.ItemType.GoldBar, Item.ItemType.Grass));
            ActionObjects.Add(DropReplace);
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Item, typeof(Items.ItemActions), "ReplaceAndDropItem", ActionObjects));
            ActionObjects = new List<object>();
            ActionObjects.Add(ReturnSprite);
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));

            return ReturnSprite;
        }
    }
}
