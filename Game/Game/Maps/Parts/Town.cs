using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Maps
{
    public class Town
    {
        public static Items.Item.ItemType[, ,] Get()
        {
            int Width = Util.Global.GetRandomInt(20, 40);
            int Height = Util.Global.GetRandomInt(20, 40);

            Items.Item.ItemType[, ,] Room = new Items.Item.ItemType[1, Width + 1, Height + 1];

            for (int i = 0; i <= Width; i++)
            {
                for (int y = 0; y <= Height; y++)
                {
                    if ((i < 1 || i > Width - 1 || y < 1 || y > Height - 1))
                    {
                        if (Util.Global.GetRandomInt(1, 5) == 1)
                            Room[0, i, y] = Items.Item.ItemType.Path;
                        else
                            Room[0, i, y] = Items.Item.ItemType.Fence;
                    }
                    else
                    {
                        switch (Util.Global.GetRandomInt(1,10))
                        {
                            case 2:
                                Room[0, i, y] = Items.Item.ItemType.Grass;
                                break;
                            case 3:
                                Room[0, i, y] = Items.Item.ItemType.Gravel;
                                break;
                            default:
                                Room[0, i, y] = Items.Item.ItemType.Path;
                                break;
                        }
                    }
                }
            }

            int SectionWidth = Width / 4;
            int SectionHeight = Height / 4;

            for (int row = 0; row <= 3; row++)
            {
                for (int col = 0; col <= 3; col++)
                {

                    int loww = (row * SectionWidth) + 3;
                    int highw = (row * SectionWidth)+ 3;

                    int lowh = (col * SectionHeight) + 3;
                    int highh = (col * SectionHeight) + 3;

                    int w = Util.Global.GetRandomInt(loww, highw); 
                    int h = Util.Global.GetRandomInt(lowh, highh); 

                    int RW = Util.Global.GetRandomInt(3, SectionWidth-2);
                    int RH = Util.Global.GetRandomInt(3, SectionHeight-2);

                    if (w + RW > Width)
                    {
                        w = w - (w + RW - Width) - Util.Global.GetRandomInt(1, Width / 2);
                    }
                    if (h + RH > Height)
                    {
                        h = h - (h + RH - Height) - Util.Global.GetRandomInt(1, Height / 2);
                    }

                    if (Util.Global.GetRandomInt(0, 4) == 1)
                    {
                        for (int i = 0; i <= RW; i++)
                        {
                            for (int y = 0; y <= RH; y++)
                            {
                                Room[0, i + w, y + h] = Items.Item.ItemType.Grass;
                            }

                        }

                        if (Util.Global.GetRandomInt(0, 5) == 1)
                        {
                            Room[0, w + Util.Global.GetRandomInt(1,RW/2), h + Util.Global.GetRandomInt(1, RH/2)] = Items.Item.ItemType.Shop;
                        }
                        else if (Util.Global.GetRandomInt(0, 5) == 1)
                        {

                            int r = ((RW / 2 + RH / 2) / 2) - 1; // radius
                            int ox = RW / 2, oy = RH / 2; // origin

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

                                    Room[0, m + w, n + h] = Items.Item.ItemType.Water;
                                }
                            }
                        }

                    }
                    else
                    {
                        for (int i = 0; i <= RW; i++)
                        {
                            for (int y = 0; y <= RH; y++)
                            {
                                if ((i < 1 || i > RW - 1 || y < 1 || y > RH - 1))
                                    Room[0, i + w, y + h] = Items.Item.ItemType.Wall2;
                                else if (Util.Global.GetRandomInt(0, 15) == 1)
                                {
                                    Items.Item.ItemType IT = Items.Item.GetRandomProp(); 
                                    List<Items.Item.ItemType> biglist = new List<Items.Item.ItemType>();
                                    biglist.Add(Items.Item.ItemType.Shelf1);
                                    biglist.Add(Items.Item.ItemType.Rug1);
                                    biglist.Add(Items.Item.ItemType.Rug2);
                                    if ((i > RW - 2 || y > RH - 2) && biglist.Contains(IT))
                                    { Room[0, i + w, y + h] = Items.Item.ItemType.Floor; }
                                    else
                                    { Room[0, i + w, y + h] = IT; }
                                }
                                else
                                    Room[0, i + w, y + h] = Items.Item.ItemType.Floor;

                                if ((i == 0 && y == 0) || (i == 0 && y == RH) || (i == RW && y == 0) || (i == RW && y == RH))
                                {
                                    Room[0, i + w, y + h] = Items.Item.ItemType.SpotLight;
                                }
                            }
                        }
                        Room[0, w + RW, h + Util.Global.GetRandomInt(1, RH)] = Items.Item.ItemType.Door;
                    }



                }
            }
           
            
            //if (Util.Global.GetRandomInt(0, 10) < 4)
            //    Room[0, Width / 2, Height / 2] = Items.Item.ItemType.ChestLoot;
            //else if (Util.Global.GetRandomInt(0, 10) < 1)
            //    Room[0, Width / 2, Height / 2] = Items.Item.ItemType.Bed;

            //Room[0, Width, Util.Global.GetRandomInt(1, Height)] = Items.Item.ItemType.Door;

            return Room;
        }
    }
}
