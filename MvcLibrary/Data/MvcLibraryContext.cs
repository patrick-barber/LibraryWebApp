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
    public class MvcLibraryContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public MvcLibraryContext (DbContextOptions<MvcLibraryContext> options)
            : base(options)
        {
        }

        public DbSet<MvcLibrary.Models.Book> Book { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customizing the ASP.NET Identity model and overriding the defaults if needed
            builder.Entity<IdentityUserRole<string>>()
                   .HasOne<ApplicationRole>()
                   .WithMany()
                   .HasForeignKey(ur => ur.RoleId)
                   .OnDelete(DeleteBehavior.NoAction);
            //The following code will set ON DELETE NO ACTION to all Foreign Keys
            //foreach (var foreignKey in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
            //}
        }
    }
}
