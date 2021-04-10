using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApp.Data.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string TmdbUrl { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Tagline { get; set; }
        public int Runtime { get; set; }
        public decimal Budget { get; set; }
        public decimal Revenue { get; set; }
        public string BackdropUrl { get; set; }
        public string PosterUrl { get; set; }
        public string ImdbUrl { get; set; }
        public string OriginalLanguage { get; set; }
        public DateTime ReleaseDate { get; set; }



        public List<Genre> genres = new List<Genre>();
        public List<Genre> Genres { get { return genres; } set { genres = value; } }



        public List<Cast> casts = new List<Cast>();
        public List<Cast> Casts { get { return casts; } set { casts = value; } }

        public List<string> characters = new List<string>();
        public List<string> Characters { get { return characters; } set { characters = value; } }



        public List<User> users = new List<User>();
        public List<User> Users { get { return users; } set { users = value; } }

        public List<decimal> ratings = new List<decimal>();
        public List<decimal> Ratings { get { return ratings; } set { ratings = value; } }

        public List<string> reviewTexts = new List<string>();
        public List<string> ReviewTexts { get { return reviewTexts; } set { reviewTexts = value; } }

    }
}
