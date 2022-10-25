using Game.Models;
using Game.ViewModels;

using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    /// Create Item
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoreCreatePage : ContentPage
    {
        // The item to create
        public GenericViewModel<ScoreModel> ViewModel { get; set; }

        // Constructor for Unit Testing
        public ScoreCreatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public ScoreCreatePage(GenericViewModel<ScoreModel> data)
        {
            InitializeComponent();

            data.Data = new ScoreModel();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Create";
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
                ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
            }

            if (string.IsNullOrEmpty(NameEntry.Text))
            {
                return;
            }

            if (string.IsNullOrEmpty(ScoreEntry.Text))
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
                NameEntry.Placeholder = "Please enter a name.";
            }

            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                NameEntry.Placeholder = "";
            }
        }

        /// <summary>
        /// Verifies if Score Entry is empty 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Score_ValidateOnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                ScoreEntry.Placeholder = "Please enter a score.";
            }

            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                ScoreEntry.Placeholder = "";
            }
        }
    }
}