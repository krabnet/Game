using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Actions;
using Microsoft.Xna.Framework;

namespace Game.Maps
{
    

    [Serializable]
    public class Map
    {
        public List<Objects.Sprite2d> Sprite2d { get; set; }
        public List<Objects.AnimSprite> AnimSprite { get; set; }
        public Vector3 MapVector { get; set; }
        public bool HasWarp { get; set; }
        public bool HasCave { get; set; }
        public bool HasTown { get; set; }
        private const int size = 32;
        private const int sizey = 32;

        #region MapGeneration

        public void GenerateMap(Vector3 mapVector, Game.Maps.MapData.Biome bio, int MapSizeH, int MapSizeW)
        {
            MapData.Init();
            HasCave = false;
            HasTown = false;
            if(mapVector == Util.Global.StartLocation)
                HasWarp = true;
            else
                HasWarp = false;

            Items.Item.ItemType BaseType = Items.Item.ItemType.Grass;
            switch (bio)
            {
                case Game.Maps.MapData.Biome.Grass:
                    BaseType = Items.Item.ItemType.Grass;
                    AddInitialBase(mapVector, MapSizeH, MapSizeW, BaseType);
                    AddWarps(mapVector);
                    AddMapParts(mapVector, BaseType, bio);
                    AddRoads(mapVector, BaseType);
                    AddBorder(mapVector, Util.Global.SizeMap, Util.Global.SizeMap, bio);
                    AddDrops(mapVector, bio, BaseType);
                    AddProps(mapVector, BaseType);

                    if (mapVector == new Vector3(50, 50, 0))
                        AddMapPart(Game.Maps.MapData.MapPart.start, 45, 45, BaseType);

                    //AddMapPart(Maps.MapData.MapPart.testDebug, 45, 55, BaseType);
                    //AddMapPart(Maps.MapData.MapPart.caveEntrance, 55, 55, BaseType);


                    AddEnemy(mapVector);
                    break;
                case Game.Maps.MapData.Biome.Mini:
                    BaseType = Items.Item.ItemType.Grass;
                    AddInitialBase(mapVector, MapSizeH, MapSizeW, BaseType);
                    AddBorder(mapVector, MapSizeH, MapSizeW, bio);
                    AddTorches(mapVector, MapSizeH, MapSizeW);
                    AddProps(mapVector, Items.Item.ItemType.Grass);
                    break;
                case Game.Maps.MapData.Biome.Fish:
                    BaseType = Items.Item.ItemType.Grass;
                    AddInitialBase(mapVector, MapSizeH, MapSizeW, BaseType);
                    //AddBorder(mapVector, MapSizeH, MapSizeW, bio);
                    //AddTorches(mapVector, MapSizeH, MapSizeW);
                    //AddProps(mapVector, Items.Item.ItemType.Grass);
                    break;
                case Game.Maps.MapData.Biome.Shop:
                    BaseType = Items.Item.ItemType.Path;
                    AddInitialBase(mapVector, MapSizeH, MapSizeW, BaseType);
                    AddBorder(mapVector, MapSizeH, MapSizeW, bio);
                    AddTorches(mapVector, MapSizeH, MapSizeW);
                    Sprite2d.Add(Items.Item.GetItemByType(Items.Item.ItemType.Shelf1, new Vector2(2 * 32, 0 * 32)));
                    Sprite2d.Add(Items.Item.GetItemByType(Items.Item.ItemType.Shelf1, new Vector2(7 * 32, 0 * 32)));
                    Sprite2d.Add(Items.Item.GetItemByType(Items.Item.ItemType.Rug2, new Vector2(2 * 32, 4 * 32)));
                    Sprite2d.Add(Items.Item.GetItemByType(Items.Item.ItemType.Rug2, new Vector2(3 * 32, 4 * 32)));
                    Sprite2d.Add(Items.Item.GetItemByType(Items.Item.ItemType.Rug2, new Vector2(5 * 32, 4 * 32)));
                    Sprite2d.Add(Items.Item.GetItemByType(Items.Item.ItemType.Rug2, new Vector2(7 * 32, 4 * 32)));
                    Sprite2d.Add(Items.Item.GetItemByType(Items.Item.GetShopProp(), new Vector2(1 * 32, 1 * 32)));
                    Sprite2d.Add(Items.Item.GetItemByType(Items.Item.GetShopProp(), new Vector2(9 * 32, 1 * 32)));
                    Sprite2d.Add(Items.Item.GetItemByType(Items.Item.GetShopProp(), new Vector2(1 * 32, 9 * 32)));
                    Sprite2d.Add(Items.Item.GetItemByType(Items.Item.GetShopProp(), new Vector2(2 * 32, 9 * 32)));
                    Sprite2d.Add(Items.Item.GetItemByType(Items.Item.GetShopProp(), new Vector2(5 * 32, 9 * 32)));
                    Sprite2d.Add(Items.Item.GetItemByType(Items.Item.GetShopProp(), new Vector2(8 * 32, 9 * 32)));
                    Sprite2d.Add(Items.Item.GetItemByType(Items.Item.GetShopProp(), new Vector2(9 * 32, 9 * 32)));
                    break;

                case Game.Maps.MapData.Biome.Stable:
                    BaseType = Items.Item.ItemType.Grass;
                    AddInitialBase(mapVector, MapSizeH, MapSizeW, BaseType);
                    AddBorder(mapVector, MapSizeH, MapSizeW, bio);
                    AddTorches(mapVector, MapSizeH, MapSizeW);
                    AddDrops(mapVector, bio, BaseType);
                    AddProps(mapVector, Items.Item.ItemType.Grass);
                    break;

                case Game.Maps.MapData.Biome.Cave:
                    BaseType = Items.Item.ItemType.Wall;
                    AddInitialBase(mapVector, MapSizeH, MapSizeW, BaseType);
                    AddBorder(mapVector, MapSizeH, MapSizeW, bio);
                    AddMapParts(mapVector, BaseType, bio);
                    break;
            }

            this.Sprite2d = Sprite2d;
            Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z] = this;
        }

