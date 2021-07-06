using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game.Actions
{
    public static class Season
    {
        private static double PreviousEventSecond { set; get; }
        public enum Weather {Clear, Rain, Snow}
        public enum SeasonType {SpringEarly, Spring, SpringLate, SummerEarly, Summer, SummerLate, FallEarly, Fall, FallLate, WinterEarly, Winter, WinterLate  }

        public static void AddTime()
        {
            Util.Global.GameClock = Util.Global.GameClock.Add(new TimeSpan(0, 0, 20)); 
        }

        public static void StartEarlyWinter()
        {
            Util.Global.Sprites.Where(x => x.model != null && x.model.Name == "grass0").Select(y => { y.model = new Maps.Asset().getContentByName("snow"); return y; }).ToList();
            Util.Global.Season = SeasonType.WinterEarly;
        }

        public static void GetWeather()
        {
            if (Util.Global.Season == SeasonType.WinterEarly || Util.Global.Season == SeasonType.Winter || Util.Global.Season == SeasonType.WinterLate)
            {
                if (Util.Global.GetRandomInt(0, 100) < 20 && Util.Global.Weather == Weather.Clear)
                {
                    Util.Global.Weather = Weather.Snow;
                    Actions.Anim.AnimTest6();
                }
                if (Util.Global.GetRandomInt(0, 100) > 80 && Util.Global.Weather == Weather.Snow)
                {
                    Util.Global.Weather = Weather.Clear;
                    new Objects.Maneuver().RemoveAll();
                }
            }
            else
            {
                if (Util.Global.GetRandomInt(0, 100) < 20 && Util.Global.Weather == Weather.Clear)
                {
                    Util.Global.Weather = Weather.Rain;
                    Actions.Anim.AnimTest4();
                }
                if (Util.Global.GetRandomInt(0, 100) > 80 && Util.Global.Weather == Weather.Rain)
                {
                    Util.Global.Weather = Weather.Clear;
                    new Objects.Maneuver().RemoveAll();
                }
            }
        }

        public static void UpdateClock(GameTime gameTime)
        {
            if (Util.Global.Sprites.Where(x => x.name == "Clock").Count() < 1)
            {
                Util.Global.Sprites.Add(new Objects.Sprite2d(null, "Clock", true, new Vector2(0, 0), new Vector2(0, 0), 0, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == "Clock").FirstOrDefault().textSize = 1f;
                Util.Global.Sprites.Where(x => x.name == "Clock").FirstOrDefault().color = Color.Red;
                Util.Global.Sprites.Where(x => x.name == "Clock").FirstOrDefault().boxColor = Color.Blue;
                Util.Global.Sprites.Where(x => x.name == "Clock").FirstOrDefault().orderNum = 980;
            }
            Util.Global.Sprites.Where(x => x.name == "Clock").FirstOrDefault().text = 
                    "Day:" + Util.Global.GameClockDay.ToString() + "\n" +
                    Util.Global.Season.ToString() + "\n" +
                    Util.Global.GameClock.TotalSeconds.ToString();


            if (Util.Global.GameClockPreviousSecond != gameTime.TotalGameTime.Seconds)
            {
                Util.Global.GameClockPreviousSecond = gameTime.TotalGameTime.Seconds;
                Util.Global.GameClock = Util.Global.GameClock.Add(new TimeSpan(0, 0, 1)); 
            }
            if (Util.Global.GameClock.Minutes == 10)
            {
                Util.Global.GameClock = new TimeSpan(0);
                Util.Global.GameClockDay++;
                if (Util.Global.GameClockDay % 5 == 0)
                {
                    MoveToNextSeason();
                }

            }

            float F = (float)Util.Base.GetBellCurvePoint((Util.Global.GameClock.Minutes * .1f), 1f);
            Util.Global.DayColor = Util.Colors.LerpColor(Color.White, Color.Black, F);
            if (Util.Global.DayColor.G < 152)
            { Util.Base.GetLightSources(); }

            RunEvents();
        }

        public static void MoveToNextSeason()
        {
            switch (Util.Global.Season)
            {
                case SeasonType.SpringEarly:
                    Util.Global.Season = SeasonType.Spring;
                    break;
                case SeasonType.Spring:
                    Util.Global.Season = SeasonType.SpringLate;
                    break;
                case SeasonType.SpringLate:
                    Util.Global.Season = SeasonType.SummerEarly;
                    break;
                case SeasonType.SummerEarly:
                    Util.Global.Season = SeasonType.Summer;
                    break;
                case SeasonType.Summer:
                    Util.Global.Season = SeasonType.SummerLate;
                    break;
                case SeasonType.SummerLate:
                    Util.Global.Season = SeasonType.FallEarly;
                    break;
                case SeasonType.FallEarly:
                    Util.Global.Season = SeasonType.Fall;
                    break;
                case SeasonType.Fall:
                    Util.Global.Season = SeasonType.FallLate;
                    break;
                case SeasonType.FallLate:
                    Util.Global.Season = SeasonType.WinterEarly;
                    break;
                case SeasonType.WinterEarly:
                    Util.Global.Season = SeasonType.Winter;
                    break;
                case SeasonType.Winter:
                    Util.Global.Season = SeasonType.WinterLate;
                    break;
                case SeasonType.WinterLate:
                    Util.Global.Season = SeasonType.SpringEarly;
                    break;
            }
        }

        public static void RunEvents()
        {
            if (PreviousEventSecond != Util.Global.GameClock.TotalSeconds)
            {
                switch ((int)Util.Global.GameClock.TotalSeconds)
                {
                    case 60:
                        GetWeather();
                        break;
                    case 120:
                        GetWeather();
                        break;
                    case 180:
                        GetWeather();
                        break;
                    case 240:
                        GetWeather();
                        break;
                    case 300:
                        GetWeather();
                        break;
                    case 360:
                        GetWeather();
                        break;
                    case 420:
                        GetWeather();
                        break;
                    case 480:
                        GetWeather();
                        break;
                    case 540:
                        GetWeather();
                        break;
                    case 599:
                        GetWeather();
                        break;
                }
            }
            
            PreviousEventSecond = Util.Global.GameClock.TotalSeconds;
        }
        

    }
}
