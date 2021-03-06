﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Project_HNClone.Models;
using Project_HNClone.Queries;
using X.PagedList;

namespace Project_HNClone.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        private IMemoryCache _cache;
        private readonly TimeSpan _cacheSlidingExpiration = TimeSpan.FromMinutes(5);
        private readonly TimeSpan _cacheAbsoluteExpiration = TimeSpan.FromMinutes(30);

        public HomeController(IConfiguration config, IMemoryCache memoryCache)
        {
            this.configuration = config;
            _cache = memoryCache;
        }

        public IActionResult Index(int? page)
        {
            StoryQueries storyQueries = new StoryQueries(configuration);

            List<Data.Story> cacheEntry;
            string cacheKey = "Index";

            // Look for cache key.
            if (!_cache.TryGetValue(cacheKey, out cacheEntry))
            {
                // Key not in cache, so get data.
                cacheEntry = storyQueries.GetStories(100, "story");

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time.
                    .SetSlidingExpiration(_cacheSlidingExpiration)
                    .SetAbsoluteExpiration(_cacheAbsoluteExpiration);

                // Save data in cache.
                _cache.Set(cacheKey, cacheEntry, cacheEntryOptions);
            }

            var pageNumber = page ?? 1;
            var onePageOfStories = cacheEntry.ToPagedList(pageNumber, 30);

            ModelState.Clear();

            ViewBag.OnePageOfStories = onePageOfStories;
            
            return View();
        }

        public IActionResult Newest(int? page)
        {
            StoryQueries storyQueries = new StoryQueries(configuration);
            
            List<Data.Story> cacheEntry;
            string cacheKey = "Newest";

            // Look for cache key.
            if (!_cache.TryGetValue(cacheKey, out cacheEntry))
            {
                // Key not in cache, so get data.
                cacheEntry = storyQueries.GetStories(100, "new");

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time.
                    .SetSlidingExpiration(_cacheSlidingExpiration)
                    .SetAbsoluteExpiration(_cacheAbsoluteExpiration);

                // Save data in cache.
                _cache.Set(cacheKey, cacheEntry, cacheEntryOptions);
            }

            var pageNumber = page ?? 1;
            var onePageOfStories = cacheEntry.ToPagedList(pageNumber, 30);

            ModelState.Clear();

            ViewBag.OnePageOfStories = onePageOfStories;

            return View();
        }

        public IActionResult Show()
        {
            return View();
        }

        public IActionResult Newcomments()
        {
            CommentQueries commentQueries = new CommentQueries(configuration);

            List<Data.Comment> cacheEntry;
            string cacheKey = "Newcomments";

            // Look for cache key.
            if (!_cache.TryGetValue(cacheKey, out cacheEntry))
            {
                // Key not in cache, so get data.
                cacheEntry = commentQueries.GetComments(100);

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time.
                    .SetSlidingExpiration(_cacheSlidingExpiration)
                    .SetAbsoluteExpiration(_cacheAbsoluteExpiration);

                // Save data in cache.
                _cache.Set(cacheKey, cacheEntry, cacheEntryOptions);
            }

            ModelState.Clear();

            return View(cacheEntry);
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

    public static class CacheKeys
    {
        public static string Entry { get { return "_Entry"; } }
        public static string CallbackEntry { get { return "_Callback"; } }
        public static string CallbackMessage { get { return "_CallbackMessage"; } }
        public static string Parent { get { return "_Parent"; } }
        public static string Child { get { return "_Child"; } }
        public static string DependentMessage { get { return "_DependentMessage"; } }
        public static string DependentCTS { get { return "_DependentCTS"; } }
        public static string Ticks { get { return "_Ticks"; } }
        public static string CancelMsg { get { return "_CancelMsg"; } }
        public static string CancelTokenSource { get { return "_CancelTokenSource"; } }
    }
}
