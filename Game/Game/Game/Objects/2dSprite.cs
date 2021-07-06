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
    public class Sprite2d : Base
    {


        public Sprite2d(Texture2D _model, string _name, bool _active, Vector2 _Position, Vector2 _Size, int _speed, ControlType _controlType)
        {
            model = _model;
            name = _name;
            active = _active;
            Position = _Position;
            Size = _Size;
            speed = _speed;
            allowScroll = false;
            controlType = _controlType;
            ID = Guid.NewGuid();
            Item = new Objects.Item();
            Item.Type = Items.Item.ItemType.None;
            Item.State = Objects.Item.ItemState.Null;
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
