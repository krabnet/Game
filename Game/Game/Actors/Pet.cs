using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game.Actors
{
    public class Pet: Base
    {
        public Objects.Spite2d Sprite { get; set; }
        public AiState aiState { get; set; }
        public float Orientation { get; set; }
        public Vector2 WanderDirection { get; set; }
        public const float ChaseDistance = 250.0f;
        public const float CaughtDistance = 60.0f;

        public Pet GetPet(ContentManager Content, int level, int health, int x, int y)
        {
            Objects.Spite2d S = new Objects.Spite2d(Content.Load<Texture2D>("Images/triangle"), Guid.NewGuid().ToString(), true, x, y, 50, 50, 10, Objects.Base.ControlType.None);
            S.orderNum = 899;
            S.actionType = Objects.Base.ActionType.Update;

            Pet returnPet = new Pet();
            returnPet.Sprite = S;
            returnPet.Level = level;
            returnPet.Health = health;

            List<Object> Objs2 = new List<object>();
            Objs2.Add(returnPet);
            returnPet.Sprite.actionCall = new Actions.ActionCall(typeof(Actors.Pet), "Update", Objs2);
            return returnPet;
        }

        public void Update(Pet pet)
        {
            float ChaseThreshold = ChaseDistance;
            float CaughtThreshold = CaughtDistance;
            Vector2 Position = new Vector2((float)pet.Sprite.x, (float)pet.Sprite.y);
            if (pet.aiState == AiState.Wander)
            {
                //Wander(Position, pet.WanderDirection, pet.Orientation, 0.10f);
                ChaseThreshold -= Hysteresis / 2;
            }
            else if (pet.aiState == AiState.Chasing)
            {
                ChaseThreshold += Hysteresis / 2;
                CaughtThreshold -= Hysteresis / 2;
            }
            else if (pet.aiState == AiState.Caught)
            {
                CaughtThreshold += Hysteresis / 2;
            }
            
            
            float Hx = Util.Global.SpritesAnim.Where(x => x.name == "Hero").FirstOrDefault().x;
            float Hy = Util.Global.SpritesAnim.Where(x => x.name == "Hero").FirstOrDefault().y;
          
            float distanceFromHero = Vector2.Distance(Position, new Vector2(Hx, Hy));
            if (distanceFromHero > ChaseThreshold)
            {
                //pet.aiState = AiState.Wander;
                pet.aiState = AiState.Chasing;
            }
            else if (distanceFromHero > CaughtThreshold)
            {
                pet.aiState = AiState.Chasing;
            }
            else
            {
                pet.aiState = AiState.Caught;
            }
            float currentSpeed;
            if (pet.aiState == AiState.Chasing)
            {
                currentSpeed = 10;
                pet.Orientation = TurnToFace(Position, new Vector2(Hx, Hy), pet.Orientation, 0.10f);
            }
            else if (pet.aiState == AiState.Wander)
            {
                currentSpeed = .25f * 10;
            }
            else
            {
                currentSpeed = 0.0f;
            }

            Vector2 heading = new Vector2(
                (float)Math.Cos(pet.Orientation), (float)Math.Sin(pet.Orientation));
            Position += heading * currentSpeed;

            pet.Sprite.x = (int)Position.X;
            pet.Sprite.y = (int)Position.Y;

        }
    }
}

