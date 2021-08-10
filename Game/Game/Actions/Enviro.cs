using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Actions
{
    public static class Enviro
    {
        public enum Weather { Clear, Rain, Snow }
        public enum SeasonType { SpringEarly, Spring, SpringLate, SummerEarly, Summer, SummerLate, FallEarly, Fall, FallLate, WinterEarly, Winter, WinterLate }
        public enum Music { blobbit, commencement, explore, fairyspringtune, sombrero, town, musicauniversalis, FantasyOrchestralTheme, Minstrel_Dance }
        public enum DarkMusic { descent, dungeonblue, narrow_corridors_short }
        public enum TimeName { Dawn, Day, Dusk, Night }

        public static bool InCave()
        {
            if (Util.Global.CurrentMap.Z == 1 || Util.Global.CurrentMap.Z == 10)
                return true;
            else
                return false;
        }

        public static TimeName GetTime()
        {
            int TS = (int)Util.Global.GameClock.TotalSeconds;

            if (TS >= 0 && TS <= 120)
                return TimeName.Day;
            if (TS >= 120 && TS <= 240)
                return TimeName.Dusk;
            if (TS >= 240 && TS <= 360)
                return TimeName.Night;
            if (TS >= 360 && TS <= 480)
                return TimeName.Dawn;
            if (TS >= 480 && TS <= 600)
                return TimeName.Day;


            return TimeName.Day;
        }

        //public static bool isFighting()
        //{
        //    return Util.Global.Fighting;
        //}

    }
}
