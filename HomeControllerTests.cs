using FizzBuzzProj.Controllers;
using FizzBuzzProj.Interfaces;
using FluentAssertions;
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
            int range = 33;
            int randomNumberDivisibleByThree = GetRandomNumberInRange(range, 3);
            List<string> fizzBuzzList = homeController.GenerateFizzBuzz(range);

            // Assert
            fizzBuzzList[randomNumberDivisibleByThree - 1].Should().Be("Fizz", because: "it is divisible by 3");
        }

        [Fact]
        public void GenerateFizzBuzz_ChecksDivisibleByFive()
        {
            // Arrange
            divisibleByFiveMock.Setup(x => x.IsDivisibleByFive(It.IsAny<int>())).Returns<int>(n => n % 5 == 0);

            var homeController = new HomeController(divisibleByThreeMock.Object, divisibleByFiveMock.Object, divisibleByThreeAndFiveMock.Object);

            // Act
            int range = 10;
            int randomNumberDivisibleByFive = GetRandomNumberInRange(range, 5);
            List<string> fizzBuzzList = homeController.GenerateFizzBuzz(range);

            // Assert
            fizzBuzzList[randomNumberDivisibleByFive - 1].Should().Be("Buzz", because: "it is divisible by 5");
        }

        [Fact]
        public void GenerateFizzBuzz_ChecksDivisibleByThreeAndFive()
        {
            // Arrange
            divisibleByThreeAndFiveMock.Setup(x => x.IsDivisibleByThreeAndFive(It.IsAny<int>())).Returns<int>(n => n % 3 == 0 && n % 5 == 0);

            var homeController = new HomeController(divisibleByThreeMock.Object, divisibleByFiveMock.Object, divisibleByThreeAndFiveMock.Object);

            // Act
            int range = 15;
            int randomNumberDivisibleByThreeAndFive = GetRandomNumberInRange(range, 15);
            List<string> fizzBuzzList = homeController.GenerateFizzBuzz(range);

            // Assert
            fizzBuzzList[randomNumberDivisibleByThreeAndFive - 1].Should().Be("Fizz Buzz", because: "it is divisible by 3 and 5");
        }

        // Helper method to get a random number within the specified range and divisible by the given divisor
        private int GetRandomNumberInRange(int range, int divisor)
        {
            Random random = new Random();
            int randomNumber = random.Next(1, range / divisor + 1) * divisor;
            return randomNumber;
        }
    }
}
