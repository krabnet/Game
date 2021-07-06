using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Actions;

namespace Game.Maps
{
    public class Start
    {
        public TileType[, ,] Get()
        {
            TileType[, ,] Room = new TileType[,,] { 
{ { TileType.wall, TileType.wall, TileType.wall, TileType.wall, TileType.wall, TileType.wall, TileType.wall, TileType.wall, TileType.wall, TileType.wall, TileType.wall, TileType.wall, TileType.wall, TileType.wall, TileType.wall } },
{ { TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.wall } },
{ { TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.wall } },
{ { TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.wall } },
{ { TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.wall } },
{ { TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.wall } },
{ { TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.wall } },
{ { TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.wall } },
{ { TileType.wall, TileType.wall, TileType.door, TileType.wall, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.wall, TileType.door, TileType.wall, TileType.wall, TileType.wall, TileType.wall } },
{ { TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.wall } },
{ { TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.wall } },
{ { TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.wall } },
{ { TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.floor, TileType.wall } },
{ { TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.wall, TileType.wall } },
{ { TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.water, TileType.water } },
{ { TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.water, TileType.grass } },
{ { TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.water, TileType.grass } },
{ { TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.water, TileType.grass } },
{ { TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.water, TileType.grass } },
{ { TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.water, TileType.grass } },
{ { TileType.wall, TileType.wall, TileType.wall, TileType.wall, TileType.wall, TileType.floor, TileType.floor, TileType.floor, TileType.wall, TileType.wall, TileType.wall, TileType.wall, TileType.wall, TileType.water, TileType.grass } },
{ { TileType.wall, TileType.wall, TileType.wall, TileType.wall, TileType.wall, TileType.torch, TileType.floor, TileType.torch, TileType.wall, TileType.wall, TileType.wall, TileType.wall, TileType.wall, TileType.water, TileType.grass } },
{ { TileType.water, TileType.water, TileType.water, TileType.water, TileType.wall, TileType.wall, TileType.door, TileType.wall, TileType.wall, TileType.water, TileType.water, TileType.water, TileType.water, TileType.water, TileType.grass } },
{ { TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass } },
{ { TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass } },
            };

            return Room;
        }
    }
}
