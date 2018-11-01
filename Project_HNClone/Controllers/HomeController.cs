using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project_HNClone.Models;
using Project_HNClone.Queries;

namespace Project_HNClone.Controllers
{
    public class HomeController : Controller
    {

        private readonly hnDatabaseContext _context;

        public HomeController(hnDatabaseContext context)
        {
          _context = context;
        }

        public async Task<IActionResult> Index()
        {
          return View(await _context.Stories.Take(100).ToListAsync());
        }

        public async Task<IActionResult> Newest()
        {
          //ViewData["Message"] = "Your application description page.";
          return View(await _context.Stories.Take(100).ToListAsync());
        }

        public IActionResult Show()
        {
            //ViewData["Message"] = "Your contact page.";

            return View();
        }

        public async Task<IActionResult> Newcomments()
        {
          return View(await _context.Comments.Take(100).ToListAsync());
        }

         public IActionResult Ask()
        {
            return View();
        }

        public IActionResult Jobs()
        {
          return View();
        }

        [Authorize]
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
