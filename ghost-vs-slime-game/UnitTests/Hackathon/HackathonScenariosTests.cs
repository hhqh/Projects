using NUnit.Framework;

using Game.Models;
using System.Threading.Tasks;
using Game.ViewModels;
using Game.Helpers;
using Game.Services;
using System.Linq;
using UnitTests.TestHelpers;
using Game.Engine.EngineGame;
using Game.Engine.EngineModels;

namespace HackathonScenario
{
    [TestFixture]
    public class HackathonScenarioTests
    {
        #region TestSetup
        readonly BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        [SetUp]
        public void Setup()
        {
            // Put seed data into the system for all tests
            _ = BattleEngineViewModel.Instance.Engine.Round.ClearLists();

            //Start the Engine in AutoBattle Mode
            _ = BattleEngineViewModel.Instance.Engine.StartBattle(false);

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.CharacterHitEnum = HitStatusEnum.Default;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowCriticalHit = false;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowCriticalMiss = false;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowMonsterItems = false;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowMonstersToStealItems = false;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowSlimeBoss = false;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowMiracleMax = false;
        }

        [TearDown]
        public void TearDown()
        {
        }
        #endregion TestSetup

        #region Scenario0
        [Test]
        public void HakathonScenario_Scenario_0_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      #
            *      
            * Description: 
            *      <Describe the scenario>
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      <List Files Changed>
            *      <List Classes Changed>
            *      <List Methods Changed>
            * 
            * Test Algrorithm:
            *      <Step by step how to validate this change>
            * 
            * Test Conditions:
            *      <List the different test conditions to make>
            * 
            * Validation:
            *      <List how to validate this change>
            *  
            */

            // Arrange

            // Act

            // Assert


            // Act
            var result = EngineViewModel;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
        #endregion Scenario0

        #region Scenario1
        [Test]
        public async Task HackathonScenario_Scenario_1_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      1
            *      
            * Description: 
            *      Make a Character called Mike, who dies in the first round
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      No Code changes requied 
            * 
            * Test Algrorithm:
            *      Create Character named Mike
            *      Set speed to -1 so he is really slow
            *      Set Max health to 1 so he is weak
            *      Set Current Health to 1 so he is weak
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify Mike is not in the Player List
            *      Verify Round Count is 1
            *  
            */

            //Arrange