        private void AddInitialBase(Vector3 mapVector, int MapSizeH, int MapSizeW, Items.Item.ItemType BaseType)
        {
            int tileW = 0;
            int tileH = 0;
            MapVector = mapVector;
            Tuple<int, int> Loc = new Tuple<int, int>(0, 0);
            Sprite2d = new List<Objects.Sprite2d>();
            AnimSprite = new List<Objects.AnimSprite>();
            for (int i = Loc.Item1; i <= MapSizeH; i++)
            {
                for (int y = Loc.Item2; y <= MapSizeW; y++)
                {
                    Objects.Sprite2d Grass = Items.Item.GetItemByType(BaseType, new Vector2(tileH, tileW));
                    Grass.name = i.ToString() + ":" + y.ToString();
                    Sprite2d.Add(Grass);
                    tileH = tileH + size;
                }
                tileH = 1;
                tileW = tileW + sizey;
            }
        }

        private void AddBorder(Vector3 mapVector, int MapSizeH, int MapSizeW, Game.Maps.MapData.Biome bio)
        {
            List<Objects.Sprite2d> Sp = new List<Objects.Sprite2d>();
            Sp.AddRange(Sprite2d);
            foreach (Objects.Sprite2d S in Sp)
            {
                if (S.name != "North" && S.name != "South" && S.name != "West" && S.name != "East")
                {
                    int lenH = MapSizeH * size;
                    int lenW = MapSizeW * size;
                    if (
                            Util.Base.collision(S, new Rectangle(-1, -1, lenW, 25)) || //Top
                            Util.Base.collision(S, new Rectangle(0, 0, 25, lenH)) || //Left
                            Util.Base.collision(S, new Rectangle(lenW + 25, 0, 0, lenH)) || //Right
                            Util.Base.collision(S, new Rectangle(0, lenH, lenW + 25, 25)) //Bottom
                        )
                    {

                        Sprite2d.RemoveAll(x => x.Position == S.Position);
                        switch (bio)
                        {
                            case Game.Maps.MapData.Biome.Shop:
                                Objects.Sprite2d Wall = new Objects.Sprite2d("wall3", S.name, true, S.Position, new Vector2(size, sizey), 5, Objects.Base.ControlType.None);
                                Wall.clipping=true;
                                Sprite2d.Add(Wall);
                                break;
                            case Game.Maps.MapData.Biome.Stable:
                                Objects.Sprite2d StableWall = new Objects.Sprite2d("wall3", S.name, true, S.Position, new Vector2(size, sizey), 5, Objects.Base.ControlType.None);
                                StableWall.clipping = true;
                                Sprite2d.Add(StableWall);
                                break;
                            default:
                                Sprite2d.Add(new Objects.Sprite2d("gravel", S.name, true, S.Position, new Vector2(size, sizey), 5, Objects.Base.ControlType.None));
                                Objects.Sprite2d rock = new Objects.Sprite2d("rocksmall", "rock" + S.name, true, S.Position, new Vector2(size, sizey), 5, Objects.Base.ControlType.None);
                                rock.clipping = true;
                                Sprite2d.Add(rock);
                                break;
                        }
                        

                    }
                }
            }
        }

