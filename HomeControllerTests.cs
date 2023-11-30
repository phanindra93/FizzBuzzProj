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
        public void GenerateFizzBuzz_RandomNumberWithinRange_ChecksFizzAndBuzz()
        {
            // Arrange
            divisibleByThreeMock.Setup(x => x.IsDivisibleByThree(It.IsAny<int>())).Returns<int>(n => n % 3 == 0);
            divisibleByFiveMock.Setup(x => x.IsDivisibleByFive(It.IsAny<int>())).Returns<int>(n => n % 5 == 0);
            divisibleByThreeAndFiveMock.Setup(x => x.IsDivisibleByThreeAndFive(It.IsAny<int>())).Returns<int>(n => n % 3 == 0 && n % 5 == 0);

            var homeController = new HomeController(divisibleByThreeMock.Object, divisibleByFiveMock.Object, divisibleByThreeAndFiveMock.Object);

            // Act
            Random random = new Random();
            int randomNumber = random.Next(1, 100); 
            List<string> fizzBuzzList = homeController.GenerateFizzBuzz(randomNumber);

            // Assert
            Assert.NotNull(fizzBuzzList);

            foreach (var item in fizzBuzzList)
            {
                bool isFizz = divisibleByThreeMock.Object.IsDivisibleByThree(randomNumber);
                bool isBuzz = divisibleByFiveMock.Object.IsDivisibleByFive(randomNumber);
                bool isFizzBuzz = divisibleByThreeAndFiveMock.Object.IsDivisibleByThreeAndFive(randomNumber);

                
                if (isFizz && isBuzz)
                {
                    Assert.Equal("Fizz Buzz", item, ignoreCase: true);
                }
                else if (isFizz)
                {
                    Assert.Equal("Fizz", item, ignoreCase: true);
                }
                else if (isBuzz)
                {
                    Assert.Equal("Buzz", item, ignoreCase: true);
                }
                else
                {
                    Assert.Equal(randomNumber.ToString(), item);
                }
            }
        }
    }
}
