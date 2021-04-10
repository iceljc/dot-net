using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Helpers;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Movie>> GetTopRatedMovies()
        {
            throw new System.NotImplementedException();
        }

        public async Task<PaginatedList<Movie>> GetMoviesByGenre(int genreId, int pageSize = 25, int page = 1)
        {
            var totalMoviesCountByGenre =
                await _dbContext.Genres.Include(g => g.Movies).Where(g => g.Id == genreId).SelectMany(g => g.Movies)
                    .CountAsync();

            if (totalMoviesCountByGenre == 0)
            {
                throw new NotFoundException("NO Movies found for this genre");
            }
            var movies = await _dbContext.Genres.Include(g => g.Movies).Where(g => g.Id == genreId)
                .SelectMany(g => g.Movies)
                .OrderByDescending(m => m.Revenue).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<Movie>(movies, totalMoviesCountByGenre, page, pageSize);
        }

        public async Task<IEnumerable<Movie>> GetHighestGrossingMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(24).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Review>> GetMovieReviews(int id)
        {
            var reviews = await _dbContext.Reviews.Where(r => r.MovieId == id).Include(r => r.User)
                .Select(r => new Review
                {
                    UserId = r.UserId,
                    Rating = r.Rating,
                    MovieId = r.MovieId,
                    ReviewText = r.ReviewText,
                    User = new User
                    {
                        Id = r.UserId,
                        FirstName = r.User.FirstName,
                        LastName = r.User.LastName
                    }
                }).Take(10).ToListAsync();
            return reviews;
        }

        public override async Task<Movie> GetByIdAsync(int id)
        {
            var movie = await _dbContext.Movies.Include(m => m.MovieCasts).ThenInclude(m => m.Cast)
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                throw new NotFoundException("Movie Not found");
            }

            var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty()
                .AverageAsync(r => r == null ? 0 : r.Rating);
            if (movieRating > 0) movie.Rating = movieRating;

            return movie;
        }
    }
}