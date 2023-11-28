using FizzBuzzProj.Interfaces;

namespace FizzBuzzProj.Services
{
    public class DivisibleByFive : IDivisibleByFive
    {
        public bool IsDivisibleByFive(int number)
        {
            return number % 5 == 0;
        }
    }
}
