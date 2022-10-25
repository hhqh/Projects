using System.Threading.Tasks;

using NUnit.Framework;
using System.Linq;
using Game.Engine.EngineGame;
using Game.Models;
using Game.Helpers;
using Game.ViewModels;

namespace ScenarioEngineGame
{
    [TestFixture]
    public class AutoBattleEngineGameScenarioTests
    {
        AutoBattleEngine AutoBattle;

        [SetUp]
        public void Setup()
        {
            AutoBattle = new AutoBattleEngine();

            AutoBattle.Battle.EngineSettings.CharacterList.Clear();
            AutoBattle.Battle.EngineSettings.MonsterList.Clear();
            AutoBattle.Battle.EngineSettings.CurrentDefender = null;
            AutoBattle.Battle.EngineSettings.CurrentAttacker = null;

            AutoBattle.Battle.EngineSettings.BattleSettingsModel.WeakMonsterSpawn = false;
            AutoBattle.Battle.EngineSettings.BattleSettingsModel.AllowCriticalHit = false;
            AutoBattle.Battle.EngineSettings.BattleSettingsModel.AllowMiracleMax = false;
            AutoBattle.Battle.EngineSettings.BattleSettingsModel.AllowMonsterItems = false;
            AutoBattle.Battle.EngineSettings.BattleSettingsModel.ReverseSpeedOrder = false;
            AutoBattle.Battle.EngineSettings.BattleSettingsModel.AllowCriticalMiss = false;
            AutoBattle.Battle.EngineSettings.BattleSettingsModel.AllowMonstersToStealItems = false;
            AutoBattle.Battle.EngineSettings.BattleSettingsModel.AllowZombieMonsters = false;


            _ = AutoBattle.Battle.StartBattle(true);   // Clear the Engine
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void AutoBattleEngine_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AutoBattle;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AutoBattleEngine_RunAutoBattle_Monsters_1_Should_Pass()
        {
            //Arrange

            // Add Characters

            AutoBattle.Battle.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1,
                                Level = 10,
                                CurrentHealth = 11,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                                ListOrder = 1,
                            });

            AutoBattle.Battle.EngineSettings.CharacterList.Add(CharacterPlayerMike);


            // Add Monsters

            // Need to set the Monster count to 1, so the battle goes to Next Round Faster
            AutoBattle.Battle.EngineSettings.MaxNumberPartyMonsters = 1;

            //Act
            var result = await AutoBattle.RunAutoBattle();

            //Reset

            //Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task AutoBattleEngine_RunAutoBattle_Character_Level_Up_Should_Pass()
        {

            /* 
             * Test to force leveling up of a character during the battle
             * 
             * 1 Character, Experience set at next level mark
             * 
             * 6 Monsters
             * 
             * Character Should Level UP 1 level
             * 
             */

            //Arrange

            // Add Characters

            AutoBattle.Battle.EngineSettings.MaxNumberPartyCharacters = 1;

            CharacterIndexViewModel.Instance.Dataset.Clear();

            // To See Level UP happening, a character needs to be close to the next level
            var Character = new CharacterModel
            {
                Name = "Leveling",
                Speed = 100,    // Go first
            };

            // Remember Start Level
            var StartLevel = Character.Level;

            var CharacterPlayer = new PlayerInfoModel(Character);

            CharacterPlayer.ExperienceTotal = 300;

            AutoBattle.Battle.EngineSettings.CharacterList.Add(CharacterPlayer);

            // Add Monsters
            AutoBattle.Battle.EngineSettings.MaxNumberPartyMonsters = 3;
            //Act
            var result = await AutoBattle.RunAutoBattle();

            //Reset

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, AutoBattle.Battle.EngineSettings.BattleScore.CharacterAtDeathList.Contains("Leveling"));
        }

