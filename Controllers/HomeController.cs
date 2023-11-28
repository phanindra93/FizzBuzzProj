using FizzBuzzProj.Interfaces;
using FizzBuzzProj.Models;
using FizzBuzzProj.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using X.PagedList;

namespace FizzBuzzProj.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDivisibleByThree _divisibleByThree;
        private readonly IDivisibleByFive _divisibleByFive;
        private readonly IDivisibleByThreeAndFive _divisibleByThreeAndFive;


        public HomeController(
            IDivisibleByThree divisibleByThree,
            IDivisibleByFive divisibleByFive,
            IDivisibleByThreeAndFive divisibleByThreeAndFive)
        {
            _divisibleByThree = divisibleByThree;
            _divisibleByFive = divisibleByFive;
            _divisibleByThreeAndFive = divisibleByThreeAndFive;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Index2(int page = 1)
        {
            int pageSize = 20;
            int number = int.Parse(Request.Query["number"].ToString());
            List<string> fizzBuzzList = GenerateFizzBuzz(number);

            IPagedList<string> pagedFizzBuzzList = fizzBuzzList.ToPagedList(page, pageSize);

            return View("Result", pagedFizzBuzzList);
        }

        [HttpPost]
        public IActionResult Index2(FizzBuzz model, int page = 1)
        {
            if (ModelState.IsValid)
            {

                List<string> fizzBuzzList = GenerateFizzBuzz(model.Number);
                int pageSize = 20;
                var pageNumber = page;
                IPagedList<string> pagedFizzBuzzList = fizzBuzzList.ToPagedList(pageNumber, pageSize);

               

                    return View("Result", pagedFizzBuzzList);
                
                    
                
            }

            return View();
        }

        private List<string> GenerateFizzBuzz(int number, int currentPage)
        {
            List<string> fizzBuzzList = new List<string>();

            int startIndex = (currentPage - 1) * 20;
            int endIndex = Math.Min(currentPage * 20, number);

            for (int i = startIndex + 1; i <= endIndex; i++)
            {
                if (_divisibleByThreeAndFive.IsDivisibleByThreeAndFive(i))
                {
                    fizzBuzzList.Add("fizz buzz");
                }
                else if (_divisibleByThree.IsDivisibleByThree(i))
                {
                    fizzBuzzList.Add("fizz");
                }
                else if (_divisibleByFive.IsDivisibleByFive(i))
                {
                    fizzBuzzList.Add("buzz");
                }
                else
                {
                    fizzBuzzList.Add(i.ToString());
                }
            }

            return fizzBuzzList;
        }

        public List<string> GenerateFizzBuzz(int number)
        {
            List<string> fizzBuzzList = new List<string>();

            for (int i = 1; i <= number; i++)
            {
                if (_divisibleByThreeAndFive.IsDivisibleByThreeAndFive(i))
                {
                    fizzBuzzList.Add("fizz buzz");
                }
                else if (_divisibleByThree.IsDivisibleByThree(i))
                {
                    fizzBuzzList.Add("fizz");
                }
                else if (_divisibleByFive.IsDivisibleByFive(i))
                {
                    fizzBuzzList.Add("buzz");
                }
                else
                {
                    fizzBuzzList.Add(i.ToString());
                }
            }

            return fizzBuzzList;
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}