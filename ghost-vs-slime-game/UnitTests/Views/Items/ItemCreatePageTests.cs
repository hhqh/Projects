using NUnit.Framework;

using Game;
using Game.Views;
using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    [TestFixture]
    public class ItemCreatePageTests : ItemCreatePage
    {
        App app;
        ItemCreatePage page;

        public ItemCreatePageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new ItemCreatePage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void ItemCreatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ItemCreatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Save_Clicked_Null_Image_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data.ImageURI = null;

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            _ = OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Value_OnStepperValueChanged_Default_Should_Pass()
        {
            // Arrange

            page = new ItemCreatePage();
            var oldValue = 0.0;
            var newValue = 1.0;

            var args = new ValueChangedEventArgs(oldValue, newValue);

            // Act
            page.Value_OnStepperValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Range_OnStepperValueChanged_Default_Should_Pass()
        {
            // Arrange

            page = new ItemCreatePage();
            var oldRange = 0.0;
            var newRange = 1.0;

            var args = new ValueChangedEventArgs(oldRange, newRange);

            // Act
            page.Range_OnStepperValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Damage_OnStepperDamageChanged_Default_Should_Pass()
        {
            // Arrange
            page = new ItemCreatePage();
            var oldDamage = 0.0;
            var newDamage = 1.0;

            var args = new ValueChangedEventArgs(oldDamage, newDamage);

            // Act
            page.Damage_OnStepperValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Name_ValidateNameOnTextChanged_Invalid_Should_Pass()
        {
            // Arrange

            var data = new ItemModel();
      
            page = new ItemCreatePage();
            var NameEntry = (Editor)page.FindByName("NameEntry");
            var newString = "";

            var args = new TextChangedEventArgs(data.Name, newString);

            // Act
            page.Name_ValidateOnTextChanged(null, args);

            // Reset

            // Assert
            Assert.AreEqual("Please enter a valid name.", NameEntry.Placeholder);

        }

        [Test]
        public void ItemCreatePage_Description_ValidateNameOnTextChanged_Invalid_Should_Pass()
        {
            // Arrange

            var data = new ItemModel();

            page = new ItemCreatePage();
            var descriptionEditor = (Editor)page.FindByName("DescriptionEditor");
            var newString = "";

            var args = new TextChangedEventArgs(data.Description, newString);

            // Act
            page.Description_ValidateOnTextChanged(null, args);

            // Reset

            // Assert
            Assert.AreEqual("Please enter a valid description.", descriptionEditor.Placeholder);

        }

        [Test]
        public void ItemCreatePage_Save_Clicked_Null_Name_Should_Pass()
        {
            // Arrange
            page = new ItemCreatePage();
            var NameEntry = (Editor)page.FindByName("NameEntry");


            // Act
            NameEntry.Text = null;
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Save_Clicked_Null_Description_Should_Pass()
        {
            // Arrange
            page = new ItemCreatePage();
            var descriptionEditor = (Editor)page.FindByName("DescriptionEditor");


            // Act
            descriptionEditor.Text = null;
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Save_Clicked_Null_AttributePicker_Should_Pass()
        {
            // Arrange
            page = new ItemCreatePage();
            var locationPicker = (Picker)page.FindByName("LocationPicker");
            var attributePicker = (Picker)page.FindByName("AttributePicker");


            // Act
            locationPicker.SelectedIndex = 1;
            attributePicker.SelectedIndex = -1;
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Save_Clicked_Should_Pass()
        {
            // Arrange
            page = new ItemCreatePage();
            var locationPicker = (Picker)page.FindByName("LocationPicker");
            var attributePicker = (Picker)page.FindByName("AttributePicker");
            var NameEntry = (Editor)page.FindByName("NameEntry");
            var descriptionEditor = (Editor)page.FindByName("DescriptionEditor");


            // Act
            locationPicker.SelectedIndex = 1;
            NameEntry.Text = "Ghost";
            descriptionEditor.Text = "Spooky";
            attributePicker.SelectedIndex = 1;
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }


    }
}