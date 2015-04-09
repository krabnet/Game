using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Reflection;

namespace Game.Objects
{
    public class Menu : Objects.Text 
    {
        public Color boxColor { get; set; }
        public int boxHeight { get { return 28; } }
        public int boxWidth { get { return this.text.Length * 13; } }

        public Menu(string _text, string _name, bool _active, Color _color, int _x, int _y, Color _boxColor, Actions.ActionCall _actionCall)
        {
            x = _x;
            y = _y;
            text = _text;
            name = _name;
            active = _active;
            color = _color;
            allowScroll = false;
            boxColor = _boxColor;
            actionCall = _actionCall;
            ID = Guid.NewGuid();
            actionType = ActionType.Mouse;
        }
        public Menu()
        {
            ID = Guid.NewGuid();
        }

        public void DefaultMenu()
        {
            if (Util.Global.Menus.Where(x => x.name == "Exit").Count() == 0)
            {
                Util.Global.Menus.Add(new Objects.Menu("Quit  ", "Exit", false, Color.Black, 0, 0, Color.White, null));
                Util.Global.Menus.Where(x => x.name == "Exit").FirstOrDefault().orderNum = 998;
                Util.Global.Menus.Add(new Objects.Menu("FullScreen", "FullScreen", false, Color.Black, 0, 25, Color.White, null));
                Util.Global.Menus.Where(x => x.name == "FullScreen").FirstOrDefault().orderNum = 998;
            }
        }

        public void AddMenu(Objects.Menu _menu)
        {
            Util.Global.Menus.Add(_menu);
        }
    }
}
