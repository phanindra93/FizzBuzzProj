// <copyright file="DivisibleByFiveTest.cs" company="ABC">
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
    /// Test class for the DivisibleByFive 
    /// </summary>
    public class DivisibleByFiveTest
    {
        private readonly Mock<IDivisibleByThree> divisibleByThreeMock;
        private readonly Mock<IDivisibleByFive> divisibleByFiveMock;
        private readonly Mock<IDivisibleByThreeAndFive> divisibleByThreeAndFiveMock;

        /// <summary>
        /// Initializes a new instances
        /// </summary>
        public DivisibleByFiveTest()
        {
            divisibleByThreeMock = new Mock<IDivisibleByThree>();
            divisibleByFiveMock = new Mock<IDivisibleByFive>();
            divisibleByThreeAndFiveMock = new Mock<IDivisibleByThreeAndFive>();
        }

        /// <summary>
        /// Tests if a number divisible by 5 returns "Buzz".
        /// </summary>
        [Fact]
        public void ChecksDivisibleByFive_Returns_Buzz()
        {
            // Arrange
            divisibleByFiveMock.Setup(x => x.IsDivisibleByFive(It.IsAny<int>())).Returns<int>(n => n % 5 == 0);
            var controller = new HomeController(divisibleByThreeMock.Object, divisibleByFiveMock.Object, divisibleByThreeAndFiveMock.Object);

            // Act
            int number = 10;  // Any number that is divisible by 5
            List<string> fizzBuzzList = controller.GenerateFizzBuzz(number);

            // Assert
            fizzBuzzList[number - 1].Should().Be("buzz", because: "it is divisible by 5");
        }

        /// <summary>
        /// Tests if a number not divisible by 5 and 3 so it returns the same number.
        /// </summary>
        [Fact]
        public void ChecksNotDivisibleByFive_Returns_Number()
        {
            // Arrange
            divisibleByFiveMock.Setup(x => x.IsDivisibleByFive(It.IsAny<int>())).Returns<int>(n => n % 5 == 0);
            var controller = new HomeController(divisibleByThreeMock.Object, divisibleByFiveMock.Object, divisibleByThreeAndFiveMock.Object);

            // Act
            int number = 7;  // Any number that is not divisible by 5 and 5 so that it gives the number as output
            List<string> fizzBuzzList = controller.GenerateFizzBuzz(number);

            // Assert
            fizzBuzzList[number - 1].Should().Be(number.ToString(), because: "it is not divisible by 5");
        }

        /// <summary>
        /// Tests if a number not divisible by 5 returns not "Buzz".
        /// </summary>
        [Fact]
        public void ChecksNotDivisibleByFive_Returns_NotBuzz()
        {
            // Arrange
            divisibleByFiveMock.Setup(x => x.IsDivisibleByFive(It.IsAny<int>())).Returns<int>(n => n % 5 == 0);
            var controller = new HomeController(divisibleByThreeMock.Object, divisibleByFiveMock.Object, divisibleByThreeAndFiveMock.Object);

            // Act
            int number = 9;  // Any number that is not divisible by 5
            List<string> fizzBuzzList = controller.GenerateFizzBuzz(number);

            // Assert
            fizzBuzzList[number - 1].Should().NotBe("buzz", because: "it is not divisible by 5");
        }
    }
}
