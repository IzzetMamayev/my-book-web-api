using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using My_Book.Data.Models;
using System;
using System.Linq;

namespace My_Book.Data
{
    public class AppDbInitialer
    {
        public static void Seed(IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                if (!context.Books.Any())
                {
                    context.Books.AddRange(new Book()
                    {
                        Title = "First book",
                        Description = "First book description",
                        IsRead = true,
                        DateRead = DateTime.Now.AddDays(-10),
                        Rate = 4,
                        Genre = "fantasy",
                        CoverUrl = "https....",
                        DateAdded = DateTime.Now,
                    }, new Book()
                    {
                        Title = "Second book",
                        Description = "Second book description",
                        IsRead = false,
                        Genre = "fantasy",
                        CoverUrl = "https....",
                        DateAdded = DateTime.Now,
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
