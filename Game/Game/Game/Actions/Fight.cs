using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Game.Actions
{
    public class Fight
    {
        public void init()
        {
            new Maps.Map().GenerateFightMap();
        }
        
        public void DisplayFight(Objects.Sprite2d Enemy)
        {
            if (!Util.Global.Fighting)
            {
                Util.Global.Combatants = new List<Guid>();
                Util.Global.FightPreviousMap = Util.Global.CurrentMap;
                Util.Global.FightPreviousHeroLocation = new Vector2(Util.Global.Hero.Position.X, Util.Global.Hero.Position.Y);

                Menu.Inventory.DisplayInventory();
                foreach (Objects.Sprite2d InvSpritesInHand in Util.Global.Sprites.Where(x => x.Item.State == Objects.Item.ItemState.Hand).ToList())
                {
                    Items.ItemActions.DropItem(InvSpritesInHand);
                }
                Menu.Inventory.HideInventory();
                Menu.Craft.HideCraft();
                new Objects.Maneuver().RemoveAll();

                Util.Global.Sprites.RemoveAll(X => X.ID == Enemy.ID);
                Util.Global.MainMap[(int)Util.Global.CurrentMap.X, (int)Util.Global.CurrentMap.Y, (int)Util.Global.CurrentMap.Z].Sprite2d.RemoveAll(X => X.ID == Enemy.ID);
                //Util.Global.Sprites.RemoveAll(X => X.name == "Hero");

                new Maps.Map().GenerateFightMap();
                Vector3 mapVector = new Vector3(99, 99, 99);
                new Maps.Map().WarpMap(mapVector, Util.Global.CurrentMap);

                Util.Global.Hero.Position = new Vector2(
                    (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Where(x => x.name == "7:4").FirstOrDefault().Position.X),
                    (Util.Global.MainMap[(int)mapVector.X, (int)mapVector.Y, (int)mapVector.Z].Sprite2d.Where(x => x.name == "7:4").FirstOrDefault().Position.Y)
                );


                Util.Global.Hero.model = Util.Global.HeroRight;
                Util.Global.Hero.orderNum = 1000;
                Util.Global.Sprites.Add(Util.Global.Hero);
                

                Enemy.Position = Util.Global.Sprites.Where(x => x.name == "7:11").FirstOrDefault().Position;
                Enemy.Actor.aiState = Objects.Actor.AiState.Figting;
                Enemy.actionCall.RemoveAll(x => x.actionMethodName == "DisplayFight");
                Util.Global.Sprites.Add(Enemy);
                
                int peti = 1;
                foreach(Guid ID in Util.Global.Pets)
                {
                        Util.Global.Sprites.Where(X => X.ID == ID).FirstOrDefault().Position = new Vector2(Util.Global.Hero.Position.X, Util.Global.Hero.Position.Y - peti * 50);
                        Util.Global.Sprites.Where(X => X.ID == ID).FirstOrDefault().Actor.aiState = Objects.Actor.AiState.Figting;
                        Util.Global.Combatants.Add(Util.Global.Sprites.Where(X => X.ID == ID).FirstOrDefault().ID);
                        peti = peti + 1;
                }
                Util.Global.Combatants.Add(Util.Global.Hero.ID);
                Util.Global.Combatants.Add(Enemy.ID);
                Util.Global.Combatants = Util.Global.Combatants.OrderBy(a => Guid.NewGuid()).ToList();

                Menu.ActorStats.RemoveStats(Util.Global.Hero);
                Menu.ActorStats.RemoveStats(Enemy);

                Util.Base.GetLightSources();
                Util.Global.Fighting = true;
                Util.Global.Cam.Pos = new Vector2((Util.Global.Hero.Position.X * -1) + 350, (Util.Global.Hero.Position.Y * -1) + 250);
                
                Turn();
            }
        }

        public void BeginAttack(Objects.Sprite2d Attacker, Objects.Sprite2d Defender)
        {
            Menu.FightActions.RemoveFightMenu();
            string name = Actions.Anim.AttackAnim(Attacker.Position, Defender.Position);
            List<object> obj = new List<object>();
            obj.Add(Attacker);
            obj.Add(Defender);
            Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().Maneuver.FinalActionCall = new ActionCall(ActionType.Update, typeof(Fight), "Attack", obj);
        }

        public void Attack(Objects.Sprite2d Attacker, Objects.Sprite2d Defender)
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
                if (Defender.Actor.actorType == Objects.Actor.ActorType.Enemy)
                {
                    Util.Global.ContentMan.Load<SoundEffect>("Sounds/slotwin").Play();
                    Util.Global.Hero.Actor.Experience = Util.Global.Hero.Actor.Experience + (Defender.Actor.Level * 5);
                    CheckExp();
                    EndFight(Defender);
                }
            }
            Turn();
        }

        public void Turn()
        {
            if (Util.Global.Fighting == true)
            {
                if (Util.Global.FightTurn > Util.Global.Combatants.Count - 1)
                {
                    Util.Global.FightTurn = 0;
                }
                Objects.Sprite2d Actor = Util.Global.Sprites.Where(x => x.ID == Util.Global.Combatants.ToArray()[Util.Global.FightTurn]).FirstOrDefault();
                Util.Global.FightTurn = Util.Global.FightTurn + 1;

                Util.Global.Sprites.RemoveAll(x => x.name == "ActiveFighter");
                Util.Global.Sprites.Add(new Objects.Sprite2d(Util.Global.AllTexture2D.Where(x => x.Name == "drop").FirstOrDefault(), "ActiveFighter", true, Actor.Position, Actor.Size, 0, Objects.Base.ControlType.None));
                Util.Global.Sprites.Where(x => x.name == "ActiveFighter").FirstOrDefault().color = Color.Yellow * 0.5f;
                Util.Global.Sprites.Where(x => x.name == "ActiveFighter").FirstOrDefault().orderNum = 500;
                Util.Global.Sprites.Where(x => x.name == "ActiveFighter").FirstOrDefault().LightIgnor = true;

                if (Actor.Actor.actorType == Objects.Actor.ActorType.Hero || Actor.Actor.actorType == Objects.Actor.ActorType.Pet)
                {
                    Objects.Sprite2d Defender = Util.Global.Sprites.Where(x => Util.Global.Combatants.Contains(x.ID) && x.Actor.actorType == Objects.Actor.ActorType.Enemy).FirstOrDefault();
                    Menu.FightActions.DisplayFightMenu(Actor.Position, Actor, Defender);
                }
                if (Actor.Actor.actorType == Objects.Actor.ActorType.Enemy)
                {
                    Objects.Sprite2d Defender = Util.Global.Sprites.Where(x => Util.Global.Combatants.Contains(x.ID) && x.Actor.actorType == Objects.Actor.ActorType.Hero).FirstOrDefault();
                    //Menu.FightActions.DisplayFightMenu(Actor.Position, Actor, Defender);
                    BeginAttack(Actor, Defender);
                }
            }
        }

        private bool Strike(Objects.Sprite2d Attacker, Objects.Sprite2d Defender)
        {
            int Hchnace = Attacker.Actor.Dex * Util.Global.GetRandomInt(Attacker.Actor.Level, Attacker.Actor.Level * 11);
            int Echance = Defender.Actor.Agl + Util.Global.GetRandomInt(Defender.Actor.Level, Defender.Actor.Level * 10);
            if (Hchnace > Echance)
            {
                return true;
            }
            return false;
        }

        private int GetDamage(Objects.Sprite2d Attacker, Objects.Sprite2d Defender)
        {
            Random rnd = new Random();
            float rtn = 0;
            rtn = (Attacker.Actor.Str - Defender.Actor.End) + Util.Global.GetRandomInt(Attacker.Actor.Level, Attacker.Actor.Level * 4);
            rtn = MathHelper.Clamp(rtn, 0, Util.Global.GetRandomInt(Attacker.Actor.Level, Attacker.Actor.Level * 2));
            return (int)rtn;
        }

        public void EndFight(Objects.Sprite2d Enemy)
        {
            Util.Global.MainMap[99, 99, 99] = null;
            Util.Global.Sprites.Where(x => x.name == "Hero").FirstOrDefault().active = true;
            Util.Global.Sprites.Remove(Util.Global.Sprites.Where(x => x.name == "ActiveFighter").FirstOrDefault());
            new Maps.Map().WarpMap(Util.Global.FightPreviousMap, new Vector3(99, 99, 99));
            Util.Global.Hero.Position = new Vector2(Util.Global.FightPreviousHeroLocation.X, Util.Global.FightPreviousHeroLocation.Y);
            Util.Global.Sprites.RemoveAll(X => X.name == "Hero");
            Util.Global.Sprites.Add(Util.Global.Hero);
            Util.Global.Sprites.RemoveAll(X => X.ID == Enemy.ID);

            Util.Global.Fighting = false;

            MakePet(Enemy);

            foreach (Guid ID in Util.Global.Pets)
            {
                Util.Global.Sprites.Where(X => X.ID == ID).FirstOrDefault().Position = Vector2.Subtract(Util.Global.Hero.Position, new Vector2((float)Util.Global.GetRandomInt(1,150),(float)Util.Global.GetRandomInt(1,150)));
                Util.Global.Sprites.Where(X => X.ID == ID).FirstOrDefault().Actor.aiState = Objects.Actor.AiState.Chasing;
            }

            int i = 1;
            foreach (Items.Item.ItemType IT in Enemy.Actor.Drops)
            {
                Util.Global.Sprites.Add(Items.Item.GetItemByType(IT, Vector2.Add(Util.Global.Hero.Position, new Vector2(25*i,25*i))));
                i++;
            }

        }

        public void MakePet(Objects.Sprite2d Enemy)
        {
            if (Util.Global.Pets.Count < 4 && Enemy.Size.X > 50)
            {
                new Actors.Pet().GetPet(Enemy.Actor.Level, 10, Util.Global.Hero.Position, Enemy.model.Name);
            }
        }

        public void CheckExp()
        {
            if (getXPforLevel(Util.Global.Hero.Actor.Level+1) <= Util.Global.Hero.Actor.Experience)
            {
                //Level up
                Util.Global.Hero.Actor.Level++;
                Util.Global.Hero.Actor.CalculateStats();
                Util.Global.Hero.Actor.Health = Util.Global.Hero.Actor.Health+10;
            }
        }

        public int getXPforLevel(int Level)
        {
            double L = Convert.ToDouble(Level);
            int R = (int)Math.Pow(L+300,L/7);
            return R;
        }

        public void Defend()
        {

        }
    }
}

