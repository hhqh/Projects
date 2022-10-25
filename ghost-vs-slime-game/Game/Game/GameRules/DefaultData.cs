using System.Collections.Generic;

using Game.Models;
using Game.ViewModels;

namespace Game.GameRules
{
    public static class DefaultData
    {
        /// <summary>
        /// Load the Default data
        /// </summary>
        /// <returns></returns>
        public static List<ItemModel> LoadData(ItemModel temp)
        {
            var datalist = new List<ItemModel>()
            {
                new ItemModel {
                    Name = "Sword",
                    Description = "Mightier than a Pen",
                    ImageURI = "sword.png",
                    Range = 0,
                    Damage = 10,
                    Value = 2,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Lightning Bolt",
                    Description = "Zap Opponents",
                    ImageURI = "lightning_bolt.png",
                    Range = 0,
                    Damage = 8,
                    Value = 1,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Crown",
                    Description = "Magic Crown",
                    ImageURI = "crown.png",
                    Range = 0,
                    Damage = 6,
                    Value = 3,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Jell-O Potion",
                    Description = "Bottoms Up Magic Health",
                    ImageURI = "jello_potion.png",
                    Range = 0,
                    Damage = 4,
                    Value = 2,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.CurrentHealth},

                new ItemModel {
                    Name = "Bone",
                    Description = "Spooky Powers",
                    ImageURI = "bone.png",
                    Range = 0,
                    Damage = 6,
                    Value = 2,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Slime Grenade",
                    Description = "Throw to Use",
                    ImageURI = "slime_grenade.png",
                    Range = 10,
                    Damage = 10,
                    Value = 4,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Salt",
                    Description = "Slimes Hate Them.",
                    ImageURI = "salt.png",
                    Range = 0,
                    Damage = 6,
                    Value = 3,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Better Sword",
                    Description = "Mightier than a Pen",
                    ImageURI = "sword.png",
                    Range = 0,
                    Damage = 12,
                    Value = 2,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Better Lightning Bolt",
                    Description = "Zap Opponents",
                    ImageURI = "lightning_bolt.png",
                    Range = 0,
                    Damage = 8,
                    Value = 4,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Better Crown",
                    Description = "Magic Crown",
                    ImageURI = "crown.png",
                    Range = 0,
                    Damage = 6,
                    Value = 8,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Better Jell-O Potion",
                    Description = "Bottoms Up Magic Health",
                    ImageURI = "jello_potion.png",
                    Range = 0,
                    Damage = 8,
                    Value = 2,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.CurrentHealth},

                new ItemModel {
                    Name = "Better Bone",
                    Description = "Spooky Powers",
                    ImageURI = "bone.png",
                    Range = 0,
                    Damage = 9,
                    Value = 2,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Better Slime Grenade",
                    Description = "Throw to Use",
                    ImageURI = "slime_grenade.png",
                    Range = 10,
                    Damage = 13,
                    Value = 4,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Kosher Salt",
                    Description = "Slimes Hate Them and it's kosher.",
                    ImageURI = "salt.png",
                    Range = 0,
                    Damage = 8,
                    Value = 3,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Attack},
                                new ItemModel {
                    Name = "Ghost Sword",
                    Description = "Mightier than a Pen and Invisible",
                    ImageURI = "sword.png",
                    Range = 0,
                    Damage = 15,
                    Value = 2,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Ghost Lightning Bolt",
                    Description = "Zap Opponents",
                    ImageURI = "lightning_bolt.png",
                    Range = 0,
                    Damage = 15,
                    Value = 1,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Ghost Crown",
                    Description = "Magic Crown",
                    ImageURI = "crown.png",
                    Range = 0,
                    Damage = 6,
                    Value = 15,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Ghost Jell-O Potion",
                    Description = "Bottoms Up Magic Health",
                    ImageURI = "jello_potion.png",
                    Range = 0,
                    Damage = 12,
                    Value = 2,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.CurrentHealth},

                new ItemModel {
                    Name = "Ghost Bone",
                    Description = "Ghosts have bones?",
                    ImageURI = "bone.png",
                    Range = 0,
                    Damage = 6,
                    Value = 8,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Ghost Slime Grenade",
                    Description = "Throw to Use",
                    ImageURI = "slime_grenade.png",
                    Range = 10,
                    Damage = 10,
                    Value = 11,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Himalayan Salt",
                    Description = "Slimes Hate Them.",
                    ImageURI = "salt.png",
                    Range = 0,
                    Damage = 11,
                    Value = 3,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Attack}
            };          

            return datalist;
        }

