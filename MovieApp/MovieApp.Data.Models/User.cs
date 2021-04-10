using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApp.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime LockoutEndDate { get; set; }
        public DateTime LastLoginDateTime { get; set; }
        public bool IsLocked { get; set; }
        public int AccessFailedCount { get; set; }


        public List<Movie> movies = new List<Movie>();
        public List<Movie> Movies { get { return movies; } set { movies = value; } }

        public List<decimal> ratings = new List<decimal>();
        public List<decimal> Ratings { get { return ratings; } set { ratings = value; } }

        public List<string> reviewTexts = new List<string>();
        public List<string> ReviewTexts { get { return reviewTexts; } set { reviewTexts = value; } }
    }
}
