using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Maps
{
    public class Stream
    {
        public TileType[, ,] Get()
        {
            TileType[, ,] Stream = new TileType[,,] { 
            { { TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass, TileType.grass } }, 
            { { TileType.grass, TileType.gravel_se, TileType.gravel, TileType.gravel, TileType.gravel_sw, TileType.grass } }, 
            { { TileType.grass, TileType.gravel, TileType.gravel, TileType.gravel, TileType.gravel, TileType.grass } }, 
            { { TileType.water, TileType.water, TileType.gravel, TileType.water, TileType.tree, TileType.tree } }, 
            { { TileType.tree, TileType.water, TileType.gravel, TileType.water, TileType.water, TileType.tree } }, 
            { { TileType.tree, TileType.tree, TileType.gravel, TileType.water, TileType.water, TileType.water } }, 
            { { TileType.grain, TileType.grain, TileType.gravel, TileType.rock, TileType.water, TileType.water } }, 
            { { TileType.grass, TileType.grass, TileType.gravel, TileType.grass, TileType.grass, TileType.grass } }, 
            { { TileType.grass, TileType.grass, TileType.gravel, TileType.grass, TileType.grass, TileType.grass } }, 
            { { TileType.grass, TileType.grass, TileType.gravel, TileType.grass, TileType.grass, TileType.grass } }, 
            };

            return Stream;
        }
    }
}
