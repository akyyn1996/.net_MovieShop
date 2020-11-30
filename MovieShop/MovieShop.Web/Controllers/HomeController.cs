using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShop.Core.ServiceInterfaces;
using MovieShop.Web.Models;

namespace MovieShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;
        public HomeController(ILogger<HomeController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetTopRevenueMovies();

            //var test = await _movieService.GetMovieAsync(23);
            //var testdata = "List of Movies";
            //ViewBag.myp = testdata;

            return View(movies);
            // By default whe  you do return View its gonna return View with Same name as action method
            // name inside the Views Folder of that Controller name folder
            // HttpContext in ASP.NET Core and ASP.NET which will provide you with all the information regarding your HTTP Request
            // Controllers will call Services ==> Repositories
            // Navigation ==> list of Genres as a dropdown
            // showing top 20 highest revenue movies as Movie Cards....with images
            // Card in bootstrap, cardimage, movieid, title
            // Movie entity has all the above,
            // Models based on your UI/API requirement.
            // Models/ViewModels in MVC
            // DTO - Data Transfer Objects in API
            // We create custom classes based on our UI/API requirement
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
