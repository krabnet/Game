using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game.Objects
{
    [Serializable]
    public class Maneuver
    {
        public Guid ID { get; set; }
        public bool active { get; set; }
        public Vector2 OriginalVector { get; set; }
        public Vector2 OriginalSize { get; set; }
        public List<Vector2> Movement { get; set; }
        public List<Vector2> MovementSize { get; set; }
        public int MoveNum { get; set; }
        public Util.ColorType ColorType { get; set; }
        public bool Repeat { get; set; }
        public bool ReturnToOriginalPosition { get; set; }
        public Game.Actions.ActionCall FinalActionCall { get; set; }
        public bool Draw { get; set; }


        public Maneuver()
        { }
        public Maneuver(Vector2 _OriginalVector, Vector2 _OriginalSize, List<Vector2> _Movement, Util.ColorType _ColorType, bool _Repeat)
        {
            ID = Guid.NewGuid();
            active = true;
            OriginalVector = _OriginalVector;
            OriginalSize = _OriginalSize;
            Movement = _Movement;
            MoveNum = 0;
            ColorType = _ColorType;
            Repeat = _Repeat;
            ReturnToOriginalPosition = true;
            FinalActionCall = null;
            MovementSize = Util.Sizes.GetSizeList(Util.SizeType.None, _OriginalSize, _Movement.Count, 0,1,1);
        }

        public void ReplaceCycloneMov(Objects.Sprite2d S, Vector2 Position)
        {
            List<Vector2> MOV = Actions.Anim.GetMovementCyclone(Position, 1200);
            Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Maneuver.Movement = MOV;
            Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Maneuver.MovementSize = Util.Sizes.GetSizeList(Util.SizeType.RandomEven, S.Size, MOV.Count, 3,1,8);
        }

        public void ReplaceRainDropMov(Objects.Sprite2d S)
        {
            try
            {
                if (S != null)
                {
                    int Hx = (int)Util.Global.Hero.Position.X;
                    int Hy = (int)Util.Global.Hero.Position.Y;
                    Vector2 start = new Vector2(Util.Global.GetRandomInt(Hx - 600, Hx + 800), Util.Global.GetRandomInt(Hy - 500, Hy - 400));
                    Vector2 end = new Vector2(Util.Global.GetRandomInt((int)start.X - 200, (int)start.X - 100), Util.Global.GetRandomInt((int)start.Y + 1000, (int)start.Y + 1200));
                    List<Vector2> MOV = Actions.Anim.GetMovement(start, end, Util.Global.GetRandomInt(25, 75));
                    Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Maneuver.Movement = MOV;
                    Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Maneuver.MovementSize = Util.Sizes.GetSizeList(Util.SizeType.None, S.Size, MOV.Count, 0, 1, 1);
                }
            }
            catch (Exception ex)
            {
                Util.Base.Log("Rain fail:" + ex.Message);
            }
        }

        public void ReplaceSnowMov(Objects.Sprite2d S)
        {
            int Hx = (int)Util.Global.Hero.Position.X;
            int Hy = (int)Util.Global.Hero.Position.Y;
            Vector2 start = new Vector2(Util.Global.GetRandomInt(Hx - 400, Hx + 500), Util.Global.GetRandomInt(Hy - 300, Hy-100));
            List<Vector2> MOV = Actions.Anim.GetMovementSnow(start, Util.Global.GetRandomInt(10, 100));
            Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Maneuver.Movement = MOV;
            Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Maneuver.MovementSize = Util.Sizes.GetSizeList(Util.SizeType.None, S.Size, MOV.Count, 2, 3, 7);
        }

        public void RemoveAll()
        {
            Util.Global.Sprites.RemoveAll(x => x.Maneuver != null);
        }

    }

}