        [Test]
        public async Task AutoBattleEngine_RunAutoBattle_GameOver_Round_1_Should_Pass()
        {
            /* 
             * 
             * 1 Character, Speed slowest, only 1 HP
             * 
             * 6 Monsters
             * 
             * Should end in the first round
             * 
             */

            //Arrange

            // Add Characters

            AutoBattle.Battle.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1, // Will go last...
                                Level = 1,
                                CurrentHealth = 1,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                ListOrder = 1,
                            });

            AutoBattle.Battle.EngineSettings.CharacterList.Add(CharacterPlayer);


            // Add Monsters

            AutoBattle.Battle.EngineSettings.MaxNumberPartyMonsters = 6;

            //Act
            var result = await AutoBattle.RunAutoBattle();

            //Reset

            //Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task AutoBattleEngine_RunAutoBattle_InValid_Round_Loop_Should_Fail()
        {
            /* 
             * Test infinate rounds.  
             * 
             * Characters overpower monsters, game never ends
             * 
             * 6 Character
             *      Speed high
             *      Hit Hard
             *      High health
             * 
             * 1 Monsters
             *      Slow
             *      Weak Hit
             *      Weak health
             * 
             * Should never end
             * 
             * Inifinite Loop Check should stop the game
             * 
             */

            //Arrange

            // Add Characters

            AutoBattle.Battle.EngineSettings.MaxNumberPartyCharacters = 6;

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 100,
                                Level = 20,
                                MaxHealth = 200,
                                CurrentHealth = 200,
                                ExperienceTotal = 1,
                            });

            var CharacterPlayerMin = new PlayerInfoModel(
                new CharacterModel
                {
                    Speed = 99,
                    Level = 1,
                    MaxHealth = 200,
                    CurrentHealth = 200,
                    ExperienceTotal = 1,
                });

            AutoBattle.Battle.EngineSettings.CharacterList.Add(CharacterPlayer);
            AutoBattle.Battle.EngineSettings.CharacterList.Add(CharacterPlayer);
            AutoBattle.Battle.EngineSettings.CharacterList.Add(CharacterPlayer);
            AutoBattle.Battle.EngineSettings.CharacterList.Add(CharacterPlayer);
            AutoBattle.Battle.EngineSettings.CharacterList.Add(CharacterPlayer);
            AutoBattle.Battle.EngineSettings.CharacterList.Add(CharacterPlayerMin);

            // Add Monsters

            AutoBattle.Battle.EngineSettings.MaxNumberPartyMonsters = 1;

            // Controll Rolls,  Hit is always a 3
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(3);

            //Act
            var result = await AutoBattle.RunAutoBattle();

            //Reset
            _ = DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(true, AutoBattle.Battle.EngineSettings.BattleScore.RoundCount > AutoBattle.Battle.EngineSettings.MaxRoundCount);
        }

        [Test]
        public async Task AutoBattleEngine_RunAutoBattle_InValid_Trun_Loop_Should_Fail()
        {
            /* 
             * Test infinate turn.  
             * 
             * Monsters overpower Characters game never ends
             * 
             * 1 Character
             *      Speed low
             *      Hit weak
             *      Health low
             * 
             * 6 Monsters
             *      Speed High
             *      Hit strong
             *      Health High
             * 
             * Rolls for always Miss
             * 
             * Should never end
             * 
             * Inifinite Loop Check should stop the game
             * 
             */

            //Arrange

            // Add Characters

            AutoBattle.Battle.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 1,
                                Level = 1,
                                MaxHealth = 1,
                                CurrentHealth = 1,
                            });

            AutoBattle.Battle.EngineSettings.CharacterList.Add(CharacterPlayerMike);


            // Add Monsters

            AutoBattle.Battle.EngineSettings.MaxNumberPartyMonsters = 6;

            // Controll Rolls,  Always Miss
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(1);

            //Act
            var result = await AutoBattle.RunAutoBattle();

            //Reset
            _ = DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(true, AutoBattle.Battle.EngineSettings.BattleScore.TurnCount > AutoBattle.Battle.EngineSettings.MaxTurnCount);
            Assert.AreEqual(true, AutoBattle.Battle.EngineSettings.BattleScore.RoundCount < AutoBattle.Battle.EngineSettings.MaxRoundCount);
        }

        [Test]
        public async Task AutoBattleEngine_RunAutoBattle_Monster_DropUnique_Should_Pass()
        {
            /* 
             * 
             * 1 Character, Speed slowest, only 1 HP 
             * 
             * 1 Monster in First Round with a Unique Item
             * 1 Monster will kill the Character
             * 
             * Should end in the first round and the Unique item should drop in the Item Pool
             * 
             */

            //Arrange

            // Add Characters

            AutoBattle.Battle.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 100, // Will go First...
                                CurrentHealth = 1,
                                MaxHealth = 1,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                ListOrder = 1,
                            });

            AutoBattle.Battle.EngineSettings.CharacterList.Add(CharacterPlayer);

            // Add Monsters

            AutoBattle.Battle.EngineSettings.MaxNumberPartyMonsters = 2;

            AutoBattle.Battle.EngineSettings.BattleSettingsModel.WeakMonsterSpawn = true;
            AutoBattle.Battle.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Hit;
            AutoBattle.Battle.EngineSettings.BattleSettingsModel.CharacterHitEnum = HitStatusEnum.Hit;

            //Act
            var result = await AutoBattle.RunAutoBattle();

            //Reset
            AutoBattle.Battle.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;
            AutoBattle.Battle.EngineSettings.BattleSettingsModel.CharacterHitEnum = HitStatusEnum.Default;
            AutoBattle.Battle.EngineSettings.BattleSettingsModel.WeakMonsterSpawn = false;
            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(1, AutoBattle.Battle.EngineSettings.ItemPool.Count);
        }

        [Test]
        public async Task AutoBattleEngine_RunAutoBattle_Character_Heal_Should_Pass()
        {
            AutoBattle.Battle.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                new CharacterModel
                {
                    Speed = -1,
                    Level = 1,
                    CurrentHealth = 1,
                    MaxHealth = 5,
                    Name = "Mike",
                    ListOrder = 1,
                    Job = CharacterJobEnum.Cleric,

        });

            //Add Heal
            CharacterPlayer.AbilityTracker[AbilityEnum.Heal] = 1;
            CharacterPlayer.AbilityTracker[AbilityEnum.Bandage] = 0;
            CharacterPlayer.AbilityTracker[AbilityEnum.Quick] = 0;
            CharacterPlayer.AbilityTracker[AbilityEnum.Barrier] = 0;
            CharacterPlayer.AbilityTracker[AbilityEnum.Curse] = 0;

            AutoBattle.Battle.EngineSettings.CharacterList.Add(CharacterPlayer);

            AutoBattle.Battle.EngineSettings.MaxNumberPartyMonsters = 1;
            AutoBattle.Battle.EngineSettings.BattleSettingsModel.WeakMonsterSpawn = true;
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(2);

            //Act
            var result = await AutoBattle.RunAutoBattle();

            //Reset
            _ = DiceHelper.DisableForcedRolls();
            AutoBattle.Battle.EngineSettings.BattleSettingsModel.WeakMonsterSpawn = false;
            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(0, CharacterPlayer.AbilityTracker[AbilityEnum.Heal]);

        }


        

    }
}