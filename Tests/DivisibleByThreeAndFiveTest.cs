// <copyright file="DivisibleByThreeAndFiveTest.cs" company="ABC">
//     Copyright (c) 2023 ABC. All rights reserved.
// </copyright>

using FizzBuzzProj.Controllers;
using FizzBuzzProj.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace FizzBuzzProj.Tests
{
    /// <summary>
    /// Test class for the DivisibleByThreeAndFive
    /// </summary>
    public class DivisibleByThreeAndFiveTest
    {
        private readonly Mock<IDivisibleByThree> divisibleByThreeMock;
        private readonly Mock<IDivisibleByFive> divisibleByFiveMock;
        private readonly Mock<IDivisibleByThreeAndFive> divisibleByThreeAndFiveMock;

        /// <summary>
        /// Initializes a new instances
        /// </summary>
        public DivisibleByThreeAndFiveTest()
        {
            divisibleByThreeMock = new Mock<IDivisibleByThree>();
            divisibleByFiveMock = new Mock<IDivisibleByFive>();
            divisibleByThreeAndFiveMock = new Mock<IDivisibleByThreeAndFive>();
        }

        /// <summary>
        /// Tests if a number divisible by both 3 and 5 returns "Fizz Buzz".
        /// </summary>
        [Fact]
        public void ChecksDivisibleByThreeAndFive_Returns_FizzBuzz()
        {
            // Arrange
            divisibleByThreeAndFiveMock.Setup(x => x.IsDivisibleByThreeAndFive(It.IsAny<int>())).Returns<int>(n => n % 3 == 0 && n % 5 == 0);
            var controller = new HomeController(divisibleByThreeMock.Object, divisibleByFiveMock.Object, divisibleByThreeAndFiveMock.Object);

            // Act
            int number = 15;  // Any number that is divisible by both 3 and 5
            List<string> fizzBuzzList = controller.GenerateFizzBuzz(number);

            // Assert
            fizzBuzzList[number - 1].Should().Be("fizz buzz", because: "it is divisible by both 3 and 5");
        }

        /// <summary>
        /// Tests if a number not divisible by both 3 and 5 returns the same number.
        /// </summary>
        [Fact]
        public void ChecksNotDivisibleByThreeAndFive_Returns_Number()
        {
            // Arrange
            divisibleByThreeAndFiveMock.Setup(x => x.IsDivisibleByThreeAndFive(It.IsAny<int>())).Returns<int>(n => n % 3 == 0 && n % 5 == 0);
            var controller = new HomeController(divisibleByThreeMock.Object, divisibleByFiveMock.Object, divisibleByThreeAndFiveMock.Object);

            // Act
            int number = 7;  // Any number that is not divisible by both 3 and 5 so it gives number as output
            List<string> fizzBuzzList = controller.GenerateFizzBuzz(number);

            // Assert
            fizzBuzzList[number - 1].Should().Be(number.ToString(), because: "it is not divisible by both 3 and 5");
        }

        /// <summary>
        /// Tests if a number not divisible by both 3 and 5 returns not "Fizz Buzz".
        /// </summary>
        [Fact]
        public void ChecksNotDivisibleByThreeAndFive_Returns_NotFizzBuzz()
        {
            // Arrange
            divisibleByThreeAndFiveMock.Setup(x => x.IsDivisibleByThreeAndFive(It.IsAny<int>())).Returns<int>(n => n % 3 == 0 && n % 5 == 0);
            var controller = new HomeController(divisibleByThreeMock.Object, divisibleByFiveMock.Object, divisibleByThreeAndFiveMock.Object);

            // Act
            int number = 9;  // Any number that is not divisible by both 3 and 5
            List<string> fizzBuzzList = controller.GenerateFizzBuzz(number);

            // Assert
            fizzBuzzList[number - 1].Should().NotBe("fizz buzz", because: "it is not divisible by both 3 and 5");
        }
    }
}
