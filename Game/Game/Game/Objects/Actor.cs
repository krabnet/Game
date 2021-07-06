using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game.Objects
{
    public class Actor
    {
        public enum AiState {Chasing,Caught,Wander,Figting}
        public enum ActorType { Hero, Pet, Enemy }

        private const float Hysteresis = 15.0f;
        private const float ChaseDistance = 250.0f;
        private const float CaughtDistance = 30.0f;

        public AiState aiState { get; set; }
        public float Orientation { get; set; }
        public Vector2 WanderDirection { get; set; }
        public Vector2 OriginalPosition { get; set; }
        public ActorType actorType { get; set; }

        public List<Items.Item.ItemType> Drops { get; set; }
        public List<Actors.Enemy.EnemyType> Parents { get; set; }

        public int Level { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Experience { get; set; }
        public int Str { get; set; }
        public int Dex { get; set; }
        public int End { get; set; }
        public int Agl { get; set; }

        public void CalculateStats()
        {
            this.Str = Util.Global.GetRandomInt(this.Level, this.Level * 2);
            this.Dex = Util.Global.GetRandomInt(this.Level, this.Level * 2);
            this.End = Util.Global.GetRandomInt(this.Level, this.Level * 2);
            this.Agl = Util.Global.GetRandomInt(this.Level, this.Level * 2);
            MaxHealth = Health;
        }

        public void Update(Sprite2d sprite)
        {
            if (actorType == Actor.ActorType.Pet)
            {
                UpdatePet(sprite);
            }
            if (actorType == Actor.ActorType.Enemy)
            {
                UpdateEnemy(sprite);
            }
        }
        
        public void UpdatePet(Sprite2d pet)
        {
            if (pet.Actor.aiState != AiState.Figting)
            {
                float ChaseThreshold = ChaseDistance;
                float CaughtThreshold = 60f;
                Vector2 Position = pet.Position;
                if (pet.Actor.aiState == AiState.Wander)
                {
                    //Wander(Position, pet.WanderDirection, pet.Orientation, 0.10f);
                    ChaseThreshold -= Hysteresis / 2;
                }
                else if (pet.Actor.aiState == AiState.Chasing)
                {
                    ChaseThreshold += Hysteresis / 2;
                    CaughtThreshold -= Hysteresis / 2;
                }
                else if (pet.Actor.aiState == AiState.Caught)
                {
                    CaughtThreshold += Hysteresis / 2;
                }


                float Hx = Util.Global.Sprites.Where(x => x.name == "Hero").FirstOrDefault().Position.X;
                float Hy = Util.Global.Sprites.Where(x => x.name == "Hero").FirstOrDefault().Position.Y;

                float distanceFromHero = Vector2.Distance(Position, new Vector2(Hx, Hy));
                if (distanceFromHero > ChaseThreshold)
                {
                    pet.Actor.aiState = AiState.Wander;
                    pet.Actor.aiState = AiState.Chasing;
                }
                else if (distanceFromHero > CaughtThreshold)
                {
                    pet.Actor.aiState = AiState.Chasing;
                }
                else
                {
                    pet.Actor.aiState = AiState.Caught;
                }
                float currentSpeed;
                if (pet.Actor.aiState == AiState.Chasing)
                {
                    currentSpeed = pet.speed;
                    pet.Orientation = TurnToFace(Position, new Vector2(Hx, Hy), pet.Orientation, 0.10f);
                }
                else if (pet.Actor.aiState == AiState.Wander)
                {
                    currentSpeed = .25f * 10;
                }
                else
                {
                    currentSpeed = 0.0f;
                }

                Vector2 heading = new Vector2((float)Math.Cos(pet.Orientation), (float)Math.Sin(pet.Orientation));
                Position += heading * currentSpeed;

                pet.Position = Vector2.Subtract(Position, new Vector2((float)Util.Global.GetRandomInt(-1, 1), (float)Util.Global.GetRandomInt(-1, 1)));
                pet.Orientation = pet.Orientation;
            }
        }

        public void UpdateEnemy(Sprite2d enemy)
        {
            if (enemy.Actor.aiState != AiState.Figting)
            {
                float ChaseThreshold = MathHelper.Clamp(enemy.Actor.Level * 75,75,500);
                float CaughtThreshold = CaughtDistance;
                Vector2 Position = enemy.Position;
                if (enemy.Actor.aiState == AiState.Wander)
                {
                    Wander(enemy, Position, enemy.Actor.WanderDirection, enemy.Orientation, 0.10f);
                    ChaseThreshold -= Hysteresis / 2;
                }
                else if (enemy.Actor.aiState == AiState.Chasing)
                {
                    ChaseThreshold += Hysteresis / 2;
                    CaughtThreshold -= Hysteresis / 2;
                }
                else if (enemy.Actor.aiState == AiState.Caught)
                {
                    CaughtThreshold += Hysteresis / 2;
                }


                float Hx = Util.Global.Sprites.Where(x => x.name == "Hero").FirstOrDefault().Position.X;
                float Hy = Util.Global.Sprites.Where(x => x.name == "Hero").FirstOrDefault().Position.Y;

                float distanceFromHero = Vector2.Distance(Position, new Vector2(Hx, Hy));
                if (distanceFromHero > ChaseThreshold)
                {
                    enemy.Actor.aiState = AiState.Wander;
                }
                else if (distanceFromHero < ChaseThreshold)
                {
                    enemy.Actor.aiState = AiState.Chasing;
                }

                if (distanceFromHero <= CaughtThreshold)
                {
                    enemy.Actor.aiState = AiState.Caught;
                }
                float currentSpeed;
                if (enemy.Actor.aiState == AiState.Chasing)
                {
                    currentSpeed = enemy.speed;
                    enemy.Orientation = TurnToFace(Position, new Vector2(Hx, Hy), enemy.Orientation, 0.50f);
                }
                else if (enemy.Actor.aiState == AiState.Wander)
                {
                    currentSpeed = .25f * 10;
                }
                else
                {
                    currentSpeed = 0.0f;
                }

                Vector2 heading = new Vector2((float)Math.Cos(enemy.Orientation), (float)Math.Sin(enemy.Orientation));
                Position += heading * currentSpeed;

                enemy.Position = Position;

                if (enemy.ClipCheck())
                {
                    enemy.Position = enemy.Actor.OriginalPosition;
                    enemy.Orientation = enemy.Orientation * -1.5f;
                }
                else
                {
                    enemy.Actor.OriginalPosition = enemy.Position;
                }

                //List<Object> Objs2 = new List<object>();
                //Objs2.Add(enemy);
                //enemy.actionCall.Where(A => A.actionMethodName == "UpdateEnemy").FirstOrDefault().parameters = Objs2;
            }

        }

        public static float TurnToFace(Vector2 position, Vector2 faceThis, float currentAngle, float turnSpeed)
        {
            // consider this diagram:
            //         B 
            //        /|
            //      /  |
            //    /    | y
            //  / o    |
            // A--------
            //     x
            // 
            // where A is the position of the object, B is the position of the target,
            // and "o" is the angle that the object should be facing in order to 
            // point at the target. we need to know what o is. using trig, we know that
            //      tan(theta)       = opposite / adjacent
            //      tan(o)           = y / x
            // if we take the arctan of both sides of this equation...
            //      arctan( tan(o) ) = arctan( y / x )
            //      o                = arctan( y / x )
            // so, we can use x and y to find o, our "desiredAngle."
            // x and y are just the differences in position between the two objects.
            float x = faceThis.X - position.X;
            float y = faceThis.Y - position.Y;

            // we'll use the Atan2 function. Atan will calculates the arc tangent of 
            // y / x for us, and has the added benefit that it will use the signs of x
            // and y to determine what cartesian quadrant to put the result in.
            // http://msdn2.microsoft.com/en-us/library/system.math.atan2.aspx
            float desiredAngle = (float)Math.Atan2(y, x);

            // so now we know where we WANT to be facing, and where we ARE facing...
            // if we weren't constrained by turnSpeed, this would be easy: we'd just 
            // return desiredAngle.
            // instead, we have to calculate how much we WANT to turn, and then make
            // sure that's not more than turnSpeed.

            // first, figure out how much we want to turn, using WrapAngle to get our
            // result from -Pi to Pi ( -180 degrees to 180 degrees )
            float difference = WrapAngle(desiredAngle - currentAngle);

            // clamp that between -turnSpeed and turnSpeed.
            difference = MathHelper.Clamp(difference, -turnSpeed, turnSpeed);

            // so, the closest we can get to our target is currentAngle + difference.
            // return that, using WrapAngle again.
            return WrapAngle(currentAngle + difference);
        }

        public static float WrapAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }

        public void Wander(Sprite2d enemy, Vector2 position, Vector2 wanderDirection, float orientation, float turnSpeed)
        {
            // The wander effect is accomplished by having the character aim in a random
            // direction. Every frame, this random direction is slightly modified.
            // Finally, to keep the characters on the center of the screen, we have them
            // turn to face the screen center. The further they are from the screen
            // center, the more they will aim back towards it.

            // the first step of the wander behavior is to use the random number
            // generator to offset the current wanderDirection by some random amount.
            // .25 is a bit of a magic number, but it controls how erratic the wander
            // behavior is. Larger numbers will make the characters "wobble" more,
            // smaller numbers will make them more stable. we want just enough
            // wobbliness to be interesting without looking odd.
            wanderDirection.X += MathHelper.Lerp(-.35f, .35f, (float)Util.Global.GetRandomDouble());
            wanderDirection.Y += MathHelper.Lerp(-.35f, .35f, (float)Util.Global.GetRandomDouble());
            //wanderDirection.X += MathHelper.Lerp((float)Util.Global.GetRandomDouble()*-1, (float)Util.Global.GetRandomDouble(), (float)Util.Global.GetRandomDouble());
            //wanderDirection.Y += MathHelper.Lerp((float)Util.Global.GetRandomDouble()*-1, (float)Util.Global.GetRandomDouble(), (float)Util.Global.GetRandomDouble());

            // we'll renormalize the wander direction, ...
            if (wanderDirection != Vector2.Zero)
            {
                wanderDirection.Normalize();
            }
            // ... and then turn to face in the wander direction. We don't turn at the
            // maximum turning speed, but at 15% of it. Again, this is a bit of a magic
            // number: it works well for this sample, but feel free to tweak it.
            orientation = TurnToFace(position, position + wanderDirection, orientation,
                .15f * turnSpeed);


            // next, we'll turn the characters back towards the center of the screen, to
            // prevent them from getting stuck on the edges of the screen.
            Vector2 screenCenter = Vector2.Zero;
            //screenCenter.X = Util.Global.Cam.Pos.X;
            //screenCenter.Y = Util.Global.Cam.Pos.Y;
            screenCenter.X = Util.Global.Sprites.Max(x => x.Position.X) / 2;
            screenCenter.Y = Util.Global.Sprites.Max(x => x.Position.Y) / 2; ;

            // Here we are creating a curve that we can apply to the turnSpeed. This
            // curve will make it so that if we are close to the center of the screen,
            // we won't turn very much. However, the further we are from the screen
            // center, the more we turn. At most, we will turn at 30% of our maximum
            // turn speed. This too is a "magic number" which works well for the sample.
            // Feel free to play around with this one as well: smaller values will make
            // the characters explore further away from the center, but they may get
            // stuck on the walls. Larger numbers will hold the characters to center of
            // the screen. If the number is too large, the characters may end up
            // "orbiting" the center.
            float distanceFromScreenCenter = Vector2.Distance(screenCenter, position);
            float MaxDistanceFromScreenCenter = Math.Min(screenCenter.Y, screenCenter.X);

            float normalizedDistance = distanceFromScreenCenter / MaxDistanceFromScreenCenter;

            float turnToCenterSpeed = .3f * normalizedDistance * normalizedDistance * turnSpeed;

            // once we've calculated how much we want to turn towards the center, we can
            // use the TurnToFace function to actually do the work.
            orientation = TurnToFace(position, screenCenter, orientation, turnToCenterSpeed);

            enemy.Orientation = orientation;
            enemy.Actor.WanderDirection = wanderDirection;
        }
    }
}