        private void AddTorches(Vector3 mapVector, int MapSizeH, int MapSizeW)
        {
            string[] torches = new string[4] { "0:0", MapSizeH.ToString() + ":0", "0:" + MapSizeW.ToString(), MapSizeH.ToString() + ":" + MapSizeW.ToString() };
            foreach (string TileName in torches)
            {
                Objects.Sprite2d TorchSprite = Items.Item.GetItemByType(Items.Item.ItemType.Torch, (Sprite2d.Where(x => x.name == TileName).FirstOrDefault().Position));
                TorchSprite.LightSourceDistance = 1000f;
                TorchSprite.Item = new Objects.Item();
                TorchSprite.orderNum = 10;
                TorchSprite.SpriteType = Objects.Base.Type.Tile;
                Sprite2d.Add(TorchSprite);
                TorchSprite.actionCall = new List<ActionCall>();
                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().modelname = "wall3";
            }
        }

        private void AddWarps(Vector3 mapVector)
        {
            int WarpLocation;
            Objects.Sprite2d Pos = new Objects.Sprite2d();

            WarpLocation = Util.Global.GetRandomInt(10, Util.Global.SizeMap - 10);
            Pos = Sprite2d.Where(x => x.name == "0:" + WarpLocation.ToString()).FirstOrDefault();
            Sprite2d.RemoveAll(x => x.name == "0:" + WarpLocation.ToString());
            Objects.Sprite2d Warp = new Objects.Sprite2d("warp", "", true, Pos.Position - new Vector2(5, 5), new Vector2(size + 15, sizey + 15), 5, Objects.Base.ControlType.None);
            Warp.name = "North";
            Warp.orderNum = 101;
            Warp.effectType = Objects.Base.EffectType.Ripple;
            List<object> obj1 = new List<object>();
            obj1.Add(new Vector3(MapVector.X + 1, MapVector.Y, MapVector.Z));
            obj1.Add(new Vector3(MapVector.X, MapVector.Y, MapVector.Z));
            ActionCall AC = new ActionCall(Game.Actions.ActionType.Collision, typeof(Maps.Map), "WarpMap", obj1);
            Warp.actionCall.Add(AC);
            Warp.LightSourceDistance = 75f;
            Sprite2d.Add(Warp);

            WarpLocation = Util.Global.GetRandomInt(10, Util.Global.SizeMap - 10);
            Pos = Sprite2d.Where(x => x.name == Util.Global.SizeMap.ToString() + ":" + WarpLocation.ToString()).FirstOrDefault();
            Sprite2d.RemoveAll(x => x.name == Util.Global.SizeMap.ToString() + ":" + WarpLocation.ToString());
            Warp = new Objects.Sprite2d("warp", "", true, Pos.Position - new Vector2(5, 5), new Vector2(size + 15, sizey + 15), 5, Objects.Base.ControlType.None);
            Warp.name = "South";
            Warp.orderNum = 101;
            Warp.effectType = Objects.Base.EffectType.Ripple;
            obj1 = new List<object>();
            obj1.Add(new Vector3(MapVector.X - 1, MapVector.Y, MapVector.Z));
            obj1.Add(new Vector3(MapVector.X, MapVector.Y, MapVector.Z));
            AC = new ActionCall(Game.Actions.ActionType.Collision, typeof(Maps.Map), "WarpMap", obj1);
            Warp.actionCall.Add(AC);
            Warp.LightSourceDistance = 75f;
            Sprite2d.Add(Warp);

            WarpLocation = Util.Global.GetRandomInt(10, Util.Global.SizeMap - 10);
            Pos = Sprite2d.Where(x => x.name == WarpLocation.ToString() + ":0").FirstOrDefault();
            Sprite2d.RemoveAll(x => x.name == WarpLocation.ToString() + ":0");
            Warp = new Objects.Sprite2d("warp", "", true, Pos.Position - new Vector2(5, 5), new Vector2(size + 15, sizey + 15), 5, Objects.Base.ControlType.None);
            Warp.name = "West";
            Warp.orderNum = 101;
            Warp.effectType = Objects.Base.EffectType.Ripple;
            obj1 = new List<object>();
            obj1.Add(new Vector3(MapVector.X, MapVector.Y - 1, MapVector.Z));
            obj1.Add(new Vector3(MapVector.X, MapVector.Y, MapVector.Z));
            AC = new ActionCall(Game.Actions.ActionType.Collision, typeof(Maps.Map), "WarpMap", obj1);
            Warp.actionCall.Add(AC);
            Warp.LightSourceDistance = 75f;
            Sprite2d.Add(Warp);

            WarpLocation = Util.Global.GetRandomInt(10, Util.Global.SizeMap - 10);
            Pos = Sprite2d.Where(x => x.name == WarpLocation.ToString() + ":" + Util.Global.SizeMap.ToString()).FirstOrDefault();
            Sprite2d.RemoveAll(x => x.name == WarpLocation.ToString() + ":" + Util.Global.SizeMap.ToString());
            Warp = new Objects.Sprite2d("warp", "", true, Pos.Position - new Vector2(5, 5), new Vector2(size + 15, sizey + 15), 5, Objects.Base.ControlType.None);
            Warp.name = "East";
            Warp.orderNum = 101;
            Warp.effectType = Objects.Base.EffectType.Ripple;
            obj1 = new List<object>();
            obj1.Add(new Vector3(MapVector.X, MapVector.Y + 1, MapVector.Z));
            obj1.Add(new Vector3(MapVector.X, MapVector.Y, MapVector.Z));
            AC = new ActionCall(Game.Actions.ActionType.Collision, typeof(Maps.Map), "WarpMap", obj1);
            Warp.actionCall.Add(AC);
            Warp.LightSourceDistance = 75f;
            Sprite2d.Add(Warp);


        }

