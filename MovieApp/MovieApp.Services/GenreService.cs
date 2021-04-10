using MovieApp.Data.Models;
using MovieApp.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Services
{
    public class GenreService
    {
        private readonly GenreRepository genreRepository;
        public GenreService()
        {
            genreRepository = new GenreRepository();
        }

        // sync
        public int AddGenre(Genre item)
        {
            return genreRepository.Insert(item);
        }

        public int UpdateGenre(Genre item)
        {
            return genreRepository.Update(item);
        }

        public int DeleteGenre(int id)
        {
            return genreRepository.Delete(id);
        }

        public IEnumerable<Genre> GetAllGenre()
        {
            return genreRepository.GetAll();
        }

        public Genre GetGenreById(int id)
        {
            return genreRepository.GetById(id);
        }

        public IEnumerable<Genre> GetGenreByIdWithMovie(int id)
        {
            return genreRepository.GetGenreByIdWithMovie(id);
        }


        // async
        public async Task<int> AddGenreAsync(Genre item)
        {
            return await genreRepository.InsertAsync(item);
        }

        public async Task<int> UpdateGenreAsync(Genre item)
        {
            return await genreRepository.UpdateAsync(item);
        }

        public async Task<int> DeleteGenreAsync(int id)
        {
            return await genreRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Genre>> GetAllGenreAsync()
        {
            return await genreRepository.GetAllAsync();
        }

        public async Task<Genre> GetGenreByIdAsync(int id)
        {
            return await genreRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Genre>> GetGenreByIdWithMovieAsync(int id)
        {
            return await genreRepository.GetGenreByIdWithMovieAsync(id);
        }
    }
}
