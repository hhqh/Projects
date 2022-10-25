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
    public class BeginGamePageTests
    {
        App app;
        BeginGamePage page;

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new BeginGamePage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void BeiginGamePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BeginGamePage_Begin_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Begin_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void BeginGamePage_AutoBattle_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.AutoBattle_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }
    }
}