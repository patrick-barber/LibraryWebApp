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
            context.SaveChanges();
        }
    }
    private static Faker<Book> GetBookGenerator()
    {
        return new Faker<Book>()
        .RuleFor(e => e.Title, f => f.Company.CompanyName())
        .RuleFor(e => e.Description, f => f.Lorem.Sentence())
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