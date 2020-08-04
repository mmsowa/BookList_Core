using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListMVC.Areas.Identity.Data;
using BookListMVC.Data;
using BookListMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListMVC.Controllers {
  [Authorize]
  public class AppUserController : Controller {
    private readonly AuthDbContext _db;

    public AppUserController(AuthDbContext db) {
      _db = db;
    }

    [BindProperty]
    public AppUser AppUser { get; set; }

    public IActionResult Index() {
      return View();
    }

    #region API Calls
    [HttpGet]
    public async Task<IActionResult> GetAll() {
      return Json(new { data = await _db.AppUsers.ToListAsync() });
    }

    public async Task<IActionResult> GetUser(string id) {
      return Json(new { data = await _db.AppUsers.FirstOrDefaultAsync(u => u.Id == id) });
    }
    #endregion
  }
}