using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Items;

namespace Game.Items.Types
{
    public class Door
    {
        public Game.Objects.Sprite2d Get(Items.Item.ItemType IT, Vector2 Position)
        {
            Objects.Sprite2d ReturnSprite = new Objects.Sprite2d();
            List<Object> ActionObjects = new List<object>();
            List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>> DropReplace = new List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>>();
            Dictionary<Item.ItemType, Item.ItemType> ItemList = new Dictionary<Item.ItemType, Item.ItemType>();

            ReturnSprite = Items.Item.GetItemSprite("NewDoor2", IT, Position);
            ReturnSprite.Item.BackGroundType = Item.ItemType.Floor;
            ReturnSprite.AnimSprite = new Objects.AnimSprite(1, 4);
            ReturnSprite.AnimSprite.actionOnSound = "Sounds/dooropen";
            ReturnSprite.AnimSprite.actionOffSound = "Sounds/doorclose";
            ReturnSprite.AnimSprite.StopOnEof = true;
            ReturnSprite.speed = 5;
            ReturnSprite.clipping = true;
            ReturnSprite.SpriteType = Objects.Base.Type.Tile;
            ActionObjects.Add(ReturnSprite);
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Items.ItemActions), "FlipClip", ActionObjects));

            return ReturnSprite;
        }
    }
}
