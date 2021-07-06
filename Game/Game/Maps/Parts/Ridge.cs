using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Maps
    {
        class Ridge
        {
            public static Items.Item.ItemType[, ,] Get()
            {
                int Width = 0;
                int Height = 0;
                int RiverDir = Util.Global.GetRandomInt(1, 5);
                switch (RiverDir)
                {
                    case 1:
                        //NS
                        Width = Util.Global.GetRandomInt(6, 8);
                        Height = Util.Global.GetRandomInt(25, 35);
                        break;
                    case 2:
                        Width = Util.Global.GetRandomInt(25, 35);
                        Height = Util.Global.GetRandomInt(6, 8);
                        break;
                    case 3:
                        Width = Util.Global.GetRandomInt(6, 35);
                        Height = Util.Global.GetRandomInt(6, 35);
                        break;
                    case 4:
                        Width = Util.Global.GetRandomInt(6, 35);
                        Height = Util.Global.GetRandomInt(6, 35);
                        break;
                }


                Items.Item.ItemType[, ,] Room = new Items.Item.ItemType[1, Width + 1, Height + 1];
                if (RiverDir == 1 || RiverDir == 2)
                {
                    for (int i = 0; i <= Width; i++)
                    {
                        for (int y = 0; y <= Height; y++)
                        {
                            if ((i < 1 || i > Width - 1 || y < 1 || y > Height - 1) && Util.Global.GetRandomInt(0, 10) < 5)
                                Room[0, i, y] = Items.Item.ItemType.Grass;
                            else if ((i < 2 || i > Width - 2 || y < 2 || y > Height - 2) && Util.Global.GetRandomInt(0, 10) < 2)
                                Room[0, i, y] = Items.Item.ItemType.Sand;
                            else if (i < 2 || i > Width - 2 || y < 2 || y > Height - 2)
                                Room[0, i, y] = Items.Item.ItemType.RockSmall;
                            else if ((i == 2 || i == Width - 2 || y == 2 || y == Height - 2) && Util.Global.GetRandomInt(0, 10) < 9)
                                Room[0, i, y] = Items.Item.ItemType.Sand;
                            else
                                Room[0, i, y] = Items.Item.ItemType.Rock;
                        }
                    }
                }
                if (RiverDir == 3)
                {
                    for (int i = 0; i <= Width; i++)
                    {
                        for (int y = 0; y <= Height; y++)
                        {
                            if (i == y)
                                Room[0, i, y] = Items.Item.ItemType.Rock;
                            else if (i == y + 1 && Util.Global.GetRandomInt(0, 10) < 6)
                                Room[0, i, y] = Items.Item.ItemType.Rock;
                            else if (i == y - 1 && Util.Global.GetRandomInt(0, 10) < 6)
                                Room[0, i, y] = Items.Item.ItemType.Rock;
                            else if (i == y + 2 && Util.Global.GetRandomInt(0, 10) < 4)
                                Room[0, i, y] = Items.Item.ItemType.Rock;
                            else if (i == y - 2 && Util.Global.GetRandomInt(0, 10) < 4)
                                Room[0, i, y] = Items.Item.ItemType.Rock;
                            else if (i == y + 2 && Util.Global.GetRandomInt(0, 10) < 5)
                                Room[0, i, y] = Items.Item.ItemType.Sand;
                            else if (i == y - 2 && Util.Global.GetRandomInt(0, 10) < 5)
                                Room[0, i, y] = Items.Item.ItemType.Sand;

                            else if (i == y + 3 && Util.Global.GetRandomInt(0, 10) < 7)
                                Room[0, i, y] = Items.Item.ItemType.RockSmall;
                            else if (i == y - 3 && Util.Global.GetRandomInt(0, 10) < 7)
                                Room[0, i, y] = Items.Item.ItemType.RockSmall;

                            else
                                Room[0, i, y] = Items.Item.ItemType.Grass;
                        }
                    }
                }

                if (RiverDir == 4)
                {
                    for (int i = 0; i <= Width; i++)
                    {
                        for (int y = 0; y <= Height; y++)
                        {
                            if (y == (Width - i))
                                Room[0, i, y] = Items.Item.ItemType.Rock;
                            else if (y == (Width - i) + 1 && Util.Global.GetRandomInt(0, 10) < 6)
                                Room[0, i, y] = Items.Item.ItemType.Rock;
                            else if (y == (Width - i) - 1 && Util.Global.GetRandomInt(0, 10) < 6)
                                Room[0, i, y] = Items.Item.ItemType.Rock;
                            else if (y == (Width - i) + 2 && Util.Global.GetRandomInt(0, 10) < 4)
                                Room[0, i, y] = Items.Item.ItemType.Rock;
                            else if (y == (Width - i) - 2 && Util.Global.GetRandomInt(0, 10) < 4)
                                Room[0, i, y] = Items.Item.ItemType.Rock;
                            else if (y == (Width - i) + 2 && Util.Global.GetRandomInt(0, 10) < 5)
                                Room[0, i, y] = Items.Item.ItemType.Sand;
                            else if (y == (Width - i) - 2 && Util.Global.GetRandomInt(0, 10) < 5)
                                Room[0, i, y] = Items.Item.ItemType.Sand;

                            else if (y == (Width - i) + 3 && Util.Global.GetRandomInt(0, 10) < 7)
                                Room[0, i, y] = Items.Item.ItemType.RockSmall;
                            else if (y == (Width - i) - 3 && Util.Global.GetRandomInt(0, 10) < 7)
                                Room[0, i, y] = Items.Item.ItemType.RockSmall;

                            else
                                Room[0, i, y] = Items.Item.ItemType.Grass;
                        }
                    }
                }


                return Room;
            }
        }
    }


