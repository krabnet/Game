//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Xna.Framework;

//namespace Game.Actors
//{
//    public class Base
//    {
//        public enum AiState
//        {
//            Chasing,
//            Caught,
//            Wander,
//            Figting
//        }
//        public const float Hysteresis = 15.0f;
//        public Objects.Sprite2d Sprite { get; set; }
//        public AiState aiState { get; set; }
//        public float Orientation { get; set; }
//        public Vector2 WanderDirection { get; set; }
//        public const float ChaseDistance = 250.0f;
//        public const float CaughtDistance = 30.0f;
//        public Vector2 OriginalPosition { get; set; }

//        public int Level { get; set; }
//        public int Health { get; set; }
//        public int Str { get; set; }
//        public int Dex { get; set; }
//        public int End { get; set; }
//        public int Agl { get; set; }

//        public void CalculateStats()
//        {
//            this.Str = Util.Global.GetRandomInt(this.Level, this.Level * 2);
//            this.Dex = Util.Global.GetRandomInt(this.Level, this.Level * 2);
//            this.End = Util.Global.GetRandomInt(this.Level, this.Level * 2);
//            this.Agl = Util.Global.GetRandomInt(this.Level, this.Level * 2);
            
//        }

//        public void UpdatePet(Pet pet)
//        {
//            if (pet.aiState != AiState.Figting)
//            {
//                float ChaseThreshold = ChaseDistance;
//                float CaughtThreshold = 60f;
//                Vector2 Position = new Vector2(pet.Sprite.Position.X, pet.Sprite.Position.Y);
//                if (pet.aiState == AiState.Wander)
//                {
//                    //Wander(Position, pet.WanderDirection, pet.Orientation, 0.10f);
//                    ChaseThreshold -= Hysteresis / 2;
//                }
//                else if (pet.aiState == AiState.Chasing)
//                {
//                    ChaseThreshold += Hysteresis / 2;
//                    CaughtThreshold -= Hysteresis / 2;
//                }
//                else if (pet.aiState == AiState.Caught)
//                {
//                    CaughtThreshold += Hysteresis / 2;
//                }


//                float Hx = Util.Global.Sprites.Where(x => x.name == "Hero").FirstOrDefault().Position.X;
//                float Hy = Util.Global.Sprites.Where(x => x.name == "Hero").FirstOrDefault().Position.Y;

//                float distanceFromHero = Vector2.Distance(Position, new Vector2(Hx, Hy));
//                if (distanceFromHero > ChaseThreshold)
//                {
//                    pet.aiState = AiState.Wander;
//                    pet.aiState = AiState.Chasing;
//                }
//                else if (distanceFromHero > CaughtThreshold)
//                {
//                    pet.aiState = AiState.Chasing;
//                }
//                else
//                {
//                    pet.aiState = AiState.Caught;
//                }
//                float currentSpeed;
//                if (pet.aiState == AiState.Chasing)
//                {
//                    currentSpeed = 10;
//                    pet.Orientation = TurnToFace(Position, new Vector2(Hx, Hy), pet.Orientation, 0.10f);
//                }
//                else if (pet.aiState == AiState.Wander)
//                {
//                    currentSpeed = .25f * 10;
//                }
//                else
//                {
//                    currentSpeed = 0.0f;
//                }

//                Vector2 heading = new Vector2(
//                    (float)Math.Cos(pet.Orientation), (float)Math.Sin(pet.Orientation));
//                Position += heading * currentSpeed;

//                pet.Sprite.Position = new Vector2(Position.X, Position.Y);
//                pet.Sprite.Orientation = pet.Orientation;
//            }
//        }

//        public void UpdateEnemy(Enemy enemy)
//        {
//            if (enemy.aiState != AiState.Figting)
//            {
//                    float ChaseThreshold = ChaseDistance - 100;
//                    float CaughtThreshold = CaughtDistance;
//                    Vector2 Position = enemy.Sprite.Position;
//                    if (enemy.aiState == AiState.Wander)
//                    {
//                        Wander(enemy, Position, enemy.WanderDirection, enemy.Orientation, 0.10f);
//                        ChaseThreshold -= Hysteresis / 2;
//                    }
//                    else if (enemy.aiState == AiState.Chasing)
//                    {
//                        ChaseThreshold += Hysteresis / 2;
//                        CaughtThreshold -= Hysteresis / 2;
//                    }
//                    else if (enemy.aiState == AiState.Caught)
//                    {
//                        CaughtThreshold += Hysteresis / 2;
//                    }


