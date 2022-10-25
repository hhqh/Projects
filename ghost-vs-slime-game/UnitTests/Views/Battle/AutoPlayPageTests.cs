using NUnit.Framework;

using Game;
using Game.Views;
using Xamarin.Forms.Mocks;
using Xamarin.Forms;
using Game.Helpers;
using Game.Models;

namespace UnitTests.Views
{
    [TestFixture]
    public class AutoPlayPageTests
    {
        App app;
        AutoPlayPage page;

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new AutoPlayPage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void AutoPlayPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void AutoPlayPage_Yes_Click_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Yes_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void AutoPlayPage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }
    }
}