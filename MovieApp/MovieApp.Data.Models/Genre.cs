using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApp.Data.Models
{
    public class Genre
    {
        public List<Movie> movies = new List<Movie>();
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Movie> Movies { get { return movies; } set { movies = value; } }
    }
}
