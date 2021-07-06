using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game.Items
{
    [Serializable]
    public class Warp
    {
        public string WarpName { get; set; }
        public Vector3 MapLocation { get; set; }
        public Vector2 WarpLocation { get; set; }
        public Guid WarpID { get; set; }
    }
}
