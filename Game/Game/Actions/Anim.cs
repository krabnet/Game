using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game.Actions
{
    public class Anim
    {
        public Guid ID { get; set; }
        public Guid ObjectGuid { get; set; }
        public bool active { get; set; }
        public Vector2 OriginalVector { get; set; }
        public List<Vector2> Movement { get; set; }
        public int MoveNum { get; set; }


        public Anim(Guid _ObjectGuid, Vector2 _OriginalVector, List<Vector2> _Movement)
        {
            ID = Guid.NewGuid();
            ObjectGuid = _ObjectGuid;
            active = true;
            OriginalVector = _OriginalVector;
            Movement = _Movement;
            MoveNum = 0;
        }
        public Anim()
        { }

        public void update()
        {
            List<Objects.AnimSprite> AS = Util.Global.SpritesAnim.Where(x => x.ID == this.ObjectGuid).ToList();
            List<Objects.Spite2d> A = Util.Global.Sprites.Where(x => x.ID == this.ObjectGuid).ToList();
            
            foreach (Objects.AnimSprite ASi in AS)
            {
                ASi.x = (int)Movement[MoveNum].X; ;
                ASi.y = (int)Movement[MoveNum].Y; ;
            }
            foreach (Objects.Spite2d Ai in A)
            {
                Ai.x = (int)Movement[MoveNum].X; ;
                Ai.y = (int)Movement[MoveNum].Y; ;
            }

            this.MoveNum++;
            if (this.MoveNum >= Movement.Count)
            {
                foreach(Objects.AnimSprite ASi in AS)
                {
                    ASi.x = (int)this.OriginalVector.X;
                    ASi.y = (int)this.OriginalVector.Y;
                }
                foreach (Objects.Spite2d Ai in A)
                {
                    Ai.x = (int)this.OriginalVector.X;
                    Ai.y = (int)this.OriginalVector.Y;
                }
                this.active = false;
            }
        }

        public List<Vector2> GetMovementJump(Vector2 start, Vector2 end)
        {
            int Speed = 10;
            float sX = start.X;
            float sY = start.Y;
            float eX = end.X;
            float eY = end.Y;
            float midPoint = (eX/Speed - sX/Speed) / 2;

            List<Vector2> returnVector = new List<Vector2>();
            for (int i = 1; i <= midPoint * 2; i++)
            {
                if (i < midPoint)
                {
                    sX = sX + Speed;
                    sY = sY - Speed;
                }
                else
                {
                     sX = sX + Speed;
                    sY = sY + Speed;
                }
                returnVector.Add(new Vector2(sX, sY));
            }
            return returnVector;
        }
    }
}
