using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Actions;
using Microsoft.Xna.Framework;

namespace Game.Maps
{
    public enum TileType { silver, gold, iron, grass, wall, door, floor, water, torch, tree, rock, dirt, fountain, warp, pad, grain, gravel, grass_s, grass_n, grass_se, grass_sw, grass_ne, grass_nw, grass_e, grass_w, gravel_s, gravel_n, gravel_se, gravel_sw, gravel_ne, gravel_nw, gravel_e, gravel_w, grass_ne2, grass_nw2, grass_se2, grass_sw2};
    public enum MapPart { room, stream, courtyard, start, forrest, river, mountain, ironvein }

    public class Map
    {
        public List<Objects.Sprite2d> Sprite2d { get; set; }
        public List<Objects.AnimSprite> AnimSprite { get; set; }
        public Vector3 MapVector { get; set; }
        private const int size = 32;
        private const int sizey = 32;
        private const int sizeMap = 100;

        public void GenerateMapPartMap(MapPart MP)
        {
            TileType[, ,] Map = GetMapByPart(MP);
            int tileW = 0;
            int tileH = 0;
            int tilenum = 1;
            int FightsizeMap1 = Map.GetLength(0) + 1;
            int FightsizeMap = Map.GetLength(2)+1;
            Sprite2d = new List<Objects.Sprite2d>();
            AnimSprite = new List<Objects.AnimSprite>();
            Tuple<int, int> Loc = new Tuple<int, int>(0, 0);
            for (int i = Loc.Item1; i <= FightsizeMap1; i++)
            {
                for (int y = Loc.Item2; y <= FightsizeMap; y++)
                {
                    if (i == Loc.Item1 || i == FightsizeMap1 || y == Loc.Item2 || y == FightsizeMap)
                    {
                        Sprite2d.Add(new Objects.Sprite2d(new Maps.Asset().getContentByName("gravel"), i.ToString() + ":" + y.ToString(), true, new Vector2(tileH, tileW), new Vector2(size, sizey), 5, Objects.Base.ControlType.None));
                        //Util.Global.Sprites.Where(x => x.name == i.ToString() + ":" + y.ToString()).FirstOrDefault().clipping = true;
                        Objects.Sprite2d rock = new Objects.Sprite2d(new Maps.Asset().getContentByName("rockprop"), "rock" + i.ToString() + ":" + y.ToString(), true, new Vector2(tileH, tileW), new Vector2(size, sizey), 5, Objects.Base.ControlType.None);
                        rock.clipping = true;
                        Sprite2d.Add(rock);
                    }
                    else
                    {
                        Sprite2d.Add(new Objects.Sprite2d(new Maps.Asset().getContentByName("grass0"), i.ToString() + ":" + y.ToString(), true, new Vector2(tileH, tileW), new Vector2(size, sizey), 5, Objects.Base.ControlType.None));
                        int propChance = Util.Global.GetRandomInt(0, 100);
                        if (propChance < 10)
                            Sprite2d.Add(new Objects.Sprite2d(new Maps.Asset().getContentByName("bush"), "bushprop" + i.ToString() + ":" + y.ToString(), true, new Vector2(tileH, tileW), new Vector2(size, sizey), 5, Objects.Base.ControlType.None));
                        else if (propChance == 11)
                            Sprite2d.Add(new Objects.Sprite2d(new Maps.Asset().getContentByName("flowerprop"), "flowerprop" + i.ToString() + ":" + y.ToString(), true, new Vector2(tileH, tileW), new Vector2(size, sizey), 5, Objects.Base.ControlType.None));
                        else if (propChance == 12)
                            Sprite2d.Add(new Objects.Sprite2d(new Maps.Asset().getContentByName("flowerprop2"), "flowerprop" + i.ToString() + ":" + y.ToString(), true, new Vector2(tileH, tileW), new Vector2(size, sizey), 5, Objects.Base.ControlType.None));
                        else if (propChance == 66)
                            Sprite2d.Add(new Objects.Sprite2d(new Maps.Asset().getContentByName("rockprop"), "rockprop" + i.ToString() + ":" + y.ToString(), true, new Vector2(tileH, tileW), new Vector2(size, sizey), 5, Objects.Base.ControlType.None));

                    }
                    tileH = tileH + size;
                    tilenum++;
                }
                tileH = 1;
                tileW = tileW + sizey;
            }

            AddMapPart(MP, 1, 1);
            
            Sprite2d.Select(c => { c.allowScroll = true; return c; }).ToList();
            Sprite2d.Where(x => x.orderNum == 0).Select(c => { c.orderNum = 1; return c; }).ToList();

            this.Sprite2d = Sprite2d;
            Util.Global.MainMap[99, 99, 98] = this;

        }

