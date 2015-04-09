using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game.Actions
{
    public class Fight
    {
        public void init()
        {
            Util.Global.Texts.Add(new Objects.Text("Hero Health:" + Util.Global.Hero.Health.ToString(), "HHealth", false, 200, 300, Color.IndianRed));
            Util.Global.Texts.Where(x => x.name == "HHealth").FirstOrDefault().orderNum = 980;
            Util.Global.Texts.Add(new Objects.Text("Enemy Health:", "EHealth", false, 400, 300, Color.IndianRed));
            Util.Global.Texts.Where(x => x.name == "EHealth").FirstOrDefault().orderNum = 980;
            Util.Global.Texts.Add(new Objects.Text("", "FightText", false, 300, 100, Color.IndianRed));
            Util.Global.Texts.Where(x => x.name == "FightText").FirstOrDefault().orderNum = 980;
            Util.Global.Texts.Add(new Objects.Text("", "FightTextE", false, 300, 125, Color.IndianRed));
            Util.Global.Texts.Where(x => x.name == "FightTextE").FirstOrDefault().orderNum = 980;

            Util.Global.Texts.Add(new Objects.Text("EnemyStat_Lev", "EnemyStat_Lev", false, 600, 75, Color.IndianRed));
            Util.Global.Texts.Add(new Objects.Text("EnemyStat_Dex", "EnemyStat_Dex", false, 600, 100, Color.IndianRed));
            Util.Global.Texts.Add(new Objects.Text("EnemyStat_Str", "EnemyStat_Str", false, 600, 125, Color.IndianRed));
            Util.Global.Texts.Add(new Objects.Text("EnemyStat_End", "EnemyStat_End", false, 600, 150, Color.IndianRed));
            Util.Global.Texts.Add(new Objects.Text("EnemyStat_Agl", "EnemyStat_Agl", false, 600, 175, Color.IndianRed));
            Util.Global.Texts.Add(new Objects.Text("HeroStat_Lev", "HeroStat_Lev", false, 10, 75, Color.IndianRed));
            Util.Global.Texts.Add(new Objects.Text("HeroStat_Dex", "HeroStat_Dex", false, 10, 100, Color.IndianRed));
            Util.Global.Texts.Add(new Objects.Text("HeroStat_Str", "HeroStat_Str", false, 10, 125, Color.IndianRed));
            Util.Global.Texts.Add(new Objects.Text("HeroStat_End", "HeroStat_End", false, 10, 150, Color.IndianRed));
            Util.Global.Texts.Add(new Objects.Text("HeroStat_Agl", "HeroStat_Agl", false, 10, 175, Color.IndianRed));

            foreach (Objects.Text T in Util.Global.Texts.Where(x => x.name.Contains("HeroStat") || x.name.Contains("EnemyStat")))
            {
                T.orderNum = 980;
            }

            List<Object> obj2 = new List<object>();
            ActionCall call2 = new ActionCall(typeof(Fight), "Attack", obj2);
            Util.Global.Menus.Add(new Objects.Menu("Attack  ", "Attack", false, Color.DarkMagenta, 125, 15, Color.DarkGray, call2));
            Util.Global.Menus.Where(x => x.name == "Attack").FirstOrDefault().orderNum = 980;

            List<Object> obj3 = new List<object>();
            ActionCall call3 = new ActionCall(typeof(Fight), "Defend", obj3);
            Util.Global.Menus.Add(new Objects.Menu("Defend  ", "Defend", false, Color.DarkMagenta, 325, 15, Color.DarkGray, call3));
            Util.Global.Menus.Where(x => x.name == "Defend").FirstOrDefault().orderNum = 980;

            List<Object> obj = new List<object>();
            ActionCall call = new ActionCall(typeof(Fight), "EndFight", obj);
            Util.Global.Menus.Add(new Objects.Menu("Run  ", "FightRun", false, Color.DarkMagenta, 525, 15, Color.DarkGray, call));
            Util.Global.Menus.Where(x => x.name == "FightRun").FirstOrDefault().orderNum = 980;
        }
        
        public void DisplayFight(Actors.Enemy Enemy)
        {
            if (!Util.Global.Fighting)
            {
                Util.Global.Fighting = true;
                Util.Global.Sprites.Where(x => x.name == "FightBackground").FirstOrDefault().active = true;
                Util.Global.Sprites.Where(x => x.name == "FightBackground").FirstOrDefault().x = 1;
                Util.Global.Sprites.Where(x => x.name == "FightBackground").FirstOrDefault().y = 1;

                Util.Global.SpritesAnim.Where(x => x.name == "Hero").FirstOrDefault().x = 200;
                Util.Global.SpritesAnim.Where(x => x.name == "Hero").FirstOrDefault().y = 350;
                Util.Global.SpritesAnim.Where(x => x.name == "Hero").FirstOrDefault().Texture = Util.Global.Hero.HeroRight;
                
                Util.Global.Sprites.Where(x => x.name == Enemy.Sprite.name).FirstOrDefault().orderNum = 1000;
                Util.Global.Sprites.Where(x => x.name == Enemy.Sprite.name).FirstOrDefault().x = 400;
                Util.Global.Sprites.Where(x => x.name == Enemy.Sprite.name).FirstOrDefault().y = 350;

                List<Object> obj2 = new List<object>();
                obj2.Add(Enemy);
                ActionCall call2 = new ActionCall(typeof(Fight), "Attack", obj2);
                Util.Global.Menus.Where(x => x.name == "Attack").FirstOrDefault().actionCall = call2;
                ActionCall call3 = new ActionCall(typeof(Fight), "EndFight", obj2);
                Util.Global.Menus.Where(x => x.name == "FightRun").FirstOrDefault().actionCall = call3;

                Enemy.CalculateStats();
                Util.Global.Texts.Where(x => x.name == "EHealth").FirstOrDefault().text = "Enemy Health:" + Enemy.Health.ToString();

                Util.Global.Menus.Where(x => x.name == "FightRun").FirstOrDefault().active = true;
                Util.Global.Menus.Where(x => x.name == "Attack").FirstOrDefault().active = true;
                Util.Global.Menus.Where(x => x.name == "Defend").FirstOrDefault().active = true;
                Util.Global.Texts.Where(x => x.name == "EHealth").FirstOrDefault().active = true;
                Util.Global.Texts.Where(x => x.name == "HHealth").FirstOrDefault().active = true;
                Util.Global.Texts.Where(x => x.name == "FightText").FirstOrDefault().active = true;
                Util.Global.Texts.Where(x => x.name == "FightTextE").FirstOrDefault().active = true;
                Util.Global.Texts.Where(x => x.name == "FightText").FirstOrDefault().text = "";
                Util.Global.Texts.Where(x => x.name == "FightTextE").FirstOrDefault().text = "";

                Util.Global.Menus.Where(x => x.name == "Attack").FirstOrDefault().boxColor = Color.DarkGray;
                Util.Global.Menus.Where(x => x.name == "FightRun").FirstOrDefault().boxColor = Color.DarkGray;
                Util.Global.Menus.Where(x => x.name == "Defend").FirstOrDefault().boxColor = Color.DarkGray;

                foreach (Objects.Text T in Util.Global.Texts.Where(x => x.name.Contains("HeroStat") || x.name.Contains("EnemyStat")))
                {
                    T.active = true;
                    switch (T.name)
                    {
                        case "EnemyStat_Lev":
                            T.text = "Level:" + Enemy.Level.ToString();
                            break;
                        case "EnemyStat_Dex":
                            T.text = "Dex:" + Enemy.Dex.ToString();
                            break;
                        case "EnemyStat_Str":
                            T.text = "Str:" + Enemy.Str.ToString();
                            break;
                        case "EnemyStat_End":
                            T.text = "End:" + Enemy.End.ToString();
                            break;
                        case "EnemyStat_Agl":
                            T.text = "Agl:" + Enemy.Agl.ToString();
                            break;
                        case "HeroStat_Lev":
                            T.text = "Level:" + Util.Global.Hero.Level.ToString();
                            break;
                        case "HeroStat_Dex":
                            T.text = "Dex:" + Util.Global.Hero.Dex.ToString();
                            break;
                        case "HeroStat_Str":
                            T.text = "Str:" + Util.Global.Hero.Str.ToString();
                            break;
                        case "HeroStat_End":
                            T.text = "End:" + Util.Global.Hero.End.ToString();
                            break;
                        case "HeroStat_Agl":
                            T.text = "Agl:" + Util.Global.Hero.Agl.ToString();
                            break;
                    }

                }

            }
        }

        public void EndFight(Actors.Enemy Enemy)
        {
            Util.Global.Sprites.Where(x => x.name == "FightBackground").FirstOrDefault().active = false;
            Util.Global.Sprites.Where(x => x.name == Enemy.Sprite.name).FirstOrDefault().active = false;
            Util.Global.Menus.Where(x => x.name == "FightRun").FirstOrDefault().active = false;
            Util.Global.Menus.Where(x => x.name == "Attack").FirstOrDefault().active = false;
            Util.Global.Menus.Where(x => x.name == "Defend").FirstOrDefault().active = false;
            Util.Global.Texts.Where(x => x.name == "EHealth").FirstOrDefault().active = false;
            Util.Global.Texts.Where(x => x.name == "HHealth").FirstOrDefault().active = false;
            Util.Global.SpritesAnim.Where(x => x.name == "Hero").FirstOrDefault().active = true;
            Util.Global.Texts.Where(x => x.name == "FightText").FirstOrDefault().active = false;
            Util.Global.Texts.Where(x => x.name == "FightTextE").FirstOrDefault().active = false;

            foreach (Objects.Text T in Util.Global.Texts.Where(x => x.name.Contains("HeroStat") || x.name.Contains("EnemyStat")))
            {
                T.active = false;
            }

            Util.Global.Fighting = false;
        }

        public void Attack(Actors.Enemy Enemy)
        {
            int Dmg = 0;
            if (Strike(Enemy, true))
            {
                Dmg = GetDamage(Enemy, true);
                Enemy.Health = Enemy.Health - Dmg;
                Util.Global.Texts.Where(x => x.name == "FightText").FirstOrDefault().text = "You hit Enemy for " + Dmg.ToString();
                Vector2 start = new Vector2(Util.Global.SpritesAnim.Where(x => x.name == "Hero").FirstOrDefault().x,Util.Global.SpritesAnim.Where(x => x.name == "Hero").FirstOrDefault().y);
                Vector2 end = new Vector2(Enemy.Sprite.x,Enemy.Sprite.y);
                Util.Global.Anim.Add(new Anim(Util.Global.SpritesAnim.Where(x => x.name == "Hero").FirstOrDefault().ID,start,new Actions.Anim().GetMovementJump(start,end)));
            }
            else
            {
                Util.Global.Texts.Where(x => x.name == "FightText").FirstOrDefault().text = "You missed";
            }
            System.Threading.Thread.Sleep(1000);
            if (Strike(Enemy, false))
            {
                Dmg = GetDamage(Enemy, false);
                Util.Global.Hero.Health = Util.Global.Hero.Health - Dmg;
                Util.Global.Texts.Where(x => x.name == "FightTextE").FirstOrDefault().text = "Enemy hit You for " + Dmg.ToString();
            }
            else
            {
                Util.Global.Texts.Where(x => x.name == "FightTextE").FirstOrDefault().text = "Enemy missed";
            }

            Util.Global.Menus.Where(x => x.name == "Attack").FirstOrDefault().boxColor = Color.DarkGray;
            Util.Global.Menus.Where(x => x.name == "FightRun").FirstOrDefault().boxColor = Color.DarkGray;
            Util.Global.Menus.Where(x => x.name == "Defend").FirstOrDefault().boxColor = Color.DarkGray;
            Util.Global.Texts.Where(x => x.name == "EHealth").FirstOrDefault().text = "Enemy Health:" + Enemy.Health.ToString();
            Util.Global.Texts.Where(x => x.name == "HHealth").FirstOrDefault().text = "Hero Health:" + Util.Global.Hero.Health.ToString();

            //Util.Global.SpritesAnim.Where(x => x.name == "Hero").FirstOrDefault().Update();

            if (Enemy.Health <= 0)
            {
                EndFight(Enemy);
            }

        }

        public void Defend()
        {

        }

        private bool Strike(Actors.Enemy Enemy, bool attack)
        {
            Random rnd = new Random();
            if (attack)
            {
                int Hchnace = Util.Global.Hero.Dex * rnd.Next(Util.Global.Hero.Level, Util.Global.Hero.Level * 4);
                //rnd = new Random();
                int Echance = Enemy.Agl + rnd.Next(Enemy.Level, Enemy.Level * 2);
                if (Hchnace > Echance)
                {
                    return true;
                }
                return false;
            }
            else
            {
                int Hchnace = Util.Global.Hero.Agl * rnd.Next(Util.Global.Hero.Level, Util.Global.Hero.Level * 4);
                //rnd = new Random();
                int Echance = Enemy.Dex + rnd.Next(Enemy.Level, Enemy.Level * 2);
                if (Echance > Hchnace)
                {
                    return true;
                }
                return false;
            }

        }

        private int GetDamage(Actors.Enemy Enemy, bool attack)
        {
            Random rnd = new Random();
            float rtn = 0;
            if (attack)
            {
                //int HHit =(Util.Global.Hero.Str * rnd.Next(Util.Global.Hero.Level, Util.Global.Hero.Level * 2));
                //int EHit =(Enemy.End * rnd.Next(Enemy.Level, Enemy.Level * 2));
                //rtn = HHit - EHit;
                rtn = (Util.Global.Hero.Str - Enemy.End) + rnd.Next(Util.Global.Hero.Level, Util.Global.Hero.Level * 4);
                rtn = MathHelper.Clamp(rtn,0,rnd.Next(Util.Global.Hero.Level, Util.Global.Hero.Level * 2));
            }
            else
            {
                //int HHit =(Util.Global.Hero.End * rnd.Next(Util.Global.Hero.Level, Util.Global.Hero.Level * 2));
                //int EHit = (Enemy.Str * rnd.Next(Enemy.Level, Enemy.Level * 2));
                //rtn = EHit - HHit;
                rtn = (Enemy.Str - Util.Global.Hero.End) + rnd.Next(Enemy.Level, Enemy.Level * 4);
                rtn = MathHelper.Clamp(rtn,0,rnd.Next(Enemy.Level, Enemy.Level * 2));
            }

            return (int)rtn;
        }


    }
}
