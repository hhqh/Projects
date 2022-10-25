using Game.Models;
using Game.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BeginGamePage : ContentPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BeginGamePage()
        {
            InitializeComponent();

        }

        /// <summary> 
        /// Goes to Pick Characters Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Begin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PickCharactersPage());
            
        }

        /// <summary>
        /// Go to autobattle page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void AutoBattle_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AutoBattlePage());
        }

    }
}