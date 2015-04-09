using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Game.Objects
{
    public class AnimSprite : Base
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int currentFrame;
        public int totalFrames;
        public bool action { get; set; }
        private bool eof { get; set; }
        private bool playSound { get; set; }
        public SoundEffect actionOnSound { get; set; }
        public SoundEffect actionOffSound { get; set; }

        public AnimSprite(Texture2D _texture, string _name, bool _active, int _rows, int _columns, int _x, int _y, int _speed, ControlType _controlType)
        {
            name = _name;
            active = _active;
            Texture = _texture;
            x = _x;
            y = _y;
            sizex = _texture.Width / _rows;
            sizey = _texture.Height / _columns;
            Rows = _rows;
            Columns = _columns;
            currentFrame = 0;
            totalFrames = (Rows * Columns);
            allowScroll = false;
            speed = _speed;
            controlType = _controlType;
            action = false;
            eof = false;
            playSound = true;
            ID = Guid.NewGuid();
            actionType = ActionType.Mouse;
        }

        public AnimSprite()
        {
            ID = Guid.NewGuid();
        }

        public void Update()
        {
            if (action)
            {
                if (controlType == Base.ControlType.None)
                {
                    if (eof)
                    {
                        if (playSound)
                        {
                            if (actionOffSound != null)
                            { actionOffSound.Play(); }
                            playSound = false;
                        }
                        currentFrame--;
                        if (currentFrame == 0)
                        { action = false; eof = false; playSound = true; }
                    }
                    else
                    {
                        if (playSound)
                        {
                            if (actionOnSound != null)
                            { actionOnSound.Play(); }
                            playSound = false;
                        }
                        currentFrame++;
                        if (currentFrame == totalFrames)
                        { action = false; eof = true; playSound = true; }
                    }
                }
                else
                {
                    currentFrame++;
                    if (currentFrame == totalFrames)
                        currentFrame = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = Texture.Width / Rows;
            int height = Texture.Height / Columns;
            int row = (int)((float)currentFrame / (float)Rows);
            int column = currentFrame % Rows;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)x, (int)y, sizex, sizey);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public void AddAnimSpite(Objects.AnimSprite _animSprite)
        {
            Util.Global.SpritesAnim.Add(_animSprite);
        }

        public void FlipClip(AnimSprite _animSprite)
        {
            if (_animSprite.action == false)
            { _animSprite.clipping = !_animSprite.clipping; }
        }
    }
}
