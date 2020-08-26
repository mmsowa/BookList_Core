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
using System.Security.Claims;
using BookListMVC.Services;

namespace BookListMVC.Controllers {
  [Authorize]
  public class AppUserController : Controller {
    private readonly AuthDbContext _db;

    public UserService _userService;

    public AppUserController(AuthDbContext db, UserService userService) {
      _db = db;
      _userService = userService;
    }

    [BindProperty]
    public AppUser AppUser { get; set; }

    public IActionResult Index() {
      return View();
    }
    

    #region API Calls

    [Route("Users/GetCurrentUserId")]
    [HttpGet]
    public IActionResult GetCurrentUserId() {
      return Json( new { id = _userService.GetUserCurrentUserId() });
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll() {
      return Json(new { data = await _db.AppUsers.ToListAsync() });
    }

    [Route("Users/GetUser")]
    [HttpGet]
    public async Task<IActionResult> GetUser(string id) {
      return Json(new { data = await _db.AppUsers.FirstOrDefaultAsync(u => u.Id == id) });
    }
    #endregion
  }
}