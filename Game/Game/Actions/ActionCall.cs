using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace Game.Actions
{
    public enum ActionType { Collision, Mouse, MouseRight, MouseAny, MouseCollision, Update, Item };

    [Serializable]
    public class ActionCall
    {
        public ActionType ActionType { get; set; }
        public Type Type { get; set; }
        public string actionMethodName { get; set; }
        public List<Object> parameters { get; set; }

        public ActionCall(ActionType _ActionType, Type _Type, string _actionMethodName, List<Object> _parameters)
        {
            ActionType = _ActionType;
            Type = _Type;
            actionMethodName = _actionMethodName;
            parameters = _parameters;
        }
    }
}
