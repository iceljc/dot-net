using System;
using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public class UserRegisterResponseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UserProfileResponseModel
    {
    }

    public class UserLoginResponseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public List<RoleModel> Roles { get; set; }
    }

    public class RoleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PurchaseResponseModel
    {
        public int UserId { get; set; }
        public List<PurchasedMovieResponseModel> PurchasedMovies { get; set; }

        public class PurchasedMovieResponseModel : MovieResponseModel
        {
            public DateTime PurchaseDateTime { get; set; }
        }
    }

    public class MovieResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PosterUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
    }

    public class MovieDetailsResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PosterUrl { get; set; }
        public string BackdropUrl { get; set; }

        public decimal? Rating { get; set; }
        public string Overview { get; set; }
        public string Tagline { get; set; }
        public decimal? Budget { get; set; }
        public decimal? Revenue { get; set; }
        public string ImdbUrl { get; set; }
        public string TmdbUrl { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? RunTime { get; set; }
        public decimal? Price { get; set; }
        public int FavoritesCount { get; set; }
        public List<CastResponseModel> Casts { get; set; }
        public List<GenreModel> Genres { get; set; }

        public class CastResponseModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Gender { get; set; }
            public string TmdbUrl { get; set; }
            public string ProfilePath { get; set; }
            public string Character { get; set; }
        }
    }

    public class GenreModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MovieChartResponseModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int PurchaseCount { get; set; }
    }

    public class FavoriteResponseModel
    {
        public int UserId { get; set; }
        public List<FavoriteMovieResponseModel> FavoriteMovies { get; set; }

        public class FavoriteMovieResponseModel : MovieResponseModel
        {
        }
    }

    public class CastDetailsResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string TmdbUrl { get; set; }
        public string ProfilePath { get; set; }
        public IEnumerable<MovieResponseModel> Movies { get; set; }
    }

    public class ReviewResponseModel
    {
        public int UserId { get; set; }
        public List<ReviewMovieResponseModel> MovieReviews { get; set; }
    }

    public class ReviewMovieResponseModel
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public string ReviewText { get; set; }
        public decimal Rating { get; set; }
        public string Name { get; set; }
    }
}