        public void GenerateFightMap()
        {
            int tileW = 0;
            int tileH = 0;
            int tilenum = 1;
            int FightsizeMap = 12;
            int FightsizeMapW = 15;
            List<Objects.Sprite2d> FightSprite2d = new List<Objects.Sprite2d>();
            AnimSprite = new List<Objects.AnimSprite>();
            Tuple<int, int> Loc = new Tuple<int, int>(0, 0);
            for (int i = Loc.Item1; i <= FightsizeMap; i++)
            {
                for (int y = Loc.Item2; y <= FightsizeMapW; y++)
                {
                    if (i == Loc.Item1 || i == FightsizeMap || y == Loc.Item2 || y == FightsizeMapW)
                    {
                        FightSprite2d.Add(new Objects.Sprite2d(new Maps.Asset().getContentByName("gravel"), i.ToString() + ":" + y.ToString(), true, new Vector2(tileH, tileW), new Vector2(size, sizey), 5, Objects.Base.ControlType.None));
                        //Util.Global.Sprites.Where(x => x.name == i.ToString() + ":" + y.ToString()).FirstOrDefault().clipping = true;
                        Objects.Sprite2d rock = new Objects.Sprite2d(new Maps.Asset().getContentByName("rockprop"), "rock" + i.ToString() + ":" + y.ToString(), true, new Vector2(tileH, tileW), new Vector2(size, sizey), 5, Objects.Base.ControlType.None);
                        rock.clipping = true;
                        FightSprite2d.Add(rock);
                    }
                    else
                    {
                        FightSprite2d.Add(new Objects.Sprite2d(new Maps.Asset().getContentByName("grass0"), i.ToString() + ":" + y.ToString(), true, new Vector2(tileH, tileW), new Vector2(size, sizey), 5, Objects.Base.ControlType.None));
                        int propChance = Util.Global.GetRandomInt(0, 100);
                        if (propChance < 10)
                            FightSprite2d.Add(new Objects.Sprite2d(new Maps.Asset().getContentByName("bush"), "bushprop" + i.ToString() + ":" + y.ToString(), true, new Vector2(tileH, tileW), new Vector2(size, sizey), 5, Objects.Base.ControlType.None));
                        else if (propChance == 11)
                            FightSprite2d.Add(new Objects.Sprite2d(new Maps.Asset().getContentByName("flowerprop"), "flowerprop" + i.ToString() + ":" + y.ToString(), true, new Vector2(tileH, tileW), new Vector2(size, sizey), 5, Objects.Base.ControlType.None));
                        else if (propChance == 12)
                            FightSprite2d.Add(new Objects.Sprite2d(new Maps.Asset().getContentByName("flowerprop2"), "flowerprop" + i.ToString() + ":" + y.ToString(), true, new Vector2(tileH, tileW), new Vector2(size, sizey), 5, Objects.Base.ControlType.None));
                        else if (propChance == 66)
                            FightSprite2d.Add(new Objects.Sprite2d(new Maps.Asset().getContentByName("rockprop"), "rockprop" + i.ToString() + ":" + y.ToString(), true, new Vector2(tileH, tileW), new Vector2(size, sizey), 5, Objects.Base.ControlType.None));

                    }
                    tileH = tileH + size;
                    tilenum++;
                }
                tileH = 1;
                tileW = tileW + sizey;
            }
            FightSprite2d.Select(c => { c.allowScroll = true; return c; }).ToList();
            FightSprite2d.Where(x => x.orderNum == 0).Select(c => { c.orderNum = 1; return c; }).ToList();

            string[] torches = new string[4] { "0:0", FightsizeMap.ToString() + ":0", "0:" + FightsizeMapW.ToString(), FightsizeMap.ToString() + ":" + FightsizeMapW.ToString() };
            foreach (string TileName in torches)
            {
                Objects.Sprite2d TorchSprite = Items.Item.GetItemByType(Items.Item.ItemType.Torch, (FightSprite2d.Where(x => x.name == TileName).FirstOrDefault().Position));
                TorchSprite.LightSourceDistance = 300f;
                TorchSprite.Item = null;
                TorchSprite.SpriteType = Objects.Base.Type.Tile;
                FightSprite2d.Add(TorchSprite);
                FightSprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("floor2");
            }

            this.Sprite2d = FightSprite2d;
            Util.Global.MainMap[99, 99, 99] = this;
        }

