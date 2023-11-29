

using FizzBuzzProj.Controllers;
using FizzBuzzProj.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace FizzBuzzProj.Tests
{
    public class HomeControllerTests
    {
        private readonly Mock<IDivisibleByThree> divisibleByThreeMock;
        private readonly Mock<IDivisibleByFive> divisibleByFiveMock;
        private readonly Mock<IDivisibleByThreeAndFive> divisibleByThreeAndFiveMock;

        public HomeControllerTests()
        {
            divisibleByThreeMock = new Mock<IDivisibleByThree>();
            divisibleByFiveMock = new Mock<IDivisibleByFive>();
            divisibleByThreeAndFiveMock = new Mock<IDivisibleByThreeAndFive>();
        }

        [Fact]
        public void GenerateFizzBuzz_ChecksDivisibleByThree()
        {
            // Arrange
            divisibleByThreeMock.Setup(x => x.IsDivisibleByThree(It.IsAny<int>())).Returns<int>(n => n % 3 == 0);

            var homeController = new HomeController(divisibleByThreeMock.Object, divisibleByFiveMock.Object, divisibleByThreeAndFiveMock.Object);

            // Act
            List<string> fizzBuzzList = homeController.GenerateFizzBuzz(33); // Any number that is divisible by 3

            // Assert
            Assert.Contains("Fizz", fizzBuzzList, StringComparer.OrdinalIgnoreCase);
        }

        [Fact]
        public void GenerateFizzBuzz_ChecksDivisibleByFive()
        {
            // Arrange
            divisibleByFiveMock.Setup(x => x.IsDivisibleByFive(It.IsAny<int>())).Returns<int>(n => n % 5 == 0);

            var homeController = new HomeController(divisibleByThreeMock.Object, divisibleByFiveMock.Object, divisibleByThreeAndFiveMock.Object);

            // Act
            List<string> fizzBuzzList = homeController.GenerateFizzBuzz(10); 

            // Assert
            Assert.Contains("Buzz", fizzBuzzList, StringComparer.OrdinalIgnoreCase);
        }

        [Fact]
        public void GenerateFizzBuzz_ChecksDivisibleByThreeAndFive()
        {
            // Arrange
            divisibleByThreeAndFiveMock.Setup(x => x.IsDivisibleByThreeAndFive(It.IsAny<int>())).Returns<int>(n => n % 3 == 0 && n % 5 == 0);

            var homeController = new HomeController(divisibleByThreeMock.Object, divisibleByFiveMock.Object, divisibleByThreeAndFiveMock.Object);

            // Act
            List<string> fizzBuzzList = homeController.GenerateFizzBuzz(15); 

            // Assert
            Assert.Contains("Fizz Buzz", fizzBuzzList, StringComparer.OrdinalIgnoreCase);
        }
    }
}

