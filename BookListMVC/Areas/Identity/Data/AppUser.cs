using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using BookListMVC.Models;
using Microsoft.AspNetCore.Identity;

namespace BookListMVC.Areas.Identity.Data {
  public class AppUser : IdentityUser {
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string FirstName { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; }

    public ICollection<AppUserBook> AppUserBooks { get; set; }
  }
}
