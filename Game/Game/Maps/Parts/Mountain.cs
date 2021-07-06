using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Maps
{
    class Mountain
    {
        public static Items.Item.ItemType[, ,] Get()
        {
            int Width = Util.Global.GetRandomInt(5, 20);
            int Height = Util.Global.GetRandomInt(5, 20);

            Items.Item.ItemType[, ,] Room = new Items.Item.ItemType[1, Width + 1, Height + 1];

            for (int i = 0; i <= Width; i++)
            {
                for (int y = 0; y <= Height; y++)
                {
                    if ((i < 1 || i > Width - 1 || y < 1 || y > Height - 1) && Util.Global.GetRandomInt(0, 10) < 3)
                        Room[0, i, y] = Items.Item.ItemType.Grass;
                    else if ((i < 2 || i > Width - 2 || y < 2 || y > Height - 2) && Util.Global.GetRandomInt(0, 10) < 3)
                        Room[0, i, y] = Items.Item.ItemType.Gravel;
                    else if (i < 2 || i > Width - 2 || y < 2 || y > Height - 2)
                        Room[0, i, y] = Items.Item.ItemType.Sand;
                    else if ((i == 2 || i == Width - 2 || y < 2 || y == Height - 2) && Util.Global.GetRandomInt(0, 10) < 4)
                        Room[0, i, y] = Items.Item.ItemType.Sand;
                    else
                        Room[0, i, y] = Items.Item.ItemType.Rock;
                }
            }

            return Room;
        }
    }
}
