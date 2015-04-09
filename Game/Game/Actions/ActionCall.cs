using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Actions
{
    public class ActionCall
    {
        public Type actionType { get; set; }
        public string actionMethodName { get; set; }
        public List<Object> parameters { get; set; }

        public ActionCall(Type _actionType, string _actionMethodName, List<Object> _parameters)
        {
            actionType = _actionType;
            actionMethodName = _actionMethodName;
            parameters = _parameters;
        }
    }
}
