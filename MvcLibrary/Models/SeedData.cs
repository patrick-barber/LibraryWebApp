using Bogus.DataSets;
using Bogus;

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
        List<Book> books = new();
        var bookGenerator = GetBookGenerator();
        var generatedBooks = bookGenerator.Generate(10);
        books.AddRange(generatedBooks);

        using (var context = new MvcLibraryContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcLibraryContext>>()))
        {
            // Look for any books.
            if (context.Book.Any())
            {
                return;   // DB has been seeded
            }
            context.Book.AddRange(books);

            //context.Book.AddRange(
            //    new Book
            //    {
            //        Title = "To Kill a Mockingbird",
            //        Description = "A book about killing mockingbirds",
            //        Author = "Harper Lee",
            //        CoverImage = "~/Images/mockingbird.png",
            //        Publisher = "Barber Inc",
            //        PublicationDate = DateTime.Parse("1984-3-13"),
            //        Category = "fiction",
            //        ISBN = 1,
            //        PageCount = 69,
            //        IsAvailable = true,
            //        Rating = 2,
            //        UserCheckedOut = null,
            //        DueDate = DateTime.Now
            //    },
            //    new Book
            //    {
            //        Title = "Troy Web",
            //        Description = "Kick ass",
            //        Author = "Jon",
            //        CoverImage = "~/Images/Meta.png",
            //        Publisher = "Troy Web Consulting",
            //        PublicationDate = DateTime.Parse("1985-3-13"),
            //        Category = "Non fiction",
            //        ISBN = 2,
            //        PageCount = 68,
            //        IsAvailable = true,
            //        Rating = 5,
            //        UserCheckedOut = null,
            //        DueDate = DateTime.Now
            //    },
            //    new Book
            //    {
            //        Title = "ULS",
            //        Description = "UL Solutions",
            //        Author = "Jenn",
            //        CoverImage = "~/Images/ULS.png",
            //        Publisher = "G Books",
            //        PublicationDate = DateTime.Parse("1982-3-13"),
            //        Category = "biography",
            //        ISBN = 3,
            //        PageCount = 670,
            //        IsAvailable = true,
            //        Rating = 3,
            //        UserCheckedOut = null,
            //        DueDate = DateTime.Now
            //    },
            //    new Book
            //    {
            //        Title = "Meta",
            //        Description = "Facebook",
            //        Author = "Mark Zuck",
            //        CoverImage = "~/Images/Meta.png",
            //        Publisher = "Meta",
            //        PublicationDate = DateTime.Parse("1981-3-13"),
            //        Category = "fiction",
            //        ISBN = 4,
            //        PageCount = 70,
            //        IsAvailable = true,
            //        Rating = 2,
            //        UserCheckedOut = null,
            //        DueDate = DateTime.Now
            //    },
            //     new Book
            //     {
            //         Title = "Instagram",
            //         Description = "a book about instagram",
            //         Author = "Mark Zuck",
            //         CoverImage = "~/Images/Meta.png",
            //         Publisher = "Meta",
            //         PublicationDate = DateTime.Parse("1981-3-13"),
            //         Category = "fiction",
            //         ISBN = 5,
            //         PageCount = 77,
            //         IsAvailable = true,
            //         Rating = 1,
            //         UserCheckedOut = null,
            //         DueDate = DateTime.Now
            //     },
            //      new Book
            //      {
            //          Title = "Threads",
            //          Description = "Meta",
            //          Author = "Mark Zuck",
            //          CoverImage = "~/Images/Meta.png",
            //          Publisher = "Meta",
            //          PublicationDate = DateTime.Parse("1981-3-13"),
            //          Category = "fiction",
            //          ISBN = 5,
            //          PageCount = 700,
            //          IsAvailable = true,
            //          Rating = 3,
            //          UserCheckedOut = null,
            //          DueDate = DateTime.Now
            //      },
            //      new Book
            //      {
            //          Title = "TikTok",
            //          Description = "a book about that app everyone is addicted to",
            //          Author = "Anonymous",
            //          CoverImage = "~/Images/tiktok.png",
            //          Publisher = "TikTok Inc",
            //          PublicationDate = DateTime.Parse("1981-3-13"),
            //          Category = "non fiction",
            //          ISBN = 6,
            //          PageCount = 999,
            //          IsAvailable = true,
            //          Rating = 5,
            //          UserCheckedOut = null,
            //          DueDate = DateTime.Now
            //      }
            //);
            context.SaveChanges();
        }
    }
    private static Faker<Book> GetBookGenerator()
    {
        return new Faker<Book>()
        .RuleFor(e => e.Title, f => f.Company.CompanyName())
        .RuleFor(e => e.Description, f => f.Lorem.Paragraph(1))
        .RuleFor(e => e.Author, f => f.Name.FullName())
        .RuleFor(e => e.CoverImage, f => f.PickRandom("~/Images/tiktok.png", "~/Images/meta.png","~/Images/mockingbird.png", "~/Images/ULS.png"))
        .RuleFor(e => e.Publisher, f => f.Company.CompanyName())
        .RuleFor(e => e.PublicationDate, f => f.Date.Past(1, DateTime.Now))
        .RuleFor(e => e.Category, f => f.PickRandom<Category>().ToString())
        .RuleFor(e => e.ISBN, f => f.Random.Int(1, 1000))
        .RuleFor(e => e.PageCount, f => f.Random.Int(1, 1000))
        .RuleFor(e => e.IsAvailable, f => true)
        .RuleFor(e => e.Rating, f => f.Random.Int(1, 5))
        .RuleFor(e => e.UserCheckedOut, f => string.Empty)
        .RuleFor(e => e.DueDate, f => DateTime.Now);
    }

    public enum Category
    {
        Fiction,
        NonFiction,
        Biography,
        Science,
        History,
        Mystery,
        Fantasy,
        Romance,
        Horror,
        SelfHelp,
        Poetry
    }

}