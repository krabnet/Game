using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game.Menu
{
    public static class HUD
    {
        public static void MainStat()
        {
            if (Util.Global.Sprites.Where(x => x.name == "HealthHudTotal").Count() == 0)
            {
                //Objects.Sprite2d SP = new Objects.Sprite2d(null, "HUD_Main", true, new Vector2(5, 10), new Vector2(10, 10), 0, Objects.Base.ControlType.None);
                //string Stat = "";
                //Stat = Stat + "Level:" + Util.Global.Hero.Actor.Level.ToString() + "\n";
                //Stat = Stat + "Health:  " + Util.Global.Hero.Actor.Health.ToString() + "/" + Util.Global.Hero.Actor.HealthMax.ToString() + "\n";
                //Stat = Stat + "Hunger:  " + Util.Global.Hero.Actor.Hunger.ToString() + "/" + Util.Global.Hero.Actor.HungerMax.ToString() + "\n";
                //SP.text = Stat;
                //SP.Viewtype = Objects.Base.ViewType.HUD;
                //SP.textSize = 1f;
                //SP.color = Color.White;
                //SP.boxColor = Color.Transparent;
                ////SP.orderNum = 2000;
                //Util.Global.Sprites.Add(SP);

                Vector2 pos = new Vector2(5, 10);

                Util.Global.Sprites.Add(new Objects.Sprite2d("SpringIcon", "SeasonIcon", true, pos, new Vector2(345 / 9, 346 / 9), 0, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == "SeasonIcon").FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;

                        pos = Vector2.Add(pos, new Vector2((345 / 9) + 3, 0));
                
                string name = "HealthHudTotal";
                Util.Global.Sprites.Add(new Objects.Sprite2d("drop", name, true, pos, new Vector2(50, 12), 10, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;

                name = "HealthHudTotalText";
                Util.Global.Sprites.Add(new Objects.Sprite2d(null, name, true, Vector2.Add(pos,new Vector2(0, 0)), new Vector2(10, 10), 0, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().text = Util.Global.Hero.Actor.Health.ToString() + "/" + Util.Global.Hero.Actor.HealthMax.ToString();
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().textSize = .8f;
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().color = Color.Black;
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().orderNum = 1000;
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().boxColor = Color.Transparent;

                name = "HealthHud";
                Util.Global.Sprites.Add(new Objects.Sprite2d("drop", name, true, Vector2.Subtract(pos, new Vector2(1, 0)), new Vector2(1, 8), 5, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().color = Color.Red;
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().Size = new Vector2(50, 12);

                        pos = Vector2.Add(pos, new Vector2(0, 15));

                name = "HungerHudTotal";
                Util.Global.Sprites.Add(new Objects.Sprite2d("drop", name, true, pos, new Vector2(50, 12), 10, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;

                name = "HungerHudTotalText";
                Util.Global.Sprites.Add(new Objects.Sprite2d(null, name, true, Vector2.Add(pos, new Vector2(0, 0)), new Vector2(10, 10), 0, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().text = Util.Global.Hero.Actor.Hunger.ToString() + "/" + Util.Global.Hero.Actor.HungerMax.ToString();
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().textSize = .8f;
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().color = Color.Black;
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().orderNum = 1000;
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().boxColor = Color.Transparent;

                name = "HungerHud";
                Util.Global.Sprites.Add(new Objects.Sprite2d("drop", name, true, Vector2.Subtract(pos, new Vector2(1, 0)), new Vector2(1, 8), 5, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().color = Color.Brown;
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().Size = new Vector2(50, 12);

                        pos = Vector2.Add(pos, new Vector2(0, 15));


                name = "DayHud";
                Util.Global.Sprites.Add(new Objects.Sprite2d(null, name, true, Vector2.Add(pos, new Vector2(0, 0)), new Vector2(10, 10), 0, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().text = Util.Global.GameClockDay.ToString();
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().textSize = .8f;
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().color = Color.White;
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().orderNum = 1000;
                Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().boxColor = Color.Transparent;
            }
        }
        public static void MainStat_Update()
        {
            if (Util.Global.Sprites.Where(x => x.name == "HungerHud").Count() > 0)
            {
                float size = ((float)Util.Global.Hero.Actor.Hunger / (float)Util.Global.Hero.Actor.HungerMax) * (float)50;
                Util.Global.Sprites.Where(x => x.name == "HungerHud").FirstOrDefault().Size = new Vector2(size, 12);
                Util.Global.Sprites.Where(x => x.name == "HungerHudTotalText").FirstOrDefault().text = Util.Global.Hero.Actor.Hunger.ToString() + "/" + Util.Global.Hero.Actor.HungerMax.ToString();
            }
            if (Util.Global.Sprites.Where(x => x.name == "HealthHud").Count() > 0)
            {
                float size = ((float)Util.Global.Hero.Actor.Health / (float)Util.Global.Hero.Actor.HealthMax) * (float)50;
                Util.Global.Sprites.Where(x => x.name == "HealthHud").FirstOrDefault().Size = new Vector2(size, 12);
                Util.Global.Sprites.Where(x => x.name == "HealthHudTotalText").FirstOrDefault().text = Util.Global.Hero.Actor.Health.ToString() + "/" + Util.Global.Hero.Actor.HealthMax.ToString();
            }

            Util.Global.Sprites.Where(x => x.name == "DayHud").FirstOrDefault().text = Util.Global.GameClockDay.ToString() + "\n" + Util.Global.GameClock.TotalSeconds.ToString();

           

            //if (Util.Global.Sprites.Where(x => x.name == "HUD_Main").Count() > 0)
            //{
            //    string Stat = "";
            //    Stat = Stat + "Level:" + Util.Global.Hero.Actor.Level.ToString() + "\n";
            //    Stat = Stat + "Health:  " + Util.Global.Hero.Actor.Health.ToString() + "/" + Util.Global.Hero.Actor.HealthMax.ToString() + "\n";
            //    Stat = Stat + "Hunger:  " + Util.Global.Hero.Actor.Hunger.ToString() + "/" + Util.Global.Hero.Actor.HungerMax.ToString() + "\n";
            //    Util.Global.Sprites.Where(x => x.name == "HUD_Main").FirstOrDefault().text = Stat;
            //    ActorStats.UpdateBuff(Util.Global.Hero);
            //}
        }
        public static void RemoveMainStat()
        {
            Util.Global.Sprites.RemoveAll(x => x.name == "HealthHudTotal");
        }
    }
}
