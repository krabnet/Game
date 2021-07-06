using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Game.Actions;

namespace Game.Menu
{
    public static class FightActions
    {
        public static void DisplayFightMenu(Vector2 Position, Objects.Sprite2d Attacker, Objects.Sprite2d Defender)
        {
            List<Actions.Fight.FightActions> FAL = Fight.GetFightActions(Attacker);
            List<Object> obj2 = new List<object>();
            List<Object> obj3 = new List<object>();
            RemoveFightMenu();
            foreach (Actions.Fight.FightActions FA in FAL)
            {
                switch (FA)
                {
                    case Fight.FightActions.Attack:
                        Util.Global.Sprites.Add(new Objects.Sprite2d(null, "Attack", true, Vector2.Subtract(Position, new Vector2(45, 0)), Util.Global.DefaultPosition, 0, Objects.Base.ControlType.None));
                        Util.Global.Sprites.Where(x => x.name=="Attack").FirstOrDefault().text="Attack";
                        Util.Global.Sprites.Where(x => x.name == "Attack").FirstOrDefault().textSize = 1f;
                        Util.Global.Sprites.Where(x => x.name == "Attack").FirstOrDefault().color = Color.Red;
                        Util.Global.Sprites.Where(x => x.name=="Attack").FirstOrDefault().boxColor = Color.Blue;
                        Util.Global.Sprites.Where(x => x.name == "Attack").FirstOrDefault().orderNum = 980;
                        obj2.Add(Attacker);
                        obj2.Add(Defender);
                        ActionCall call2 = new ActionCall(ActionType.Mouse, typeof(Fight), "BeginAttack", obj2);
                        Util.Global.Sprites.Where(x => x.name == "Attack").FirstOrDefault().actionCall.Add(call2);
                        break;
                    case Fight.FightActions.Defend:
                        Util.Global.Sprites.Add(new Objects.Sprite2d(null, "Defend", true, Vector2.Subtract(Position, new Vector2(45, -40)), Util.Global.DefaultPosition, 0, Objects.Base.ControlType.None));
                        Util.Global.Sprites.Where(x => x.name == "Defend").FirstOrDefault().text = "Defend";
                        Util.Global.Sprites.Where(x => x.name == "Defend").FirstOrDefault().textSize = 1f;
                        Util.Global.Sprites.Where(x => x.name == "Defend").FirstOrDefault().color = Color.Red;
                        Util.Global.Sprites.Where(x => x.name == "Defend").FirstOrDefault().boxColor = Color.Blue;
                        Util.Global.Sprites.Where(x => x.name == "Defend").FirstOrDefault().orderNum = 980;
                        obj2 = new List<object>();
                        obj2.Add(Attacker); 
                        call2 = new ActionCall(ActionType.Mouse, typeof(Fight), "Defend", obj2);
                        Util.Global.Sprites.Where(x => x.name == "Defend").FirstOrDefault().actionCall.Add(call2);
                        break;
                    case Fight.FightActions.Run:
                        Util.Global.Sprites.Add(new Objects.Sprite2d(null, "Run", true, Vector2.Subtract(Position, new Vector2(45, -80)), Util.Global.DefaultPosition, 0, Objects.Base.ControlType.None));
                        Util.Global.Sprites.Where(x => x.name == "Run").FirstOrDefault().text = "Run";
                        Util.Global.Sprites.Where(x => x.name == "Run").FirstOrDefault().textSize = 1f;
                        Util.Global.Sprites.Where(x => x.name == "Run").FirstOrDefault().color = Color.Red;
                        Util.Global.Sprites.Where(x => x.name == "Run").FirstOrDefault().boxColor = Color.Blue;
                        Util.Global.Sprites.Where(x => x.name == "Run").FirstOrDefault().orderNum = 980;
                        obj3.Add(Defender);
                        ActionCall RunCall = new ActionCall(ActionType.Mouse, typeof(Fight), "EndFight", obj3);
                        Util.Global.Sprites.Where(x => x.name == "Run").FirstOrDefault().actionCall.Add(RunCall);
                        break;
                    case Fight.FightActions.Up:
                        Util.Global.Sprites.Add(new Objects.Sprite2d(null, "Up", true, Vector2.Subtract(Position, new Vector2(50, -125)), Util.Global.DefaultPosition, 0, Objects.Base.ControlType.None));
                        Util.Global.Sprites.Where(x => x.name == "Up").FirstOrDefault().text = "Up";
                        Util.Global.Sprites.Where(x => x.name == "Up").FirstOrDefault().textSize = 1f;
                        Util.Global.Sprites.Where(x => x.name == "Up").FirstOrDefault().color = Color.Red;
                        Util.Global.Sprites.Where(x => x.name == "Up").FirstOrDefault().boxColor = Color.Blue;
                        Util.Global.Sprites.Where(x => x.name == "Up").FirstOrDefault().orderNum = 980;
                        obj3 = new List<object>();
                        obj3.Add(Attacker);
                        obj3.Add(Actions.Fight.FightActions.Up);
                        RunCall = new ActionCall(ActionType.Mouse, typeof(Fight), "Move", obj3);
                        Util.Global.Sprites.Where(x => x.name == "Up").FirstOrDefault().actionCall.Add(RunCall);
                        break;
                    case Fight.FightActions.Down:
                        Util.Global.Sprites.Add(new Objects.Sprite2d(null, "Down", true, Vector2.Subtract(Position, new Vector2(55, -155)), Util.Global.DefaultPosition, 0, Objects.Base.ControlType.None));
                        Util.Global.Sprites.Where(x => x.name == "Down").FirstOrDefault().text = "Down";
                        Util.Global.Sprites.Where(x => x.name == "Down").FirstOrDefault().textSize = 1f;
                        Util.Global.Sprites.Where(x => x.name == "Down").FirstOrDefault().color = Color.Red;
                        Util.Global.Sprites.Where(x => x.name == "Down").FirstOrDefault().boxColor = Color.Blue;
                        Util.Global.Sprites.Where(x => x.name == "Down").FirstOrDefault().orderNum = 980;
                        obj3 = new List<object>();
                        obj3.Add(Attacker);
                        obj3.Add(Actions.Fight.FightActions.Down);
                        RunCall = new ActionCall(ActionType.Mouse, typeof(Fight), "Move", obj3);
                        Util.Global.Sprites.Where(x => x.name == "Down").FirstOrDefault().actionCall.Add(RunCall);
                        break;
                    case Fight.FightActions.Right:
                        Util.Global.Sprites.Add(new Objects.Sprite2d(null, "Right", true, Vector2.Subtract(Position, new Vector2(15, -140)), Util.Global.DefaultPosition, 0, Objects.Base.ControlType.None));
                        Util.Global.Sprites.Where(x => x.name == "Right").FirstOrDefault().text = "Right";
                        Util.Global.Sprites.Where(x => x.name == "Right").FirstOrDefault().textSize = 1f;
                        Util.Global.Sprites.Where(x => x.name == "Right").FirstOrDefault().color = Color.Red;
                        Util.Global.Sprites.Where(x => x.name == "Right").FirstOrDefault().boxColor = Color.Blue;
                        Util.Global.Sprites.Where(x => x.name == "Right").FirstOrDefault().orderNum = 980;
                        obj3 = new List<object>();
                        obj3.Add(Attacker);
                        obj3.Add(Actions.Fight.FightActions.Right);
                        RunCall = new ActionCall(ActionType.Mouse, typeof(Fight), "Move", obj3);
                        Util.Global.Sprites.Where(x => x.name == "Right").FirstOrDefault().actionCall.Add(RunCall);
                        break;
                    case Fight.FightActions.Left:
                        Util.Global.Sprites.Add(new Objects.Sprite2d(null, "Left", true, Vector2.Subtract(Position, new Vector2(85, -140)), Util.Global.DefaultPosition, 0, Objects.Base.ControlType.None));
                        Util.Global.Sprites.Where(x => x.name == "Left").FirstOrDefault().text = "Left";
                        Util.Global.Sprites.Where(x => x.name == "Left").FirstOrDefault().textSize = 1f;
                        Util.Global.Sprites.Where(x => x.name == "Left").FirstOrDefault().color = Color.Red;
                        Util.Global.Sprites.Where(x => x.name == "Left").FirstOrDefault().boxColor = Color.Blue;
                        Util.Global.Sprites.Where(x => x.name == "Left").FirstOrDefault().orderNum = 980;
                        obj3 = new List<object>();
                        obj3.Add(Attacker);
                        obj3.Add(Actions.Fight.FightActions.Left);
                        RunCall = new ActionCall(ActionType.Mouse, typeof(Fight), "Move", obj3);
                        Util.Global.Sprites.Where(x => x.name == "Left").FirstOrDefault().actionCall.Add(RunCall);
                        break;
                    case Fight.FightActions.Pass:
                        Util.Global.Sprites.Add(new Objects.Sprite2d(null, "Pass", true, Vector2.Subtract(Position, new Vector2(55, -200)), Util.Global.DefaultPosition, 0, Objects.Base.ControlType.None));
                        Util.Global.Sprites.Where(x => x.name == "Pass").FirstOrDefault().text = "Pass";
                        Util.Global.Sprites.Where(x => x.name == "Pass").FirstOrDefault().textSize = 1f;
                        Util.Global.Sprites.Where(x => x.name == "Pass").FirstOrDefault().color = Color.White;
                        Util.Global.Sprites.Where(x => x.name == "Pass").FirstOrDefault().boxColor = Color.Blue;
                        Util.Global.Sprites.Where(x => x.name == "Pass").FirstOrDefault().orderNum = 980;
                        obj3 = new List<object>();
                        RunCall = new ActionCall(ActionType.Mouse, typeof(Fight), "Turn", obj3);
                        Util.Global.Sprites.Where(x => x.name == "Pass").FirstOrDefault().actionCall.Add(RunCall);
                        break;
                }
            }
        }

        public static void RemoveFightMenu()
        {
            foreach (Actions.Fight.FightActions FA in Enum.GetValues(typeof(Actions.Fight.FightActions)))
            {
                Util.Global.Sprites.RemoveAll(x => x.name == FA.ToString());
            }
        }
    }
}
