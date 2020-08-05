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

      builder.Entity<AppUserBook>().HasKey(ab => new { ab.AppUserId, ab.BookId });
      builder.Entity<AppUserBook>().HasOne(au => au.AppUser).WithMany(a => a.AppUserBooks).HasForeignKey(au => au.AppUserId);
      builder.Entity<AppUserBook>().HasOne(au => au.Book).WithMany(a => a.AppUserBooks).HasForeignKey(au => au.BookId);
    }

    public DbSet<AppUser> AppUsers { get; set; }

    public DbSet<Book> Books { get; set; }

    public DbSet<AppUserBook> AppUserBooks { get; set; }
  }
}
