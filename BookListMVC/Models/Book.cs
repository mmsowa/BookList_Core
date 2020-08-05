using BookListMVC.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Models {
  public class Book {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Author { get; set; }

    public string ISBN { get; set; }

    public ICollection<AppUserBook> AppUserBooks { get; set; }
  }
}
