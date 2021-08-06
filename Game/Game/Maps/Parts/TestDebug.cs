﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Maps
{
    class TestDebug
    {
        public static Items.Item.ItemType[, ,] Get()
        {
            Items.Item.ItemType[, ,] Room = new Items.Item.ItemType[,,] { 
            { { Items.Item.ItemType.FountainHealth, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor,Items.Item.ItemType.Floor,  Items.Item.ItemType.Spawner } }, 
            { { Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Torch, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.TestChest, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Torch, Items.Item.ItemType.Anvil,    Items.Item.ItemType.Floor,  Items.Item.ItemType.Floor } }, 
            { { Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor,    Items.Item.ItemType.Floor,  Items.Item.ItemType.Floor } }, 
            { { Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Stove, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor,    Items.Item.ItemType.Floor,  Items.Item.ItemType.Floor } }, 
            { { Items.Item.ItemType.Bed, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor,  Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor,  Items.Item.ItemType.Floor, Items.Item.ItemType.WorkBench, Items.Item.ItemType.Floor, Items.Item.ItemType.Caldron,  Items.Item.ItemType.Floor,  Items.Item.ItemType.GreenHouse } }, 
            { { Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor,    Items.Item.ItemType.Floor,  Items.Item.ItemType.Floor } }, 
            { { Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor,    Items.Item.ItemType.Floor,  Items.Item.ItemType.Totem } }, 
            { { Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Torch, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Floor, Items.Item.ItemType.Torch, Items.Item.ItemType.Floor,    Items.Item.ItemType.Floor,  Items.Item.ItemType.Floor } }, 
            { { Items.Item.ItemType.Grass, Items.Item.ItemType.Totem, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Shop, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Tent, Items.Item.ItemType.Grass,    Items.Item.ItemType.Grass,  Items.Item.ItemType.Grass } }, 
            { { Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass,    Items.Item.ItemType.Grass,  Items.Item.ItemType.Grass } }, 
            { { Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass,    Items.Item.ItemType.Grass,  Items.Item.ItemType.Grass } }, 
            { { Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass,    Items.Item.ItemType.Grass,  Items.Item.ItemType.CaveEntrance } },
            { { Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass,    Items.Item.ItemType.Grass,  Items.Item.ItemType.Grass } },
            { { Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass,    Items.Item.ItemType.Grass,  Items.Item.ItemType.Grass } },
            { { Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass,    Items.Item.ItemType.Grass,  Items.Item.ItemType.Grass } },
            { { Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass,    Items.Item.ItemType.Grass,  Items.Item.ItemType.Grass } },
            { { Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass,    Items.Item.ItemType.Grass,  Items.Item.ItemType.Grass } },
            { { Items.Item.ItemType.Wall, Items.Item.ItemType.Wall, Items.Item.ItemType.Wall, Items.Item.ItemType.Wall, Items.Item.ItemType.Wall, Items.Item.ItemType.Wall, Items.Item.ItemType.Wall, Items.Item.ItemType.Wall, Items.Item.ItemType.Wall, Items.Item.ItemType.Wall, Items.Item.ItemType.Wall,    Items.Item.ItemType.Wall,  Items.Item.ItemType.Wall } }, 
            { { Items.Item.ItemType.Wall, Items.Item.ItemType.Wall, Items.Item.ItemType.Wall, Items.Item.ItemType.Wall, Items.Item.ItemType.Wall, Items.Item.ItemType.Wall, Items.Item.ItemType.Wall, Items.Item.ItemType.Wall, Items.Item.ItemType.Wall, Items.Item.ItemType.Wall, Items.Item.ItemType.Wall,    Items.Item.ItemType.Wall,  Items.Item.ItemType.Wall } }, 
            { { Items.Item.ItemType.Totem, Items.Item.ItemType.Totem, Items.Item.ItemType.Totem, Items.Item.ItemType.Totem, Items.Item.ItemType.Totem, Items.Item.ItemType.Totem, Items.Item.ItemType.Totem, Items.Item.ItemType.Totem, Items.Item.ItemType.Totem, Items.Item.ItemType.Totem, Items.Item.ItemType.Totem,    Items.Item.ItemType.Totem,  Items.Item.ItemType.Totem } }, 
            { { Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass,    Items.Item.ItemType.Grass,  Items.Item.ItemType.Grass } }, 
            { { Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass,    Items.Item.ItemType.Grass,  Items.Item.ItemType.Grass } }, 
            { { Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass,    Items.Item.ItemType.Grass,  Items.Item.ItemType.Grass } }, 
            { { Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass, Items.Item.ItemType.Grass,    Items.Item.ItemType.Grass,  Items.Item.ItemType.Grass } }, 
            { { Items.Item.ItemType.WarpPad, Items.Item.ItemType.Grass, Items.Item.ItemType.WarpPad, Items.Item.ItemType.Grass, Items.Item.ItemType.WarpPad, Items.Item.ItemType.Grass, Items.Item.ItemType.WarpPad, Items.Item.ItemType.Grass, Items.Item.ItemType.WarpPad, Items.Item.ItemType.Grass, Items.Item.ItemType.WarpPad,    Items.Item.ItemType.WarpPad,  Items.Item.ItemType.Grass } }, 

            };

            return Room;
        }
    }
}
