using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Game.Util
{
    public static class Global
    {
        public static int screenSizeHeight { get; set; }
        public static int screenSizeWidth { get; set; }
        public static int WindowBoarder { get; set; }
        public static TimeSpan GameClock { get; set; }
        public static bool Fighting { get; set; }
        public static List<Objects.Spite2d> Sprites { get; set; }
        public static List<Objects.AnimSprite> SpritesAnim { get; set; }
        public static List<Objects.Text> Texts { get; set; }
        public static List<Objects.Menu> Menus { get; set; }
        public static List<Actions.Anim> Anim { get; set; }
        public static ContentManager ContentMan { get; set; }

        public static Objects.Spite2d GameMouse { get; set; }
        public static Objects.Spite2d GameBackground { get; set; }
        public static Objects.Spite2d GameFightBackground { get; set; }

        public static List<Texture2D> AllTexture2D { get; set; }

        public static Actors.Hero Hero { get; set; }

        public static Maps.Map[, ,] MainMap { get; set; }
        public static Vector3 CurrentMap { get; set; }
    }
}

