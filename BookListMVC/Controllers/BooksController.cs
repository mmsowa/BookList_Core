using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BookListMVC.Areas.Identity.Data;
using BookListMVC.Data;
using BookListMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListMVC.Controllers {
  public class BooksController : Controller {

    private readonly AuthDbContext _db;

    public BooksController(AuthDbContext db) {
      _db = db;
    }

    [BindProperty]
    public Book Book { get; set; }

    [BindProperty]
    public AppUserBook AppUserBook { get; set; }

    public AppUser AppUser { get; set; }

    public IActionResult Index() {
      return View();
    }

    public IActionResult AppUserBooks() {
      return View();
    }

    #region API Calls

    public IActionResult Upsert(string? id) {
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
      if (ModelState.IsValid) {
        if (Book.Id == null) {
          //create
          _db.Books.Add(Book);
        } else {
          _db.Books.Update(Book);
        }
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(Book);
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
    public IActionResult AddBookToUser(string appUserId, string bookId) {
      var appUser = _db.AppUsers.FirstOrDefault(u => u.Id == appUserId);
      var book = _db.Books.FirstOrDefault(b => b.Id == bookId);

      if (appUser != null && book != null) {
        var appUserBook = new AppUserBook {
          AppUserId = appUserId,
          AppUser = appUser,
          BookId = bookId,
          Book = book,
        };

        if (appUser.AppUserBooks == null) {
          appUser.AppUserBooks = new List<AppUserBook>();
        }

        if (appUser.AppUserBooks.Contains(appUserBook)) {
          return RedirectToAction("Index");
        } else {
          appUser.AppUserBooks.Add(appUserBook);
          _db.SaveChanges();
        }
      }

      return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> GetBooksForUser (string userId) {
      var appUserBooks = await _db.AppUserBooks.Where(ab => ab.AppUserId == userId).ToListAsync();
      var books = await _db.Books.ToListAsync();
      var booksOfUser = new List<object>();

      foreach(var entry in appUserBooks) {
        booksOfUser.Add(books.FirstOrDefault(b => b.Id == entry.BookId));
      }

      return Json(new { data = booksOfUser.ToList() });
    }

    #endregion
  }
}