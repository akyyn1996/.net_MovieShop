using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieShop.Core.Entities
{
    [Table(name: "Genre")]
    public class Genre
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