        private void AddRoads(Vector3 mapVector, Items.Item.ItemType BaseType)
        {
            List<Vector2> Path = new List<Vector2>();
            Objects.Sprite2d Pos1 = new Objects.Sprite2d();
            Objects.Sprite2d Pos2 = new Objects.Sprite2d();
            Pos1 = Sprite2d.Where(x => x.name == "North").FirstOrDefault();
            Pos2 = Sprite2d.Where(x => x.name == "South").FirstOrDefault();
            Path = Actions.Anim.GetMovement(Pos1.Position + new Vector2(0, 35), Pos2.Position - new Vector2(-32, 35), Util.Global.SizeMap);

            Pos1 = Sprite2d.Where(x => x.name == "West").FirstOrDefault();
            Pos2 = Sprite2d.Where(x => x.name == "East").FirstOrDefault();
            Path.AddRange(Actions.Anim.GetMovement(Pos1.Position + new Vector2(35, 35), Pos2.Position - new Vector2(35, -30), Util.Global.SizeMap));

            List<Objects.Sprite2d> Sp = new List<Objects.Sprite2d>();
            Sp.AddRange(Sprite2d);
            foreach (Objects.Sprite2d S in Sp)
            {
                foreach (Vector2 V in Path)
                {
                    if (Util.Global.GetRandomInt(1, 5) > 2)
                    {
                        if (S.Item.Type == BaseType && Util.Base.collision(S, new Rectangle((int)V.X, (int)V.Y, 35, 35)))
                        {
                            if (Util.Global.GetRandomInt(1, 7) > 2)
                            {
                                Sprite2d.Where(x => x == S).FirstOrDefault().modelname = "path";
                            }
                            else
                            {
                                Sprite2d.Where(x => x == S).FirstOrDefault().modelname = "gravel0";
                            }
                            Sprite2d.Where(x => x == S).FirstOrDefault().orderNum = 1;
                            Sprite2d.Where(x => x == S).FirstOrDefault().clipping = false;
                            Sprite2d.Where(x => x == S).FirstOrDefault().effectType = Objects.Base.EffectType.None;
                        }
                    }
                }
            }
        }

