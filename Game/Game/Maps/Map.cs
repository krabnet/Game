using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Actions;
using Microsoft.Xna.Framework;

namespace Game.Maps
{
    public enum TileType { grass, wall, door, floor, water, tree, rock, dirt, grain, gravel, grass_s, grass_n, grass_se, grass_sw, grass_ne, grass_nw, grass_e, grass_w, gravel_s, gravel_n, gravel_se, gravel_sw, gravel_ne, gravel_nw, gravel_e, gravel_w, grass_ne2, grass_nw2, grass_se2, grass_sw2};
    public enum MapPart { room, stream, courtyard }

    public class Map
    {
        public List<Objects.Spite2d> Sprite2d { get; set; }
        public List<Objects.AnimSprite> AnimSprite { get; set; }
        public Vector3 MapVector { get; set; }
        private const int size = 32;
        private const int sizey = 32;
        private const int sizeMap = 100;

        public void GenerateBaseMap(Vector3 mapVector)
        {
            MapVector = mapVector;
            int tileW = 0;
            int tileH = 0;
            Random rnd = new Random();
            int tilenum = 1;
            Sprite2d = new List<Objects.Spite2d>();
            AnimSprite = new List<Objects.AnimSprite>();
            int WarpLocation = rnd.Next(10, sizeMap-10);
            Tuple<int, int> Loc = new Tuple<int, int>(0, 0);
            for (int i = Loc.Item1; i <= sizeMap; i++)
            {
                for (int y = Loc.Item2; y <= sizeMap; y++)
                {
                    if (i == Loc.Item1 || i == sizeMap || y == Loc.Item2 || y == sizeMap)
                    {
                        if ((i == WarpLocation && y == 0) || (i == WarpLocation && y == sizeMap) || (y == WarpLocation && i == 0) || (y == WarpLocation && i == sizeMap))
                        {
                            Objects.Spite2d Warp = new Objects.Spite2d(new Maps.Asset().getContentByName("warp"), i.ToString() + ":" + y.ToString(), true, tileH-5, tileW-5, size+15, sizey+15, 5, Objects.Base.ControlType.None);
                            if (i == 0)
                            {
                                Warp.name = "North";
                                List<object> obj1 = new List<object>();
                                obj1.Add(new Vector3(MapVector.X + 1,MapVector.Y,MapVector.Z));
                                obj1.Add(new Vector3(MapVector.X, MapVector.Y, MapVector.Z));
                                ActionCall AC = new ActionCall(typeof(Maps.Map), "WarpMap", obj1);
                                Warp.actionCall = AC;
                                Warp.actionType = Objects.Base.ActionType.Collision;
                            }
                            if (i == sizeMap)
                            {
                                Warp.name = "South";
                                List<object> obj1 = new List<object>();
                                obj1.Add(new Vector3(MapVector.X - 1, MapVector.Y, MapVector.Z));
                                obj1.Add(new Vector3(MapVector.X, MapVector.Y, MapVector.Z));
                                ActionCall AC = new ActionCall(typeof(Maps.Map), "WarpMap", obj1);
                                Warp.actionCall = AC;
                                Warp.actionType = Objects.Base.ActionType.Collision;
                            }
                            if (y == 0)
                            {
                                Warp.name = "West";
                                List<object> obj1 = new List<object>();
                                obj1.Add(new Vector3(MapVector.X, MapVector.Y-1, MapVector.Z));
                                obj1.Add(new Vector3(MapVector.X, MapVector.Y, MapVector.Z));
                                ActionCall AC = new ActionCall(typeof(Maps.Map), "WarpMap", obj1);
                                Warp.actionCall = AC;
                                Warp.actionType = Objects.Base.ActionType.Collision;
                            }
                            if (y == sizeMap)
                            {
                                Warp.name = "East";
                                List<object> obj1 = new List<object>();
                                obj1.Add(new Vector3(MapVector.X, MapVector.Y+1, MapVector.Z));
                                obj1.Add(new Vector3(MapVector.X, MapVector.Y, MapVector.Z));
                                ActionCall AC = new ActionCall(typeof(Maps.Map), "WarpMap", obj1);
                                Warp.actionCall = AC;
                                Warp.actionType = Objects.Base.ActionType.Collision;
                            }
                            Sprite2d.Add(Warp);
                        }
                        else
                        {
                            Sprite2d.Add(new Objects.Spite2d(new Maps.Asset().getContentByName("gravel"), i.ToString() + ":" + y.ToString(), true, tileH, tileW, size, sizey, 5, Objects.Base.ControlType.None));
                            //Util.Global.Sprites.Where(x => x.name == i.ToString() + ":" + y.ToString()).FirstOrDefault().clipping = true;
                            Objects.Spite2d rock = new Objects.Spite2d(new Maps.Asset().getContentByName("rockprop"), "rock" + i.ToString() + ":" + y.ToString(), true, tileH, tileW, size, sizey, 5, Objects.Base.ControlType.None);
                            rock.clipping = true;
                            Sprite2d.Add(rock);
                        }
                    }
                    else
                    {
                        Sprite2d.Add(new Objects.Spite2d(new Maps.Asset().getContentByName("grass0"), i.ToString() + ":" + y.ToString(), true, tileH, tileW, size, sizey, 5, Objects.Base.ControlType.None));
                        int propChance = rnd.Next(0, 100);
                        if (propChance < 10)
                            Sprite2d.Add(new Objects.Spite2d(new Maps.Asset().getContentByName("bush"), "bushprop" + i.ToString() + ":" + y.ToString(), true, tileH, tileW, size, sizey, 5, Objects.Base.ControlType.None));
                        else if (propChance == 11)
                            Sprite2d.Add(new Objects.Spite2d(new Maps.Asset().getContentByName("flowerprop"), "flowerprop" + i.ToString() + ":" + y.ToString(), true, tileH, tileW, size, sizey, 5, Objects.Base.ControlType.None));
                        else if (propChance == 12)
                            Sprite2d.Add(new Objects.Spite2d(new Maps.Asset().getContentByName("flowerprop2"), "flowerprop" + i.ToString() + ":" + y.ToString(), true, tileH, tileW, size, sizey, 5, Objects.Base.ControlType.None));
                        else if (propChance == 66)
                            Sprite2d.Add(new Objects.Spite2d(new Maps.Asset().getContentByName("rockprop"), "rockprop" + i.ToString() + ":" + y.ToString(), true, tileH, tileW, size, sizey, 5, Objects.Base.ControlType.None));

                    }


                    //Objects.Text TileText = new Objects.Text(i.ToString() + ":" + y.ToString(), "TileText" + tilenum.ToString(), true, tileH, tileW, Color.Red);
                    //TileText.speed = 5;
                    //TileText.allowScroll = true;
                    //TileText.controlType = Objects.Base.ControlType.None;
                    //TileText.orderNum = 1000;
                    //Util.Global.Texts.Add(TileText);

                    tileH = tileH + size;
                    tilenum++;
                }
                tileH = 1;
                tileW = tileW + sizey;
            }
            Sprite2d.Select(c => { c.allowScroll = true; return c; }).ToList();
            Sprite2d.Where(x => x.orderNum == 0).Select(c => { c.orderNum = 1; return c; }).ToList();
            Sprite2d.Add(new Items.Sapling().GetSaplingItem(200, 300));
            //DisplayMap(new Maps.Room().GetRoom(), -5, 0);
            //DisplayMap(new Maps.Stream().GetStream(), 10, 0);
            //DisplayMap(new Maps.Start().GetStart(), -5, 0);
            this.Sprite2d = Sprite2d;
            Util.Global.MainMap[(int)MapVector.X, (int)MapVector.Y, (int)MapVector.Z] = this;
        }

