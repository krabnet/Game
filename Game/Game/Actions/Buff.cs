using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game.Actions
{
    public static class Buff
    {
        public enum BuffType { Starvation, Strength, Speed, Dexterity, Endurance, Health, Feeding, Blindness }

        public static Objects.Sprite2d GetBuffByType(BuffType T)
        {
            Objects.Sprite2d ReturnSprite = new Objects.Sprite2d();
            switch(T)
            {
                case BuffType.Starvation:
                    ReturnSprite = GetBuffSprite("starve");
                    break;
                case BuffType.Strength:
                    ReturnSprite = GetBuffSprite("strength");
                    break;
                case BuffType.Speed:
                    ReturnSprite = GetBuffSprite("speed");
                    break;
                case BuffType.Endurance:
                    ReturnSprite = GetBuffSprite("endurance");
                    break;
                case BuffType.Blindness:
                    ReturnSprite = GetBuffSprite("blindness");
                    break;
                case BuffType.Dexterity:
                    ReturnSprite = GetBuffSprite("dexterity");
                    break;
            }
            return ReturnSprite;
        }

        public static Objects.Sprite2d GetBuffSprite(string Texture2dName)
        {
            Vector2 Position = Util.Global.DefaultPosition;
            Objects.Sprite2d SpriteItem = new Objects.Sprite2d(Texture2dName, Texture2dName, true, Position, new Vector2(25, 25), 0, Objects.Base.ControlType.None);
            SpriteItem.orderNum = 100;
            SpriteItem.SpriteType = Objects.Base.Type.Tile;

            return SpriteItem;
        }

        public static void AddBuff(Objects.Sprite2d Actor, BuffType T, int Duration)
        {
            if (Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Actor.Buffs.Where(x => x == T).Count() == 0)
            {
                Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Actor.Buffs.Add(T);
                Util.Base.Log("AddBuff: " + Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Actor.Buffs.FirstOrDefault().ToString());

                ActionEvents AE = new ActionEvents();
                List<object> obj1 = new List<object>();
                switch (T)
                {
                    case BuffType.Feeding:
                        int NewHunger = Convert.ToInt32(MathHelper.Clamp(Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Actor.Hunger + 900, 1, Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Actor.HungerMax));
                        Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Actor.Hunger = NewHunger;
                        obj1.Add(Actor);
                        obj1.Add(T);
                         
                        AE.Duration = Duration;
                        AE.InitialDuration = Duration;
                        Util.Global.ActionEvents.Add(AE);
                        break;
                    case BuffType.Speed:
                        Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().speed += 2;
                        obj1.Add(Actor);
                        obj1.Add(T);
                        AE.actionCall.Add(new ActionCall(ActionType.Item, typeof(Buff), "RemoveBuff", obj1));
                        AE.Duration = Duration;
                        AE.InitialDuration = Duration;
                        Util.Global.ActionEvents.Add(AE);
                        break;
                    case BuffType.Strength:
                        Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Actor.Str += 2;
                        obj1.Add(Actor);
                        obj1.Add(T);
                        AE.actionCall.Add(new ActionCall(ActionType.Item, typeof(Buff), "RemoveBuff", obj1));
                        AE.Duration = Duration;
                        AE.InitialDuration = Duration;
                        Util.Global.ActionEvents.Add(AE);
                        break;
                    case BuffType.Endurance:
                        Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Actor.End += 1;
                        obj1.Add(Actor);
                        obj1.Add(T);
                        AE.actionCall.Add(new ActionCall(ActionType.Item, typeof(Buff), "RemoveBuff", obj1));
                        AE.Duration = Duration;
                        AE.InitialDuration = Duration;
                        Util.Global.ActionEvents.Add(AE);
                        break;
                    case BuffType.Dexterity:
                        Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Actor.Dex += 1;
                        obj1.Add(Actor);
                        obj1.Add(T);
                        AE.actionCall.Add(new ActionCall(ActionType.Item, typeof(Buff), "RemoveBuff", obj1));
                        AE.Duration = Duration;
                        AE.InitialDuration = Duration;
                        Util.Global.ActionEvents.Add(AE);
                        break;
                    case BuffType.Health:
                        Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Actor.Health = Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Actor.HealthMax;
                        obj1.Add(Actor);
                        obj1.Add(T);
                        AE.actionCall.Add(new ActionCall(ActionType.Item, typeof(Buff), "RemoveBuff", obj1));
                        AE.Duration = Duration;
                        AE.InitialDuration = Duration;
                        Util.Global.ActionEvents.Add(AE);
                        break;
                    case BuffType.Blindness:
                        Util.Global.DrawDistance = 100;
                        obj1.Add(Actor);
                        obj1.Add(T);
                        AE.actionCall.Add(new ActionCall(ActionType.Item, typeof(Buff), "RemoveBuff", obj1));
                        AE.Duration = Duration;
                        AE.InitialDuration = Duration;
                        Util.Global.ActionEvents.Add(AE);
                        break;
                }
                Menu.ActorStats.UpdateStats(Actor);
                string Name = "AddBuff" + T.ToString() + Actor.ID.ToString();
                Objects.Sprite2d AddBuff = GetBuffByType(T);
                AddBuff.name = Name;
                Util.Global.Sprites.Add(AddBuff);
                List<Vector2> MOV = Actions.Anim.GetMovementCyclone(Actor.Position, 350);
                Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().Maneuver = new Objects.Maneuver(Actor.Position, new Vector2(25, 25), MOV, Util.ColorType.Random, false);
                Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().Maneuver.MovementSize = null;
            }

        }

        public static void RemoveBuff(Objects.Sprite2d Actor, BuffType T)
        {
            Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Actor.Buffs.Remove(T);
            
            switch (T)
            {
                case BuffType.Speed:
                    Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().speed -= 2;
                        break;
                case BuffType.Strength:
                        Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Actor.Str = Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Actor.StrMax;
                        break;
                case BuffType.Endurance:
                        Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Actor.End = Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Actor.EndMax;
                        break;
                case BuffType.Dexterity:
                        Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Actor.Dex = Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Actor.DexMax;
                        break;
                case BuffType.Blindness:
                        Util.Global.DrawDistance = 500;
                        break;
            }
            Menu.ActorStats.UpdateStats(Actor);
        }

    }
}
