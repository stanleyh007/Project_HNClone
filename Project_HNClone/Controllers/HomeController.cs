﻿using System;
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
using X.PagedList;

namespace Project_HNClone.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration config)
        {
            this.configuration = config;
        }

        public IActionResult Index(int? page)
        {
            StoryQueries storyQueries = new StoryQueries(configuration);
            ModelState.Clear();

            var storyIndex = storyQueries.GetStories(10000, "story"); 
            var pageNumber = page ?? 1; 
            var onePageOfStories = storyIndex.ToPagedList(pageNumber, 100); 

            ViewBag.OnePageOfStories = onePageOfStories;
            return View();
        }

        public IActionResult Newest()
        {
            StoryQueries storyQueries = new StoryQueries(configuration);
            ModelState.Clear();
            return View(storyQueries.GetStories(100, "new"));
        }

        public IActionResult Show()
        {
            return View();
        }

        public IActionResult Newcomments()
        {
            CommentQueries commentQueries = new CommentQueries(configuration);
            ModelState.Clear();
            return View(commentQueries.GetComments());
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
