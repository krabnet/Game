using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Game.Items
{
    public static class Item
    {
        public enum ItemType { None, Grass, Axe, Door, GoldBar, GoldOre, Hammer, Log, Sapling_Tree, Seed_Tree, Grain, Bread, Tree, Torch, Wall, IronBar, IronOre, Pickaxe, Coin, SilverBar, SilverOre, Oil, Health, Fountain, Plank, Stove };

        public static Game.Objects.Sprite2d GetItemByType(Item.ItemType IT, Vector2 Position)
        {
            Objects.Sprite2d ReturnSprite = new Objects.Sprite2d();
            List<Object> ActionObjects = new List<object>();
            List<Tuple<ItemType, ItemType, ItemType>> DropReplace = new List<Tuple<ItemType, ItemType, ItemType>>();
            
            switch (IT)
            {
                #region ores
                case Item.ItemType.IronBar:
                    ReturnSprite = GetItemSprite("ironbar", IT, Position);
                    ReturnSprite.collisionSound = Util.Global.ContentMan.Load<SoundEffect>("Sounds/shovel");
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    break;

                case Item.ItemType.IronOre:
                    ReturnSprite = GetItemSprite("IronOre", IT, Position);
                    ReturnSprite.orderNum = 1;
                    ReturnSprite.SpriteType = Objects.Base.Type.Tile;
                    break;

                case Item.ItemType.SilverBar:
                    ReturnSprite = GetItemSprite("silverbar", IT, Position);
                    ReturnSprite.collisionSound = Util.Global.ContentMan.Load<SoundEffect>("Sounds/shovel");
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    break;

                case Item.ItemType.SilverOre:
                    ReturnSprite = GetItemSprite("SilverOre", IT, Position);
                    ReturnSprite.orderNum = 1;
                    ReturnSprite.SpriteType = Objects.Base.Type.Tile;
                    break;

                case Item.ItemType.GoldBar:
                    ReturnSprite = GetItemSprite("goldbar", IT, Position);
                    ReturnSprite.collisionSound = Util.Global.ContentMan.Load<SoundEffect>("Sounds/shovel");
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    break;

                case Item.ItemType.GoldOre:
                    ReturnSprite = GetItemSprite("GoldOre", IT, Position);
                    ReturnSprite.orderNum = 1;
                    ReturnSprite.SpriteType = Objects.Base.Type.Tile;
                    break;
                #endregion

                #region tools
                case Item.ItemType.Axe:
                    ReturnSprite = GetItemSprite("axe", IT, Position);
                    ReturnSprite.collisionSound = Util.Global.ContentMan.Load<SoundEffect>("Sounds/shovel");
                    DropReplace = new List<Tuple<ItemType, ItemType, ItemType>>();
                    DropReplace.Add(new Tuple<ItemType, ItemType, ItemType>(ItemType.Sapling_Tree, ItemType.Log, ItemType.None));
                    DropReplace.Add(new Tuple<ItemType, ItemType, ItemType>(ItemType.Tree, ItemType.Log, ItemType.None));
                    DropReplace.Add(new Tuple<ItemType, ItemType, ItemType>(ItemType.Grain, ItemType.Grain, ItemType.None));
                    ActionObjects.Add(DropReplace);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Item, typeof(Items.ItemActions), "ReplaceAndDropItem", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.Hammer:
                    ReturnSprite = GetItemSprite("hammer", IT, Position);
                    ReturnSprite.collisionSound = Util.Global.ContentMan.Load<SoundEffect>("Sounds/shovel");
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    ActionObjects = new List<object>();
                    Dictionary<ItemType, ItemType> ItemList = new Dictionary<ItemType, ItemType>();
                    ItemList.Add(ItemType.Wall, ItemType.Grass);
                    ItemList.Add(ItemType.Plank, ItemType.Grass);
                    ActionObjects.Add(ItemList);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Item, typeof(Items.ItemActions), "PickupTile", ActionObjects));
                    break;

                case Item.ItemType.Pickaxe:
                    ReturnSprite = GetItemSprite("pick", IT, Position);
                    ReturnSprite.collisionSound = Util.Global.ContentMan.Load<SoundEffect>("Sounds/shovel");
                    DropReplace = new List<Tuple<ItemType, ItemType, ItemType>>();
                    DropReplace.Add(new Tuple<ItemType, ItemType, ItemType>(ItemType.IronOre, ItemType.IronBar, ItemType.Grass));
                    DropReplace.Add(new Tuple<ItemType, ItemType, ItemType>(ItemType.SilverOre, ItemType.SilverBar, ItemType.Grass));
                    DropReplace.Add(new Tuple<ItemType, ItemType, ItemType>(ItemType.GoldOre, ItemType.GoldBar, ItemType.Grass));
                    ActionObjects.Add(DropReplace);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Item, typeof(Items.ItemActions), "ReplaceAndDropItem", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.Stove:
                    ReturnSprite = GetItemSprite("stove", IT, Position);
                    ReturnSprite.orderNum = 75;
                    ReturnSprite.Size = new Vector2(75, 75);
                    ActionObjects = new List<object>();
                    ActionObjects.Add(Actions.Crafting.Type.Stove);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Menu.Craft), "DisplayCraft", ActionObjects));
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Menu.Craft), "DisplayCraft", ActionObjects));
                    break;
                #endregion

                #region plants
                case Item.ItemType.Sapling_Tree:
                    ReturnSprite = Items.Item.GetItemSprite("treegrow", IT, Position);
                    ReturnSprite.name = "tree";
                    ReturnSprite.Size = new Vector2(75, 75);
                    ReturnSprite.speed = 100;
                    ReturnSprite.AnimSprite = new Objects.AnimSprite(12, 11);
                    ReturnSprite.AnimSprite.action = true;
                    ReturnSprite.AnimSprite.currentFrame = 0;
                    ReturnSprite.AnimSprite.totalFrames = 127;
                    ReturnSprite.AnimSprite.StopOnEof = true;
                    break;

                case Item.ItemType.Seed_Tree:
                    ReturnSprite = GetItemSprite("sapling", IT, Position);
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Item, typeof(Items.ItemActions), "AddSapling", null));
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.Tree:
                    ReturnSprite = GetItemSprite("tree2", IT, Position);
                    break;

                case Item.ItemType.Grain:
                    ReturnSprite = GetItemSprite("grain", IT, Position);
                    ReturnSprite.collisionSound = Util.Global.ContentMan.Load<SoundEffect>("Sounds/shovel");
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    break;

                case Item.ItemType.Grass:
                    ReturnSprite = GetItemSprite("grass0", IT, Position);
                    ReturnSprite.orderNum = 1;
                    break;
                #endregion

                #region materials
                case Item.ItemType.Log:
                    ReturnSprite = GetItemSprite("log", IT, Position);
                    ReturnSprite.collisionSound = Util.Global.ContentMan.Load<SoundEffect>("Sounds/shovel");
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    break;

                case Item.ItemType.Oil:
                    ReturnSprite = GetItemSprite("oil", IT, Position);
                    ReturnSprite.collisionSound = Util.Global.ContentMan.Load<SoundEffect>("Sounds/pickup");
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    break;
                #endregion

                #region tiles
                case Item.ItemType.Wall:
                    ReturnSprite = GetItemSprite("wall", IT, Position);
                    ActionObjects.Add(IT);
                    ActionObjects.Add(true);
                    ActionObjects.Add("Sounds/ChopWood");
                    ActionObjects.Add(true);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Item, typeof(Items.ItemActions), "ReplaceTile", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.Plank:
                    ReturnSprite = GetItemSprite("floor2", IT, Position);
                    ActionObjects.Add(IT);
                    ActionObjects.Add(false);
                    ActionObjects.Add("Sounds/ChopWood");
                    ActionObjects.Add(true);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Item, typeof(Items.ItemActions), "ReplaceTile", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.Door:
                    ReturnSprite = Items.Item.GetItemSprite("NewDoor2", IT, Position);
                    ReturnSprite.AnimSprite = new Objects.AnimSprite(1, 4);
                    ReturnSprite.AnimSprite.actionOnSound = Util.Global.ContentMan.Load<SoundEffect>("Sounds/dooropen");
                    ReturnSprite.AnimSprite.actionOffSound = Util.Global.ContentMan.Load<SoundEffect>("Sounds/doorclose");
                    ReturnSprite.AnimSprite.StopOnEof = true;
                    ReturnSprite.clipping = true;
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Objects.Base), "FlipClip", ActionObjects));
                    break;

                case ItemType.Fountain:
                    ReturnSprite = Items.Item.GetItemSprite("waterfountain", IT, Position);
                    ReturnSprite.speed = 10;
                    ReturnSprite.AnimSprite = new Objects.AnimSprite(5, 6);
                    ReturnSprite.AnimSprite.action = false;
                    ReturnSprite.AnimSprite.StopOnEof = false;
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Update, typeof(Items.ItemActions), "ProximityAnimAction", ActionObjects));
                    break;
                #endregion

                #region items
                case Item.ItemType.Coin:
                    ReturnSprite = GetItemSprite("coin", IT, Position);
                    ReturnSprite.collisionSound = Util.Global.ContentMan.Load<SoundEffect>("Sounds/pickup");
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    break;



                case Item.ItemType.Bread:
                    ReturnSprite = GetItemSprite("bread", IT, Position);
                    break;





                case Item.ItemType.Health:
                    ReturnSprite = GetItemSprite("heart_full", IT, Position);
                    ReturnSprite.collisionSound = Util.Global.ContentMan.Load<SoundEffect>("Sounds/gulp");
                    ReturnSprite.Size = new Vector2(10, 10);
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "HealthPickup", ActionObjects));
                    break;

                case Item.ItemType.Torch:
                    ReturnSprite = Items.Item.GetItemSprite("animatedtorch", IT, Position);
                    ReturnSprite.AnimSprite = new Objects.AnimSprite(9, 1);
                    ReturnSprite.AnimSprite.action = true;
                    ReturnSprite.AnimSprite.StopOnEof = false;
                    ReturnSprite.clipping = false;
                    ReturnSprite.LightSourceDistance = 30f;
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                #endregion
            }
            return ReturnSprite;
        }

        public static Objects.Sprite2d GetItemSprite(string Texture2dName, Items.Item.ItemType itemType, Vector2 Position)
        {
            Items.Item.ItemType IT = itemType;

            Objects.Sprite2d SpriteItem = new Objects.Sprite2d(Util.Global.AllTexture2D.Where(x => x.Name == Texture2dName).FirstOrDefault(), Texture2dName, true, Position, new Vector2(32, 32), 0, Objects.Base.ControlType.None);
            SpriteItem.orderNum = 100;
            SpriteItem.SpriteType = Objects.Base.Type.Item;
            SpriteItem.Item.Type = itemType;
            
            return SpriteItem;
        }
    }
}
