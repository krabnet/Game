using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Game.Actions
{
    public static class Fight
    {
        public enum FightActions { Right, Left, Up, Down, Attack, Defend, Run, Wait, Pass }

        public static void init()
        {
            new Maps.Map().GenerateMap(new Vector3(99, 99, 99), Game.Maps.MapData.Biome.Mini, 12, 15);
        }

        public static void DisplayFight(Objects.Sprite2d Enemy)
        {
            if (!Util.Global.Fighting)
            {
                Util.Global.Combatants = new List<Guid>();
                Util.Global.FightAuto = false;
                Util.Global.FightPreviousMap = Util.Global.CurrentMap;
                Util.Global.FightPreviousHeroLocation = new Vector2(Util.Global.Hero.Position.X, Util.Global.Hero.Position.Y);

                Items.ItemActions.DropItemInHand();
                Menu.Inventory.HideInventory();
                Menu.Craft.HideCraft();
                new Objects.Maneuver().RemoveAll();

                Util.Global.Sprites.RemoveAll(X => X.ID == Enemy.ID);
                Util.Global.MainMap[(int)Util.Global.CurrentMap.X, (int)Util.Global.CurrentMap.Y, (int)Util.Global.CurrentMap.Z].Sprite2d.RemoveAll(X => X.ID == Enemy.ID);
                //Util.Global.Sprites.RemoveAll(X => X.name == "Hero");
                
                Vector3 mapVector = new Vector3(99, 99, 99);
                new Maps.Map().GenerateMap(mapVector, Game.Maps.MapData.Biome.Mini , 12, 15);
                Util.Global.FightBoard = new Objects.Sprite2d[12, 15];

                new Maps.Map().WarpMap(mapVector, Util.Global.CurrentMap);

                Util.Global.Sprites.Where(y => y.modelname == "grass0").Select(x => { x.modelname = "grassline"; return x; }).ToList();
 

                Util.Global.Hero.modelname = "HeroRight";
                Util.Global.Hero.orderNum = 1000;

                
                Enemy.Actor.aiState = Objects.Actor.AiState.Figting;
                Enemy.actionCall.RemoveAll(x => x.actionMethodName == "DisplayFight");
                Util.Global.Sprites.Add(Enemy);
                
                int peti = 7;
                foreach(Objects.Sprite2d S in Util.Global.Pets)
                {   
                        Util.Global.FightBoard[peti, 2] = S;
                        Util.Global.Sprites.Where(X => X.ID == S.ID).FirstOrDefault().Actor.aiState = Objects.Actor.AiState.Figting;
                        Util.Global.Combatants.Add(Util.Global.Sprites.Where(X => X.ID == S.ID).FirstOrDefault().ID);
                        peti = peti - 2;
                }
                Util.Global.Combatants.Add(Util.Global.Hero.ID);
                Util.Global.Combatants.Add(Enemy.ID);
                Util.Global.Combatants = Util.Global.Combatants.OrderBy(a => Guid.NewGuid()).ToList();

                Menu.ActorStats.RemoveStats(Util.Global.Hero);
                Menu.ActorStats.RemoveStats(Enemy);

                Util.Global.FightBoard[9, 2] = Util.Global.Hero;
                Util.Global.FightBoard[7, 11] = Enemy;


                //Objects.Sprite2d Enemy2 = Actions.Enemy.GetEnemyByType(Enemy.Actor.enemyType, new Vector2(0,0));
                //Enemy2.Actor.aiState = Objects.Actor.AiState.Figting;
                //Enemy2.actionCall.RemoveAll(x => x.actionMethodName == "DisplayFight");
                //Util.Global.Sprites.Add(Enemy2);
                //Util.Global.Combatants.Add(Enemy2.ID);
                //Util.Global.FightBoard[5, 11] = Enemy2;

                PlaceCombatants();
                Util.Base.GetLightSources();
                Util.Global.Fighting = true;
                Actions.Season.GetMusic();
                Util.Global.Cam.Pos = new Vector2(200,50);
                Util.Global.FightTurn = -1;
                Turn();
            }
        }

        public static void ToggleAuto()
        {
            if (Util.Global.FightAuto)
                Util.Global.FightAuto = false;
            else
            {
                Util.Global.FightAuto = true;
                Menu.FightActions.RemoveFightMenu();
                AutoAction(Util.Global.Hero);
            }
            ShowAutoFightButton();
        }

        public static void ShowAutoFightButton()
        {
            Util.Global.Sprites.RemoveAll(x => x.name == "AutoAttack");
            Util.Global.Sprites.Add(new Objects.Sprite2d(null, "AutoAttack", true, new Vector2(10, 100), Util.Global.DefaultPosition, 0, Objects.Base.ControlType.None));
            Util.Global.Sprites.Where(x => x.name == "AutoAttack").FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
            Util.Global.Sprites.Where(x => x.name == "AutoAttack").FirstOrDefault().text = "Auto Attack";
            Util.Global.Sprites.Where(x => x.name == "AutoAttack").FirstOrDefault().textSize = 1f;
            if (Util.Global.FightAuto)
            {
                Util.Global.Sprites.Where(x => x.name == "AutoAttack").FirstOrDefault().color = Color.Green;
                Util.Global.Sprites.Where(x => x.name == "AutoAttack").FirstOrDefault().boxColor = Color.Blue;
            }
            else
            {
                Util.Global.Sprites.Where(x => x.name == "AutoAttack").FirstOrDefault().color = Color.Red;
                Util.Global.Sprites.Where(x => x.name == "AutoAttack").FirstOrDefault().boxColor = Color.Blue;
            }
            Util.Global.Sprites.Where(x => x.name == "AutoAttack").FirstOrDefault().orderNum = 980;
            ActionCall call2 = new ActionCall(ActionType.Mouse, typeof(Fight), "ToggleAuto", null);
            Util.Global.Sprites.Where(x => x.name == "AutoAttack").FirstOrDefault().actionCall.Add(call2);
        }

        public static void PlaceCombatants()
        {
            for (int x = 0; x < Util.Global.FightBoard.GetLength(0); x += 1)
            {
                for (int y = 0; y < Util.Global.FightBoard.GetLength(1); y += 1)
                {
                    if (Util.Global.FightBoard[x, y] != null)
                    {
                        Util.Global.Sprites.Where(P => P.ID == Util.Global.FightBoard[x, y].ID).FirstOrDefault().Position = Util.Global.Sprites.Where(I => I.name == x.ToString() + ":" + y.ToString()).FirstOrDefault().Position;
                    }

                }
            }
        }

        public static void BeginAttack(Objects.Sprite2d Attacker, Objects.Sprite2d Defender)
        {
            Menu.FightActions.RemoveFightMenu();
            string name = Actions.Anim.AttackAnim(Attacker, Defender);
            List<object> obj = new List<object>();
            obj.Add(Attacker);
            obj.Add(Defender);
            Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().Maneuver.FinalActionCall = new ActionCall(ActionType.Update, typeof(Fight), "Attack", obj);
        }

        public static void Attack(Objects.Sprite2d Attacker, Objects.Sprite2d Defender)
        {
            //List<Vector2> MOV = Actions.Anim.GetMovementJump(Util.Global.Sprites.Where(x => x.ID == Attacker.ID).FirstOrDefault().Position, Defender.Position, 10);
            //List<Vector2> MOV = Actions.Anim.GetMovementRandom(Util.Global.Sprites.Where(x => x.ID == Attacker.ID).FirstOrDefault().Position, 10);
            //Util.Global.Sprites.Where(x => x.ID == Attacker.ID).FirstOrDefault().Maneuver = new Objects.Maneuver(Util.Global.Sprites.Where(x => x.ID == Attacker.ID).FirstOrDefault().Position, Util.Global.Sprites.Where(x => x.ID == Attacker.ID).FirstOrDefault().Size, MOV, Util.ColorType.None, false);
            //Util.Global.Sprites.Where(x => x.ID == Attacker.ID).FirstOrDefault().Maneuver.MovementSize = Util.Sizes.GetSizeList(Util.SizeType.GrowShrink, Util.Global.Sprites.Where(x => x.ID == Attacker.ID).FirstOrDefault().Size, MOV.Count, 5, 3, 5);
            //string name = Actions.Anim.AttackAnim(Attacker.Position, Defender.Position);
            //Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().Maneuver.FinalActionCall = new ActionCall(ActionType.Update, typeof(Fight), "Turn", null);

            int Dmg = 0;
            if (Strike(Attacker, Defender))
            {
                Dmg = GetDamage(Attacker, Defender);
                Defender.Actor.Health = Defender.Actor.Health - Dmg;
                Util.Global.Sprites.Where(x => x.ID == Defender.ID).FirstOrDefault().Actor.Health = Defender.Actor.Health;
                Menu.ActorStats.UpdateStats(Defender);
                Actions.Anim.AnimText("-" + Dmg.ToString(), Defender.Position, 2, Color.Red);

                int Crit = GetCriticalDamage(Attacker);
                if (Crit > 0 && Defender.Actor.Health > 0)
                {
                    Defender.Actor.Health = Defender.Actor.Health - Crit;
                    Util.Global.Sprites.Where(x => x.ID == Defender.ID).FirstOrDefault().Actor.Health = Defender.Actor.Health;
                    Menu.ActorStats.UpdateStats(Defender);
                    Actions.Anim.AnimText("-" + Crit.ToString(), Defender.Position, 3, Color.Yellow);
                    Util.Global.ContentMan.Load<SoundEffect>("Sounds/Gong").Play();
                }

            }
            else
            {
                Actions.Anim.AnimText("miss", Defender.Position, 2, Color.Red);
            }

            if (Defender.Actor.Health <= 0)
            {
                if (Defender.Actor.actorType == Objects.Actor.ActorType.Hero)
                {
                    Util.Global.Fighting = false;
                    Util.Global.ContentMan.Load<SoundEffect>("Sounds/oooh").Play();
                    new Util.Main().Init();
                }

                if (Defender.Actor.actorType == Objects.Actor.ActorType.Pet)
                {
                    KillPet(Defender);
                }

                if (Defender.Actor.actorType == Objects.Actor.ActorType.Enemy)
                {
                    KillEnemy(Defender);

                    bool EnemyLeft = false;
                    foreach (Guid G in Util.Global.Combatants)
                    {
                        if (Util.Global.Sprites.Where(x => x.ID == G).Count() > 0)
                        {
                            if (Util.Global.Sprites.Where(x => x.ID == G).FirstOrDefault().Actor.actorType == Objects.Actor.ActorType.Enemy)
                            {
                                EnemyLeft = true;
                            }
                        }
                    }

                    if (!EnemyLeft)
                    {
                        Util.Global.ContentMan.Load<SoundEffect>("Sounds/slotwin").Play();
                        EndFight(Defender);
                    }
                }
            }
            Turn();
        }

        public static void Turn()
        {
            if (Util.Global.Fighting == true)
            {
                Util.Global.FightTurn = Util.Global.FightTurn + 1;
                if (Util.Global.FightTurn > Util.Global.Combatants.Count - 1)
                {Util.Global.FightTurn = 0;}
                Objects.Sprite2d Actor = Util.Global.Sprites.Where(x => x.ID == Util.Global.Combatants.ToArray()[Util.Global.FightTurn]).FirstOrDefault();
                if (Actor != null)
                {
                    UpdateActiveFighter();
                    if (Actor.Actor.actorType == Objects.Actor.ActorType.Hero || Actor.Actor.actorType == Objects.Actor.ActorType.Pet)
                    {
                        if (Util.Global.FightAuto)
                        {
                            AutoAction(Actor);
                        }
                        else
                        {
                            if (Actor.Actor.actorType == Objects.Actor.ActorType.Hero)
                            { ShowAutoFightButton(); }
                            else
                            { Util.Global.Sprites.RemoveAll(x => x.name == "AutoAttack"); }
                            Objects.Sprite2d Defender = Util.Global.Sprites.Where(x => Util.Global.Combatants.Contains(x.ID) && x.Actor.actorType == Objects.Actor.ActorType.Enemy).FirstOrDefault();
                            Menu.FightActions.DisplayFightMenu(new Vector2(-20, 100), Actor, Defender);
                        }

                    }
                    if (Actor.Actor.actorType == Objects.Actor.ActorType.Enemy)
                    {
                        AutoAction(Actor);
                        if (Util.Global.GetRandomInt(0, 10) < 2)
                        {
                            Say.speak(Actor.ID, Say.GetSpeech(Objects.Actor.ActorType.Enemy, SpeechType.Random, Actor.Actor.enemyType));
                        }
                    }
                }
            }
        }

        private static void UpdateActiveFighter()
        {
            Util.Global.Sprites.RemoveAll(x => x.name == "ActiveFighter");
            Objects.Sprite2d Actor = Util.Global.Sprites.Where(x => x.ID == Util.Global.Combatants.ToArray()[Util.Global.FightTurn]).FirstOrDefault();
            if (Actor != null)
            {
                Util.Global.Sprites.Add(new Objects.Sprite2d("drop", "ActiveFighter", true, Actor.Position, Actor.Size, 0, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == "ActiveFighter").FirstOrDefault().color = Color.Yellow * 0.5f;
                Util.Global.Sprites.Where(x => x.name == "ActiveFighter").FirstOrDefault().orderNum = 500;
                Util.Global.Sprites.Where(x => x.name == "ActiveFighter").FirstOrDefault().LightIgnor = true;
            }
        }

        private static Objects.Sprite2d GetDefender()
        {
            Objects.Sprite2d Defender = new Objects.Sprite2d();
            List<Objects.Sprite2d> Defenders = new List<Objects.Sprite2d>();
            Defenders.AddRange(Util.Global.Sprites.Where(x => Util.Global.Combatants.Contains(x.ID) && x.Actor.actorType != Objects.Actor.ActorType.Enemy));

            Defender = Defenders.OrderBy(x => x.Actor.Level).FirstOrDefault();

            return Defender;
        }

        private static bool Strike(Objects.Sprite2d Attacker, Objects.Sprite2d Defender)
        {
            int Hchnace = Attacker.Actor.Dex * Util.Global.GetRandomInt(Attacker.Actor.Level, Attacker.Actor.Level * 11);
            int Echance = Defender.Actor.Agl + Util.Global.GetRandomInt(Defender.Actor.Level, Defender.Actor.Level * 10);
            if (Hchnace > Echance)
            {
                return true;
            }
            return false;
        }

        private static int GetCriticalDamage(Objects.Sprite2d Attacker)
        {
            float rtn = 0;
            int Rnd = Util.Global.GetRandomInt(1, 100);
            if (Rnd == Attacker.Actor.Agl || Rnd == Attacker.Actor.Agl + 1 || Rnd == Attacker.Actor.Agl - 1 || Rnd == Attacker.Actor.Agl + 2 || Rnd == Attacker.Actor.Agl - 2)
            {
                rtn = rtn + Util.Global.GetRandomInt(1, Attacker.Actor.Agl);
            }
            return (int)rtn;
        }

        private static int GetDamage(Objects.Sprite2d Attacker, Objects.Sprite2d Defender)
        {
            Random rnd = new Random();
            float rtn = 0;
            rtn = (Attacker.Actor.Str - Defender.Actor.End) + Util.Global.GetRandomInt(Attacker.Actor.Level, Attacker.Actor.Level * 4);
            rtn = MathHelper.Clamp(rtn, 0, Util.Global.GetRandomInt(Attacker.Actor.Level, Attacker.Actor.Level * 2));
            return (int)rtn;
        }

        public static void EndFight(Objects.Sprite2d Enemy)
        {
            Util.Global.Sprites.Where(x => x.name == "Hero").FirstOrDefault().active = true;
            Util.Global.Sprites.Remove(Util.Global.Sprites.Where(x => x.name == "ActiveFighter").FirstOrDefault());
            new Maps.Map().WarpMap(Util.Global.FightPreviousMap, new Vector3(99, 99, 99));
            Util.Global.Hero.Position = new Vector2(Util.Global.FightPreviousHeroLocation.X, Util.Global.FightPreviousHeroLocation.Y);
            Util.Global.Sprites.RemoveAll(X => X.name == "Hero");
            Util.Global.Sprites.Add(Util.Global.Hero);
            Util.Global.Sprites.RemoveAll(X => X.ID == Enemy.ID);

            Util.Global.Fighting = false;
            Util.Global.FightBoard = new Objects.Sprite2d[12, 15];
            Actions.Season.GetMusic();

            int i = 1;
            foreach (Tuple<Items.Item.ItemType, float> IT in Enemy.Actor.Drops)
            {
                double chance = Util.Global.GetRandomDouble();
                Vector2 Pos = new Vector2(0,0);
                if (chance < IT.Item2)
                {
                    int Chance = Util.Global.GetRandomInt(1, 5);
                    switch (Chance)
                    {
                        case 1:
                            Pos = new Vector2(Util.Global.GetRandomInt(25, 35) * Util.Global.GetRandomInt(1, 3), Util.Global.GetRandomInt(25, 35) * Util.Global.GetRandomInt(1, 3));
                            break;
                        case 2:
                            Pos = new Vector2(Util.Global.GetRandomInt(25, 35) * Util.Global.GetRandomInt(1, 3) *-1, Util.Global.GetRandomInt(25, 35) * Util.Global.GetRandomInt(1, 3));
                            break;
                        case 3:
                            Pos = new Vector2(Util.Global.GetRandomInt(25, 35) * Util.Global.GetRandomInt(1, 3), Util.Global.GetRandomInt(25, 35) * Util.Global.GetRandomInt(1, 3)*-1);
                            break;
                        case 4:
                            Pos = new Vector2(Util.Global.GetRandomInt(25, 35) * Util.Global.GetRandomInt(1, 3)*-1, Util.Global.GetRandomInt(25, 35) * Util.Global.GetRandomInt(1, 3)*-1);
                            break;
                    }
                    Util.Global.Sprites.Add(Items.Item.GetItemByType(IT.Item1, Vector2.Add(Util.Global.Hero.Position, Pos)));
                    i++;
                }
            }

            bool MP = MakePet(Enemy);

            foreach (Objects.Sprite2d S in Util.Global.Pets)
            {
                Util.Global.Sprites.Where(X => X.ID == S.ID).FirstOrDefault().Position = Vector2.Subtract(Util.Global.Hero.Position, new Vector2((float)Util.Global.GetRandomInt(1, 150), (float)Util.Global.GetRandomInt(1, 150)));
                Util.Global.Sprites.Where(X => X.ID == S.ID).FirstOrDefault().Actor.aiState = Objects.Actor.AiState.Chasing;
            }
            if (MP)
            {
                Say.speak(Enemy.ID, Say.GetSpeech(Objects.Actor.ActorType.Pet, SpeechType.MakePet, Enemy.Actor.enemyType));
            }
        }

        public static bool MakePet(Objects.Sprite2d Enemy)
        {
            bool makepet = true;
            foreach (Actions.Enemy.EnemyType ET in Enemy.Actor.Parents)
            {
                if (Util.Global.Pets.Where(x => x.Actor.enemyType == ET).Count() == 0)
                { makepet = false; }
            }

            if (Util.Global.Pets.Where(x => x.Actor.enemyType == Enemy.Actor.enemyType).Count() > 0)
            { makepet = false; }

            if(makepet)
            {
                Enemy.Actor.Health = Enemy.Actor.HealthMax;
                if (Util.Global.Pets.Count() < 4)
                {
                    Enemy.Actor.actorType = Objects.Actor.ActorType.Pet;
                    Enemy.Actor.aiState = Objects.Actor.AiState.Chasing;
                    Enemy.clipping = false;
                    Enemy.actionCall = new List<ActionCall>();

                    List<Object> Objs2 = new List<object>();
                    Objs2.Add(Enemy);

                    ActionCall call2 = new ActionCall(ActionType.MouseAny, typeof(Menu.ActorStats), "ToggleStats", Objs2);
                    Enemy.actionCall.Add(call2);
                    Util.Global.Pets.Add(Enemy);
                    Util.Global.Sprites.Add(Enemy);
                }
                else
                {
                    if (Util.Global.Stable.Where(x => x.Actor.enemyType == Enemy.Actor.enemyType).Count() < 1)
                    {
                        Util.Global.Stable.Add(Enemy);
                    }
                }
            }
            return makepet;
        }

        public static void KillEnemy(Objects.Sprite2d Enemy)
        {
            Util.Global.ContentMan.Load<SoundEffect>("Sounds/growl").Play();
            Util.Global.Sprites.RemoveAll(x => x.ID == Enemy.ID);
            Util.Global.Combatants.RemoveAll(x => x == Enemy.ID);
            Menu.ActorStats.RemoveStats(Enemy);
            Util.Global.Hero.Actor.Experience = Util.Global.Hero.Actor.Experience + (Enemy.Actor.Level * 5);
            CheckExp();
        }

        public static void KillPet(Objects.Sprite2d Pet)
        {
            Util.Global.ContentMan.Load<SoundEffect>("Sounds/growl").Play();
            Util.Global.Pets.RemoveAll(x => x.ID == Pet.ID);

            if (Util.Global.Fighting)
            {
                RemoveFromBoard(Pet);
                Util.Global.Combatants.RemoveAll(x => x == Pet.ID);
            }
            Util.Global.Sprites.RemoveAll(x => x.ID == Pet.ID);
            Menu.ActorStats.RemoveStats(Pet);
            Util.Global.Sprites.RemoveAll(x => x.name == "PetToStable");
            Util.Global.Sprites.RemoveAll(x => x.name == "StableToPet");
            Util.Global.Sprites.RemoveAll(x => x.name == "KillPet");
        }

        public static void CheckExp()
        {
            if (getXPforLevel(Util.Global.Hero.Actor.Level+1) <= Util.Global.Hero.Actor.Experience)
            {
                //Level up
                Util.Global.Hero.Actor.Level++;
                Util.Global.Hero.Actor.CalculateStats();
                Util.Global.Hero.Actor.HealthMax = Util.Global.Hero.Actor.HealthMax + 5;
                Util.Global.ContentMan.Load<SoundEffect>("Sounds/levelup").Play();
            }
        }

        public static int getXPforLevel(int Level)
        {
            double L = Convert.ToDouble(Level);
            int R = (int)Math.Pow(L+300,L/7);
            return R;
        }

        public static void Defend(Objects.Sprite2d Actor)
        {
            Actions.Buff.AddBuff(Actor, Buff.BuffType.Endurance, 10);
            Turn();
        }

        public static void Move(Objects.Sprite2d Actor, FightActions FA)
        {
            Vector2 ActorTile = GetBoardPosition(Actor);
            Vector2 Start = GetTilePositon(ActorTile);
            List<Vector2> MOV = new List<Vector2>();

            switch (FA)
            {
                case FightActions.Right:
                    ActorTile = Vector2.Add(ActorTile, new Vector2(0, 1));
                    break;
                case FightActions.Left:
                    ActorTile = Vector2.Add(ActorTile, new Vector2(0, -1));
                    break;
                case FightActions.Up:
                    ActorTile = Vector2.Add(ActorTile, new Vector2(-1, 0));
                    break;
                case FightActions.Down:
                    ActorTile = Vector2.Add(ActorTile, new Vector2(1, 0));
                    break;
            }

            Vector2 End = GetTilePositon(ActorTile);
            RemoveFromBoard(Actor);
            Util.Global.FightBoard[(int)ActorTile.X, (int)ActorTile.Y] = Actor;

            
            if(Actor.Actor.actorType == Objects.Actor.ActorType.Hero)
                MOV = Actions.Anim.GetMovement(Start, End, 10);
            else
                MOV = Actions.Anim.GetMovementVarience(Start, End, 10, 5);

            if (Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault() != null)
            {
                Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Maneuver = new Objects.Maneuver(End, Actor.Size, MOV, Util.ColorType.None, false);
                Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Maneuver.FinalActionCall = new ActionCall(ActionType.Update, typeof(Fight), "Turn", null);
            }
            else
            {
                Turn();
            }
        }
        
        //public static void MoveRight(Objects.Sprite2d Actor)
        //{
        //    Vector2 ActorTile = GetBoardPosition(Actor);
        //    Vector2 Start = GetTilePositon(ActorTile);
        //    ActorTile = Vector2.Add(ActorTile, new Vector2(0,1));
        //    Vector2 End = GetTilePositon(ActorTile);
        //    RemoveFromBoard(Actor);
        //    Util.Global.FightBoard[(int)ActorTile.X, (int)ActorTile.Y] = Actor;
        //    List<Vector2> MOV = Actions.Anim.GetMovementVarience(Start, End, 10, 5);
        //    Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Maneuver = new Objects.Maneuver(End, Actor.Size, MOV, Util.ColorType.None, false);
        //    Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Maneuver.FinalActionCall = new ActionCall(ActionType.Update, typeof(Fight), "UpdateActiveFighter", null);
        //    Turn();
        //}

        //public static void MoveLeft(Objects.Sprite2d Actor)
        //{
        //    Vector2 ActorTile = GetBoardPosition(Actor);
        //    Vector2 Start = GetTilePositon(ActorTile);
        //    ActorTile = Vector2.Add(ActorTile, new Vector2(0, -1));
        //    Vector2 End = GetTilePositon(ActorTile);
        //    RemoveFromBoard(Actor);
        //    Util.Global.FightBoard[(int)ActorTile.X, (int)ActorTile.Y] = Actor;
        //    List<Vector2> MOV = Actions.Anim.GetMovementVarience(Start, End, 10, 5);
        //    Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Maneuver = new Objects.Maneuver(End, Actor.Size, MOV, Util.ColorType.None, false);
        //    Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Maneuver.FinalActionCall = new ActionCall(ActionType.Update, typeof(Fight), "UpdateActiveFighter", null);
        //    Turn();
        //}

        //public static void MoveDown(Objects.Sprite2d Actor)
        //{
        //    Vector2 ActorTile = GetBoardPosition(Actor);
        //    Vector2 Start = GetTilePositon(ActorTile);
        //    ActorTile = Vector2.Add(ActorTile, new Vector2(1, 0));
        //    Vector2 End = GetTilePositon(ActorTile);
        //    RemoveFromBoard(Actor);
        //    Util.Global.FightBoard[(int)ActorTile.X, (int)ActorTile.Y] = Actor;
        //    List<Vector2> MOV = Actions.Anim.GetMovementVarience(Start, End, 10, 5);
        //    Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Maneuver = new Objects.Maneuver(End, Actor.Size, MOV, Util.ColorType.None, false);
        //    Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Maneuver.FinalActionCall = new ActionCall(ActionType.Update, typeof(Fight), "UpdateActiveFighter", null);
        //    Turn();
        //}

        //public static void MoveUp(Objects.Sprite2d Actor)
        //{
        //    Vector2 ActorTile = GetBoardPosition(Actor);
        //    Vector2 Start = GetTilePositon(ActorTile);
        //    ActorTile = Vector2.Add(ActorTile, new Vector2(-1, 0));
        //    Vector2 End = GetTilePositon(ActorTile);
        //    RemoveFromBoard(Actor);
        //    Util.Global.FightBoard[(int)ActorTile.X, (int)ActorTile.Y] = Actor;
        //    List<Vector2> MOV = Actions.Anim.GetMovementVarience(Start, End, 10, 5);
        //    Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Maneuver = new Objects.Maneuver(End, Actor.Size, MOV, Util.ColorType.None, false);
        //    Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Maneuver.FinalActionCall = new ActionCall(ActionType.Update, typeof(Fight), "UpdateActiveFighter", null);
        //    Turn();
        //}

        private static Vector2 GetTilePositon(Vector2 BoardPosition)
        {
            return Util.Global.Sprites.Where(I => I.name == BoardPosition.X.ToString() + ":" + BoardPosition.Y.ToString()).FirstOrDefault().Position;
        }

        public static Vector2 GetBoardPositionFromName(string Name)
        {
            if (Name.Contains(":"))
            {
                int I = Name.IndexOf(":");
                int x = Int32.Parse(Name.Substring(0,I));
                int y = Int32.Parse(Name.Substring(I+1,Name.Length-1-I));
                return new Vector2(x, y);
            }
            return Util.Global.DefaultPosition;
        }

        private static Vector2 GetBoardPosition(Objects.Sprite2d Actor)
        {
            if (Actor != null)
            {
                for (int x = 0; x < Util.Global.FightBoard.GetLength(0); x += 1)
                {
                    for (int y = 0; y < Util.Global.FightBoard.GetLength(1); y += 1)
                    {
                        if (Util.Global.FightBoard[x, y] != null && (Util.Global.FightBoard[x, y].ID == Actor.ID))
                        {
                            return new Vector2(x, y);
                        }

                    }
                }
            }
            return new Vector2(0,0);
        }

        private static void RemoveFromBoard(Objects.Sprite2d Actor)
        {
            for (int x = 0; x < Util.Global.FightBoard.GetLength(0); x += 1)
            {
                for (int y = 0; y < Util.Global.FightBoard.GetLength(1); y += 1)
                {
                    if (Util.Global.FightBoard[x, y] != null && (Util.Global.FightBoard[x, y].ID == Actor.ID))
                    {
                        Util.Global.FightBoard[x, y] = null;
                    }

                }
            }
        }

        public static List<FightActions> GetFightActions(Objects.Sprite2d Actor)
        {
            List<FightActions> RetList = new List<FightActions>();
            Vector2 ActorBoardPos = GetBoardPosition(Actor);
            Vector2 TestPos = new Vector2();
            
            TestPos = Vector2.Add(ActorBoardPos, new Vector2(0, -1));
            if (TestPos.Y > 0 && Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y] == null)
            {
                RetList.Add(FightActions.Left);
            }
            TestPos = Vector2.Add(ActorBoardPos, new Vector2(0, 1));
            if (TestPos.Y < 15 && Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y] == null)
            {
                RetList.Add(FightActions.Right);
            }
            TestPos = Vector2.Add(ActorBoardPos, new Vector2(1, 0));
            if (TestPos.X < 11 && Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y] == null)
            {
                RetList.Add(FightActions.Down);
            }
            TestPos = Vector2.Add(ActorBoardPos, new Vector2(-1, 0));
            if (TestPos.X > 0 && Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y] == null)
            {
                RetList.Add(FightActions.Up);
            }
            if (GetEnemiesInRange(Actor, Actor.Actor.Level).Count() > 0)
            {
                RetList.Add(FightActions.Attack);
            }

            RetList.Add(FightActions.Pass);

            return RetList;
        }

        public static List<Objects.Sprite2d> GetEnemiesInRange(Objects.Sprite2d Actor, int Range)
        {
            List<Objects.Sprite2d> RetEnemies = new List<Objects.Sprite2d>();
            Vector2 ActorBoardPos = GetBoardPosition(Actor);
            List<Objects.Actor.ActorType> ActorExclusionType = new List<Objects.Actor.ActorType>();
            if (Actor.Actor.actorType == Objects.Actor.ActorType.Hero || Actor.Actor.actorType == Objects.Actor.ActorType.Pet)
            {
                ActorExclusionType.Add(Objects.Actor.ActorType.Enemy);
            }
            if (Actor.Actor.actorType == Objects.Actor.ActorType.Enemy)
            {
                ActorExclusionType.Add(Objects.Actor.ActorType.Hero);
                ActorExclusionType.Add(Objects.Actor.ActorType.Pet);
            }
            for (int x = 0; x < Util.Global.FightBoard.GetLength(0); x += 1)
            {
                for (int y = 0; y < Util.Global.FightBoard.GetLength(1); y += 1)
                {
                    if (Util.Global.FightBoard[x, y]!= null && (ActorExclusionType.Contains(Util.Global.FightBoard[x, y].Actor.actorType) && Util.Base.collision(ActorBoardPos, new Vector2(x, y), Range)))
                    {
                        RetEnemies.Add(Util.Global.FightBoard[x, y]);
                    }
                }
            }
            return RetEnemies;
        }

        public static void AutoAction(Objects.Sprite2d Actor)
        {
            List<FightActions> FA = GetFightActions(Actor).OrderBy(a => Guid.NewGuid()).ToList();
            FA.RemoveAll(x => x == FightActions.Pass);
            
            if (FA.Where(x => x == FightActions.Attack).Count() > 0)
                FA.RemoveAll(x => x != FightActions.Attack);

            FightActions Dir = FightActions.Wait;
            Dir = GetDirectionOfAnEnemy(Actor);
            if (FA.Where(x => x == Dir).Count() > 0)
                FA.RemoveAll(x => x != Dir);

            switch(FA.FirstOrDefault())
            {
                case FightActions.Attack:
                    BeginAttack(Actor, GetEnemiesInRange(Actor, Actor.Actor.Level).OrderBy(a => Guid.NewGuid()).FirstOrDefault());
                    break;
                case FightActions.Defend:
                    Defend(Actor);
                    break;
                case FightActions.Run:
                    break;
                case  FightActions.Left:
                    Move(Actor, FA.FirstOrDefault());
                    break;
                case FightActions.Right:
                    Move(Actor, FA.FirstOrDefault());
                    break;
                case FightActions.Up:
                    Move(Actor, FA.FirstOrDefault());
                    break;
                case FightActions.Down:
                    Move(Actor, FA.FirstOrDefault());
                    break;

            }
        }

        private static FightActions GetDirectionOfAnEnemy(Objects.Sprite2d Actor)
        {
            
            int Modifyer = 4;
            if (Actor.Actor.actorType == Objects.Actor.ActorType.Hero || Actor.Actor.actorType == Objects.Actor.ActorType.Pet)
                { Modifyer = 10; }
            List<FightActions> FA = new List<FightActions>();
            Objects.Sprite2d Sprite = GetEnemiesInRange(Actor, Actor.Actor.Level*Modifyer).OrderBy(a => Guid.NewGuid()).FirstOrDefault();
            Vector2 EnemyPos = GetBoardPosition(Sprite);
            if (EnemyPos != Util.Global.DefaultPosition)
            {
                Vector2 ActorPos = GetBoardPosition(Actor);

                if (EnemyPos.X > ActorPos.X)
                    FA.Add(FightActions.Down);
                if (EnemyPos.X < ActorPos.X)
                    FA.Add(FightActions.Up);
                if (EnemyPos.Y > ActorPos.Y)
                    FA.Add(FightActions.Right);
                if (EnemyPos.Y < ActorPos.Y)
                    FA.Add(FightActions.Left);

                return FA.OrderBy(a => Guid.NewGuid()).Take(1).FirstOrDefault();
            }
            return FightActions.Wait;
        }
    }
}

