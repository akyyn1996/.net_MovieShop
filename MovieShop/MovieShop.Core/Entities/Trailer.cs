using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.Entities
{
    public class Trailer
    {
        // one class can have more than one trailer
        public int Id { get; set; }
        // foreign key from table movie
        public int MovieId { get; set; }
        public string TrailerUrl { get; set; }
        public string Name { get; set; }

        // Navigation proporties , help us to related entities
        // trailerId 24 => get me Movie title and movie Overview
        public Movie Movie { get; set; }
    }
}
