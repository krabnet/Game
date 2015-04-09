using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Maps
{
    class Courtyard
    {
            public TileType[, ,] Get()
            {
            
            TileType[, ,] MapPart = new TileType[,,] { 
            { { TileType.gravel, TileType.gravel, TileType.gravel, TileType.gravel, TileType.gravel, TileType.gravel } }, 
            { { TileType.gravel, TileType.gravel, TileType.gravel, TileType.gravel, TileType.gravel, TileType.gravel } }, 
            { { TileType.gravel, TileType.gravel, TileType.gravel, TileType.gravel, TileType.gravel, TileType.gravel } }, 
            { { TileType.gravel, TileType.gravel_nw, TileType.gravel_n, TileType.gravel_n, TileType.gravel_ne, TileType.gravel } }, 
            { { TileType.gravel, TileType.gravel_w, TileType.grass, TileType.grass, TileType.gravel_se, TileType.gravel } }, 
            { { TileType.gravel, TileType.gravel_w, TileType.grass, TileType.gravel_e, TileType.gravel, TileType.gravel } }, 
            { { TileType.gravel, TileType.gravel_w, TileType.grass, TileType.gravel_e, TileType.gravel, TileType.gravel } }, 
            { { TileType.gravel, TileType.gravel_w, TileType.grass, TileType.grass_sw2, TileType.gravel_ne, TileType.gravel } }, 
            { { TileType.gravel, TileType.gravel_w, TileType.grass, TileType.grass, TileType.gravel_e, TileType.gravel } }, 
            { { TileType.gravel, TileType.gravel_sw, TileType.gravel_s, TileType.gravel_s, TileType.gravel_se, TileType.gravel } }, 
            { { TileType.wall, TileType.wall, TileType.wall, TileType.wall, TileType.wall, TileType.wall } }, 
            { { TileType.wall, TileType.wall, TileType.wall, TileType.wall, TileType.wall, TileType.wall } }
            };

                return MapPart;
            }
    }
}
