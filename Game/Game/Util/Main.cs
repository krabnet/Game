using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game.Actions;

namespace Game.Util
{
    public class Main
    {
        public void Init()
        {
            //video = Content.Load<Video>("Wildlife");
            //player = new VideoPlayer();
            Sys.DeleteLog();

            Util.Global.rnd = new Random();
            Util.Global.Pets = new List<Objects.Sprite2d>();
            //Util.Global.Cam.Zoom = .9f;
            Util.Global.DrawDistance = 500;
            Util.Global.DayColor = Color.White;
            Util.Global.GameClock = new TimeSpan(0);
            Util.Global.GameClockDay = 1;
            Util.Global.GameClockRandomSpeech = Util.Global.GetRandomInt(30, 599);
            Util.Global.Weather = Actions.Enviro.Weather.Clear;
            Util.Global.Season = Actions.Enviro.SeasonType.Spring;
            Util.Global.ActionEvents = new List<Actions.ActionEvents>();
            Util.Global.MouseHistory = new List<GameMouseHistory>();
            Util.Global.Warp = new List<Items.Warp>();
            Util.Global.Journal = new List<string>();
            Util.Global.MouseHistoryFlag = true;
            Util.Global.RunFullScreenToggle = false;
            Util.Global.QuitGameToggle = false;
            Util.Global.PauseGameToggle = false;
            Util.Global.ScreenShotFlag = false;
            //SpeechSynthesizer synth = new SpeechSynthesizer();
            //synth.SetOutputToDefaultAudioDevice();
            //synth.Speak("Welcome to the game!");
            new Util.Asset().PopulateContent(Util.Global.ContentMan);
            initHero();
            Util.Global.MainMap = new Maps.Map[100, 100, 100];
            Util.Global.GameMouseTime = 0;
            Actions.Fight.init();
            Util.Global.UpdateClock = 100;
            Util.Global.SoundMute = false;
            Actions.Anim.InitRain();

            Maps.Map Smap = new Maps.Map();
            Util.Global.CurrentMap = Util.Global.StartLocation;
            Smap.GenerateMap(Util.Global.StartLocation, Game.Maps.MapData.Biome.Grass, Util.Global.SizeMap, Util.Global.SizeMap);
            new Maps.Map().LoadMapByInt((int)Util.Global.StartLocation.X, (int)Util.Global.StartLocation.Y, (int)Util.Global.StartLocation.Z);
            Actions.Season.GetMusic();
            Actions.Season.GetMusicBackGround();
            //Smap.AddMapPart(Maps.MapPart.mountain, 7, 7);
            //new Maps.Map().LoadMapByInt(99, 99, 98);
            //new Maps.Map().GenerateMapPartMap(Maps.MapPart.start);
            Vector2 DefaultPosition = new Vector2(-1000, -1000);
            Items.ItemActions.AddItemToInventory(Items.Item.GetItemByType(Items.Item.ItemType.Log, Util.Global.DefaultPosition), 35, 1);
            Items.ItemActions.AddItemToInventory(Items.Item.GetItemByType(Items.Item.ItemType.IronBar, Util.Global.DefaultPosition), 35, 1);
            Items.ItemActions.AddItemToInventory(Items.Item.GetItemByType(Items.Item.ItemType.GoldBar, Util.Global.DefaultPosition), 6, 1);
            Items.ItemActions.AddItemToInventory(Items.Item.GetItemByType(Items.Item.ItemType.BerryHealth, Util.Global.DefaultPosition), 5, 1);
            Items.ItemActions.AddItemToInventory(Items.Item.GetItemByType(Items.Item.ItemType.BerryStrength, Util.Global.DefaultPosition), 5, 1);
            Items.ItemActions.AddItemToInventory(Items.Item.GetItemByType(Items.Item.ItemType.Coin, Util.Global.DefaultPosition), 10, 1);
            Items.ItemActions.AddItemToInventory(Items.Item.GetItemByType(Items.Item.ItemType.Bread, Util.Global.DefaultPosition), 1, 1);

            //Items.ItemActions.AddItemToInventory(Items.Item.GetItemByType(Items.Item.ItemType.Glass, Util.Global.DefaultPosition), 10, 1);
            Items.ItemActions.AddItemToInventory(Items.Item.GetItemByType(Items.Item.ItemType.Hammer, Util.Global.DefaultPosition), 1, 1);
            //Items.ItemActions.AddItemToInventory(Items.Item.GetItemByType(Items.Item.ItemType.Torch, Util.Global.DefaultPosition), 1, 1);
            Items.ItemActions.AddItemToInventory(Items.Item.GetItemByType(Items.Item.ItemType.Axe, Util.Global.DefaultPosition), 1, 1);
            Items.ItemActions.AddItemToInventory(Items.Item.GetItemByType(Items.Item.ItemType.Shovel, Util.Global.DefaultPosition), 1, 1);
            //Items.ItemActions.AddItemToInventory(Items.Item.GetItemByType(Items.Item.ItemType.PotionStrength, Util.Global.DefaultPosition), 10, 1);
            //Items.ItemActions.AddItemToInventory(Items.Item.GetItemByType(Items.Item.ItemType.PotionSpeed, Util.Global.DefaultPosition), 10, 1);
            //Items.ItemActions.AddItemToInventory(Items.Item.GetItemByType(Items.Item.ItemType.Sand, Util.Global.DefaultPosition), 10, 1);
            

            Util.Global.Stable = new List<Objects.Sprite2d>();
            //Util.Global.Stable.Add(Actions.Enemy.GetEnemyByType(Actions.Enemy.EnemyType.LightBug, new Vector2(100, 100)));
            //Util.Global.Stable.Add(Actions.Enemy.GetEnemyByType(Actions.Enemy.EnemyType.Fly, new Vector2(100, 100)));
            //Util.Global.Stable.Add(Actions.Enemy.GetEnemyByType(Actions.Enemy.EnemyType.Lizard, new Vector2(100, 100)));
            //Util.Global.Stable.Add(Actions.Enemy.GetEnemyByType(Actions.Enemy.EnemyType.Turtle, new Vector2(100, 100)));
            //Util.Global.Stable.Add(Actions.Enemy.GetEnemyByType(Actions.Enemy.EnemyType.Moth, new Vector2(100, 100)));
            //Util.Global.Stable.Add(Actions.Enemy.GetEnemyByType(Actions.Enemy.EnemyType.Snake, new Vector2(100, 100)));
            //Util.Global.Stable.Add(Actions.Enemy.GetEnemyByType(Actions.Enemy.EnemyType.Bee, new Vector2(100, 100)));

            //new Actions.Pet().GetPet(1, 10, 100, 100);
            
            //Util.Global.Texts.Add(new Game.Objects.Text("Counter", "Counter", true, 10, 10, Color.Black));

            Util.Base.GetLightSources();
        }


