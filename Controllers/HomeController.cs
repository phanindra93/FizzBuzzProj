using FizzBuzzProj.Interfaces;
using FizzBuzzProj.Models;
using FizzBuzzProj.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using X.PagedList;

namespace FizzBuzzProj.Controllers
    /// <summary>
    /// Controller for handling FizzBuzz-related actions.
    /// </summary>
{
    public class HomeController : Controller
    {
        private readonly IDivisibleByThree _divisibleByThree;
        private readonly IDivisibleByFive _divisibleByFive;
        private readonly IDivisibleByThreeAndFive _divisibleByThreeAndFive;
        /// <summary>
        /// Constructor for HomeController.
        /// </summary>


        public HomeController(
            IDivisibleByThree divisibleByThree,
            IDivisibleByFive divisibleByFive,
            IDivisibleByThreeAndFive divisibleByThreeAndFive)
        {
            _divisibleByThree = divisibleByThree;
            _divisibleByFive = divisibleByFive;
            _divisibleByThreeAndFive = divisibleByThreeAndFive;

        }
        /// <summary>
        /// Default action for rendering the index view.
        /// </summary>
        /// <returns>Index view.</returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Action for handling GET requests to the Index2 endpoint.
        /// </summary>
        [HttpGet]
        public IActionResult Index2(int page = 1)
        {
            int pageSize = 20;
            int number = int.Parse(Request.Query["number"].ToString());
            List<string> fizzBuzzList = GenerateFizzBuzz(number);

            IPagedList<string> pagedFizzBuzzList = fizzBuzzList.ToPagedList(page, pageSize);

            return View("Result", pagedFizzBuzzList);
        }
        /// <summary>
        /// Action for handling POST requests to the Index2 endpoint.
        /// </summary>
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
        
      
        /// <summary>
        /// Generates a FizzBuzz list up to the specified number.
        /// </summary>
        /// <param name="number">The upper limit for FizzBuzz generation.</param>
        /// <returns>List of FizzBuzz strings.</returns>

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