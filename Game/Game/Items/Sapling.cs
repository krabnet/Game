using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game.Items
{
    public class Sapling
    {
        public Objects.AnimSprite GetSaplingObject(int x, int y)
        {
            Objects.AnimSprite sapling = new Objects.AnimSprite(new Maps.Asset().getContentByName("treegrow"), "Tree", true, 12, 11, x, y, 100, Objects.Base.ControlType.None);
            sapling.actionType = Objects.Base.ActionType.Update;
            sapling.orderNum = 100;
            sapling.action = true;
            sapling.currentFrame = 0;
            sapling.totalFrames = 127;
            //List<Object> Objs3 = new List<object>();
            //Objs3.Add(sapling);
            //sapling.actionCall = new Actions.ActionCall(typeof(Items.Sapling), "update", Objs3);
            return sapling;
        }

        public Objects.Spite2d GetSaplingItem(int x, int y)
        {
            Objects.Spite2d sapling = new Objects.Spite2d(new Maps.Asset().getContentByName("sapling"), "Sapling", true, x, y, 50, 50, 0, Objects.Base.ControlType.None);
            sapling.actionType = Objects.Base.ActionType.Mouse;
            sapling.orderNum = 100;
            List<Object> Objs3 = new List<object>();
            Objs3.Add(sapling);
            sapling.actionCallB = new Actions.ActionCall(typeof(Items.Sapling), "pickupSapling", Objs3);
            return sapling;
        }

        public void pickupSapling(Objects.Spite2d sapling)
        {
            sapling.controlType = Objects.Base.ControlType.Mouse;
            List<Object> Objs3 = new List<object>();
            sapling.actionCall = new Actions.ActionCall(typeof(Items.Sapling), "AddSapling", Objs3);
            List<Object> Objs2 = new List<object>();
            Objs2.Add(sapling);
            sapling.actionCallB = new Actions.ActionCall(typeof(Items.Sapling), "DropSapling", Objs2);
        }

        public void DropSapling(Objects.Spite2d sapling)
        {
            sapling.controlType = Objects.Base.ControlType.None;
            List<Object> Objs3 = new List<object>();
            Objs3.Add(sapling);
            sapling.actionCallB = new Actions.ActionCall(typeof(Items.Sapling), "pickupSapling", Objs3);
        }

        public void AddSapling()
        {
            int x = Util.Global.GameMouse.x;
            int y = Util.Global.GameMouse.y;

            Util.Base B = new Util.Base();
            List<Objects.Spite2d> spritsintheway = Util.Global.Sprites.Where(l => B.collision(l, new Rectangle(x, y, 50, 50)) == true && l.active == true && l.orderNum > 50 && l.name != "Sapling" && l.name != "MainGame_mouse").ToList();
            List<Objects.AnimSprite> Aspritsintheway = Util.Global.SpritesAnim.Where(l => B.collision(l, new Rectangle(x, y, 50, 50)) == true && l.active == true).ToList();
            if (spritsintheway.Count == 0 && Aspritsintheway.Count==0)
            {
                Objects.AnimSprite AS = GetSaplingObject(x, y);
                Util.Global.SpritesAnim.Add(AS);
                Util.Global.MainMap[(int)Util.Global.CurrentMap.X, (int)Util.Global.CurrentMap.Y, (int)Util.Global.CurrentMap.Z].AnimSprite.Add(AS);
            }
        }
    }
}
