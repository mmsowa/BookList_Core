using System.Threading.Tasks;
using BookListMVC.Areas.Identity.Data;
using BookListMVC.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookListMVC.Services;

namespace BookListMVC.Controllers {
  [Authorize]
  [Route("Users")]
  public class AppUserController : Controller {
    private readonly AuthDbContext _db;
    private readonly UserService _userService;
    [BindProperty]
    public AppUser AppUser { get; set; }

    public AppUserController(AuthDbContext db, UserService userService) {
      _db = db;
      _userService = userService;
    }

    [Route("GetCurrentUserId")]
    [HttpGet]
    public IActionResult GetCurrentUserId() {
      return Json( new { id = _userService.GetUserCurrentUserId() });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() {
      return Json(new { data = await _db.AppUsers.ToListAsync() });
    }

    [Route("GetUser")]
    [HttpGet]
    public async Task<IActionResult> GetUser(string id) {
      return Json(new { data = await _db.AppUsers.FirstOrDefaultAsync(u => u.Id == id) });
    }
  }
}