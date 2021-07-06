using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game.Actions
{
    public static class Stable
    {
        public static void EnterStable()
        {
            if (Util.Global.Sprites.Where(x => x.Item != null && x.Item.State == Items.Item.ItemState.Hand).Count() == 0)
            {

                Util.Global.FightPreviousMap = Util.Global.CurrentMap;
                Util.Global.FightPreviousHeroLocation = new Vector2(Util.Global.Hero.Position.X, Util.Global.Hero.Position.Y);

                Vector3 mapVector = new Vector3(98, 98, 98);

                new Maps.Map().GenerateMap(mapVector, Game.Maps.MapData.Biome.Stable, 25, 25);
                new Maps.Map().WarpMap(mapVector, Util.Global.CurrentMap);

                Objects.Sprite2d ReturnDoor = new Objects.Sprite2d("doorway", "StableExit", true, new Vector2(300, 300), new Vector2(596, 640) / 7, 0, Objects.Base.ControlType.None);
                ReturnDoor.orderNum = 100;
                ReturnDoor.Position = Util.Global.Sprites.Where(x => x.name == "0:12").FirstOrDefault().Position;
                ReturnDoor.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseAny, typeof(Actions.Stable), "ExitStable", null));
                ReturnDoor.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Actions.Stable), "ExitStable", null));
                Util.Global.Sprites.Add(ReturnDoor);
                Util.Base.GetLightSources();

                Vector2 BasePos = new Vector2(200, 200);
                int i = 5;
                int y = 5;
                foreach (Objects.Sprite2d S in Util.Global.Stable)
                {
                    S.Actor.actorType = Objects.Actor.ActorType.Pet;
                    S.Actor.aiState = Objects.Actor.AiState.Stabled;
                    S.actionCall.RemoveAll(x => x.actionMethodName == "DisplayFight");
                    S.Position = Util.Global.Sprites.Where(x => x.name == i.ToString() + ":" + y.ToString()).FirstOrDefault().Position;
                    Util.Global.Sprites.Add(S);
                    y += 5;
                    if (y > 20)
                    {
                        y = 5; i += 5;
                    }
                }

                Util.Global.Sprites.Where(x => x.ID == Util.Global.Hero.ID).FirstOrDefault().Position = new Vector2(100, 100);

                Vector2 BasPos = new Vector2(-200, 0);
                foreach (Objects.Sprite2d S in Util.Global.Pets)
                {
                    Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Actor.aiState = Objects.Actor.AiState.Figting;
                    Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Position = BasPos;
                    BasPos = Vector2.Add(BasPos, new Vector2(0, 100));
                }
            }
        }

        public static void ExitStable()
        {
            new Maps.Map().WarpMap(Util.Global.FightPreviousMap, new Vector3(98, 98, 98));
            Util.Global.Sprites.Where(x => x.ID == Util.Global.Hero.ID).FirstOrDefault().Position = Util.Global.FightPreviousHeroLocation;
            foreach (Objects.Sprite2d S in Util.Global.Pets)
            {
                Util.Global.Sprites.Where(X => X.ID == S.ID).FirstOrDefault().Position = Vector2.Subtract(Util.Global.FightPreviousHeroLocation, new Vector2((float)Util.Global.GetRandomInt(1, 150), (float)Util.Global.GetRandomInt(1, 150)));
                Util.Global.Sprites.Where(X => X.ID == S.ID).FirstOrDefault().Actor.aiState = Objects.Actor.AiState.Chasing;
            }
        }


        public static void JoinParty(Objects.Sprite2d Pet)
        {
            Util.Global.Stable.Remove(Pet);
            Pet.Actor.actorType = Objects.Actor.ActorType.Pet;
            Pet.Actor.aiState = Objects.Actor.AiState.Chasing;
            Pet.clipping = false;
            Pet.actionCall = new List<ActionCall>();

            List<Object> Objs2 = new List<object>();
            Objs2.Add(Pet);

            ActionCall call2 = new ActionCall(ActionType.MouseAny, typeof(Menu.ActorStats), "ToggleStats", Objs2);
            Pet.actionCall.Add(call2);
            Util.Global.Pets.Add(Pet);

            Menu.ActorStats.RemoveStats(Pet);

            Util.Global.Sprites.RemoveAll(x => x.name == "StableToPet");

            Vector2 BasPos = new Vector2(-200, 0);
            foreach (Objects.Sprite2d S in Util.Global.Pets)
            {
                Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Actor.aiState = Objects.Actor.AiState.Figting;
                Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Position = BasPos;
                BasPos = Vector2.Add(BasPos, new Vector2(0, 100));
            }

            Say.speak(Pet.ID, Say.GetSpeech(Objects.Actor.ActorType.Pet, SpeechType.MakePet, Pet.Actor.enemyType));

        }

        public static void LeaveParty(Objects.Sprite2d Pet)
        {
            Util.Global.Pets.RemoveAll(x => x.ID == Pet.ID);
            Util.Global.Stable.Add(Pet);
            Pet.Actor.aiState = Objects.Actor.AiState.Stabled;
            Pet.clipping = true;
            Pet.Position = new Vector2(Util.Global.GetRandomInt(200, 400), Util.Global.GetRandomInt(200, 400));
            Pet.actionCall = new List<ActionCall>();
            List<Object> Objs2 = new List<object>();
            Objs2.Add(Pet);
            ActionCall call2 = new ActionCall(ActionType.MouseAny, typeof(Menu.ActorStats), "ToggleStats", Objs2);
            Pet.actionCall.Add(call2);
            Menu.ActorStats.RemoveStats(Pet);
            Util.Global.Sprites.RemoveAll(x => x.name == "PetToStable");
        }

    }
}