//private static List<Objects.Sprite2d> GetEnemiesInRange_OLD(Objects.Sprite2d Actor, int Range)
//{
//    Util.Base.Log("Testing Range("+Range.ToString()+":"+Actor.modelname);

//    List<Objects.Sprite2d> RetEnemies = new List<Objects.Sprite2d>();
//    Vector2 TestPos = new Vector2();
//    Vector2 ActorBoardPos = GetBoardPosition(Actor);
//    List<Objects.Actor.ActorType> ActorExclusionType = new List<Objects.Actor.ActorType>();

//    if (Actor.Actor.actorType == Objects.Actor.ActorType.Hero || Actor.Actor.actorType == Objects.Actor.ActorType.Pet)
//    {
//        ActorExclusionType.Add(Objects.Actor.ActorType.Enemy);
//    }
//    if (Actor.Actor.actorType == Objects.Actor.ActorType.Enemy)
//    {
//        ActorExclusionType.Add(Objects.Actor.ActorType.Hero);
//        ActorExclusionType.Add(Objects.Actor.ActorType.Pet);
//    }

//     for(int i=1;i<=Range;i++)
//        {
//            TestPos = Vector2.Add(ActorBoardPos, new Vector2(0, i*-1));
//            if (TestLimits(TestPos) && Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y] != null && ActorExclusionType.Contains(Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y].Actor.actorType))
//            { RetEnemies.Add(Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y]); }
//            TestPos = Vector2.Add(ActorBoardPos, new Vector2(0, i));
//            if (TestLimits(TestPos) && Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y] != null && ActorExclusionType.Contains(Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y].Actor.actorType))
//            { RetEnemies.Add(Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y]); }
//            TestPos = Vector2.Add(ActorBoardPos, new Vector2(i*-1, 0));
//            if (TestLimits(TestPos) && Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y] != null && ActorExclusionType.Contains(Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y].Actor.actorType))
//            { RetEnemies.Add(Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y]); }
//            TestPos = Vector2.Add(ActorBoardPos, new Vector2(i, 0));
//            if (TestLimits(TestPos) && Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y] != null && ActorExclusionType.Contains(Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y].Actor.actorType))
//            { RetEnemies.Add(Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y]); }