            // Set Character Conditions

            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1, // Will go last...
                                Level = 1,
                                CurrentHealth = 1,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                            });

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerMike);

            // Set Monster Conditions

            // Auto Battle will add the monsters

            // Monsters always hit
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Hit;

            //Act
            var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();

            //Reset
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(null, EngineViewModel.Engine.EngineSettings.PlayerList.Find(m => m.Name.Equals("Mike")));
            Assert.AreEqual(1, EngineViewModel.Engine.EngineSettings.BattleScore.RoundCount);
        }
        #endregion Scenario1

        #region Scenario30
        [Test]
        public async Task HackathonScenario_Scenario_30_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      30
            *      
            * Description: 
            *      Allows monster to have items
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      No Code changes required 
            * 
            * Test Algrorithm:
            *      Set debug setting to allow monster to have items
            *      Make a monster called Mike and assigned items
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify that Mike has items in each location
            */

            //Arrange

            var itemViewModel = ItemIndexViewModel.Instance;

            var headItem= itemViewModel.GetDefaultItemId(ItemLocationEnum.Head);
            var neckItem = itemViewModel.GetDefaultItemId(ItemLocationEnum.Necklass);
            var primaryItem = itemViewModel.GetDefaultItemId(ItemLocationEnum.PrimaryHand);
            var offItem = itemViewModel.GetDefaultItemId(ItemLocationEnum.OffHand);
            var rightItem = itemViewModel.GetDefaultItemId(ItemLocationEnum.RightFinger);
            var leftItem = itemViewModel.GetDefaultItemId(ItemLocationEnum.LeftFinger);
            var feetItem = itemViewModel.GetDefaultItemId(ItemLocationEnum.Feet);

            // Set Monster Conditions
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowMonsterItems = true;

            // Monsters have items
            EngineViewModel.Engine.EngineSettings.MaxNumberPartyMonsters = 1;

            var MonsterPlayer = new PlayerInfoModel(
                            new MonsterModel
                            {
                                Speed = 50, 
                                Level = 20,
                                CurrentHealth = 50,
                                Name = "Mike",
                                Head = headItem,
                                Necklass = neckItem,
                                PrimaryHand = primaryItem,
                                OffHand = offItem,
                                RightFinger = rightItem,
                                LeftFinger = leftItem,
                                Feet = feetItem
                            });

            EngineViewModel.Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            //Act
            var index = EngineViewModel.Engine.EngineSettings.MonsterList.IndexOf(MonsterPlayer);

            var head = EngineViewModel.Engine.EngineSettings.MonsterList[index].Head;
            var neck = EngineViewModel.Engine.EngineSettings.MonsterList[index].Necklass;
            var primary = EngineViewModel.Engine.EngineSettings.MonsterList[index].PrimaryHand;
            var offhand = EngineViewModel.Engine.EngineSettings.MonsterList[index].OffHand;
            var leftfinger = EngineViewModel.Engine.EngineSettings.MonsterList[index].LeftFinger;
            var rightfinger = EngineViewModel.Engine.EngineSettings.MonsterList[index].RightFinger;
            var feet = EngineViewModel.Engine.EngineSettings.MonsterList[index].Feet;
            
            var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();

            //Reset
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowMonsterItems = false;

            //Assert
            Assert.AreEqual(true, result);   
            Assert.AreEqual(headItem, head);
            Assert.AreEqual(primaryItem, primary);
            Assert.AreEqual(offItem, offhand);
            Assert.AreEqual(rightItem, rightfinger);
            Assert.AreEqual(leftItem, leftfinger);
            Assert.AreEqual(feetItem, feet);
            Assert.AreEqual(neckItem, neck);       
        }
        #endregion Scenario30

        //#region Scenario2
        //[Test]
        //public async Task HackathonScenario_Scenario_2_Valid_Default_Should_Pass()
        //{
        //    /* 
        //    * Scenario Number:  
        //    *      2
        //    *      
        //    * Description: 
        //    *      Make a Character called Doug, who is really strong with 50 health, but he always misses
        //    * 
        //    * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
        //    *      TurnEngine.cs, TurnAsAttack Method 
        //    *           In the method, add an if statement that checks the party for a Player named Doug.
        //    *           If there's a Doug, set his HitStatusEnum.Miss
        //    * 
        //    * Test Algrorithm:
        //    *      Create Character named Doug
        //    *      Set Max Health to 50
        //    *      Set Current Health to 50 so he is strong
        //    *      
        //    *      Create a Monster 
        //    *      Set Max Health to 1
        //    *      Set Current Health to 1
        //    *      Set Speed to 1
        //    *      Set Level to 1
        //    *      
        //    *  
        //    *      Startup Battle
        //    *      Run Auto Battle
        //    * 
        //    * Test Conditions:
        //    *      Default condition is sufficient
        //    * 
        //    * Validation:
        //    *      Verify Battle Returned True
        //    *      Verify Doug is not in the Player List
        //    *      Verify Monster is still in the Monster List
        //    *      Verify Round Count is 1
        //    *  
        //    */

        //    //Arrange

        //    // Set Character Conditions

        //    EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

        //    var CharacterPlayerDoug = new PlayerInfoModel(
        //                    new CharacterModel
        //                    {
        //                        Speed = 100, 
        //                        MaxHealth = 1,
        //                        CurrentHealth = 1,
        //                        Name = "Doug",
        //                    });

        //    EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerDoug);

        //    EngineViewModel.Engine.EngineSettings.MaxNumberPartyMonsters = 1;


        //    // Monsters always hit
        //    EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Hit;

        //    //Act
        //    var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();

        //    //Reset
        //    EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

        //    //Assert
        //    Assert.AreEqual(true, result);
        //    Assert.AreEqual(null, EngineViewModel.Engine.EngineSettings.PlayerList.Find(m => m.Name.Equals("Doug")));
        //    Assert.AreEqual(1, EngineViewModel.Engine.EngineSettings.MonsterList.Count);
        //    Assert.AreEqual(1, EngineViewModel.Engine.EngineSettings.BattleScore.RoundCount);
        //}
        //#endregion Scenario2

        #region Scenario13
        [Test]
        public async Task HackathonScenario_Scenario_13_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      13
            *      
            * Description: 
            *      A percentage chance of Slimes stealing a Player's equipped items upon Player's death for tax payment.
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      BattleSettingsModel.cs: Add a boolean AllowMonstersToStealItem
            *      TurnEngine.cs: Added a method called MonsterStealItemRoll
            *                   : In DropItems, have two if statements to check if AllowMonstersToStealitem is true, and
            *                      check MonsterStealItemRoll
            *               
            * 
            * Test Algrorithm:
            *      Create a weak character called Richie Rich with a Primary hand item
            *      Have a 6 monsters
            *      
            *      Enable Forced Roll
            *      Enable AllowMonstersToStealItem
            *      Set Forced Roll value to 2 to allow Monsters to steal item
            *      
            *      Make Monsters always hit
            *      Make Players always miss
            *      
            *      
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify Battle Message Model prints "The Slime Revenue Services has taken all your items for tax payment!"
            *      Verify Item Pool is empty cause the Slime stole all the drops
            *  
            */

            //Arrange

            // Set Character Conditions
            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;
            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1, // Will go Last
                                PrimaryHand = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.PrimaryHand),
                                Level = 1,
                                MaxHealth = 1,
                                CurrentHealth = 1,
                                Name = "Richie Rich",
                            });

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayer);
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowMonstersToStealItems = true;


            EngineViewModel.Engine.EngineSettings.MaxNumberPartyMonsters = 6;


            // Monsters always hit
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Hit;

            // Characters always miss
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.CharacterHitEnum = HitStatusEnum.Miss;

            // Enable Force rolls
            DiceHelper.EnableForcedRolls();

            // Set for roll value to 2 so that Slimes will steal items.
            DiceHelper.SetForcedRollValue(2);

            //Act
            var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();

            //Reset
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.CharacterHitEnum = HitStatusEnum.Default;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowMonstersToStealItems = false;
            DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual("The Slime Revenue Services has taken all your items for tax payment!", EngineViewModel.Engine.EngineSettings.BattleMessagesModel.DroppedMessage);
            Assert.AreEqual(0, EngineViewModel.Engine.EngineSettings.ItemPool.Count);
        }

        [Test]
        public async Task HackathonScenario_Scenario_13_Invalid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      13
            *      
            * Description: 
            *      A percentage chance of Slimes stealing a Player's equipped items upon Player's death for tax payment.
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      BattleSettingsModel.cs: Add a boolean AllowMonstersToStealItem
            *      TurnEngine.cs: Added a method called MonsterStealItemRoll
            *                   : In DropItems, have two if statements to check if AllowMonstersToStealitem is true, and
            *                      check MonsterStealItemRoll
            *               
            * 
            * Test Algrorithm:
            *      Create a weak character called Richie Rich with a Primary hand item
            *      Have a 6 monsters
            *      
            *      Enable Forced Roll
            *      Enable AllowMonstersToStealItem
            *      Set Forced Roll value to 5 to don't allow Monsters to steal item
            *      
            *      Make Monsters always hit
            *      Make Players always miss
            *      
            *      
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify Battle Message Model does not prints item drop to be item taken
            *      Verify Item Pool is not empty
            *  
            */

            //Arrange

            // Set Character Conditions
            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;
            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1, // Will go Last
                                PrimaryHand = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.PrimaryHand),
                                Level = 1,
                                MaxHealth = 1,
                                CurrentHealth = 1,
                                Name = "Richie Rich",
                            });

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayer);
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowMonstersToStealItems = true;


            EngineViewModel.Engine.EngineSettings.MaxNumberPartyMonsters = 6;


            // Monsters always hit
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Hit;

            // Characters always miss
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.CharacterHitEnum = HitStatusEnum.Miss;

            // Enable Force rolls
            DiceHelper.EnableForcedRolls();

            // Set for roll value to 5
            DiceHelper.SetForcedRollValue(5);

            //Act
            var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();

            //Reset
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.CharacterHitEnum = HitStatusEnum.Default;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowMonstersToStealItems = false;
            DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreNotEqual("The Slime Revenue Services has taken all your items for tax payment!", EngineViewModel.Engine.EngineSettings.BattleMessagesModel.DroppedMessage);
            Assert.AreNotEqual(0, EngineViewModel.Engine.EngineSettings.ItemPool.Count);
        }

        #endregion Scenario13

        #region Scenario10
        [Test]
        public async Task HackathonScenario_Scenario_10_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      10
            *      
            * Description: 
            *      Mostly Dead is not Entirely Dead - Allows a character who is nearly dead to be revive by Miracle Max 
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      EngineSettingsModel.cs: added Miracle Max int set to 1 by default
            *      
            *      BattleEngineBase.cs: set Miracle Max to 1 when a battle starts
            *      
            *      TurnEngineBase.cs: check in ApplyDamage() if the damage applied is enough to make a target die. If the target is a character, and miracle max count is 1, then use up 
            *      miracle max, revive the character to its max health, and outputs message (modified in BattleMessageModel.cs)
            *      to show that miracle max is used and target's current health is max health. 
            *      
            *      BattleMessageModel.cs: Added miracle max message when miracle max is used
            * 
            * Test Algorithm:
            *      Make a really weak character called Lizzie Cat.
            *      Make a really strong character called Cat.
            *      Character always misses.
            *      Monsters always hit.
            *      
            *      Run AutoBattle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Auto Battle runs
            *      Verfify Miracle Max Message displays Miracle Count as 0 
            *      and Current Health of the Target as the Max Health
            *  
            */

            // Arrange
            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 2;

            // Lizzie cat will always be the first to die and get miracle max
            var CharacterPlayerOne = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1, // Will go Last
                                Level = 1,
                                MaxHealth = 1,
                                CurrentHealth = 1,
                                Name = "Lizzie Cat",
                            });

            var CharacterPlayerTwo = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 50,
                                Level = 5,
                                MaxHealth = 100,
                                CurrentHealth = 100,
                                Name = "Cat",
                            });
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowMiracleMax = true;
            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerOne);
            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerTwo);
            EngineViewModel.Engine.EngineSettings.MiracleMax = 1;

            // Character always misses
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.CharacterHitEnum = HitStatusEnum.Miss;

            // Monsters always hit
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Hit;

            //Act
            var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();

            //Reset
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.CharacterHitEnum = HitStatusEnum.Default;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowMiracleMax = false;
            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual("Miracle Max to the Rescue! Miracle Max Count: 0 Current Health: " + CharacterPlayerOne.CurrentHealth, EngineViewModel.Engine.EngineSettings.BattleMessagesModel.MiracleMax);
        }
        #endregion Scenario10

        #region Scenario16

        [Test]
        public void HackathonScenario_Scenario_16_OrderPlayerListByLowerSpeedFirst_Valid_Should_Be_C()
        {
            /* 
            * Scenario Number:  
            *      16
            *      
            * Description: 
            *      Suddenly the room has a time warp, and the sort order for initiative is reversed.  Lowest speed 
                    goes first. Add a debug switch to enable the feature and a % chance.
                    For each round determine of the % happens.  If it does happen, then reverse the initiative.  
                    Slowest goes first.
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
                Added two variables ReverseSpeedOrder and ReverseInitiativePercentage to BattleSettingsModel.cs
            *      Added Reverse order toggle to BattleSettingsPage.xaml and BattleSettingsPage.xaml.cs
            *      Added if statement, dice roll, and Ascending speed implementation for playerList in RoundEngine.cs, 
            *           OrderPlayerListByTurnOrder() method.
            * 
            * Test Algrorithm:
            *       Create Monster with high speed
            *       Create CHaracter with low speed
            *       Set ReverseSpeedOrder true
            *       Set ReverseInitiativePercentage to 1 force effective
            *       Create PlayerList
            *       Scramble list descending to force reset
            *       Reset ReverseSpeedOrder false
            *       Reset ReverseInitiativePercentage to 5
            *       Compare name of character to person at front of list
            * 
            * Test Conditions:
            *      Make ReverseSpeedOrder true in BattleSettingsModel
            *      Make ReverseInitiativePercentage = 1 in BattleSettingsModel
            * 
            * Validation:
            *      Verify the first player in the PlayerList is the one with the lowest speed
            *  
            */
            // Arrange
            var Monster = new MonsterModel
            {
                Speed = 20,
                Level = 20,
                CurrentHealth = 100,
                ExperienceTotal = 1000,
                Name = "Z",
                ListOrder = 1,
            };

            var MonsterPlayer = new PlayerInfoModel(Monster);
            EngineViewModel.Engine.EngineSettings.MonsterList.Clear();
            EngineViewModel.Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            var Character = new CharacterModel
            {
                Speed = 1,
                Level = 1,
                CurrentHealth = 2,
                ExperienceTotal = 1,
                Name = "C",
                ListOrder = 10
            };

            var CharacterPlayer = new PlayerInfoModel(Character);
            EngineViewModel.Engine.EngineSettings.CharacterList.Clear();
            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.ReverseSpeedOrder = true;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.ReverseInitiativePercentage = 1;

            // Make the List
            EngineViewModel.Engine.EngineSettings.PlayerList = EngineViewModel.Engine.Round.MakePlayerList();

            // Sort the list by Current Health, so it has to be resorted.
            EngineViewModel.Engine.EngineSettings.PlayerList = EngineViewModel.Engine.EngineSettings.PlayerList.OrderByDescending(m => m.CurrentHealth).ToList();




            // Act
            var result = EngineViewModel.Engine.Round.OrderPlayerListByTurnOrder();

            // Reset
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.ReverseSpeedOrder = false;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.ReverseInitiativePercentage = 5;

            // Assert
            Assert.AreEqual("C", result[0].Name);
        }


        [Test]
        public void HackathonScenario_Scenario_16_OrderPlayerListByHigherSpeedFirst_Valid_Should_Be_Z()
        {
            /* 
            * Scenario Number:  
            *      16
            *      
            * Description: 
            *      Suddenly the room has a time warp, and the sort order for initiative is reversed.  Lowest speed 
                    goes first. Add a debug switch to enable the feature and a % chance.
                    For each round determine of the % happens.  If it does happen, then reverse the initiative.  
                    Slowest goes first.
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
                Added two variables ReverseSpeedOrder and ReverseInitiativePercentage to BattleSettingsModel.cs
            *      Added Reverse order toggle to BattleSettingsPage.xaml and BattleSettingsPage.xaml.cs
            *      Added if statement, dice roll, and Ascending speed implementation for playerList in RoundEngine.cs, 
            *           OrderPlayerListByTurnOrder() method.
            * 
            * Test Algrorithm:
            *       Create Monster with high speed
            *       Create CHaracter with low speed
            *       Set ReverseSpeedOrder true
            *       Set ReverseInitiativePercentage to 11 force ineffective
            *       Create PlayerList
            *       Scramble inOrder list to force reset
            *       Reset ReverseSpeedOrder false
            *       Reset ReverseInitiativePercentage to 5
            *       Compare name of character to person at front of list
            * 
            * Test Conditions:
            *      Make ReverseSpeedOrder true in BattleSettingsModel
            *      Make ReverseInitiativePercentage = 1 in BattleSettingsModel
            * 
            * Validation:
            *      Verify the first player in the PlayerList is the one with the lowest speed
            *  
            */
            // Arrange
            var Monster = new MonsterModel
            {
                Speed = 20,
                Level = 20,
                CurrentHealth = 100,
                ExperienceTotal = 1000,
                Name = "Z",
                ListOrder = 1,
            };

            var MonsterPlayer = new PlayerInfoModel(Monster);
            EngineViewModel.Engine.EngineSettings.MonsterList.Clear();
            EngineViewModel.Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            var Character = new CharacterModel
            {
                Speed = 1,
                Level = 1,
                CurrentHealth = 2,
                ExperienceTotal = 1,
                Name = "C",
                ListOrder = 10
            };

            var CharacterPlayer = new PlayerInfoModel(Character);
            EngineViewModel.Engine.EngineSettings.CharacterList.Clear();
            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.ReverseSpeedOrder = true;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.ReverseInitiativePercentage = 11;

            // Make the List
            EngineViewModel.Engine.EngineSettings.PlayerList = EngineViewModel.Engine.Round.MakePlayerList();

            // Sort the list by Current Health, so it has to be resorted.
            EngineViewModel.Engine.EngineSettings.PlayerList = EngineViewModel.Engine.EngineSettings.PlayerList.OrderBy(m => m.CurrentHealth).ToList();




            // Act
            var result = EngineViewModel.Engine.Round.OrderPlayerListByTurnOrder();

            // Reset
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.ReverseSpeedOrder = false;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.ReverseInitiativePercentage = 5;

            // Assert
            Assert.AreEqual("Z", result[0].Name);
        }
        #endregion Scenario16

        #region Scenario14
        [Test]
        public async Task HackathonScenario_Scenario_14_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      14
            *      
            * Description: 
            *      A percentage chance of Boss Slime spawning if AllowSlimeBoss is enabled. 
            *      
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      BattleSettingsModel.cs: Add a boolean AllowSlimeBoss
            *      RoundEngine.cs: Added an if statement that will roll d10 if AllowSlimeBoss is enabled inside AddMonstersToRound
            *               
            * 
            * Test Algrorithm:
            *      Create a weak character called Richie Rich
            *      Have a 6 monsters
            *      
            *      Enable Forced Roll
            *      Enable AllowMonstersToStealItem
            *      Set Forced Roll value to 4 to allow Slime Boss to Spawn
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify MonsterList Has 1 Monster
            *      Verify Monster inside Monster List is Called "Boss Slime 01"
            *  
            */

            //Arrange
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowSlimeBoss = true;
            // Set Character Conditions
            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                new CharacterModel
                {
                    Speed = -1, // Will go Last
                    PrimaryHand = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.PrimaryHand),
                    Level = 1,
                    MaxHealth = 1,
                    CurrentHealth = 1,
                    Name = "Richie Rich",
                });

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            EngineViewModel.Engine.EngineSettings.MaxNumberPartyMonsters = 6;

            // Enable Force rolls
            DiceHelper.EnableForcedRolls();

            // Set for roll value to 3 so that Slimes will steal items.
            DiceHelper.SetForcedRollValue(3);

            //Act
            var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();

            //Reset
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowMonstersToStealItems = false;
            DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(1, EngineViewModel.Engine.EngineSettings.MonsterList.Count);
            Assert.AreEqual("Big Boss 01", EngineViewModel.Engine.EngineSettings.MonsterList[0].Name);
        }
        #endregion Scenario14

    }
}