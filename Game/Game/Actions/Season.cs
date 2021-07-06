using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Actions
{
    public static class Season
    {
        private static double PreviousEventSecond { set; get; }

        public static void UpdateClock(GameTime gameTime)
        {
            if (Util.Global.Sprites.Where(x => x.name == "Clock").Count() < 1)
            {
                Util.Global.Sprites.Add(new Objects.Sprite2d(null, "Clock", true, new Vector2(-50, 0), Util.Global.DefaultPosition, 0, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == "Clock").FirstOrDefault().textSize = 1f;
                Util.Global.Sprites.Where(x => x.name == "Clock").FirstOrDefault().color = Color.Red;
                Util.Global.Sprites.Where(x => x.name == "Clock").FirstOrDefault().boxColor = Color.Blue;
                Util.Global.Sprites.Where(x => x.name == "Clock").FirstOrDefault().orderNum = 101;
                Util.Global.Sprites.Where(x => x.name == "Clock").FirstOrDefault().active = false;
            }
            if (Util.Global.GameClockPreviousSecond != gameTime.TotalGameTime.Seconds)
            {
                Util.Global.Sprites.Where(x => x.name == "Clock").ToList().FirstOrDefault().text =
                    "Day:" + Util.Global.GameClockDay.ToString() + "\n" +
                    Util.Global.Season.ToString() + "\n" +
                    Util.Global.GameClock.TotalSeconds.ToString() + "\n" +
                    Util.Global.screenSizeWidth.ToString() + "x" + Util.Global.screenSizeHeight.ToString() + "\n" +
                    Util.Global.Cam.Zoom.ToString() + "\n" +
                    //Util.Base.GetRandomName() + "\n" +
                    Enviro.GetTime().ToString() + "\n" +
                    (Util.Global.Music.Duration - MediaPlayer.PlayPosition).ToString(@"mm\:ss");

                RunEvents();

                Util.Global.GameClockPreviousSecond = gameTime.TotalGameTime.Seconds;
                Util.Global.GameClock = Util.Global.GameClock.Add(new TimeSpan(0, 0, 1));
                Util.Global.Sprites.Where(x => x.ID == Util.Global.Hero.ID).FirstOrDefault().Actor.Hunger = Util.Global.Sprites.Where(x => x.ID == Util.Global.Hero.ID).FirstOrDefault().Actor.Hunger - 1;
                Menu.HUD.MainStat_Update();
                Util.GameMouse.DetectMouseHover();

                if (Util.Global.Sprites.Where(x => x.ID == Util.Global.Hero.ID).FirstOrDefault().Actor.Hunger == 900)
                {
                    Say.speak(Util.Global.Hero.ID, "Starting to get peckish.");
                }
                if (Util.Global.Sprites.Where(x => x.ID == Util.Global.Hero.ID).FirstOrDefault().Actor.Hunger == 600)
                {
                    Say.speak(Util.Global.Hero.ID, "I could eat something");
                }
                if (Util.Global.Sprites.Where(x => x.ID == Util.Global.Hero.ID).FirstOrDefault().Actor.Hunger == 300)
                {
                    Say.speak(Util.Global.Hero.ID, "Sooooo Hungry");
                }
                if (Util.Global.Sprites.Where(x => x.ID == Util.Global.Hero.ID).FirstOrDefault().Actor.Hunger == 100)
                {
                    Say.speak(Util.Global.Hero.ID, "I'm Starving!");
                }
                if (Util.Global.Sprites.Where(x => x.ID == Util.Global.Hero.ID).FirstOrDefault().Actor.Hunger == 10)
                {
                    Say.speak(Util.Global.Hero.ID, "Warrior is about to die!");
                }

                if (Util.Global.Sprites.Where(x => x.ID == Util.Global.Hero.ID).FirstOrDefault().Actor.Hunger < 0)
                {
                    Util.Global.ContentMan.Load<SoundEffect>("Sounds/oooh").Play();
                    new Util.Main().Init();
                }
            }
            if (Util.Global.GameClock.Minutes == 10)
            {
                Util.Global.GameClock = new TimeSpan(0);
                Util.Global.GameClockDay++;
                if (Util.Global.GameClockDay % 5 == 0)
                {
                    MoveToNextSeason();
                    Shop.ResetAllShopsInventory();
                }

            }

            float F = (float)Util.Base.GetBellCurvePoint((Util.Global.GameClock.Minutes * .1f), 1f);
            Util.Global.DayColor = Util.Colors.LerpColor(Color.White, Color.Black, F);
            if (Enviro.InCave())
            {
                Util.Global.DayColor = Color.Black;
            }
            if (Util.Global.DayColor.G < 152)
            { Util.Base.GetLightSources(); }

            if (MediaPlayer.State == MediaState.Playing && MediaPlayer.PlayPosition < new TimeSpan(0, 0, 6))
            {
                MediaPlayer.Volume = MathHelper.Clamp(MediaPlayer.PlayPosition.Seconds * .2F, 0.1F, 1F);
            }

            if (MediaPlayer.State == MediaState.Playing && (Util.Global.Music.Duration - MediaPlayer.PlayPosition) < new TimeSpan(0, 0, 6))
            {
                MediaPlayer.Volume = MathHelper.Clamp((Util.Global.Music.Duration - MediaPlayer.PlayPosition).Seconds * .2F, 0.1F, 1F);
            }

            if (Util.Global.MusicChange)
            {
                MediaPlayer.Play(Util.Global.Music);
                Util.Global.MusicChange = false;
            }
        }

        public static void AddTime()
        {
            Util.Global.GameClock = Util.Global.GameClock.Add(new TimeSpan(0, 0, 5)); 
        }

        public static void SubtractTime()
        {
            Util.Global.GameClock = Util.Global.GameClock.Subtract(new TimeSpan(0, 0, 5));
        }

        public static void StartEarlyWinter()
        {
            //Util.Global.Sprites.Where(x => x.modelname != null && x.modelname == "grass0").Select(y => { y.modelname = "snow"; return y; }).ToList();

            Texture2D T = Util.Global.ContentMan.Load<Texture2D>("Images/Prop/tree3dead2");
            T.Name = "tree3";
            Util.Global.AllTexture2D.RemoveAll(x => x.Name == "tree3");
            Util.Global.AllTexture2D.Add(T);

            T = Util.Global.ContentMan.Load<Texture2D>("Images/Tile/snow");
            T.Name = "grass0";
            Util.Global.AllTexture2D.RemoveAll(x => x.Name == "grass0");
            Util.Global.AllTexture2D.Add(T);

            Util.Global.Season = Enviro.SeasonType.WinterEarly;
        }

        public static void StartEarlyFall()
        {
            Texture2D T = Util.Global.ContentMan.Load<Texture2D>("Images/Prop/tree3fall");
            T.Name = "tree3";
            Util.Global.AllTexture2D.RemoveAll(x => x.Name == "tree3");
            Util.Global.AllTexture2D.Add(T);
            Util.Global.Season = Actions.Enviro.SeasonType.FallEarly;
        }

        public static void StartEarlySpring()
        {
            Texture2D T = Util.Global.ContentMan.Load<Texture2D>("Images/Prop/tree3main");
            T.Name = "tree3";
            Util.Global.AllTexture2D.RemoveAll(x => x.Name == "tree3");
            Util.Global.AllTexture2D.Add(T);
            Util.Global.Season = Actions.Enviro.SeasonType.SpringEarly;

            T = Util.Global.ContentMan.Load<Texture2D>("Images/Tile/grass0");
            T.Name = "grass0";
            Util.Global.AllTexture2D.RemoveAll(x => x.Name == "grass0");
            Util.Global.AllTexture2D.Add(T);
        }

        public static void CheckWeather()
        {
            new Objects.Maneuver().RemoveAll();
            if (!Enviro.InCave())
            {
                switch(Util.Global.Weather)
                {
                    case Enviro.Weather.Snow:
                        Actions.Anim.AnimTest6();
                        break;
                    case Enviro.Weather.Rain:
                        Actions.Anim.AnimTest4();
                        break;
                }
            }
            GetMusicBackGround();
        }

        private static void GetWeather()
        {
            if (Util.Global.Season == Enviro.SeasonType.WinterEarly || Util.Global.Season == Enviro.SeasonType.Winter || Util.Global.Season == Enviro.SeasonType.WinterLate)
            {
                if (Util.Global.GetRandomInt(0, 100) < 20 && Util.Global.Weather == Enviro.Weather.Clear)
                {
                    Util.Global.Weather = Enviro.Weather.Snow;
                    Actions.Anim.AnimTest6();
                    GetMusicBackGround();
                }
                if (Util.Global.GetRandomInt(0, 100) > 80 && Util.Global.Weather == Enviro.Weather.Snow)
                {
                    Util.Global.Weather = Enviro.Weather.Clear;
                    new Objects.Maneuver().RemoveAll();
                    GetMusicBackGround();
                }
            }
            else
            {
                if (Util.Global.GetRandomInt(0, 100) < 20 && Util.Global.Weather == Enviro.Weather.Clear)
                {
                    Util.Global.Weather = Enviro.Weather.Rain;
                    Actions.Anim.AnimTest4();
                    GetMusicBackGround();
                }
                if (Util.Global.GetRandomInt(0, 100) > 80 && Util.Global.Weather == Enviro.Weather.Rain)
                {
                    Util.Global.Weather = Enviro.Weather.Clear;
                    new Objects.Maneuver().RemoveAll();
                    GetMusicBackGround();
                }
            }
        }

        public static void MoveToNextSeason()
        {
            switch (Util.Global.Season)
            {
                case Enviro.SeasonType.SpringEarly:
                    Util.Global.Season = Enviro.SeasonType.Spring;
                    break;
                case Enviro.SeasonType.Spring:
                    Util.Global.Season = Enviro.SeasonType.SpringLate;
                    break;
                case Enviro.SeasonType.SpringLate:
                    Util.Global.Season = Enviro.SeasonType.SummerEarly;
                    Actions.Say.speak(Util.Global.Hero.ID, "It's starting to feel like Summer!");
                    Util.Global.Sprites.Where(x => x.name == "SeasonIcon").FirstOrDefault().modelname = "SummerIcon";
                    break;
                case Enviro.SeasonType.SummerEarly:
                    Util.Global.Season = Enviro.SeasonType.Summer;
                    Util.Global.Sprites.Where(x => x.name == "SeasonIcon").FirstOrDefault().color = Color.Red;
                    break;
                case Enviro.SeasonType.Summer:
                    Util.Global.Season = Enviro.SeasonType.SummerLate;
                    break;
                case Enviro.SeasonType.SummerLate:
                    Util.Global.Season = Enviro.SeasonType.FallEarly;
                    Actions.Say.speak(Util.Global.Hero.ID, "Looks like the leaves are changing!");
                    Util.Global.Sprites.Where(x => x.name == "SeasonIcon").FirstOrDefault().modelname = "FallIcon";
                    StartEarlyFall();
                    break;
                case Enviro.SeasonType.FallEarly:
                    Util.Global.Season = Enviro.SeasonType.Fall;
                    Util.Global.Sprites.Where(x => x.name == "SeasonIcon").FirstOrDefault().color = Color.Brown;
                    break;
                case Enviro.SeasonType.Fall:
                    Util.Global.Season = Enviro.SeasonType.FallLate;
                    Util.Global.Sprites.Where(x => x.name == "SeasonIcon").FirstOrDefault().color = Color.BurlyWood;
                    break;
                case Enviro.SeasonType.FallLate:
                    Util.Global.Season = Enviro.SeasonType.WinterEarly;
                    Actions.Say.speak(Util.Global.Hero.ID, "Brrrrrrrr it's getting cold out!");
                    Util.Global.Sprites.Where(x => x.name == "SeasonIcon").FirstOrDefault().modelname = "WinterIcon";
                    StartEarlyWinter();
                    break;
                case Enviro.SeasonType.WinterEarly:
                    Util.Global.Season = Enviro.SeasonType.Winter;
                    break;
                case Enviro.SeasonType.Winter:
                    Util.Global.Season = Enviro.SeasonType.WinterLate;
                    break;
                case Enviro.SeasonType.WinterLate:
                    Util.Global.Season = Enviro.SeasonType.SpringEarly;
                    Actions.Say.speak(Util.Global.Hero.ID, "Spring is here!");
                    Util.Global.Sprites.Where(x => x.name == "SeasonIcon").FirstOrDefault().modelname = "SpringIcon";
                    StartEarlySpring();
                    break;
            }

            
        }

        private static void RunEvents()
        {
            if (PreviousEventSecond != Util.Global.GameClock.TotalSeconds)
            {
                foreach (ActionEvents AE in Util.Global.ActionEvents.Where(x => x.Duration <= 0).ToList())
                {
                    foreach(ActionCall AC in AE.actionCall)
                    {
                        Util.Base.CallMethod(AC);
                    }
                    Util.Global.ActionEvents.RemoveAll(x => x.ID == AE.ID);
                }
                Util.Global.ActionEvents.Select(c => { c.Duration = c.Duration - 1; return c; }).ToList();


                if ((int)Util.Global.GameClock.TotalSeconds % 10 == 0)
                {
                    if (Enviro.GetTime() == Enviro.TimeName.Day)
                    {
                        Actions.Anim.AnimTest7();
                    }
                    else if (!Util.Global.Fighting && Util.Global.Weather == Enviro.Weather.Clear && (Enviro.GetTime() == Enviro.TimeName.Dusk || Enviro.GetTime() == Enviro.TimeName.Night))
                    {
                        for (int i = 0; i < Util.Global.GetRandomInt(2,12); i++)
                        {
                            Actions.Anim.firefly();
                        }
                    }
                }

                if ((int)Util.Global.GameClock.TotalSeconds % 60 == 0)
                {
                    if (!Enviro.InCave()) 
                    {
                        GetWeather();
                    }
                    CheckMusic();
                }

                if ((int)Util.Global.GameClock.TotalSeconds % Util.Global.GameClockRandomSpeech == 0)
                {
                    Say.SayRandomPetSpeech();
                    Util.Global.GameClockRandomSpeech = Util.Global.GetRandomInt(30, 599);
                }

                switch ((int)Util.Global.GameClock.TotalSeconds)
                {
                    case 60:
                        break;
                    case 120:
                        break;
                    case 180:
                        GetMusicBackGround();
                        break;
                    case 240:
                        break;
                    case 300:
                        Actions.Enemy.SpawnEnemy();
                        break;
                    case 360:
                        Items.ItemActions.ChangeTotemText();
                        break;
                    case 420:
                        GetMusicBackGround();
                        break;
                    case 480:
                        break;
                    case 540:
                        break;
                    case 599:
                        break;
                }
            }
            
            PreviousEventSecond = Util.Global.GameClock.TotalSeconds;
        }

        public static void CheckMusic()
        {
            if (Util.Global.SoundMute)
            {
                Util.Global.MusicBackGround.Stop();
                MediaPlayer.Stop();
            }
            else
            {
                if (MediaPlayer.State == MediaState.Stopped)
                {
                    GetMusic();
                    Util.Global.MusicChange = true;
                }
            }
        }

        public static void GetMusic()
        {
            MediaPlayer.Stop();

            if (!Util.Global.SoundMute)
            {
                Array values = Enum.GetValues(typeof(Enviro.Music));
                Util.Global.Music = Util.Global.ContentMan.Load<Song>("Music/" + values.GetValue(Util.Global.GetRandomInt(0, values.Length)).ToString());

                if (Util.Global.Fighting)
                    Util.Global.Music = Util.Global.ContentMan.Load<Song>("Music/fight");

                if (Util.Global.Sprites.Where(x => x.name == "ShopExit").Count() > 0)
                    Util.Global.Music = Util.Global.ContentMan.Load<Song>("Music/Shop");

                Util.Global.MusicChange = true;
            }
        }

        public static void GetMusicBackGround(string Music = null)
        {
            if (Util.Global.MusicBackGround != null && Util.Global.MusicBackGround.State == SoundState.Playing)
                Util.Global.MusicBackGround.Stop();

            if (Music != null)
                Util.Global.MusicBackGround = Util.Global.ContentMan.Load<SoundEffect>("Music/" + Music).CreateInstance();
            else if ((int)Util.Global.GameClock.TotalSeconds >= 180 && (int)Util.Global.GameClock.TotalSeconds < 420)
                Util.Global.MusicBackGround = Util.Global.ContentMan.Load<SoundEffect>("Music/night").CreateInstance();
            else
                Util.Global.MusicBackGround = Util.Global.ContentMan.Load<SoundEffect>("Music/naturesounds").CreateInstance();

            if (Util.Global.Weather == Enviro.Weather.Rain)
                Util.Global.MusicBackGround = Util.Global.ContentMan.Load<SoundEffect>("Music/rain").CreateInstance();

            if (Enviro.InCave())
                Util.Global.MusicBackGround = Util.Global.ContentMan.Load<SoundEffect>("Music/cave").CreateInstance();

            Util.Global.MusicBackGround.IsLooped = true;
    
            if (!Util.Global.SoundMute)
                Util.Global.MusicBackGround.Play();
        }
    }
}
