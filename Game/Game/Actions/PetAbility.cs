using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Game.Actions
{
    public static class PetAbility
    {
        public static void ChopWood(Objects.Sprite2d Actor)
        {
            Actor = Util.Global.Sprites.Where(x => x.ID == Util.Global.Pets.FirstOrDefault().ID).FirstOrDefault();
            
            Objects.Sprite2d Tree = FindClosestTree(Actor);

            if (Tree != null)
            {
                Objects.Maneuver Mov = new Objects.Maneuver();
                Mov.active = true;

                Vector2 start = Actor.Position;
                Vector2 end = Tree.Position;
                float Distance = Vector2.Distance(start, end);
                int Speed = (int)Distance / Actor.speed;
                List<Vector2> MOV = Actions.Anim.GetMovement(start, end, Speed);

                Objects.Maneuver Man = new Objects.Maneuver(Actor.Position, Actor.Size, MOV, Util.ColorType.None, false);
                Man.ReturnToOriginalPosition = false;
                Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Maneuver = Man;


                List<object> Obj1 = new List<object>();
                Obj1.Add(Actor);
                Obj1.Add(Tree);
                Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Maneuver.FinalActionCall = new ActionCall(ActionType.Update, typeof(PetAbility), "ChopTree", Obj1);


                Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Actor.aiState = Objects.Actor.AiState.Figting;
            } 
        }

        public static void ChopTree(Objects.Sprite2d Actor, Objects.Sprite2d Tree)
        {
            Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Actor.aiState = Objects.Actor.AiState.Chasing;
            //Util.Global.Sprites.Where(x => x.ID == Actor.ID).FirstOrDefault().Position = Tree.Position;
            Util.Global.ContentMan.Load<SoundEffect>("Sounds/ChopWood").Play();
            Util.Global.Sprites.Add(Items.Item.GetItemByType(Items.Item.ItemType.Log,Tree.Position));     
            Util.Global.Sprites.RemoveAll(x => x.ID == Tree.ID);
        }

        public static Objects.Sprite2d FindClosestTree(Objects.Sprite2d Actor)
        {
            int Distance = 1000;
            Objects.Sprite2d Tree = new Objects.Sprite2d();
            List<Objects.Sprite2d> SIR = Util.Global.Sprites.Where(r => r.Item.Type == Items.Item.ItemType.Tree &&
                (r.Position.X < Actor.Position.X + Distance
                && r.Position.Y < Actor.Position.Y + Distance
                && r.Position.X > Actor.Position.X - Distance
                && r.Position.Y > Actor.Position.Y - Distance)).OrderBy(y => Vector2.Distance(y.Position, Actor.Position)).ToList();

            return SIR.FirstOrDefault();
        }
    }
}
