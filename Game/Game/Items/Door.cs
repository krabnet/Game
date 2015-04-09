using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Game.Items
{
    public class Door
    {
        public Objects.AnimSprite GetDoor(ContentManager Content, int _x, int _y, int _sizex, int _sizey)
        {
            Objects.AnimSprite returnDoor = new Objects.AnimSprite(Content.Load<Texture2D>("Slides/NewDoor2"), "Door", true, 1, 4, 0, 0, 100, Objects.Base.ControlType.None);

            returnDoor.orderNum = 100;
            returnDoor.actionOnSound = Content.Load<SoundEffect>("Sounds/dooropen");
            returnDoor.actionOffSound = Content.Load<SoundEffect>("Sounds/doorclose");
            returnDoor.clipping = true;
            returnDoor.x=_x;
            returnDoor.y=_y;
            returnDoor.sizex = _sizex;
            returnDoor.sizey = _sizey;

            List<Object> Objs2 = new List<object>();
            Objs2.Add(returnDoor);
            returnDoor.actionCall = new Actions.ActionCall(typeof(Objects.AnimSprite), "FlipClip", Objs2);

            return returnDoor;
        }
    }
}
