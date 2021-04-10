using MovieApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;

namespace MovieApp.Data.Repository
{
    public class CastRepository : IRepository<Cast>
    {
        private readonly DBHelper dbHelper;

        public CastRepository()
        {
            dbHelper = new DBHelper();
        }

        // sync
        public int Delete(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                return connection.Execute("Delete from [Cast] where Id=@id", new { id = id });
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

        public IEnumerable<Cast> GetAll()
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                string cmd = "select * from (select top 10 * from [Cast] order by Id desc) sub order by Id asc";
                return connection.Query<Cast>(cmd);
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


        public IEnumerable<Cast> GetCastByIdWithMovie(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                string cmd = "select c.Id, c.Name, c.Gender, c.TmdbUrl, c.ProfilePath, " +
                    "m.Id as movieId, m.TmdbUrl, m.Title, m.Overview, m.Tagline, m.Runtime, m.Budget, m.Revenue, " +
                    "m.BackdropUrl, m.PosterUrl, m.ImdbUrl, m.OriginalLanguage, m.ReleaseDate, mc.Character " +
                    "from [Cast] c inner join [MovieCast] mc on c.Id = mc.CastId " +
                    "inner join [Movie] m on m.Id = mc.MovieId " +
                    "where c.Id = @id";

                var castWithMovie = connection.Query<Cast, Movie, string, Cast>(cmd, (c, m, ch) => {
                    c.Movies.Add(m);
                    c.Characters.Add(ch);
                    return c;
                }, new { id = id }, splitOn: "movieId, Character");

                return castWithMovie;
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

        public Cast GetById(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                return connection.QueryFirstOrDefault<Cast>("select * from [Cast] where Id=@id", new { id = id });
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

        public int Insert(Cast item)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                return connection.Execute("insert into [Cast] values (@Name, @Gender, @TmdbUrl, @ProfilePath)", item);
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

        public int Update(Cast item)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                string cmd = "update [Cast] set Name=@name, Gender=@gender, " +
                    "TmdbUrl=@tmdbUrl, ProfilePath=@profilePath where Id=@id";
                return connection.Execute(cmd, 
                    new { id = item.Id, name = item.Name, 
                        gender=item.Gender, tmdbUrl=item.TmdbUrl, 
                        profilePath=item.ProfilePath});
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
        public async Task<int> InsertAsync(Cast item)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                var res = await connection.ExecuteAsync("insert into [Cast] values (@Name, @Gender, @TmdbUrl, @ProfilePath)", item);
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

        public async Task<int> UpdateAsync(Cast item)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                string cmd = "update [Cast] set Name=@name, Gender=@gender, " +
                    "TmdbUrl=@tmdbUrl, ProfilePath=@profilePath where Id=@id";
                var res = await connection.ExecuteAsync(cmd,
                    new
                    {
                        id = item.Id,
                        name = item.Name,
                        gender = item.Gender,
                        tmdbUrl = item.TmdbUrl,
                        profilePath = item.ProfilePath
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

        public async Task<int> DeleteAsync(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                var res = await connection.ExecuteAsync("Delete from [Cast] where Id=@id", new { id = id });
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

        public async Task<Cast> GetByIdAsync(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                var res = await connection.QueryFirstOrDefaultAsync<Cast>("select * from [Cast] where Id=@id", new { id = id });
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

        public async Task<IEnumerable<Cast>> GetAllAsync()
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                string cmd = "select * from (select top 10 * from [Cast] order by Id desc) sub order by Id asc";
                return await connection.QueryAsync<Cast>(cmd);
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

        public async Task<IEnumerable<Cast>> GetCastByIdWithMovieAsync(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                string cmd = "select c.Id, c.Name, c.Gender, c.TmdbUrl, c.ProfilePath, " +
                    "m.Id as movieId, m.TmdbUrl, m.Title, m.Overview, m.Tagline, m.Runtime, m.Budget, m.Revenue, " +
                    "m.BackdropUrl, m.PosterUrl, m.ImdbUrl, m.OriginalLanguage, m.ReleaseDate, mc.Character " +
                    "from [Cast] c inner join [MovieCast] mc on c.Id = mc.CastId " +
                    "inner join [Movie] m on m.Id = mc.MovieId " +
                    "where c.Id = @id";

                var castWithMovie = await connection.QueryAsync<Cast, Movie, string, Cast>(cmd, (c, m, ch) => {
                    c.Movies.Add(m);
                    c.Characters.Add(ch);
                    return c;
                }, new { id = id }, splitOn: "movieId, Character");

                return castWithMovie;
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