        public void AddMapPart(MapPart MP, int StartX, int StartY)
        {
            int tilex = StartX;
            int tiley = StartY;
            TileType[, ,] Map = new TileType[, ,]{{}};
            switch(MP)
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
            }
            int columncount = 0;
            foreach (TileType TT in Map)
            {
                string TileName = string.Format("{0}:{1}", tilex.ToString(), tiley.ToString());
                Sprite2d.Remove(Sprite2d.Where(x => x.name.Contains("prop" + TileName)).FirstOrDefault());
                switch (TT)
                {
                    case TileType.wall:
                        Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("wall");
                        Sprite2d.Where(x => x.name == TileName).FirstOrDefault().clipping = true;
                        break;
                    case TileType.door:
                        AnimSprite.Add(new Items.Door().GetDoor(Util.Global.ContentMan, Sprite2d.Where(x => x.name == TileName).FirstOrDefault().x, Sprite2d.Where(x => x.name == TileName).FirstOrDefault().y, size, sizey));
                        Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("floor2");
                        break;
                    case TileType.floor:
                        Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("floor2");
                        break;
                    case TileType.gravel:
                        Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("gravel");
                        break;
                    case TileType.tree:
                        Objects.Spite2d tree = new Objects.Spite2d(new Maps.Asset().getContentByName("tree2"), "tree", true, Sprite2d.Where(x => x.name == TileName).FirstOrDefault().x, Sprite2d.Where(x => x.name == TileName).FirstOrDefault().y, size, sizey, 5, Objects.Base.ControlType.None);
                        tree.orderNum = 1;
                        Sprite2d.Add(tree);
                        break;
                    case TileType.grain:
                        Objects.Spite2d grain = new Objects.Spite2d(new Maps.Asset().getContentByName("grain"), "grain", true, Sprite2d.Where(x => x.name == TileName).FirstOrDefault().x, Sprite2d.Where(x => x.name == TileName).FirstOrDefault().y, size, sizey, 5, Objects.Base.ControlType.None);
                        grain.orderNum = 1;
                        grain.actionType = Objects.Base.ActionType.Mouse;
                        List<Object> Objs3 = new List<object>();
                        Objs3.Add(grain);
                        grain.actionCall = new ActionCall(typeof(Objects.Spite2d), "FlipMouseControl", Objs3);
                        Sprite2d.Add(grain);
                        break;
                    case TileType.water:
                        Sprite2d.Where(x => x.name == TileName).FirstOrDefault().model = new Maps.Asset().getContentByName("water");
                        Sprite2d.Where(x => x.name == TileName).FirstOrDefault().clipping = true;
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
                        Objects.Spite2d rock = new Objects.Spite2d(new Maps.Asset().getContentByName("rock1"), "rock", true, Sprite2d.Where(x => x.name == TileName).FirstOrDefault().x, Sprite2d.Where(x => x.name == TileName).FirstOrDefault().y, size, sizey, 5, Objects.Base.ControlType.None);
                        rock.orderNum = 1;
                        rock.clipping = true;
                        Sprite2d.Add(rock);
                        break;
                    default:
                        break;
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

        public void WarpMap(Vector3 mapVector, Vector3 FromMapVector)
        {
            int warp = 65;
            LoadMapByVector(mapVector);
            if (FromMapVector.X > mapVector.X)
            {
                //N->S
                int changey = Util.Global.Hero.HeroSprite.y - (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Where(x => x.name == "North").FirstOrDefault().y + warp);
                int changex = Util.Global.Hero.HeroSprite.x - (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Where(x => x.name == "North").FirstOrDefault().x);
                Maps.Asset.MoveTiles(changex, changey);
            }
            else if (FromMapVector.X < mapVector.X)
            {
                //S->N
                int changey = Util.Global.Hero.HeroSprite.y - (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Where(x => x.name == "South").FirstOrDefault().y - warp);
                int changex = Util.Global.Hero.HeroSprite.x - (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Where(x => x.name == "South").FirstOrDefault().x);
                Maps.Asset.MoveTiles(changex, changey);
            }
            else if (FromMapVector.Y > mapVector.Y)
            {
                //E->W
                int changey = Util.Global.Hero.HeroSprite.y - (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Where(x => x.name == "East").FirstOrDefault().y);
                int changex = Util.Global.Hero.HeroSprite.x - (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Where(x => x.name == "East").FirstOrDefault().x - warp);
                Maps.Asset.MoveTiles(changex, changey);
            }
            else if (FromMapVector.Y < mapVector.Y)
            {
                //W->E
                int changey = Util.Global.Hero.HeroSprite.y - (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Where(x => x.name == "West").FirstOrDefault().y);
                int changex = Util.Global.Hero.HeroSprite.x - (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Where(x => x.name == "West").FirstOrDefault().x + warp);
                Maps.Asset.MoveTiles(changex, changey);
            }
        }

        public void LoadMapByVector(Vector3 mapVector)
        {
            if (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z] == null)
            {
                GenerateBaseMap(mapVector);
            }
            Util.Global.Sprites = new List<Objects.Spite2d>();
            Util.Global.SpritesAnim = new List<Objects.AnimSprite>();

            Util.Global.Sprites.Add(Util.Global.GameMouse);
            Util.Global.Sprites.Add(Util.Global.GameBackground);
            Util.Global.Sprites.Add(Util.Global.GameFightBackground);
            Util.Global.SpritesAnim.Add(Util.Global.Hero.HeroSprite);

            Util.Global.Sprites.AddRange(Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d);
            Util.Global.SpritesAnim.AddRange(Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].AnimSprite);
            Util.Global.CurrentMap = mapVector;
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
    }
}



//for (int i = 1; i <= 15; i++)
//{
//    Objects.Spite2d gold = new Items.Gold().GetGoldObject(Content);
//    gold.x = i * 100; gold.y = i * 100; S.AddSpite2d(gold);
//}

//S.AddSpite2d(new Actors.Enemy().GetEnemy(Content, 1, 5, 750, 750).Sprite);
//S.AddSpite2d(new Actors.Enemy().GetEnemy(Content, 2, 7, 850, 750).Sprite);
//S.AddSpite2d(new Actors.Enemy().GetEnemy(Content, 3, 10, 950, 750).Sprite);
//S.AddSpite2d(new Actors.Enemy().GetEnemy(Content, 4, 10, 1050, 750).Sprite);



//S.AddSpite2d(new Items.Sapling().GetSaplingItem(Content, 200, 300));
//S.AddSpite2d(new Actors.Pet().GetPet(Content,10,10,100,100).Sprite);


//Sprites.Add(new Objects.Spite2d(Content.Load<Texture2D>("Images/triangle"), "Obstical", false, 100, 100, 100, screenSizeHeight, screenSizeWidth));
//Sprites.Add(new Objects.Spite2d(Content.Load<Texture2D>("Images/bluetile"), "Tile", true, 100, 100, 5,true));
//SpritesAnim.Add(new Objects.AnimSprite(Content.Load<Texture2D>("Slides/dice"),"Dice", true, 6, 1, new Vector2(250, 250)));
//SpritesAnim.Add(new Objects.AnimSprite(Content.Load<Texture2D>("Slides/walk2"), "Walker", true,6,5,250,250));
//Texts.Add(new Objects.Text("", "TileText", true, new Vector2(1, 1), Color.Red, screenSizeHeight, screenSizeWidth));
//Texts.Add(new Objects.Text("", "GameTime", true, new Vector2(1, 1), Color.Red, screenSizeHeight, screenSizeWidth));
//RD = new Actions.RollDice(Content.Load<Texture2D>("Slides/dice"), "Dice", false, 6, 1, new Vector2(250, 250));