//                    float Hx = Util.Global.Sprites.Where(x => x.name == "Hero").FirstOrDefault().Position.X;
//                    float Hy = Util.Global.Sprites.Where(x => x.name == "Hero").FirstOrDefault().Position.Y;

//                    float distanceFromHero = Vector2.Distance(Position, new Vector2(Hx, Hy));
//                    if (distanceFromHero > ChaseThreshold)
//                    {
//                        enemy.aiState = AiState.Wander;
//                    }
//                    else if (distanceFromHero < ChaseThreshold)
//                    {
//                        enemy.aiState = AiState.Chasing;
//                    }
                    
//                    if (distanceFromHero <= CaughtThreshold)
//                    {
//                        enemy.aiState = AiState.Caught;
//                    }
//                    float currentSpeed;
//                    if (enemy.aiState == AiState.Chasing)
//                    {
//                        currentSpeed = 10;
//                        enemy.Orientation = TurnToFace(Position, new Vector2(Hx, Hy), enemy.Orientation, 0.50f);
//                    }
//                    else if (enemy.aiState == AiState.Wander)
//                    {
//                        currentSpeed = .25f * 10;
//                    }
//                    else
//                    {
//                        currentSpeed = 0.0f;
//                    }

//                    Vector2 heading = new Vector2((float)Math.Cos(enemy.Orientation), (float)Math.Sin(enemy.Orientation));
//                    Position += heading * currentSpeed;

//                    enemy.Sprite.Position = Position;

//                    if (enemy.Sprite.ClipCheck())
//                    {
//                        enemy.Sprite.Position = enemy.OriginalPosition; 
//                        enemy.Orientation = enemy.Orientation * -1.5f;
//                    }
//                   else
//                   {
//                       enemy.OriginalPosition = enemy.Sprite.Position;
//                   }

//                List<Object> Objs2 = new List<object>();
//                Objs2.Add(enemy);
//                enemy.Sprite.actionCall.Where(A => A.actionMethodName == "UpdateEnemy").FirstOrDefault().parameters = Objs2;
//            }

//        }

//        public static float TurnToFace(Vector2 position, Vector2 faceThis,float currentAngle, float turnSpeed)
//        {
//            // consider this diagram:
//            //         B 
//            //        /|
//            //      /  |
//            //    /    | y
//            //  / o    |
//            // A--------
//            //     x
//            // 
//            // where A is the position of the object, B is the position of the target,
//            // and "o" is the angle that the object should be facing in order to 
//            // point at the target. we need to know what o is. using trig, we know that
//            //      tan(theta)       = opposite / adjacent
//            //      tan(o)           = y / x
//            // if we take the arctan of both sides of this equation...
//            //      arctan( tan(o) ) = arctan( y / x )
//            //      o                = arctan( y / x )
//            // so, we can use x and y to find o, our "desiredAngle."
//            // x and y are just the differences in position between the two objects.
//            float x = faceThis.X - position.X;
//            float y = faceThis.Y - position.Y;

//            // we'll use the Atan2 function. Atan will calculates the arc tangent of 
//            // y / x for us, and has the added benefit that it will use the signs of x
//            // and y to determine what cartesian quadrant to put the result in.
//            // http://msdn2.microsoft.com/en-us/library/system.math.atan2.aspx
//            float desiredAngle = (float)Math.Atan2(y, x);

//            // so now we know where we WANT to be facing, and where we ARE facing...
//            // if we weren't constrained by turnSpeed, this would be easy: we'd just 
//            // return desiredAngle.
//            // instead, we have to calculate how much we WANT to turn, and then make
//            // sure that's not more than turnSpeed.

//            // first, figure out how much we want to turn, using WrapAngle to get our
//            // result from -Pi to Pi ( -180 degrees to 180 degrees )
//            float difference = WrapAngle(desiredAngle - currentAngle);

