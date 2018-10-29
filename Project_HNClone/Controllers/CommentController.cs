using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_HNClone.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_HNClone.Controllers
{
  public class CommentController : Controller
  {
    private readonly hnDatabaseContext _context;

    public CommentController(hnDatabaseContext context)
    {
      _context = context;
    }
    // GET: /<controller>/
    public async Task<IActionResult> Index()
    {
      return View(await _context.Comments.ToListAsync());
    }

    public IActionResult Comment()
    {
      return View();
    }
  }
}
