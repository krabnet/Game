using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Game.Objects
{
    public class Spite2d : Base
    {
        public Texture2D model { get; set; }
        public SoundEffect collisionSound { get; set; }

        public Spite2d(Texture2D _model, string _name, bool _active, int _x, int _y, int _sizex, int _sizey, int _speed, ControlType _controlType)
        {
            model = _model;
            name = _name;
            active = _active;
            x = _x;
            y = _y;
            sizex = _sizex;
            sizey = _sizey;
            speed = _speed;
            allowScroll = false;
            controlType = _controlType;
            ID = Guid.NewGuid();
            actionType = ActionType.Collision;
        }

        public Spite2d()
        {
            ID = Guid.NewGuid();
        }

        public void AddSpite2d(Objects.Spite2d _sprite2d)
        {
            Util.Global.Sprites.Add(_sprite2d);
        }

        public void Pickup(Spite2d _Spite2d)
        {
            _Spite2d.active = false;
        }

        public static void FlipMouseControl(Objects.Spite2d Sprite)
        {
            if (Sprite.controlType == Objects.Base.ControlType.Mouse)
            { Sprite.controlType = Objects.Base.ControlType.None; }
            else
            { Sprite.controlType = Objects.Base.ControlType.Mouse; }
        }
    }
}
