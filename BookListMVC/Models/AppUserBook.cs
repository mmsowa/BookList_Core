using BookListMVC.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Models {
  public class AppUserBook {
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }

    public string BookId { get; set; }
    public Book Book { get; set; }

    public DateTime BorrowedAt { get; set; }
  }
}
