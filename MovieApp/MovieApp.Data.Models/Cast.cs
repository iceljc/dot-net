using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApp.Data.Models
{
    public class Cast
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string TmdbUrl { get; set; }
        public string ProfilePath { get; set; }

        public List<Movie> movies = new List<Movie>();
        public List<Movie> Movies { get { return movies; } set { movies = value; } }

        public List<string> characters = new List<string>();
        public List<string> Characters { get { return characters; } set { characters = value; } }
    }
}
