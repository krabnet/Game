using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game.Actors
{
    public class Hero : Base
    {
        public Objects.AnimSprite HeroSprite;
        public Texture2D HeroLeft { get; set; }
        public Texture2D HeroRight { get; set; }
        public Texture2D HeroUp { get; set; }
        public Texture2D HeroDown { get; set; }

        public void init(ContentManager Content)
        {
            Health = 10;
            Level = 2;
            CalculateStats();
            HeroDown = Content.Load<Texture2D>("Slides/HeroDown");
            HeroUp = Content.Load<Texture2D>("Slides/HeroUp");
            HeroLeft = Content.Load<Texture2D>("Slides/HeroLeft");
            HeroRight = Content.Load<Texture2D>("Slides/HeroRight");

            Objects.AnimSprite H = new Objects.AnimSprite(Util.Global.Hero.HeroDown, "Hero", true, 7, 1, Util.Global.screenSizeWidth / 2, Util.Global.screenSizeHeight / 2, 5, Objects.Base.ControlType.Keyboard);
            H.clipping = true;
            H.action = true;
            H.orderNum = 990;
            H.sizex = (int)(H.sizex / 1.5);
            H.sizey = (int)(H.sizey / 1.5);
            HeroSprite = H;
            Util.Global.SpritesAnim.Add(H);
        }

    }
}
