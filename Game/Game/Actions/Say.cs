using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Game.Actions
{
    public enum SpeechType { Random, MakePet };

    public static class Say
    {
        public static void speak(Guid ActorID, string Speech)
        {
            if (!string.IsNullOrWhiteSpace(Speech) && Util.Global.Sprites.Where(x => x.ID == ActorID).Count() > 0)
            {
                speakEnd(ActorID);
                float PrintSize = 1f;
                Vector2 TextSize = Util.Global.font.MeasureString(Speech);
                TextSize = Vector2.Multiply(TextSize, PrintSize * 1.2F);
                TextSize = Vector2.Add(TextSize, new Vector2(30, 30));

                Vector2 Position = Util.Global.Sprites.Where(x => x.ID == ActorID).FirstOrDefault().Position;
                Vector2 BasePos = Vector2.Add(Position, new Vector2(-50, -55));

                Vector2 Size = TextSize;
                Objects.Sprite2d SpriteItem = new Objects.Sprite2d("speech", ActorID.ToString() + "SpeechSAY", true, BasePos, Size, 5, Objects.Base.ControlType.None);
                SpriteItem.orderNum = 2000;
                SpriteItem.SpriteType = Objects.Base.Type.Tile;

                List<Object> obj2 = new List<object>();
                obj2.Add(ActorID);
                ActionCall call2 = new ActionCall(ActionType.Update, typeof(Say), "speakMove", obj2);
                SpriteItem.actionCall.Add(call2);
                Util.Global.Sprites.Add(SpriteItem);

                BasePos = Vector2.Add(BasePos, new Vector2(10, 4));
                string Name = ActorID.ToString() + "SpeechSAYTEXT";
                Util.Global.Sprites.Add(new Objects.Sprite2d(null, Name, true, BasePos, Size, 0, Objects.Base.ControlType.None));
                string Stat = "";
                Stat = Stat + Speech;
                Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().text = Stat;
                Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().textSize = PrintSize;
                Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().color = Color.Black;
                Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().orderNum = 2001;

                Util.Global.ContentMan.Load<SoundEffect>("Sounds/speech" + Util.Global.GetRandomInt(1, 3).ToString()).Play();

                ActionEvents AE = new ActionEvents();
                List<object> obj1 = new List<object>();
                obj1.Add(ActorID);
                AE.actionCall.Add(new ActionCall(ActionType.Item, typeof(Say), "speakEnd", obj1));
                int Duration = Convert.ToInt32(Math.Round(Speech.Length * .2, MidpointRounding.AwayFromZero));
                AE.Duration = Duration;
                AE.InitialDuration = Duration;
                Util.Global.ActionEvents.Add(AE);
            }
        }

        public static void RemoveAllSpeach()
        {
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("SpeechSAY"));
            Util.Global.Sprites.RemoveAll(x => x.name.Contains("SpeechSAYTEXT"));
        }

        public static void speakEnd(Guid ActorID)
        {
            Util.Global.Sprites.RemoveAll(x => x.name == ActorID.ToString() + "SpeechSAY");
            Util.Global.Sprites.RemoveAll(x => x.name == ActorID.ToString() + "SpeechSAYTEXT");
        }

        public static void speakMove(Guid ActorID)
        {
            Sprite2d SP = Util.Global.Sprites.Where(x => x.ID == ActorID).FirstOrDefault();
            if (SP != null)
            {
                Vector2 Position = SP.Position;
                Vector2 BasePos = Vector2.Add(Position, new Vector2(-50, -55));
                Util.Global.Sprites.Where(x => x.name == ActorID.ToString() + "SpeechSAY").FirstOrDefault().Position = BasePos;
                BasePos = Vector2.Add(BasePos, new Vector2(20, 4));
                Util.Global.Sprites.Where(x => x.name == ActorID.ToString() + "SpeechSAYTEXT").FirstOrDefault().Position = BasePos;
            }
        }

        public static void SayRandomPetSpeech()
        {
            Sprite2d SP = Util.Global.Pets.Where(x => x.Actor.aiState != Actor.AiState.Stabled).OrderBy(x => Guid.NewGuid()).Take(1).FirstOrDefault();
            if (SP != null)
            {
                string speech = GetSpeech(Actor.ActorType.Pet, SpeechType.Random, SP.Actor.enemyType);
                if (speech != null)
                    speak(SP.ID, speech);
            }
        }

        public static void SayRandomHeroSpeech()
        {
            string speech = GetSpeech(Actor.ActorType.Hero, SpeechType.Random, null);
            if (speech != null)
                speak(Util.Global.Hero.ID, speech);
        }

        public static string GetSpeech(Actor.ActorType AT, SpeechType ST, Actions.Enemy.EnemyType? ET)
        {
            List<Tuple<Actor.ActorType, Actions.Enemy.EnemyType?, SpeechType, string>> Speeches = GetSpeeches();
            Tuple<Actor.ActorType, Actions.Enemy.EnemyType?, SpeechType, string> returnstring;

            if (ET!= null)
                returnstring = Speeches.Where(x => x.Item1 == AT && x.Item2 == ET && x.Item3 == ST).OrderBy(x => Guid.NewGuid()).Take(1).FirstOrDefault();
            else
                returnstring = Speeches.Where(x => x.Item1 == AT && x.Item2 == null && x.Item3 == ST).OrderBy(x => Guid.NewGuid()).Take(1).FirstOrDefault();

            if (returnstring == null)
                return null;
            else
                return returnstring.Item4;
        }

        private static Tuple<Actor.ActorType, Actions.Enemy.EnemyType?, SpeechType, string> GetSpeechEntry(Actor.ActorType AT, Actions.Enemy.EnemyType? ET, SpeechType ST, string speech)
        {
            return new Tuple<Actor.ActorType, Enemy.EnemyType?, SpeechType, string>(AT, ET, ST, speech);
        }

        private static List<Tuple<Actor.ActorType, Actions.Enemy.EnemyType?, SpeechType, string>> GetSpeeches()
        {
            List<Tuple<Actor.ActorType, Actions.Enemy.EnemyType?, SpeechType, string>> Speech = new List<Tuple<Actor.ActorType, Actions.Enemy.EnemyType?, SpeechType, string>>();
            
            Speech.Add(GetSpeechEntry(Actor.ActorType.Hero, null, SpeechType.Random, "A Tent could help me manage my pets"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Hero, null, SpeechType.Random, "I think the hammer can break cave walls"));

            Speech.Add(GetSpeechEntry(Actor.ActorType.Enemy, Enemy.EnemyType.LightBug, SpeechType.Random, "I'll show you the light!"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Enemy, Enemy.EnemyType.LightBug, SpeechType.Random, "I'll blind you with science!"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Enemy, Enemy.EnemyType.LightBug, SpeechType.Random, "This should be over in a flash"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.LightBug, SpeechType.Random, "blink twinkle twinkle blink"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.LightBug, SpeechType.Random, "I'll keep shining"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.LightBug, SpeechType.Random, "just call me Firefly"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.LightBug, SpeechType.MakePet, "I only shine for you now"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.LightBug, SpeechType.MakePet, "I'll be your guiding light"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.LightBug, SpeechType.MakePet, "I shall follow you\nand light your path"));

            Speech.Add(GetSpeechEntry(Actor.ActorType.Enemy, Enemy.EnemyType.Ant, SpeechType.Random, "Ant gonna be your day"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Enemy, Enemy.EnemyType.Ant, SpeechType.Random, "I eat Raid for breakfast"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Enemy, Enemy.EnemyType.Ant, SpeechType.Random, "you're the piss-ant"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Ant, SpeechType.Random, "Keep your eyes peeled for any sugar cubes"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Ant, SpeechType.Random, "Sometimes I have nightmares\nabout magnifying glasses"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Ant, SpeechType.Random, "Need me to carry anything?"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Ant, SpeechType.Random, "Let's get moving. I've got ants in my pants"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Ant, SpeechType.MakePet, "Let me take a load off your back"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Ant, SpeechType.MakePet, "You are my queen now"));

            Speech.Add(GetSpeechEntry(Actor.ActorType.Enemy, Enemy.EnemyType.Worm, SpeechType.Random, "You're gonna be worm food!"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Enemy, Enemy.EnemyType.Worm, SpeechType.Random, "I'm going to play pinochle on your snout!"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Enemy, Enemy.EnemyType.Worm, SpeechType.Random, "I killed the early bird"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Enemy, Enemy.EnemyType.Worm, SpeechType.Random, "You won't worm your way out of this one"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Worm, SpeechType.Random, "Just don't use me as bait"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Worm, SpeechType.Random, "My uncle went out into the rain and never returned"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Worm, SpeechType.MakePet, "All my hearts belong to you"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Worm, SpeechType.MakePet, "Always on the hook for you"));

            Speech.Add(GetSpeechEntry(Actor.ActorType.Enemy, Enemy.EnemyType.Bee, SpeechType.Random, "This might sting!"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Enemy, Enemy.EnemyType.Bee, SpeechType.Random, "Just buzz off!"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Bee, SpeechType.Random, "Smoke makes me sleepy"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Bee, SpeechType.Random, "Bee's knees"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Bee, SpeechType.Random, "Can we stop and smell the flowers?"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Bee, SpeechType.MakePet, "Can I call you honey?"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Bee, SpeechType.MakePet, "Hive-five friend!"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Bee, SpeechType.MakePet, "I'll be the beein your bonnet"));

            Speech.Add(GetSpeechEntry(Actor.ActorType.Enemy, Enemy.EnemyType.Beetle, SpeechType.Random, "Heheh you tickle"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Enemy, Enemy.EnemyType.Beetle, SpeechType.Random, "Bashing time"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Beetle, SpeechType.Random, "Wait up"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Beetle, SpeechType.Random, "Let's make camp"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Beetle, SpeechType.MakePet, "You lead, I follow."));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Beetle, SpeechType.MakePet, "Since we are going the same way"));

            Speech.Add(GetSpeechEntry(Actor.ActorType.Enemy, Enemy.EnemyType.Fly, SpeechType.Random, "Shoo! Don't bother me."));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Enemy, Enemy.EnemyType.Fly, SpeechType.Random, "I'm the Fly, you're the ointment"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Fly, SpeechType.Random, "Know a bar we can hang out in?"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Fly, SpeechType.Random, "I'll be on the wall."));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Fly, SpeechType.MakePet, "I'm going to stick to you like fly paper"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Fly, SpeechType.MakePet, "You're more like honey than vinegar"));

            Speech.Add(GetSpeechEntry(Actor.ActorType.Enemy, Enemy.EnemyType.Moth, SpeechType.Random, "You look bright and tasty!"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Enemy, Enemy.EnemyType.Moth, SpeechType.Random, "Time for the moth death dance"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Moth, SpeechType.Random, "My cousin Mothra says hi"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Moth, SpeechType.Random, "Aw moth balls!"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Moth, SpeechType.MakePet, "You're my new flame!"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Moth, SpeechType.MakePet, "Let's just stay away from the cedar"));

            Speech.Add(GetSpeechEntry(Actor.ActorType.Enemy, Enemy.EnemyType.Frog, SpeechType.Random, "Yum, hero legs"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Enemy, Enemy.EnemyType.Frog, SpeechType.Random, "Supa frog toung lash!"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Frog, SpeechType.Random, "Is it starting to get warm in here?"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Frog, SpeechType.Random, "Ribbit"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Frog, SpeechType.MakePet, "Hoppin' your way"));
            Speech.Add(GetSpeechEntry(Actor.ActorType.Pet, Enemy.EnemyType.Frog, SpeechType.MakePet, "With good friends you can't lose."));

            return Speech;
        }
    }
}
