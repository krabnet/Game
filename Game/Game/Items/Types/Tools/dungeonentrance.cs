using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Items;

namespace Game.Items.Types
{
    public class dungeonentrance
    {
        public Game.Objects.Sprite2d Get(Items.Item.ItemType IT, Vector2 Position)
        {
            Objects.Sprite2d ReturnSprite = new Objects.Sprite2d();
            List<Object> ActionObjects = new List<object>();
            List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>> DropReplace = new List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>>();
            Dictionary<Item.ItemType, Item.ItemType> ItemList = new Dictionary<Item.ItemType, Item.ItemType>();

            ReturnSprite = Items.Item.GetItemSprite("tower", IT, Position);
            ReturnSprite.Item.Description = "deep dark";
            ReturnSprite.Item.BackGroundType = Item.ItemType.Grass;
            ReturnSprite.Size = new Vector2(239, 486) / 4;
            ReturnSprite.Item.OriSize = ReturnSprite.Size;
            ReturnSprite.SpriteType = Objects.Base.Type.Tile;
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Maps.Dungeon), "WarpDungeon", null));

            return ReturnSprite;
        }
    }
}
