using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Game.Util
{
    [Serializable]
    public static class Global
    {
        public const int SizeMap = 100;
        
        public static Vector3 StartLocation {get { return new Vector3(50, 50, 0); } }
        public static Vector2 DefaultPosition { get { return new Vector2(-1000, -1000); } }
        public static bool ScreenShotFlag { get; set; }
        public static string ScreenShotName { get; set; }
        public static RenderTarget2D ScreenShotRender { get; set; }
        public static GraphicsDevice graphicsDevice { get; set; }
        public static Util.Camera2D Cam;
        public static int DrawDistance { get; set; }
        public static int screenSizeHeight { get; set; }
        public static int screenSizeWidth { get; set; }
        public static int WindowBoarder { get; set; }
        public static Song Music { get; set; }
        public static SoundEffectInstance MusicBackGround { get; set; }
        public static bool MusicChange { get; set; }
        public static bool SoundMute { get; set; }

        public static float TorchLightDistance { get { return 75f; } }

        public static TimeSpan GameClock { get; set; }
        public static int UpdateClock { get; set; }
        public static int UpdateClockPreviousMili { get; set; }
        public static int GameClockPreviousSecond { get; set; }
        public static int GameClockDay { get; set; }
        public static int GameClockRandomSpeech { get; set; }
        public static Color DayColor { get; set; }
        public static List<GameMouseHistory> MouseHistory { get; set; }
        public static bool MouseHistoryFlag { get; set; }
        public static Actions.Enviro.Weather Weather { get; set; }
        public static Actions.Enviro.SeasonType Season { get; set; }
        public static List<Actions.ActionEvents> ActionEvents { get; set; }

        public static List<Objects.Sprite2d> Sprites { get; set; }
        public static ContentManager ContentMan { get; set; }
        public static List<Tuple<int, int, float>> Lights { get; set; }
        public static int counter { get; set; }

        public static SpriteFont font { get; set; }

        public static MouseState PreviousMouseState { get; set; }
        public static KeyboardState PreviousKeyboardState { get; set; }
        public static int GameMouseTime { get; set; }
        public static Objects.Sprite2d GameBackground { get; set; }
        public static bool RunFullScreenToggle { get; set; }
        public static bool QuitGameToggle { get; set; }
        public static bool PauseGameToggle { get; set; }

        public static bool Fighting { get; set; }
        public static bool FightAuto { get; set; }
        public static Objects.Sprite2d[,] FightBoard { get; set; }
        public static Objects.Sprite2d GameFightBackground { get; set; }
        public static Vector3 FightPreviousMap { get; set; }
        public static Vector2 FightPreviousHeroLocation { get; set; }
        public static List<Guid> Combatants { get; set; }
        public static int FightTurn { get; set; }

        public static List<Texture2D> AllTexture2D { get; set; }

        public static Objects.Sprite2d Hero { get; set; }
        public static Objects.Sprite2d Chest { get; set; }
        public static List<Objects.Sprite2d> Pets { get; set; }
        public static List<Objects.Sprite2d> Stable { get; set; }
        public static List<Items.Warp> Warp { get; set; }
        public static List<string> Journal { get; set; }

        public static Maps.Map[, ,] MainMap { get; set; }
        public static Vector3 CurrentMap { get; set; }
        public static Vector3 PreviousMap { get; set; }
        public static Vector2 PreviousMapPosition { get; set; }
        public static Random rnd { get; set; }

        public static Vector3 CavePreviousMap { get; set; }
        public static Vector2 CavePreviousHeroLocation { get; set; }

        public static List<Objects.Sprite2d> RainDrops { get; set; }

        public static int GetRandomInt(int s, int e)
        {
            return rnd.Next(s, e);
        }

        public static float GetRandomFloat(float s, float e)
        {
            return (float)rnd.NextDouble() * (e - s) + s; 
        }

        public static double GetRandomDouble()
        {
            return rnd.NextDouble();
        }
    }
}

