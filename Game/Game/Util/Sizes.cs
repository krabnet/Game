using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game.Util
{
    public enum SizeType { None, Grow, Shrink, GrowShrink, Random, RandomEven }

    public static class Sizes
    {
        public static List<Vector2> GetSizeList(SizeType ST, Vector2 startSize, int Steps, int modifier, int MinSizeMultiplyer, int MaxSizeMultiplyer)
        {
            List<Vector2> ret = new List<Vector2>();
            Vector2 MinSize = new Vector2(startSize.X - (modifier * MinSizeMultiplyer), startSize.Y - (modifier * MinSizeMultiplyer));
            Vector2 MaxSize = new Vector2(startSize.X + (modifier * MinSizeMultiplyer), startSize.Y + (modifier * MinSizeMultiplyer));
 

            Vector2 size = startSize;
            for(int i=0;i<=Steps+1;i++)
            {
                if (ST == SizeType.GrowShrink)
                {
                    if (i < Steps / 2)
                    {
                        size = new Vector2(size.X + modifier, size.Y + modifier);
                    }
                    else
                    {
                        size = new Vector2(size.X - modifier, size.Y - modifier);
                    }
                }
                if (ST == SizeType.Grow)
                {
                    size = new Vector2(size.X + modifier, size.Y + modifier);
                }
                if (ST == SizeType.Random)
                {
                    int x = Util.Global.GetRandomInt(1, 5);
                    switch (x)
                    {
                        case 1:
                            size = new Vector2(size.X + modifier, size.Y + modifier);
                            break;
                        case 2:
                            size = new Vector2(size.X - modifier, size.Y - modifier);
                            break;
                        case 3:
                            size = new Vector2(size.X - modifier, size.Y + modifier);
                            break;
                        case 4:
                            size = new Vector2(size.X + modifier, size.Y - modifier);
                            break;
                    }
                    
                }
                if (ST == SizeType.RandomEven)
                {
                    int x = Util.Global.GetRandomInt(1, 3);
                    switch (x)
                    {
                        case 1:
                            size = new Vector2(size.X + modifier, size.Y + modifier);
                            break;
                        case 2:
                            size = new Vector2(size.X - modifier, size.Y - modifier);
                            break;
                    }

                }

                ret.Add(Vector2.Clamp(size, MinSize, MaxSize));
            }
            return ret;
        }
    }
}
