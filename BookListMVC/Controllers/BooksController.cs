using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListMVC.Data;
using BookListMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListMVC.Controllers {
  [Authorize]
  public class BooksController : Controller {

    private readonly AuthDbContext _db;

    public BooksController(AuthDbContext db) {
      _db = db;
    }

    [BindProperty]
    public Book Book { get; set; }

    public AppUserBook AppUserBook { get; set; }

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

    [HttpPost]
    public IActionResult CreateAppUserBookEntry(string AppUserId, string BookId) {
      if (ModelState.IsValid) {
        AppUserBook = new AppUserBook();
        _db.AppUserBooks.AddAsync(AppUserBook);
      }

      return View(AppUserBook);
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
    #endregion
  }
}