using Microsoft.EntityFrameworkCore;
using My_Book.Data.Models;
using System;

namespace My_Book.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book_Author> Book_Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Log> Logs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Publisher>()
            //    .HasOne<Book>(s => s.Book)
            //    .WithMany(g => g.Students)
            //    .HasForeignKey(s => s.CurrentGradeId);

            modelBuilder.Entity<Book_Author>().HasOne(a => a.Book)
                .WithMany(ba => ba.Book_Authors)
                .HasForeignKey(bi => bi.BookId);

            modelBuilder.Entity<Book_Author>().HasOne(a => a.Author)
                .WithMany(ba => ba.Book_Authors)
                .HasForeignKey(a => a.AuthorId);

            modelBuilder.Entity<Log>().HasKey(n => n.Id);     
        }

    }
}
