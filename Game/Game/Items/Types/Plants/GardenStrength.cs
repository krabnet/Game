using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Items;

namespace Game.Items.Types
{
    public class GardenStrength
    {
        public Game.Objects.Sprite2d Get(Items.Item.ItemType IT, Vector2 Position)
        {
            Objects.Sprite2d ReturnSprite = new Objects.Sprite2d();
            List<Object> ActionObjects = new List<object>();
            List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>> DropReplace = new List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>>();
            Dictionary<Item.ItemType, Item.ItemType> ItemList = new Dictionary<Item.ItemType, Item.ItemType>();

            ReturnSprite = Items.Item.GetItemSprite("GrowPlant", IT, Position);
            ReturnSprite.Item.BackGroundType = Item.ItemType.Grass;
            ReturnSprite.AnimSprite = new Objects.AnimSprite(5, 1);
            ReturnSprite.AnimSprite.StopOnEof = true;
            ReturnSprite.AnimSprite.action = true;
            ReturnSprite.speed = 600;
            if (Util.Global.Weather == Actions.Enviro.Weather.Rain)
            { ReturnSprite.speed = 350; }
            ReturnSprite.clipping = false;
            ReturnSprite.SpriteType = Objects.Base.Type.Tile;
            ActionObjects.Add(ReturnSprite);
            ActionObjects.Add(Item.ItemType.BerryStrength);
            ReturnSprite.AnimSprite.ActionCallEOF = new Actions.ActionCall(Actions.ActionType.Item, typeof(Items.ItemActions), "GardenUpdate", ActionObjects);

            return ReturnSprite;
        }
    }
}
