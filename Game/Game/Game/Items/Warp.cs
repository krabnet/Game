using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game.Items
{
    public static class Warp
    {
        public static Objects.Sprite2d GetItem(Vector2 _Position)
        {
            Objects.Sprite2d Item = new Objects.Sprite2d(Util.Global.AllTexture2D.Where(x => x.Name == "padon").FirstOrDefault(), "warp", true, new Vector2(1, 1), new Vector2(35, 35), 0, Objects.Base.ControlType.None);
            Item.orderNum = 100;
            Item.Position = _Position;
            Item.effectType = Objects.Base.EffectType.Ripple;
            //List<Object> Objs3 = new List<object>();
            //Objs3.Add(Item);
            //Item.actionCall.Add(new Actions.ActionCall(Actions.ActionType.Collision, typeof(Objects.Sprite2d), "Pickup", Objs3));
            //Item.actionCall.Add(new Actions.ActionCall(Actions.ActionType.Collision, typeof(Items.Health), "HealthPickup", null));

            return Item;
        }
        public static Objects.Sprite2d GetPad(Vector2 _Position)
        {
            Objects.Sprite2d Item = new Objects.Sprite2d(Util.Global.AllTexture2D.Where(x => x.Name == "pad").FirstOrDefault(), "pad", true, new Vector2(1, 1), new Vector2(32, 32), 0, Objects.Base.ControlType.None);
            Item.orderNum = 100;
            Item.Position = _Position;
            Item.effectType = Objects.Base.EffectType.Ripple;
            //List<Object> Objs3 = new List<object>();
            //Objs3.Add(Item);
            //Item.actionCall.Add(new Actions.ActionCall(Actions.ActionType.Collision, typeof(Objects.Sprite2d), "Pickup", Objs3));
            //Item.actionCall.Add(new Actions.ActionCall(Actions.ActionType.Collision, typeof(Items.Health), "HealthPickup", null));

            return Item;
        }

        public static Objects.Sprite2d GetWarpToMapPart(Vector2 _Position, Maps.MapPart MP)
        {
            Objects.Sprite2d Item = GetItem(_Position);
            List<Object> Objs3 = new List<object>();
            Objs3.Add(MP);
            Item.actionCall.Add(new Actions.ActionCall(Actions.ActionType.Collision, typeof(Maps.Map), "LoadMapByMapPart", Objs3));
            return Item;
        }
        public static Objects.Sprite2d GetReturnWarp(Vector2 _Position)
        {
            Objects.Sprite2d Item = GetItem(_Position);
            List<Object> Objs3 = new List<object>();
            Item.actionCall.Add(new Actions.ActionCall(Actions.ActionType.Collision, typeof(Maps.Map), "ReturnToPreviousWarp", Objs3));
            return Item;
        }
        public static Objects.Sprite2d GetWarpByMapVector(Vector2 _Position, Vector3 MapVector)
        {
            Objects.Sprite2d Item = GetItem(_Position);
            List<Object> Objs3 = new List<object>();
            Objs3.Add(MapVector);
            Item.actionCall.Add(new Actions.ActionCall(Actions.ActionType.Collision, typeof(Maps.Map), "LoadMapByVector", Objs3));
            return Item;
        }
    }
}
