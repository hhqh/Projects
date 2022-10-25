using Game.Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace UnitTests.Helpers
{
    [TestFixture]
    public class ItemLocationEnumHelperTests
    {

        [Test]
        public void ItemLocationEnumHelper_ItemLocationEnum_Valid_1Time_6sided_Should_Between_1_and_6()
        {
            // Arrange

            // Act
            var result = ItemLocationEnumHelper.GetListItem;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ItemLocationEnumHelper_GetListCharacter_Should_Pass()
        {
            // Arrange

            // Instantiate a new ItemLocation Base, should have default of 1 for all values
            var myDataList = ItemLocationEnumHelper.GetListCharacter;

            // Get Expected set
            var myList = Enum.GetNames(typeof(ItemLocationEnum)).ToList();
            var myExpectedList = myList.Where(a =>
                                          a.ToString() != ItemLocationEnum.Unknown.ToString() &&
                                           a.ToString() != ItemLocationEnum.Finger.ToString()
                                            )
                                            .OrderBy(a => a)
                                            .ToList();

            // Act

            // Make sure each item is in the list
            foreach (var item in myDataList)
            {
                var found = false;
                foreach (var expected in myExpectedList)
                {
                    if (item == expected)
                    {
                        found = true;
                        break;
                    }
                }

                // Assert
                Assert.AreEqual(true, found, "item : " + item + TestContext.CurrentContext.Test.Name);
            }

            // reverse it, to make sure the list has each item
            // Make sure each item is in the list
            foreach (var expected in myExpectedList)
            {
                var found = false;
                {
                    foreach (var item in myDataList)
                        if (item == expected)
                        {
                            found = true;
                            break;
                        }
                }

                // Assert
                Assert.AreEqual(true, found, "expected : " + expected + TestContext.CurrentContext.Test.Name);
            }

        }

        [Test]
        public void ItemLocationEnumHelper_GetListItem_Should_Pass()
        {
            // Arrange

            // Instantiate a new ItemLocation Base, should have default of 1 for all values
            var myDataList = ItemLocationEnumHelper.GetListItem;

            // Get Expected set
            var myList = Enum.GetNames(typeof(ItemLocationEnum)).ToList();
            var myExpectedList = myList.Where(a =>
                                            a.ToString() != ItemLocationEnum.Unknown.ToString() &&
                                            a.ToString() != ItemLocationEnum.LeftFinger.ToString() &&
                                            a.ToString() != ItemLocationEnum.RightFinger.ToString()
                                            )
                                            .OrderBy(a => a)
                                            .ToList();

            // Act

            // Make sure each item is in the list
            foreach (var item in myDataList)
            {
                var found = false;
                foreach (var expected in myExpectedList)
                {
                    if (item == expected)
                    {
                        found = true;
                        break;
                    }
                }

                // Assert
                Assert.AreEqual(true, found, "item : " + item + TestContext.CurrentContext.Test.Name);
            }

            // reverse it, to make sure the list has each item
            // Make sure each item is in the list
            foreach (var expected in myExpectedList)
            {
                var found = false;
                {
                    foreach (var item in myDataList)
                        if (item == expected)
                        {
                            found = true;
                            break;
                        }
                }

                // Assert
                Assert.AreEqual(true, found, "expected : " + expected + TestContext.CurrentContext.Test.Name);
            }

        }

        [Test]
        public void ItemLocationEnumHelper_GetListMessageCharacter_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnumHelper.GetListMessageCharacter;

            // Reset

            // Assert
            Assert.AreEqual(7, result.Count);
        }

        [Test]
        public void ItemLocationEnumHelper_ConvertStringToEnum_Should_Pass()
        {
            // Arrange

            var myList = Enum.GetNames(typeof(ItemLocationEnum)).ToList();

            ItemLocationEnum myActual;
            ItemLocationEnum myExpected;

            // Act

            foreach (var item in myList)
            {
                myActual = ItemLocationEnumHelper.ConvertStringToEnum(item);
                myExpected = (ItemLocationEnum)Enum.Parse(typeof(ItemLocationEnum), item);

                // Assert
                Assert.AreEqual(myExpected, myActual, "string: " + item + TestContext.CurrentContext.Test.Name);
            }
        }

        #region ConvertMessageToEnum

        [Test]
        public void ItemLocationEnumHelper_ConvertMessageToEnum_Feet_Should_Pass()
        {
            // Arrange
            var message = "Feet";

            // Act
            var Actual = ItemLocationEnumHelper.ConvertMessageToEnum(message);
            var Expected = ItemLocationEnum.Feet;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ItemLocationEnumHelper_ConvertMessageToEnum_Head_Should_Pass()
        {
            // Arrange
            var message = "Head";

            // Act
            var Actual = ItemLocationEnumHelper.ConvertMessageToEnum(message);
            var Expected = ItemLocationEnum.Head;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ItemLocationEnumHelper_ConvertMessageToEnum_Necklass_Should_Pass()
        {
            // Arrange
            var message = "Necklass";

            // Act
            var Actual = ItemLocationEnumHelper.ConvertMessageToEnum(message);
            var Expected = ItemLocationEnum.Necklass;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ItemLocationEnumHelper_ConvertMessageToEnum_Unknown_Should_Pass()
        {
            // Arrange
            var message = "Unknown";

            // Act
            var Actual = ItemLocationEnumHelper.ConvertMessageToEnum(message);
            var Expected = ItemLocationEnum.Unknown;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ItemLocationEnumHelper_ConvertMessageToEnum_PrimaryHand_Should_Pass()
        {
            // Arrange
            var message = "Primary Hand";

            // Act
            var Actual = ItemLocationEnumHelper.ConvertMessageToEnum(message);
            var Expected = ItemLocationEnum.PrimaryHand;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ItemLocationEnumHelper_ConvertMessageToEnum_Finger_Should_Pass()
        {
            // Arrange
            var message = "Any Finger";

            // Act
            var Actual = ItemLocationEnumHelper.ConvertMessageToEnum(message);
            var Expected = ItemLocationEnum.Finger;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ItemLocationEnumHelper_ConvertMessageToEnum_LeftFinger_Should_Pass()
        {
            // Arrange
            var message = "Left Finger";

            // Act
            var Actual = ItemLocationEnumHelper.ConvertMessageToEnum(message);
            var Expected = ItemLocationEnum.LeftFinger;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ItemLocationEnumHelper_ConvertMessageToEnum_OffHand_Should_Pass()
        {
            // Arrange
            var message = "Off Hand";

            // Act
            var Actual = ItemLocationEnumHelper.ConvertMessageToEnum(message);
            var Expected = ItemLocationEnum.OffHand;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ItemLocationEnumHelper_ConvertMessageToEnum_RightFinger_Should_Pass()
        {
            // Arrange
            var message = "Right Finger";

            // Act
            var Actual = ItemLocationEnumHelper.ConvertMessageToEnum(message);
            var Expected = ItemLocationEnum.RightFinger;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ItemLocationEnumHelper_ConvertMessageToEnum_Bogus_Should_Pass()
        {
            // Arrange
            var message = "Bogus";

            // Act
            var Actual = ItemLocationEnumHelper.ConvertMessageToEnum(message);
            var Expected = ItemLocationEnum.Unknown;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        #endregion ConvertMessageToEnum

        #region GetLocationByPosition

        [Test]
        public void ItemLocationEnumHelper_GetLocationByPosition_1_Should_Pass()
        {
            // Arrange

            var value = 1;

            // Act
            var Actual = ItemLocationEnumHelper.GetLocationByPosition(value);
            var Expected = ItemLocationEnum.Head;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ItemLocationEnumHelper_GetLocationByPosition_2_Should_Pass()
        {
            // Arrange

            var value = 2;

            // Act
            var Actual = ItemLocationEnumHelper.GetLocationByPosition(value);
            var Expected = ItemLocationEnum.Necklass;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ItemLocationEnumHelper_GetLocationByPosition_3_Should_Pass()
        {
            // Arrange

            var value = 3;

            // Act
            var Actual = ItemLocationEnumHelper.GetLocationByPosition(value);
            var Expected = ItemLocationEnum.PrimaryHand;

            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ItemLocationEnumHelper_GetLocationByPosition_4_Should_Pass()
        {
            // Arrange

            var value = 4;

            // Act
            var Actual = ItemLocationEnumHelper.GetLocationByPosition(value);
            var Expected = ItemLocationEnum.OffHand;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ItemLocationEnumHelper_GetLocationByPosition_5_Should_Pass()
        {
            // Arrange

            var value = 5;

            // Act
            var Actual = ItemLocationEnumHelper.GetLocationByPosition(value);
            var Expected = ItemLocationEnum.RightFinger;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ItemLocationEnumHelper_GetLocationByPosition_6_Should_Pass()
        {
            // Arrange

            var value = 6;

            // Act
            var Actual = ItemLocationEnumHelper.GetLocationByPosition(value);
            var Expected = ItemLocationEnum.LeftFinger;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ItemLocationEnumHelper_GetLocationByPosition_7_Should_Pass()
        {
            // Arrange

            var value = 7;

            // Act
            var Actual = ItemLocationEnumHelper.GetLocationByPosition(value);
            var Expected = ItemLocationEnum.Feet;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        #endregion GetLocationByPosition
    }
}