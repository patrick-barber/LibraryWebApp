using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using MvcLibrary.Data;

using System;
using System.Linq;

namespace MvcLibrary.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcLibraryContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcLibraryContext>>()))
        {
            // Look for any books.
            if (context.Book.Any())
            {
                return;   // DB has been seeded
            }
            context.Book.AddRange(
                new Book
                {
                    Title = "To Kill a Mockingbird",
                    Description = "A book about killing mockingbirds",
                    Author = "Harper Lee",
                    CoverImage = "~/Images/mockingbird.png",
                    Publisher = "Barber Inc",
                    PublicationDate = DateTime.Parse("1984-3-13"),
                    Category = "fiction",
                    ISBN = 1,
                    PageCount = 69,
                    IsAvailable = true,
                    Rating = 2,
                    UserCheckedOut = null,
                    DueDate = DateTime.Now
                },
                new Book
                {
                    Title = "Troy Web",
                    Description = "Kick ass",
                    Author = "Jon",
                    CoverImage = "~/Images/Meta.png",
                    Publisher = "Troy Web Consulting",
                    PublicationDate = DateTime.Parse("1985-3-13"),
                    Category = "Non fiction",
                    ISBN = 2,
                    PageCount = 68,
                    IsAvailable = true,
                    Rating = 5,
                    UserCheckedOut = null,
                    DueDate = DateTime.Now
                },
                new Book
                {
                    Title = "ULS",
                    Description = "UL Solutions",
                    Author = "Jenn",
                    CoverImage = "~/Images/ULS.png",
                    Publisher = "G Books",
                    PublicationDate = DateTime.Parse("1982-3-13"),
                    Category = "biography",
                    ISBN = 3,
                    PageCount = 670,
                    IsAvailable = true,
                    Rating = 3,
                    UserCheckedOut = null,
                    DueDate = DateTime.Now
                },
                new Book
                {
                    Title = "Meta",
                    Description = "Facebook",
                    Author = "Mark Zuck",
                    CoverImage = "~/Images/Meta.png",
                    Publisher = "Meta",
                    PublicationDate = DateTime.Parse("1981-3-13"),
                    Category = "fiction",
                    ISBN = 4,
                    PageCount = 70,
                    IsAvailable = true,
                    Rating = 2,
                    UserCheckedOut = null,
                    DueDate = DateTime.Now
                },
                 new Book
                 {
                     Title = "Instagram",
                     Description = "a book about instagram",
                     Author = "Mark Zuck",
                     CoverImage = "~/Images/Meta.png",
                     Publisher = "Meta",
                     PublicationDate = DateTime.Parse("1981-3-13"),
                     Category = "fiction",
                     ISBN = 5,
                     PageCount = 77,
                     IsAvailable = true,
                     Rating = 1,
                     UserCheckedOut = null,
                     DueDate = DateTime.Now
                 },
                  new Book
                  {
                      Title = "Threads",
                      Description = "Meta",
                      Author = "Mark Zuck",
                      CoverImage = "~/Images/Meta.png",
                      Publisher = "Meta",
                      PublicationDate = DateTime.Parse("1981-3-13"),
                      Category = "fiction",
                      ISBN = 5,
                      PageCount = 700,
                      IsAvailable = true,
                      Rating = 3,
                      UserCheckedOut = null,
                      DueDate = DateTime.Now
                  },
                  new Book
                  {
                      Title = "TikTok",
                      Description = "a book about that app everyone is addicted to",
                      Author = "Anonymous",
                      CoverImage = "~/Images/tiktok.png",
                      Publisher = "TikTok Inc",
                      PublicationDate = DateTime.Parse("1981-3-13"),
                      Category = "non fiction",
                      ISBN = 6,
                      PageCount = 999,
                      IsAvailable = true,
                      Rating = 5,
                      UserCheckedOut = null,
                      DueDate = DateTime.Now
                  }
            );
            context.SaveChanges();
        }
    }
}