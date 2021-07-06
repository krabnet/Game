using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Game.Actions;

namespace Game.Items
{
    public static class ItemActions
    {
        public static void PickupItem(Game.Objects.Sprite2d Item)
        {
            if (Util.Global.Sprites.Where(x => x.Item.State == Items.Item.ItemState.Hand).Count() == 0)
            {
                //List<Objects.Sprite2d> spritsintheway = Util.Global.Sprites.Where(l => Util.Base.collision(l, new Rectangle((int)Item.Position.X, (int)Item.Position.Y, 25, 25)) == true && l.active == true && l.actionCall.Count > 0 && l.ID != Item.ID).ToList();
                //if (spritsintheway.Count == 0)
                //{
                    //Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().actionCall.Where(x => x.ActionType == Actions.ActionType.Item).Select(y => { y.ActionType = Actions.ActionType.Mouse; return y; }).ToList();
                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().actionCall.RemoveAll(x => x.actionMethodName == "PickupItem");
                    List<Object> Objs2 = new List<object>();
                    Objs2.Add(Item);
                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "DropItem", Objs2));
                    //Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().Position = new Vector2(Item.Position.X - 5, Item.Position.Y - 5);
                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().controlType = Objects.Base.ControlType.Mouse;
                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().Item.State = Items.Item.ItemState.Hand;
                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().orderNum = 102000;
                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().LightIgnor = true;
                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().active = true;
                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().clipping = false;
                    if (Item.Item.OriSize != null && Item.Item.OriSize.X > 0 && Item.Item.OriSize.Y > 0)
                    {
                        Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().Size = Item.Item.OriSize;
                    }
                    if (Item.Item.Type == Items.Item.ItemType.Torch)
                    {
                        Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().LightSourceDistance = Util.Global.TorchLightDistance;
                        Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().Viewtype = Objects.Base.ViewType.Default;
                        Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().orderNum = 102000;
                    }
                //}
            }
        }

        public static void DropItem(Game.Objects.Sprite2d Item)
        {
            bool badplacement = true;
            if (Item != null)
            {
                if (Item.Item.Number == 0)
                {
                    Util.Global.Sprites.RemoveAll(x => x.ID == Item.ID);
                }
                else
                {
                    List<Objects.Sprite2d> SIR = Util.Global.Sprites.Where(l => l.active == true && l.actionCall.Count > 0 && l.Item.Type != Item.Item.Type).ToList();
                    List<Objects.Sprite2d> spritsintheway = SIR.Where(l => Util.Base.collision(l, new Rectangle((int)Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.Default).X, (int)Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.Default).Y, 50, 50)) == true).ToList();

                    if (Menu.Inventory.IsInventoryOpen() && Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.HUD).X > Util.Global.screenSizeWidth - 150 && Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.HUD).Y < Util.Global.screenSizeHeight - 50)
                    {
                        Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().Position = Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.Default);
                        Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().actionCall.RemoveAll(x => x.actionMethodName == "DropItem");
                        Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().controlType = Objects.Base.ControlType.None;
                        Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().Item.State = Items.Item.ItemState.Null;
                        Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().Viewtype = Objects.Base.ViewType.Default;
                        Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().orderNum = 500;
                        Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().LightIgnor = false;
                        List<Object> Objs3 = new List<object>();
                        Objs3.Add(Item);
                        Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", Objs3));
                                
                        Util.Global.Sprites.RemoveAll(x => x.ID == Item.ID);
                        Items.ItemActions.AddItemToInventory(Item, Item.Item.Number, 1);
                        badplacement = false;
                    }
                    else if (Menu.Hotbar.IsOpen() && Menu.Hotbar.IsMouseCollision())
                    {
                            int Slot = Menu.Hotbar.GetMouseSlot();
                            if (Slot > 0)
                            {
                                Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().Position = Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.Default);
                                Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().actionCall.RemoveAll(x => x.actionMethodName == "DropItem");
                                Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().controlType = Objects.Base.ControlType.None;
                                Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().Item.State = Items.Item.ItemState.Hotbar;
                                Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().Item.ItemSlot = Slot;
                                Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().Viewtype = Objects.Base.ViewType.Default;
                                Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().orderNum = 500;
                                Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().LightIgnor = true;
                                List<Object> Objs3 = new List<object>();
                                Objs3.Add(Item);
                                Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", Objs3));

                                Menu.Hotbar.Display();
                                badplacement = false;
                            }

                    }
                    else if (spritsintheway.Count == 0)
                    {
                        if (Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.Default).X > 0 && Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.Default).Y > 0 && Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.Default).X < Util.Global.SizeMap * 32 && Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.Default).Y < Util.Global.SizeMap * 32)
                        {
                            //Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().actionCall.Where(x => x.ActionType == Actions.ActionType.Mouse).Select(y => { y.ActionType = Actions.ActionType.Item; return y; }).ToList();
                            Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().Position = Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.Default);
                            Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().actionCall.RemoveAll(x => x.actionMethodName == "DropItem");
                            Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().controlType = Objects.Base.ControlType.None;
                            Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().Item.State = Items.Item.ItemState.Null;
                            Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().Viewtype = Objects.Base.ViewType.Default;
                            Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().orderNum = 500;
                            Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().LightIgnor = false;
                            List<Object> Objs3 = new List<object>();
                            Objs3.Add(Item);
                            Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", Objs3));

                            if (Util.Global.Chest != null && Item.Item.Type != Items.Item.ItemType.Chest)
                            {
                                Item.Item.State = Items.Item.ItemState.Chest;
                                Util.Global.Sprites.RemoveAll(x => x.ID == Item.ID);
                                Util.Global.Sprites.Where(x => x.ID == Util.Global.Chest.ID).FirstOrDefault().Inventory.Add(Item.Item);
                                Menu.Chest.DisplayChest(Util.Global.Chest);
                            }
                            badplacement = false;
                        }
                    }

                    if (badplacement)
                    {
                        Objects.Sprite2d Sprite = new Objects.Sprite2d("drop", "BadPlacement", true, Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.Default), Item.Size, 0, Objects.Base.ControlType.None);
                        Sprite.orderNum = 105001;
                        Sprite.color = Color.Red * 0.5f;
                        Sprite.LightIgnor = true;
                        Util.Global.Sprites.Add(Sprite);

                        ActionEvents AE = new ActionEvents();
                        List<object> obj1 = new List<object>();
                        obj1.Add(Sprite);
                        AE.actionCall.Add(new ActionCall(ActionType.Item, typeof(ItemActions), "RemoveSprite", obj1));
                        AE.Duration = 1;
                        AE.InitialDuration = 1;
                        Util.Global.ActionEvents.Add(AE);
                    }
                }
            }
        }

        public static void RemoveSprite(Objects.Sprite2d Item)
        {
            Util.Global.Sprites.RemoveAll(x => x.ID == Item.ID);
        }

        public static void CollisionPickup(Objects.Sprite2d Item)
        {
            if (Item.Item.State != Items.Item.ItemState.Hand)
            {
                Util.Global.Sprites.RemoveAll(x => x.ID == Item.ID);
                int count = 1;
                if (Item.Item.Number > 1)
                { count = Item.Item.Number; }
                Items.ItemActions.AddItemToInventory(Items.Item.GetItemByType(Item.Item.Type, Util.Global.DefaultPosition), count, 1);

                if (Item.collisionSound != null)
                { Util.Global.ContentMan.Load<SoundEffect>(Item.collisionSound).Play(); }

            }
        }

        public static void HealthPickup(Objects.Sprite2d Item)
        {
            if (Util.Global.Hero.Actor.Health < Util.Global.Hero.Actor.HealthMax)
            {
                Util.Global.Sprites.RemoveAll(x => x.ID == Item.ID);
                Util.Global.ContentMan.Load<SoundEffect>("Sounds/Gulp").Play();
                Util.Global.Sprites.Where(x => x.ID == Util.Global.Hero.ID).FirstOrDefault().Actor.Health++;
                Menu.ActorStats.UpdateStats(Util.Global.Hero);
            }
        }

        public static void ChangeTotemText()
        {
            foreach (Objects.Sprite2d Totem in Util.Global.Sprites.Where(x => x.Item != null && x.Item.Type == Items.Item.ItemType.Totem))
            {
                Util.Global.Sprites.Where(x => x.ID == Totem.ID).FirstOrDefault().actionCall.RemoveAll(y => y.actionMethodName == "ShowText");
                List<object> ActionObjects = new List<object>();
                ActionObjects.Add(Totem);
                ActionObjects.Add(Items.ItemActions.GetTotemText());
                Util.Global.Sprites.Where(x => x.ID == Totem.ID).FirstOrDefault().actionCall.Add((new Actions.ActionCall(Game.Actions.ActionType.MouseAny, typeof(Items.ItemActions), "ShowText", ActionObjects)));
            }
        }

        public static void ReplaceAndDropItem(List<Tuple<Items.Item.ItemType, Items.Item.ItemType, Items.Item.ItemType>> ItemList)
        {
            Vector2 MousePos = Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.Default);
            int x = (int)MousePos.X;
            int y = (int)MousePos.Y;

            foreach (Tuple<Items.Item.ItemType, Items.Item.ItemType, Items.Item.ItemType> I in ItemList)
            {
                Objects.Sprite2d ObjectToFind = Items.Item.GetItemByType(I.Item1, Util.Global.DefaultPosition);
                Objects.Sprite2d ObjectToDrop = Items.Item.GetItemByType(I.Item2, Util.Global.DefaultPosition);
                Objects.Sprite2d ObjectToReplace = new Objects.Sprite2d();
                if (I.Item3 != Item.ItemType.None)
                {
                    ObjectToReplace = Items.Item.GetItemByType(I.Item3, Util.Global.DefaultPosition);
                }

                List<Objects.Sprite2d> spritsintheway = Util.Global.Sprites.Where(l => Util.Base.collision(l, new Rectangle(x, y, 5, 5)) == true && l.active == true && l.modelname!=null && l.modelname.Contains(ObjectToFind.modelname)).ToList();
                if (spritsintheway.Count > 0)
                {
                    Util.Global.Sprites.Add(Items.Item.GetItemByType(I.Item2, (spritsintheway.FirstOrDefault().Position)));
                    if (I.Item3 != Item.ItemType.None)
                    {
                        Util.Global.Sprites.Where(s => s.ID == spritsintheway.FirstOrDefault().ID).FirstOrDefault().modelname = ObjectToReplace.modelname;
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
            bool RemoveTile = true;
            Vector2 MousePos = Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.Default);
            foreach (System.Collections.Generic.KeyValuePair<Items.Item.ItemType, Items.Item.ItemType> I in ItemList)
            {
                Objects.Sprite2d SP = Items.Item.GetItemByType(I.Key,new Vector2(0,0));
                Objects.Sprite2d SP2 = Items.Item.GetItemByType(I.Value, Util.Global.DefaultPosition);

                List<Objects.Sprite2d> spritsintheway = Util.Global.Sprites.Where(l => Util.Base.collision(l, new Rectangle((int)MousePos.X, (int)MousePos.Y, 1, 1)) == true && l.active == true && l.modelname == SP.modelname).OrderByDescending(o => o.orderNum).ToList();
                if (spritsintheway.Count > 0)
                {
                    switch (SP.Item.Type)
                    {
                        case Item.ItemType.Wall:
                            Items.ItemActions.AddItemToInventory(Items.Item.GetItemByType(Item.ItemType.BuildWall, Util.Global.DefaultPosition), 1, 1);
                            break;
                        case Item.ItemType.Fence:
                            Items.ItemActions.AddItemToInventory(Items.Item.GetItemByType(Item.ItemType.BuildFence, Util.Global.DefaultPosition), 1, 1);
                            Util.Global.Sprites.RemoveAll(x => x.ID == spritsintheway.FirstOrDefault().ID);
                            RemoveTile = false;
                            break;
                        default:
                            Items.ItemActions.AddItemToInventory(Items.Item.GetItemByType(I.Key, new Vector2(0,0)), 1, 1);
                            break;
                    }
                    if (RemoveTile)
                    {
                        Util.Global.Sprites.Where(s => s.ID == spritsintheway.FirstOrDefault().ID).FirstOrDefault().modelname = SP2.modelname;
                        Util.Global.Sprites.Where(s => s.ID == spritsintheway.FirstOrDefault().ID).FirstOrDefault().Item.Type = SP2.Item.Type;
                        Util.Global.Sprites.Where(s => s.ID == spritsintheway.FirstOrDefault().ID).FirstOrDefault().clipping = false;
                    }

                    Util.Global.ContentMan.Load<SoundEffect>("Sounds/ChopWood").Play();
                }
            }
        }

        public static void ReplaceTile(Item.ItemType IT, bool clip, string Sound, bool Consume)
        {
            if (Util.Global.Sprites.Where(s => s.Item.Type == IT).FirstOrDefault().Item.Number > 0)
            {
                bool ReplaceFlag = true;
                Vector2 MousePos = Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.Default);
                int x = (int)MousePos.X;
                int y = (int)MousePos.Y;
                Objects.Sprite2d SP = new Objects.Sprite2d();
                switch (IT)
                    {
                        case Item.ItemType.BuildWall:
                            SP = Items.Item.GetItemByType(Item.ItemType.Wall, Util.Global.DefaultPosition);
                            break;
                        case Item.ItemType.BuildFence:
                            SP = Items.Item.GetItemByType(Item.ItemType.Fence, Util.Global.DefaultPosition);
                            ReplaceFlag = false;
                            break;
                        case Item.ItemType.Plank:
                            SP = Items.Item.GetItemByType(Item.ItemType.Floor, Util.Global.DefaultPosition);
                            break;
                        default:
                            SP = Items.Item.GetItemByType(IT, Util.Global.DefaultPosition);
                            break;
                    }
                //&& l.modelname != SP.modelname
                List<Objects.Sprite2d> spritsintheway = Util.Global.Sprites.Where(l => l.Actor == null && Util.Base.collision(l, new Rectangle(x, y, 1, 1)) == true && l.active == true).OrderByDescending(o => o.orderNum).ToList();
                if (spritsintheway.Count > 0 && !Util.Base.collision(spritsintheway.FirstOrDefault(), Util.Global.Hero))
                {
                    if (spritsintheway.FirstOrDefault().name.Contains(":") && spritsintheway.All(f => f.Item.Type != SP.Item.Type))
                    {
                        if (ReplaceFlag)
                        {
                            Util.Global.Sprites.Where(s => s.ID == spritsintheway.FirstOrDefault().ID).FirstOrDefault().modelname = SP.modelname;
                            Util.Global.Sprites.Where(s => s.ID == spritsintheway.FirstOrDefault().ID).FirstOrDefault().Item.Type = SP.Item.Type;
                            Util.Global.Sprites.Where(s => s.ID == spritsintheway.FirstOrDefault().ID).FirstOrDefault().clipping = clip;
                            Util.Global.Sprites.Where(s => s.ID == spritsintheway.FirstOrDefault().ID).FirstOrDefault().orderNum = SP.orderNum;
                            Util.Global.Sprites.Where(s => s.ID == spritsintheway.FirstOrDefault().ID).FirstOrDefault().Viewtype = Objects.Base.ViewType.Default;
                        }
                        else
                        {
                            SP.Position = Util.Global.Sprites.Where(s => s.ID == spritsintheway.FirstOrDefault().ID).FirstOrDefault().Position;
                            Util.Global.Sprites.Add(SP);  
                        }

                        if (Sound != null)
                        {
                            Util.Global.ContentMan.Load<SoundEffect>(Sound).Play();
                        }

                        if (Consume)
                        {
                            if (Util.Global.Sprites.Where(s => s.Item.Type == IT && s.Item.State == Item.ItemState.Hand).FirstOrDefault() != null)
                            {
                                Util.Global.Sprites.Where(s => s.Item.Type == IT && s.Item.State == Item.ItemState.Hand).FirstOrDefault().Item.Number -= 1;
                                Menu.Inventory.UpdateInventoryCounts();

                            if (Util.Global.Sprites.Where(s => s.Item.Type == IT && s.Item.State == Item.ItemState.Hand).FirstOrDefault().Item.Number <= 0)
                            {
                                RemoveSprite(Util.Global.Sprites.Where(s => s.Item.Type == IT).FirstOrDefault());
                            }

                            }
                        }
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

        public static void ProximitySpawnAction(Objects.Sprite2d Item)
        {
            if (Util.Global.Sprites.Where(x => x.ID == Item.ID).Count() > 0)
            {
                Rectangle rec1 = new Rectangle((int)Util.Global.Hero.Position.X, (int)Util.Global.Hero.Position.Y, (int)Util.Global.Hero.Size.X, (int)Util.Global.Hero.Size.Y);
                Rectangle rec2 = new Rectangle((int)Item.Position.X, (int)Item.Position.Y, (int)Item.Size.X, (int)Item.Size.Y);
                if (Util.Base.collision(rec1, 100, rec2))
                {
                    Util.Global.ContentMan.Load<SoundEffect>("Sounds/growl").Play();
                    Actions.Enemy.SpawnEnemy(Item.Position);
                    Util.Global.Sprites.RemoveAll(x => x.ID == Item.ID);
                }
                else
                {

                }
            }
        }

        public static void FlipClip(Objects.Sprite2d S)
        {
            if (Util.Base.collision(S, Util.Global.Hero) != true)
            {
                if (Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().AnimSprite.action == false)
                { Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().clipping = !S.clipping; }
                if (Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().AnimSprite != null)
                {
                    Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().AnimSprite.action = true;
                }
            }
        }

        public static void ConsumeItemAndBuff(Item.ItemType IT, Objects.Sprite2d Actor, Game.Actions.Buff.BuffType T, int Duration)
        {
            if (Util.Global.Sprites.Where(s => s.Item.Type == IT).FirstOrDefault().Item.Number > 0)
            {
                Actions.Buff.AddBuff(Actor, T, Duration);
                Util.Global.Sprites.Where(s => s.Item.Type == IT).FirstOrDefault().Item.Number -= 1;
                Menu.Inventory.UpdateInventoryCounts();
            }
        }

        public static void ConsumeItemObjectAndBuff(Objects.Sprite2d IT, Objects.Sprite2d Actor, Game.Actions.Buff.BuffType T, int Duration)
        {
            if (Util.Global.Sprites.Where(s => s.ID == IT.ID).FirstOrDefault().Item.Number > 0)
            {
                Actions.Buff.AddBuff(Actor, T, Duration);
                Util.Global.Sprites.Where(s => s.ID == IT.ID).FirstOrDefault().Item.Number -= 1;
                if (Util.Global.Sprites.Where(s => s.ID == IT.ID).FirstOrDefault().Item.Number <= 0)
                {
                    Util.Global.Sprites.RemoveAll(s => s.ID == IT.ID);
                }
                Menu.Inventory.UpdateInventoryCounts();
            }
        }

        public static void AddSapling(Objects.Sprite2d Item, Item.ItemType IT)
        {
            Vector2 MousePos = Util.GameMouse.GetTrueMousePosition(Objects.Base.ViewType.Default);
                List<Objects.Sprite2d> spritsintheway = Util.Global.Sprites.Where(l => Util.Base.collision(l, new Rectangle((int)MousePos.X, (int)MousePos.Y, 1, 1)) == true && l.active == true && l.Item.Type != Item.Item.Type && l.name != "MainGame_mouse").ToList();
                if (spritsintheway.Count == 1 && spritsintheway.FirstOrDefault().modelname == "grass0"  && Item.Item.Number > 0)
                {
                    Objects.Sprite2d AS = Items.Item.GetItemByType(IT, spritsintheway.FirstOrDefault().Position);
                    Util.Global.Sprites.Add(AS);
                    Util.Global.MainMap[(int)Util.Global.CurrentMap.X, (int)Util.Global.CurrentMap.Y, (int)Util.Global.CurrentMap.Z].Sprite2d.Add(AS);

                    Util.Global.Sprites.Where(x => x.ID == Item.ID).FirstOrDefault().Item.Number--;
                    Menu.Inventory.UpdateInventoryCounts();
                }
                else
                {
                    Objects.Sprite2d Sprite = new Objects.Sprite2d("drop", "BadPlacement", true, Item.Position, Item.Size, 0, Objects.Base.ControlType.None);
                    Sprite.orderNum = 1001;
                    Sprite.color = Color.Red * 0.5f;
                    Sprite.LightIgnor = true;
                    Util.Global.Sprites.Add(Sprite);

                    ActionEvents AE = new ActionEvents();
                    List<object> obj1 = new List<object>();
                    obj1.Add(Sprite);
                    AE.actionCall.Add(new ActionCall(ActionType.Item, typeof(ItemActions), "RemoveSprite", obj1));
                    AE.Duration = 1;
                    AE.InitialDuration = 1;
                    Util.Global.ActionEvents.Add(AE);

                }
                
                if(Item.Item.Number <= 0)
                {
                    Util.Global.Sprites.RemoveAll(x => x.ID == Item.ID);
                }
        }

        public static void ShowText(Objects.Sprite2d Item, string text)
        {
            string Name = "ItemText" + Item.ID.ToString();
            Vector2 BasePos = Item.Position;
            BasePos = Vector2.Add(BasePos, new Vector2(0, -25));
            Util.Global.Sprites.Add(new Objects.Sprite2d(null, Name, true, BasePos, Item.Size, 0, Objects.Base.ControlType.None));
            string Stat = "";
            //Stat = Stat + "ID:" + Actor.ID.ToString() + "\n";
            Stat = Stat + text;
            Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().text = Stat;
            Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().textSize = 1f;
            Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().color = Item.color;
            Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().boxColor = Item.boxColor;
            Util.Global.Sprites.Where(x => x.name == Name).FirstOrDefault().orderNum = 2000;

            ActionEvents AE = new ActionEvents();
            List<object> obj1 = new List<object>();
            obj1.Add(Item);
            AE.actionCall.Add(new ActionCall(ActionType.Item, typeof(Items.ItemActions), "HideText", obj1));
            AE.Duration = 5;
            AE.InitialDuration = 5;
            Util.Global.ActionEvents.Add(AE);

        }

        public static void HideText(Objects.Sprite2d Item)
        {
            string Name = "ItemText" + Item.ID.ToString();
            Util.Global.Sprites.RemoveAll(x => x.name == Name);
        }

        public static void SayTotemText(Objects.Sprite2d Item, string text)
        {
            AddJournal(text);
            ShowText(Item, text);
        }

        public static string GetTotemText()
        {
            List<string> Totem = new List<string>();

            //Totem.Add("omg");
            //Totem.Add("wth");
            //Totem.Add("ftw");
            //Totem.Add("*<8O)");

            foreach(Actions.Enemy.EnemyType ET in Enum.GetValues(typeof(Actions.Enemy.EnemyType)))
            {
                Objects.Sprite2d Actor = Actions.Enemy.GetEnemyByType(ET,new Vector2(0,0));

                string TotemText = "";
                if (Actor.Actor.Parents.Count() > 0)
                {
                    if (Actor.Actor.enemyType.ToString().Substring(0, 1) == "A")
                    {
                        TotemText = TotemText + "If you want to tame an " + Actor.Actor.enemyType.ToString() + " \n bring along ";
                    }
                    else
                    {
                        TotemText = TotemText + "If you want to tame a " + Actor.Actor.enemyType.ToString() + " \n bring along ";
                    }
                    foreach (Actions.Enemy.EnemyType ET2 in Actor.Actor.Parents)
                    {
                        if (Actor.Actor.Parents.Last() == ET2 && Actor.Actor.Parents.First() != ET2)
                        {
                            TotemText = TotemText + " and the " + ET2.ToString() + ",";
                        }
                        else
                        {
                            TotemText = TotemText + " the " + ET2.ToString() + ",";
                        }
                    }
                    TotemText = TotemText.Remove(TotemText.Length - 1, 1) + ".";
                    Totem.Add(TotemText);
                }
            }

            string ret = Totem.OrderBy(s => Guid.NewGuid()).First();
            return ret;
        }

        public static void GardenUpdate(Objects.Sprite2d Item, Item.ItemType IT)
        {
            Util.Global.Sprites.Add(Items.Item.GetItemByType(IT,Item.Position));
            Util.Global.Sprites.RemoveAll(x => x.ID == Item.ID);
        }

        public static void AddItemToInventory(Objects.Sprite2d Item, int Count, int slot)
        {
            bool addflag = true;

            if (Item.Item.Stack == true)
            {
                foreach (Objects.Sprite2d S in Util.Global.Sprites.Where(x => x.Item.State == Items.Item.ItemState.Inventory))
                {
                    if (Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Item.Type == Item.Item.Type)
                    {
                        Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Item.Number += Count;
                        if ( Util.Global.Sprites.Where(x => x.name == "InventoryText" + S.ID).Count() == 1)
                              Util.Global.Sprites.Where(x => x.name == "InventoryText" + S.ID).FirstOrDefault().text = Util.Global.Sprites.Where(x => x.ID == S.ID).FirstOrDefault().Item.Number.ToString();
                        addflag = false;
                    }
                }
            }

            if (addflag)
            {
                //Item.Item.Number = (Count < 1) ? 1 : Count;
                Item.Item.Number = Count;
                Item.Item.ItemSlot = 0;
                Item.Item.State = Items.Item.ItemState.Inventory;
                Util.Global.Sprites.Add(Item);
                Menu.Inventory.UpdateInventoryCounts();
            }
            
        }

        public static void HotbarAction(int Slot)
        {
            Objects.Sprite2d Item = Util.Global.Sprites.Where(x => x.Item != null && x.Item.State == Items.Item.ItemState.Hotbar && x.Item.ItemSlot == Slot).FirstOrDefault();
            List<Objects.Sprite2d> inHand = Util.Global.Sprites.Where(x => x.Item != null && x.Item.State == Items.Item.ItemState.Hand).ToList();
            if (inHand.Count > 0)
            {
                if (inHand.FirstOrDefault().Item.ItemSlot > 0 || Item == null)
                {
                    Util.Global.Sprites.Where(x => x.ID == inHand.FirstOrDefault().ID).FirstOrDefault().Item.State = Items.Item.ItemState.Hotbar;
                    Util.Global.Sprites.Where(x => x.ID == inHand.FirstOrDefault().ID).FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
                    Util.Global.Sprites.Where(x => x.ID == inHand.FirstOrDefault().ID).FirstOrDefault().controlType = Objects.Base.ControlType.None;
                    Util.Global.Sprites.Where(x => x.ID == inHand.FirstOrDefault().ID).FirstOrDefault().active = false;
                    if (Menu.Hotbar.IsOpen())
                    {
                        Menu.Hotbar.Display();
                    }
                }
                else
                {
                    AddItemToInventory(Item, 1, 0);
                }
            }
            if (Item != null)
                PickupItem(Item);
        }

        public static void DropItemInHand()
        {
            List<Objects.Sprite2d> inHand = Util.Global.Sprites.Where(x => x.Item != null && x.Item.State == Items.Item.ItemState.Hand).ToList();
            if (inHand.Count > 0)
            {
                if (inHand.FirstOrDefault().Item.ItemSlot > 0)
                {
                    Util.Global.Sprites.Where(x => x.ID == inHand.FirstOrDefault().ID).FirstOrDefault().Item.State = Items.Item.ItemState.Hotbar;
                    Util.Global.Sprites.Where(x => x.ID == inHand.FirstOrDefault().ID).FirstOrDefault().Viewtype = Objects.Base.ViewType.HUD;
                    Util.Global.Sprites.Where(x => x.ID == inHand.FirstOrDefault().ID).FirstOrDefault().controlType = Objects.Base.ControlType.None;
                    Util.Global.Sprites.Where(x => x.ID == inHand.FirstOrDefault().ID).FirstOrDefault().active = false;

                    List<Object> Objs3 = new List<object>();
                    Objs3.Add(inHand.FirstOrDefault());
                    Util.Global.Sprites.Where(x => x.ID == inHand.FirstOrDefault().ID).FirstOrDefault().actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", Objs3));

                    if (Menu.Hotbar.IsOpen())
                    {
                        Menu.Hotbar.Display();
                    }
                }
                else
                {
                    DropItem(inHand.FirstOrDefault());
                }
            }
        }

        public static void ActivateWarp(Objects.Sprite2d S)
        {
            string WarpName = Util.Base.GetRandomName();
            Vector2 WarpPos = S.Position;
            Util.Global.Sprites.RemoveAll(x => x.ID == S.ID);

            

            Objects.Sprite2d ActiveWarp = Item.GetItemByType(Item.ItemType.WarpPadOn, WarpPos);
            Util.Global.Sprites.Add(ActiveWarp);
           
            Items.Warp addWarp = new Warp();
            addWarp.MapLocation = Util.Global.CurrentMap;
            addWarp.WarpName = ActiveWarp.Item.Description;
            addWarp.WarpLocation = WarpPos;
            addWarp.WarpID = ActiveWarp.ID;

            Util.Asset.CaptureScreenShot("WarpShot:" + addWarp.WarpName);

            Util.Global.Warp.Add(addWarp);
        }

        public static void Sleep()
        {
            if ((int)Util.Global.GameClock.TotalSeconds >= 180 && (int)Util.Global.GameClock.TotalSeconds < 420)
            {
                Actions.Say.speak(Util.Global.Hero.ID, "Good Morning!");
                Util.Global.ContentMan.Load<SoundEffect>("Sounds/rooster").Play();
                int sec = 419 - (int)Util.Global.GameClock.TotalSeconds;
                Util.Global.GameClock = Util.Global.GameClock.Add(new TimeSpan(0, 0, sec));
            }
            else
            {
                Actions.Say.speak(Util.Global.Hero.ID, "I'm Not Tired!");
            }
        }

        public static void AddJournal(string messgae)
        {
            if (Util.Global.Journal.Where(x => x == messgae).Count() == 0)
            {
                Util.Global.Journal.Add(messgae);
            }
        }
    }
}
