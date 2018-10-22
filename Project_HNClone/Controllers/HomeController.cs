using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Project_HNClone.Models;
using Project_HNClone.Queries;

namespace Project_HNClone.Controllers
{
    public class HomeController : Controller
    {
        // Needed to read connectionstring
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration config)
        {
            this.configuration = config;
        }

        public IActionResult Index()
        {
            // Test to see that it works and it does
            test test = new test();
            test.testCake(configuration.GetConnectionString("DefaultConnection"));
            return View();
        }

        public IActionResult Newest()
        {
            //ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Show()
        {
            //ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Newcomments()
        {
            return View();
        }

         public IActionResult Ask()
        {
            return View();
        }

        public IActionResult Jobs()
        {
          return View();
        }

        public IActionResult Submit()
        {
          return View();
        }

        public IActionResult NewsGuidelines()
        {
          return View();
        }


        public IActionResult Newsfaq()
        {
          return View();
        }

        public IActionResult Security()
        {
          return View();
        }

        public IActionResult Lists()
        {
          return View();
        }

        public IActionResult Bookmarklet()
        {
          return View();
        }

        public IActionResult Legal()
        {
          return View();
        }

        public IActionResult Apply()
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
