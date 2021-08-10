using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game.Maps
{
    public class Dungeon
    {
        public static void WarpDungeon()
        {
            try
            {
                Util.Global.DungeonPreviousMap = Util.Global.CurrentMap;
                Util.Global.DungeonPreviousHeroLocation = new Vector2(Util.Global.Hero.Position.X, Util.Global.Hero.Position.Y);

                Vector3 mapVector = Vector3.Add(Util.Global.CurrentMap, new Vector3(0, 0, 10));

                new Maps.Map().GenerateMap(mapVector, Game.Maps.MapData.Biome.Dungeon, 50, 50);
                new Maps.Map().WarpMap(mapVector, Util.Global.CurrentMap);

                Objects.Sprite2d ReturnDoor = new Objects.Sprite2d("doorway", "DungeonExit", true, new Vector2(0, 0), new Vector2(596, 640) / 8, 0, Objects.Base.ControlType.None);
                ReturnDoor.orderNum = 500;
                ReturnDoor.LightIgnor = true;
                //ReturnDoor.Position = Vector2.Add(Util.Global.Sprites.Where(x => x.name == "0:4").FirstOrDefault().Position, new Vector2(6, -50));
                ReturnDoor.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseAny, typeof(Maps.Dungeon), "ExitDungeon", null));
                Util.Global.Sprites.Add(ReturnDoor);
                Actions.Season.GetMusic();
                Util.Global.Sprites.Where(x => x.ID == Util.Global.Hero.ID).FirstOrDefault().Position = Util.Global.Sprites.Where(y => y.name == "1:1").FirstOrDefault().Position;
                new Maps.Map().WarpPets();
            }
            catch(Exception ex)
            {
                Util.Base.Log("Warp dungeon Fail");
            }
        }

        public static void ExitDungeon()
        {
            Util.Global.DrawDistance = 500;
            new Maps.Map().WarpMap(Util.Global.DungeonPreviousMap, new Vector3(97, 97, 97));
            Util.Global.Sprites.Where(x => x.ID == Util.Global.Hero.ID).FirstOrDefault().Position = Util.Global.DungeonPreviousHeroLocation;
            Util.Global.Fighting = false;

            Actions.Season.GetMusic();
        }
    }
}
