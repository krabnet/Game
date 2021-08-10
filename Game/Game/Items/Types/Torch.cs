using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Items;

namespace Game.Items.Types
{
    public class Torch
    {
        public Game.Objects.Sprite2d Get(Items.Item.ItemType IT, Vector2 Position)
        {
            Objects.Sprite2d ReturnSprite = new Objects.Sprite2d();
            List<Object> ActionObjects = new List<object>();
            List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>> DropReplace = new List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>>();
            Dictionary<Item.ItemType, Item.ItemType> ItemList = new Dictionary<Item.ItemType, Item.ItemType>();

            ReturnSprite = Items.Item.GetItemSprite("animatedtorch", IT, Position);
            ReturnSprite.Item.Description = "Flame";
            ReturnSprite.Item.BackGroundType = Item.ItemType.Floor;
            ReturnSprite.Item.Cost = 5;
            ReturnSprite.Item.Stack = false;
            ReturnSprite.AnimSprite = new Objects.AnimSprite(9, 1);
            ReturnSprite.AnimSprite.action = true;
            ReturnSprite.AnimSprite.StopOnEof = false;
            ReturnSprite.clipping = false;
            ReturnSprite.speed = 25;
            ReturnSprite.LightSourceDistance = Util.Global.TorchLightDistance;
            ActionObjects.Add(ReturnSprite);
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));

            return ReturnSprite;
        }
    }
}
