using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Maps
{
    class Forrest
    {
        public TileType[, ,] Get()
        {
            TileType[, ,] MapPart = new TileType[,,] { 
            { { TileType.grass, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.grass, TileType.grass, TileType.grass, TileType.tree, TileType.tree, TileType.tree, TileType.grass,  TileType.grass, TileType.tree, TileType.grass, TileType.grass, TileType.tree, TileType.grass } }, 
            { { TileType.grass, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.grass, TileType.tree, TileType.grass, TileType.tree, TileType.tree, TileType.grass,  TileType.grass, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.grass } }, 
            { { TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.grass, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree,  TileType.grass, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree } }, 
            { { TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree,  TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree } }, 
            { { TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree,  TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.grass } }, 
            { { TileType.grass, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree,  TileType.tree, TileType.tree, TileType.tree, TileType.grass, TileType.tree, TileType.tree } }, 
            { { TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.grass, TileType.tree, TileType.tree, TileType.tree,  TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.grass } }, 
            { { TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.grass, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.grass,  TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree } }, 
            { { TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.grass, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.grass,  TileType.tree, TileType.tree, TileType.grass, TileType.grass, TileType.tree, TileType.grass } }, 
            { { TileType.grass, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.grass, TileType.grass, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.grass,  TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree, TileType.tree } }, 
            };

            return MapPart;
        }
    }
}
