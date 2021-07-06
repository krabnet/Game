using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Game.Items
{
    public static class ItemActions
    {
        public static void PickupItem(Game.Objects.Sprite2d Item)
        {
            if (Util.Global.Sprites.Where(x => x.Item.State == Objects.Item.ItemState.Hand).Count() == 0)
            {
                List<Objects.Sprite2d> spritsintheway = Util.Global.Sprites.Where(l => Util.Base.collision(l, new Rectangle((int)Item.Position.X, (int)Item.Position.Y, 25, 25)) == true && l.active == true && l.actionCall.Count > 0 && l.ID != Item.ID).ToList();
                if (spritsintheway.Count == 0)
                {
                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().actionCall.Where(x => x.ActionType == Actions.ActionType.Item).Select(y => { y.ActionType = Actions.ActionType.Mouse; return y; }).ToList();

                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().actionCall.RemoveAll(x => x.actionMethodName == "PickupItem");
                    List<Object> Objs2 = new List<object>();
                    Objs2.Add(Item);
                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "DropItem", Objs2));
                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().Position = new Vector2(Item.Position.X - 5, Item.Position.Y - 5);
                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().controlType = Objects.Base.ControlType.Mouse;
                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().Item.State = Objects.Item.ItemState.Hand;
                    if (Item.Item.Type == Items.Item.ItemType.Torch)
                    {
                        Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().LightSourceDistance = 30f;
                    }
                }
            }
        }

        public static void DropItem(Game.Objects.Sprite2d Item)
        {
            if (Item != null)
            {
                List<Objects.Sprite2d> spritsintheway = Util.Global.Sprites.Where(l => Util.Base.collision(l, new Rectangle((int)Item.Position.X, (int)Item.Position.Y, 50, 50)) == true && l.active == true && l.actionCall.Count > 0 && l.Item.Type != Item.Item.Type).ToList();
                if (spritsintheway.Count == 0)
                {
                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().actionCall.Where(x => x.ActionType == Actions.ActionType.Mouse).Select(y => { y.ActionType = Actions.ActionType.Item; return y; }).ToList();
                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().actionCall.RemoveAll(x => x.actionMethodName == "DropItem");
                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().controlType = Objects.Base.ControlType.None;
                    List<Object> Objs3 = new List<object>();
                    Objs3.Add(Item);
                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", Objs3));
                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().controlType = Objects.Base.ControlType.None;
                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().Item.State = Objects.Item.ItemState.Null;
                    if (Util.Global.Sprites.Where(x => x.name == "Inventory").Count() > 0)
                    {
                        Util.Global.Sprites.RemoveAll(x => x.ID == Item.ID);
                        new Objects.Item().AddItemToInventory(Item, Item.Item.Number, 1);
                        //Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().Item.State = Objects.Item.ItemState.Inventory;
                        Menu.Inventory.DisplayInventory();
                    }

                    //if (Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().Item.Number > 1)
                    //{
                    //    Util.Global.Sprites.Add(Items.Item.GetItemByType(Item.Item.Type, Util.Global.GetTrueMousePosition()));
                    //    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().Item.Number--;
                    //}
                    //else
                    //{

                    //}
                }
            }
        }

        public static void CollisionPickup(Objects.Sprite2d Item)
        {
            //Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().active = false;
            Util.Global.Sprites.RemoveAll(x => x.ID == Item.ID);
            new Objects.Item().AddItemToInventory(Items.Item.GetItemByType(Item.Item.Type, new Vector2(0, 0)), 1, 1);
        }

        public static void HealthPickup(Objects.Sprite2d Item)
        {
            Item.active = false;
            Util.Global.Sprites.RemoveAll(x => x.ID == Item.ID);
            Util.Global.Hero.Actor.Health++;
        }

        public static void ReplaceAndDropItem(List<Tuple<Items.Item.ItemType, Items.Item.ItemType, Items.Item.ItemType>> ItemList)
        {
            Vector2 MousePos = Util.Global.GetTrueMousePosition();
            int x = (int)MousePos.X;
            int y = (int)MousePos.Y;

            foreach (Tuple<Items.Item.ItemType, Items.Item.ItemType, Items.Item.ItemType> I in ItemList)
            {
                Objects.Sprite2d ObjectToFind = Items.Item.GetItemByType(I.Item1, new Vector2(0, 0));
                Objects.Sprite2d ObjectToDrop = Items.Item.GetItemByType(I.Item2, new Vector2(0, 0));
                Objects.Sprite2d ObjectToReplace = new Objects.Sprite2d();
                if (I.Item3 != Item.ItemType.None)
                {
                    ObjectToReplace = Items.Item.GetItemByType(I.Item3, new Vector2(0, 0));
                }

                List<Objects.Sprite2d> spritsintheway = Util.Global.Sprites.Where(l => Util.Base.collision(l, new Rectangle(x, y, 5, 5)) == true && l.active == true && l.model.Name.Contains(ObjectToFind.model.Name)).ToList();
                if (spritsintheway.Count > 0)
                {
                    Util.Global.Sprites.Add(Items.Item.GetItemByType(I.Item2, (spritsintheway.FirstOrDefault().Position)));
                    if (I.Item3 != Item.ItemType.None)
                    {
                        Util.Global.Sprites.Where(s => s.ID == spritsintheway.FirstOrDefault().ID).FirstOrDefault().model = new Maps.Asset().getContentByName(ObjectToReplace.model.Name);
                        Util.Global.Sprites.Where(s => s.ID == spritsintheway.FirstOrDefault().ID).FirstOrDefault().clipping = false;
                        Util.Global.Sprites.Where(s => s.ID == spritsintheway.FirstOrDefault().ID).FirstOrDefault().orderNum = 1;
                    }
                    else
                    {
                        Util.Global.Sprites.RemoveAll(s => s.ID == spritsintheway.FirstOrDefault().ID);
                    }                   
                    Util.Global.ContentMan.Load<SoundEffect>("Sounds/ChopWood").Play();
                }
            }
        }

        public static void PickupTile(Dictionary<Items.Item.ItemType, Items.Item.ItemType> ItemList)
        {
            Vector2 MousePos = Util.Global.GetTrueMousePosition();
            int x = (int)MousePos.X;
            int y = (int)MousePos.Y;

            foreach (System.Collections.Generic.KeyValuePair<Items.Item.ItemType, Items.Item.ItemType> I in ItemList)
            {
                Objects.Sprite2d SP = Items.Item.GetItemByType(I.Key,new Vector2(0,0));
                Objects.Sprite2d SP2 = Items.Item.GetItemByType(I.Value, new Vector2(0, 0));

                List<Objects.Sprite2d> spritsintheway = Util.Global.Sprites.Where(l => Util.Base.collision(l, new Rectangle(x, y, 5, 5)) == true && l.active == true && l.model.Name == SP.model.Name).ToList();
                if (spritsintheway.Count > 0)
                {
                    Util.Global.Sprites.Where(s => s.ID == spritsintheway.FirstOrDefault().ID).FirstOrDefault().model = new Maps.Asset().getContentByName(SP2.model.Name);
                    Util.Global.Sprites.Where(s => s.ID == spritsintheway.FirstOrDefault().ID).FirstOrDefault().clipping = false;
                    new Objects.Item().AddItemToInventory(Items.Item.GetItemByType(I.Key, MousePos), 1, 1);
                    Util.Global.ContentMan.Load<SoundEffect>("Sounds/ChopWood").Play();
                }
            }
        }

        public static void ReplaceTile(Item.ItemType IT, bool clip, string Sound, bool Consume)
        {
            if (Util.Global.Sprites.Where(s => s.Item.Type == IT).FirstOrDefault().Item.Number > 0)
            {

                Vector2 MousePos = Util.Global.GetTrueMousePosition();
                int x = (int)MousePos.X;
                int y = (int)MousePos.Y;
                Objects.Sprite2d SP = Items.Item.GetItemByType(IT, new Vector2(0, 0));
                List<Objects.Sprite2d> spritsintheway = Util.Global.Sprites.Where(l => l.Actor == null && Util.Base.collision(l, new Rectangle(x, y, 15, 15)) == true && l.active == true && l.model.Name != SP.model.Name).ToList();
                if (spritsintheway.Count > 0 && !Util.Base.collision(spritsintheway.FirstOrDefault(), Util.Global.Hero))
                {
                    Util.Global.Sprites.Where(s => s.ID == spritsintheway.FirstOrDefault().ID).FirstOrDefault().model = SP.model;
                    Util.Global.Sprites.Where(s => s.ID == spritsintheway.FirstOrDefault().ID).FirstOrDefault().clipping = clip;

                    if (Sound != null)
                    {
                        Util.Global.ContentMan.Load<SoundEffect>(Sound).Play();
                    }

                    if (Consume)
                    {   
                        Util.Global.Sprites.Where(s => s.Item.Type == IT).FirstOrDefault().Item.Number -= 1;
                        Menu.Inventory.UpdateInventoryCounts();
                    }
                }
            }
        }

        public static void ProximityAnimAction(Objects.Sprite2d Item)
        {
            if (Util.Global.Sprites.Where(x => x.ID == Item.ID).Count() > 0)
            {
                Rectangle rec1 = new Rectangle((int)Util.Global.Hero.Position.X, (int)Util.Global.Hero.Position.Y, (int)Util.Global.Hero.Size.X, (int)Util.Global.Hero.Size.Y);
                Rectangle rec2 = new Rectangle((int)Item.Position.X, (int)Item.Position.Y, (int)Item.Size.X, (int)Item.Size.Y);
                if (Util.Base.collision(rec1, 100, rec2))
                {
                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().AnimSprite.action = true;
                }
                else
                {
                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().AnimSprite.action = false;
                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().AnimSprite.currentFrame = 0;
                }
            }
        }

        public static void AddSapling()
        {
            Vector2 MousePos = Util.Global.GetTrueMousePosition();
            int x = (int)MousePos.X;
            int y = (int)MousePos.Y;

            List<Objects.Sprite2d> spritsintheway = Util.Global.Sprites.Where(l => Util.Base.collision(l, new Rectangle(x, y, 50, 50)) == true && l.active == true && l.orderNum > 50 && l.name != "sapling" && l.name != "MainGame_mouse").ToList();
            if (spritsintheway.Count == 0)
            {
                Objects.Sprite2d AS = Items.Item.GetItemByType(Item.ItemType.Sapling_Tree, new Vector2(x, y));
                Util.Global.Sprites.Add(AS);
                Util.Global.MainMap[(int)Util.Global.CurrentMap.X, (int)Util.Global.CurrentMap.Y, (int)Util.Global.CurrentMap.Z].Sprite2d.Add(AS);
            }
        }
    }
}
