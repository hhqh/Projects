using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.ViewModels;
using Game.Models;
using System.Collections.Generic;

namespace Game.Views
{
    /// <summary>
    /// Monster Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterUpdatePage : ContentPage
    {
        // The Monster to create
        public GenericViewModel<MonsterModel> ViewModel { get; set; }

        // Hold a copy of the original data for Cancel to use
        public MonsterModel DataCopy;

        // Empty Constructor for UTs
        public MonsterUpdatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public MonsterUpdatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            // Make a copy of the character for cancel to restore
            DataCopy = new MonsterModel(data.Data);

            this.ViewModel.Title = "Update " + data.Title;

            List<ItemModel> items = new List<ItemModel>();

            items.AddRange(ItemIndexViewModel.Instance.GetAllItems());

            ItemPicker.ItemsSource = items;
           
            DifficultyPicker.SelectedItem = data.Data.Difficulty.ToString();

            _ = UpdatePageBindingContext();
        }

        /// <summary>
        /// Redo the Binding to cause a refresh
        /// </summary>
        /// <returns></returns>
        public bool UpdatePageBindingContext()
        {
            // Temp store off the Difficulty
            var difficulty = this.ViewModel.Data.Difficulty;
            var uniqueitem = this.ViewModel.Data.UniqueItem;

            // Clear the Binding and reset it
            BindingContext = null;
            BindingContext = this.ViewModel;

            ViewModel.Data.Difficulty = difficulty;
            ViewModel.Data.UniqueItem = uniqueitem;

            DifficultyPicker.SelectedIndex = 0;
            ItemPicker.SelectedIndex = 0;

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

            if (DifficultyPicker.SelectedIndex == -1)
            {
                return;
            }

            if (ItemPicker.SelectedIndex == -1)
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
            ViewModel.Data.Update(DataCopy);

            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Catch the change to the Slider for Range
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Attack_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            var value = Math.Round(e.NewValue / 1);
            AttackValue.Text = string.Format("{0}", value);
        }

        /// <summary>
        /// Catch the change to the stepper for Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Defense_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            var value = Math.Round(e.NewValue / 1);
            DefenseValue.Text = string.Format("{0}", value);
        }

        /// <summary>
        /// Catch the change to the stepper for Damage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Speed_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            var value = Math.Round(e.NewValue / 1);
            SpeedValue.Text = string.Format("{0}", value);
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
        /// Assign item from itemModel to unique item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void UniqueItem_Changed(object sender, EventArgs args)
        {
            var picker = (Picker)sender;

            if (picker.SelectedIndex != -1)
            {
                ViewModel.Data.UniqueItem = picker.Items[picker.SelectedIndex];
            }

        }
    }
}