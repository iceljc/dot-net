using MovieApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using System.Threading.Tasks;
using System.Linq;

namespace MovieApp.Data.Repository
{
    public class UserRepository : IRepository<User>
    {

        private readonly DBHelper dbHelper;

        public UserRepository()
        {
            dbHelper = new DBHelper();
        }

        // sync
        public int Delete(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                return connection.Execute("Delete from [User] where Id=@id", new { id = id });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Dispose();
            }

            return 0;
        }

        public IEnumerable<User> GetAll()
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                string cmd = "select * from (select top 10 * from [User] order by Id desc) sub order by Id asc";
                return connection.Query<User>(cmd);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Dispose();
            }

            return null;
        }

        public User GetById(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                return connection.QueryFirstOrDefault<User>("select * from [User] where Id=@id", new { id = id });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Dispose();
            }

            return null;
        }

        public IEnumerable<User> GetUserByIdWithMovie(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);

            try
            {
                string cmd = "select u.Id, u.FirstName, u.LastName, u.DateOfBirth, u.Email, " +
                    "u.HashedPassword, u.Salt, u.PhoneNumber, u.TwoFactorEnabled, " +
                    "u.LockoutEndDate, u.LastLoginDateTime, u.IsLocked, u.AccessFailedCount, " +
                    "m.Id as movieId, m.TmdbUrl, m.Title, m.Overview, " +
                    "m.Tagline, m.Runtime, m.Budget, m.Revenue, " +
                    "m.BackdropUrl, m.PosterUrl, m.ImdbUrl, m.OriginalLanguage, m.ReleaseDate, " +
                    "r.Rating, r.ReviewText " +
                    "from [User] u inner join [Review] r on u.Id = r.UserId " +
                    "inner join [Movie] m on m.Id = r.MovieId " +
                    "where u.Id = @id";

                var userWithMovie = connection.Query<User, Movie, decimal, string, User>(cmd, (u, m, rating, txt) => {
                    u.Movies.Add(m);
                    u.Ratings.Add(rating);
                    u.ReviewTexts.Add(txt);
                    return u;
                }, new { id = id }, splitOn: "movieId, Rating, ReviewText");

                return userWithMovie;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Dispose();
            }

            return null;
        }

        public int Insert(User item)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                string cmd = "insert into [User] values " +
                    "(@FirstName, @LastName, @DateOfBirth, @Email, " +
                    "@HashedPassword, @Salt, @PhoneNumber, @TwoFactorEnabled, " +
                    "@LockoutEndDate, @LastLoginDateTime, @IsLocked, @AccessFailedCount)";
                int res = connection.Execute(cmd, item);
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Dispose();
            }

            return 0;
        }

        public int Update(User item)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                string cmd = "update [User] set " +
                    "FirstName=@firstName, LastName=@lastName, DateOfBirth=@dateOfBirth, Email=@email, " +
                    "HashedPassword=@hashedPassword, Salt=@salt, PhoneNumber=@phoneNumber, " +
                    "IsLocked=@isLocked " +
                    "where Id=@id";
                return connection.Execute(cmd, 
                    new { id = item.Id, firstName = item.FirstName, lastName = item.LastName, dateOfBirth = item.DateOfBirth, email = item.Email,
                        hashedPassword = item.HashedPassword, salt = item.Salt, phoneNumber = item.PhoneNumber, isLocked = item.IsLocked
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Dispose();
            }

            return 0;
        }

        


        // async
        public async Task<int> DeleteAsync(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                var res = await connection.ExecuteAsync("Delete from [User] where Id=@id", new { id = id });
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Dispose();
            }

            return 0;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                string cmd = "select * from (select top 10 * from [User] order by Id desc) sub order by Id asc";
                var res = await connection.QueryAsync<User>(cmd);
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Dispose();
            }

            return null;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                var res = await connection.QueryFirstOrDefaultAsync<User>("select * from [User] where Id=@id", new { id = id });
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Dispose();
            }

            return null;
        }

        public async Task<int> InsertAsync(User item)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                string cmd = "insert into [User] values " +
                    "(@FirstName, @LastName, @DateOfBirth, @Email, " +
                    "@HashedPassword, @Salt, @PhoneNumber, @TwoFactorEnabled, " +
                    "@LockoutEndDate, @LastLoginDateTime, @IsLocked, @AccessFailedCount)";
                var res = await connection.ExecuteAsync(cmd, item);
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Dispose();
            }

            return 0;
        }

        public async Task<int> UpdateAsync(User item)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                string cmd = "update [User] set " +
                    "FirstName=@firstName, LastName=@lastName, DateOfBirth=@dateOfBirth, Email=@email, " +
                    "HashedPassword=@hashedPassword, Salt=@salt, PhoneNumber=@phoneNumber, " +
                    "IsLocked=@isLocked " +
                    "where Id=@id";
                var res = await connection.ExecuteAsync(cmd,
                    new
                    {
                        id = item.Id,
                        firstName = item.FirstName,
                        lastName = item.LastName,
                        dateOfBirth = item.DateOfBirth,
                        email = item.Email,
                        hashedPassword = item.HashedPassword,
                        salt = item.Salt,
                        phoneNumber = item.PhoneNumber,
                        isLocked = item.IsLocked
                    });
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Dispose();
            }

            return 0;
        }

        public async Task<IEnumerable<User>> GetUserByIdWithMovieAsync(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);

            try
            {
                string cmd = "select u.Id, u.FirstName, u.LastName, u.DateOfBirth, u.Email, " +
                    "u.HashedPassword, u.Salt, u.PhoneNumber, u.TwoFactorEnabled, " +
                    "u.LockoutEndDate, u.LastLoginDateTime, u.IsLocked, u.AccessFailedCount, " +
                    "m.Id as movieId, m.TmdbUrl, m.Title, m.Overview, " +
                    "m.Tagline, m.Runtime, m.Budget, m.Revenue, " +
                    "m.BackdropUrl, m.PosterUrl, m.ImdbUrl, m.OriginalLanguage, m.ReleaseDate, " +
                    "r.Rating, r.ReviewText " +
                    "from [User] u inner join [Review] r on u.Id = r.UserId " +
                    "inner join [Movie] m on m.Id = r.MovieId " +
                    "where u.Id = @id";

                var userWithMovie = await connection.QueryAsync<User, Movie, decimal, string, User>(cmd, (u, m, rating, txt) => {
                    u.Movies.Add(m);
                    u.Ratings.Add(rating);
                    u.ReviewTexts.Add(txt);
                    return u;
                }, new { id = id }, splitOn: "movieId, Rating, ReviewText");

                return userWithMovie;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Dispose();
            }

            return null;
        }
    }
}
