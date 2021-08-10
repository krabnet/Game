using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Items;

namespace Game.Items.Types
{
    public class PotionDexterity
    {
        public Game.Objects.Sprite2d Get(Items.Item.ItemType IT, Vector2 Position)
        {
            Objects.Sprite2d ReturnSprite = new Objects.Sprite2d();
            List<Object> ActionObjects = new List<object>();
            List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>> DropReplace = new List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>>();
            Dictionary<Item.ItemType, Item.ItemType> ItemList = new Dictionary<Item.ItemType, Item.ItemType>();

            ReturnSprite = Items.Item.GetItemSprite("potion", IT, Position);
            ReturnSprite.Item.Description = "Accurate";
            ReturnSprite.Item.Cost = 10;
            ReturnSprite.color = Color.Orange * .8f;
            ActionObjects.Add(IT);
            ActionObjects.Add(Util.Global.Hero);
            ActionObjects.Add(Actions.Buff.BuffType.Dexterity);
            ActionObjects.Add(60);
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Item, typeof(Items.ItemActions), "ConsumeItemAndBuff", ActionObjects));
            ActionObjects = new List<object>();
            ActionObjects.Add(ReturnSprite);
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));

            return ReturnSprite;
        }
    }
}