        public void initHero()
        {
            Objects.Sprite2d H = new Objects.Sprite2d("HeroDown", "Hero", true, new Vector2(1600, 1600), new Vector2(32, 32), 5, Objects.Base.ControlType.Keyboard);
            H.AnimSprite = new Objects.AnimSprite(7, 1);
            H.AnimSprite.action = true;
            H.AnimSprite.StopOnEof = false;
            H.clipping = true;
            H.orderNum = 990;
            H.speed = 4;
            H.Size = new Microsoft.Xna.Framework.Vector2(90 / 4, 250 / 4);
            //H.LightSourceDistance = 175f;

            Objects.Actor actor = new Objects.Actor();
            actor.Level = 1;
            actor.Experience = 0;
            actor.Health = 10;
            actor.actorType = Objects.Actor.ActorType.Hero;
            actor.CalculateStats();
            actor.Hunger = 1800;
            actor.HungerMax = 1800;
            actor.Buffs = new List<Game.Actions.Buff.BuffType>();
            H.Actor = actor;
            Util.Global.Hero = H;

            //actor.Buffs.Add(Buff.BuffType.Speed);

            List<Object> obj2 = new List<object>();
            obj2.Add(H);
            ActionCall call2 = new ActionCall(ActionType.MouseAny, typeof(Menu.ActorStats), "ToggleStats", obj2);
            H.actionCall.Add(call2);

            Util.Global.Sprites.Add(H);
        }
    }
}