//            // clamp that between -turnSpeed and turnSpeed.
//            difference = MathHelper.Clamp(difference, -turnSpeed, turnSpeed);

//            // so, the closest we can get to our target is currentAngle + difference.
//            // return that, using WrapAngle again.
//            return WrapAngle(currentAngle + difference);
//        }

//        public static float WrapAngle(float radians)
//        {
//            while (radians < -MathHelper.Pi)
//            {
//                radians += MathHelper.TwoPi;
//            }
//            while (radians > MathHelper.Pi)
//            {
//                radians -= MathHelper.TwoPi;
//            }
//            return radians;
//        }

//        public void Wander(Enemy enemy, Vector2 position, Vector2 wanderDirection, float orientation, float turnSpeed)
//        {
//            // The wander effect is accomplished by having the character aim in a random
//            // direction. Every frame, this random direction is slightly modified.
//            // Finally, to keep the characters on the center of the screen, we have them
//            // turn to face the screen center. The further they are from the screen
//            // center, the more they will aim back towards it.

//            // the first step of the wander behavior is to use the random number
//            // generator to offset the current wanderDirection by some random amount.
//            // .25 is a bit of a magic number, but it controls how erratic the wander
//            // behavior is. Larger numbers will make the characters "wobble" more,
//            // smaller numbers will make them more stable. we want just enough
//            // wobbliness to be interesting without looking odd.
//            wanderDirection.X += MathHelper.Lerp(-.35f, .35f, (float)Util.Global.GetRandomDouble());
//            wanderDirection.Y += MathHelper.Lerp(-.35f, .35f, (float)Util.Global.GetRandomDouble());
//            //wanderDirection.X += MathHelper.Lerp((float)Util.Global.GetRandomDouble()*-1, (float)Util.Global.GetRandomDouble(), (float)Util.Global.GetRandomDouble());
//            //wanderDirection.Y += MathHelper.Lerp((float)Util.Global.GetRandomDouble()*-1, (float)Util.Global.GetRandomDouble(), (float)Util.Global.GetRandomDouble());

//            // we'll renormalize the wander direction, ...
//            if (wanderDirection != Vector2.Zero)
//            {
//                wanderDirection.Normalize();
//            }
//            // ... and then turn to face in the wander direction. We don't turn at the
//            // maximum turning speed, but at 15% of it. Again, this is a bit of a magic
//            // number: it works well for this sample, but feel free to tweak it.
//            orientation = TurnToFace(position, position + wanderDirection, orientation,
//                .15f * turnSpeed);


//            // next, we'll turn the characters back towards the center of the screen, to
//            // prevent them from getting stuck on the edges of the screen.
//            Vector2 screenCenter = Vector2.Zero;
//            //screenCenter.X = Util.Global.Cam.Pos.X;
//            //screenCenter.Y = Util.Global.Cam.Pos.Y;
//            screenCenter.X = Util.Global.Sprites.Max(x => x.Position.X) / 2;
//            screenCenter.Y = Util.Global.Sprites.Max(x => x.Position.Y) / 2; ;

//            // Here we are creating a curve that we can apply to the turnSpeed. This
//            // curve will make it so that if we are close to the center of the screen,
//            // we won't turn very much. However, the further we are from the screen
//            // center, the more we turn. At most, we will turn at 30% of our maximum
//            // turn speed. This too is a "magic number" which works well for the sample.
//            // Feel free to play around with this one as well: smaller values will make
//            // the characters explore further away from the center, but they may get
//            // stuck on the walls. Larger numbers will hold the characters to center of
//            // the screen. If the number is too large, the characters may end up
//            // "orbiting" the center.
//            float distanceFromScreenCenter = Vector2.Distance(screenCenter, position);
//            float MaxDistanceFromScreenCenter = Math.Min(screenCenter.Y, screenCenter.X);

//            float normalizedDistance = distanceFromScreenCenter / MaxDistanceFromScreenCenter;

//            float turnToCenterSpeed = .3f * normalizedDistance * normalizedDistance *  turnSpeed;

//            // once we've calculated how much we want to turn towards the center, we can
//            // use the TurnToFace function to actually do the work.
//            orientation = TurnToFace(position, screenCenter, orientation,turnToCenterSpeed);

