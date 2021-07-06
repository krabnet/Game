using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Maps
{
    public static class Field
    {
        public static Items.Item.ItemType[, ,] Get()
        {
            int Width = Util.Global.GetRandomInt(6, 9);
            int Height = Util.Global.GetRandomInt(6, 9);

            List<Items.Item.ItemType> RndType = new List<Items.Item.ItemType>();
            RndType.Add(Items.Item.ItemType.Grain);
            RndType.AddRange(Enum.GetValues(typeof(Items.Item.ItemType)).Cast<Items.Item.ItemType>().ToList().Where(x => x.ToString().Contains("Garden")).ToList());

            Items.Item.ItemType[, ,] Room = new Items.Item.ItemType[1, Width + 1, Height + 1];

            for (int i = 0; i <= Width; i++)
            {
                for (int y = 0; y <= Height; y++)
                {
                    if ((i == 0 || i == Width || y == 0 || y == Height) && Util.Global.GetRandomInt(0, 10) < 8)
                        Room[0, i, y] = Items.Item.ItemType.Fence;
                    else if ((i < 1 || i > Width - 1 || y < 1 || y > Height - 1) && Util.Global.GetRandomInt(0, 10) < 3)
                        Room[0, i, y] = Items.Item.ItemType.Gravel;
                    else if ((i < 2 || i > Width - 2 || y < 2 || y > Height - 2) && Util.Global.GetRandomInt(0, 10) < 3)
                        Room[0, i, y] = Items.Item.ItemType.Grass;
                    else if (Util.Global.GetRandomInt(0, 10) < 1)
                        Room[0, i, y] = Items.Item.ItemType.RockSmall;
                    else if (Util.Global.GetRandomInt(0, 10) < 2)
                        Room[0, i, y] = RndType.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                    else if (Util.Global.GetRandomInt(0, 10) < 1)
                        Room[0, i, y] = Items.Item.ItemType.Water;
                    else
                        Room[0, i, y] = Items.Item.ItemType.Grain;
                }
            }

            return Room;
        }
    }
}
