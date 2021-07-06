using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Actions;

namespace Game.Maps
{
    public class Room
    {
        public static Items.Item.ItemType[, ,] Get()
        {
            int Width = Util.Global.GetRandomInt(5, 12);
            int Height = Util.Global.GetRandomInt(5, 12);

            Items.Item.ItemType[, ,] Room = new Items.Item.ItemType[1, Width + 1, Height + 1];

            for (int i = 0; i <= Width; i++)
            {
                for (int y = 0; y <= Height; y++)
                {
                    if ((i < 1 || i > Width - 1 || y < 1 || y > Height - 1))
                        Room[0, i, y] = Items.Item.ItemType.Wall2;
                    else if (Util.Global.GetRandomInt(0, 20) == 1)
                    {
                        if(i <= Width-1)
                        Room[0, i, y] = Items.Item.GetRandomProp();
                    }
                    else
                        Room[0, i, y] = Items.Item.ItemType.Floor;

                    if ((i == 0 && y == 0) || (i == 0 && y == Height) || (i == Width && y == 0) || (i == Width && y == Height))
                    {
                        Room[0, i, y] = Items.Item.ItemType.SpotLight;
                    }
                }
            }

            //for (int i = 1; i <= Width-1; i++)
            //{
            //    for (int y = 1; y <= Height-1; y++)
            //    {
            //        if (Util.Global.GetRandomInt(0, 20) < 1)
            //        {
            //            Room[0, i, y] = Items.Item.GetRandomProp();
            //        }
            //    }
            //}

            if (Util.Global.GetRandomInt(0,10) < 4)
                Room[0, Width / 2, Height / 2] = Items.Item.ItemType.ChestLoot;
            else if(Util.Global.GetRandomInt(0,10) < 1)
                Room[0, Width / 2, Height / 2] = Items.Item.ItemType.Bed;

            Room[0, Width, Util.Global.GetRandomInt(1, Height)] = Items.Item.ItemType.Door;

            return Room;
        }
    }
}
