using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game.Actions
{
    public static class Progress
    {
        public static void AddProgressBar()
        {
            //how to change progress speed, and what sprite to progress on, and final action?
            string name = "ProgressTest";
            Vector2 pos = Util.Global.Hero.Position;
            Util.Global.Sprites.Add(new Objects.Sprite2d("drop", name, true, pos, new Vector2(25, 3), 10, Objects.Base.ControlType.None));
            Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().orderNum = 1000;
            Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().LightIgnor = true;


            name = "ProgressTestProg";
            pos = Util.Global.Hero.Position;
            Util.Global.Sprites.Add(new Objects.Sprite2d("drop", name, true, Vector2.Subtract(pos, new Vector2(1,0)), new Vector2(1, 3), 5, Objects.Base.ControlType.None));
            Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().color = Color.Red;
            Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().orderNum = 1000;
            Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().LightIgnor = true;

            List<object> obj1 = new List<object>();
            Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Update, typeof(Progress), "UpdateProgressBar", obj1));


        }

        public static void UpdateProgressBar()
        {
            string name = "ProgressTestProg";
            Vector2 size = Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().Size;
            size = Vector2.Add(size, new Vector2(1, 0));
            Util.Global.Sprites.Where(x => x.name == name).FirstOrDefault().Size = size;

            if (size.X >= 25)
            {
                Util.Global.Sprites.RemoveAll(x => x.name == name);
                name = "ProgressTest";
                Util.Global.Sprites.RemoveAll(x => x.name == name);
            }

        }

    }
}