        private void AddProps(Vector3 mapVector, Items.Item.ItemType BaseType)
        {
            List<Tuple<Items.Item.ItemType, float>> DropList = new List<Tuple<Items.Item.ItemType, float>>();
            DropList.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.Bush, .01F));
            DropList.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.Bush2, .005F));
            DropList.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.Flower, .01F));
            DropList.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.Flower2, .01F));
            DropList.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.RockSmall, .01F));
            List<Objects.Sprite2d> Sp = new List<Objects.Sprite2d>();
            Sp.AddRange(Sprite2d);
            foreach (Objects.Sprite2d S in Sp.Where(x => x.Item.Type == BaseType))
            {
                bool DropAdded = false;
                foreach (Tuple<Items.Item.ItemType, float> PL in DropList)
                {
                    if (!DropAdded)
                    {
                        double chance = Util.Global.GetRandomDouble();
                        if (chance < PL.Item2)
                        {
                            Objects.Sprite2d TileAdd = Items.Item.GetItemByType(PL.Item1, S.Position);
                            if (Sprite2d.Where(l => Util.Base.collision(l, S) == true).ToList().Count == 1)
                            {
                                Sprite2d.Add(TileAdd);
                            }
                            DropAdded = true;
                        }
                    }
                }
            }
        }

        private void AddDrops(Vector3 mapVector, Game.Maps.MapData.Biome bio, Items.Item.ItemType BaseType)
        {
            List<MapData.MapItems> MIs = MapData.GetMapItemsByBiome(mapVector,bio);

            List<Objects.Sprite2d> Sp = new List<Objects.Sprite2d>();
            Sp.AddRange(Sprite2d);
            foreach (Objects.Sprite2d S in Sp.Where(x => x.Item.Type == BaseType))
            {
                bool DropAdded = false;
                foreach (MapData.MapItems MI in MIs)
                {
                    if (!DropAdded)
                    {
                        double chance = Util.Global.GetRandomDouble();
                        if (chance < MI.chance)
                        {
                            if (
                                    (MI.itemType == Items.Item.ItemType.WarpPad && HasWarp == true) 
                                 || (MI.itemType == Items.Item.ItemType.CaveEntrance && HasCave == true)
                               )
                            {
                                Util.Base.Log("Tried to add too many of the same mappart");
                            }
                            else
                            {
                                Objects.Sprite2d TileAdd = Items.Item.GetItemByType(MI.itemType, S.Position);
                                if (Sprite2d.Where(l => Util.Base.collision(l, S) == true).ToList().Count == 1)
                                {
                                    Sprite2d.Add(TileAdd);
                                    if (MI.itemType == Items.Item.ItemType.WarpPad)
                                        HasWarp = true;
                                    if (MI.itemType == Items.Item.ItemType.CaveEntrance)
                                        HasCave = true;
                                }
                                DropAdded = true;
                            }
                        }
                    }
                }
            }
        }

        [Obsolete("Old&busted")]
        public void AddEnemyOLD(Vector3 mapVector)
        {
            int MaxEnemyCount = 8;
            for (int i = 0; i < MaxEnemyCount; i++)
            {
                Vector3 Distance = Vector3.Subtract(new Vector3(50, 50, 0), mapVector);
                int EnemyLevel = (int)Math.Abs(Distance.X) + (int)Math.Abs(Distance.Y);
                int x = Util.Global.GetRandomInt(3, Util.Global.SizeMap - 5);
                int y = Util.Global.GetRandomInt(3, Util.Global.SizeMap - 5);
                Vector2 Pos = Sprite2d.Where(z => z.name == x.ToString() + ":" + y.ToString()).FirstOrDefault().Position;
                Objects.Sprite2d En = Actions.Enemy.GetEnemyByLevel(Util.Global.GetRandomInt(0,EnemyLevel), Pos);
                En.name = i.ToString() + "|" + x.ToString() + ":" + y.ToString();
                Sprite2d.Add(En);
                Util.Base.Log("MAP Add Enemy:" + En.ID.ToString() + " | " + Pos.X.ToString() + "-" + Pos.Y.ToString());
            }
        }

        public void AddEnemy(Vector3 mapVector)
        {
            int MaxEnemyCount = 8;
            for (int i = 0; i < MaxEnemyCount; i++)
            {
                Vector3 Distance = Vector3.Subtract(new Vector3(50, 50, 0), mapVector);
                int EnemyLevel = (int)Math.Abs(Distance.X) + (int)Math.Abs(Distance.Y);

                bool SpawnLoc = true;
                int x = 0;
                int y = 0;
                while (SpawnLoc)
                {
                    x = Util.Global.GetRandomInt(3, Util.Global.SizeMap - 8);
                    y = Util.Global.GetRandomInt(3, Util.Global.SizeMap - 8);
                    Objects.Sprite2d Tile = this.Sprite2d.Where(z => z.name == x.ToString() + ":" + y.ToString()).FirstOrDefault();
                    if (Tile!=null && Tile.modelname == "grass0")
                    {
                        SpawnLoc = false;
                    }
                }
                Vector2 Pos = this.Sprite2d.Where(z => z.name == x.ToString() + ":" + y.ToString()).FirstOrDefault().Position;
                Objects.Sprite2d En = Actions.Enemy.GetEnemyByLevel(Util.Global.GetRandomInt(0, EnemyLevel), Pos);
                En.name = i.ToString() + "|" + x.ToString() + ":" + y.ToString();
                this.Sprite2d.Add(En);
                Util.Base.Log("Spawn Enemy:" + En.ID.ToString() + " | " + Pos.X.ToString() + "-" + Pos.Y.ToString());
            }
        }

        private void AddMapParts(Vector3 mapVector, Items.Item.ItemType BaseType, Game.Maps.MapData.Biome Bio)
        {
            int MaxCount = Util.Global.GetRandomInt(5,12);
            for (int i = 1; i < MaxCount+1; i++)
            {
                //MapPart Mp = GetRandomMapPart();
                Game.Maps.MapData.MapPart Mp = MapData.GetRandomMapPartsByBiome(mapVector, Bio);
                if (HasTown)
                {
                        while (Mp == MapData.MapPart.town)
                        {
                            Mp = MapData.GetRandomMapPartsByBiome(mapVector, Bio);
                        }
                }
                if (Mp == MapData.MapPart.town)
                { HasTown = true; }

                Items.Item.ItemType[, ,] Map = MapData.GetMapByPart(Mp);
                AddMapPart(Mp, Util.Global.GetRandomInt(3, Util.Global.SizeMap - (Map.GetLength(1) + 3)), Util.Global.GetRandomInt(3, Util.Global.SizeMap - (Map.GetLength(2) + 3)), BaseType);
            }
        }

        public void AddMapPart(Game.Maps.MapData.MapPart MP, int StartX, int StartY, Items.Item.ItemType BaseType)
        {
            Util.Base.Log("AddMapPart:"+MP.ToString() + "|" + StartX.ToString() + ":" + StartY.ToString());

            int tilex = StartX;
            int tiley = StartY;
            Items.Item.ItemType[, ,] Map = MapData.GetMapByPart(MP);
            int columncount = 0;

            foreach (Items.Item.ItemType TT in Map)
            {
                string TileName = string.Format("{0}:{1}", tilex.ToString(), tiley.ToString());
                Objects.Sprite2d OriTile = Sprite2d.Where(x => x.name == TileName).FirstOrDefault();

                if ((OriTile != null && Sprite2d.Where(x => x.name == TileName).FirstOrDefault().Item.Type == BaseType) || MP == MapData.MapPart.caveEntrance || MP == MapData.MapPart.start)
                {
                    Sprite2d.RemoveAll(x => x.name == OriTile.name);

                    //if (MP == MapData.MapPart.caveEntrance)
                    //{
                    //    Sprite2d.RemoveAll(x => x.name == OriTile.name);
                    //}
                    //else
                    //{
                    //    Sprite2d.RemoveAll(x => x.ID == OriTile.ID);
                    //}

                    Objects.Sprite2d NewTile = Items.Item.GetItemByType(TT, OriTile.Position);
                    NewTile.name = TileName;

                    if (NewTile.Item.BackGroundType != null)
                    {
                        Objects.Sprite2d BG = Items.Item.GetItemByType((Items.Item.ItemType)NewTile.Item.BackGroundType, OriTile.Position);
                        BG.name = TileName;
                        BG.orderNum = 1;
                        Sprite2d.Add(BG);
                        //Util.Base.Log("AddTileBG:" + BG.modelname + ":" + BG.name);
                    }
                    Sprite2d.Add(NewTile);
                    //Util.Base.Log("AddTile:" + NewTile.modelname + ":" + NewTile.name);

                }
                columncount++;
                tiley++;
                if (columncount == Map.GetLength(2))
                {
                    columncount = 0;
                    tiley = StartY;
                    tilex++;
                }
            }
            Util.Global.MainMap[(int)MapVector.X, (int)MapVector.Y, (int)MapVector.Z] = this;
        }

        #endregion

        #region MapNavigation

        public void WarpMap(Vector3 mapVector, Vector3 FromMapVector)
        {
            int warp = 65;
            LoadMapByVector(mapVector);
            try
            {
                if (FromMapVector.X > mapVector.X)
                {
                    //N->S
                    Util.Global.Hero.Position = new Vector2(
                        (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Where(x => x.name == "North").FirstOrDefault().Position.X),
                        (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Where(x => x.name == "North").FirstOrDefault().Position.Y + warp)
                        );

                }
                else if (FromMapVector.X < mapVector.X)
                {
                    //S->N
                    Util.Global.Hero.Position = new Vector2(
                        (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Where(x => x.name == "South").FirstOrDefault().Position.X),
                        (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Where(x => x.name == "South").FirstOrDefault().Position.Y - warp)
                        );
                }
                else if (FromMapVector.Y > mapVector.Y)
                {
                    //E->W
                    Util.Global.Hero.Position = new Vector2(
                         (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Where(x => x.name == "East").FirstOrDefault().Position.X - warp),
                         (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Where(x => x.name == "East").FirstOrDefault().Position.Y)
                         );
                }
                else if (FromMapVector.Y < mapVector.Y)
                {
                    //W->E
                    Util.Global.Hero.Position = new Vector2(
                         (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Where(x => x.name == "West").FirstOrDefault().Position.X + warp),
                         (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Where(x => x.name == "West").FirstOrDefault().Position.Y + warp)
                         );
                }
            }
            catch(Exception ex)
            {
                Util.Base.Log("default hero position set to previous map: "+ex.Message);
                Util.Global.Hero.Position = Util.Global.PreviousMapPosition;
            }

            if (Util.Global.Hero.ClipCheck())
            {
                Util.Base.Log("Warped Hero to Clip!");
                //We got a Problem!
            }


            foreach (Objects.Sprite2d S in Util.Global.Pets)
            {
                Objects.Sprite2d X = Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault();
                if (X != null)
                {
                    Util.Global.Sprites.Where(x => x.ID == X.ID).FirstOrDefault().Position = Util.Global.Hero.Position;
                }
            }
            Season.CheckWeather();
        }

        public void LoadMapByVector(Vector3 mapVector)
        {
            Util.Global.PreviousMap = Util.Global.CurrentMap;
            Util.Global.PreviousMapPosition = Util.Global.Hero.Position;
            Util.Global.Sprites.RemoveAll(x=>x.name.Contains("SpeechSAY"));
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("SpeechSAYTEXT"));

            List<Objects.Sprite2d> Items = new List<Objects.Sprite2d>();
            Items.AddRange(Util.Global.Sprites.Where(x => x.Item != null && x.Item.State != Game.Items.Item.ItemState.Null));

            List<Objects.Sprite2d> Pets = new List<Objects.Sprite2d>();
            Pets.AddRange(Util.Global.Pets);

            new Objects.Maneuver().RemoveAll();
            Menu.ActorStats.RemoveStats(Util.Global.Hero);
            Menu.Inventory.HideInventory();
            Menu.Craft.HideCraft();
            Util.Global.Sprites.RemoveAll(x => x.Item != null && x.Item.State != Game.Items.Item.ItemState.Null);
            foreach (Objects.Sprite2d S in Util.Global.Pets)
            {
                Util.Global.Sprites.RemoveAll(x => x.ID==S.ID);
            }
            
            Util.Global.Sprites.RemoveAll(x => x.ID == Util.Global.Hero.ID);
            List<Objects.Sprite2d> CurrentState = Util.Global.Sprites;

            if (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z] == null)
            {
                GenerateMap(mapVector, Game.Maps.MapData.Biome.Grass, Util.Global.SizeMap, Util.Global.SizeMap);
            }

            Util.Global.Sprites = new List<Objects.Sprite2d>();
            Util.Global.Sprites.AddRange(Items);
            Util.Global.Sprites.AddRange(Pets);

            foreach (Objects.Sprite2d S in Util.Global.Pets)
            {
                Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Position = Util.Global.Hero.Position;
            }

            Util.Global.Sprites.AddRange(Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d);
            Util.Global.Sprites.Add(Util.Global.Hero);
            Menu.HUD.MainStat();

            Objects.Sprite2d InHand = Items.Where(x => x.Item.State == Game.Items.Item.ItemState.Hand).FirstOrDefault();
            if (InHand != null)
            {
                Util.Base.Log(InHand.controlType.ToString());
                Util.Base.Log(InHand.Position.ToString());
                Util.Global.Sprites.Where(x => x.ID == InHand.ID).FirstOrDefault().Position = Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.Default);
            }

            Util.Global.FullScreenSize = new Vector2(Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Max(x => x.Position.X), Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Max(x => x.Position.Y));
            Util.Base.Log("ScreenSize:" + Util.Global.FullScreenSize.X.ToString() + "," + Util.Global.FullScreenSize.Y.ToString());

            Util.Global.MainMap[(int)Util.Global.CurrentMap.X, (int)Util.Global.CurrentMap.Y, (int)Util.Global.CurrentMap.Z].Sprite2d = CurrentState;
            Util.Global.CurrentMap = mapVector;
        }

        public void WarpMapPosition(Vector3 mapVector, Vector2 Position)
        {
            Menu.Warp.HideWarp();
            if (mapVector != Util.Global.CurrentMap)
            {
                LoadMapByVector(mapVector);
            }
            Util.Global.Sprites.Where(x => x.ID == Util.Global.Hero.ID).FirstOrDefault().Position = Position;
        }

        public void ReturnToPreviousWarp()
        {
            Util.Global.Hero.Position = Vector2.Subtract(Util.Global.PreviousMapPosition, new Vector2(32, 32));
            foreach (Objects.Sprite2d S in Util.Global.Pets)
            {
                Util.Global.Sprites.Where(X => X.ID == S.ID).FirstOrDefault().Position = Vector2.Subtract(Util.Global.PreviousMapPosition, new Vector2(10, 10));
            }

            LoadMapByVector(Util.Global.PreviousMap);
        }

        public void LoadMap()
        {
            Vector3 V = new Vector3();
            V.X = MapVector.X;
            V.Y = MapVector.Y;
            V.Z = MapVector.Z;
            LoadMapByVector(V);
        }

        public void LoadMapByInt(int X, int Y, int Z)
        {
            Vector3 V = new Vector3();
            V.X = X;
            V.Y = Y;
            V.Z = Z;
            LoadMapByVector(V);
        }

        #endregion



    }
}

