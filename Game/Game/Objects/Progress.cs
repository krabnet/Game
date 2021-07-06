using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Objects
{
    [Serializable]
    public class Progress
    {
        public Guid ID { get; set; }
        public int Max { get; set; }
        public int Prog { get; set; }
        public Actions.ActionCall FinalAction { get; set; }
    }
}
 