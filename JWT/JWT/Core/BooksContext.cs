using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using JWT.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace JWT.Core
{
    public class BooksContext : IdentityDbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}