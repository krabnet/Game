using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game.Actions
{
    public static class Cave
    {
        public static void EnterCave()
        {
            Util.Global.CavePreviousMap = Util.Global.CurrentMap;
            Util.Global.CavePreviousHeroLocation = new Vector2(Util.Global.Hero.Position.X, Util.Global.Hero.Position.Y);

            Vector3 mapVector = new Vector3(Util.Global.CurrentMap.X, Util.Global.CurrentMap.Y, Util.Global.CurrentMap.Z+1);

            if (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z] == null)
            {
                new Maps.Map().GenerateMap(mapVector, Game.Maps.MapData.Biome.Cave, Util.Global.SizeMap, Util.Global.SizeMap);
                List<Objects.Sprite2d> spritsintheway = Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Where(l => Util.Base.collision(l, new Rectangle((int)Util.Global.CavePreviousHeroLocation.X - 250, (int)Util.Global.CavePreviousHeroLocation.Y - 250, 1, 1)) == true && l.name.Contains(":")).ToList();
                Vector2 EntrancePos = new Vector2(Util.Global.SizeMap / 2, Util.Global.SizeMap / 2);
                if (spritsintheway.Count() > 0)
                    EntrancePos = Fight.GetBoardPositionFromName(spritsintheway.FirstOrDefault().name);
                

                Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].AddMapPart(Game.Maps.MapData.MapPart.caveEntrance, (int)EntrancePos.X, (int)EntrancePos.Y, Items.Item.ItemType.Wall);
                Objects.Sprite2d ReturnDoor = Items.Item.GetItemByType(Items.Item.ItemType.Ladder, Util.Global.DefaultPosition);
                ReturnDoor.active = true;
                ReturnDoor.name = "CaveExit";
                ReturnDoor.orderNum = 100;
                ReturnDoor.LightIgnor = true;
                ReturnDoor.Position = Util.Global.Sprites.Where(x => x.ID == Util.Global.Hero.ID).FirstOrDefault().Position;
                ReturnDoor.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseAny, typeof(Actions.Cave), "ExitCave", null));
                Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Add(ReturnDoor);
            }
            new Maps.Map().WarpMap(mapVector, Util.Global.CurrentMap);
            //Make sure there is no Clip in this location
            Util.Global.Sprites.Where(x => x.ID == Util.Global.Hero.ID).FirstOrDefault().Position = Util.Global.Sprites.Where(x => x.name == "CaveExit").FirstOrDefault().Position;


        }
        public static void ExitCave()
        {
            new Maps.Map().WarpMap(Util.Global.CavePreviousMap, new Vector3(98, 98, 98));
            Util.Global.Sprites.Where(x => x.ID == Util.Global.Hero.ID).FirstOrDefault().Position = Util.Global.CavePreviousHeroLocation;
            foreach (Objects.Sprite2d S in Util.Global.Pets)
            {
                Util.Global.Sprites.Where(X => X.ID == S.ID).FirstOrDefault().Position = Vector2.Subtract(Util.Global.CavePreviousHeroLocation, new Vector2((float)Util.Global.GetRandomInt(1, 150), (float)Util.Global.GetRandomInt(1, 150)));
                Util.Global.Sprites.Where(X => X.ID == S.ID).FirstOrDefault().Actor.aiState = Objects.Actor.AiState.Chasing;
            }
        }
    }
}
