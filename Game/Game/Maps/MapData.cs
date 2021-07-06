using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game.Maps
{
    public static class MapData
    {

    public enum MapPart { caveEntrance, field, forrest, lavapond, mountain, pond, ridge, river, room, spawner, start, testDebug, totem, vein, volcano, town}
    public enum Biome { Grass, Mini, Cave, Shop, Stable }

    public static List<MapItems> GameMapItems { get; set; }
    public static List<MapMapParts> GameMapParts { get; set; }


    public struct MapItems
    {
        public Items.Item.ItemType itemType {get;set;}
        public float chance {get;set;}
        public List<Biome> biomes { get; set; }
        public int minLevel { get; set; }
    }

    public struct MapMapParts
    {
        public MapPart mapPart {get;set;}
        public float chance {get;set;}
        public List<Biome> biomes { get; set; }
        public int minLevel { get; set; }
    }

    public static List<MapItems> GetMapItemsByBiome(Vector3 mapVector, Game.Maps.MapData.Biome Bio)
    {
        Vector3 Distance = Vector3.Subtract(new Vector3(50, 50, 0), mapVector);
        int Level = (int)Math.Abs(Distance.X) + (int)Math.Abs(Distance.Y);
        List<MapItems> MIs = GameMapItems.Where(x => Level >= x.minLevel  && x.biomes.Contains(Bio)).ToList();
        return MIs;
    }

    public static MapPart GetRandomMapPartsByBiome(Vector3 mapVector, Game.Maps.MapData.Biome Bio)
    {
        Vector3 Distance = Vector3.Subtract(new Vector3(50, 50, 0), mapVector);
        int Level = (int)Math.Abs(Distance.X) + (int)Math.Abs(Distance.Y);
        List<MapMapParts> MPs = GameMapParts.Where(x => Level >= x.minLevel && x.biomes.Contains(Bio)).ToList();
        return MPs.OrderBy(x => Guid.NewGuid()).FirstOrDefault().mapPart;
    }

    static string UppercaseFirst(string s)
    {
        // Check for empty string.
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        // Return char and concat substring.
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    public static Items.Item.ItemType[, ,] GetMapByPart(MapPart MP)
    {
        try
        {
            string myclass = "Game.Maps." + UppercaseFirst(MP.ToString());
            Type T = Type.GetType(myclass);
            System.Reflection.MethodInfo mi = T.GetMethod("Get");
            object ret = mi.Invoke(null, null);
            return (Items.Item.ItemType[, ,])ret;
        }
        catch (Exception ex)
        {
            Util.Base.Log(ex.Message);
            return null;
        }

    }

    public static void Init()
    {
        Init_MP();
        Init_MI();
    }

    public static void Init_MP()
    {
        GameMapParts = new List<MapMapParts>();
        MapMapParts MP = new MapMapParts();
        List<Biome> B = new List<Biome>();

        MP = new MapMapParts(); MP.mapPart = MapPart.vein;
        B = new List<Biome>(); B.Add(Biome.Cave);
        MP.chance = 1F; MP.minLevel = 0; MP.biomes = B; GameMapParts.Add(MP);

        MP = new MapMapParts(); MP.mapPart = MapPart.forrest;
        B = new List<Biome>(); B.Add(Biome.Grass);
        MP.chance = 1F; MP.minLevel = 0; MP.biomes = B; GameMapParts.Add(MP);

        MP = new MapMapParts(); MP.mapPart = MapPart.river;
        B = new List<Biome>(); B.Add(Biome.Grass);
        MP.chance = 1F; MP.minLevel = 0; MP.biomes = B; GameMapParts.Add(MP);

        MP = new MapMapParts(); MP.mapPart = MapPart.mountain;
        B = new List<Biome>(); B.Add(Biome.Grass);
        MP.chance = 1F; MP.minLevel = 0; MP.biomes = B; GameMapParts.Add(MP);

        MP = new MapMapParts(); MP.mapPart = MapPart.ridge;
        B = new List<Biome>(); B.Add(Biome.Grass);
        MP.chance = 1F; MP.minLevel = 0; MP.biomes = B; GameMapParts.Add(MP);

        MP = new MapMapParts(); MP.mapPart = MapPart.pond;
        B = new List<Biome>(); B.Add(Biome.Grass);
        MP.chance = 1F; MP.minLevel = 0; MP.biomes = B; GameMapParts.Add(MP);

        MP = new MapMapParts(); MP.mapPart = MapPart.field;
        B = new List<Biome>(); B.Add(Biome.Grass);
        MP.chance = 1F; MP.minLevel = 0; MP.biomes = B; GameMapParts.Add(MP);

        MP = new MapMapParts(); MP.mapPart = MapPart.spawner;
        B = new List<Biome>(); B.Add(Biome.Cave); B.Add(Biome.Grass);
        MP.chance = 1F; MP.minLevel = 1; MP.biomes = B; GameMapParts.Add(MP);

        MP = new MapMapParts(); MP.mapPart = MapPart.room;
        B = new List<Biome>(); B.Add(Biome.Cave); B.Add(Biome.Grass);
        MP.chance = 1F; MP.minLevel = 1; MP.biomes = B; GameMapParts.Add(MP);

        MP = new MapMapParts(); MP.mapPart = MapPart.totem;
        B = new List<Biome>(); B.Add(Biome.Grass);
        MP.chance = 1F; MP.minLevel = 1; MP.biomes = B; GameMapParts.Add(MP);

        MP = new MapMapParts(); MP.mapPart = MapPart.town;
        B = new List<Biome>(); B.Add(Biome.Grass); B.Add(Biome.Cave);
        MP.chance = 1F; MP.minLevel = 1; MP.biomes = B; GameMapParts.Add(MP);

        MP = new MapMapParts(); MP.mapPart = MapPart.volcano;
        B = new List<Biome>(); B.Add(Biome.Grass);
        MP.chance = 1F; MP.minLevel = 3; MP.biomes = B; GameMapParts.Add(MP);

        MP = new MapMapParts(); MP.mapPart = MapPart.lavapond;
        B = new List<Biome>(); B.Add(Biome.Grass); B.Add(Biome.Cave);
        MP.chance = 1F; MP.minLevel = 3; MP.biomes = B; GameMapParts.Add(MP);
    }

    public static void Init_MI()
        {
            GameMapItems = new List<MapItems>();
            MapItems MI = new MapItems();
            List<Biome> B = new List<Biome>();

            MI = new MapItems(); MI.itemType = Items.Item.ItemType.Tree;
            B = new List<Biome>(); B.Add(Biome.Grass);
            MI.chance = Util.Global.GetRandomFloat(.001F, .09F); MI.minLevel = 0; MI.biomes = B; GameMapItems.Add(MI);

            //MI = new MapItems(); MI.itemType = Items.Item.ItemType.TreeStump;
            //B = new List<Biome>(); B.Add(Biome.Grass);
            //MI.chance = Util.Global.GetRandomFloat(.001F, .004F); MI.minLevel = 0; MI.biomes = B; GameMapItems.Add(MI);

            MI = new MapItems(); MI.itemType = Items.Item.ItemType.Coin;
            B = new List<Biome>(); B.Add(Biome.Grass);
            MI.chance = Util.Global.GetRandomFloat(.0001F, .002F); MI.minLevel = 0; MI.biomes = B; GameMapItems.Add(MI);

            MI = new MapItems(); MI.itemType = Items.Item.ItemType.Grain;
            B = new List<Biome>(); B.Add(Biome.Grass);
            MI.chance = Util.Global.GetRandomFloat(.0001F, .002F); MI.minLevel = 0; MI.biomes = B; GameMapItems.Add(MI);
            
            MI = new MapItems(); MI.itemType = Items.Item.ItemType.Health;
            B = new List<Biome>(); B.Add(Biome.Grass);
            MI.chance = Util.Global.GetRandomFloat(.0001F, .001F); MI.minLevel = 0; MI.biomes = B; GameMapItems.Add(MI);

            MI = new MapItems(); MI.itemType = Items.Item.ItemType.IronOre;
            B = new List<Biome>(); B.Add(Biome.Grass);
            MI.chance = Util.Global.GetRandomFloat(.0001F, .001F); MI.minLevel = 0; MI.biomes = B; GameMapItems.Add(MI);

            MI = new MapItems(); MI.itemType = Items.Item.ItemType.CaveEntrance;
            B = new List<Biome>(); B.Add(Biome.Grass);
            MI.chance = .0001F; MI.minLevel = 1; MI.biomes = B; GameMapItems.Add(MI);

            MI = new MapItems(); MI.itemType = Items.Item.ItemType.WarpPad;
            B = new List<Biome>(); B.Add(Biome.Grass);
            MI.chance = .0001F; MI.minLevel = 1; MI.biomes = B; GameMapItems.Add(MI);

            MI = new MapItems(); MI.itemType = Items.Item.ItemType.Shop;
            B = new List<Biome>(); B.Add(Biome.Grass);
            MI.chance = .0001F; MI.minLevel = 1; MI.biomes = B; GameMapItems.Add(MI);

            MI = new MapItems(); MI.itemType = Items.Item.ItemType.GoldOre;
            B = new List<Biome>(); B.Add(Biome.Grass);
            MI.chance = .0002F; MI.minLevel = 2; MI.biomes = B; GameMapItems.Add(MI);

            MI = new MapItems(); MI.itemType = Items.Item.ItemType.Fountain;
            B = new List<Biome>(); B.Add(Biome.Grass);
            MI.chance = .0001F; MI.minLevel = 3; MI.biomes = B; GameMapItems.Add(MI);

            MI = new MapItems(); MI.itemType = Items.Item.ItemType.SilverOre;
            B = new List<Biome>(); B.Add(Biome.Grass);
            MI.chance = .0004F; MI.minLevel = 5; MI.biomes = B; GameMapItems.Add(MI);

            MI = new MapItems(); MI.itemType = Items.Item.ItemType.HayStack;
            B = new List<Biome>(); B.Add(Biome.Stable);
            MI.chance = .003F; MI.minLevel = 0; MI.biomes = B; GameMapItems.Add(MI);

            List<Items.Item.ItemType> RndType = new List<Items.Item.ItemType>();
            RndType.AddRange(Enum.GetValues(typeof(Items.Item.ItemType)).Cast<Items.Item.ItemType>().ToList().Where(x => x.ToString().Contains("Garden")).ToList());
            foreach (Items.Item.ItemType I in RndType)
            {
                MI = new MapItems(); MI.itemType = I;
                B = new List<Biome>(); B.Add(Biome.Grass);
                MI.chance = .0002F; MI.minLevel = 2; MI.biomes = B; GameMapItems.Add(MI);
            }
           

        }



    }
}
