using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game.Actions;

namespace Game.Actions
{
    public static class Enemy
    {
        public enum EnemyType { LightBug, Ant, Worm, Bee, Beetle, Fly, Moth, Frog, Salamander, Lizard, Snake, Turtle, Alligator, Chameleon }

        public static void SpawnEnemy()
        {
            if (!Util.Global.Fighting)
            {
                Vector3 Distance = Vector3.Subtract(new Vector3(50, 50, 0), Util.Global.CurrentMap);
                int EnemyLevel = (int)Math.Abs(Distance.X) + (int)Math.Abs(Distance.Y);

                bool SpawnLoc = true;
                int x = 0;
                int y = 0;
                while (SpawnLoc)
                {
                    x = Util.Global.GetRandomInt(5, 95);
                    y = Util.Global.GetRandomInt(5, 95);
                    Objects.Sprite2d Tile = Util.Global.Sprites.Where(z => z.name == x.ToString() + ":" + y.ToString()).FirstOrDefault();
                    if (Tile.modelname == "grass0")
                    {
                        SpawnLoc = false;
                    }
                }
                Vector2 Pos = Util.Global.Sprites.Where(z => z.name == x.ToString() + ":" + y.ToString()).FirstOrDefault().Position;
                Objects.Sprite2d En = Actions.Enemy.GetEnemyByLevel(EnemyLevel, Pos);
                Util.Global.Sprites.Add(En);
                Util.Base.Log("Spawn Enemy:" + En.ID.ToString() + " | " + Pos.X.ToString() + "-" + Pos.Y.ToString());
            }
        }

        public static void SpawnEnemy(Vector2 Pos)
        {
            Vector3 Distance = Vector3.Subtract(new Vector3(50, 50, 0), Util.Global.CurrentMap);
            int EnemyLevel = (int)Math.Abs(Distance.X) + (int)Math.Abs(Distance.Y);
            Objects.Sprite2d En = Actions.Enemy.GetEnemyByLevel(EnemyLevel, Pos);
            Util.Global.Sprites.Add(En);
            Util.Base.Log("Spawn Enemy Loc:" + En.ID.ToString() + " | " + Pos.X.ToString() + "-" + Pos.Y.ToString());
        }

        public static Objects.Sprite2d GetEnemyByLevel(int level, Vector2 Position)
        {
            EnemyType ET = new EnemyType();
            ET = (EnemyType)level;
            return GetEnemyByType(ET, Position);
        }

