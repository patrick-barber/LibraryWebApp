using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using MvcLibrary.Data;
using MvcLibrary.Migrations;
using MvcLibrary.Models;

namespace MvcLibrary.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly MvcLibraryContext _context;

        public BooksController(MvcLibraryContext context)
        {
            _context = context;
        }

        // GET: Books
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Index(string sortOrder, string category, string TitleSearchString, string AuthorSearchString, string Availability)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'MvcLibraryContext.Book' is null.");
            }

            // Set the current sorting order
            ViewData["TitleSortParam"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["AuthorSortParam"] = sortOrder == "Author" ? "author_desc" : "Author";
            ViewData["CurrentSort"] = sortOrder;

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

            // Sort based on sortOrder
            books = sortOrder switch
            {
                "title_desc" => books.OrderByDescending(b => b.Title),
                "Author" => books.OrderBy(b => b.Author),
                "author_desc" => books.OrderByDescending(b => b.Author),
                _ => books.OrderBy(b => b.Title),
            };

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

        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> FeaturedBooks()
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'MvcLibraryContext.Book' is null.");
            }
            var books = from b in _context.Book
                        select b;

            var booksList = await books.Where(book => book.IsAvailable).ToListAsync();

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
        [Authorize(Roles = "Admin, User")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Author,CoverImage,Publisher,PublicationDate,Category,ISBN,PageCount,IsAvailable,Rating,UserCheckedOut,DueDate")] Book book)
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Author,CoverImage,Publisher,PublicationDate,Category,ISBN,PageCount,IsAvailable,Rating,UserCheckedOut,DueDate")] Book book)
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

        // GET: Books/CheckOut/5
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> CheckOut(int? id)
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

        [Authorize(Roles = "Admin, User")]
        [HttpPost, ActionName("CheckOut")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(int id)
        {
            var book = await _context.Book.FindAsync(id);
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;

            if (book != null)
            {
                try
                {
                    book.IsAvailable = false;
                    book.UserCheckedOut = currentUser.Identity.Name;
                    book.DueDate = DateTime.Now.AddDays(5);
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
            return RedirectToAction(nameof(Index));
        }

        // GET: Books/CheckIn/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CheckIn(int? id)
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

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("CheckIn")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckIn(int id)
        {
            var book = await _context.Book.FindAsync(id);

            if (book != null)
            {
                try
                {
                    book.IsAvailable = true;
                    book.UserCheckedOut = string.Empty;
                    book.DueDate = DateTime.Now;
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
            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Rate/5
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Rate(int? id)
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

        [Authorize(Roles = "Admin, User")]
        [HttpPost, ActionName("Rate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rate(int id, int rating)
        {
            var book = await _context.Book.FindAsync(id);

            if (book != null)
            {
                try
                {
                    book.Rating = rating;
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

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
