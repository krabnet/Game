using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;
using System.IO;

namespace Game.Util
{
    public class Asset
    {
        public void PopulateContent(ContentManager Content)
        {
            Util.Global.AllTexture2D = new List<Texture2D>();
            List<string> tex = new List<string>();
            List<string> Folders = new List<string>();
            Folders.Add("Images");
            Folders.Add("Images/Item");
            Folders.Add("Images/Enemy");
            Folders.Add("Images/Menu");
            Folders.Add("Images/Prop");
            Folders.Add("Images/Tile");
            Folders.Add("Slides");
            foreach(string Folder in Folders)
            {
                tex = GetContentList(Content, Folder);
                foreach (string S in tex)
                {
                    Texture2D T = Content.Load<Texture2D>(Folder + "/" + S);
                    T.Name = S.ToLower();
                    Util.Global.AllTexture2D.Add(T);
                    string x = Util.Global.AllTexture2D.FirstOrDefault().Name;
                }
            }
            Util.Global.WindowBoarder = 200;
            //Util.Global.Texts = new List<Objects.Text>();
            //Util.Global.Menus = new List<Objects.Menu>();
            Util.Global.Sprites = new List<Objects.Sprite2d>();
            Util.Global.ContentMan = Content;

            //Util.Global.Hero = new Actions.Hero();
            //Util.Global.Hero.init(Content);
            //new Actions.Hero().init(Content);

            //Util.Global.Texts.Add(new Objects.Text("", "mouseCord", true, 50, 50, Color.HotPink));
            //Util.Global.Texts.Where(x => x.name == "mouseCord").FirstOrDefault().controlType = Objects.Base.ControlType.Mouse;
            //Util.Global.Texts.Where(x => x.name == "mouseCord").FirstOrDefault().orderNum = 999;
            //Util.Global.GameFightBackground = new Objects.Sprite2d("valley2", "MainGame_FightBackground", false,new Vector2(1, 1), new Vector2(800, 500), 0, Objects.Base.ControlType.None);
            //Util.Global.GameFightBackground.orderNum = 980;
            //Util.Global.GameFightBackground.LightIgnor = true;
            //Util.Global.GameBackground = new Objects.Sprite2d("gravel", "MainGame_Background", false, new Vector2(-100, -100), new Vector2(3000, 3000), 0, Objects.Base.ControlType.None);
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

        public static Texture2D CaptureScreenShot_old(string Name)
        {
            GraphicsDevice GD = Util.Global.graphicsDevice;
            int width = GD.PresentationParameters.BackBufferWidth;
            int height = GD.PresentationParameters.BackBufferHeight;
            int[] buffer = new int[width * height];
            GD.GetBackBufferData<int>(buffer);
            Texture2D screenShot = new Texture2D(GD, width, height, false, GD.PresentationParameters.BackBufferFormat);
            screenShot.Name = Name.ToLower();
            screenShot.SetData<int>(buffer);
            Util.Global.AllTexture2D.Add(screenShot);
            return screenShot;
            //screenShot.Dispose();
        }

        public static void CaptureScreenShot(string Name)
        {
            Util.Global.ScreenShotFlag = true;
            Util.Global.ScreenShotName = Name;


            
            //GraphicsDevice device = Util.Global.graphicsDevice;
            //string fileName = Name;

            //byte[] screenData = new byte[device.PresentationParameters.BackBufferWidth * device.PresentationParameters.BackBufferHeight * 4];
            //device.GetBackBufferData(screenData);
            //using (Texture2D texture = new Texture2D(device, device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight, false, device.PresentationParameters.BackBufferFormat))
            //{
            //    texture.SetData(screenData);
            //    if (!string.IsNullOrEmpty(fileName))
            //    {
            //        RenderTarget2D newRTex = new RenderTarget2D(texture.GraphicsDevice, texture.Width, texture.Height);
            //        newRTex.SetData(screenData);
            //        //Texture2D newTex = new Texture2D(texture.GraphicsDevice, texture.Width, texture.Height);
            //        newRTex.Name = fileName.ToLower();
            //        Game.Util.Global.AllTexture2D.Add(newRTex);
            //        Game.Util.Base.Log("Adding Texture");
            //        Game.Actions.Say.speak(Game.Util.Global.Hero.ID, "I've activated a Warp Pad!");
            //    }
            //}

            
            //GraphicsDevice device = Util.Global.graphicsDevice;
            //Color[] screenData = new Color[device.PresentationParameters.BackBufferWidth * device.PresentationParameters.BackBufferHeight];
            //RenderTarget2D screenShot = new RenderTarget2D(device,device.PresentationParameters.BackBufferWidth,device.PresentationParameters.BackBufferHeight);
            //device.SetRenderTarget(screenShot);
            //screenShot.Name = Name.ToLower();
            //Util.Global.AllTexture2D.Add((Texture2D)screenShot);

            //Put your drawing method here.  Just copy and paste if from your game class/wherever  
            //you are performing drawing.  Alternatively, you can just pass in to this method whatever object is performing the drawing

            //In My case, I am using the GameStateManagement Sample,  
            //where all screens draw themselves.  So in my case this  
            //method is contained in the Screen Manager Class 
            //Draw();

            //device.SetRenderTarget(null);

            //int index = 0;
            //string name = "Screenshot" + index + ".png";
            //while (File.Exists(name))
            //{
            //    index++;
            //    name = "Screenshot" + index + ".jpg";
            //}

            //using (FileStream stream = new FileStream(name, FileMode.Create))
            //{
            //    screenShot.SaveAsJpeg(stream, screenShot.Width, screenShot.Height);
            //    screenShot.Dispose();
            //}
        } 

        public List<string> GetContentList(ContentManager contentManager, string contentFolder)
        {
            DirectoryInfo dir = new DirectoryInfo(contentManager.RootDirectory + "/" + contentFolder);
            if (!dir.Exists)
            {
                Util.Base.Log("Load Content Fail Missing Directory");
                throw new DirectoryNotFoundException();
            }
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


