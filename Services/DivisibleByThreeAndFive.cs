using FizzBuzzProj.Interfaces;

namespace FizzBuzzProj.Services
{
    public class DivisibleByThreeAndFive : IDivisibleByThreeAndFive
    {
        public bool IsDivisibleByThreeAndFive(int number)
        {
            return number % 3 == 0 && number % 5 == 0;
        }
    }
}
