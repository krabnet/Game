using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Items;

namespace Game.Items
{
    class ItemOLD
    {
        /*
        public static Game.Objects.Sprite2d GetItemByType(Item.ItemType IT, Vector2 Position)
        {
            Objects.Sprite2d ReturnSprite = new Objects.Sprite2d();
            List<Object> ActionObjects = new List<object>();
            List<Tuple<ItemType, ItemType, ItemType>> DropReplace = new List<Tuple<ItemType, ItemType, ItemType>>();
            Dictionary<ItemType, ItemType> ItemList = new Dictionary<ItemType, ItemType>();
            Actions.ActionCall AC = new Actions.ActionCall(Actions.ActionType.Item, null, null, null);
            switch (IT)
            {
                #region warp
                case Item.ItemType.CaveEntrance:



                    AC = new Actions.ActionCall(Actions.ActionType.Item, Type.GetType("Game.Items.Types." + IT.ToString()), "Get", ActionObjects);

                    ReturnSprite = GetItemSprite("CaveEntrance", IT, Position);
                    ReturnSprite.Item.Description = "Going Down?";
                    ReturnSprite.Size = new Vector2(600, 402) / 6;
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    ReturnSprite.orderNum = 5;
                    ActionObjects = new List<object>();
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Actions.Cave), "EnterCave", ActionObjects));

                    break;

                case Item.ItemType.WarpPad:
                    ActionObjects.Add(IT);
                    ActionObjects.Add(Position);
                    AC = new Actions.ActionCall(Actions.ActionType.Item, Type.GetType("Game.Items.Types." + IT.ToString()), "Get", ActionObjects);
                    return CallMethod(AC);

                    //ReturnSprite = GetItemSprite("pad", IT, Position);
                    //ReturnSprite.Item.Description = "Travel";
                    //ReturnSprite.Size = new Vector2(32, 32);
                    //ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    //ReturnSprite.orderNum = 5;
                    //ActionObjects.Add(ReturnSprite);
                    //ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "ActivateWarp", ActionObjects));
                    break;

                case Item.ItemType.WarpPadOn:
                    ReturnSprite = GetItemSprite("padon", IT, Position);
                    ReturnSprite.Item.Description = Util.Base.GetRandomName();
                    ReturnSprite.Size = new Vector2(32, 32);
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    ReturnSprite.orderNum = 5;
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Menu.Warp), "ShowWarp", ActionObjects));
                    break;

                #endregion

                #region ores
                case Item.ItemType.IronBar:
                    ReturnSprite = GetItemSprite("ironbar", IT, Position);
                    ReturnSprite.Item.Description = ReturnSprite.modelname;
                    ReturnSprite.Item.Cost = 5;
                    ReturnSprite.collisionSound = "Sounds/shovel";
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    break;

                case Item.ItemType.IronOre:
                    ReturnSprite = GetItemSprite("IronOre", IT, Position);
                    ReturnSprite.Item.Description = ReturnSprite.modelname;
                    ReturnSprite.orderNum = 1;
                    ReturnSprite.SpriteType = Objects.Base.Type.Tile;
                    break;

                case Item.ItemType.SilverBar:
                    ReturnSprite = Items.Item.GetItemSprite("silverbar", IT, Position);
                    ReturnSprite.Item.Description = ReturnSprite.modelname;
                    ReturnSprite.Item.Cost = 3;
                    ReturnSprite.collisionSound = "Sounds/shovel";
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    break;

                case Item.ItemType.SilverOre:
                    ReturnSprite = Items.Item.GetItemSprite("SilverOre", IT, Position);
                    ReturnSprite.Item.Description = ReturnSprite.modelname;
                    ReturnSprite.orderNum = 1;
                    ReturnSprite.SpriteType = Objects.Base.Type.Tile;
                    break;

                case Item.ItemType.GoldBar:
                    ReturnSprite = Items.Item.GetItemSprite("goldbar", IT, Position);
                    ReturnSprite.Item.Description = ReturnSprite.modelname;
                    ReturnSprite.Item.Cost = 7;
                    ReturnSprite.collisionSound = "Sounds/shovel";
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    break;

                case Item.ItemType.GoldOre:
                    ReturnSprite = Items.Item.GetItemSprite("GoldOre", IT, Position);
                    ReturnSprite.Item.Description = ReturnSprite.modelname;
                    ReturnSprite.orderNum = 1;
                    ReturnSprite.SpriteType = Objects.Base.Type.Tile;
                    break;
                #endregion

                #region tools
                case Item.ItemType.Axe:
                    ReturnSprite = Items.Item.GetItemSprite("axe", IT, Position);
                    ReturnSprite.Item.Description = "Chop Chop";
                    ReturnSprite.Item.Stack = false;
                    ReturnSprite.collisionSound = "Sounds/shovel";
                    DropReplace = new List<Tuple<ItemType, ItemType, ItemType>>();
                    DropReplace.Add(new Tuple<ItemType, ItemType, ItemType>(ItemType.Tree, ItemType.Log, ItemType.None));
                    DropReplace.Add(new Tuple<ItemType, ItemType, ItemType>(ItemType.Grain, ItemType.Grain, ItemType.None));
                    ActionObjects.Add(DropReplace);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Item, typeof(Items.ItemActions), "ReplaceAndDropItem", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.Hammer:
                    ReturnSprite = Items.Item.GetItemSprite("hammer", IT, Position);
                    ReturnSprite.Item.Description = "Bang Bang";
                    ReturnSprite.Item.Stack = false;
                    ReturnSprite.collisionSound = "Sounds/shovel";
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    ActionObjects = new List<object>();
                    ItemList = new Dictionary<ItemType, ItemType>();
                    ItemList.Add(ItemType.Wall, ItemType.Grass);
                    ItemList.Add(ItemType.Plank, ItemType.Grass);
                    ItemList.Add(ItemType.Glass, ItemType.Grass);
                    ItemList.Add(ItemType.Fence, ItemType.Grass);
                    ActionObjects.Add(ItemList);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Item, typeof(Items.ItemActions), "PickupTile", ActionObjects));
                    break;

                case Item.ItemType.Pickaxe:
                    ReturnSprite = Items.Item.GetItemSprite("pick", IT, Position);
                    ReturnSprite.Item.Description = "Metal Collector";
                    ReturnSprite.Item.Stack = false;
                    ReturnSprite.collisionSound = "Sounds/shovel";
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

                case Item.ItemType.Shovel:
                    ReturnSprite = Items.Item.GetItemSprite("shovel", IT, Position);
                    ReturnSprite.Item.Description = "Dig Dig";
                    ReturnSprite.Item.Stack = false;
                    ReturnSprite.collisionSound = "Sounds/shovel";
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    ActionObjects = new List<object>();
                    ItemList = new Dictionary<ItemType, ItemType>();
                    ItemList.Add(ItemType.Sand, ItemType.Grass);
                    ItemList.Add(ItemType.Flower, ItemType.None);
                    ItemList.Add(ItemType.Flower2, ItemType.None);
                    ItemList.Add(ItemType.Bush, ItemType.None);
                    ItemList.Add(ItemType.Bush2, ItemType.None);
                    ItemList.Add(ItemType.RockSmall, ItemType.None);
                    ActionObjects.Add(ItemList);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Item, typeof(Items.ItemActions), "PickupTile", ActionObjects));
                    break;

                case Item.ItemType.FishPoll:
                    ReturnSprite = Items.Item.GetItemSprite("FishPole", IT, Position);
                    ReturnSprite.Item.Description = "Fish Cathcer";
                    ReturnSprite.Item.Stack = false;
                    ActionObjects.Add(ReturnSprite);
                    break;

                case Item.ItemType.Stove:
                    ReturnSprite = Items.Item.GetItemSprite("stove", IT, Position);
                    ReturnSprite.Item.Description = "Cooking";
                    ReturnSprite.Item.BackGroundType = ItemType.Floor;
                    ReturnSprite.Item.Stack = false;
                    ReturnSprite.Item.Cost = 25;
                    ReturnSprite.orderNum = 75;
                    ReturnSprite.Size = new Vector2(75, 75);
                    ReturnSprite.Item.OriSize = new Vector2(75, 75);
                    ActionObjects = new List<object>();
                    ActionObjects.Add(Actions.Crafting.Type.Stove);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Menu.Craft), "DisplayCraft", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.Caldron:
                    ReturnSprite = Items.Item.GetItemSprite("caldron", IT, Position);
                    ReturnSprite.Item.Description = "Brew";
                    ReturnSprite.Item.BackGroundType = ItemType.Floor;
                    ReturnSprite.Item.Stack = false;
                    ReturnSprite.Item.Cost = 25;
                    ReturnSprite.orderNum = 75;
                    ReturnSprite.Size = new Vector2(50, 50);
                    ReturnSprite.Item.OriSize = new Vector2(50, 50);
                    ActionObjects = new List<object>();
                    ActionObjects.Add(Actions.Crafting.Type.Caldron);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Menu.Craft), "DisplayCraft", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.WorkBench:
                    ReturnSprite = Items.Item.GetItemSprite("workbench", IT, Position);
                    ReturnSprite.Item.Description = "Make Stuff";
                    ReturnSprite.Item.BackGroundType = ItemType.Floor;
                    ReturnSprite.Item.Stack = false;
                    ReturnSprite.Item.Cost = 15;
                    ReturnSprite.orderNum = 75;
                    ReturnSprite.Size = new Vector2(50, 50);
                    ReturnSprite.Item.OriSize = new Vector2(50, 50);
                    ActionObjects = new List<object>();
                    ActionObjects.Add(Actions.Crafting.Type.Workbench);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Menu.Craft), "DisplayCraft", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.GreenHouse:
                    ReturnSprite = Items.Item.GetItemSprite("greenhouse", IT, Position);
                    ReturnSprite.Item.Description = "Seedlings";
                    ReturnSprite.Item.BackGroundType = ItemType.Floor;
                    ReturnSprite.Item.Stack = false;
                    ReturnSprite.Item.Cost = 15;
                    ReturnSprite.orderNum = 75;
                    ReturnSprite.Size = new Vector2(960 / 15, 539 / 15);
                    ReturnSprite.Item.OriSize = ReturnSprite.Size;
                    ActionObjects = new List<object>();
                    ActionObjects.Add(Actions.Crafting.Type.GreenHouse);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Menu.Craft), "DisplayCraft", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.Anvil:
                    ReturnSprite = Items.Item.GetItemSprite("anvil", IT, Position);
                    ReturnSprite.Item.Description = "Tools";
                    ReturnSprite.Item.Stack = false;
                    ReturnSprite.Item.BackGroundType = ItemType.Floor;
                    ReturnSprite.Item.Cost = 25;
                    ReturnSprite.orderNum = 75;
                    ReturnSprite.Size = new Vector2(50, 50);
                    ReturnSprite.Item.OriSize = new Vector2(50, 50);
                    ActionObjects = new List<object>();
                    ActionObjects.Add(Actions.Crafting.Type.Anvil);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Menu.Craft), "DisplayCraft", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;


                #endregion

                #region plants
                #region Health
                case Item.ItemType.SeedHealth:
                    ReturnSprite = Items.Item.GetItemSprite("seed", IT, Position);
                    ReturnSprite.Item.Description = "Red Berry Seed";
                    ReturnSprite.Size = new Vector2(32, 32);
                    ReturnSprite.color = Color.Red * .8f;
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ActionObjects.Add(Item.ItemType.GardenHealth);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Items.ItemActions), "AddSapling", ActionObjects));
                    break;

                case Item.ItemType.GardenHealth:
                    ReturnSprite = Items.Item.GetItemSprite("GrowPlant", IT, Position);
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    ReturnSprite.AnimSprite = new Objects.AnimSprite(5, 1);
                    ReturnSprite.AnimSprite.StopOnEof = true;
                    ReturnSprite.AnimSprite.action = true;
                    ReturnSprite.speed = 600;
                    if (Util.Global.Weather == Actions.Enviro.Weather.Rain)
                    { ReturnSprite.speed = 350; }
                    ReturnSprite.clipping = false;
                    ReturnSprite.SpriteType = Objects.Base.Type.Tile;
                    ActionObjects.Add(ReturnSprite);
                    ActionObjects.Add(ItemType.BerryHealth);
                    ReturnSprite.AnimSprite.ActionCallEOF = new Actions.ActionCall(Actions.ActionType.Item, typeof(Items.ItemActions), "GardenUpdate", ActionObjects);
                    break;

                case Item.ItemType.BerryHealth:
                    ReturnSprite = Items.Item.GetItemSprite("berry", IT, Position);
                    ReturnSprite.Item.Description = "Red Berry";
                    ReturnSprite.collisionSound = "Sounds/pop";
                    ReturnSprite.Item.Cost = 2;
                    ReturnSprite.Size = new Vector2(24, 24);
                    ReturnSprite.color = Color.Red * .8f;
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;
                #endregion

                #region strength
                case Item.ItemType.SeedStrength:
                    ReturnSprite = Items.Item.GetItemSprite("seed", IT, Position);
                    ReturnSprite.Item.Description = "Purple Berry Seed";
                    ReturnSprite.Size = new Vector2(32, 32);
                    ReturnSprite.color = Color.Purple * .8f;
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ActionObjects.Add(Item.ItemType.GardenStrength);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Items.ItemActions), "AddSapling", ActionObjects));
                    break;

                case Item.ItemType.GardenStrength:
                    ReturnSprite = Items.Item.GetItemSprite("GrowPlant", IT, Position);
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    ReturnSprite.AnimSprite = new Objects.AnimSprite(5, 1);
                    ReturnSprite.AnimSprite.StopOnEof = true;
                    ReturnSprite.AnimSprite.action = true;
                    ReturnSprite.speed = 600;
                    if (Util.Global.Weather == Actions.Enviro.Weather.Rain)
                    { ReturnSprite.speed = 350; }
                    ReturnSprite.clipping = false;
                    ReturnSprite.SpriteType = Objects.Base.Type.Tile;
                    ActionObjects.Add(ReturnSprite);
                    ActionObjects.Add(ItemType.BerryStrength);
                    ReturnSprite.AnimSprite.ActionCallEOF = new Actions.ActionCall(Actions.ActionType.Item, typeof(Items.ItemActions), "GardenUpdate", ActionObjects);
                    break;

                case Item.ItemType.BerryStrength:
                    ReturnSprite = Items.Item.GetItemSprite("berry", IT, Position);
                    ReturnSprite.Item.Description = "Purple Berry";
                    ReturnSprite.collisionSound = "Sounds/pop";
                    ReturnSprite.Item.Cost = 2;
                    ReturnSprite.Size = new Vector2(24, 24);
                    ReturnSprite.color = Color.Purple * .8f;
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;
                #endregion

                #region Speed
                case Item.ItemType.SeedSpeed:
                    ReturnSprite = Items.Item.GetItemSprite("seed", IT, Position);
                    ReturnSprite.Item.Description = "Green Berry Seed";
                    ReturnSprite.Size = new Vector2(32, 32);
                    ReturnSprite.color = Color.Green * .8f;
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ActionObjects.Add(Item.ItemType.GardenSpeed);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Items.ItemActions), "AddSapling", ActionObjects));
                    break;

                case Item.ItemType.GardenSpeed:
                    ReturnSprite = Items.Item.GetItemSprite("GrowPlant", IT, Position);
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    ReturnSprite.AnimSprite = new Objects.AnimSprite(5, 1);
                    ReturnSprite.AnimSprite.StopOnEof = true;
                    ReturnSprite.AnimSprite.action = true;
                    ReturnSprite.speed = 600;
                    if (Util.Global.Weather == Actions.Enviro.Weather.Rain)
                    { ReturnSprite.speed = 350; }
                    ReturnSprite.clipping = false;
                    ReturnSprite.SpriteType = Objects.Base.Type.Tile;
                    ActionObjects.Add(ReturnSprite);
                    ActionObjects.Add(ItemType.BerrySpeed);
                    ReturnSprite.AnimSprite.ActionCallEOF = new Actions.ActionCall(Actions.ActionType.Item, typeof(Items.ItemActions), "GardenUpdate", ActionObjects);
                    break;

                case Item.ItemType.BerrySpeed:
                    ReturnSprite = Items.Item.GetItemSprite("berry", IT, Position);
                    ReturnSprite.Item.Description = "Green Berry";
                    ReturnSprite.collisionSound = "Sounds/pop";
                    ReturnSprite.Item.Cost = 2;
                    ReturnSprite.Size = new Vector2(24, 24);
                    ReturnSprite.color = Color.Green * .8f;
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;
                #endregion

                #region Dexterity
                case Item.ItemType.SeedDexterity:
                    ReturnSprite = Items.Item.GetItemSprite("seed", IT, Position);
                    ReturnSprite.Item.Description = "Orange Berry Seed";
                    ReturnSprite.Size = new Vector2(32, 32);
                    ReturnSprite.color = Color.Orange * .8f;
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ActionObjects.Add(Item.ItemType.GardenDexterity);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Items.ItemActions), "AddSapling", ActionObjects));
                    break;

                case Item.ItemType.GardenDexterity:
                    ReturnSprite = Items.Item.GetItemSprite("GrowPlant", IT, Position);
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    ReturnSprite.AnimSprite = new Objects.AnimSprite(5, 1);
                    ReturnSprite.AnimSprite.StopOnEof = true;
                    ReturnSprite.AnimSprite.action = true;
                    ReturnSprite.speed = 600;
                    if (Util.Global.Weather == Actions.Enviro.Weather.Rain)
                    { ReturnSprite.speed = 350; }
                    ReturnSprite.clipping = false;
                    ReturnSprite.SpriteType = Objects.Base.Type.Tile;
                    ActionObjects.Add(ReturnSprite);
                    ActionObjects.Add(ItemType.BerryDexterity);
                    ReturnSprite.AnimSprite.ActionCallEOF = new Actions.ActionCall(Actions.ActionType.Item, typeof(Items.ItemActions), "GardenUpdate", ActionObjects);
                    break;

                case Item.ItemType.BerryDexterity:
                    ReturnSprite = Items.Item.GetItemSprite("berry", IT, Position);
                    ReturnSprite.Item.Description = "Orange Berry";
                    ReturnSprite.collisionSound = "Sounds/pop";
                    ReturnSprite.Item.Cost = 2;
                    ReturnSprite.Size = new Vector2(24, 24);
                    ReturnSprite.color = Color.Orange * .8f;
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;
                #endregion

                #region tree
                case Item.ItemType.SeedTree:
                    ReturnSprite = Items.Item.GetItemSprite("sapling", IT, Position);
                    ReturnSprite.Item.Description = "Tree Seed";
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ActionObjects.Add(Item.ItemType.GardenTree);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Items.ItemActions), "AddSapling", ActionObjects));
                    break;

                case Item.ItemType.GardenTree:
                    ReturnSprite = Items.Item.GetItemSprite("GrowPlant", IT, Position);
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    ReturnSprite.AnimSprite = new Objects.AnimSprite(5, 1);
                    ReturnSprite.AnimSprite.StopOnEof = true;
                    ReturnSprite.AnimSprite.action = true;
                    ReturnSprite.speed = 600;
                    if (Util.Global.Weather == Actions.Enviro.Weather.Rain)
                    { ReturnSprite.speed = 350; }
                    ReturnSprite.clipping = false;
                    ReturnSprite.SpriteType = Objects.Base.Type.Tile;
                    ActionObjects.Add(ReturnSprite);
                    ActionObjects.Add(ItemType.Tree);
                    ReturnSprite.AnimSprite.ActionCallEOF = new Actions.ActionCall(Actions.ActionType.Item, typeof(Items.ItemActions), "GardenUpdate", ActionObjects);
                    break;

                case Item.ItemType.Tree:
                    ReturnSprite = Items.Item.GetItemSprite("tree3", IT, Position);
                    ReturnSprite.Size = new Vector2(592 / 6, 640 / 6);
                    ReturnSprite.Position = Vector2.Subtract(ReturnSprite.Position, Vector2.Multiply(ReturnSprite.Size, .5F));
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    break;

                case Item.ItemType.TreeStump:
                    ReturnSprite = Items.Item.GetItemSprite("tree3stump", IT, Position);
                    ReturnSprite.Size = new Vector2(592 / 6, 640 / 6);
                    ReturnSprite.Position = Vector2.Subtract(ReturnSprite.Position, Vector2.Multiply(ReturnSprite.Size, .5F));
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    break;

                #endregion

                #region grain
                case Item.ItemType.SeedGrain:
                    ReturnSprite = Items.Item.GetItemSprite("seed", IT, Position);
                    ReturnSprite.Item.Description = "Grain Seed";
                    ReturnSprite.Size = new Vector2(32, 32);
                    ReturnSprite.color = Color.Tan * .8f;
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ActionObjects.Add(Item.ItemType.GardenGrain);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Items.ItemActions), "AddSapling", ActionObjects));
                    break;

                case Item.ItemType.GardenGrain:
                    ReturnSprite = Items.Item.GetItemSprite("GrowPlant", IT, Position);
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    ReturnSprite.AnimSprite = new Objects.AnimSprite(5, 1);
                    ReturnSprite.AnimSprite.StopOnEof = true;
                    ReturnSprite.AnimSprite.action = true;
                    ReturnSprite.speed = 600;
                    if (Util.Global.Weather == Actions.Enviro.Weather.Rain)
                    { ReturnSprite.speed = 350; }
                    ReturnSprite.clipping = false;
                    ReturnSprite.SpriteType = Objects.Base.Type.Tile;
                    ActionObjects.Add(ReturnSprite);
                    ActionObjects.Add(ItemType.Grain);
                    ReturnSprite.AnimSprite.ActionCallEOF = new Actions.ActionCall(Actions.ActionType.Item, typeof(Items.ItemActions), "GardenUpdate", ActionObjects);
                    break;

                case Item.ItemType.Grain:
                    ReturnSprite = Items.Item.GetItemSprite("grain", IT, Position);
                    ReturnSprite.Item.Description = "Grain";
                    ReturnSprite.collisionSound = "Sounds/pop";
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    ReturnSprite.Item.Cost = 2;
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;
                #endregion
                #endregion

                #region materials
                case Item.ItemType.Log:
                    ReturnSprite = Items.Item.GetItemSprite("log", IT, Position);
                    ReturnSprite.Item.Description = "It's Log!";
                    ReturnSprite.Item.Cost = 1;
                    ReturnSprite.collisionSound = "Sounds/shovel";
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.Oil:
                    ReturnSprite = Items.Item.GetItemSprite("oil", IT, Position);
                    ReturnSprite.Item.Description = "Fuel";
                    ReturnSprite.Item.Cost = 3;
                    ReturnSprite.collisionSound = "Sounds/pickup";
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.Cloth:
                    ReturnSprite = Items.Item.GetItemSprite("cloth", IT, Position);
                    ReturnSprite.Item.Description = "Fibers";
                    ReturnSprite.Item.Cost = 5;
                    ReturnSprite.collisionSound = "Sounds/pickup";
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;
                #endregion

                #region tiles
                case Item.ItemType.Grass:
                    ReturnSprite = Items.Item.GetItemSprite("grass0", IT, Position);
                    ReturnSprite.orderNum = 0;
                    break;

                case Item.ItemType.Gravel:
                    ReturnSprite = Items.Item.GetItemSprite("gravel", IT, Position);
                    ReturnSprite.orderNum = 0;
                    break;

                case Item.ItemType.Path:
                    ReturnSprite = Items.Item.GetItemSprite("path", IT, Position);
                    ReturnSprite.orderNum = 0;
                    break;

                case Item.ItemType.Sand:
                    ReturnSprite = Items.Item.GetItemSprite("sand", IT, Position);
                    ReturnSprite.orderNum = 1;
                    break;

                case Item.ItemType.Water:
                    ReturnSprite = Items.Item.GetItemSprite("water", IT, Position);
                    ReturnSprite.orderNum = 1;
                    ReturnSprite.clipping = true;
                    ReturnSprite.effectType = Objects.Base.EffectType.Ripple;
                    break;

                case Item.ItemType.Lava:
                    ReturnSprite = Items.Item.GetItemSprite("lava", IT, Position);
                    ReturnSprite.orderNum = 1;
                    ReturnSprite.clipping = true;
                    ReturnSprite.LightSourceDistance = Util.Global.TorchLightDistance;
                    ReturnSprite.effectType = Objects.Base.EffectType.Ripple;
                    break;

                case Item.ItemType.Rock:
                    ReturnSprite = Items.Item.GetItemSprite("rock1", IT, Position);
                    ReturnSprite.Item.BackGroundType = ItemType.Gravel;
                    ReturnSprite.orderNum = 1;
                    ReturnSprite.clipping = true;
                    break;

                case Item.ItemType.Glass:
                    ReturnSprite = Items.Item.GetItemSprite("glass", IT, Position);
                    ReturnSprite.Item.Description = "It's clear to me";
                    ReturnSprite.orderNum = 1;
                    ReturnSprite.clipping = true;
                    ActionObjects.Add(IT);
                    ActionObjects.Add(true);
                    ActionObjects.Add("Sounds/ChopWood");
                    ActionObjects.Add(true);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Item, typeof(Items.ItemActions), "ReplaceTile", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.Wall:
                    ReturnSprite = Items.Item.GetItemSprite("wall", IT, Position);
                    ReturnSprite.orderNum = 2;
                    ReturnSprite.clipping = true;
                    break;

                case Item.ItemType.Wall2:
                    ReturnSprite = Items.Item.GetItemSprite("wall2", IT, Position);
                    ReturnSprite.orderNum = 6;
                    ReturnSprite.clipping = true;
                    break;

                case Item.ItemType.Wall3:
                    ReturnSprite = Items.Item.GetItemSprite("wall3", IT, Position);
                    ReturnSprite.orderNum = 6;
                    ReturnSprite.clipping = true;
                    break;

                case Item.ItemType.Fence:
                    ReturnSprite = Items.Item.GetItemSprite("fence", IT, Position);
                    ReturnSprite.active = true;
                    ReturnSprite.Item.BackGroundType = Item.ItemType.Grass;
                    ReturnSprite.orderNum = 1;
                    ReturnSprite.clipping = true;
                    break;

                case Item.ItemType.BuildWall:
                    ReturnSprite = Items.Item.GetItemSprite("buildwall", IT, Position);
                    ReturnSprite.Item.Description = "Build Wall";
                    ReturnSprite.active = false;
                    ActionObjects.Add(IT);
                    ActionObjects.Add(true);
                    ActionObjects.Add("Sounds/ChopWood");
                    ActionObjects.Add(true);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Item, typeof(Items.ItemActions), "ReplaceTile", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.BuildFence:
                    ReturnSprite = Items.Item.GetItemSprite("buildfence", IT, Position);
                    ReturnSprite.Item.Description = "Build Fence";
                    ActionObjects.Add(IT);
                    ActionObjects.Add(true);
                    ActionObjects.Add("Sounds/ChopWood");
                    ActionObjects.Add(true);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Item, typeof(Items.ItemActions), "ReplaceTile", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.Floor:
                    ReturnSprite = Items.Item.GetItemSprite("floor2", IT, Position);
                    ReturnSprite.orderNum = 1;
                    break;

                case Item.ItemType.Floor2:
                    ReturnSprite = Items.Item.GetItemSprite("Floor3", IT, Position);
                    ReturnSprite.orderNum = 1;
                    break;

                case Item.ItemType.Plank:
                    ReturnSprite = Items.Item.GetItemSprite("floor2", IT, Position);
                    ReturnSprite.Item.Description = "Build Floor";
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
                    ReturnSprite.Item.BackGroundType = ItemType.Floor;
                    ReturnSprite.AnimSprite = new Objects.AnimSprite(1, 4);
                    ReturnSprite.AnimSprite.actionOnSound = "Sounds/dooropen";
                    ReturnSprite.AnimSprite.actionOffSound = "Sounds/doorclose";
                    ReturnSprite.AnimSprite.StopOnEof = true;
                    ReturnSprite.speed = 5;
                    ReturnSprite.clipping = true;
                    ReturnSprite.SpriteType = Objects.Base.Type.Tile;
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Items.ItemActions), "FlipClip", ActionObjects));
                    break;

                case ItemType.FountainSpeed:
                    ReturnSprite = Item.GetItemSprite("waterfountain", IT, Position);
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    ReturnSprite.speed = 5;
                    ReturnSprite.color = Color.Green;
                    ReturnSprite.Size = new Vector2(64, 64);
                    ReturnSprite.AnimSprite = new Objects.AnimSprite(5, 6);
                    ReturnSprite.AnimSprite.action = false;
                    ReturnSprite.AnimSprite.StopOnEof = false;
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Update, typeof(Items.ItemActions), "ProximityAnimAction", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(Util.Global.Hero);
                    ActionObjects.Add(Actions.Buff.BuffType.Speed);
                    ActionObjects.Add(5);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Actions.Buff), "AddBuff", ActionObjects));
                    break;

                case ItemType.FountainHealth:
                    ReturnSprite = Items.Item.GetItemSprite("waterfountain", IT, Position);
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    ReturnSprite.speed = 5;
                    ReturnSprite.color = Color.Red;
                    ReturnSprite.Size = new Vector2(64, 64);
                    ReturnSprite.AnimSprite = new Objects.AnimSprite(5, 6);
                    ReturnSprite.AnimSprite.action = false;
                    ReturnSprite.AnimSprite.StopOnEof = false;
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Update, typeof(Items.ItemActions), "ProximityAnimAction", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(Util.Global.Hero);
                    ActionObjects.Add(Actions.Buff.BuffType.Health);
                    ActionObjects.Add(5);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Actions.Buff), "AddBuff", ActionObjects));
                    break;
                #endregion

                #region items
                case Item.ItemType.Ladder:
                    ReturnSprite = Items.Item.GetItemSprite("CaveEntrance", IT, Position);
                    ReturnSprite.Item.Description = "Going Up?";
                    ReturnSprite.Size = new Vector2(600, 402) / 6;
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    ReturnSprite.orderNum = 5;
                    break;

                case Item.ItemType.Bed:
                    ReturnSprite = Items.Item.GetItemSprite("bed", IT, Position);
                    ReturnSprite.Item.Description = "Snooze";
                    ReturnSprite.Item.Cost = 50;
                    ReturnSprite.Item.Stack = false;
                    ReturnSprite.Size = new Vector2(32, 64);
                    ReturnSprite.Item.OriSize = ReturnSprite.Size;
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    ReturnSprite.orderNum = 5;
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Items.ItemActions), "Sleep", null));
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.Coin:
                    ReturnSprite = Items.Item.GetItemSprite("coin", IT, Position);
                    ReturnSprite.Item.Description = "Moola";
                    ReturnSprite.collisionSound = "Sounds/pickup";
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "CollisionPickup", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.Bread:
                    ReturnSprite = Items.Item.GetItemSprite("bread", IT, Position);
                    ReturnSprite.Item.Description = "Eat me";
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ActionObjects.Add(Util.Global.Hero);
                    ActionObjects.Add(Actions.Buff.BuffType.Feeding);
                    ActionObjects.Add(100);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Item, typeof(Items.ItemActions), "ConsumeItemObjectAndBuff", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.Fish:
                    ReturnSprite = Items.Item.GetItemSprite("fish", IT, Position);
                    ReturnSprite.Item.Description = "Eat me";
                    ReturnSprite.color = Color.Green;
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ActionObjects.Add(Util.Global.Hero);
                    ActionObjects.Add(Actions.Buff.BuffType.Feeding);
                    ActionObjects.Add(100);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Item, typeof(Items.ItemActions), "ConsumeItemObjectAndBuff", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.Totem:
                    ReturnSprite = Items.Item.GetItemSprite("totem", IT, Position);
                    ReturnSprite.Item.Description = "Knowledge";
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    ReturnSprite.Size = new Vector2(477, 640) / 6;
                    ReturnSprite.LightIgnor = false;
                    //ReturnSprite.text = "test test";
                    ReturnSprite.color = Util.Colors.GetRandomWhiteScale();
                    ReturnSprite.boxColor = Util.Colors.GetRandomBlueScale();
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ActionObjects.Add(ItemActions.GetTotemText());
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseAny, typeof(Items.ItemActions), "SayTotemText", ActionObjects));
                    break;

                case Item.ItemType.Tent:
                    ReturnSprite = Items.Item.GetItemSprite("tent", IT, Position);
                    ReturnSprite.Item.Description = "Zoo";
                    ReturnSprite.Item.Stack = false;
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    ReturnSprite.Item.Cost = 25;
                    ReturnSprite.Size = new Vector2(640, 508) / 6;
                    ReturnSprite.Item.OriSize = ReturnSprite.Size;
                    ReturnSprite.SpriteType = Objects.Base.Type.Tile;
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Actions.Stable), "EnterStable", null));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.Shop:
                    ReturnSprite = Items.Item.GetItemSprite("treehouse", IT, Position);
                    ReturnSprite.Item.Description = "Shop";
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    ReturnSprite.Size = new Vector2(509, 640) / 5;
                    ReturnSprite.Item.OriSize = ReturnSprite.Size;
                    ReturnSprite.SpriteType = Objects.Base.Type.Tile;

                    ReturnSprite.Inventory = new List<Objects.Item>();
                    ReturnSprite.Inventory.AddRange(Actions.Shop.GetRandomItemList(3, 10));

                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Actions.Shop), "EnterShop", ActionObjects));
                    break;

                case Item.ItemType.dungeonentrance:
                    ReturnSprite = Items.Item.GetItemSprite("tower", IT, Position);
                    ReturnSprite.Item.Description = "deep dark";
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    ReturnSprite.Size = new Vector2(239, 486) / 4;
                    ReturnSprite.Item.OriSize = ReturnSprite.Size;
                    ReturnSprite.SpriteType = Objects.Base.Type.Tile;
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Maps.Dungeon), "WarpDungeon", null));
                    break;

                case Item.ItemType.Health:
                    ReturnSprite = Items.Item.GetItemSprite("heart_full", IT, Position);
                    ReturnSprite.Size = new Vector2(10, 10);
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Collision, typeof(Items.ItemActions), "HealthPickup", ActionObjects));
                    break;

                case Item.ItemType.Torch:
                    ReturnSprite = Items.Item.GetItemSprite("animatedtorch", IT, Position);
                    ReturnSprite.Item.Description = "Flame";
                    ReturnSprite.Item.BackGroundType = ItemType.Floor;
                    ReturnSprite.Item.Cost = 5;
                    ReturnSprite.Item.Stack = false;
                    ReturnSprite.AnimSprite = new Objects.AnimSprite(9, 1);
                    ReturnSprite.AnimSprite.action = true;
                    ReturnSprite.AnimSprite.StopOnEof = false;
                    ReturnSprite.clipping = false;
                    ReturnSprite.speed = 25;
                    ReturnSprite.LightSourceDistance = Util.Global.TorchLightDistance;
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.SpotLight:
                    ReturnSprite = Items.Item.GetItemSprite("spotlight", IT, Position);
                    ReturnSprite.orderNum = 6;
                    ReturnSprite.Item.BackGroundType = ItemType.Wall2;
                    ReturnSprite.Item.Cost = 50;
                    ReturnSprite.Size = new Vector2(702 / 22, 720 / 22);
                    ReturnSprite.Item.Stack = false;
                    ReturnSprite.clipping = false;
                    ReturnSprite.speed = 0;
                    ReturnSprite.LightSourceDistance = Util.Global.TorchLightDistance;
                    break;
                case Item.ItemType.Chest:
                    ReturnSprite = Items.Item.GetItemSprite("chest", IT, Position);
                    ReturnSprite.Item.Stack = false;
                    ReturnSprite.Item.BackGroundType = ItemType.Floor;
                    ReturnSprite.Inventory = new List<Objects.Item>();
                    ReturnSprite.Size = new Vector2(32, 32);
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Menu.Chest), "DisplayChest", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.ChestLoot:
                    ReturnSprite = Items.Item.GetItemSprite("chest", IT, Position);
                    ReturnSprite.Item.Stack = false;
                    ReturnSprite.Item.BackGroundType = ItemType.Floor;
                    ReturnSprite.Inventory = new List<Objects.Item>();
                    ReturnSprite.Inventory.AddRange(Actions.Shop.GetRandomItemList(0, 5));
                    for (int i = 0; i < Util.Global.GetRandomInt(0, 7); i++)
                    {
                        ReturnSprite.Inventory.Add(GetItemByType(ItemType.Coin, Util.Global.DefaultPosition).Item);
                    }
                    ReturnSprite.Size = new Vector2(32, 32);
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Menu.Chest), "DisplayChest", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.TestChest:
                    ReturnSprite = Items.Item.GetItemSprite("chest", IT, Position);
                    ReturnSprite.Item.Stack = false;
                    ReturnSprite.Item.BackGroundType = ItemType.Floor;
                    ReturnSprite.Inventory = new List<Objects.Item>();
                    ReturnSprite.Inventory.Add(GetItemByType(ItemType.Hammer, Util.Global.DefaultPosition).Item);
                    ReturnSprite.Inventory.Add(GetItemByType(ItemType.Pickaxe, Util.Global.DefaultPosition).Item);
                    ReturnSprite.Inventory.Add(GetItemByType(ItemType.Torch, Util.Global.DefaultPosition).Item);
                    ReturnSprite.Inventory.Add(GetItemByType(ItemType.PotionSpeed, Util.Global.DefaultPosition).Item);
                    ReturnSprite.Inventory.Add(GetItemByType(ItemType.PotionStrength, Util.Global.DefaultPosition).Item);
                    ReturnSprite.Inventory.Add(GetItemByType(ItemType.PotionDexterity, Util.Global.DefaultPosition).Item);
                    ReturnSprite.Size = new Vector2(32, 32);
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Mouse, typeof(Menu.Chest), "DisplayChest", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                #endregion

                #region potions
                case Item.ItemType.PotionHealth:
                    ReturnSprite = Items.Item.GetItemSprite("potion", IT, Position);
                    ReturnSprite.Item.Description = "Heal";
                    ReturnSprite.Item.Cost = 10;
                    ReturnSprite.color = Color.Red * .8f;
                    ActionObjects.Add(IT);
                    ActionObjects.Add(Util.Global.Hero);
                    ActionObjects.Add(Actions.Buff.BuffType.Health);
                    ActionObjects.Add(1);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Item, typeof(Items.ItemActions), "ConsumeItemAndBuff", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.PotionSpeed:
                    ReturnSprite = Items.Item.GetItemSprite("potion", IT, Position);
                    ReturnSprite.Item.Description = "Quick";
                    ReturnSprite.Item.Cost = 10;
                    ReturnSprite.color = Color.Green * .8f;
                    ActionObjects.Add(IT);
                    ActionObjects.Add(Util.Global.Hero);
                    ActionObjects.Add(Actions.Buff.BuffType.Speed);
                    ActionObjects.Add(60);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Item, typeof(Items.ItemActions), "ConsumeItemAndBuff", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.PotionStrength:
                    ReturnSprite = Items.Item.GetItemSprite("potion", IT, Position);
                    ReturnSprite.Item.Description = "Strong";
                    ReturnSprite.Item.Cost = 10;
                    ReturnSprite.color = Color.Purple * .8f;
                    ActionObjects.Add(IT);
                    ActionObjects.Add(Util.Global.Hero);
                    ActionObjects.Add(Actions.Buff.BuffType.Strength);
                    ActionObjects.Add(60);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Item, typeof(Items.ItemActions), "ConsumeItemAndBuff", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;

                case Item.ItemType.PotionDexterity:
                    ReturnSprite = Items.Item.GetItemSprite("potion", IT, Position);
                    ReturnSprite.Item.Description = "Accurate";
                    ReturnSprite.Item.Cost = 10;
                    ReturnSprite.color = Color.Orange * .8f;
                    ActionObjects.Add(IT);
                    ActionObjects.Add(Util.Global.Hero);
                    ActionObjects.Add(Actions.Buff.BuffType.Dexterity);
                    ActionObjects.Add(60);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Item, typeof(Items.ItemActions), "ConsumeItemAndBuff", ActionObjects));
                    ActionObjects = new List<object>();
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;
                #endregion

                #region Props
                case Item.ItemType.HayStack:
                    ReturnSprite = Items.Item.GetItemSprite("haystack", IT, Position);
                    ReturnSprite.Item.BackGroundType = ItemType.Floor;
                    ReturnSprite.Size = new Vector2(960 / 13, 539 / 13);
                    ReturnSprite.orderNum = 5;
                    break;
                case Item.ItemType.Barrel:
                    ReturnSprite = Items.Item.GetItemSprite("claypot", IT, Position);
                    ReturnSprite.Item.BackGroundType = ItemType.Floor;
                    ReturnSprite.orderNum = 5;
                    break;
                case Item.ItemType.ClayPot:
                    ReturnSprite = Items.Item.GetItemSprite("claypot", IT, Position);
                    ReturnSprite.Item.BackGroundType = ItemType.Floor;
                    ReturnSprite.orderNum = 5;
                    break;
                case Item.ItemType.ClayPot2:
                    ReturnSprite = Items.Item.GetItemSprite("claypot2", IT, Position);
                    ReturnSprite.Item.BackGroundType = ItemType.Floor;
                    ReturnSprite.orderNum = 5;
                    break;
                case Item.ItemType.ClayPot3:
                    ReturnSprite = Items.Item.GetItemSprite("claypot3", IT, Position);
                    ReturnSprite.Item.BackGroundType = ItemType.Floor;
                    ReturnSprite.orderNum = 5;
                    break;
                case Item.ItemType.Flower:
                    ReturnSprite = Items.Item.GetItemSprite("flower", IT, Position);
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    ReturnSprite.orderNum = 5;
                    break;
                case Item.ItemType.Flower2:
                    ReturnSprite = Items.Item.GetItemSprite("flower2", IT, Position);
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    ReturnSprite.orderNum = 5;
                    break;
                case Item.ItemType.RockSmall:
                    ReturnSprite = Items.Item.GetItemSprite("rocksmall", IT, Position);
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    ReturnSprite.orderNum = 5;
                    break;
                case Item.ItemType.Shelf1:
                    ReturnSprite = Items.Item.GetItemSprite("shelf1", IT, Position);
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    ReturnSprite.orderNum = 5;
                    ReturnSprite.Size = new Vector2(64, 64);
                    break;
                case Item.ItemType.Bush2:
                    ReturnSprite = Items.Item.GetItemSprite("bush2", IT, Position);
                    ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    ReturnSprite.Size = new Vector2(Util.Global.GetRandomInt(32, 64), Util.Global.GetRandomInt(32, 64));
                    ReturnSprite.orderNum = 5;
                    break;
                case ItemType.Chair:
                    ReturnSprite = Items.Item.GetItemSprite("chair", IT, Position);
                    ReturnSprite.Item.Description = "Take a Load off";
                    ReturnSprite.Item.Cost = 50;
                    ReturnSprite.Size = new Vector2(585 / 20, 720 / 20);
                    ReturnSprite.Item.BackGroundType = ItemType.Floor;
                    ReturnSprite.orderNum = 5;
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.MouseRight, typeof(Items.ItemActions), "PickupItem", ActionObjects));
                    break;
                case Item.ItemType.Candle:
                    ReturnSprite = Items.Item.GetItemSprite("candle", IT, Position);
                    ReturnSprite.Item.BackGroundType = ItemType.Floor2;
                    ReturnSprite.Size = new Vector2(11, 52);
                    ReturnSprite.orderNum = 5;
                    ReturnSprite.LightSourceDistance = 60;
                    break;
                case Item.ItemType.Web:
                    ReturnSprite = Items.Item.GetItemSprite("web", IT, Position);
                    ReturnSprite.Item.BackGroundType = ItemType.Floor2;
                    ReturnSprite.Size = new Vector2(64, 64);
                    ReturnSprite.orderNum = 5;
                    break;
                case Item.ItemType.Pillar:
                    ReturnSprite = Items.Item.GetItemSprite("pillar", IT, Position);
                    ReturnSprite.Item.BackGroundType = ItemType.Floor2;
                    ReturnSprite.Size = new Vector2(13 * 2, 33 * 2);
                    ReturnSprite.orderNum = 5;
                    break;
                #endregion

                case ItemType.Spawner:
                    ReturnSprite = Items.Item.GetItemSprite("coin", IT, Position);
                    ReturnSprite.orderNum = 5;
                    ReturnSprite.speed = 5;
                    ReturnSprite.Item.BackGroundType = ItemType.Gravel;
                    //ReturnSprite.Size = new Vector2(693 / 12, 720 / 12);
                    ActionObjects.Add(ReturnSprite);
                    ReturnSprite.actionCall.Add(new Actions.ActionCall(Game.Actions.ActionType.Update, typeof(Items.ItemActions), "ProximitySpawnAction", ActionObjects));
                    break;

                default:
                    ReturnSprite = Items.Item.GetItemSprite(IT.ToString(), IT, Position);
                    //ReturnSprite.Item.BackGroundType = ItemType.Grass;
                    Texture2D model = Util.Global.AllTexture2D.Where(x => x.Name == IT.ToString().ToLower()).FirstOrDefault();
                    ReturnSprite.Size = new Vector2(model.Width, model.Height);
                    ReturnSprite.orderNum = 5;
                    ReturnSprite.Item.Class = ItemClass.Prop;
                    break;
            }
            return ReturnSprite;
        
        }
        */
    }
}
