using Microsoft.AspNetCore.Mvc.Rendering;

using System.Collections.Generic;

namespace MvcLibrary.Models;

public class FeaturedBooksViewModel
{
    public List<Book>? Books { get; set; }
}