//public void GenerateBaseMap(Vector3 mapVector, Items.Item.ItemType BaseType)
//{
//    AddInitialBase(mapVector, Util.Global.SizeMap, Util.Global.SizeMap, BaseType);
//    if (mapVector == new Vector3(50, 50, 0))
//    {
//        AddMapPart(Maps.MapPart.start, 45, 45, BaseType);
//    }

//    AddWarps(mapVector);
//    AddMapParts(BaseType);
//    AddRoads(mapVector, BaseType);
//    //AddMapPart(MapPart.test, 10, 10);
//    AddBorder(mapVector, Util.Global.SizeMap, Util.Global.SizeMap);
//    AddDrops(mapVector, BaseType);
//    AddProps(mapVector, BaseType);
//    AddEnemy(mapVector);

//    this.Sprite2d = Sprite2d;
//    Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z] = this;
//}

//public void GenerateMiniMap(Vector3 mapVector, int MapSizeH, int MapSizeW)
//{
//    AddInitialBase(mapVector, MapSizeH, MapSizeW, Items.Item.ItemType.Grass);
//    AddBorder(mapVector, MapSizeH, MapSizeW);
//    AddTorches(mapVector, MapSizeH, MapSizeW);
//    AddProps(mapVector, Items.Item.ItemType.Grass);

