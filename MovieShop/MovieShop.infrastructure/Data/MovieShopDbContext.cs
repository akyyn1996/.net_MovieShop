using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;

namespace MovieShop.infrastructure.Data
{
    //how to find?
    public class MovieShopDbContext: DbContext
    {
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options): base(options)
        {

        }

        public DbSet<Genre> Genres { get; set; }


    }
}
