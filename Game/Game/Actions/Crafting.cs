using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Actions
{
    public class Crafting
    {
        public enum Type { Hand, Stove, Anvil, Workbench, Caldron, GreenHouse };

        public class Recipie
        {
            public bool Active { get; set; }
            public Items.Item.ItemType Result { get; set; }
            public int ResultNum { get; set; }
            public Dictionary<Items.Item.ItemType, int> ItemCost { get; set; }
        }

        public static List<Crafting.Recipie> GetRecipiesByType(Type T)
        {
            switch (T)
            {
                case Type.Hand:
                    return GetHandRecipies();
                case Type.Stove:
                    return GetStoveRecipies();
                case Type.Caldron:
                    return GetCaldronRecipies();
                case Type.Workbench:
                    return GetWorkBenchRecipies();
                case Type.Anvil:
                    return GetAnvilRecipies();
                case Type.GreenHouse:
                    return GetGreenHouseRecipies();
            }
            return null;
        }

        public static List<Crafting.Recipie> GetWorkBenchRecipies()
        {
            List<Crafting.Recipie> RR = new List<Recipie>();
            RR.AddRange(GetHandRecipies());
            Recipie R = new Recipie();

            R = new Recipie();
            R.Active = true;
            R.ResultNum = 1;
            R.Result = Items.Item.ItemType.Plank;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.Log, 2);
            RR.Add(R);

            R = new Recipie();
            R.Active = true;
            R.ResultNum = 1;
            R.Result = Items.Item.ItemType.BuildFence;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.Log, 4);
            RR.Add(R);

            R = new Recipie();
            R.Active = true;
            R.ResultNum = 1;
            R.Result = Items.Item.ItemType.Chest;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.Plank, 5);
            R.ItemCost.Add(Items.Item.ItemType.IronBar, 1);
            RR.Add(R);

            R = new Recipie();
            R.Active = true;
            R.ResultNum = 1;
            R.Result = Items.Item.ItemType.GreenHouse;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.Log, 5);
            RR.Add(R);

            R = new Recipie();
            R.Active = true;
            R.ResultNum = 1;
            R.Result = Items.Item.ItemType.Anvil;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.IronBar, 10);
            RR.Add(R);

            R = new Recipie();
            R.Active = true;
            R.ResultNum = 1;
            R.Result = Items.Item.ItemType.Stove;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.IronBar, 5);
            R.ItemCost.Add(Items.Item.ItemType.GoldBar, 1);
            RR.Add(R);

            R = new Recipie();
            R.Active = true;
            R.ResultNum = 1;
            R.Result = Items.Item.ItemType.Tent;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.Log, 10);
            R.ItemCost.Add(Items.Item.ItemType.Plank, 5);
            R.ItemCost.Add(Items.Item.ItemType.IronBar, 10);
            R.ItemCost.Add(Items.Item.ItemType.GoldBar, 5);
            RR.Add(R);

            R = new Recipie();
            R.Active = true;
            R.ResultNum = 1;
            R.Result = Items.Item.ItemType.Caldron;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.Log, 10);
            R.ItemCost.Add(Items.Item.ItemType.BerryHealth, 10);
            R.ItemCost.Add(Items.Item.ItemType.BerryStrength, 10);
            R.ItemCost.Add(Items.Item.ItemType.GoldBar, 5);
            RR.Add(R);


            R = new Recipie();
            R.Active = true;
            R.ResultNum = 1;
            R.Result = Items.Item.ItemType.Bed;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.Log, 20);
            R.ItemCost.Add(Items.Item.ItemType.Cloth, 5);
            R.ItemCost.Add(Items.Item.ItemType.IronBar, 5);
            R.ItemCost.Add(Items.Item.ItemType.GoldBar, 1);
            RR.Add(R);

            R = new Recipie();
            R.Active = true;
            R.ResultNum = 1;
            R.Result = Items.Item.ItemType.Cloth;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.Grain, 10);
            RR.Add(R);

            return RR;
        }

        public static List<Crafting.Recipie> GetGreenHouseRecipies()
        {
            List<Crafting.Recipie> RR = new List<Recipie>();
            Recipie R = new Recipie();

            R = new Recipie();
            R.Active = true;
            R.ResultNum = 2;
            R.Result = Items.Item.ItemType.SeedGrain;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.Grain, 1);
            RR.Add(R);

            R = new Recipie();
            R.Active = true;
            R.ResultNum = 2;
            R.Result = Items.Item.ItemType.SeedHealth;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.BerryHealth, 1);
            RR.Add(R);

            R = new Recipie();
            R.Active = true;
            R.ResultNum = 2;
            R.Result = Items.Item.ItemType.SeedStrength;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.BerryStrength, 1);
            RR.Add(R);

            R = new Recipie();
            R.Active = true;
            R.ResultNum = 2;
            R.Result = Items.Item.ItemType.SeedSpeed;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.BerrySpeed, 1);
            RR.Add(R);

            R = new Recipie();
            R.Active = true;
            R.ResultNum = 2;
            R.Result = Items.Item.ItemType.SeedTree;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.Log, 1);
            RR.Add(R);

            return RR;
        }

        public static List<Crafting.Recipie> GetAnvilRecipies()
        {
            List<Crafting.Recipie> RR = new List<Recipie>();
            Recipie R = new Recipie();

            R = new Recipie();
            R.Active = true;
            R.ResultNum = 1;
            R.Result = Items.Item.ItemType.Axe;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.Log, 2);
            R.ItemCost.Add(Items.Item.ItemType.IronBar, 5);
            RR.Add(R);

            R = new Recipie();
            R.Active = true;
            R.ResultNum = 1;
            R.Result = Items.Item.ItemType.Pickaxe;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.Log, 2);
            R.ItemCost.Add(Items.Item.ItemType.IronBar, 10);
            RR.Add(R);

            R = new Recipie();
            R.Active = true;
            R.ResultNum = 1;
            R.Result = Items.Item.ItemType.Hammer;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.Log, 2);
            R.ItemCost.Add(Items.Item.ItemType.IronBar, 20);
            RR.Add(R);

            R = new Recipie();
            R.Active = true;
            R.ResultNum = 1;
            R.Result = Items.Item.ItemType.Shovel;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.Log, 2);
            R.ItemCost.Add(Items.Item.ItemType.IronBar, 15);
            RR.Add(R);

            return RR;
        }

        public static List<Crafting.Recipie> GetStoveRecipies()
        {
            List<Crafting.Recipie> RR = new List<Recipie>();

            Recipie R = new Recipie();
            R.Active = true;
            R.Result = Items.Item.ItemType.Bread;
            R.ResultNum = 1;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.Grain, 5);
            RR.Add(R);

            R = new Recipie();
            R.Active = true;
            R.Result = Items.Item.ItemType.Glass;
            R.ResultNum = 2;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.Sand, 1);
            RR.Add(R);

            return RR;
        }

        public static List<Crafting.Recipie> GetCaldronRecipies()
        {
            List<Crafting.Recipie> RR = new List<Recipie>();
            Recipie R = new Recipie();

            R = new Recipie();
            R.Active = true;
            R.ResultNum = 1;
            R.Result = Items.Item.ItemType.PotionHealth;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.Glass, 5);
            R.ItemCost.Add(Items.Item.ItemType.BerryHealth, 10);
            RR.Add(R);

            R = new Recipie();
            R.Active = true;
            R.ResultNum = 1;
            R.Result = Items.Item.ItemType.PotionStrength;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.Glass, 5);
            R.ItemCost.Add(Items.Item.ItemType.BerryStrength, 10);
            RR.Add(R);

            R = new Recipie();
            R.Active = true;
            R.ResultNum = 1;
            R.Result = Items.Item.ItemType.PotionSpeed;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.Glass, 5);
            R.ItemCost.Add(Items.Item.ItemType.BerrySpeed, 10);
            RR.Add(R);

            R = new Recipie();
            R.Active = true;
            R.ResultNum = 1;
            R.Result = Items.Item.ItemType.PotionDexterity;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.Glass, 5);
            R.ItemCost.Add(Items.Item.ItemType.BerryDexterity, 10);
            RR.Add(R);

            return RR;
        }

        public static List<Crafting.Recipie> GetHandRecipies()
        {
            List<Crafting.Recipie> RR = new List<Recipie>();

            Recipie R = new Recipie();
            R.Active=true;
            R.Result = Items.Item.ItemType.Torch;
            R.ResultNum = 1;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.Oil, 1);
            R.ItemCost.Add(Items.Item.ItemType.Log, 1);
            RR.Add(R);

            R = new Recipie();
            R.Active = true;
            R.Result = Items.Item.ItemType.WorkBench;
            R.ResultNum = 1;
            R.ItemCost = new Dictionary<Items.Item.ItemType, int>();
            R.ItemCost.Add(Items.Item.ItemType.Log, 10);
            RR.Add(R);

            return RR;
        }

    }
}
