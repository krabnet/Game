using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Items
{
    public static class Item
    {
        public enum ItemState { Null, Inventory, Hand, Chest, Hotbar }

        public enum ItemClass { Item, Tile, Tool, Prop }

        public enum ItemType 
        {
            TestChest, Spawner,
            None, Grass, Wall, Wall2, Wall3, Plank, Floor, Floor2, Door, Gravel, Water, Rock, Sand, Path, Glass, Lava, Fence,
            BuildWall, BuildFence, 
            Coin, Chest, ChestLoot, Torch, SpotLight, Log, Bread, Fish, Oil, Health, Bed, Cloth,
            Pickaxe, Hammer, Axe, Shovel, FishPoll,
            Totem, CaveEntrance, Ladder, dungeonentrance,
            Caldron, WorkBench, Anvil, Tent, Shop, Stove, WarpPad, WarpPadOn, GreenHouse,
            IronBar, IronOre, SilverBar, SilverOre, GoldBar, GoldOre,
            SeedGrain, GardenGrain, Grain,
            SeedTree, GardenTree, Tree, TreeStump,
            SeedHealth, GardenHealth, BerryHealth, PotionHealth,
            SeedStrength, GardenStrength, BerryStrength, PotionStrength,
            SeedSpeed, GardenSpeed, BerrySpeed, PotionSpeed,
            SeedDexterity, GardenDexterity, BerryDexterity, PotionDexterity,
            FountainSpeed, FountainHealth,
            Bush, Bush2, RockSmall, Flower, Flower2, 
            Shelf1, Rug1, Rug2, ClayPot, ClayPot2, ClayPot3, Barrel, Chair, HayStack, Candle, Web, Pillar,
            Remove
        };

        public static Game.Objects.Sprite2d GetItemByType(Item.ItemType IT, Vector2 Position)
        {
            Actions.ActionCall AC = new Actions.ActionCall(Actions.ActionType.Item, null, null, null);
            List<Object> ActionObjects = new List<object>();
            ActionObjects.Add(IT);
            ActionObjects.Add(Position);
            AC = new Actions.ActionCall(Actions.ActionType.Item, Type.GetType("Game.Items.Types." + IT.ToString()), "Get", ActionObjects);
            if(AC.Type == null)
            {
                Util.Base.Log("Non-Specific Sprite Type! - " + IT.ToString());
                Objects.Sprite2d ReturnSprite = new Objects.Sprite2d();
                ReturnSprite = Items.Item.GetItemSprite(IT.ToString(), IT, Position);
                Texture2D model = Util.Global.AllTexture2D.Where(x => x.Name == IT.ToString().ToLower()).FirstOrDefault();
                ReturnSprite.Size = new Vector2(model.Width, model.Height);
                ReturnSprite.orderNum = 5;
                ReturnSprite.Item.Class = ItemClass.Prop;
                return ReturnSprite;
            }
            else
            { 
                MethodInfo mi = AC.Type.GetMethod(AC.actionMethodName);
                object classInstance = Activator.CreateInstance(AC.Type);
                return (Game.Objects.Sprite2d)mi.Invoke(classInstance, AC.parameters.ToArray());
            }
        }

        public static Objects.Sprite2d GetItemSprite(string Texture2dName, Items.Item.ItemType itemType, Vector2 Position)
        {
            Items.Item.ItemType IT = itemType;
            Objects.Sprite2d SpriteItem = new Objects.Sprite2d(Texture2dName, itemType.ToString(), true, Position, new Vector2(32, 32), 0, Objects.Base.ControlType.None);
            SpriteItem.orderNum = 499;
            SpriteItem.SpriteType = Objects.Base.Type.Item;
            SpriteItem.Item.Type = itemType;
            SpriteItem.Item.Class = ItemClass.Item;
            SpriteItem.Item.Number = 1;
            SpriteItem.Item.ItemSlot = 0;
            if (Position == Util.Global.DefaultPosition)
            {
                SpriteItem.active = false;
            }


            return SpriteItem;
        }

        public static Items.Item.ItemType GetRandomProp()
        {
            List<Items.Item.ItemType> include = new List<ItemType>();
            include.Add(ItemType.Barrel);
            include.Add(ItemType.ClayPot);
            include.Add(ItemType.ClayPot2);
            include.Add(ItemType.ClayPot3);
            include.Add(ItemType.Chair);
            include.Add(ItemType.Shelf1);
            include.Add(ItemType.Rug1);
            include.Add(ItemType.Rug2);

            return include.OrderBy(a => Guid.NewGuid()).FirstOrDefault();
        }

        public static Items.Item.ItemType GetShopProp()
        {
            List<Items.Item.ItemType> include = new List<ItemType>();
            include.Add(ItemType.Barrel);
            include.Add(ItemType.ClayPot);
            include.Add(ItemType.ClayPot2);
            include.Add(ItemType.ClayPot3);
            include.Add(ItemType.Chair);
            return include.OrderBy(a => Guid.NewGuid()).FirstOrDefault();
        }
    }
}
