﻿using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;

namespace Game.Views
{
    /// <summary>
    /// Index Page
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterIndexPage : ContentPage
    {
        // The view model, used for data binding
        readonly CharacterIndexViewModel ViewModel = CharacterIndexViewModel.Instance;

        // Empty Constructor for UTs
        public CharacterIndexPage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Index Page
        /// 
        /// Get the CharacterIndexView Model
        /// </summary>
        public CharacterIndexPage()
        {
            InitializeComponent();

            BindingContext = ViewModel;
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            CharacterModel data = args.SelectedItem as CharacterModel;
            if (data == null)
            {
                return;
            }

            // Open the Read Page
            await Navigation.PushAsync(new CharacterReadPage(new GenericViewModel<CharacterModel>(data)));

            // Manually deselect Character.
            CharactersListView.SelectedItem = null;
        }

        /// <summary>
        /// Call to Add a new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CharacterCreatePage(new GenericViewModel<CharacterModel>())));
        }

        /// <summary>
        /// Refresh the list on page appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            // If no data, then set it for needing refresh
            if (ViewModel.Dataset.Count == 0)
            {
                _ = ViewModel.SetNeedsRefresh(true);
            }

            // If the needs Refresh flag is set update it
            if (ViewModel.NeedsRefresh())
            {
                ViewModel.LoadDatasetCommand.Execute(null);
            }

            BindingContext = ViewModel;
        }

        /// <summary>
        /// Update character in list by swiping right
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Swipe_Update(object sender, EventArgs e)
        {
            var selectedItem = (sender as SwipeItem).BindingContext as CharacterModel;

            await Navigation.PushModalAsync(new NavigationPage(new CharacterUpdatePage(new GenericViewModel<CharacterModel>(selectedItem))));
            _ = await Navigation.PopAsync();
        }

        /// <summary>
        /// Delete character in list by swiping right
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Swipe_Delete(object sender, EventArgs e)
        {
            var selectedItem = (sender as SwipeItem).BindingContext as CharacterModel;

            await Navigation.PushModalAsync(new NavigationPage(new CharacterDeletePage(new GenericViewModel<CharacterModel>(selectedItem))));
            _ = await Navigation.PopAsync();
        }
    }
}