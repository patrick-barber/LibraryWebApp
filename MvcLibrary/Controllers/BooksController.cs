using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using MvcLibrary.Data;
using MvcLibrary.Models;

namespace MvcLibrary.Controllers
{
    public class BooksController : Controller
    {
        private readonly MvcLibraryContext _context;

        public BooksController(MvcLibraryContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index(string category, string TitleSearchString, string AuthorSearchString, string Availability)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'MvcLibraryContext.Book' is null.");
            }

            // Use LINQ to get list of genres.
            IQueryable<string> categoryQuery = from b in _context.Book
                                            orderby b.Category
                                            select b.Category;
            var books = from b in _context.Book
                         select b;

            if (!string.IsNullOrEmpty(TitleSearchString))
            {
                books = books.Where(s => s.Title!.ToUpper().Contains(TitleSearchString.ToUpper()));
            }

            if (!string.IsNullOrEmpty(category))
            {
                books = books.Where(x => x.Category == category);
            }

            if (!string.IsNullOrEmpty(AuthorSearchString))
            {
                books = books.Where(s => s.Author!.ToUpper().Contains(AuthorSearchString.ToUpper()));
            }

            if (!string.IsNullOrEmpty(Availability))
            {
                if (Availability.Equals("Checked Out"))
                {
                    books = books.Where(s => s.IsAvailable == false);
                }
                else if (Availability.Equals("Available"))
                {
                    books = books.Where(s => s.IsAvailable == true);
                }
                else
                {
                    //Availability == All
                    //Return all books
                }    
            }

            var options = new List<string>
            {
                "Checked Out",
                "Available"
            };

            var bookCategoryVM = new BookCategoryViewModel
            {
                Categories = new SelectList(await categoryQuery.Distinct().ToListAsync()),
                Books = await books.ToListAsync(),
                AvailabilityOptions = new SelectList(options)
            };

            return View(bookCategoryVM);
        }

        public async Task<IActionResult> FeaturedBooks()
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'MvcLibraryContext.Book' is null.");
            }
            var books = from b in _context.Book
                        select b;

            var booksList = await books.ToListAsync();

            // Shuffle the list and take 5 random books
            var random = new Random();
            var randomBooks = booksList.OrderBy(x => random.Next()).Take(5).ToList();

            // Pass the random books to the view model
            var featuredBooksVM = new FeaturedBooksViewModel
            {
                Books = randomBooks
            };
   
            return View(featuredBooksVM);
        }  

            // GET: Books/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Author,CoverImage,Publisher,PublicationDate,Category,ISBN,PageCount,IsAvailable,Rating")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Author,CoverImage,Publisher,PublicationDate,Category,ISBN,PageCount,IsAvailable,Rating")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, bool notUsed)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
