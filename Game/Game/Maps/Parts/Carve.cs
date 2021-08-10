using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Maps
{
    public class Carve
    {
        public static Items.Item.ItemType[,,] Get()
        {
            int Width = Util.Global.GetRandomInt(7, 15);
            int Height = Util.Global.GetRandomInt(7, 15);

            Items.Item.ItemType[,,] Room = new Items.Item.ItemType[1, Width + 1, Height + 1];

            for (int i = 0; i <= Width; i++)
            {
                for (int y = 0; y <= Height; y++)
                {
                    if ((i < 1 || i > Width - 1 || y < 1 || y > Height - 1))
                        Room[0, i, y] = Items.Item.ItemType.Wall3;
                    else
                        Room[0, i, y] = Items.Item.ItemType.Remove;
                }
            }
            return Room;
        }
    }
}
