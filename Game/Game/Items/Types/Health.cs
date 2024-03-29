﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Items;

namespace Game.Items.Types
{
    public class Health
    {
        public Game.Objects.Sprite2d Get(Items.Item.ItemType IT, Vector2 Position)
        {
            Objects.Sprite2d ReturnSprite = new Objects.Sprite2d();
            List<Object> ActionObjects = new List<object>();
            List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>> DropReplace = new List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>>();
            Dictionary<Item.ItemType, Item.ItemType> ItemList = new Dictionary<Item.ItemType, Item.ItemType>();

            ReturnSprite = Items.Item.GetItemSprite("heart_full", IT, Position);
            ReturnSprite.Size = new Vector2(10, 10);
            ActionObjects.Add(ReturnSprite);
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "HealthPickup", ActionObjects));

            return ReturnSprite;
        }
    }
}
