using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Maps
{
    class CaveEntrance
    {
        public static Items.Item.ItemType[, ,] Get()
        {
            int Width = Util.Global.GetRandomInt(20, 30);
            int Height = Width;

            Items.Item.ItemType[, ,] Room = new Items.Item.ItemType[1, Width + 1, Height + 1];

            for (int i = 0; i <= Width; i++)
            {
                for (int h = 0; h <= Height; h++)
                {
                    if ((i == 0 || h == 0 || i == Width || h == Height) && Util.Global.GetRandomInt(0, 10) < 2)
                        Room[0, i, h] = Items.Item.ItemType.Wall;
                    else
                        Room[0, i, h] = Items.Item.ItemType.Wall;
                }
            }

            int r = ((Width / 2 + Height / 2) / 2) - 1; // radius
            int ox = Width / 2, oy = Height / 2; // origin

            for (int x = -r; x < r; x++)
            {
                int height = (int)Math.Sqrt(r * r - x * x);

                for (int y = -height; y < height; y++)
                {
                    int m = (y + oy) < 1 ? 1 : y + oy;
                    int n = (x + ox) < 1 ? 1 : x + ox;

                    if (n > Height)
                        n = Height;
                    if (m > Width)
                        m = Width;

                    Room[0, m, n] = Items.Item.ItemType.Gravel;
                }
            }


            return Room;
        }
    }
}
