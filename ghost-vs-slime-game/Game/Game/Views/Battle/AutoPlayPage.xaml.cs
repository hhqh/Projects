using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.Models;
using Game.ViewModels;
using Game.Engine.EngineInterfaces;
using System.Linq;


namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AutoPlayPage : ContentPage
    {
        public IAutoBattleInterface AutoBattle = BattleEngineViewModel.Instance.AutoBattleEngine;
        /// <summary>
        /// Constructor
        /// </summary>
        public AutoPlayPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// Goes to Pick Characters Page
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Yes_Clicked(object sender, EventArgs e)
        {
                // Call into Auto Battle from here to do the Battle...

                // To See Level UP happening, a character needs to be close to the next level
                var Character = new CharacterModel
                {
                    ExperienceTotal = 300,    // Enough for next level
                    Name = "Mike Level Example",
                    Speed = 1,    // Go first
                };

                var CharacterPlayer = new PlayerInfoModel(Character);

                BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

                _ = await AutoBattle.RunAutoBattle();

                var BattleMessage = string.Format("Done {0} Rounds", AutoBattle.Battle.EngineSettings.BattleScore.RoundCount);

                BattleMessageValue.Text = BattleMessage;
        }

        /// <summary>
        /// Go to autobattle page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            _ = await Navigation.PopAsync();
        }

    }
}