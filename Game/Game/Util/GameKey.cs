using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Game.Util
{
    public static class GameKey
    {
        public static void Key(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.F1))
            {
                Util.Sys.Save();
            }
            if (state.IsKeyDown(Keys.F2))
            {
                Util.Sys.Load();
            }
            if (state.IsKeyDown(Keys.F4))
            {
                Actions.Season.MoveToNextSeason();
            }

            if (state.IsKeyDown(Keys.F5))
            {
                Util.Global.Weather = Actions.Enviro.Weather.Snow;
                Actions.Season.CheckWeather();
            }
            if (state.IsKeyDown(Keys.F6))
            {
                Actions.Anim.AnimTest5();
            }

            if (state.IsKeyDown(Keys.F9) && !Util.Global.PreviousKeyboardState.IsKeyDown(Keys.F9))
            {
                Util.Global.ScreenShotFlag = true;
            }

            if (state.IsKeyDown(Keys.F10) && !Util.Global.PreviousKeyboardState.IsKeyDown(Keys.F10))
            {
                //Actions.Progress.AddProgressBar();
                Actions.PetAbility.ChopWood(null); 
                //Actions.Buff.AddBuff(Util.Global.Hero, Actions.Buff.BuffType.Blindness, 30);
            }
            

            if (state.IsKeyDown(Keys.F11) && !Util.Global.PreviousKeyboardState.IsKeyDown(Keys.F11))
            {
                Util.Global.RunFullScreenToggle = true;
            }
            if (state.IsKeyDown(Keys.F12))
            {
                Util.Global.Weather = Actions.Enviro.Weather.Clear;
                Actions.Season.GetMusicBackGround();
                new Objects.Maneuver().RemoveAll();
            }

            if (state.IsKeyDown(Keys.Add))
            {
                Actions.Season.AddTime();
            }
            if (state.IsKeyDown(Keys.Subtract))
            {
                Actions.Season.SubtractTime();
            }
            //if (state.IsKeyDown(Keys.Q))
            //{
            //    Util.Global.Cam.Rotation += 0.05f;
            //}
            //if (state.IsKeyDown(Keys.R))
            //{
            //    Util.Global.Cam.Rotation -= 0.05f;
            //}
            //if (ms.ScrollWheelValue < previousScrollValue)
            //{
            //    Util.Global.Cam.Zoom -= 0.1f;
            //}
            //if (ms.ScrollWheelValue > previousScrollValue)
            //{
            //    Util.Global.Cam.Zoom += 0.1f;
            //}
            //previousScrollValue = ms.ScrollWheelValue;
           
            
            

            if (state.IsKeyDown(Keys.Tab) && !Util.Global.PreviousKeyboardState.IsKeyDown(Keys.Tab))
            {
                if (Util.Global.Sprites.Where(x => x.name == "Inventory").FirstOrDefault() == null)
                { Menu.Inventory.DisplayInventory(); }
                else
                { Menu.Inventory.HideInventory(); }
            }

            if (state.IsKeyDown(Keys.H) && !Util.Global.PreviousKeyboardState.IsKeyDown(Keys.H))
            {
                if (Menu.Hotbar.IsOpen())
                { Menu.Hotbar.Hide(); }
                else
                { Menu.Hotbar.Display(); }
            }

            if (state.IsKeyDown(Keys.M) && !Util.Global.PreviousKeyboardState.IsKeyDown(Keys.M))
            {
                if (Util.Global.Sprites.Where(x => x.name.Contains("HUD_MiniMap")).Count() > 0)
                { Menu.MiniMap.HideMiniMap(); }
                else
                { Menu.MiniMap.ShowMiniMap();}
            }

            if (state.IsKeyDown(Keys.J) && !Util.Global.PreviousKeyboardState.IsKeyDown(Keys.J))
            {
                if (Util.Global.Sprites.Where(x => x.name.Contains("HUD_Journal")).Count() > 0)
                { Menu.Journal.Hide(); }
                else
                { Menu.Journal.Show(); }
            }

            if (state.IsKeyDown(Keys.C) && !Util.Global.PreviousKeyboardState.IsKeyDown(Keys.C))
            {
                if (Util.Global.Sprites.Where(x => x.name == "Craft").FirstOrDefault() == null)
                { Menu.Craft.DisplayCraft(Actions.Crafting.Type.Hand); }
                else
                { Menu.Craft.HideCraft(); }
            }

            if (state.IsKeyDown(Keys.T) && !Util.Global.PreviousKeyboardState.IsKeyDown(Keys.T))
            {
                if (Util.Global.Sprites.Where(x => x.name == "Stats" + Util.Global.Hero.ID.ToString()).FirstOrDefault() == null)
                { Menu.ActorStats.DisplayStats(Util.Global.Hero); }
                else
                { Menu.ActorStats.RemoveStats(Util.Global.Hero); }
            }

            if (state.IsKeyDown(Keys.Q) && !Util.Global.PreviousKeyboardState.IsKeyDown(Keys.Q))
            {
                Items.ItemActions.DropItemInHand();
            }

            if (state.IsKeyDown(Keys.Escape) && !Util.Global.PreviousKeyboardState.IsKeyDown(Keys.Escape))
            {
                if (Util.Global.Sprites.Where(x => x.name.Contains("HUD_Escape")).Count() > 0)
                { Menu.Escape.Hide(); }
                else
                { Menu.Escape.Show(); }
            }

            List<Keys> CheckKeys = new List<Keys>();
            CheckKeys.Add(Keys.D1);
            CheckKeys.Add(Keys.D2);
            CheckKeys.Add(Keys.D3);
            CheckKeys.Add(Keys.D4);
            CheckKeys.Add(Keys.D5);
            CheckKeys.Add(Keys.D6);
            CheckKeys.Add(Keys.D7);
            CheckKeys.Add(Keys.D8);
            int i = 1;
            foreach (Keys K in CheckKeys)
            {
                if (state.IsKeyDown(K) && !Util.Global.PreviousKeyboardState.IsKeyDown(K))
                {
                    if (Util.Global.Sprites.Where(x => x.Item != null && (x.Item.State == Items.Item.ItemState.Hotbar||x.Item.State == Items.Item.ItemState.Hand) && x.Item.ItemSlot == i).Count() == 1)
                    {
                        Items.ItemActions.HotbarAction(i);
                    }
                }
                i++;
            }
        }

    }
}
