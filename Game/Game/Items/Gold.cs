using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Game.Items
{
    public class Gold
    {
        public Objects.Spite2d GetGoldObject(ContentManager Content)
        {
            Objects.Spite2d gold = new Objects.Spite2d(Content.Load<Texture2D>("Images/gold"), "gold", true,1,1, 50, 50, 0, Objects.Base.ControlType.None);
            gold.orderNum = 100;
            List<Object> Objs3 = new List<object>();
            Objs3.Add(gold);
            gold.actionCall = new Actions.ActionCall(typeof(Objects.Spite2d), "Pickup", Objs3);
            gold.collisionSound = Content.Load<SoundEffect>("Sounds/pickup");

            return gold;
        }

    }
}
