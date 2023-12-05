// <copyright file="PaginationTest.cs" company="ABC">
//     Copyright (c) 2023 ABC. All rights reserved.
// </copyright>
using FizzBuzzProj.Controllers;
using FizzBuzzProj.Interfaces;
using FizzBuzzProj.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using X.PagedList;
using Xunit;

namespace FizzBuzzProj.Tests
{
    /// <summary>
    /// Test class for pagination
    /// </summary>
    public class PaginationTest
    {
        private readonly Mock<IDivisibleByThree> divisibleByThreeMock;
        private readonly Mock<IDivisibleByFive> divisibleByFiveMock;
        private readonly Mock<IDivisibleByThreeAndFive> divisibleByThreeAndFiveMock;

        /// <summary>
        /// Initializes a new instances.
        /// </summary>
        public PaginationTest()
        {
            divisibleByThreeMock = new Mock<IDivisibleByThree>();
            divisibleByFiveMock = new Mock<IDivisibleByFive>();
            divisibleByThreeAndFiveMock = new Mock<IDivisibleByThreeAndFive>();
        }

        /// <summary>
        /// Tests if the Index2 action returns a view result.
        /// </summary>
        [Fact]
        public void Index2_Action_Returns_ViewResult()
        {
            // Arrange
            var homeController = new HomeController(divisibleByThreeMock.Object, divisibleByFiveMock.Object, divisibleByThreeAndFiveMock.Object);

            // Act
            IActionResult result = homeController.Index2();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }

        /// <summary>
        /// Tests if the Index2 action returns a view result with a model of type IPagedList<string>.
        /// </summary>
        [Fact]
        public void Index2_Action_Returns_ViewResult_With_PagedList_Model()
        {
            // Arrange
            var homeController = new HomeController(divisibleByThreeMock.Object, divisibleByFiveMock.Object, divisibleByThreeAndFiveMock.Object);

            // Act
            IActionResult result = homeController.Index2();

            // Assert
            result.Should().BeOfType<ViewResult>().Which.Model.Should().BeAssignableTo<IPagedList<string>>();
        }

        /// <summary>
        /// Tests if the Index2 action returns the correct view name.
        /// </summary>
        [Fact]
        public void Index2_Action_Returns_Correct_View_Name()
        {
            // Arrange
            var homeController = new HomeController(divisibleByThreeMock.Object, divisibleByFiveMock.Object, divisibleByThreeAndFiveMock.Object);

            // Act
            IActionResult result = homeController.Index2();

            // Assert
            result.Should().BeOfType<ViewResult>().Which.ViewName.Should().Be("Index2");
        }


        /// <summary>
        /// Tests if the Index2 action returns a view result with the correct model type.
        /// </summary>
        [Fact]
        public void Index2_Action_Returns_ViewResult_With_Correct_Model_Type_And_PageSize()
        {
            // Arrange
            var homeController = new HomeController(divisibleByThreeMock.Object, divisibleByFiveMock.Object, divisibleByThreeAndFiveMock.Object);

            // Act
            IActionResult result = homeController.Index2();

            // Assert
            result.Should().BeOfType<ViewResult>().Which.Model.Should().BeAssignableTo<IPagedList<string>>()
                .Which.PageSize.Should().Be(20);  // As our default page size is 20
        }

        /// <summary>
        /// Tests if the Index2 next page is giving actual values
        /// </summary>

        [Fact]
        public void HomeController_Index2_GetNextPage()
        {
            // Arrange
            var mockDivisibleByThree = new Mock<IDivisibleByThree>();
            var mockDivisibleByFive = new Mock<IDivisibleByFive>();
            var mockDivisibleByThreeAndFive = new Mock<IDivisibleByThreeAndFive>();

            var controller = new HomeController(mockDivisibleByThree.Object, mockDivisibleByFive.Object, mockDivisibleByThreeAndFive.Object);

            var value = 15; // Assuming user input for FizzBuzz

            // Mocking the expected list for the second page
            var expectedList = new List<string> { "11", "fizz", "13", "14", "fizzbuzz" };

            var fizzBuzzModel = new FizzBuzz { Number = value };

            // Act
            var result = controller.Index2(fizzBuzzModel, page: 2) as ViewResult;

            // Assert
            result.Should().NotBeNull();
            result.Model.Should().BeAssignableTo<IPagedList<string>>();

            var actualPagedList = (IPagedList<string>)result.Model;
            Assert.Equal(expectedList, actualPagedList);
        }




    }
}
