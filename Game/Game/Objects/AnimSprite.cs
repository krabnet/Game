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
    [Serializable]
    public class AnimSprite
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int currentFrame;
        public int totalFrames;
        public bool action { get; set; }
        public bool eof { get; set; }
        public bool StopOnEof { get; set; }
        private bool playSound { get; set; }
        public string actionOnSound { get; set; }
        public string actionOffSound { get; set; }
        public Actions.ActionCall ActionCallEOF { get; set; }

        public AnimSprite(int _rows, int _columns)
        {
            Rows = _rows;
            Columns = _columns;
            currentFrame = 0;
            totalFrames = (Rows * Columns);
            action = false;
            eof = false;
            playSound = true;
        }

        public void Update()
        {
            if (action)
            {
                if (eof)
                {
                    if (playSound)
                    {
                        if (actionOffSound != null)
                        { Util.Global.ContentMan.Load<SoundEffect>(actionOffSound).Play(); }
                        playSound = false;
                    }

                    currentFrame--;
                    if (currentFrame == 0)
                    {
                        playSound = true;
                        if (StopOnEof)
                        {
                            action = false;
                            eof = false;
                            currentFrame = 0;
                        }
                    }
                }
                else
                {
                    if (playSound)
                    {
                        if (actionOnSound != null)
                        { Util.Global.ContentMan.Load<SoundEffect>(actionOnSound).Play(); }
                        playSound = false;
                    }

                    currentFrame++;
                    if (currentFrame == totalFrames)
                    {
                        playSound = true;
                        if (StopOnEof)
                        {
                            if (ActionCallEOF != null)
                            {
                                Util.Base.CallMethod(ActionCallEOF);
                            }
                            action = false;
                            eof = true;
                            currentFrame = totalFrames-1;
                        }
                        else
                        {
                            currentFrame = 0;
                        }
                    }
                }
                
            }

                //if (eof)
                //    {
                //        if (playSound)
                //        {
                //            if (actionOffSound != null)
                //            { actionOffSound.Play(); }
                //            playSound = false;
                //        }
                //        currentFrame--;
                //        if (currentFrame <= 0)
                //        { action = false; eof = false; playSound = true; }
                //    }
                //    else
                //    {
                //        if (playSound)
                //        {
                //            if (actionOnSound != null)
                //            { actionOnSound.Play(); }
                //            playSound = false;
                //        }
                //        currentFrame++;
                //        if (currentFrame == totalFrames)
                //        { action = false; eof = true; playSound = true; }
                //    }
                //    currentFrame++;
                //    if (currentFrame >= totalFrames)
                //        currentFrame = 0;
            
        }
    }
}