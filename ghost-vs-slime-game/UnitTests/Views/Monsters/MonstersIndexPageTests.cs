﻿using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

using Game;
using Game.Views;
using Game.Models;
using Game.ViewModels;

namespace UnitTests.Views
{
    [TestFixture]
    public class MonsterIndexPageTests : MonsterIndexPage
    {
        App app;
        MonsterIndexPage page;

        public MonsterIndexPageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new MonsterIndexPage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void MonsterIndexPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void MonsterIndexPage_AddMonster_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Add_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterIndexPage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            _ = OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterIndexPage_OnMonsterSelected_Clicked_Default_Should_Pass()
        {
            // Arrange

            var selectedMonster = new MonsterModel();

            var selectedMonsterChangedEventArgs = new SelectedItemChangedEventArgs(selectedMonster, 0);

            // Act
            page.OnItemSelected(null, selectedMonsterChangedEventArgs);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterIndexPage_OnMonsterSelected_Clicked_Invalid_Null_Should_Fail()
        {
            // Arrange

            var selectedMonsterChangedEventArgs = new SelectedItemChangedEventArgs(null, 0);

            // Act
            page.OnItemSelected(null, selectedMonsterChangedEventArgs);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterIndexPage_OnAppearing_Valid_Should_Pass()
        {
            // Arrange
            var ViewModel = MonsterIndexViewModel.Instance;

            // Act
            OnAppearing();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
            Assert.IsNotNull(ViewModel);
        }

        [Test]
        public void MonsterIndexPage_OnAppearing_Valid_Empty_Should_Pass()
        {
            // Arrange

            var ViewModel = MonsterIndexViewModel.Instance;
            ViewModel.Dataset.Clear();

            // Act
            OnAppearing();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterIndexPage_UpdateMonster_Swipe_Default_Should_Pass()
        {
            // Arrange
            var data = new MonsterModel();

            var item = new SwipeItem { BindingContext = data };

            // Act
            page.Swipe_Update(item, null);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void MonsterIndexPage_DeleteMonster_Swipe_Default_Should_Pass()
        {
            // Arrange
            var data = new MonsterModel();

            var item = new SwipeItem { BindingContext = data };

            // Act
            page.Swipe_Delete(item, null);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }
    }
}