using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookListMVC.Areas.Identity.Data
{
  public class LibraryCard
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public DateTime LastDayOfValidity { get; set; }

    public bool isBanned { get; set; }
  }
}