using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Game.Actions;

namespace Game.Menu
{
    class ActorStats
    {
        public static void DisplayStats(Vector2 Position, Objects.Sprite2d Actor)
        {
            string Name = "Stats" + Actor.ID.ToString();
            Util.Global.Sprites.Add(new Objects.Sprite2d(null, Name, true, Vector2.Add(Position, Actor.Size), new Vector2(10, 10), 0, Objects.Base.ControlType.None));
            //string Stat = "ID:" + Actor.ID.ToString() + "\n";
            string Stat = "Level:" + Actor.Actor.Level.ToString() + "\n";
            Stat = Stat + "Health:  " + Actor.Actor.Health.ToString() + "\n";
            Stat = Stat + "Dex:  " + Actor.Actor.Dex.ToString() + "\n";
            Stat = Stat + "Str:  " + Actor.Actor.Dex.ToString() + "\n";
            Stat = Stat + "End:  " + Actor.Actor.Dex.ToString() + "\n";
            Stat = Stat + "Agl:  " + Actor.Actor.Dex.ToString();
            Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().text = Stat;
            Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().textSize = .8f;
            Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().color = Color.White;
            Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().boxColor = Color.Black;
            Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().orderNum = 980;

            List<Object> obj2 = new List<object>();
            obj2.Add(Actor);
            ActionCall call2 = new ActionCall(ActionType.Update, typeof(ActorStats), "MoveStats", obj2);
            Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().actionCall.Add(call2);

            if (Actor.Actor.actorType == Objects.Actor.ActorType.Hero)
            {
                string HName = "StatsHealth" + Actor.ID.ToString();
                float healthpecentage = (float)Actor.Actor.Health / (float)Actor.Actor.MaxHealth;
                if (healthpecentage > .2)
                {
                    Util.Global.Sprites.Add(new Objects.Sprite2d(Util.Global.AllTexture2D.Where(x => x.Name == "heart_full").FirstOrDefault(), HName + "1", true, Vector2.Subtract(Position, new Vector2(10, 10)), new Vector2(10, 10), Actor.speed, Objects.Base.ControlType.Keyboard));
                }
                else
                {
                    Util.Global.Sprites.Add(new Objects.Sprite2d(Util.Global.AllTexture2D.Where(x => x.Name == "heart_empty").FirstOrDefault(), HName + "1", true, Vector2.Subtract(Position, new Vector2(10, 10)), new Vector2(10, 10), Actor.speed, Objects.Base.ControlType.Keyboard));
                }
                if (healthpecentage > .4)
                {
                    Util.Global.Sprites.Add(new Objects.Sprite2d(Util.Global.AllTexture2D.Where(x => x.Name == "heart_full").FirstOrDefault(), HName + "2", true, Vector2.Subtract(Position, new Vector2(0, 10)), new Vector2(10, 10), Actor.speed, Objects.Base.ControlType.Keyboard));
                }
                else
                {
                    Util.Global.Sprites.Add(new Objects.Sprite2d(Util.Global.AllTexture2D.Where(x => x.Name == "heart_empty").FirstOrDefault(), HName + "2", true, Vector2.Subtract(Position, new Vector2(10, 10)), new Vector2(10, 10), Actor.speed, Objects.Base.ControlType.Keyboard));
                }
                if (healthpecentage > .6)
                {
                    Util.Global.Sprites.Add(new Objects.Sprite2d(Util.Global.AllTexture2D.Where(x => x.Name == "heart_full").FirstOrDefault(), HName + "3", true, Vector2.Subtract(Position, new Vector2(-10, 10)), new Vector2(10, 10), Actor.speed, Objects.Base.ControlType.Keyboard));
                }
                else
                {
                    Util.Global.Sprites.Add(new Objects.Sprite2d(Util.Global.AllTexture2D.Where(x => x.Name == "heart_empty").FirstOrDefault(), HName + "3", true, Vector2.Subtract(Position, new Vector2(10, 10)), new Vector2(10, 10), Actor.speed, Objects.Base.ControlType.Keyboard));
                }
                if (healthpecentage > .8)
                {
                    Util.Global.Sprites.Add(new Objects.Sprite2d(Util.Global.AllTexture2D.Where(x => x.Name == "heart_full").FirstOrDefault(), HName + "4", true, Vector2.Subtract(Position, new Vector2(-20, 10)), new Vector2(10, 10), Actor.speed, Objects.Base.ControlType.Keyboard));
                }
                else
                {
                    Util.Global.Sprites.Add(new Objects.Sprite2d(Util.Global.AllTexture2D.Where(x => x.Name == "heart_empty").FirstOrDefault(), HName + "4", true, Vector2.Subtract(Position, new Vector2(10, 10)), new Vector2(10, 10), Actor.speed, Objects.Base.ControlType.Keyboard));
                }
                if (healthpecentage == 1)
                {
                    Util.Global.Sprites.Add(new Objects.Sprite2d(Util.Global.AllTexture2D.Where(x => x.Name == "heart_full").FirstOrDefault(), HName + "5", true, Vector2.Subtract(Position, new Vector2(-30, 10)), new Vector2(10, 10), Actor.speed, Objects.Base.ControlType.Keyboard));
                }
                else
                {
                    Util.Global.Sprites.Add(new Objects.Sprite2d(Util.Global.AllTexture2D.Where(x => x.Name == "heart_empty").FirstOrDefault(), HName + "5", true, Vector2.Subtract(Position, new Vector2(10, 10)), new Vector2(10, 10), Actor.speed, Objects.Base.ControlType.Keyboard));
                }
                Util.Global.Sprites.Where(x => x.name.Contains(HName)).Select(c => { c.orderNum = 980; return c; }).ToList();
                Util.Global.Sprites.Where(x => x.name.Contains(HName)).Select(c => { c.actionCall.Add(call2); return c; }).ToList();
            }
        }

        public static void UpdateStats(Objects.Sprite2d Actor)
        {
            string Name = "Stats" + Actor.ID.ToString();
            if (Util.Global.Sprites.Where(x => x.name == Name).Count() > 0)
            {
                RemoveStats(Actor);
                DisplayStats(Actor.Position, Actor);
            }
        }

        public static void RemoveStats(Objects.Sprite2d Actor)
        {
            string Name = "Stats" + Actor.ID.ToString();
            Util.Global.Sprites.RemoveAll(x => x.name == Name);
            string HName = "StatsHealth" + Actor.ID.ToString();
            Util.Global.Sprites.RemoveAll(x => x.name.Contains(HName));
            Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().actionCall.RemoveAll(x => x.actionMethodName == "MoveStats");
        }

        public static void ToggleStats(Objects.Sprite2d Actor)
        {
            string Name = "Stats" + Actor.ID.ToString();
            if (Util.Global.Sprites.Where(x => x.name == Name).Count() > 0)
            {
                RemoveStats(Actor);
            }
            else
            {
                DisplayStats(Actor.Position, Actor);
            }
        }
        public static void MoveStats(Objects.Sprite2d Actor)
        {
            string Name = "Stats" + Actor.ID.ToString();
            if (Util.Global.Sprites.Where(x => x.name == Name).Count() > 0)
            {
                Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().Position = Vector2.Add(Actor.Position, Actor.Size);
            }
            else
            {
                foreach(Objects.Sprite2d O in Util.Global.Sprites.Where(x => x.actionCall.Any(y =>y.actionMethodName == "MoveStats")))
                {
                     Util.Global.Sprites.Where(x => x.ID == O.ID).FirstOrDefault().actionCall.RemoveAll(x => x.actionMethodName == "MoveStats");
                }
            }
        }

    }
}
