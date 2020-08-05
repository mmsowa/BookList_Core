using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListMVC.Areas.Identity.Data;
using BookListMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookListMVC.Data {
  public class AuthDbContext : IdentityDbContext<AppUser> {
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder builder) {
      base.OnModelCreating(builder);
    }

    public DbSet<AppUser> AppUsers { get; set; }

    public DbSet<Book> Books { get; set; }
  }
}
