using MovieApp.Data.Models;
using MovieApp.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Services
{
    public class UserService
    {
        private readonly UserRepository userRepository;
        public UserService()
        {
            userRepository = new UserRepository();
        }

        // sync
        public int AddUser(User item)
        {
            return userRepository.Insert(item);
        }

        public int UpdateUser(User item)
        {
            return userRepository.Update(item);
        }

        public int DeleteUser(int id)
        {
            return userRepository.Delete(id);
        }

        public IEnumerable<User> GetAllUser()
        {
            return userRepository.GetAll();
        }

        public User GetUserById(int id)
        {
            return userRepository.GetById(id);
        }

        public IEnumerable<User> GetUserByIdWithMovie(int id)
        {
            return userRepository.GetUserByIdWithMovie(id);
        }


        // async
        public async Task<int> AddUserAsync(User item)
        {
            return await userRepository.InsertAsync(item);
        }

        public async Task<int> UpdateUserAsync(User item)
        {
            return await userRepository.UpdateAsync(item);
        }

        public async Task<int> DeleteUserAsync(int id)
        {
            return await userRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await userRepository.GetAllAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await userRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetUserByIdWithMovieAsync(int id)
        {
            return await userRepository.GetUserByIdWithMovieAsync(id);
        }

    }
}
