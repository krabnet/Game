using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

namespace Game.Objects
{
    [Serializable]
    public class Sprite2d : Base
    {


        public Sprite2d(string _model, string _name, bool _active, Vector2 _Position, Vector2 _Size, int _speed, ControlType _controlType)
        {
            modelname = _model;
            name = _name;
            active = _active;
            Position = _Position;
            previousPosition = _Position;
            Size = _Size;
            SizeScale = 1;
            speed = _speed;
            allowScroll = false;
            Viewtype = ViewType.Default;
            controlType = _controlType;
            ID = Guid.NewGuid();
            Item = new Objects.Item();
            Item.Type = Items.Item.ItemType.None;
            Item.State = Items.Item.ItemState.Null;
            effectType = EffectType.None;
            color = Color.White;
            Orientation = 0F;
            if (actionCall == null)
            { actionCall = new List<Actions.ActionCall>(); }
        }

        public Sprite2d()
        {
            ID = Guid.NewGuid();
            if (actionCall == null)
            { actionCall = new List<Actions.ActionCall>(); }
        }
    }
}
