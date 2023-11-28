using FizzBuzzProj.Interfaces;

namespace FizzBuzzProj.Services
{
    public class DivisibleByThree : IDivisibleByThree
    {
        public bool IsDivisibleByThree(int number)
        {
            return number % 3 == 0;
        }
    }
}
