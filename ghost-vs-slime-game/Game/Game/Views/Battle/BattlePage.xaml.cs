using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    public partial class BattlePage : ContentPage
    {
        // Empty Constructor for UTs
        bool UnitTestSetting;

        public BattlePage(bool UnitTest) { UnitTestSetting = UnitTest; }

        /// <summary>
        /// Constructor
        /// </summary>
        public BattlePage()
        {
            InitializeComponent();

            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Starting;
            
            BindingContext = BattleEngineViewModel.Instance;

            MonsterListView.ItemsSource = BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList;

            // Start the Battle Engine
            _ = BattleEngineViewModel.Instance.Engine.StartBattle(false);

            // Initialize UI
            BackgroundImageSource = "battle_background.png";

            InitializePlayerPositions();

            DrawPlayerBoxes();

            CreateCharactersProgress();

            CreateMonstersProgress();

            DrawPlayerBattle();

            DrawUniqueItems();
        }

        #region PlayerOverview
        /// <summary>
        /// Draw the Player Boxes
        /// </summary>
        public void DrawPlayerBoxes()
        {
            var CharacterBoxList = CharacterBox.Children.ToList();
            foreach (var data in CharacterBoxList)
            {
                _ = CharacterBox.Children.Remove(data);
            }

            // Draw the Characters
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.ToList())
            {
                CharacterBox.Children.Add(PlayerInfoDisplayBox(data));
            }

            var MonsterBoxList = MonsterBox.Children.ToList();
            foreach (var data in MonsterBoxList)
            {
                _ = MonsterBox.Children.Remove(data);
            }

            // Draw the Monsters
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.ToList())
            {
                MonsterBox.Children.Add(PlayerInfoDisplayBox(data));
            }

            // Add one black PlayerInfoDisplayBox to hold space in case the list is empty
            CharacterBox.Children.Add(PlayerInfoDisplayBox(null));

            // Add one black PlayerInfoDisplayBox to hold space in case the list is empty
            MonsterBox.Children.Add(PlayerInfoDisplayBox(null));

        }

        /// <summary>
        /// Put the Player into a Display Box
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout PlayerInfoDisplayBox(PlayerInfoModel data)
        {
            if (data == null)
            {
                data = new PlayerInfoModel
                {
                    ImageURI = ""
                };
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                Style = (Style)Application.Current.Resources["PlayerBattleMediumStyle"],
                Source = data.ImageURI
            };

            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                Children = {
                    PlayerImage,
                },
            };

            return PlayerStack;
        }
        #endregion PlayerOverview

        #region BattleScene

        /// <summary>
        /// Stores player's position
        /// </summary>
        public void InitializePlayerPositions()
        {
            bool front = true;
            var y = 0;

            // Draw the characters (ghosts)
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Character).ToList())
            {
                if (front)
                {
                    data.X = 3;
                }
                else
                {
                    data.X = 4;
                }

                data.Y = y;

                y++;
                front = !front;
            }

            y = 0;
            front = true;

            // Draw the monster (slimes)
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.ToList())
            {
                if (front)
                {
                    data.X = 1;
                }
                else
                {
                    data.X = 0;
                }

                data.Y = y;

                y++;
                front = !front;
            }
        }

        /// <summary>
        /// Draw the Battle Player Positions
        /// </summary>
        public void DrawPlayerBattle()
        {
            var BattlePlayerList = BattlePlayers.Children.ToList();

            foreach (var data in BattlePlayerList)
            {
                _ = BattlePlayers.Children.Remove(data);
            }

            // Draw the ghosts
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Character).ToList())
            {
                BattlePlayers.Children.Add(BattlePlayerDisplayBox(data), data.X, data.Y);
            }

            // Draw the slimes
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.ToList())
            {
                BattlePlayers.Children.Add(BattlePlayerDisplayBox(data), data.X, data.Y);
            }

            // Draw ghost graves
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.CharacterModelRoundDeathList.ToList())
            {
                BattlePlayers.Children.Add(BattlePlayerDisplayBox(data), data.X, data.Y);
            }

            // Draw slime graves
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.MonsterModelRoundDeathList.ToList())
            {
                BattlePlayers.Children.Add(BattlePlayerDisplayBox(data), data.X, data.Y);
            }
        }

        /// <summary>
        /// Put the Player into a Player Battle Box
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout BattlePlayerDisplayBox(PlayerInfoModel data)
        {
            var opacity = 0;

            if (data == null)
            {
                data = new PlayerInfoModel
                {
                    ImageURI = ""
                };
            }

            var image = data.ImageURI;

            // Avoids divide by zero
            if (data.MaxHealth == 0)
            {
                data.MaxHealth = 1;
            }

            // For characters gifs
            if (data.PlayerType == PlayerTypeEnum.Monster)
            {
                opacity = 1;
            }

            // Draw graves if player is dead
            if (!data.Alive)
            {
                image = "grave.png";
                opacity = 1;
            }

            // Hookup the image
            var PlayerImageButton = new ImageButton
            {
                Style = (Style)Application.Current.Resources["PlayerSmallImageStyle"],
                Source = image,
                Opacity = opacity
            };

            var PlayerImage = new Image
            {
                Style = (Style)Application.Current.Resources["PlayerSmallStyle"],
                Source = image.Replace("png", "gif"),
                IsAnimationPlaying = true
            };

            var IndividualHealth = new ProgressBar
            {
                Style = (Style)Application.Current.Resources["IndividualProgress"],
                Progress = (float)data.CurrentHealth / (float)data.MaxHealth,
                IsVisible = EmptyBattleSlot(data.CurrentHealth)
            };

            // Put the player health progress and image inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["VerticalStack"],
                Children = {
                    IndividualHealth,
                    PlayerImage,
                    PlayerImageButton
                },
            };

            return PlayerStack;
        }

        /// <summary>
        /// Check if the battle slot is empty
        /// </summary>
        /// <param name="maxHealth"></param>
        /// <returns></returns>
        public bool EmptyBattleSlot(int maxHealth)
        {
            bool notEmpty = false;

            if (maxHealth != 0)
            {
                notEmpty = true;
            }

            return notEmpty;
        }

        #endregion BattleScene

        #region BattleHealthProgress

        /// <summary>
        /// Calculates ghost total health
        /// </summary>
        public double CalculateCharacterTotalHealth()
        {
            var total = 0.0;

            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Character).ToList())
            {
                total += data.MaxHealth;
            }

            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.CharacterModelDeathList.ToList())
            {
                total += data.MaxHealth;
            }

            return total;
        }

        /// <summary>
        /// Calculates ghost current total health
        /// </summary>
        public double CalculateCharacterCurrentTotalHealth()
        {
            var total = 0.0;

            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Character).ToList())
            {
                total += data.CurrentHealth;
            }

            return total;
        }

        /// <summary>
        /// Creates total progress label for ghosts
        /// </summary>
        public void CreateCharactersProgress()
        {
            var CharacterHealthList = CharacterHealth.Children.ToList();

            foreach (var item in CharacterHealthList)
            {
                _ = CharacterHealth.Children.Remove(item);
            }

            var image = new Image
            {
                Source = "regular_ghost.png",
                Style = (Style)Application.Current.Resources["ImageBattleSmallStyle"]
            };

            var label = new Label
            {
                Text = "Ghost: " + CalculateCharacterCurrentTotalHealth().ToString(),
                Style = (Style)Application.Current.Resources["LabelStyle"]
            };

            var totalProgress = new ProgressBar
            {
                Progress = CalculateCharacterCurrentTotalHealth() / CalculateCharacterTotalHealth(),
                Style = (Style)Application.Current.Resources["TotalProgress"]
            };

            var labelStack = new StackLayout { Style = (Style)Application.Current.Resources["TotalProgressStack"] };

            labelStack.Children.Add(label);
            labelStack.Children.Add(totalProgress);

            CharacterHealth.Children.Add(image);
            CharacterHealth.Children.Add(labelStack);
        }

        /// <summary>
        /// Calculates slimes total health
        /// </summary>
        public double CalculateMonsterTotalHealth()
        {
            var total = 0.0;

            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.ToList())
            {
                total += data.MaxHealth;
            }

            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.MonsterModelDeathList.ToList())
            {
                total += data.MaxHealth;
            }

            return total;
        }

        /// <summary>
        /// Calculates slimes current total health
        /// </summary>
        public double CalculateMonsterCurrentTotalHealth()
        {
            var total = 0.0;

            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.ToList())
            {
                total += data.CurrentHealth;
            }

            return total;
        }


        /// <summary>
        /// Creates total progress label for slimes
        /// </summary>
        public void CreateMonstersProgress()
        {
            var MonsterHealthList = MonsterHealth.Children.ToList();

            foreach (var item in MonsterHealthList)
            {
                _ = MonsterHealth.Children.Remove(item);
            }

            var image = new Image
            {
                Source = "slime.png",
                Style = (Style)Application.Current.Resources["ImageBattleSmallStyle"]
            };

            var label = new Label
            {
                Text = "Slime: " + CalculateMonsterCurrentTotalHealth().ToString(),
                Style = (Style)Application.Current.Resources["LabelStyle"]
            };

            var totalProgress = new ProgressBar
            {
                Progress = CalculateMonsterCurrentTotalHealth() / CalculateMonsterTotalHealth(),
                Style = (Style)Application.Current.Resources["TotalProgress"]
            };

            var labelStack = new StackLayout { Style = (Style)Application.Current.Resources["TotalProgressStack"] };

            labelStack.Children.Add(label);
            labelStack.Children.Add(totalProgress);

            MonsterHealth.Children.Add(image);
            MonsterHealth.Children.Add(labelStack);

        }

        #endregion BattleHealthProgress

        #region CombatGrammar

        /// <summary>
        /// Displays game message in text
        /// </summary>
        public void GameMessage()
        {
            // Output The Message that happened.
            BattleMessages.Text = string.Format("{0} {1}", BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.TurnMessage, BattleMessages.Text);

            Debug.WriteLine(BattleMessages.Text);

            if (!string.IsNullOrEmpty(BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.LevelUpMessage))
            {
                BattleMessages.Text = string.Format("\n{0} {1}\n", BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.LevelUpMessage, BattleMessages.Text);
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.LevelUpMessage = "";
            }

            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.TurnMessage = " ";
        }

        /// <summary>
        /// Battle sequence
        /// </summary>
        public void Rumble()
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;

            // Hold the current state
            var RoundCondition = BattleEngineViewModel.Instance.Engine.Round.RoundNextTurn();

            // Output the Message of what happened.
            GameMessage();

            // Battle UI elements
            CreateCharactersProgress();
            CreateMonstersProgress();
            DrawPlayerBattle();

            // Show the outcome on the Board
            if (RoundCondition == RoundEnum.NewRound)
            {                
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.NewRound;

                // Pause
                Debug.WriteLine("New Round");

                // Clear dead ghosts and slimes in the previous round
                foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.CharacterModelRoundDeathList.ToList())
                {
                    _ = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.CharacterModelRoundDeathList.Remove(data);
                }

                foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.MonsterModelRoundDeathList.ToList())
                {
                    _ = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.MonsterModelRoundDeathList.Remove(data);
                }

                // Redraw UI elements
                DrawUniqueItems();
                NewRoundPage();

                // Show the Round Over, after that is cleared, it will show the New Round Dialog
                return;
            }

            // Check for Game Over
            if (RoundCondition == RoundEnum.GameOver)
            {
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.GameOver;

                // Wrap up
                _ = BattleEngineViewModel.Instance.Engine.EndBattle();

                // Pause
                Debug.WriteLine("Game Over");

                ShowGameOverPage();

                return;
            }
        }

        /// <summary>
        /// Sets the MonsterList to visible if Player's turn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ShowMonster_Clicked(object sender, EventArgs e)
        {
            MonsterList.IsVisible = true;
            TreasureMessageBox.IsVisible = false;
        }

        /// <summary>
        /// Sets the selected Monster as a Target if Player's turn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnMonsterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(args.SelectedItem as PlayerInfoModel);
            Attack.IsEnabled = false;
            MonsterList.IsVisible = false;
            NextButton.IsEnabled = true;

            Rumble();

            // Refresh
            MonsterListView.ItemsSource = null;
            MonsterListView.SelectedItem = null;
            MonsterListView.ItemsSource = BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList;
        }

        /// <summary>
        /// Get Next Attacker 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GetAttacker(object sender, EventArgs e)
        {
            PlayerInfoModel next = BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn();

            // While loop to make Monsters Auto Attack (unnecessary for Player to keep pressing next turn if not Player turn)
            while (next.PlayerType == PlayerTypeEnum.Monster)
            {
                _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(next);
                PlayerInfoModel nextTarget = BattleEngineViewModel.Instance.Engine.Round.Turn.AttackChoice(next);

                // null target means all characters are dead 
                if (nextTarget == null)
                {
                    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.GameOver;

                    // Wrap up
                    _ = BattleEngineViewModel.Instance.Engine.EndBattle();

                    Debug.WriteLine("Game Over");

                    ShowGameOverPage();

                    break;
                }

                // Next target still alive, proceed with loop
                if (nextTarget.Alive)
                {
                    _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(nextTarget);
                    Rumble();
                    next = BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn();
                }
            }

            // If next Attacker is a Character, show the combat UI for Character
            if (next.PlayerType == PlayerTypeEnum.Character)
            {
                _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(next);
                Attack.IsEnabled = true;               
                NextButton.IsEnabled = false;
                Messages.IsVisible = true;
                return;
            }
        }

        /// <summary>
        /// Show Game Over Page
        /// </summary>
        public void ShowGameOverPage()
        {
            TotalRoundsCleared.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.RoundCount.ToString();
            TotalMonstersSlain.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.MonsterSlainNumber.ToString();
            TotalExperienceGained.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ExperienceGainedTotal.ToString();
            TotalItemsFound.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Count().ToString();

            BackgroundImageSource = "afterbattle_background.png";
            PlayerOverview.IsVisible = false;
            BattleView.IsVisible = false;
            MessageStack.IsVisible = false;
            ButtonGroup.IsVisible = false;
            GameOver.IsVisible = true;
        }

        /// <summary>
        /// Show Round Over Page
        /// </summary>
        public async void NewRoundPage()
        {
            await Navigation.PushModalAsync(new RoundOverPage());
        }

        #endregion

        #region TreasureItems

        /// <summary>
        /// Draw monster's unique items
        /// </summary>
        public void DrawUniqueItems()
        {
            var itemPicList = new List<String>();
            var item = ItemIndexViewModel.Instance;

            var itemList = ItemsDrop.Children.ToList();            

            var itemImages = new StackLayout
            { 
                Style = (Style)Application.Current.Resources["HorizontalStack"], 
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            var itemText = new Label
            {
                Text = "Possible Drops",
                Style = (Style)Application.Current.Resources["CenterMessageStyle"],
                Margin = new Thickness(0, 10, 0, 10),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.FromHex("#3F2178")
            };

            foreach (var data in itemList)
            {
                _ = ItemsDrop.Children.Remove(data);
            }

            foreach (var monster in BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.ToList())
            {
                // Skip if unique item is not on the list
                if (item.GetItem(monster.UniqueItem) == null)
                {
                    continue;
                }

                itemPicList.Add(item.GetItem(monster.UniqueItem).ImageURI);
            }

            // Get distinct drop items
            foreach (var data in itemPicList.Distinct())
            {
                 itemImages.Children.Add(new Image 
                { 
                    Source =  data,
                    Style = (Style)Application.Current.Resources["ImageMediumStyle"]
                });
            }

            ItemsDrop.Children.Add(itemText);
            ItemsDrop.Children.Add(itemImages);
        }

        #endregion TreasureItems

        #region UI

        /// <summary>
        /// Reset UI elements
        /// </summary>
        public void ResetUIElements()
        {
            PlayerOverview.IsVisible = true;
            BeginBattle.IsVisible = true;
            BattleSettings.IsVisible = true;

            BattleView.IsVisible = false;
            MessageStack.IsVisible = false;
            ButtonGroup.IsVisible = false;
            CombatUI.IsVisible = false;
            Messages.IsVisible = false;
        }

        /// <summary>
        ///  Clears the messages on the UX
        /// </summary>
        public void ClearMessages()
        {
            BattleMessages.Text = "";
        }

        /// <summary>
        /// Begin battle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BeginButton_Clicked(object sender, EventArgs e)
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;

            PlayerOverview.IsVisible = false;
            BeginBattle.IsVisible = false;
            BattleSettings.IsVisible = false;

            BattleView.IsVisible = true;
            MessageStack.IsVisible = true;
            ButtonGroup.IsVisible = true;
            CombatUI.IsVisible = true;
        }

        /// <summary>
        /// Shows the slimes drop items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TreasureButton_Clicked(object sender, EventArgs e)
        {
            bool clicked = TreasureMessageBox.IsVisible;

            if (Messages.IsVisible)
            {
                Messages.IsVisible = false;
            }

            TreasureMessageBox.IsVisible = !clicked;
        }

        /// <summary>
        /// Goes to Pick Characters Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Exit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Goes to Battle Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Settings_Clicked(object sender, EventArgs e)
        {
            await ShowBattleSettingsPage();
        }

        /// <summary>
        /// Show Settings
        /// </summary>
        public async Task ShowBattleSettingsPage()
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new BattleSettingsPage());
        }

        /// <summary>
        /// Refreshes page after new round
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            ShowBattleMode();
        }

        /// <summary>
        /// Show the proper Battle Mode
        /// </summary>
        public void ShowBattleMode()
        {
            // If running in UT mode, 
            if (UnitTestSetting)
            {
                return;
            }
            MonsterListView.ItemsSource = null;
            MonsterListView.SelectedItem = null;
            MonsterListView.ItemsSource = BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList;

            ResetUIElements();

            // Redraws UI elements
            ClearMessages();
            InitializePlayerPositions();
            DrawPlayerBoxes();
            CreateCharactersProgress();
            CreateMonstersProgress();
            DrawPlayerBattle();
            DrawUniqueItems();            
        }
        #endregion
    }
}