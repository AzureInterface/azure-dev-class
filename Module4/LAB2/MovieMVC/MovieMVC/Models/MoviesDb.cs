using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MovieMVC.Models
{
    public class MoviesDb : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public MoviesDb() : base("DefaultConnection")
        {
        }

        public static MoviesDb Create()
        {
            return new MoviesDb();
        }
    }
}