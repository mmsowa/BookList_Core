using BookListMVC.Data;
using BookListMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Services {
  public class BookService {
    private readonly AuthDbContext _db;

    public BookService(AuthDbContext db) {
      _db = db;
    }

    public async Task<object> GetBooksForUser(string id, bool includeAppUserBooks = false) {
      var appUserBooks = await _db.AppUserBooks.Where(ab => ab.AppUserId == id).ToListAsync();
      var books = await _db.Books.ToListAsync();
      var booksOfUser = new List<Book>();

      foreach (var book in appUserBooks.Select(entry => books.FirstOrDefault(b => b.Id == entry.BookId))) {
        if (book == null) {
          return null ;
        }

        if (!includeAppUserBooks) {
          book.AppUserBooks = null;
        }
        booksOfUser.Add(book);
      }

      return booksOfUser.ToList();
    }

    public async Task<bool> IsBookInUser(string bookId, string appUserId) {
      var appUserBooks = await _db.AppUserBooks.ToListAsync();
      return appUserBooks.Any(ab => ab.AppUserId == appUserId && ab.BookId == bookId);
    }
  }
}
