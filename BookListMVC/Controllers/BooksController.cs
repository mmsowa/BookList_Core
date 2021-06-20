using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListMVC.Areas.Identity.Data;
using BookListMVC.Data;
using BookListMVC.Models;
using BookListMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListMVC.Controllers {
  [Authorize]
  public class BooksController : Controller {

    private readonly AuthDbContext _db;
    private readonly BookService _bookService;

    public BooksController(AuthDbContext db, BookService bookService) {
      _db = db;
      _bookService = bookService;
    }

    [BindProperty]
    public Book Book { get; set; }

    [BindProperty]
    public AppUserBook AppUserBook { get; set; }

    [BindProperty]
    public AppUser AppUser { get; set; }

    [Route("/")]
    [Route("/Books")]
    public IActionResult Index() {
      return View();
    }

    public IActionResult Upsert(string id) {
      Book = new Book();
      if (id == null) {
        //create
        return View(Book);
      }

      //update
      Book = _db.Books.FirstOrDefault(u => u.Id == id);
      if (Book == null) {
        return NotFound();
      }
      return View(Book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert() {
      if (!ModelState.IsValid) {
        return View();
      }

      if (Book.Id == null) {
        _db.Books.Add(Book);
      } else {
        _db.Books.Update(Book);
      }

      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() {
      return Json(new { data = await _db.Books.ToListAsync() });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string id) {
      var bookFromDb = await _db.Books.FirstOrDefaultAsync(u => u.Id == id);
      if (bookFromDb == null) {
        return Json(new { success = false, message = "Error while Deleting" });
      }

      _db.Books.Remove(bookFromDb);
      await _db.SaveChangesAsync();
      return Json(new { success = true, message = "Delete successful" });
    }

    [HttpGet]
    public async Task<IActionResult> GetBook(string id) {
      return Json(new { data = await _db.Books.FirstOrDefaultAsync(bk => bk.Id == id) });
    }

    [HttpPost]
    public async Task<IActionResult> AddBookToUser(string appUserId, string bookId) {
      var appUser = await _db.AppUsers.FirstOrDefaultAsync(u => u.Id == appUserId);
      var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == bookId);

      if (appUser == null || book == null) {
        return RedirectToAction("Index");
      }

      var appUserBook = new AppUserBook {
        AppUserId = appUserId,
        AppUser = appUser,
        BookId = bookId,
        Book = book,
      };

      appUser.AppUserBooks ??= new List<AppUserBook>();

      if (await IsBookInUser(bookId, appUserId)) {
        return RedirectToAction("Index");
      }

      appUser.AppUserBooks.Add(appUserBook);
      await _db.SaveChangesAsync();

      return RedirectToAction("Index");
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveBookFromUser(string bookId, string appUserId) {
      var appUserBook = await _db.AppUserBooks.FirstAsync((row) => row.AppUserId == appUserId && row.BookId == bookId);
      if (appUserBook == null) {
        return RedirectToAction("Index");
      }

      _db.Remove(appUserBook);
      await _db.SaveChangesAsync();

      return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> GetBooksForUser(string id, bool includeAppUserBooks = false) {
      return Json(await _bookService.GetBooksForUser(id, includeAppUserBooks));
    }

    [HttpGet]
    public async Task<bool> IsBookInUser(string bookId, string appUserId) {
      return await _bookService.IsBookInUser(bookId, appUserId);
    }
  }
}