using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Maps
{
    class Mountain
    {
        public TileType[, ,] Get()
        {
            TileType[, ,] MapPart = new TileType[,,] { 
            { { TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass} }, 
            { { TileType.grass, TileType.grass, TileType.rock, TileType.rock, TileType.grass, TileType.grass} }, 
            { { TileType.grass, TileType.rock, TileType.rock, TileType.rock, TileType.rock, TileType.grass} }, 
            { { TileType.rock, TileType.rock, TileType.rock, TileType.rock, TileType.rock, TileType.rock} }, 
            { { TileType.rock, TileType.rock, TileType.rock, TileType.rock, TileType.rock, TileType.rock} }, 
            { { TileType.rock, TileType.rock, TileType.rock, TileType.rock, TileType.rock, TileType.rock} }, 
            { { TileType.grass, TileType.rock, TileType.rock, TileType.rock, TileType.rock, TileType.grass} }, 
            { { TileType.grass, TileType.grass, TileType.rock, TileType.rock, TileType.grass, TileType.grass} }, 
            { { TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass} }, 
            };

            return MapPart;
        }
    }
}