//            enemy.Orientation = orientation;
//            enemy.WanderDirection = wanderDirection;
//        }

//        //private void UpdateMouse()
//        //{
//        //    // first, calculate how far away the mouse is from the cat, and use that
//        //    // information to decide how to behave. If they are too close, the mouse
//        //    // will switch to "active" mode - fleeing. if they are far apart, the mouse
//        //    // will switch to "idle" mode, where it roams around the screen.
//        //    // we use a hysteresis constant in the decision making process, as described
//        //    // in the accompanying doc file.
//        //    float distanceFromCat = Vector2.Distance(mousePosition, catPosition);

//        //    // the cat is a safe distance away, so the mouse should idle:
//        //    if (distanceFromCat > MouseEvadeDistance + MouseHysteresis)
//        //    {
//        //        mouseState = MouseAiState.Wander;
//        //    }
//        //    // the cat is too close; the mouse should run:
//        //    else if (distanceFromCat < MouseEvadeDistance - MouseHysteresis)
//        //    {
//        //        mouseState = MouseAiState.Evading;
//        //    }
//        //    // if neither of those if blocks hit, we are in the "hysteresis" range,
//        //    // and the mouse will continue doing whatever it is doing now.

//        //    // the mouse will move at a different speed depending on what state it
//        //    // is in. when idle it won't move at full speed, but when actively evading
//        //    // it will move as fast as it can. this variable is used to track which
//        //    // speed the mouse should be moving.
//        //    float currentMouseSpeed;

//        //    // the second step of the Update is to change the mouse's orientation based
//        //    // on its current state.
//        //    if (mouseState == MouseAiState.Evading)
//        //    {
//        //        // If the mouse is "active," it is trying to evade the cat. The evasion
//        //        // behavior is accomplished by using the TurnToFace function to turn
//        //        // towards a point on a straight line facing away from the cat. In other
//        //        // words, if the cat is point A, and the mouse is point B, the "seek
//        //        // point" is C.
//        //        //     C
//        //        //   B
//        //        // A
//        //        Vector2 seekPosition = 2 * mousePosition - catPosition;

//        //        // Use the TurnToFace function, which we introduced in the AI Series 1:
//        //        // Aiming sample, to turn the mouse towards the seekPosition. Now when
//        //        // the mouse moves forward, it'll be trying to move in a straight line
//        //        // away from the cat.
//        //        mouseOrientation = TurnToFace(mousePosition, seekPosition,
//        //            mouseOrientation, MouseTurnSpeed);

//        //        // set currentMouseSpeed to MaxMouseSpeed - the mouse should run as fast
//        //        // as it can.
//        //        currentMouseSpeed = MaxMouseSpeed;
//        //    }
//        //    else
//        //    {
//        //        // if the mouse isn't trying to evade the cat, it should just meander
//        //        // around the screen. we'll use the Wander function, which the mouse and
//        //        // tank share, to accomplish this. mouseWanderDirection and
//        //        // mouseOrientation are passed by ref so that the wander function can
//        //        // modify them. for more information on ref parameters, see
//        //        // http://msdn2.microsoft.com/en-us/library/14akc2c7(VS.80).aspx
//        //        Wander(mousePosition, ref mouseWanderDirection, ref mouseOrientation,
//        //            MouseTurnSpeed);

//        //        // if the mouse is wandering, it should only move at 25% of its maximum
//        //        // speed. 
//        //        currentMouseSpeed = .25f * MaxMouseSpeed;
//        //    }

//        //    // The final step is to move the mouse forward based on its current
//        //    // orientation. First, we construct a "heading" vector from the orientation
//        //    // angle. To do this, we'll use Cosine and Sine to tell us the x and y
//        //    // components of the heading vector. See the accompanying doc for more
//        //    // information.
//        //    Vector2 heading = new Vector2(
//        //        (float)Math.Cos(mouseOrientation), (float)Math.Sin(mouseOrientation));

//        //    // by multiplying the heading and speed, we can get a velocity vector. the
//        //    // velocity vector is then added to the mouse's current position, moving him
//        //    // forward.
//        //    mousePosition += heading * currentMouseSpeed;
//        //}
//    }

//}
