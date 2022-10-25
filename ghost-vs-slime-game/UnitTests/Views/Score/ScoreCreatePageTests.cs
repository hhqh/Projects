using NUnit.Framework;

using Game;
using Game.Views;
using Game.ViewModels;
using Game.Models;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    [TestFixture]
    public class ScoreCreatePageTests : ScoreCreatePage
    {
        App app;
        ScoreCreatePage page;

        public ScoreCreatePageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new ScoreCreatePage(new GenericViewModel<ScoreModel>(new ScoreModel()));
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void ScoreCreatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ScoreCreatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreCreatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreCreatePage_Save_Clicked_Null_Image_Should_Pass()
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
        public void ScoreCreatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            _ = OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreCreatePage_Name_ValidateNameOnTextChanged_Invalid_Should_Pass()
        {
            // Arrange

            var data = new ScoreModel();
            var ViewModel = new GenericViewModel<ScoreModel>(data);
            page = new ScoreCreatePage(ViewModel);
            var NameEntry = (Entry)page.FindByName("NameEntry");
            var newString = "";

            var args = new TextChangedEventArgs(data.Name, newString);

            // Act
            page.Name_ValidateOnTextChanged(null, args);

            // Reset

            // Assert
            Assert.AreEqual("Please enter a name.", NameEntry.Placeholder);

        }

        [Test]
        public void ScoreCreatePage_Name_ValidateScoreOnTextChanged_Invalid_Should_Pass()
        {
            // Arrange

            var data = new ScoreModel();
            var ViewModel = new GenericViewModel<ScoreModel>(data);
            page = new ScoreCreatePage(ViewModel);
            var scoreEntry = (Entry)page.FindByName("ScoreEntry");
            var newString = "";

            var args = new TextChangedEventArgs(data.ScoreTotal.ToString(), newString);

            // Act
            page.Score_ValidateOnTextChanged(null, args);

            // Reset

            // Assert
            Assert.AreEqual("Please enter a score.", scoreEntry.Placeholder);

        }

        [Test]
        public void ScoreCreatePage_Save_Clicked_Null_Name_Should_Pass()
        {
            // Arrange
            var data = new ScoreModel();
            var ViewModel = new GenericViewModel<ScoreModel>(data);
            page = new ScoreCreatePage(ViewModel);
            var NameEntry = (Entry)page.FindByName("NameEntry");


            // Act
            NameEntry.Text = null;
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreCreatePage_Save_Clicked_Null_Score_Should_Pass()
        {
            // Arrange
            var data = new ScoreModel();
            var ViewModel = new GenericViewModel<ScoreModel>(data);
            page = new ScoreCreatePage(ViewModel);
            var scoreEntry = (Entry)page.FindByName("ScoreEntry");


            // Act
            scoreEntry.Text = null;
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}