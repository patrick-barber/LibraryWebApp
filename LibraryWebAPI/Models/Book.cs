﻿namespace LibraryWebAPI.Models
{
    public class Book
    {
        //Title, Author, Description, and Cover Image, Publisher, Publication Date, Category, ISBN, and Page Count, Availability
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string CoverImage { get; set; }
        public string Publisher { get; set; }
        public string PublicationDate { get; set; }
        public string Category { get; set; }
        public string ISBN { get; set; }
        public int PageCount    { get; set; }
        public bool IsAvailable { get; set; }

    }
}
