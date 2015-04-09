using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Actors
{
    public class Enemy : Base
    {
        public Objects.Spite2d Sprite { get; set; }

        public Enemy GetEnemy(ContentManager Content, int level, int health, int x, int y)
        {
            Objects.Spite2d S = new Objects.Spite2d(Content.Load<Texture2D>("Images/cactus"), Guid.NewGuid().ToString(), true, x, y, 100, 100, 10, Objects.Base.ControlType.None);
            S.orderNum = 899;

            Enemy returnEnemy = new Enemy();
            returnEnemy.Sprite = S;
            returnEnemy.Level = level;
            returnEnemy.Health = health;
            
            List<Object> Objs2 = new List<object>();
            Objs2.Add(returnEnemy);
            returnEnemy.Sprite.actionCall = new Actions.ActionCall(typeof(Actions.Fight), "DisplayFight", Objs2);
            return returnEnemy;
        }
    }
}
