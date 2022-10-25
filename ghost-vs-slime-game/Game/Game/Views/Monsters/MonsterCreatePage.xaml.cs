using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.Models;
using Game.ViewModels;
using System.Collections.Generic;

namespace Game.Views
{
    /// <summary>
    /// Create Monster
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterCreatePage : ContentPage
    {
        // The Monster to create
        public GenericViewModel<MonsterModel> ViewModel { get; set; }

        // Empty Constructor for UTs
        public MonsterCreatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public MonsterCreatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            List<ItemModel> items = new List<ItemModel>();

            items.AddRange(ItemIndexViewModel.Instance.GetAllItems());

            ItemPicker.ItemsSource = items;

            data.Data = new MonsterModel();
            this.ViewModel = data;

            BindingContext = this.ViewModel;

            this.ViewModel.Title = "Create";

            _ = UpdatePageBindingContext();
        }

        /// <summary>
        /// Redo the Binding to cause a refresh
        /// </summary>
        /// <returns></returns>
        public bool UpdatePageBindingContext()
        {
            // Temp store off the difficulty
            var difficulty = this.ViewModel.Data.Difficulty;

            // Clear the Binding and reset it
            BindingContext = null;
            BindingContext = this.ViewModel;

            ViewModel.Data.Difficulty = difficulty;

            return true;
        }

        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = new MonsterModel().ImageURI;
            }

            if (string.IsNullOrEmpty(NameEntry.Text))
            {
                return;
            }

            if (string.IsNullOrEmpty(DescriptionEntry.Text))
            {
                return;
            }

            if (ItemPicker.SelectedIndex == -1)
            {
                return;
            }

            if (DifficultyPicker.SelectedIndex == -1)
            {
                return;
            }

            MessagingCenter.Send(this, "Create", ViewModel.Data);
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Verifies if Name Entry is empty 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Name_ValidateOnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                NameEntry.Placeholder = "Please enter a valid name.";

            }
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                NameEntry.Placeholder = "";
            }
        }

        /// <summary>
        /// Verifies if Description Entry is empty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Description_ValidateOnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                DescriptionEntry.Placeholder = "Please enter a valid description.";
            }

            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                DescriptionEntry.Placeholder = "";
            }
        }

        /// <summary>
        /// Catch the change to the Stepper for Attack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Attack_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            var value = Math.Round(e.NewValue / 1);
            AttackValue.Text = string.Format("{0}", value);
        }

        /// <summary>
        /// Catch the change to the Stepper for Defense
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Defense_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            var value = Math.Round(e.NewValue / 1);
            DefenseValue.Text = string.Format("{0}", value);
        }

        /// <summary>
        /// Catch the change to the Stepper for Speed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Speed_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            var value = Math.Round(e.NewValue / 1);
            SpeedValue.Text = string.Format("{0}", value);
        }

        /// <summary>
        /// Assign item from itemModel to unique item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void UniqueItem_Changed(object sender, EventArgs args)
        {
            var picker = (Picker)sender;

            ViewModel.Data.UniqueItem = picker.Items[picker.SelectedIndex];

        }
    }
}