using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListMVC.Data;
using BookListMVC.Models;
using BookListMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListMVC.Controllers {
  public class DebugController : Controller {

    private readonly AuthDbContext _db;
    private readonly UserService _userService;
    public DebugController(AuthDbContext db, UserService userService) {
      _db = db;
      _userService = userService;
    }

    [Route("Debug/GetAppUserBooksForActiveUser")]
    [HttpGet]
    public async Task<ICollection<AppUserBook>> GetAppUserBooksForActiveUser() {
      return await _db.AppUsers.Where(u => u.Id == _userService.GetUserCurrentUserId()).Select(u => u.AppUserBooks).FirstOrDefaultAsync();
    }
  }
}
