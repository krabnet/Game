using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game.Util
{
    public enum ColorType { None, Random, GreyScale, BlueScale, WhiteScale, YellowScale }

    public static class Colors
    {
        public static Color GetColor(ColorType CT)
        {
            switch (CT)
            {
                case ColorType.Random:
                    return GetRandom();
                case ColorType.GreyScale:
                    return GetRandomGreyScale();
                case ColorType.BlueScale:
                    return GetRandomBlueScale();
                case ColorType.WhiteScale:
                    return GetRandomWhiteScale();
                case ColorType.YellowScale:
                    return GetRandomYellowScale();
            }
            return Color.Transparent;
        }

        public static Color GetRandom()
        {
            return new Color(
                        (float)Util.Global.GetRandomDouble(),
                        (float)Util.Global.GetRandomDouble(),
                        (float)Util.Global.GetRandomDouble());
        }

        public static Color GetRandomWhiteScale()
        {
            switch (Util.Global.GetRandomInt(1, 6))
            {
                case 1:
                    return Color.White;
                case 2:
                    return Color.Silver;
                case 3:
                    return Color.Gainsboro;
                case 4:
                    return Color.WhiteSmoke;
                case 5:
                    return Color.FloralWhite;
                case 6:
                    return Color.AntiqueWhite;
              
            }
            return Color.White;
        }

        public static Color GetRandomGreyScale()
        {
            switch (Util.Global.GetRandomInt(1, 8))
            {
                case 1:
                    return Color.DarkGray;
                case 2:
                    return Color.Gray;
                case 3:
                    return Color.DimGray;
                case 4:
                    return Color.Silver;
                case 5:
                    return Color.LightGray;
                case 6:
                    return Color.Gainsboro;
                case 7:
                    return Color.WhiteSmoke;
                case 8:
                    return Color.White;
            }
            return Color.Gray;
        }

        public static Color GetRandomBlueScale()
        {
            switch (Util.Global.GetRandomInt(1, 8))
            {
                case 1:
                    return Color.SteelBlue;
                case 2:
                    return Color.LightSkyBlue;
                case 3:
                    return Color.DeepSkyBlue;
                case 4:
                    return Color.DodgerBlue;
                case 5:
                    return Color.Cyan;
                case 6:
                    return Color.Aqua;
                case 7:
                    return Color.Blue;
                case 8:
                    return Color.White;
            }
            return Color.Gray;
        }

        public static Color GetRandomYellowScale()
        {
            switch (Util.Global.GetRandomInt(1, 7))
            {
                case 1:
                    return Color.Yellow;
                case 2:
                    return Color.YellowGreen;
                case 3:
                    return Color.Gold;
                case 4:
                    return Color.WhiteSmoke;
                case 5:
                    return Color.Tan;
                case 6:
                    return Color.Khaki;
            }
            return Color.Yellow;
        }


        public static Color LerpColor(Color a, Color b, float percentage)
        {
            return new Color(
                (byte)MathHelper.Lerp(a.R, b.R, percentage),
                (byte)MathHelper.Lerp(a.G, b.G, percentage),
                (byte)MathHelper.Lerp(a.B, b.B, percentage),
                (byte)MathHelper.Lerp(a.A, b.A, percentage));
        }
    }
}
