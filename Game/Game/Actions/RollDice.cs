using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game.Actions
{
    public class RollDice : Objects.AnimSprite
    {
        private bool rolling { get; set; }
        private int startSeconds { get; set; }
        private int curSeconds { get; set; }
        private int prevSeconds { get; set; }

        public RollDice(Texture2D _texture, string _name, bool _active, int _rows, int _columns)
        {
            name = _name;
            active = _active;
            Texture = _texture;
            Rows = _rows;
            Columns = _columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
        }
        public RollDice() { }

        public void Roll(int StartSeconds)
        {
            if (!rolling)
            {
                rolling = true;
                startSeconds = StartSeconds;
                curSeconds = startSeconds;
            }
        }

        public void update(GameTime gameTime)
        {
            if (rolling == true)
            {
                Random rnd = new Random();
                if (curSeconds == startSeconds)
                {
                    active = true;
                }
                if (prevSeconds != curSeconds && startSeconds+5 >= curSeconds)
                {
                    currentFrame = rnd.Next(1, 6);
                }
                if (startSeconds + 15 <= curSeconds)
                {
                    rolling = false;
                    active = false;
                }
                prevSeconds = curSeconds;
                curSeconds = gameTime.TotalGameTime.Seconds;
            }

        }
    }
}
