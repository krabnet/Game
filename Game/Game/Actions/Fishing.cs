using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Game.Actions
{
    public static class Fishing
    {
        public static bool Caught { set; get; }

        public static void GoFish()
        {
            Caught = false;
            Util.Global.FightPreviousMap = Util.Global.CurrentMap;
            Util.Global.FightPreviousHeroLocation = new Vector2(Util.Global.Hero.Position.X, Util.Global.Hero.Position.Y);

            Vector3 mapVector = new Vector3(97, 97, 97);

            new Maps.Map().GenerateMap(mapVector, Game.Maps.MapData.Biome.Fish, 15, 19);
            new Maps.Map().WarpMap(mapVector, Util.Global.CurrentMap);

            Objects.Sprite2d CastStrength = new Objects.Sprite2d("drop", "CastStrength", true, new Vector2(-50,50), new Vector2 (25,275), 0, Objects.Base.ControlType.None);
            CastStrength.color = Color.CadetBlue;
            Util.Global.Sprites.Add(CastStrength);

            Objects.Sprite2d CastStrengthlvl = new Objects.Sprite2d("drop", "CastStrengthlvl", true, new Vector2(-44, 50), new Vector2(13, 1), 0, Objects.Base.ControlType.None);
            CastStrengthlvl.color = Color.Yellow;
            CastStrengthlvl.orderNum = 2000;
            CastStrengthlvl.Maneuver = new Objects.Maneuver();
            CastStrengthlvl.Maneuver.active = true;
            CastStrengthlvl.Maneuver.OriginalVector = new Vector2(-44, 50);
            CastStrengthlvl.Maneuver.OriginalSize = new Vector2(13, 1);
            CastStrengthlvl.Maneuver.ColorType = Util.ColorType.None;
            CastStrengthlvl.Maneuver.Movement = new List<Vector2>();
            CastStrengthlvl.Maneuver.MovementSize = new List<Vector2>();
            CastStrengthlvl.Maneuver.Repeat = true;
            for (int i=1; i < 12; i++)
            {
                CastStrengthlvl.Maneuver.Movement.Add(new Vector2(-44, 50));
                CastStrengthlvl.Maneuver.MovementSize.Add(new Vector2(13, i*25));
            }
            Util.Global.Sprites.Add(CastStrengthlvl);

            Objects.Sprite2d CastButton = new Objects.Sprite2d(null, "CastButton", true, new Vector2(50, 200), new Vector2(8, 8), 0, Objects.Base.ControlType.None);
            CastButton.boxColor = Color.CadetBlue;
            CastButton.text = "CAST";
            CastButton.textSize = 2f;
            List<object> Obj1 = new List<object>();
            //Obj1.Add();
            CastButton.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Actions.Fishing), "CastLine", Obj1));
            Util.Global.Sprites.Add(CastButton);


            Objects.Sprite2d ReturnDoor = new Objects.Sprite2d("doorway", "FishExit", true, new Vector2(-90, 350), new Vector2(596, 640) / 7, 0, Objects.Base.ControlType.None);
            ReturnDoor.orderNum = 1000;
            ReturnDoor.LightIgnor = true;
            //ReturnDoor.Position = Vector2.Add(Util.Global.Sprites.Where(x => x.name == "0:4").FirstOrDefault().Position, new Vector2(6, -50));
            ReturnDoor.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseAny, typeof(Actions.Fishing), "ExitFish", null));
            ReturnDoor.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Actions.Fishing), "ExitFish", null));
            Util.Global.Sprites.Add(ReturnDoor);

            for (int i = 0; i <= 19; i++)
            {
                for (int y = 0; y <= 5; y++)
                {

                    Util.Global.Sprites.Where(x => x.name == y+":"+i).FirstOrDefault().modelname = "sky";
                    Util.Global.Sprites.Where(x => x.name == y + ":" + i).FirstOrDefault().effectType = Objects.Base.EffectType.Ripple;
                }
            }

            for (int i = 5; i <= 18; i++)
            {
                for (int y = 6; y <= 12; y++)
                {

                    Util.Global.Sprites.Where(x => x.name == y + ":" + i).FirstOrDefault().modelname = "water";
                    Util.Global.Sprites.Where(x => x.name == y + ":" + i).FirstOrDefault().effectType = Objects.Base.EffectType.Ripple;
                    Util.Global.Sprites.Where(x => x.name == y + ":" + i).FirstOrDefault().clipping = false;
                }
            }

            for (int i = 5; i <= 18; i++)
            {
                for (int y = 13; y <= 14; y++)
                {

                    Util.Global.Sprites.Where(x => x.name == y + ":" + i).FirstOrDefault().modelname = "sand";
                    Util.Global.Sprites.Where(x => x.name == y + ":" + i).FirstOrDefault().clipping = true;
                }
            }

            for (int i = 5; i <= 18; i++) //Top Clip Box
            {
                //Util.Global.Sprites.Where(x => x.name == "5:" + i).FirstOrDefault().modelname = "sand";
                Util.Global.Sprites.Where(x => x.name == "5:" + i).FirstOrDefault().clipping = true;
            }
            
            for (int i = 6; i <= 13; i++) //Left Clip Box
            {
                //Util.Global.Sprites.Where(x => x.name == i + ":4").FirstOrDefault().modelname = "sand";
                Util.Global.Sprites.Where(x => x.name == i + ":4").FirstOrDefault().clipping = true;
            }

            for (int i = 6; i <= 13; i++) //Right Clip Box
            {
                //Util.Global.Sprites.Where(x => x.name == i + ":19").FirstOrDefault().modelname = "sand";
                Util.Global.Sprites.Where(x => x.name == i + ":19").FirstOrDefault().clipping = true;
            }




            Util.Global.Sprites.Where(x => x.ID == Util.Global.Hero.ID).FirstOrDefault().Position = new Vector2(40, 140);
            Util.Global.Fighting = true;
            Util.Global.Hero.modelname = "HeroRight";
            Util.Global.Cam.Pos = new Vector2(100, 25);
            Util.Global.DrawDistance = 700;

            Objects.Sprite2d FishPole = new Objects.Sprite2d("FishPole", "FishPole", true, new Vector2(-19, 35), new Vector2(200, 200), 0, Objects.Base.ControlType.None);
            Util.Global.Sprites.Add(FishPole);

            AddFish();

            Season.GetMusic();

        }

        public static void ExitFish()
        {
            Util.Global.DrawDistance = 500;
            new Maps.Map().WarpMap(Util.Global.FightPreviousMap, new Vector3(97, 97, 97));
            Util.Global.Sprites.Where(x => x.ID == Util.Global.Hero.ID).FirstOrDefault().Position = Util.Global.FightPreviousHeroLocation;
            Util.Global.Fighting = false;

            Season.GetMusic();
        }

        public static void CastLine()
        {
            Util.Global.Sprites.Where(x => x.name == "CastButton").FirstOrDefault().active = false;

            int CastStrength = (int)Util.Global.Sprites.Where(x => x.name == "CastStrengthlvl").FirstOrDefault().Maneuver.MoveNum;
            CastStrength = CastStrength + Util.Global.GetRandomInt(1, 5);
            CastStrength = 145 + (CastStrength*32);

            Vector2 start = new Vector2(100, 100);
            Vector2 end = new Vector2(CastStrength, 195);
            Vector2 size = new Vector2(2, 2);
            int steps = 100;
            int speed = 1;

            List<Vector2> MOV = Actions.Anim.GetMovement(start, end, steps);
            Objects.Sprite2d Line = new Objects.Sprite2d("drop", "FishLine", true, start, size, speed, Objects.Base.ControlType.None);
            Line.color = Color.Black;
            Line.Maneuver = new Objects.Maneuver(start, size, MOV, Util.ColorType.None, false);
            Line.Maneuver.Draw = true;

            List<object> Obj1 = new List<object>();
            Obj1.Add(end);
            Line.Maneuver.FinalActionCall = new ActionCall(ActionType.Update, typeof(Fishing), "DropLine", Obj1);

            Util.Global.Sprites.Add(Line);

            Util.Global.ContentMan.Load<SoundEffect>("Sounds/whoosh").Play();
        }

        public static void DropLine(Vector2 start)
        {
            Vector2 end = new Vector2(start.X, 420);
            Vector2 size = new Vector2(2, 2);
            int steps = 100;
            int speed = 1;

            List<Vector2> MOV = Actions.Anim.GetMovement(start, end, steps);
            Objects.Sprite2d Line = new Objects.Sprite2d("drop", "FishLine", true, start, size, speed, Objects.Base.ControlType.None);
            Line.color = Color.Black;
            Line.Maneuver = new Objects.Maneuver(start, size, MOV, Util.ColorType.None, false);
            Line.Maneuver.Draw = true;
           

            List<object> Obj1 = new List<object>();
            Line.Maneuver.FinalActionCall = new ActionCall(ActionType.Update, typeof(Fishing), "RemoveLine", Obj1);

            


            Util.Global.Sprites.Add(Line);
        }

        public static void RemoveLine()
        {
            Util.Global.Sprites.RemoveAll(x => x.name == "FishLine");
            Util.Global.Sprites.Where(x => x.name == "CastButton").FirstOrDefault().active = true;
        }

        public static void AddFish()
        {
            Objects.Actor fishes = new Objects.Actor();
            fishes.actorType = Objects.Actor.ActorType.Enemy;
            fishes.Level = 1;
            Objects.Sprite2d fishsprite = new Objects.Sprite2d();
            fishsprite.Actor = fishes;
            fishsprite.modelname = "fish";
            fishsprite.color = Util.Colors.GetColor(Util.ColorType.Random);
            fishsprite.name = "fish";
            //fishsprite.boxColor = Color.Black;
            fishsprite.orderNum = 1000;
            fishsprite.active = true;
            fishsprite.clipping = true;
            //fishsprite.text = "FISH";
            fishsprite.speed = 5;
            fishsprite.Position = new Vector2(250, 350);
            fishsprite.Size = new Vector2(32, 32);

            fishsprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.FishCollision, typeof(Actions.Fishing), "CatchFish", null));

            Util.Global.Sprites.Add(fishsprite);

        }

        public static void CatchFish()
        {
            Objects.Sprite2d CaughtFish = Util.Global.Sprites.Where(x => x.name == "fish").FirstOrDefault();

            if (CaughtFish!=null && Caught == false)
            {
            Caught = true;
            Util.Global.ContentMan.Load<SoundEffect>("Sounds/bubbles").Play();

            Vector2 start = CaughtFish.Position;
            Vector2 end = Util.Global.Hero.Position;
            int steps = 25;

            List<Vector2> MOV = Actions.Anim.GetMovement(start, end, steps);

            Util.Global.Sprites.Where(x => x.name == "fish").FirstOrDefault().Maneuver = new Objects.Maneuver(start, CaughtFish.Size, MOV, Util.ColorType.None, false);

            List<object> Obj1 = new List<object>();
            Util.Global.Sprites.Where(x => x.name == "fish").FirstOrDefault().Maneuver.FinalActionCall = new ActionCall(ActionType.Update, typeof(Fishing), "RemoveFish", Obj1);
            }
        }

        public static void RemoveFish()
        {
            Caught = false;
            Util.Global.Sprites.RemoveAll(x => x.name == "fish");
            Items.ItemActions.AddItemToInventory(Items.Item.GetItemByType(Items.Item.ItemType.Fish, Util.Global.DefaultPosition), 1, 1);
        }

    }
}
