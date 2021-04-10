using MovieApp.Data.Models;
using MovieApp.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Services
{
    public class MovieService
    {
        private readonly MovieRepository movieRepository;

        public MovieService()
        {
            movieRepository = new MovieRepository();
        }

        // sync
        public int AddMovie(Movie item)
        {
            return movieRepository.Insert(item);
        }

        public int UpdateMovie(Movie item)
        {
            return movieRepository.Update(item);
        }

        public int DeleteMovie(int id)
        {
            return movieRepository.Delete(id);
        }

        public IEnumerable<Movie> GetAllMovie()
        {
            return movieRepository.GetAll();
        }

        public Movie GetMovieById(int id)
        {
            return movieRepository.GetById(id);
        }

        public IEnumerable<Movie> GetMovieByIdWithGenre(int id) {
            return movieRepository.GetMovieByIdWithGenre(id);
        }

        public IEnumerable<Movie> GetMovieByIdWithCast(int id)
        {
            return movieRepository.GetMovieByIdWithCast(id);
        }

        public IEnumerable<Movie> GetMovieByIdWithUser(int id)
        {
            return movieRepository.GetMovieByIdWithUser(id);
        }


        // async
        public async Task<int> AddMovieAsync(Movie item)
        {
            return await movieRepository.InsertAsync(item);
        }

        public async Task<int> UpdateMovieAsync(Movie item)
        {
            return await movieRepository.UpdateAsync(item);
        }

        public async Task<int> DeleteMovieAsync(int id)
        {
            return await movieRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Movie>> GetAllMovieAsync()
        {
            return await movieRepository.GetAllAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await movieRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Movie>> GetMovieByIdWithGenreAsync(int id)
        {
            return await movieRepository.GetMovieByIdWithGenreAsync(id);
        }

        public async Task<IEnumerable<Movie>> GetMovieByIdWithCastAsync(int id)
        {
            return await movieRepository.GetMovieByIdWithCastAsync(id);
        }

        public async Task<IEnumerable<Movie>> GetMovieByIdWithUserAsync(int id)
        {
            return await movieRepository.GetMovieByIdWithUserAsync(id);
        }
    }
}
