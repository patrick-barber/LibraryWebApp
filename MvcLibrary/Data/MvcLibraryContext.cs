using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MvcLibrary.Data
{
    public class MvcLibraryContext : IdentityDbContext<IdentityUser>
    {
        public MvcLibraryContext (DbContextOptions<MvcLibraryContext> options)
            : base(options)
        {
        }

        public DbSet<MvcLibrary.Models.Book> Book { get; set; } = default!;
    }
}
