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
    public class GenreRepository : IRepository<Genre>
    {
        private readonly DBHelper dbHelper;

        public GenreRepository()
        {
            dbHelper = new DBHelper();
        }

        // sycn
        public int Delete(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                return connection.Execute("Delete from [Genre] where Id=@id", new { id=id });
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

        public IEnumerable<Genre> GetAll()
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                return connection.Query<Genre>("select * from [Genre]");
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
        
        public IEnumerable<Genre> GetGenreByIdWithMovie(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);

            try
            {
                string cmd = "select g.Id, g.Name, m.Id as movieId, m.TmdbUrl, m.Title, " +
                    "m.Overview, m.Tagline, m.Runtime, m.Budget, m.Revenue, " +
                    "m.BackdropUrl, m.PosterUrl, m.ImdbUrl, m.OriginalLanguage, m.ReleaseDate " +
                    "from [Genre] g inner join [MovieGenre] mg on g.Id = mg.GenreId " +
                    "inner join [Movie] m on m.Id = mg.MovieId " +
                    "where g.Id = @id";

                var genreWithMovie = connection.Query<Genre, Movie, Genre>(cmd, (g, m) => { 
                    g.Movies.Add(m); 
                    return g; 
                }, new { id = id }, splitOn: "movieId");

                return genreWithMovie;

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

        public Genre GetById(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                return connection.QueryFirstOrDefault<Genre>("select * from [Genre] where Id=@id", new { id = id });
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

        public int Insert(Genre item)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                int res = connection.Execute("insert into [Genre] values (@Name)", item);
                //int res = connection.QuerySingle<int>(@"insert into [Genre] output Inserted.Id values (@Name)", item);

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

        public int Update(Genre item)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                return connection.Execute("update [Genre] set Name=@name where Id=@id", new { id=item.Id, name=item.Name });
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
        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                var res = await connection.QueryAsync<Genre>("select * from [Genre]");
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


        public async Task<IEnumerable<Genre>> GetGenreByIdWithMovieAsync(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);

            try
            {
                string cmd = "select g.Id, g.Name, m.Id as movieId, m.TmdbUrl, m.Title, " +
                    "m.Overview, m.Tagline, m.Runtime, m.Budget, m.Revenue, " +
                    "m.BackdropUrl, m.PosterUrl, m.ImdbUrl, m.OriginalLanguage, m.ReleaseDate " +
                    "from [Genre] g inner join [MovieGenre] mg on g.Id = mg.GenreId " +
                    "inner join [Movie] m on m.Id = mg.MovieId " +
                    "where g.Id = @id";

                var genreWithMovie = await connection.QueryAsync<Genre, Movie, Genre>(cmd, (g, m) => {
                    g.Movies.Add(m);
                    return g;
                }, new { id = id }, splitOn: "movieId");

                return genreWithMovie;

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

        public async Task<int> InsertAsync(Genre item)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                var res = await connection.ExecuteAsync("insert into [Genre] values (@Name)", item);
                //int res = connection.QuerySingle<int>(@"insert into [Genre] output Inserted.Id values (@Name)", item);

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

        public async Task<int> UpdateAsync(Genre item)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                var res = await connection.ExecuteAsync("update [Genre] set Name=@name where Id=@id", new { id = item.Id, name = item.Name });
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

        public async Task<int> DeleteAsync(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                var res = await connection.ExecuteAsync("Delete from [Genre] where Id=@id", new { id = id });
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

        public async Task<Genre> GetByIdAsync(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                var res = await connection.QueryFirstOrDefaultAsync<Genre>("select * from [Genre] where Id=@id", new { id = id });
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
    }
}
