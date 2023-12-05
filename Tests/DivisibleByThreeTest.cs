// <copyright file="DivisibleByThreeTest.cs" company="ABC">
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
    /// Test class for the DivisibleByThree 
    /// </summary>
    public class DivisibleByThreeTest
    {
        private readonly Mock<IDivisibleByThree> divisibleByThreeMock;
        private readonly Mock<IDivisibleByFive> divisibleByFiveMock;
        private readonly Mock<IDivisibleByThreeAndFive> divisibleByThreeAndFiveMock;

        /// <summary>
        /// Initializes a new instances
        /// </summary>
        public DivisibleByThreeTest()
        {
            divisibleByThreeMock = new Mock<IDivisibleByThree>();
            divisibleByFiveMock = new Mock<IDivisibleByFive>();
            divisibleByThreeAndFiveMock = new Mock<IDivisibleByThreeAndFive>();
        }

        /// <summary>
        /// Tests if a number divisible by 3 returns "Fizz".
        /// </summary>
        [Fact]
        public void ChecksDivisibleByThree_Returns_Fizz()
        {
            // Arrange
            divisibleByThreeMock.Setup(x => x.IsDivisibleByThree(It.IsAny<int>())).Returns<int>(n => n % 3 == 0);
            var controller = new HomeController(divisibleByThreeMock.Object, divisibleByFiveMock.Object, divisibleByThreeAndFiveMock.Object);

            // Act
            int number = 9;  // Any number that includes numbers divisible by 3
            List<string> fizzBuzzList = controller.GenerateFizzBuzz(number);

            // Assert
            fizzBuzzList[number - 1].Should().Be("fizz", because: "it is divisible by 3");
        }

        /// <summary>
        /// Tests if a number not divisible by 3 and 5 so it returns the same number.
        /// </summary>
        [Fact]
        public void ChecksNotDivisibleByThree_Returns_Number()
        {
            // Arrange
            divisibleByThreeMock.Setup(x => x.IsDivisibleByThree(It.IsAny<int>())).Returns<int>(n => n % 3 == 0);
            var controller = new HomeController(divisibleByThreeMock.Object, divisibleByFiveMock.Object, divisibleByThreeAndFiveMock.Object);

            // Act
            int number = 7;  // Any number that is not divisible by 3 and 5 so that we get a number as string
            List<string> fizzBuzzList = controller.GenerateFizzBuzz(number);

            // Assert
            fizzBuzzList[number - 1].Should().Be(number.ToString(), because: "it is not divisible by 3");
        }

        /// <summary>
        /// Tests if a number not divisible by 3 returns not "Fizz".
        /// </summary>
        [Fact]
        public void ChecksNotDivisibleByThree_Returns_NotFizz()
        {
            // Arrange
            divisibleByThreeMock.Setup(x => x.IsDivisibleByThree(It.IsAny<int>())).Returns<int>(n => n % 3 == 0);
            var controller = new HomeController(divisibleByThreeMock.Object, divisibleByFiveMock.Object, divisibleByThreeAndFiveMock.Object);

            // Act
            int number = 10;  // Any number that is not divisible by 3
            List<string> fizzBuzzList = controller.GenerateFizzBuzz(number);

            // Assert
            fizzBuzzList[number - 1].Should().NotBe("fizz", because: "it is not divisible by 3");
        }
    }
}
