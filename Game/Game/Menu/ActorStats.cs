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
        public static void DisplayStats(Objects.Sprite2d Actor)
        {
            //if (Util.Global.Chest == null && Util.Global.Sprites.Where(x => x.name == "Inventory").Count() == 0 && Util.Global.Sprites.Where(x => x.name == "Craft").Count() == 0)
            if (1==1)
            {
                RemoveAllStats();
                if (Actor.Actor.actorType == Objects.Actor.ActorType.Pet)
                {
                    Say.speak(Actor.ID, Say.GetSpeech(Objects.Actor.ActorType.Pet, SpeechType.Random, Actor.Actor.enemyType));
                }

                Util.Base.Log("DisplayStats");
                //Actor = Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault();

                string Name = "Stats" + Actor.ID.ToString();
                Objects.Sprite2d SP = new Objects.Sprite2d("Char", Name, true, Util.Global.DefaultPosition, new Vector2(150, 400), 0, Objects.Base.ControlType.None);
                SP.boxColor = Color.White;
                SP.Viewtype = Objects.Base.ViewType.HUD;
                SP.orderNum = 500;
                Util.Global.Sprites.Add(SP);

                SP = new Objects.Sprite2d(null, Name + "text", true, new Vector2(5, 10), new Vector2(100, 400), 0, Objects.Base.ControlType.None);
                SP.Viewtype = Objects.Base.ViewType.HUD;
                SP.boxColor = Color.Transparent;
                SP.textSize = 1f;
                SP.orderNum = 501;
                SP.color = Color.White;
                SP.boxColor = Color.Transparent;
                Util.Global.Sprites.Add(SP);

                if (Actor.Actor.actorType != Objects.Actor.ActorType.Hero)
                {
                    SP = new Objects.Sprite2d(Actor.modelname, Name + "image", true, new Vector2(5, 125), Actor.Size, 0, Objects.Base.ControlType.None);
                    SP.Viewtype = Objects.Base.ViewType.HUD;
                    SP.orderNum = 501;
                    SP.boxColor = Color.White;
                    Util.Global.Sprites.Add(SP);
                }
                else
                {
                    SP = new Objects.Sprite2d("Hero", Name + "image", true, new Vector2(5, 125), Actor.Size, 0, Objects.Base.ControlType.None);
                    SP.Viewtype = Objects.Base.ViewType.HUD;
                    SP.orderNum = 501;
                    SP.boxColor = Color.White;
                    Util.Global.Sprites.Add(SP);
                }

                List<Object> obj2 = new List<object>();
                obj2.Add(Actor);
                ActionCall call2 = new ActionCall(ActionType.Update, typeof(ActorStats), "UpdateStatsText", obj2);
                Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().actionCall.Add(call2);

                ShowCloseButton(Actor);
                ShowStablePet(Actor);
            }
        }

        public static void UpdateStatsText(Objects.Sprite2d Actor)
        {
            string Name = "Stats" + Actor.ID.ToString() + "text";

            string Stat = "";
            //Stat = Stat + "ID:" + Actor.ID.ToString() + "\n";
            //Stat = Stat + "Position:" + Actor.Position.X.ToString() + ":" + Actor.Position.Y.ToString() + "\n";

            Stat = Stat + "Level:" + Actor.Actor.Level.ToString() + "\n";
            Stat = Stat + "Health:  " + Actor.Actor.Health.ToString() + "/" + Actor.Actor.HealthMax.ToString() + "\n";
            if (Actor.Actor.actorType == Objects.Actor.ActorType.Hero)
                Stat = Stat + "Hunger:  " + Actor.Actor.Hunger.ToString() + "/" + Actor.Actor.HungerMax.ToString() + "\n";
            else
            {
                HUD.RemoveMainStat();
                Stat = Stat + "Name:  " + Actor.Actor.enemyType.ToString() + "\n";
            }

            Stat = Stat + "Dex:  " + Actor.Actor.Dex.ToString() + "\n";
            Stat = Stat + "Str:  " + Actor.Actor.Str.ToString() + "\n";
            Stat = Stat + "End:  " + Actor.Actor.End.ToString() + "\n";
            Stat = Stat + "Agl:  " + Actor.Actor.Agl.ToString();

            if (Util.Global.Sprites.Where(x => x.name == Name).Count() == 1)
                Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().text = Stat;
            else if (Util.Global.Sprites.Where(x => x.ID == Actor.ID && x.actionCall.Any(y => y.actionMethodName == "UpdateStatsText")).Count() > 0)
                Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().actionCall.RemoveAll(x => x.actionMethodName == "UpdateStatsText");

            UpdateBuff(Actor);
        }

        private static void ShowStablePet(Objects.Sprite2d Actor)
        {
            if (Actor.Actor.aiState == Objects.Actor.AiState.Stabled && Util.Global.Pets.Count() < 4)
            {
                Util.Global.Sprites.RemoveAll(x => x.name == "StableToPet");
                Util.Global.Sprites.Add(new Objects.Sprite2d(null, "StableToPet", true, new Vector2(155, 5), new Vector2(10, 10), 0, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == "StableToPet").FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
                Util.Global.Sprites.Where(x => x.name == "StableToPet").FirstOrDefault().text = "Join Party";
                Util.Global.Sprites.Where(x => x.name == "StableToPet").FirstOrDefault().textSize = 1f;
                Util.Global.Sprites.Where(x => x.name == "StableToPet").FirstOrDefault().color = Color.Red;
                Util.Global.Sprites.Where(x => x.name == "StableToPet").FirstOrDefault().boxColor = Color.Blue;
                Util.Global.Sprites.Where(x => x.name == "StableToPet").FirstOrDefault().orderNum = 10980;
                List<Object> obj3 = new List<object>();
                obj3.Add(Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault());
                ActionCall call3 = new ActionCall(ActionType.Mouse, typeof(Actions.Stable), "JoinParty", obj3);
                Util.Global.Sprites.Where(x => x.name == "StableToPet").FirstOrDefault().actionCall.Add(call3);
                ShowKillPet(Actor);
            }

            if (Actor.Actor.aiState == Objects.Actor.AiState.Figting && Util.Global.CurrentMap == new Vector3(98, 98, 98))
            {
                Util.Global.Sprites.RemoveAll(x => x.name == "PetToStable");
                Util.Global.Sprites.Add(new Objects.Sprite2d(null, "PetToStable", true, new Vector2(155, 5), new Vector2(10, 10), 0, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == "PetToStable").FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
                Util.Global.Sprites.Where(x => x.name == "PetToStable").FirstOrDefault().text = "Leave Party";
                Util.Global.Sprites.Where(x => x.name == "PetToStable").FirstOrDefault().textSize = 1f;
                Util.Global.Sprites.Where(x => x.name == "PetToStable").FirstOrDefault().color = Color.Blue;
                Util.Global.Sprites.Where(x => x.name == "PetToStable").FirstOrDefault().boxColor = Color.Red;
                Util.Global.Sprites.Where(x => x.name == "PetToStable").FirstOrDefault().orderNum = 10980;
                List<Object> obj3 = new List<object>();
                obj3.Add(Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault());
                ActionCall call3 = new ActionCall(ActionType.Mouse, typeof(Actions.Stable), "LeaveParty", obj3);
                Util.Global.Sprites.Where(x => x.name == "PetToStable").FirstOrDefault().actionCall.Add(call3);
                ShowUpgradePet(Actor);
                ShowHealPet(Actor);
                ShowKillPet(Actor);
                //Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().Position = new Vector2(100, 0);
                //Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().actionCall.RemoveAll(x => x.actionMethodName == "MoveStats");
            }
        }

        private static void ShowCloseButton(Objects.Sprite2d Actor)
        {
            Util.Global.Sprites.RemoveAll(x => x.name == "CloseStats");
            Util.Global.Sprites.Add(new Objects.Sprite2d(null, "CloseStats", true, new Vector2(135, 5), new Vector2(10, 10), 0, Objects.Base.ControlType.None));
            Util.Global.Sprites.Where(x => x.name == "CloseStats").FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
            Util.Global.Sprites.Where(x => x.name == "CloseStats").FirstOrDefault().text = "X";
            Util.Global.Sprites.Where(x => x.name == "CloseStats").FirstOrDefault().textSize = 1f;
            Util.Global.Sprites.Where(x => x.name == "CloseStats").FirstOrDefault().color = Color.Azure;
            Util.Global.Sprites.Where(x => x.name == "CloseStats").FirstOrDefault().boxColor = Color.Black;
            Util.Global.Sprites.Where(x => x.name == "CloseStats").FirstOrDefault().orderNum = 10980;
            List<Object> obj3 = new List<object>();
            obj3.Add(Actor);
            ActionCall call3 = new ActionCall(ActionType.Mouse, typeof(ActorStats), "RemoveStats", obj3);
            Util.Global.Sprites.Where(x => x.name == "CloseStats").FirstOrDefault().actionCall.Add(call3);
        }

        private static void ShowUpgradePet(Objects.Sprite2d Actor)
        {
            Util.Global.Sprites.RemoveAll(x => x.name == "UpgradePet");
            Util.Global.Sprites.Add(new Objects.Sprite2d(null, "UpgradePet", true, new Vector2(155, 50), new Vector2(10, 10), 0, Objects.Base.ControlType.None));
            Util.Global.Sprites.Where(x => x.name == "UpgradePet").FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
            Util.Global.Sprites.Where(x => x.name == "UpgradePet").FirstOrDefault().text = "Upgrade Pet";
            Util.Global.Sprites.Where(x => x.name == "UpgradePet").FirstOrDefault().textSize = 1f;
            Util.Global.Sprites.Where(x => x.name == "UpgradePet").FirstOrDefault().color = Color.Blue;
            Util.Global.Sprites.Where(x => x.name == "UpgradePet").FirstOrDefault().boxColor = Color.Red;
            Util.Global.Sprites.Where(x => x.name == "UpgradePet").FirstOrDefault().orderNum = 10980;
            List<Object> obj3 = new List<object>();
            obj3.Add(Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault());
            ActionCall call3 = new ActionCall(ActionType.Mouse, typeof(Fight), "UpgradePet", obj3);
            Util.Global.Sprites.Where(x => x.name == "UpgradePet").FirstOrDefault().actionCall.Add(call3);
        }

        private static void ShowHealPet(Objects.Sprite2d Actor)
        {
            Util.Global.Sprites.RemoveAll(x => x.name == "HealPet");
            Util.Global.Sprites.Add(new Objects.Sprite2d(null, "HealPet", true, new Vector2(155, 100), new Vector2(10, 10), 0, Objects.Base.ControlType.None));
            Util.Global.Sprites.Where(x => x.name == "HealPet").FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
            Util.Global.Sprites.Where(x => x.name == "HealPet").FirstOrDefault().text = "Heal Pet";
            Util.Global.Sprites.Where(x => x.name == "HealPet").FirstOrDefault().textSize = 1f;
            Util.Global.Sprites.Where(x => x.name == "HealPet").FirstOrDefault().color = Color.Blue;
            Util.Global.Sprites.Where(x => x.name == "HealPet").FirstOrDefault().boxColor = Color.Red;
            Util.Global.Sprites.Where(x => x.name == "HealPet").FirstOrDefault().orderNum = 10980;
            List<Object> obj3 = new List<object>();
            obj3.Add(Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault());
            ActionCall call3 = new ActionCall(ActionType.Mouse, typeof(Fight), "HealPet", obj3);
            Util.Global.Sprites.Where(x => x.name == "HealPet").FirstOrDefault().actionCall.Add(call3);
        }

        private static void ShowKillPet(Objects.Sprite2d Actor)
        {
            Util.Global.Sprites.RemoveAll(x => x.name == "KillPet");
            Util.Global.Sprites.Add(new Objects.Sprite2d(null, "KillPet", true, new Vector2(155, 200), new Vector2(10, 10), 0, Objects.Base.ControlType.None));
            Util.Global.Sprites.Where(x => x.name == "KillPet").FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
            Util.Global.Sprites.Where(x => x.name == "KillPet").FirstOrDefault().text = "Kill Pet";
            Util.Global.Sprites.Where(x => x.name == "KillPet").FirstOrDefault().textSize = 1f;
            Util.Global.Sprites.Where(x => x.name == "KillPet").FirstOrDefault().color = Color.Blue;
            Util.Global.Sprites.Where(x => x.name == "KillPet").FirstOrDefault().boxColor = Color.Red;
            Util.Global.Sprites.Where(x => x.name == "KillPet").FirstOrDefault().orderNum = 10980;
            List<Object> obj3 = new List<object>();
            obj3.Add(Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault());
            ActionCall call3 = new ActionCall(ActionType.Mouse, typeof(Fight), "KillPet", obj3);
            Util.Global.Sprites.Where(x => x.name == "KillPet").FirstOrDefault().actionCall.Add(call3);
        }

        public static void UpdateStats(Objects.Sprite2d Actor)
        {
            string Name = "Stats" + Actor.ID.ToString();
            if (Util.Global.Sprites.Where(x => x.name == Name).Count() > 0)
            {
                RemoveStats(Actor);
                DisplayStats(Actor);
            }
        }

        public static void UpdateBuff(Objects.Sprite2d Actor)
        {
            string Name = "Stats" + Actor.ID.ToString();
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("Buff"+Name));
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("BuffDuration" + Name));
            
            //Vector2 BasePos = Vector2.Add(Position, new Vector2(32, 0));
            Vector2 BasePos = new Vector2(10, 370);
            if (Actor.Actor.Buffs.Count() > 0)
            {
                Objects.Sprite2d buffItem = new Objects.Sprite2d();
                foreach (Actions.Buff.BuffType B in Actor.Actor.Buffs)
                {
                    buffItem = Actions.Buff.GetBuffByType(B);
                    buffItem.Position = BasePos;
                    buffItem.Viewtype = Objects.Base.ViewType.HUD;
                    buffItem.orderNum = 10980;
                    buffItem.name = "Buff" + Name;
                    buffItem.boxColor = Color.White;
                    Util.Global.Sprites.Add(buffItem);
                    //Util.Global.Sprites.Where(x => x.name == "Buff" + Name).Select(c => { c.actionCall.Add(call2); return c; }).ToList();

                    int Duration = 0;
                    int IDuration = 0;
                    foreach (Actions.ActionEvents AE in Util.Global.ActionEvents)
                    {
                        foreach (ActionCall AC in AE.actionCall)
                        {
                            if (AC.parameters.Where(x => x == Actor).Count() > 0 && AC.parameters.Where(x => x.ToString() == B.ToString()).Count() > 0)
                            {
                                Duration = AE.Duration;
                                IDuration = AE.InitialDuration;
                            }
                        }
                    }

                    float DurationPercent = (float)Duration / (float)IDuration;
                    float sizeDur = DurationPercent * 25;

                    Color DurationColor = Color.Green;
                    if (DurationPercent < .66)
                        DurationColor = Color.Yellow;
                    if (DurationPercent < .33)
                        DurationColor = Color.Red;

                    Objects.Sprite2d Sprite = new Objects.Sprite2d("drop", "BuffDuration" + Name, true, BasePos, new Vector2(25, sizeDur), 0, Objects.Base.ControlType.None);
                    Sprite.orderNum = 101001;
                    Sprite.color = DurationColor * 0.75f;
                    Sprite.boxColor = DurationColor * 0.75f;
                    Sprite.LightIgnor = true;
                    Sprite.Viewtype = Objects.Base.ViewType.HUD;
                    Util.Global.Sprites.Add(Sprite);


                    BasePos = Vector2.Add(BasePos, new Vector2(0, -26));
                }
            }
        }

        public static void RemoveAllStats()
        {
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("Stats"));
            Util.Global.Sprites.RemoveAll(x => x.name == "KillPet");
            Util.Global.Sprites.RemoveAll(x => x.name == "UpgradePet");
            Util.Global.Sprites.RemoveAll(x => x.name == "HealPet");
            Util.Global.Sprites.RemoveAll(x => x.name == "StableToPet");
            Util.Global.Sprites.RemoveAll(x => x.name == "PetToStable");
            HUD.MainStat();
        }

        public static void RemoveStats(Objects.Sprite2d Actor)
        {
            Util.Base.Log("RemoveStats");
            HUD.MainStat();

            string Name = "Stats" + Actor.ID.ToString();

            Util.Global.Sprites.RemoveAll(x => x.name.Contains(Name));
            Util.Global.Sprites.RemoveAll(x => x.name == "KillPet");
            Util.Global.Sprites.RemoveAll(x => x.name == "UpgradePet");
            Util.Global.Sprites.RemoveAll(x => x.name == "HealPet");
            Util.Global.Sprites.RemoveAll(x => x.name == "StableToPet");
            Util.Global.Sprites.RemoveAll(x => x.name == "PetToStable");
            Util.Global.Sprites.RemoveAll(x => x.name == "CloseStats");
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("Buff" + Name));
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("BuffDuration" + Name));

            string HName = "StatsHealth" + Actor.ID.ToString();
            Util.Global.Sprites.RemoveAll(x => x.name.Contains(HName));

            if (Util.Global.Sprites.Where(x => x.ID == Actor.ID && x.actionCall.Any(y => y.actionMethodName == "UpdateStatsText")).Count() > 0)
            {
                Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().actionCall.RemoveAll(x => x.actionMethodName == "UpdateStatsText");
            }
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
                DisplayStats(Actor);
            }
        }

        public static void MoveStats(Objects.Sprite2d Actor)
        {
            UpdateBuff(Actor);
            string Name = "Stats" + Actor.ID.ToString();
            Vector2 BasePos = Vector2.Add(Actor.Position, new Vector2(32, 0));
            if (Util.Global.Sprites.Where(x => x.name == Name).Count() > 0)
            {
                foreach (Objects.Sprite2d S in Util.Global.Sprites.Where(x => x.name.Contains(Name)))
                {
                    Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Position = Vector2.Subtract(Actor.Position, new Vector2(100, 200));
                }
                foreach (Objects.Sprite2d S in Util.Global.Sprites.Where(x => x.name == "Buff"+Name))
                {
                    Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Position = BasePos;
                    BasePos=Vector2.Add(BasePos, new Vector2(26, 0));
                }
                
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
//if (Actor.Actor.actorType == Objects.Actor.ActorType.Hero)
//{
//    string HName = "StatsHealth" + Actor.ID.ToString();
//    float healthpecentage = (float)Actor.Actor.Health / (float)Actor.Actor.MaxHealth;
//    if (healthpecentage > .2)
//    {
//        Util.Global.Sprites.Add(new Objects.Sprite2d("heart_full", HName + "1", true, Vector2.Subtract(Position, new Vector2(10, 10)), new Vector2(10, 10), Actor.speed, Objects.Base.ControlType.Keyboard));
//    }
//    else
//    {
//        Util.Global.Sprites.Add(new Objects.Sprite2d("heart_empty", HName + "1", true, Vector2.Subtract(Position, new Vector2(10, 10)), new Vector2(10, 10), Actor.speed, Objects.Base.ControlType.Keyboard));
//    }
//    if (healthpecentage > .4)
//    {
//        Util.Global.Sprites.Add(new Objects.Sprite2d("heart_full", HName + "2", true, Vector2.Subtract(Position, new Vector2(0, 10)), new Vector2(10, 10), Actor.speed, Objects.Base.ControlType.Keyboard));
//    }
//    else
//    {
//        Util.Global.Sprites.Add(new Objects.Sprite2d("heart_empty", HName + "2", true, Vector2.Subtract(Position, new Vector2(10, 10)), new Vector2(10, 10), Actor.speed, Objects.Base.ControlType.Keyboard));
//    }
//    if (healthpecentage > .6)
//    {
//        Util.Global.Sprites.Add(new Objects.Sprite2d("heart_full", HName + "3", true, Vector2.Subtract(Position, new Vector2(-10, 10)), new Vector2(10, 10), Actor.speed, Objects.Base.ControlType.Keyboard));
//    }
//    else
//    {
//        Util.Global.Sprites.Add(new Objects.Sprite2d("heart_empty", HName + "3", true, Vector2.Subtract(Position, new Vector2(10, 10)), new Vector2(10, 10), Actor.speed, Objects.Base.ControlType.Keyboard));
//    }
//    if (healthpecentage > .8)
//    {
//        Util.Global.Sprites.Add(new Objects.Sprite2d("heart_full", HName + "4", true, Vector2.Subtract(Position, new Vector2(-20, 10)), new Vector2(10, 10), Actor.speed, Objects.Base.ControlType.Keyboard));
//    }
//    else
//    {
//        Util.Global.Sprites.Add(new Objects.Sprite2d("heart_empty", HName + "4", true, Vector2.Subtract(Position, new Vector2(10, 10)), new Vector2(10, 10), Actor.speed, Objects.Base.ControlType.Keyboard));
//    }
//    if (healthpecentage == 1)
//    {
//        Util.Global.Sprites.Add(new Objects.Sprite2d("heart_full", HName + "5", true, Vector2.Subtract(Position, new Vector2(-30, 10)), new Vector2(10, 10), Actor.speed, Objects.Base.ControlType.Keyboard));
//    }
//    else
//    {
//        Util.Global.Sprites.Add(new Objects.Sprite2d("heart_empty", HName + "5", true, Vector2.Subtract(Position, new Vector2(10, 10)), new Vector2(10, 10), Actor.speed, Objects.Base.ControlType.Keyboard));
//    }
//    Util.Global.Sprites.Where(x => x.name.Contains(HName)).Select(c => { c.orderNum = 980; return c; }).ToList();
//    Util.Global.Sprites.Where(x => x.name.Contains(HName)).Select(c => { c.actionCall.Add(call2); return c; }).ToList();
//}