using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Actions
{
    public class Inventory
    {
        public Objects.Spite2d GetInventory(ContentManager Content)
        {
            Objects.Spite2d ReturnInv = new Objects.Spite2d(Content.Load<Texture2D>("Images/Inventory"), "Inventory", false, 100, 100, 400, 400, 0, Objects.Base.ControlType.None);
            ReturnInv.orderNum = 5000;
            return ReturnInv;
        }

        public void ShowInventory()
        {
            //Util.Global.Sprites.Where(x => x.name == "Inventory").FirstOrDefault().active = true;
            //Util.Global.Sprites.Where(x => x.name == "Inventory").FirstOrDefault().x = 100;
            //Util.Global.Sprites.Where(x => x.name == "Inventory").FirstOrDefault().y = 100;

            //Util.Global.Sprites.Where(x => x.name == "Inventory").FirstOrDefault().x = Util.Global.SpritesAnim.Where(x => x.name == "Hero").FirstOrDefault().x - 100;
            //Util.Global.Sprites.Where(x => x.name == "Inventory").FirstOrDefault().y = Util.Global.SpritesAnim.Where(x => x.name == "Hero").FirstOrDefault().y - 100;
        }

        public void HideInventory()
        {
            //Util.Global.Sprites.Where(x => x.name == "Inventory").FirstOrDefault().active = false;
        }
    }
}