        public void GenerateBaseMap(Vector3 mapVector)
        {
            int tileW = 0;
            int tileH = 0;
            MapVector = mapVector;
            Tuple<int, int> Loc = new Tuple<int, int>(0, 0);
            Sprite2d = new List<Objects.Sprite2d>();
            AnimSprite = new List<Objects.AnimSprite>();
            for (int i = Loc.Item1; i <= sizeMap; i++)
            {
                for (int y = Loc.Item2; y <= sizeMap; y++)
                {
                    Objects.Sprite2d BaseSprite = new Objects.Sprite2d(new Maps.Asset().getContentByName("grass0"), i.ToString() + ":" + y.ToString(), true, new Vector2(tileH, tileW), new Vector2(size, sizey), 5, Objects.Base.ControlType.None);
                    Sprite2d.Add(BaseSprite);
                    tileH = tileH + size;
                }
                tileH = 1;
                tileW = tileW + sizey;
            }
            AddProps(mapVector);
            AddDrops(mapVector);
            AddWarps(mapVector);
            AddRoads(mapVector);
            AddMapParts();
            AddBorder(mapVector);
            AddEnemy(mapVector);
            AddSpriteToMap(Items.Item.GetItemByType(Items.Item.ItemType.Stove,new Vector2(300,300)));
            Util.Global.MainMap[(int)MapVector.X, (int)MapVector.Y, (int)MapVector.Z] = this;
        }

        public void AddProps(Vector3 mapVector)
        {
            List<Tuple<string, float>> PropList = new List<Tuple<string, float>>();
            PropList.Add(new Tuple<string, float>("bush", .01F));
            PropList.Add(new Tuple<string, float>("flowerprop", .01F));
            PropList.Add(new Tuple<string, float>("flowerprop2", .01F));
            PropList.Add(new Tuple<string, float>("rockprop", .01F));
            PropList.Add(new Tuple<string, float>("tree2", .01F));
            
            List<Objects.Sprite2d> Sp = new List<Objects.Sprite2d>();
            Sp.AddRange(Sprite2d);
            foreach (Objects.Sprite2d S in Sp)
            {
                bool PropAdded = false;
                foreach (Tuple<string, float> PL in PropList)
                {
                    if (!PropAdded)
                    {
                        double chance = Util.Global.GetRandomDouble();
                        if (chance < PL.Item2)
                        {
                            Sprite2d.Add(new Objects.Sprite2d(new Maps.Asset().getContentByName(PL.Item1), PL.Item1 + "prop" + S.name, true, S.Position, new Vector2(size, sizey), 5, Objects.Base.ControlType.None));
                            PropAdded = true;
                        }
                    }
                }
            }
        }