        /// <summary>
        /// Load Example Scores
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<ScoreModel> LoadData(ScoreModel temp)
        {
            var datalist = new List<ScoreModel>()
            {
                new ScoreModel {
                    Name = "First Score",
                    Description = "Test Data",
                },

                new ScoreModel {
                    Name = "Second Score",
                    Description = "Test Data",
                }
            };

            return datalist;
        }

        /// <summary>
        /// Load Characters
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<CharacterModel> LoadData(CharacterModel temp)
        {
            var HeadString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Head);
            var NecklassString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Necklass);
            var PrimaryHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.PrimaryHand);
            var OffHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.OffHand);
            var FeetString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Feet);
            var RightFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);
            var LeftFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);

            var datalist = new List<CharacterModel>()
            {
                new CharacterModel {
                    Name = "Posh Ghost",
                    Description = "Controlling Elements",
                    Level = 1,
                    MaxHealth = 5,
                    ImageURI = "posh_ghost.png",
                    Job = CharacterJobEnum.Mage,
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "Sporty Ghost",
                    Description = "Shapeshift",
                    Level = 1,
                    MaxHealth = 5,
                    ImageURI = "sporty_ghost.png",
                    Job = CharacterJobEnum.Fighter,
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "Regular Ghost",
                    Description = "Who Do You Think You Are",
                    Level = 1,
                    MaxHealth = 8,
                    ImageURI = "regular_ghost.png",
                    Job = CharacterJobEnum.Ranger,
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "Scary Ghost",
                    Description = "Possession",
                    Level = 4,
                    MaxHealth = 38,
                    ImageURI = "scary_ghost.png",
                    Job = CharacterJobEnum.Ranger
                },

                new CharacterModel {
                    Name = "Baby Ghost",
                    Description = "Wannabe",
                    Level = 5,
                    MaxHealth = 43,
                    ImageURI = "baby_ghost.png",
                    Job = CharacterJobEnum.Cleric
                },

                new CharacterModel {
                    Name = "Ginger Ghost",
                    Description = "Spice Your Life Up",
                    Level = 5,
                    MaxHealth = 43,
                    ImageURI = "ginger_ghost.png",
                    Job = CharacterJobEnum.Mage
                },

               new CharacterModel {
                    Name = "Devil Ghost",
                    Description = "So Hot!",
                    Level = 1,
                    MaxHealth = 3,
                    ImageURI = "devil_ghost.png",
                    Job = CharacterJobEnum.Fighter
                },

                new CharacterModel {
                    Name = "Angel Ghost",
                    Description = "Cutie Wink",
                    Level = 3,
                    MaxHealth = 23,
                    ImageURI = "angel_ghost.png",
                    Job = CharacterJobEnum.Cleric
                }
            };

            return datalist;
        }

        /// <summary>
        /// Load Characters
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<MonsterModel> LoadData(MonsterModel temp)
        {
            var item1 = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.PrimaryHand);
            var item2 = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.OffHand);
            var item3 = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Feet);
            var item4 = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Necklass);
            var item5 = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Head);
            var item6 = ItemIndexViewModel.Instance.GetLocationItems(ItemLocationEnum.Head)[1].Id;

            var datalist = new List<MonsterModel>()
            {
                new MonsterModel {
                    Name = "Boss Slime",
                    Description = "2022 Best Slime",
                    ImageURI = "boss_slime.png",
                    UniqueItem = item1
        },

                new MonsterModel {
                    Name = "Helper Slime",
                    Description = "Help on the Way!",
                    ImageURI = "slime.png",
                    UniqueItem = item2
                },

                new MonsterModel {
                    Name = "Skeleton Slime",
                    Description = "Jello Bones",
                    ImageURI = "skull_slime.png",
                    UniqueItem = item3
                },

                new MonsterModel {
                    Name = "Racing Slime",
                    Description = "Faster than Any Slime",
                    ImageURI = "racing_slime.png",
                    UniqueItem = item4
                },

                new MonsterModel {
                    Name = "Archer Slime",
                    Description = "Shoot for the Sky",
                    ImageURI = "archer_slime.png",
                    UniqueItem = item5
                },

                new MonsterModel {
                    Name = "Jester Slime",
                    Description = "Not Your Typical Slime",
                    ImageURI = "jester_slime.png",
                    UniqueItem = item6
                },

                new MonsterModel {
                    Name = "Angel Slime",
                    Description = "Cute as a Devil",
                    ImageURI = "angel_slime.png",
                    UniqueItem = item1
                },

                new MonsterModel {
                    Name = "Sexy Slime",
                    Description = "It's Full Moon",
                    ImageURI = "sexy_slime.png",
                    UniqueItem = item2
                },
            };

            return datalist;
        }
    }
}