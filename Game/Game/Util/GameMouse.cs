using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Game.Util
{
    public class GameMouseHistory
    {
        public int TotalGameSeconds { get; set; }
        public Objects.Base.ViewType ViewType { get; set; }
        public Vector2 MousePosition { get; set; }
    }

    public static class GameMouse
    {
        public static Vector2 GetTrueMousePosition(Objects.Base.ViewType View)
        {
            if (View == Objects.Base.ViewType.Default)
            {
                MouseState mouseSprite = Mouse.GetState();
                Matrix inverseViewMatrix = Matrix.Invert(Util.Global.Cam.Transform);
                Vector2 worldMousePosition = Vector2.Transform(new Vector2(mouseSprite.X, mouseSprite.Y), inverseViewMatrix);
                return new Vector2(worldMousePosition.X, worldMousePosition.Y);
            }
            else if (View == Objects.Base.ViewType.HUD)
            {
                return new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            }


            return Util.Global.DefaultPosition;
        }

        public static void MouseClick(MouseState ms)
        {
           bool allowaction = true;
            List<Objects.Sprite2d> MouseObjects = Util.Global.Sprites.Where(x => x.actionCall.Count() > 0).OrderByDescending(y => y.orderNum).ToList();
           foreach(Objects.Sprite2d S in MouseObjects)
           {
               if (allowaction)
               {
                   Vector2 MP1 = Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.Default);
                   Vector2 MP2 = new Vector2(ms.X, ms.Y);
                   Rectangle rect1 = new Rectangle((int)MP1.X, (int)MP1.Y, 1, 1);
                   Rectangle rect3 = new Rectangle((int)MP2.X, (int)MP2.Y, 1, 1);
                   Rectangle rect2 = new Rectangle((int)S.Position.X, (int)S.Position.Y, (int)S.Size.X, (int)S.Size.Y);
                   if ((S.Viewtype == Objects.Base.ViewType.Default && Util.Base.collision(rect1, rect2)) || (S.Viewtype == Objects.Base.ViewType.HUD && Util.Base.collision(rect3, rect2)))
                   {
                       Util.Base.Log("ActionOn:"+S.name);
                       allowaction = false;
                       if (ms.LeftButton == ButtonState.Pressed)
                       {
                           foreach (Actions.ActionCall AC in S.actionCall.Where(x => x.ActionType == Actions.ActionType.Mouse).ToList())
                           {
                               if (S.Item != null && S.Item.State == Items.Item.ItemState.Inventory)
                               { }
                               else
                               {
                                   Util.Base.Log("LeftButtonAction:" + AC.actionMethodName);
                                   Util.Base.CallMethodByString(AC.Type, AC.actionMethodName, AC.parameters);
                               }
                           }
                           foreach (Actions.ActionCall AC in S.actionCall.Where(x => x.ActionType == Actions.ActionType.Item).ToList())
                           {
                               Util.Base.Log("ItemAction:" + AC.actionMethodName);
                               Util.Base.CallMethodByString(AC.Type, AC.actionMethodName, AC.parameters);
                           }
                       }
                       if (ms.RightButton == ButtonState.Pressed)
                       {
                           foreach (Actions.ActionCall AC in S.actionCall.Where(x => x.ActionType == Actions.ActionType.MouseRight).ToList())
                           {
                               Util.Base.Log("RightButtonAction:" + AC.actionMethodName);
                               Util.Base.CallMethodByString(AC.Type, AC.actionMethodName, AC.parameters);
                           }
                       }

                       if (ms.RightButton == ButtonState.Pressed || ms.LeftButton == ButtonState.Pressed)
                       {
                           foreach (Actions.ActionCall AC in S.actionCall.Where(x => x.ActionType == Actions.ActionType.MouseAny).ToList())
                           {
                               Util.Base.Log("AnyButtonAction:" + AC.actionMethodName);
                               Util.Base.CallMethodByString(AC.Type, AC.actionMethodName, AC.parameters);
                           }
                       }
                   }
               }
           }
        }

        public static void DetectMouseHover()
        {
            Util.GameMouseHistory GMH = new Util.GameMouseHistory();
            GMH.TotalGameSeconds = Convert.ToInt32(Util.Global.GameClock.TotalSeconds);
            GMH.ViewType = Objects.Base.ViewType.HUD;
            GMH.MousePosition = Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.HUD);
            Util.Global.MouseHistory.Add(GMH);

            if (Util.Global.MouseHistory.Count() > 3 && Util.Global.GameMouseTime < 200)
            {
                //Util.Global.MouseHistory.Remove(Util.Global.MouseHistory.OrderBy(x => x.TotalGameSeconds).FirstOrDefault());
                Util.Global.MouseHistory.RemoveRange(0, Util.Global.MouseHistory.Count()-3);

                if (Util.Global.Sprites.Where(x => x.Item != null && x.Item.State == Items.Item.ItemState.Hand).Count() == 0)
                {
                    if (Util.Global.MouseHistory.FirstOrDefault().MousePosition.X > Util.Global.MouseHistory.Last().MousePosition.X - 20 &&
                        Util.Global.MouseHistory.FirstOrDefault().MousePosition.X < Util.Global.MouseHistory.Last().MousePosition.X + 20 &&
                        Util.Global.MouseHistory.FirstOrDefault().MousePosition.Y > Util.Global.MouseHistory.Last().MousePosition.Y - 20 &&
                        Util.Global.MouseHistory.FirstOrDefault().MousePosition.Y < Util.Global.MouseHistory.Last().MousePosition.Y + 20)
                    {
                        List<Objects.Sprite2d> SIR = Util.Global.Sprites.Where(l => l.Item != null && l.active == true && l.Item.Description != null).ToList();
                        List<Objects.Sprite2d> spritsintheway = new List<Objects.Sprite2d>();
                        foreach (Objects.Base.ViewType VT in Enum.GetValues(typeof(Objects.Base.ViewType)))
                        {
                            spritsintheway.AddRange(SIR.Where(l => Util.Base.collision(l, new Rectangle((int)Util.GameMouse.GetTrueMousePosition(VT).X, (int)Util.GameMouse.GetTrueMousePosition(VT).Y, 1, 1)) == true).ToList());
                        }

                        if (Util.Global.MouseHistoryFlag && spritsintheway.Count() > 0)
                        {
                            Actions.Say.speak(Util.Global.Hero.ID, spritsintheway.FirstOrDefault().Item.Description);
                            Util.Global.MouseHistoryFlag = false;
                        }
                    }
                    else
                    {
                        Util.Global.MouseHistoryFlag = true;
                    }
                }
            }


        }
    }
}
