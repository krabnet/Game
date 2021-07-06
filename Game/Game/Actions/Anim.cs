using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game.Actions
{
   

    public static class Anim
    {

        public static void AnimTest()
        {
            //AnimTest1();
        }

        public static void firefly()
        {
            int size = 3;
            string name = "firefly" + Guid.NewGuid().ToString();
            Vector2 pos = new Vector2();
            int check = Util.Global.GetRandomInt(1, 4);
            int limitLow = 10;
            int limitHigh = 350;
            switch (check)
            {
                case 1:
                    pos = Vector2.Add(Util.Global.Hero.Position, new Vector2(Util.Global.GetRandomInt(limitLow, limitHigh), Util.Global.GetRandomInt(limitLow, limitHigh)));
                    break;
                case 2:
                    pos = Vector2.Add(Util.Global.Hero.Position, new Vector2(Util.Global.GetRandomInt(limitLow, limitHigh)*-1, Util.Global.GetRandomInt(limitLow, limitHigh)));
                    break;
                case 3:
                    pos = Vector2.Add(Util.Global.Hero.Position, new Vector2(Util.Global.GetRandomInt(limitLow, limitHigh), Util.Global.GetRandomInt(limitLow, limitHigh)*-1));
                    break;
                case 4:
                    pos = Vector2.Add(Util.Global.Hero.Position, new Vector2(Util.Global.GetRandomInt(limitLow, limitHigh) * -1, Util.Global.GetRandomInt(limitLow, limitHigh)*-1));
                    break;
            }
            Util.Global.Sprites.Add(new Objects.Sprite2d("drop", name, true, pos, new Vector2(size, size), 10, Objects.Base.ControlType.None));
            Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().orderNum = 1000;
            Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().LightIgnor = true;
            Vector2 start = pos;
            Vector2 end = pos;
            List<Vector2> MOV = Actions.Anim.GetMovement(start, end, Util.Global.GetRandomInt(1, 1));
            MOV.AddRange(GetMovementRandom(end, Util.Global.GetRandomInt(10,60)));
            Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().Maneuver = new Objects.Maneuver(Util.Global.DefaultPosition, new Vector2(size, size), MOV, Util.ColorType.YellowScale, false);
        }

        //cloud or smoke
        public static void AnimTest1()
        {
            Random rnd = new Random();
            int size;
            for (int i = 1; i <= Util.Global.GetRandomInt(25, 200); i++)
            {
                rnd = new Random();
                size = Util.Global.GetRandomInt(50, 75);
                Util.Global.Sprites.Add(new Objects.Sprite2d("smoke", "smoke" + i.ToString(), true, new Vector2(600, 400), new Vector2(size, size), 10, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == "smoke" + i.ToString()).FirstOrDefault().orderNum = 1000;
                Util.Global.Sprites.Where(x => x.name == "smoke" + i.ToString()).FirstOrDefault().LightIgnor = true;
                List<Vector2> MOV =Actions.Anim.GetMovementRandom(new Vector2(700, 400), Util.Global.GetRandomInt(10, 100));
                Util.Global.Sprites.Where(x => x.name == "smoke" + i.ToString()).FirstOrDefault().Maneuver = new Objects.Maneuver(new Vector2(600, 400), new Vector2(size, size),MOV, Util.ColorType.GreyScale, false);
                Util.Global.Sprites.Where(x => x.name == "smoke" + i.ToString()).FirstOrDefault().Maneuver.MovementSize = Util.Sizes.GetSizeList(Util.SizeType.Random, new Vector2(size, size), MOV.Count, 5,3,3);
            }
        }

        //moving color ball
        public static void AnimTest2()
        {
            int i = 1;
            int size = 25;
            Util.Global.Sprites.Add(new Objects.Sprite2d("ball", "ball" + i.ToString(), true, Util.Global.DefaultPosition, new Vector2(size, size), 10, Objects.Base.ControlType.None));
            Util.Global.Sprites.Where(x => x.name == "ball" + i.ToString()).FirstOrDefault().orderNum = 1000;
            Util.Global.Sprites.Where(x => x.name == "ball" + i.ToString()).FirstOrDefault().LightIgnor = true;
            List<Vector2> MOV = Actions.Anim.GetMovement(Util.Global.DefaultPosition, new Vector2(800, 400), 120);
            Util.Global.Sprites.Where(x => x.name == "ball" + i.ToString()).FirstOrDefault().Maneuver = new Objects.Maneuver(Util.Global.DefaultPosition, new Vector2(size, size),MOV, Util.ColorType.Random, false);
            Util.Global.Sprites.Where(x => x.name == "ball" + i.ToString()).FirstOrDefault().Maneuver.MovementSize = Util.Sizes.GetSizeList(Util.SizeType.Grow, new Vector2(size, size), MOV.Count, 1,100,100);
        }

        //firework
        public static void AnimTest3()
        {
            int size = 5;
            for (int i = 1; i <= 50; i++)
            {
                Util.Global.Sprites.Add(new Objects.Sprite2d("drop", "drop" + i.ToString(), true, Util.Global.DefaultPosition, new Vector2(size, size), 10, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == "drop" + i.ToString()).FirstOrDefault().orderNum = 1000;
                Util.Global.Sprites.Where(x => x.name == "drop" + i.ToString()).FirstOrDefault().LightIgnor = true;
                Vector2 start = new Vector2(Util.Global.GetRandomInt(200, 210), Util.Global.GetRandomInt(200, 210));
                Vector2 end = new Vector2(Util.Global.GetRandomInt(195, 215), Util.Global.GetRandomInt(0, 50));
                List<Vector2> MOV = Actions.Anim.GetMovement(start, end, Util.Global.GetRandomInt(10, 50));
                MOV.AddRange(GetMovementRandom(end,20));
                Util.Global.Sprites.Where(x => x.name == "drop" + i.ToString()).FirstOrDefault().Maneuver = new Objects.Maneuver(Util.Global.DefaultPosition, new Vector2(size, size),MOV, Util.ColorType.Random, false);
            }
        }

        public static void InitRain()
        {
            List<Objects.Sprite2d> drops = new List<Objects.Sprite2d>();
            int Hx = (int)Util.Global.Hero.Position.X;
            int Hy = (int)Util.Global.Hero.Position.Y;
            for (int i = 1; i <= 200; i++)
            {
                Vector2 Position = Util.Global.DefaultPosition;
                Vector2 Size = new Vector2(Util.Global.GetRandomInt(1, 3), Util.Global.GetRandomInt(5, 15));

                Objects.Sprite2d drop = new Objects.Sprite2d("drop", "drop" + i.ToString(), true, Position, Size, 10, Objects.Base.ControlType.None);
                drop.orderNum = 1000;
                drop.LightIgnor = true;

                Vector2 start = new Vector2(-500,-500);
                Vector2 end = new Vector2(-500, -500);
                List<Vector2> MOV = Actions.Anim.GetMovement(start, end, Util.Global.GetRandomInt(1, 200));
                drop.Maneuver = new Objects.Maneuver(Position, Size, MOV, Util.ColorType.BlueScale, true);

                List<object> Obj1 = new List<object>();
                Obj1.Add(drop);
                drop.Maneuver.FinalActionCall = new ActionCall(ActionType.Update, typeof(Objects.Maneuver), "ReplaceRainDropMov", Obj1);

                new Objects.Maneuver().ReplaceRainDropMov(Util.Global.Sprites.Where(x => x.name == "drop" + i.ToString()).FirstOrDefault());

                drops.Add(drop);
            }
            Util.Global.RainDrops = drops;
        }
        
        //rain
        public static void AnimTest4()
        {
            Util.Global.Sprites.AddRange(Util.Global.RainDrops);
            //List<Objects.Sprite2d> drops = new List<Objects.Sprite2d>();
            //for (int i = 1; i <= 100; i++)
            //{
            //    Vector2 Position = Util.Global.DefaultPosition;
            //    Vector2 Size = new Vector2(Util.Global.GetRandomInt(1, 3), Util.Global.GetRandomInt(5, 15));

            //    Objects.Sprite2d drop = new Objects.Sprite2d("drop", "drop" + i.ToString(), true, Position, Size, 10, Objects.Base.ControlType.None);
            //    drop.orderNum=1000;
            //    drop.LightIgnor = true;
                
            //    Vector2 start = new Vector2(Util.Global.GetRandomInt(0, 1000), Util.Global.GetRandomInt(-200, -100));
            //    Vector2 end = new Vector2(Util.Global.GetRandomInt(-100, (int)start.X - 100), Util.Global.GetRandomInt(700, 800));
            //    List<Vector2> MOV = Actions.Anim.GetMovement(start, end, Util.Global.GetRandomInt(10, 75));
            //    drop.Maneuver = new Objects.Maneuver(Position, Size, MOV, Util.ColorType.BlueScale, true);

            //    List<object> Obj1 = new List<object>();
            //    Obj1.Add(drop);
            //    drop.Maneuver.FinalActionCall = new ActionCall(ActionType.Update, typeof(Objects.Maneuver), "ReplaceRainDropMov", Obj1);
                
            //    new Objects.Maneuver().ReplaceRainDropMov(Util.Global.Sprites.Where(x => x.name == "drop" + i.ToString()).FirstOrDefault());

            //    drops.Add(drop);
            //}
            //Util.Global.Sprites.AddRange(drops);
        }

        //Cyclone
        public static void AnimTest5()
        {
            int size = 5;
            for (int i = 1; i <= Util.Global.GetRandomInt(50, 100); i++)
            {
                Vector2 Position = Util.Global.Hero.Position;
                size = Util.Global.GetRandomInt(5, 8);
                Vector2 Size = new Vector2(size, size);

                Util.Global.Sprites.Add(new Objects.Sprite2d("drop", "Cyclone" + i.ToString(), true, Position, Size, 10, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == "Cyclone" + i.ToString()).FirstOrDefault().orderNum = 1000;
                Util.Global.Sprites.Where(x => x.name == "Cyclone" + i.ToString()).FirstOrDefault().LightIgnor = true;
                List<Vector2> MOV = Actions.Anim.GetMovementCyclone(Position, 1200);
                Util.Global.Sprites.Where(x => x.name == "Cyclone" + i.ToString()).FirstOrDefault().Maneuver = new Objects.Maneuver(Position,Size, MOV, Util.ColorType.GreyScale, true);
                List<object> Obj1 = new List<object>();
                Obj1.Add(Util.Global.Sprites.Where(x => x.name == "Cyclone" + i.ToString()).FirstOrDefault());
                Obj1.Add(Util.Global.Hero.Position);
                Util.Global.Sprites.Where(x => x.name == "Cyclone" + i.ToString()).FirstOrDefault().Maneuver.FinalActionCall = new ActionCall(ActionType.Update, typeof(Objects.Maneuver), "ReplaceCycloneMov", Obj1);
                Util.Global.Sprites.Where(x => x.name == "Cyclone" + i.ToString()).FirstOrDefault().Maneuver.MovementSize = Util.Sizes.GetSizeList(Util.SizeType.Grow, Size, MOV.Count, 1,3,3);
            }
        }

        public static void AnimTest6()
        {
            for (int i = 1; i <= 150; i++)
            {
                Vector2 Position = Util.Global.DefaultPosition;
                Vector2 Size = new Vector2(Util.Global.GetRandomInt(3, 7), Util.Global.GetRandomInt(3, 7));
                Util.Global.Sprites.Add(new Objects.Sprite2d("drop", "snow" + i.ToString(), true, Position, Size, 10, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == "snow" + i.ToString()).FirstOrDefault().orderNum = 1000;
                Util.Global.Sprites.Where(x => x.name == "snow" + i.ToString()).FirstOrDefault().LightIgnor = true;
                Vector2 start = new Vector2(Util.Global.GetRandomInt(0, 1000), Util.Global.GetRandomInt(-200, -100));
                List<Vector2> MOV = Actions.Anim.GetMovementSnow(start, Util.Global.GetRandomInt(10, 75));
                Util.Global.Sprites.Where(x => x.name == "snow" + i.ToString()).FirstOrDefault().Maneuver = new Objects.Maneuver(Position, Size, MOV, Util.ColorType.WhiteScale, true);

                List<object> Obj1 = new List<object>();
                Obj1.Add(Util.Global.Sprites.Where(x => x.name == "snow" + i.ToString()).FirstOrDefault());
                Util.Global.Sprites.Where(x => x.name == "snow" + i.ToString()).FirstOrDefault().Maneuver.FinalActionCall = new ActionCall(ActionType.Update, typeof(Objects.Maneuver), "ReplaceSnowMov", Obj1);
                new Objects.Maneuver().ReplaceSnowMov(Util.Global.Sprites.Where(x => x.name == "snow" + i.ToString()).FirstOrDefault());

            }
        }

        public static string GetRandomEdgeTileName(int[] options)
        {
            switch(options[Util.Global.GetRandomInt(0,options.Count())])
            {
                case 1:
                    return "0:" + Util.Global.GetRandomInt(0, Util.Global.SizeMap).ToString();
                case 2:
                    return Util.Global.GetRandomInt(0, Util.Global.SizeMap).ToString() + ":0";
                case 3:
                    return (Util.Global.SizeMap - 1).ToString() + ":" + Util.Global.GetRandomInt(0, Util.Global.SizeMap).ToString();
                case 4:
                    return Util.Global.GetRandomInt(0, Util.Global.SizeMap).ToString() + ":" + (Util.Global.SizeMap-1).ToString();
            }
            return null;
        }

        //bird
        public static void AnimTest7()
        {
            if (Util.Global.CurrentMap.Z == 0)
            {
                string StartTileName = "";
                string EndTileName = "";
                for (int i = 1; i <= Util.Global.GetRandomInt(3, 10); i++)
                {
                    Vector2 start;
                    Vector2 end;
                    bool foundtile = true;
                    while (foundtile)
                    {
                        int StartTile = Util.Global.GetRandomInt(1, 4);
                        StartTileName = GetRandomEdgeTileName(new int[1] { StartTile });
                        int[] options = { 1, 2, 3, 4 };
                        options = options.ToList().Where(x => x != StartTile).ToArray();
                        EndTileName = GetRandomEdgeTileName(options);

                        if (Util.Global.Sprites.Where(x => x.name == StartTileName).Count() > 0 && Util.Global.Sprites.Where(x => x.name == EndTileName).Count() > 0)
                        {
                            foundtile = false;
                        }
                    }
                    start = Util.Global.Sprites.Where(x => x.name == StartTileName).FirstOrDefault().Position;
                    end = Util.Global.Sprites.Where(x => x.name == EndTileName).FirstOrDefault().Position;
                    //Objects.Sprite2d Bird = new Objects.Sprite2d("BirdAnim", "BirdAnim", true, Util.Global.DefaultPosition, new Vector2(32, 32), 20, Objects.Base.ControlType.None);
                    //Bird.Position = start;
                    //Bird.orderNum = 500;
                    //Bird.AnimSprite = new Objects.AnimSprite(4, 1);
                    //Bird.AnimSprite.action = true;
                    //Bird.AnimSprite.StopOnEof = false;

                    Objects.Sprite2d Bird = new Objects.Sprite2d("bird2", "BirdAnim", true, Util.Global.DefaultPosition, new Vector2(32, 32), 20, Objects.Base.ControlType.None);
                    Bird.Position = start;
                    Bird.orderNum = 500;
                    Bird.AnimSprite = new Objects.AnimSprite(5, 1);
                    Bird.AnimSprite.action = true;
                    Bird.AnimSprite.StopOnEof = false;

                    float xDiff = start.X - end.X;
                    float yDiff = start.Y - end.Y;
                    //Bird.Orientation = (float)Math.Atan2(yDiff, xDiff) - 89.9f; //*(float)(180 / Math.PI);
                    Bird.Orientation = (float)Math.Atan2(xDiff, yDiff) - 99.9f; ;
                    List<Vector2> MOV = Actions.Anim.GetMovementVarience(start, end, Util.Global.GetRandomInt(150, 200), 5);
                    Bird.Maneuver = new Objects.Maneuver(start, new Vector2(32, 32), MOV, Util.ColorType.None, false);

                    Util.Global.Sprites.Add(Bird);
                }
            }
        }


        public static void AnimText(string Text, Vector2 Position, float TextSize, Color Color)
        {
            string Name = "AnimText"+Text;
            Util.Global.Sprites.Add(new Objects.Sprite2d(null, Name, true, Position, new Vector2(10, 10), 0, Objects.Base.ControlType.None));
            Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().orderNum = 1000;
            Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().LightIgnor = true;
            Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().text=Text;
            Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().color = Color;
            Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().textSize = TextSize;
            List<Vector2> MOV = GetMovementCyclone(Position, 300);
            Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().Maneuver = new Objects.Maneuver(Position, new Vector2(10,10), MOV, Util.ColorType.None, false);
            Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().Maneuver.MovementSize = null;
        }

        public static string AttackAnim(Objects.Sprite2d Attacker, Objects.Sprite2d Defender)
        {
            Vector2 start = Attacker.Position;
            Vector2 end = Defender.Position;
            Vector2 size = new Vector2(10, 10);
            string model = "ball";
            if (Attacker.Actor.actorType != Objects.Actor.ActorType.Hero && Attacker.Actor.enemyType == Enemy.EnemyType.LightBug)
            {
                model = "lightbolt";
                size = new Vector2(30, 60);
            }

            string name = "AttackAnim"+Guid.NewGuid().ToString();
            Util.Global.Sprites.Add(new Objects.Sprite2d(model, name, true, start, size, 10, Objects.Base.ControlType.None));
            Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().orderNum = 1000;
            Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().LightIgnor = true;
            List<Vector2> MOV = Actions.Anim.GetMovement(start, end, 10);
            Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().Maneuver = new Objects.Maneuver(Util.Global.DefaultPosition, size, MOV, Util.ColorType.Random, false);
            return name;
        }

        public static List<Vector2> GetMovement(Vector2 start, Vector2 end, int steps)
        {
            float step = 1.0F / (float)steps;
            float x = 0;
            List<Vector2> returnVector = new List<Vector2>();
            for (float i = 1; i <= steps; i++)
            {
                x= x+step;
                returnVector.Add(Vector2.Lerp(start, end, x));
            }
            return returnVector;
        }

        public static List<Vector2> GetMovementVarience(Vector2 start, Vector2 end, int steps, int Varience)
        {
            float step = 1.0F / (float)steps;
            float x = 0;
            List<Vector2> returnVector = new List<Vector2>();
            for (float i = 1; i <= steps; i++)
            {
                x = x + step;
                if (Util.Global.GetRandomInt(0, 10) > 5)
                {
                    returnVector.Add(Vector2.Lerp(start, end, x));
                }
                else
                {
                    returnVector.Add(Vector2.Add(Vector2.Lerp(start, end, x), new Vector2(Util.Global.GetRandomInt(Varience * -1, Varience), Util.Global.GetRandomInt(Varience * -1, Varience))));
                }
            }
            return returnVector;
        }

        public static List<Vector2> GetMovementCyclone(Vector2 start, int steps)
        {
            List<Vector2> returnVector = new List<Vector2>();
            Point center = new Point((int)start.X, (int)start.Y - Util.Global.GetRandomInt(0, 15));
            double angle = Util.Global.GetRandomInt(0, steps/2);
            double radius = 500;
            for (; angle < steps; angle += 10)
            {
                double scaledRadius = radius * angle / 3600;
                double radians = Math.PI * angle / 180;
                double x = center.X + scaledRadius * Math.Cos(radians);
                //double y = center.Y + scaledRadius * Math.Sin(radians);
                double y = center.Y - scaledRadius;
                returnVector.Add(new Vector2((float)x, (float)y));
            }
            return returnVector;
        }

        public static List<Vector2> GetMovementRandom(Vector2 start, int length)
        {
            float sX = start.X;
            float sY = start.Y;
            int boxsize = 100;
            Rectangle Rec1 = new Rectangle((int)start.X - (boxsize / 2), (int)start.Y - (boxsize / 2), boxsize, boxsize);
            bool InBox = false;

            List<Vector2> returnVector = new List<Vector2>();
            for (int i = 1; i <= length; i++)
            {
                while (!InBox)
                {
                    int L = 0;
                    while (L == 0)
                    {
                        L = Util.Global.GetRandomInt(-3, 3);
                    }
                    sX = sX + Util.Global.GetRandomInt(1, 5) * L;
                    L = 0;
                    while (L == 0)
                    {
                        L = Util.Global.GetRandomInt(-3, 3);
                    }
                    sY = sY + Util.Global.GetRandomInt(1, 5) * L;

                    Rectangle Rec2 = new Rectangle((int)sX, (int)sY, 25, 25);
                    InBox = Util.Base.collision(Rec1, Rec2);
                    if (!InBox)
                    {
                        sX = start.X;
                        sY = start.Y;
                    }
                }
                returnVector.Add(new Vector2(sX, sY));
                InBox = false;
            }
            return returnVector;
        }

        public static List<Vector2> GetMovementJump(Vector2 start, Vector2 end, int steps)
        {
            //int Speed = 10;
            float sX = start.X;
            float sY = start.Y;
            float eX = end.X;
            float eY = end.Y;
            float midPoint = (eX / steps - sX / steps) / 2;

            List<Vector2> returnVector = new List<Vector2>();
            for (int i = 1; i <= midPoint * 2; i++)
            {
                if (i < midPoint)
                {
                    sX = sX + steps;
                    sY = sY - steps;
                }
                else
                {
                    sX = sX + steps;
                    sY = sY + steps;
                }
                returnVector.Add(new Vector2(sX, sY));
            }
            return returnVector;
        }

        public static List<Vector2> GetMovementSnow(Vector2 start, int steps)
        {
            List<Vector2> returnVector = new List<Vector2>();
            Vector2 end = Vector2.Add(start, new Vector2(Util.Global.GetRandomInt(100, 200), Util.Global.GetRandomInt(300, 500)));
            float step = 1.0F / (float)steps;
            float x = 0;
            for (float i = 1; i <= steps; i++)
            {
                x = x + step;
                Vector2 AddVector = Vector2.Lerp(start, end, x);
                if (Util.Global.GetRandomInt(0, 100) < 2)
                {
                    returnVector.Add(Vector2.Add(AddVector, new Vector2(Util.Global.GetRandomInt(1, 5), Util.Global.GetRandomInt(1, 5))));
                }
                else
                {
                    returnVector.Add(AddVector);
                }
            }
            return returnVector;
        }
    }
}