//            TestPos = Vector2.Add(ActorBoardPos, new Vector2(i, i));
//            if (TestLimits(TestPos) && Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y] != null && ActorExclusionType.Contains(Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y].Actor.actorType))
//            { RetEnemies.Add(Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y]); }
//            TestPos = Vector2.Add(ActorBoardPos, new Vector2(i*-1, i));
//            if (TestLimits(TestPos) && Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y] != null && ActorExclusionType.Contains(Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y].Actor.actorType))
//            { RetEnemies.Add(Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y]); }
//            TestPos = Vector2.Add(ActorBoardPos, new Vector2(i, i*-1));
//            if (TestLimits(TestPos) && Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y] != null && ActorExclusionType.Contains(Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y].Actor.actorType))
//            { RetEnemies.Add(Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y]); }
//            TestPos = Vector2.Add(ActorBoardPos, new Vector2(i*-1, i*-1));
//            if (TestLimits(TestPos) && Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y] != null && !ActorExclusionType.Contains(Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y].Actor.actorType))
//            { RetEnemies.Add(Util.Global.FightBoard[(int)TestPos.X, (int)TestPos.Y]); }
//        }

//    return RetEnemies;
//}

//private static bool TestLimits(Vector2 Test)
//{
//    if (Test.X > 0 && Test.Y > 0 && Test.X < 12 && Test.Y < 15)
//    {
//        Util.Base.Log("Range X:" + Test.X.ToString() + " Y:" + Test.Y.ToString());
//        return true;
//    }
//    else
//        return false;
//}