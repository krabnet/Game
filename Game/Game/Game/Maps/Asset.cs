using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;
using System.IO;

namespace Game.Maps
{
    public class Asset
    {
        public void PopulateContent(ContentManager Content)
        {
            Util.Global.AllTexture2D = new List<Texture2D>();
            List<string> tex = GetContentList(Content, "Images");
            foreach(string S in tex)
            {
                Texture2D T = Content.Load<Texture2D>("Images/" + S);
                T.Name = S;
                Util.Global.AllTexture2D.Add(T);
                string x =Util.Global.AllTexture2D.FirstOrDefault().Name;
            }
            List<string> slide = GetContentList(Content, "Slides");
            foreach (string S in slide)
            {
                Texture2D T = Content.Load<Texture2D>("Slides/" + S);
                T.Name = S;
                Util.Global.AllTexture2D.Add(T);
                string x = Util.Global.AllTexture2D.FirstOrDefault().Name;
            }

            Util.Global.WindowBoarder = 200;
            //Util.Global.Texts = new List<Objects.Text>();
            //Util.Global.Menus = new List<Objects.Menu>();
            Util.Global.Sprites = new List<Objects.Sprite2d>();
            Util.Global.SpritesAnim = new List<Objects.AnimSprite>();
            Util.Global.ContentMan = Content;

            //Util.Global.Hero = new Actors.Hero();
            //Util.Global.Hero.init(Content);
            new Actors.Hero().init(Content);

            //Util.Global.Texts.Add(new Objects.Text("", "mouseCord", true, 50, 50, Color.HotPink));
            //Util.Global.Texts.Where(x => x.name == "mouseCord").FirstOrDefault().controlType = Objects.Base.ControlType.Mouse;
            //Util.Global.Texts.Where(x => x.name == "mouseCord").FirstOrDefault().orderNum = 999;
            Util.Global.GameFightBackground = new Objects.Sprite2d(Content.Load<Texture2D>("Images/valley2"), "MainGame_FightBackground", false,new Vector2(1, 1), new Vector2(800, 500), 0, Objects.Base.ControlType.None);
            Util.Global.GameFightBackground.orderNum = 980;
            Util.Global.GameFightBackground.LightIgnor = true;
            Util.Global.GameBackground = new Objects.Sprite2d(Content.Load<Texture2D>("Images/gravel"), "MainGame_Background", false, new Vector2(-100, -100), new Vector2(3000, 3000), 0, Objects.Base.ControlType.None);
            //S.AddSprite2d(new Actions.Inventory().GetInventory(Content));
        }

        public Texture2D getContentByName(string name)
        {
            return Util.Global.AllTexture2D.Where(x => x.Name == name).FirstOrDefault();
        }

        public static void MoveTiles(int changex, int changey)
        {
            //foreach (Objects.Sprite2d S in Util.Global.Sprites.Where(x => x.controlType == Objects.Base.ControlType.None))
            //{
            //    S.x = S.x + changex;
            //    S.y = S.y + changey;
            //}

            //foreach (Objects.Text S in Util.Global.Texts.Where(x => x.controlType == Objects.Base.ControlType.None))
            //{
            //    S.x = S.x + changex;
            //    S.y = S.y + changey;
            //}

            //foreach (Objects.AnimSprite S in Util.Global.SpritesAnim.Where(x => x.controlType == Objects.Base.ControlType.None))
            //{
            //    S.x = S.x + changex;
            //    S.y = S.y + changey;
            //}
        }

        public List<string> GetContentList(ContentManager contentManager, string contentFolder)
        {
            DirectoryInfo dir = new DirectoryInfo(contentManager.RootDirectory + "/" + contentFolder);
            if (!dir.Exists)
                throw new DirectoryNotFoundException();
            List<string> result = new List<string>();

            FileInfo[] files = dir.GetFiles("*.*");
            foreach (FileInfo file in files)
            {
                result.Add(Path.GetFileNameWithoutExtension(file.Name));
            }
            return result;
        }
    }
}


