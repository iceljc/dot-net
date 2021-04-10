using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Helpers;
using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        Task<PagedResultSet<MovieResponseModel>> GetMoviesByPagination(int pageSize = 20, int page = 1, string title = "");
        Task<PagedResultSet<MovieResponseModel>> GetAllMoviePurchasesByPagination(int pageSize = 20, int page = 1);
        Task<PagedResultSet<MovieResponseModel>> GetAllPurchasesByMovieId(int movieId);
        Task<PaginatedList<MovieResponseModel>> GetMoviesByGenre(int genreId, int pageSize = 25, int page = 1);

        Task<MovieDetailsResponseModel> GetMovieAsync(int id);
        Task<IEnumerable<ReviewMovieResponseModel>> GetReviewsForMovie(int id);

        Task<int> GetMoviesCount(string title = "");
        Task<IEnumerable<MovieResponseModel>> GetTopRatedMovies();
        Task<IEnumerable<MovieResponseModel>> GetHighestGrossingMovies();

        Task<MovieDetailsResponseModel> CreateMovie(MovieCreateRequest movieCreateRequest);
        Task<MovieDetailsResponseModel> UpdateMovie(MovieCreateRequest movieCreateRequest);
    }
}