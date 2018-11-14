using System;
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

        private readonly hnDatabaseContext _context;

        private IMemoryCache _cache;

        public HomeController(IConfiguration config, hnDatabaseContext context, IMemoryCache memoryCache)
        {
            this.configuration = config;
            _context = context;
            _cache = memoryCache;
        }

        public IActionResult Index(int? page)
        {
            StoryQueries storyQueries = new StoryQueries(configuration);

            IPagedList<Data.Story> cacheEntry;
            string cacheKey = System.Reflection.MethodBase.GetCurrentMethod().Name;

            // Look for cache key.
            if (!_cache.TryGetValue(cacheKey, out cacheEntry))
            {
                // Key not in cache, so get data.
                var storyIndex = storyQueries.GetStories(100, "story");
                var pageNumber = page ?? 1;
                cacheEntry = storyIndex.ToPagedList(pageNumber, 100);

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time.
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                // Save data in cache.
                _cache.Set(cacheKey, cacheEntry, cacheEntryOptions);
            }

            ModelState.Clear();

            ViewBag.OnePageOfStories = cacheEntry;

            return View();
        }

        public async Task<IActionResult> Newest()
        {
            List<Stories> cacheEntry;
            string cacheKey = System.Reflection.MethodBase.GetCurrentMethod().Name;

            // Look for cache key.
            if (!_cache.TryGetValue(cacheKey, out cacheEntry))
            {
                // Key not in cache, so get data.
                cacheEntry = await _context.Stories.Take(100).ToListAsync();

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time.
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                // Save data in cache.
                _cache.Set(cacheKey, cacheEntry, cacheEntryOptions);
            }

            return View(cacheEntry);
        }

        public IActionResult Show()
        {
            return View();
        }

        public IActionResult Newcomments()
        {
            CommentQueries commentQueries = new CommentQueries(configuration);

            List<Data.Comment> cacheEntry;
            string cacheKey = System.Reflection.MethodBase.GetCurrentMethod().Name;

            // Look for cache key.
            if (!_cache.TryGetValue(cacheKey, out cacheEntry))
            {
                // Key not in cache, so get data.
                cacheEntry = commentQueries.GetComments();

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time.
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

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
