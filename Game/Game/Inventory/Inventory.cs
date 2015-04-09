using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Inventory
{
    public class Inventory
    {
        public Guid ID { get; set; }
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public object ItemObj { get; set; }
        public int ItemSlot { get; set; }
    }
}
