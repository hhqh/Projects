﻿using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;

namespace Game.Views
{
    /// <summary>
    /// Item Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemUpdatePage : ContentPage
    {
        // View Model for Item
        public readonly GenericViewModel<ItemModel> ViewModel;

        // Hold a copy of the original data for Cancel to use
        public ItemModel DataCopy;

        // Empty Constructor for Tests
        public ItemUpdatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor that takes and existing data item
        /// </summary>
        public ItemUpdatePage(GenericViewModel<ItemModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;


            // Make a copy of the character for cancel to restore
            DataCopy = new ItemModel(data.Data);

            this.ViewModel.Title = "Update " + data.Title;

            //Need to make the SelectedItem a string, so it can select the correct item.
            LocationPicker.SelectedItem = data.Data.Location.ToMessage();
            AttributePicker.SelectedItem = data.Data.Attribute.ToString();
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
            }

            if (string.IsNullOrEmpty(NameEntry.Text))
            {
                return;
            }

            if (string.IsNullOrEmpty(DescriptionEntry.Text))
            {
                return;
            }

            if (LocationPicker.SelectedIndex == -1)
            {
                return;
            }

            if (AttributePicker.SelectedIndex == -1)
            {
                return;
            }

            MessagingCenter.Send(this, "Update", ViewModel.Data);
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            ViewModel.Data.Update(DataCopy);

            await Navigation.PopModalAsync();
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
                return;
            }
            
            NameEntry.Placeholder = "";
            
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
                return;
            }
            
            DescriptionEntry.Placeholder = "";
            
        }

        /// <summary>
        /// Catch the change to the Slider for Range
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Range_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            var value = Math.Round(e.NewValue / 1);
            RangeValue.Text = string.Format("{0}", value);
        }

        /// <summary>
        /// Catch the change to the stepper for Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Value_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            var value = Math.Round(e.NewValue / 1);
            ValueValue.Text = string.Format("{0}", value);
        }

        /// <summary>
        /// Catch the change to the stepper for Damage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Damage_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            var value = Math.Round(e.NewValue / 1);
            DamageValue.Text = string.Format("{0}", value);
        }
    }
}