        public void AddDrops(Vector3 mapVector)
        {
            List<Tuple<Items.Item.ItemType, float>> DropList = new List<Tuple<Items.Item.ItemType, float>>();
            DropList.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.Coin, .0009F));
            DropList.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.Health, .0009F));
            DropList.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.Fountain, .0009F));
            DropList.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.IronOre, .0006F));
            DropList.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.SilverOre, .0004F));
            DropList.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.GoldOre, .0002F));
            DropList.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.Grain, .0003F));
            List<Objects.Sprite2d> Sp = new List<Objects.Sprite2d>();
            Sp.AddRange(Sprite2d);
            foreach (Objects.Sprite2d S in Sp)
            {
                bool DropAdded = false;
                foreach (Tuple<Items.Item.ItemType, float> PL in DropList)
                {
                    if (!DropAdded)
                    {
                        double chance = Util.Global.GetRandomDouble();
                        if (chance < PL.Item2)
                        {
                            Sprite2d.Add(Items.Item.GetItemByType(PL.Item1, S.Position));
                            DropAdded = true;
                        }
                    }
                }
            }
        }

        public void AddBorder(Vector3 mapVector)
        {
            List<Objects.Sprite2d> Sp = new List<Objects.Sprite2d>();
            Sp.AddRange(Sprite2d);
            foreach (Objects.Sprite2d S in Sp)
            {
                if (S.name != "North" && S.name != "South" && S.name != "West" && S.name != "East")
                {
                    int len = sizeMap * size;
                    if (
                            Util.Base.collision(S, new Rectangle(0, 0, len, 25)) || //Top
                            Util.Base.collision(S, new Rectangle(0, 0, 25, len)) || //Left
                            Util.Base.collision(S, new Rectangle(len+25, 0, 0, len)) || //Right
                            Util.Base.collision(S, new Rectangle(0, len, len+25, 25)) //Bottom
                        )
                    {
                  
                            Sprite2d.RemoveAll(x => x.Position == S.Position);
                            Sprite2d.Add(new Objects.Sprite2d(new Maps.Asset().getContentByName("gravel"), S.name, true, S.Position, new Vector2(size, sizey), 5, Objects.Base.ControlType.None));
                            Objects.Sprite2d rock = new Objects.Sprite2d(new Maps.Asset().getContentByName("rockprop"), "rock" + S.name, true, S.Position, new Vector2(size, sizey), 5, Objects.Base.ControlType.None);
                            rock.clipping = true;
                            Sprite2d.Add(rock);
                    }
                }
            }
        }

        public void AddEnemy(Vector3 mapVector)
        {
            int MaxEnemyCount = 7;
            for (int i = 0; i < MaxEnemyCount; i++)
            {
                Vector3 Distance = Vector3.Subtract(new Vector3(50, 50, 0), mapVector);
                int EnemyLevel = (int)Math.Abs(Distance.X) + (int)Math.Abs(Distance.Y) + 1;
                Sprite2d.Add(Actors.Enemy.GetEnemy(EnemyLevel, 5 * EnemyLevel, Util.Global.GetRandomInt(10, sizeMap - 10)*32, Util.Global.GetRandomInt(10, sizeMap - 10)*32));
            }
        }

        public void AddWarps(Vector3 mapVector)
        {
            int WarpLocation;
            Objects.Sprite2d Pos = new Objects.Sprite2d();

            WarpLocation = Util.Global.GetRandomInt(10, sizeMap - 10);
            Pos = Sprite2d.Where(x => x.name == "0:" + WarpLocation.ToString()).FirstOrDefault();
            Objects.Sprite2d Warp = new Objects.Sprite2d(new Maps.Asset().getContentByName("warp"), "", true, Pos.Position-new Vector2(5,5), new Vector2(size + 15, sizey + 15), 5, Objects.Base.ControlType.None);
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

            WarpLocation = Util.Global.GetRandomInt(10, sizeMap - 10);
            Pos = Sprite2d.Where(x => x.name == sizeMap.ToString() + ":" + WarpLocation.ToString()).FirstOrDefault();
            Warp = new Objects.Sprite2d(new Maps.Asset().getContentByName("warp"), "", true, Pos.Position - new Vector2(5, 5), new Vector2(size + 15, sizey + 15), 5, Objects.Base.ControlType.None);
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

            WarpLocation = Util.Global.GetRandomInt(10, sizeMap - 10);
            Pos = Sprite2d.Where(x => x.name == WarpLocation.ToString() + ":0").FirstOrDefault();
            Warp = new Objects.Sprite2d(new Maps.Asset().getContentByName("warp"), "", true, Pos.Position - new Vector2(5, 5), new Vector2(size + 15, sizey + 15), 5, Objects.Base.ControlType.None);
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

            WarpLocation = Util.Global.GetRandomInt(10, sizeMap - 10);
            Pos = Sprite2d.Where(x => x.name == WarpLocation.ToString() + ":" + sizeMap.ToString()).FirstOrDefault();
            Warp = new Objects.Sprite2d(new Maps.Asset().getContentByName("warp"), "", true, Pos.Position - new Vector2(5, 5), new Vector2(size + 15, sizey + 15), 5, Objects.Base.ControlType.None);
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

        public void AddRoads(Vector3 mapVector)
        {
            Objects.Sprite2d Pos1 = new Objects.Sprite2d();
            Objects.Sprite2d Pos2 = new Objects.Sprite2d();
            Pos1 = Sprite2d.Where(x => x.name == "North").FirstOrDefault();
            Pos2 = Sprite2d.Where(x => x.name == "South").FirstOrDefault();
            List<Vector2> Path = Actions.Anim.GetMovement(Pos1.Position + new Vector2(0, 35), Pos2.Position - new Vector2(-32, 35), sizeMap);

            Pos1 = Sprite2d.Where(x => x.name == "West").FirstOrDefault();
            Pos2 = Sprite2d.Where(x => x.name == "East").FirstOrDefault();
            Path.AddRange(Actions.Anim.GetMovement(Pos1.Position + new Vector2(35, 35), Pos2.Position - new Vector2(35, -30), sizeMap));

            List<Objects.Sprite2d> Sp = new List<Objects.Sprite2d>();
            Sp.AddRange(Sprite2d);
            foreach (Objects.Sprite2d S in Sp)
            {
                foreach (Vector2 V in Path)
                {
                    if (Util.Base.collision(S, new Rectangle((int)V.X,(int)V.Y,35,35)))
                    {
                        Sprite2d.Where(x => x == S).FirstOrDefault().model = new Maps.Asset().getContentByName("gravel");
                        Sprite2d.Where(x => x == S).FirstOrDefault().clipping = false;
                    }
                }
            }
        }

        public void AddMapParts()
        {
            int MaxCount = 2;
            for (int i = 0; i < MaxCount; i++)
            {
                AddMapPart(GetRandomMapPart(), Util.Global.GetRandomInt(10, sizeMap - 30), Util.Global.GetRandomInt(10, sizeMap - 30));
            }
        }

        public void Fill(Vector2 Start, Vector2 End)
        {

        }

        public void AddSpriteToMap(Objects.Sprite2d sprite)
        {
                Util.Global.MainMap[(int)MapVector.X, (int)MapVector.Y, (int)MapVector.Z].Sprite2d.Add(sprite);
        }

        public void AddMapPart(MapPart MP, int StartX, int StartY)
        {
            //try
            //{
                int tilex = StartX;
                int tiley = StartY;
                TileType[, ,] Map = GetMapByPart(MP);
                int columncount = 0;
                foreach (TileType TT in Map)
                {
                    string TileName = string.Format("{0}:{1}", tilex.ToString(), tiley.ToString());
                    if (Sprite2d.Where(x => x.name == TileName).FirstOrDefault() != null)
                    {
                        Sprite2d.Remove(Sprite2d.Where(x => x.name.Contains("prop" + TileName)).FirstOrDefault());
                        switch (TT)
                        {
                            case TileType.warp:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("warp");
                                Sprite2d.Add(Items.Warp.GetReturnWarp(Sprite2d.Where(x => x.name == TileName).FirstOrDefault().Position));
                                break;
                            case TileType.pad:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("warp");
                                Sprite2d.Add(Items.Warp.GetPad(Sprite2d.Where(x => x.name == TileName).FirstOrDefault().Position));
                                break;
                            case TileType.wall:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("wall");
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().clipping = true;
                                break;
                            case TileType.door:
                                Sprite2d.Add(Items.Item.GetItemByType(Items.Item.ItemType.Door, Sprite2d.Where(x => x.name == TileName).FirstOrDefault().Position));
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("floor2");
                                break;
                            case TileType.fountain:
                                Sprite2d.Add(Items.Item.GetItemByType(Items.Item.ItemType.Fountain, Sprite2d.Where(x => x.name == TileName).FirstOrDefault().Position));
                                break;
                            case TileType.torch:
                                Sprite2d.Add(Items.Item.GetItemByType(Items.Item.ItemType.Torch, Sprite2d.Where(x => x.name == TileName).FirstOrDefault().Position));
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("floor2");
                                break;
                            case TileType.floor:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("floor2");
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().clipping = false;
                                break;
                            case TileType.gravel:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("gravel");
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().clipping = false;
                                break;
                            case TileType.iron:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("IronOre");
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().clipping = false;
                                break;
                            case TileType.silver:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("SilverOre");
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().clipping = false;
                                break;
                            case TileType.gold:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("GoldOre");
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().clipping = false;
                                break;
                            case TileType.tree:
                                Objects.Sprite2d tree = new Objects.Sprite2d(new Maps.Asset().getContentByName("tree2"), "tree", true, Sprite2d.Where(x => x.name == TileName).FirstOrDefault().Position, new Vector2(size, sizey), 5, Objects.Base.ControlType.None);
                                tree.orderNum = 1;
                                Sprite2d.Add(tree);
                                break;
                            case TileType.grain:
                                 Sprite2d.Add(Items.Item.GetItemByType(Items.Item.ItemType.Grain, Sprite2d.Where(x => x.name == TileName).FirstOrDefault().Position));
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("grain");
                                break;
                            case TileType.water:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("water");
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().clipping = true;
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().effectType = Objects.Base.EffectType.Ripple;
                                break;
                            case TileType.grass_e:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("grass_e");
                                break;
                            case TileType.grass_n:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("grass_n");
                                break;
                            case TileType.grass_ne:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("grass_ne");
                                break;
                            case TileType.grass_nw:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("grass_nw");
                                break;
                            case TileType.grass_s:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("grass_s");
                                break;
                            case TileType.grass_se:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("grass_se");
                                break;
                            case TileType.grass_sw:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("grass_sw");
                                break;
                            case TileType.grass_w:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("grass_w");
                                break;
                            case TileType.gravel_e:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("gravel_e");
                                break;
                            case TileType.gravel_n:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("gravel_n");
                                break;
                            case TileType.gravel_ne:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("gravel_ne");
                                break;
                            case TileType.gravel_nw:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("gravel_nw");
                                break;
                            case TileType.gravel_s:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("gravel_s");
                                break;
                            case TileType.gravel_se:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("gravel_se");
                                break;
                            case TileType.gravel_sw:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("gravel_sw");
                                break;
                            case TileType.gravel_w:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("gravel_w");
                                break;
                            case TileType.grass_ne2:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("grass_ne2");
                                break;
                            case TileType.grass_nw2:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("grass_nw2");
                                break;
                            case TileType.grass_se2:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("grass_se2");
                                break;
                            case TileType.grass_sw2:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("grass_sw2");
                                break;
                            case TileType.rock:
                                Sprite2d.Where(x => x.name == TileName).FirstOrDefault().clipping = true;
                                Objects.Sprite2d rock = new Objects.Sprite2d(new Maps.Asset().getContentByName("rock1"), "rock", true, Sprite2d.Where(x => x.name == TileName).FirstOrDefault().Position, new Vector2(size, sizey), 5, Objects.Base.ControlType.None);
                                rock.orderNum = 1;
                                rock.clipping = true;
                                Sprite2d.Add(rock);
                                break;
                            default:
                                break;
                        }
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
            //}
            //catch (Exception ex)
            //{ }
        }

        public void WarpMap(Vector3 mapVector, Vector3 FromMapVector)
        {
            Menu.ActorStats.RemoveStats(Util.Global.Hero);
            int warp = 60;
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
            catch(Exception ex)//if (mapVector == new Vector3(99, 99, 99))
            {
                string err = ex.Message;
                Util.Global.Hero.Position = new Vector2(
                        (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Where(x => x.name == "6:2").FirstOrDefault().Position.X),
                        (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Where(x => x.name == "6:2").FirstOrDefault().Position.Y)
                        );
            }

            foreach (Guid ID in Util.Global.Pets)
            {
                Objects.Sprite2d X = Util.Global.Sprites.Where(x => x.ID == ID).FirstOrDefault();
                if (X != null)
                {
                    X.Position = Util.Global.Hero.Position;
                }
            }
        }

        public void LoadMapByVector(Vector3 mapVector)
        {
            Util.Global.PreviousMap = Util.Global.CurrentMap;
            Util.Global.PreviousMapPosition = Util.Global.Hero.Position;
            new Objects.Maneuver().RemoveAll();

            if (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z] == null)
            {
                GenerateBaseMap(mapVector);
            }
            List<Objects.Sprite2d> pets = new List<Objects.Sprite2d>();
            foreach (Guid ID in Util.Global.Pets)
            {
                pets.Add(Util.Global.Sprites.Where(X => X.ID == ID).FirstOrDefault());
            }
            Util.Global.Sprites.RemoveAll(x => x.ID != null && x.SpriteType != Objects.Base.Type.Item);
            Util.Global.Sprites.AddRange(pets);

            Util.Global.Sprites.AddRange(Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d);
            Util.Global.Sprites.Add(Util.Global.Hero);
            Util.Global.CurrentMap = mapVector;

            Objects.Sprite2d Pos = Util.Global.Sprites.Where(x => x.name == "pad").FirstOrDefault();
            if (Pos != null)
            { Util.Global.Sprites.Where(x => x.Actor != null && x.Actor.actorType == Objects.Actor.ActorType.Hero).FirstOrDefault().Position = Vector2.Subtract(Pos.Position, new Vector2(0, 10)); }

        }

        public void ReturnToPreviousWarp()
        {
            Util.Global.Hero.Position = Vector2.Subtract(Util.Global.PreviousMapPosition, new Vector2(32, 32));
            foreach (Guid ID in Util.Global.Pets)
            {
                Util.Global.Sprites.Where(X => X.ID == ID).FirstOrDefault().Position = Vector2.Subtract(Util.Global.PreviousMapPosition, new Vector2(10, 10));
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

        public void LoadMapByMapPart(MapPart MP)
        {
            GenerateMapPartMap(MP);
            LoadMapByInt(99,99,98);
        }

        public TileType[, ,] GetMapByPart(MapPart MP)
        {
            TileType[, ,] Map = new TileType[,,] { { } };
            switch (MP)
            {
                case MapPart.room:
                    Map = new Room().Get();
                    break;
                case MapPart.stream:
                    Map = new Stream().Get();
                    break;
                case MapPart.courtyard:
                    Map = new Courtyard().Get();
                    break;
                case MapPart.start:
                    Map = new Start().Get();
                    break;
                case MapPart.forrest:
                    Map = new Forrest().Get();
                    break;
                case MapPart.river:
                    Map = new River().Get();
                    break;
                case MapPart.mountain:
                    Map = new Mountain().Get();
                    break;
                case MapPart.ironvein:
                    Map = Vein.GetIron();
                    break;
            }
            return Map;
        }
        
        public MapPart GetRandomMapPart()
        {
            Array values = Enum.GetValues(typeof(MapPart));
            Random random = new Random();
            MapPart MP = (MapPart)values.GetValue(Util.Global.GetRandomInt(0,values.Length));
            return MP;
        }
    }
}



//for (int i = 1; i <= 15; i++)
//{
//    Objects.Sprite2d gold = new Items.Gold().GetGoldObject(Content);
//    gold.x = i * 100; gold.y = i * 100; S.AddSprite2d(gold);
//}

//S.AddSprite2d(new Actors.Enemy().GetEnemy(Content, 1, 5, 750, 750).Sprite);
//S.AddSprite2d(new Actors.Enemy().GetEnemy(Content, 2, 7, 850, 750).Sprite);
//S.AddSprite2d(new Actors.Enemy().GetEnemy(Content, 3, 10, 950, 750).Sprite);
//S.AddSprite2d(new Actors.Enemy().GetEnemy(Content, 4, 10, 1050, 750).Sprite);



//S.AddSprite2d(new Items.Sapling().GetSaplingItem(Content, 200, 300));
//S.AddSprite2d(new Actors.Pet().GetPet(Content,10,10,100,100).Sprite);


//Sprites.Add(new Objects.Sprite2d(Content.Load<Texture2D>("Images/triangle"), "Obstical", false, 100, 100, 100, screenSizeHeight, screenSizeWidth));
//Sprites.Add(new Objects.Sprite2d(Content.Load<Texture2D>("Images/bluetile"), "Tile", true, 100, 100, 5,true));
//SpritesAnim.Add(new Objects.AnimSprite(Content.Load<Texture2D>("Slides/dice"),"Dice", true, 6, 1, new Vector2(250, 250)));
//SpritesAnim.Add(new Objects.AnimSprite(Content.Load<Texture2D>("Slides/walk2"), "Walker", true,6,5,250,250));
//Texts.Add(new Objects.Text("", "TileText", true, new Vector2(1, 1), Color.Red, screenSizeHeight, screenSizeWidth));
//Texts.Add(new Objects.Text("", "GameTime", true, new Vector2(1, 1), Color.Red, screenSizeHeight, screenSizeWidth));
//RD = new Actions.RollDice(Content.Load<Texture2D>("Slides/dice"), "Dice", false, 6, 1, new Vector2(250, 250));