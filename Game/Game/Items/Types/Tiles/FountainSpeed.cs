using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Items;

namespace Game.Items.Types
{
    public class FountainSpeed
    {
        public Game.Objects.Sprite2d Get(Items.Item.ItemType IT, Vector2 Position)
        {
            Objects.Sprite2d ReturnSprite = new Objects.Sprite2d();
            List<Object> ActionObjects = new List<object>();
            List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>> DropReplace = new List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>>();
            Dictionary<Item.ItemType, Item.ItemType> ItemList = new Dictionary<Item.ItemType, Item.ItemType>();

            ReturnSprite = Item.GetItemSprite("waterfountain", IT, Position);
            ReturnSprite.Item.BackGroundType = Item.ItemType.Grass;
            ReturnSprite.speed = 5;
            ReturnSprite.color = Color.Green;
            ReturnSprite.Size = new Vector2(64, 64);
            ReturnSprite.AnimSprite = new Objects.AnimSprite(5, 6);
            ReturnSprite.AnimSprite.action = false;
            ReturnSprite.AnimSprite.StopOnEof = false;
            ActionObjects.Add(ReturnSprite);
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Update, typeof(Items.ItemActions), "ProximityAnimAction", ActionObjects));
            ActionObjects = new List<object>();
            ActionObjects.Add(Util.Global.Hero);
            ActionObjects.Add(Actions.Buff.BuffType.Speed);
            ActionObjects.Add(5);
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Actions.Buff), "AddBuff", ActionObjects));

            return ReturnSprite;
        }
    }
}
