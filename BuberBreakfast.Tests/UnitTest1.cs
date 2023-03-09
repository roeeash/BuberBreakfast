using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using System;
using System.Collections.Generic;
using Xunit;

namespace BuberBreakfast.Tests
{
    public class BreakfastTests
    {
        [Fact]
        public void Create_ReturnsError_WhenNameIsTooShort()
        {
            // Arrange
            var name = "ab";
            var description = "A delicious breakfast";
            var startDateTime = DateTime.Now.Date.AddHours(7);
            var endDateTime = DateTime.Now.Date.AddHours(10);
            var savory = new List<string>() { "Eggs", "Bacon" };
            var sweet = new List<string>() { "Pancakes", "Waffles" };

            // Act
            var result = Breakfast.Create(name, description, startDateTime, endDateTime, savory, sweet);

            // Assert
            Assert.True(result.IsError);
            Assert.Collection(result.Errors, err =>
            {
                Assert.Equal(Errors.Breakfast.InvalidName, err);
            });
        }

        [Fact]
        public void Create_ReturnsError_WhenStartDateIsLaterThanEndDate()
        {
            // Arrange
            var name = "Big Breakfast";
            var description = "A delicious breakfast";
            var startDateTime = DateTime.Now.Date.AddHours(11);
            var endDateTime = DateTime.Now.Date.AddHours(10);
            var savory = new List<string>() { "Eggs", "Bacon" };
            var sweet = new List<string>() { "Pancakes", "Waffles" };

            // Act
            var result = Breakfast.Create(name, description, startDateTime, endDateTime, savory, sweet);

            // Assert
            Assert.True(result.IsError);
            Assert.Collection(result.Errors, err =>
            {
                Assert.Equal(Errors.Breakfast.InvalidDateTimeGap, err);
            });
        }

        [Fact]
        public void Create_ReturnsError_WhenStartTimeIsEarlierThan7am()
        {
            // Arrange
            var name = "Big Breakfast";
            var description = "A delicious breakfast";
            var startDateTime = DateTime.Now.Date.AddHours(6);
            var endDateTime = DateTime.Now.Date.AddHours(9);
            var savory = new List<string>() { "Eggs", "Bacon" };
            var sweet = new List<string>() { "Pancakes", "Waffles" };

            // Act
            var result = Breakfast.Create(name, description, startDateTime, endDateTime, savory, sweet);

            // Assert
            Assert.True(result.IsError);
            Assert.Collection(result.Errors, err =>
            {
                Assert.Equal(Errors.Breakfast.InvalidDuration, err);
            });
        }

        [Fact]
        public void Create_ReturnsError_WhenEndTimeIsLaterThan10am()
        {
            // Arrange
            var name = "Big Breakfast";
            var description = "A delicious breakfast";
            var startDateTime = DateTime.Now.Date.AddHours(8);
            var endDateTime = DateTime.Now.Date.AddHours(11);
            var savory = new List<string>() { "Eggs", "Bacon" };
            var sweet = new List<string>() { "Pancakes", "Waffles" };

            // Act
            var result = Breakfast.Create(name, description, startDateTime, endDateTime, savory, sweet);

            // Assert
            Assert.True(result.IsError);
            Assert.Collection(result.Errors, err =>
            {
                Assert.Equal(Errors.Breakfast.InvalidDuration, err);
            });
        }
    }
}
