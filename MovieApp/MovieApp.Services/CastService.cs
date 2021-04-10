using System;
using System.Collections.Generic;
using System.Text;
using MovieApp.Data.Models;
using MovieApp.Data.Repository;
using System.Threading.Tasks;

namespace MovieApp.Services
{
    public class CastService
    {
        private readonly CastRepository castRepository;
        public CastService()
        {
            castRepository = new CastRepository();
        }

        // sync
        public int AddCast(Cast item)
        {
            return castRepository.Insert(item);
        }

        public int UpdateCast(Cast item)
        {
            return castRepository.Update(item);
        }

        public int DeleteCast(int id)
        {
            return castRepository.Delete(id);
        }

        public IEnumerable<Cast> GetAllCast()
        {
            return castRepository.GetAll();
        }

        public Cast GetCastById(int id)
        {
            return castRepository.GetById(id);
        }

        public IEnumerable<Cast> GetCastByIdWithMovie(int id)
        {
            return castRepository.GetCastByIdWithMovie(id);
        }


        // async
        public async Task<int> AddCastAsync(Cast item)
        {
            return await castRepository.InsertAsync(item);
        }

        public async Task<int> UpdateCastAsync(Cast item)
        {
            return await castRepository.UpdateAsync(item);
        }

        public async Task<int> DeleteCastAsync(int id)
        {
            return await castRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Cast>> GetAllCastAsync()
        {
            return await castRepository.GetAllAsync();
        }

        public async Task<Cast> GetCastByIdAsync(int id)
        {
            return await castRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Cast>> GetCastByIdWithMovieAsync(int id)
        {
            return await castRepository.GetCastByIdWithMovieAsync(id);
        }
    }
}
