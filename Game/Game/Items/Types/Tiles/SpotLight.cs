using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Items;

namespace Game.Items.Types
{
    public class SpotLight
    {
        public Game.Objects.Sprite2d Get(Items.Item.ItemType IT, Vector2 Position)
        {
            Objects.Sprite2d ReturnSprite = new Objects.Sprite2d();
            List<Object> ActionObjects = new List<object>();
            List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>> DropReplace = new List<Tuple<Item.ItemType, Item.ItemType, Item.ItemType>>();
            Dictionary<Item.ItemType, Item.ItemType> ItemList = new Dictionary<Item.ItemType, Item.ItemType>();

            ReturnSprite = Items.Item.GetItemSprite("spotlight", IT, Position);
            ReturnSprite.orderNum = 6;
            ReturnSprite.Item.BackGroundType = Item.ItemType.Wall2;
            ReturnSprite.Item.Cost = 50;
            ReturnSprite.Size = new Vector2(702 / 22, 720 / 22);
            ReturnSprite.Item.Stack = false;
            ReturnSprite.clipping = false;
            ReturnSprite.speed = 0;
            ReturnSprite.LightSourceDistance = Util.Global.TorchLightDistance;

            return ReturnSprite;
        }
    }
}
