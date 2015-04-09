using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Actions;

namespace Game.Maps
{
    public class Room
    {
        public TileType[, ,] Get()
        {
            TileType[, ,] Room = new TileType[,,] { 
            { { TileType.wall, TileType.wall, TileType.wall, TileType.door, TileType.wall, TileType.wall } }, 
            { { TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.wall } }, 
            { { TileType.door, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.wall } }, 
            { { TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.wall } }, 
            { { TileType.wall, TileType.wall, TileType.wall, TileType.door, TileType.wall, TileType.wall } }, 
            };
            
            return Room;
        }
    }
}
