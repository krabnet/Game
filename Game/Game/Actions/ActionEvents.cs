using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Actions
{
    public class ActionEvents
    {
        public Guid ID { get; set; }
        public List<ActionCall> actionCall { get; set; }
        public int Duration { get; set; }
        public int InitialDuration { get; set; }

        public ActionEvents()
        {
            ID = Guid.NewGuid();
            if (actionCall == null)
            { actionCall = new List<Actions.ActionCall>(); }
        }
    }
}
