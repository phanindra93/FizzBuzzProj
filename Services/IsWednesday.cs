using FizzBuzzProj.Interfaces;

namespace FizzBuzzProj.Services
{
    public class IsWednesday : IIsWednesday
    {
        public bool IsWednesdaytoday()
        {
            return DateTime.Now.DayOfWeek == DayOfWeek.Wednesday;
        }
    }
}
