using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;
using Microsoft.Xna.Framework;
using Game.Actions;

namespace Game.Util
{
    [Serializable]
    public class Save
    {
        public Maps.Map[, ,] MainMap { get; set; }
        public Objects.Sprite2d Hero { get; set; }
        public List<Objects.Sprite2d> Pets { get; set; }
        public List<Objects.Sprite2d> Stable { get; set; }
        public TimeSpan GameClock { get; set; }
        public int GameClockDay { get; set; }
        public int GameClockPreviousSecond { get; set; }
        public Enviro.SeasonType SeasonSave { get; set; }
        public Vector3 CurrentMap { get; set; }
        public Enviro.Weather Weather { get; set; }
        public List<Items.Warp> Warp { get; set; }
        public bool Fighting { get; set; }
    }

    
    public static class Sys
    {

        public static void Save()
        {
            Actions.Say.RemoveAllSpeach();
            if (!Util.Global.Fighting)
            {
                List<Objects.Sprite2d> CurrentState = Util.Global.Sprites.Where(x => x.ID != Util.Global.Hero.ID && x.Maneuver == null).ToList();
                Util.Global.MainMap[(int)Util.Global.CurrentMap.X, (int)Util.Global.CurrentMap.Y, (int)Util.Global.CurrentMap.Z].Sprite2d = CurrentState;

                Save GameSave = new Util.Save();
                GameSave.MainMap = Util.Global.MainMap;
                GameSave.Hero = Util.Global.Hero;
                GameSave.Pets = Util.Global.Pets;
                GameSave.Stable = Util.Global.Stable;
                GameSave.GameClock = Util.Global.GameClock;
                GameSave.GameClockDay = Util.Global.GameClockDay;
                GameSave.GameClockPreviousSecond = Util.Global.GameClockPreviousSecond;
                GameSave.SeasonSave = Util.Global.Season;
                GameSave.CurrentMap = Util.Global.CurrentMap;
                GameSave.Weather = Util.Global.Weather;
                GameSave.Warp = Util.Global.Warp;

                SerializeObject(GameSave, "GameSave.Dat");
            }
        }

        public static void Load()
        {
            if (!Util.Global.Fighting)
            {
                Util.Global.Sprites = null;
                Util.Global.MainMap = null;

                Save GameSave = (Save)DeSerializeObject("GameSave.Dat");
                Util.Global.MainMap = GameSave.MainMap;
                Util.Global.Hero = GameSave.Hero;
                Util.Global.Pets = GameSave.Pets;
                Util.Global.Stable = GameSave.Stable;
                Util.Global.GameClock = GameSave.GameClock;
                Util.Global.GameClockDay = GameSave.GameClockDay;
                Util.Global.GameClockPreviousSecond = GameSave.GameClockPreviousSecond;
                Util.Global.Season = GameSave.SeasonSave;
                Util.Global.CurrentMap = GameSave.CurrentMap;
                Util.Global.Weather = GameSave.Weather;
                Util.Global.Warp = GameSave.Warp;

                Util.Global.Sprites = Util.Global.MainMap[(int)Util.Global.CurrentMap.X, (int)Util.Global.CurrentMap.Y, (int)Util.Global.CurrentMap.Z].Sprite2d;
                Util.Global.Sprites.Add(Util.Global.Hero);
                Util.Global.Sprites.Where(x => x.ID == Util.Global.Hero.ID).FirstOrDefault().Position = Util.Global.Hero.Position;
                foreach (Objects.Sprite2d S in Util.Global.Pets)
                {
                    Objects.Sprite2d X = Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault();
                    if (X != null)
                    {
                        Util.Global.Sprites.Where(x => x.ID == X.ID).FirstOrDefault().Position = Util.Global.Hero.Position;
                    }
                }
                Season.CheckWeather();
            }
        }

        public static void WritetoLog(string Log)
        {
            string filename = "GameLog.txt";
            using (StreamWriter sw = File.AppendText(filename))
            {
                sw.WriteLine(Log);
            }
        }

        public static void DeleteLog()
        {
            string filename = "GameLog.txt";
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            StreamWriter fs = File.CreateText(filename);
            fs.Close();
        }

        public static void SerializeObject(object Ser, string file)
        {
            FileStream fs = new FileStream(file, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, Ser);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                Util.Base.Log("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        public static object DeSerializeObject(string file)
        {
            if (File.Exists(file))
            {
                FileStream fs = new FileStream(file, FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return formatter.Deserialize(fs);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                    Util.Base.Log("Failed to deserialize. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
            else
            { return null; }
        }
    }
}
