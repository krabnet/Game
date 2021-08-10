using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Maps
{
    class DungeonEntrance
    {
        public static Items.Item.ItemType[,,] Get()
        {
            Items.Item.ItemType[,,] Room = new Items.Item.ItemType[,,] {

            { { Items.Item.ItemType.Wall3, Items.Item.ItemType.Wall3, Items.Item.ItemType.Wall3, Items.Item.ItemType.Wall3, Items.Item.ItemType.Wall3   } },
            { { Items.Item.ItemType.Wall3, Items.Item.ItemType.Floor2, Items.Item.ItemType.Floor2, Items.Item.ItemType.Candle, Items.Item.ItemType.Floor2} },
            { { Items.Item.ItemType.Wall3, Items.Item.ItemType.Floor2, Items.Item.ItemType.Floor2, Items.Item.ItemType.Floor2, Items.Item.ItemType.Floor2} },
            { { Items.Item.ItemType.Wall3, Items.Item.ItemType.Floor2, Items.Item.ItemType.Floor2, Items.Item.ItemType.Floor2, Items.Item.ItemType.Floor2} },
            { { Items.Item.ItemType.Wall3, Items.Item.ItemType.Floor2, Items.Item.ItemType.Floor2, Items.Item.ItemType.Floor2, Items.Item.ItemType.Floor2} },
            { { Items.Item.ItemType.Wall3, Items.Item.ItemType.Floor2, Items.Item.ItemType.Floor2, Items.Item.ItemType.Floor2, Items.Item.ItemType.Floor2} },

            };
            return Room;
        }
    }
}