//    this.Sprite2d = Sprite2d;
//    Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z] = this;
//}


//for (int i = 1; i <= 15; i++)
//{
//    Objects.Sprite2d gold = new Items.Gold().GetGoldObject(Content);
//    gold.x = i * 100; gold.y = i * 100; S.AddSprite2d(gold);
//}

//S.AddSprite2d(new Actions.Enemy().GetEnemy(Content, 1, 5, 750, 750).Sprite);
//S.AddSprite2d(new Actions.Enemy().GetEnemy(Content, 2, 7, 850, 750).Sprite);
//S.AddSprite2d(new Actions.Enemy().GetEnemy(Content, 3, 10, 950, 750).Sprite);
//S.AddSprite2d(new Actions.Enemy().GetEnemy(Content, 4, 10, 1050, 750).Sprite);



//S.AddSprite2d(new Items.Sapling().GetSaplingItem(Content, 200, 300));
//S.AddSprite2d(new Actions.Pet().GetPet(Content,10,10,100,100).Sprite);


//Sprites.Add(new Objects.Sprite2d(Content.Load<Texture2D>("Images/triangle"), "Obstical", false, 100, 100, 100, screenSizeHeight, screenSizeWidth));
//Sprites.Add(new Objects.Sprite2d(Content.Load<Texture2D>("Images/bluetile"), "Tile", true, 100, 100, 5,true));
//SpritesAnim.Add(new Objects.AnimSprite(Content.Load<Texture2D>("Slides/dice"),"Dice", true, 6, 1, new Vector2(250, 250)));
//SpritesAnim.Add(new Objects.AnimSprite(Content.Load<Texture2D>("Slides/walk2"), "Walker", true,6,5,250,250));
//Texts.Add(new Objects.Text("", "TileText", true, new Vector2(1, 1), Color.Red, screenSizeHeight, screenSizeWidth));
//Texts.Add(new Objects.Text("", "GameTime", true, new Vector2(1, 1), Color.Red, screenSizeHeight, screenSizeWidth));
//RD = new Actions.RollDice(Content.Load<Texture2D>("Slides/dice"), "Dice", false, 6, 1, new Vector2(250, 250));
//public void AddSpriteToMap(Objects.Sprite2d sprite)
//{
//    Util.Global.MainMap[(int)MapVector.X, (int)MapVector.Y, (int)MapVector.Z].Sprite2d.Add(sprite);
//}
//public void LoadMapByMapPart(MapPart MP)
//{
//    Vector3 mapVector = new Vector3(99, 99, 98);
//    GenerateMapPartMap(mapVector,MP);
//    LoadMapByVector(mapVector);
//}

//public void GenerateMapPartMap(Vector3 mapVector, MapPart MP)
//{
//    Items.Item.ItemType[, ,] Map = GetMapByPart(MP);
//    int FightUtil.Global.SizeMap1 = Map.GetLength(0) + 1;
//    int FightUtil.Global.SizeMap = Map.GetLength(2) + 1;
//    AddInitialBase(mapVector, FightUtil.Global.SizeMap1, FightUtil.Global.SizeMap, Items.Item.ItemType.Grass);
//    AddMapPart(MP, 1, 1, Items.Item.ItemType.Grass);

//    this.Sprite2d = Sprite2d;
//    Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z] = this;
//}