        public static Objects.Sprite2d GetEnemyByType(EnemyType ET, Vector2 Position)
        {
            string imagename = ET.ToString().ToLower();
            Objects.Sprite2d S = new Objects.Sprite2d(imagename, Guid.NewGuid().ToString(), true, Position, new Vector2(50, 50), 10, Objects.Base.ControlType.AI);
            S.orderNum = 899;
            S.clipping = true;
            S.speed = 5 + (int)ET;
            Objects.Actor actor = new Objects.Actor();
            actor.Level = (int)ET;
            actor.Health = 5 * (int)ET;
            actor.actorType = Objects.Actor.ActorType.Enemy;
            actor.enemyType = ET;
            actor.Buffs = new List<Buff.BuffType>();
            actor.Parents = new List<EnemyType>();
            actor.Drops = new List<Tuple<Items.Item.ItemType, float>>();
            S.Actor = actor;
            if (Util.Global.GetRandomInt(1, 100) > 80)
            {
                actor.Level = (int)ET + 1;
                S.color = Util.Colors.GetRandom();
                S.LightIgnor = true;
                S.Size = new Vector2(65, 65);
            }
            S.Actor.CalculateStats();

            List<Object> Objs2 = new List<object>();
            Objs2.Add(S);
            ActionCall call2 = new ActionCall(ActionType.MouseAny, typeof(Menu.ActorStats), "ToggleStats", Objs2);
            S.actionCall.Add(call2);
            List<Object> Objs3 = new List<object>();
            Objs3.Add(S);
            S.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Actions.Fight), "DisplayFight", Objs3));

            S.Actor.Drops.AddRange(GetCoinDrop((int)ET));
            return AddAbility(S);
        }

        public static List<Tuple<Items.Item.ItemType, float>> GetCoinDrop(int Level)
        {
            List<Tuple<Items.Item.ItemType, float>> Return = new List<Tuple<Items.Item.ItemType, float>>();
            for (int i = 0; i <= Level; i++)
            {
                Return.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.Coin, .5F));
                Return.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.Health, .5F));
            }
            return Return;
        }

        public static Objects.Sprite2d AddAbility(Objects.Sprite2d Enemy)
        {
            EnemyType ET = Enemy.Actor.enemyType;
            switch (ET)
            {
                case EnemyType.LightBug:
                    Enemy.speed = 10;
                    Enemy.LightSourceDistance = 100f;
                    Enemy.Actor.Level = 1;
                    Enemy.Actor.CalculateStats();
                    Enemy.Actor.Health = 5;
                    Enemy.Actor.HealthMax = 10;
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.Oil, 1F));
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.Oil, .5F));
                    break;
                case EnemyType.Ant:
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.Oil, 1F));
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.Oil, .3F));
                    Enemy.Actor.Parents.Add(EnemyType.LightBug);
                    break;
                case EnemyType.Worm:
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.Oil, .5F));
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.Health, .5F));
                    Enemy.Actor.Parents.Add(EnemyType.Ant);
                    break;
                case EnemyType.Bee:
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.Oil, .5F));
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.Health, .3F));
                    Enemy.Actor.Parents.Add(EnemyType.LightBug);
                    Enemy.Actor.Parents.Add(EnemyType.Ant);
                    break;
                case EnemyType.Beetle:
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.Oil, .3F));
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.SeedStrength, .5F));
                    Enemy.Actor.Parents.Add(EnemyType.Worm);
                    Enemy.Actor.Parents.Add(EnemyType.Bee);
                    break;
                case EnemyType.Fly:
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.Oil, .3F));
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.SeedSpeed, .5F));
                    Enemy.Actor.Parents.Add(EnemyType.Ant);
                    Enemy.Actor.Parents.Add(EnemyType.Worm);
                    break;
                case EnemyType.Moth:
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.SeedTree, .5F));
                    Enemy.Actor.Parents.Add(EnemyType.LightBug);
                    Enemy.Actor.Parents.Add(EnemyType.Ant);
                    Enemy.Actor.Parents.Add(EnemyType.Beetle);
                    break;
                case EnemyType.Frog:
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.SeedHealth, .5F));
                    Enemy.Actor.Parents.Add(EnemyType.Fly);
                    break;
                case EnemyType.Salamander:
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.IronBar, .3F));
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.SeedHealth, .5F));
                    Enemy.Actor.Parents.Add(EnemyType.Moth);
                    Enemy.Actor.Parents.Add(EnemyType.Frog);
                    break;
                case EnemyType.Lizard:
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.IronBar, .3F));
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.SeedHealth, .5F));
                    Enemy.Actor.Parents.Add(EnemyType.Beetle);
                    Enemy.Actor.Parents.Add(EnemyType.Frog);
                    break;
                case EnemyType.Snake:
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.IronBar, .3F));
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.SeedHealth, .5F));
                    Enemy.Actor.Parents.Add(EnemyType.Salamander);
                    Enemy.Actor.Parents.Add(EnemyType.Bee);
                    break;
                case EnemyType.Turtle:
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.IronBar, .3F));
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.SeedHealth, .5F));
                    Enemy.Actor.Parents.Add(EnemyType.Lizard);
                    Enemy.Actor.Parents.Add(EnemyType.Ant);
                    Enemy.Actor.Parents.Add(EnemyType.Beetle);
                    break;
                case EnemyType.Alligator:
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.IronBar, .3F));
                    Enemy.Actor.Drops.Add(new Tuple<Items.Item.ItemType, float>(Items.Item.ItemType.SeedHealth, .5F));
                    Enemy.Actor.Parents.Add(EnemyType.Frog);
                    Enemy.Actor.Parents.Add(EnemyType.Snake);
                    Enemy.Actor.Parents.Add(EnemyType.Worm);
                    break;
                case EnemyType.Chameleon:
                    Enemy.Actor.Parents.Add(EnemyType.Frog);
                    Enemy.Actor.Parents.Add(EnemyType.Snake);
                    Enemy.Actor.Parents.Add(EnemyType.Salamander);
                    Enemy.Actor.Parents.Add(EnemyType.Moth);
                    break;
            }

            return Enemy;
        }
    }
}
