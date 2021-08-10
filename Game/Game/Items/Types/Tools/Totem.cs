using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Items;

namespace Game.Items.Types
{
    public class Totem
    {
        public Game.Objects.Sprite2d Get(Items.Item.ItemType IT, Vector2 Position)
        {
            Objects.Sprite2d ReturnSprite = new Objects.Sprite2d();
            List<Object> ActionObjects = new List<object>();
            List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>> DropReplace = new List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>>();
            Dictionary<Item.ItemType, Item.ItemType> ItemList = new Dictionary<Item.ItemType, Item.ItemType>();

            ReturnSprite = Items.Item.GetItemSprite("totem", IT, Position);
            ReturnSprite.Item.Description = "Knowledge";
            ReturnSprite.Item.BackGroundType = Item.ItemType.Grass;
            ReturnSprite.Size = new Vector2(477, 640) / 6;
            ReturnSprite.LightIgnor = false;
            //ReturnSprite.text = "test test";
            ReturnSprite.color = Util.Colors.GetRandomWhiteScale();
            ReturnSprite.boxColor = Util.Colors.GetRandomBlueScale();
            ActionObjects = new List<object>();
            ActionObjects.Add(ReturnSprite);
            ActionObjects.Add(ItemActions.GetTotemText());
            ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseAny, typeof(Items.ItemActions), "SayTotemText", ActionObjects));

            return ReturnSprite;
        }
    }
}
