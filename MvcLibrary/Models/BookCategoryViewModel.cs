using Microsoft.AspNetCore.Mvc.Rendering;

using System.Collections.Generic;

namespace MvcLibrary.Models;

public class BookCategoryViewModel
{
    public List<Book>? Books { get; set; }
    public SelectList? Categories { get; set; }
    public string? Category { get; set; }
    public string? TitleSearchString { get; set; }
    public string? AuthorSearchString { get; set; }
    public SelectList? AvailabilityOptions { get; set; }
    public string? Availability {  get; set; }
}