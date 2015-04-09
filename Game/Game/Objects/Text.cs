using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game.Objects
{
    public class Text : Base
    {
        public string text { get; set; }
        public Color color { get; set; }

        public Text(string _text, string _name, bool _active, int _x, int _y, Color _color)
        {
            text = _text;
            name = _name;
            active = _active;
            color = _color;
            allowScroll = false;
            x = _x;
            y = _y;
            ID = Guid.NewGuid();
        }

        public Text()
        {
            ID = Guid.NewGuid();
        }

        public void AddText(Objects.Text _text)
        {
            Util.Global.Texts.Add(_text);
        }
       
    }
}
