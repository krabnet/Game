using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game.Objects
{
    [Serializable]
    public class Item
    {
        public Guid ID { get; set; }
        public Items.Item.ItemType Type { get; set; }
        public Items.Item.ItemClass Class { get; set; }
        public string Description { get; set; }
        public Vector2 OriSize { get; set; }
        public int ItemSlot { get; set; }
        public int Number { get; set; }
        public Items.Item.ItemState State { get; set; }
        public Items.Item.ItemType? BackGroundType { get; set; }
        public int Cost { get; set; }
        public bool Stack { get; set; }

        public Item()
        {
            this.ID = Guid.NewGuid();
            this.Stack = true;
        }
    }

    
}
