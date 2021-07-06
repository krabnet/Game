using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Maps
{
    class Totem
    {
        public static Items.Item.ItemType[, ,] Get()
        {
            int Width = Util.Global.GetRandomInt(9, 13);
            int Height = Util.Global.GetRandomInt(9, 13);

            Items.Item.ItemType[, ,] Room = new Items.Item.ItemType[1, Width, Height];

            for (int i = 0; i < Width; i++)
            {
                for (int h = 0; h < Height; h++)
                {
                    if (( i == 0 || h == 0 || i == Width || h == Height) && Util.Global.GetRandomInt(0,10)<5)
                        Room[0, i, h] = Items.Item.ItemType.Grass;
                    else
                        Room[0, i, h] = Items.Item.ItemType.Flower;
                }
            }

            Items.Item.ItemType CirType = Items.Item.ItemType.Path;

            int xc = Width / 2;
            int yc = Height / 2;
            int x = 0;
            int y = ((Width / 3 + Height / 3) / 2)+1;
            int p = 1 - (Width / 3 + Height / 3) / 2;
            Room[0, xc + x, yc - y] = CirType;
            Room[0, xc + y, yc - x] = CirType;
            Room[0, xc + y, yc + x] = CirType;
            Room[0, xc + x, yc + y] = CirType;
            Room[0, xc - x, yc + y] = CirType;
            Room[0, xc - y, yc + x] = CirType;
            Room[0, xc - y, yc - x] = CirType;
            Room[0, xc - x, yc - y] = CirType;
            for (x = 0; y > x; x++)
            {
                if (p < 0)
                    p += 2 * x + 3;
                else
                {
                    p += 2 * (x - y) + 5;
                    y--;
                }
                Room[0, xc + x, yc - y] = CirType;
                Room[0, xc + y, yc - x] = CirType;
                Room[0, xc + y, yc + x] = CirType;
                Room[0, xc + x, yc + y] = CirType;
                Room[0, xc - x, yc + y] = CirType;
                Room[0, xc - y, yc + x] = CirType;
                Room[0, xc - y, yc - x] = CirType;
                Room[0, xc - x, yc - y] = CirType;
            }

            Room[0, (Width / 2)-2, (Height / 2)-1] = Items.Item.ItemType.Totem;

            return Room;
        }
    }
}
