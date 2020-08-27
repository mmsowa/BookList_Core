using BookListMVC.Data;
using BookListMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Services {
  public class BookService {
    private readonly AuthDbContext _db;

    public BookService(AuthDbContext db) {
      _db = db;
    }

    public async Task<object> GetBooksForUser(string id) {
      var appUserBooks = await _db.AppUserBooks.Where(ab => ab.AppUserId == id).ToListAsync();
      var books = await _db.Books.ToListAsync();
      var booksOfUser = new List<Book>();

      foreach (var entry in appUserBooks) {
        booksOfUser.Add(books.FirstOrDefault(b => b.Id == entry.BookId));
      }

      return (new { data = booksOfUser.ToList() });
    }

    public async Task<bool> IsBookInUser(string bookId, string appUserId) {
      var appUserBooks = await _db.AppUserBooks.ToListAsync();
      var appUser = await _db.AppUsers.FirstOrDefaultAsync(u => u.Id == appUserId);
      var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == bookId);

      return appUserBooks.Any(ab => ab.AppUserId == appUserId && ab.BookId == bookId);
    }
  }
}
