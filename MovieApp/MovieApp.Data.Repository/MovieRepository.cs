using MovieApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace MovieApp.Data.Repository
{
    public class MovieRepository : IRepository<Movie>
    {
        private readonly DBHelper dbHelper;

        public MovieRepository()
        {
            dbHelper = new DBHelper();
        }

        // sync
        public int Delete(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                return connection.Execute("Delete from [Movie] where Id=@id", new { id = id });
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

        public IEnumerable<Movie> GetAll()
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                string cmd = "select * from (select top 10 * from [Movie] order by Id desc) sub order by Id asc";
                return connection.Query<Movie>(cmd);
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

        public IEnumerable<Movie> GetMovieByIdWithGenre(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                string cmd = "select m.Id, m.TmdbUrl, m.Title, m.Overview, m.Tagline, m.Runtime, m.Budget, m.Revenue, " +
                    "m.BackdropUrl, m.PosterUrl, m.ImdbUrl, m.OriginalLanguage, m.ReleaseDate, g.Id as genreId, g.Name " +
                    "from [Movie] m inner join [MovieGenre] mg on m.Id = mg.MovieId " +
                    "inner join [Genre] g on g.Id = mg.GenreId " +
                    "where m.Id = @id";

                var movieWithGenre = connection.Query<Movie, Genre, Movie>(cmd, (m, g) => {
                    m.Genres.Add(g);
                    return m;
                }, new { id = id }, splitOn: "genreId");

                return movieWithGenre;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Dispose();
            }

            return null;
            
        }


        public IEnumerable<Movie> GetMovieByIdWithCast(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                string cmd = "select m.Id, m.TmdbUrl, m.Title, m.Overview, m.Tagline, m.Runtime, m.Budget, m.Revenue, " +
                    "m.BackdropUrl, m.PosterUrl, m.ImdbUrl, m.OriginalLanguage, m.ReleaseDate, " +
                    "c.Id as castId, c.Name, c.Gender, c.TmdbUrl, c.ProfilePath, mc.Character " + 
                    "from [Cast] c inner join [MovieCast] mc on c.Id = mc.CastId " +
                    "inner join [Movie] m on m.Id = mc.MovieId " +
                    "where m.Id = @id";

                var movieWithCast = connection.Query<Movie, Cast, string, Movie>(cmd, (m, c, ch) => {
                    m.Casts.Add(c);
                    m.Characters.Add(ch);
                    return m;
                }, new { id = id }, splitOn: "castId, Character");

                return movieWithCast;
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


        public IEnumerable<Movie> GetMovieByIdWithUser(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                string cmd = "select m.Id, m.TmdbUrl, m.Title, m.Overview, " +
                    "m.Tagline, m.Runtime, m.Budget, m.Revenue, " +
                    "m.BackdropUrl, m.PosterUrl, m.ImdbUrl, m.OriginalLanguage, m.ReleaseDate, " +
                    "u.Id as userId, u.FirstName, u.LastName, u.DateOfBirth, u.Email, " +
                    "u.HashedPassword, u.Salt, u.PhoneNumber, u.TwoFactorEnabled, " +
                    "u.LockoutEndDate, u.LastLoginDateTime, u.IsLocked, u.AccessFailedCount, " +
                    "r.Rating, r.ReviewText " +
                    "from [User] u inner join [Review] r on u.Id = r.UserId " +
                    "inner join [Movie] m on m.Id = r.MovieId " +
                    "where m.Id = @id";

                var movieWithUser = connection.Query<Movie, User, decimal, string,  Movie>(cmd, (m, u, rating, txt) => {
                    m.Users.Add(u);
                    m.Ratings.Add(rating);
                    m.ReviewTexts.Add(txt);
                    return m;
                }, new { id = id }, splitOn: "userId, Rating, ReviewText");

                return movieWithUser;
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


        public Movie GetById(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                return connection.QueryFirstOrDefault<Movie>("select * from [Movie] where Id=@id", new { id = id });
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


        public int Insert(Movie item)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                int res = connection.Execute("insert into [Movie] values (@TmdbUrl, @Title, @Overview, @Tagline, @Runtime, @Budget, @Revenue, @BackdropUrl, @PosterUrl, @ImdbUrl, @OriginalLanguage, @ReleaseDate)", item);
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

        public int Update(Movie item)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                return connection.Execute("update [Movie] set" +
                    " TmdbUrl=@tmdbUrl, Title=@title, Overview=@overview, Tagline=@tagline, " +
                    " Runtime=@runtime, Budget=@budget, Revenue=@revenue, BackdropUrl=@backdropUrl, " +
                    " PosterUrl=@postUrl, ImdbUrl=@imdbUrl, OriginalLanguage=@originalLanguage, ReleaseDate=@releaseDate " +
                    " where Id=@id", 
                    new { id = item.Id, tmdbUrl = item.ImdbUrl, title = item.Title,  
                           overview = item.Overview, tagline = item.Tagline, runtime = item.Runtime,
                           budget = item.Budget, revenue = item.Revenue, backdropUrl = item.BackdropUrl,
                           posturl = item.PosterUrl, imdbUrl = item.ImdbUrl, originalLanguage = item.OriginalLanguage,
                           releaseDate = item.ReleaseDate
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
        public async Task<int> InsertAsync(Movie item)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                int res = await connection.ExecuteAsync("insert into [Movie] values (@TmdbUrl, @Title, @Overview, @Tagline, @Runtime, @Budget, @Revenue, @BackdropUrl, @PosterUrl, @ImdbUrl, @OriginalLanguage, @ReleaseDate)", item);
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

        public async Task<int> UpdateAsync(Movie item)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                var res = await connection.ExecuteAsync("update [Movie] set" +
                    " TmdbUrl=@tmdbUrl, Title=@title, Overview=@overview, Tagline=@tagline, " +
                    " Runtime=@runtime, Budget=@budget, Revenue=@revenue, BackdropUrl=@backdropUrl, " +
                    " PosterUrl=@postUrl, ImdbUrl=@imdbUrl, OriginalLanguage=@originalLanguage, ReleaseDate=@releaseDate " +
                    " where Id=@id",
                    new
                    {
                        id = item.Id,
                        tmdbUrl = item.ImdbUrl,
                        title = item.Title,
                        overview = item.Overview,
                        tagline = item.Tagline,
                        runtime = item.Runtime,
                        budget = item.Budget,
                        revenue = item.Revenue,
                        backdropUrl = item.BackdropUrl,
                        posturl = item.PosterUrl,
                        imdbUrl = item.ImdbUrl,
                        originalLanguage = item.OriginalLanguage,
                        releaseDate = item.ReleaseDate
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
                var res = await connection.ExecuteAsync("Delete from [Movie] where Id=@id", new { id = id });
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


        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                string cmd = "select * from (select top 10 * from [Movie] order by Id desc) sub order by Id asc";
                var res = await connection.QueryAsync<Movie>(cmd);
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

        public async Task<Movie> GetByIdAsync(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                var res = await connection.QueryFirstOrDefaultAsync<Movie>("select * from [Movie] where Id=@id", new { id = id });
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


        public async Task<IEnumerable<Movie>> GetMovieByIdWithGenreAsync(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                string cmd = "select m.Id, m.TmdbUrl, m.Title, m.Overview, m.Tagline, m.Runtime, m.Budget, m.Revenue, " +
                    "m.BackdropUrl, m.PosterUrl, m.ImdbUrl, m.OriginalLanguage, m.ReleaseDate, g.Id as genreId, g.Name " +
                    "from [Movie] m inner join [MovieGenre] mg on m.Id = mg.MovieId " +
                    "inner join [Genre] g on g.Id = mg.GenreId " +
                    "where m.Id = @id";

                var movieWithGenre = await connection.QueryAsync<Movie, Genre, Movie>(cmd, (m, g) => {
                    m.Genres.Add(g);
                    return m;
                }, new { id = id }, splitOn: "genreId");

                return movieWithGenre;
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


        public async Task<IEnumerable<Movie>> GetMovieByIdWithCastAsync(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                string cmd = "select m.Id, m.TmdbUrl, m.Title, m.Overview, m.Tagline, m.Runtime, m.Budget, m.Revenue, " +
                    "m.BackdropUrl, m.PosterUrl, m.ImdbUrl, m.OriginalLanguage, m.ReleaseDate, " +
                    "c.Id as castId, c.Name, c.Gender, c.TmdbUrl, c.ProfilePath, mc.Character " +
                    "from [Cast] c inner join [MovieCast] mc on c.Id = mc.CastId " +
                    "inner join [Movie] m on m.Id = mc.MovieId " +
                    "where m.Id = @id";

                var movieWithCast = await connection.QueryAsync<Movie, Cast, string, Movie>(cmd, (m, c, ch) => {
                    m.Casts.Add(c);
                    m.Characters.Add(ch);
                    return m;
                }, new { id = id }, splitOn: "castId, Character");

                return movieWithCast;
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


        public async Task<IEnumerable<Movie>> GetMovieByIdWithUserAsync(int id)
        {
            SqlConnection connection = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                string cmd = "select m.Id, m.TmdbUrl, m.Title, m.Overview, " +
                    "m.Tagline, m.Runtime, m.Budget, m.Revenue, " +
                    "m.BackdropUrl, m.PosterUrl, m.ImdbUrl, m.OriginalLanguage, m.ReleaseDate, " +
                    "u.Id as userId, u.FirstName, u.LastName, u.DateOfBirth, u.Email, " +
                    "u.HashedPassword, u.Salt, u.PhoneNumber, u.TwoFactorEnabled, " +
                    "u.LockoutEndDate, u.LastLoginDateTime, u.IsLocked, u.AccessFailedCount, " +
                    "r.Rating, r.ReviewText " +
                    "from [User] u inner join [Review] r on u.Id = r.UserId " +
                    "inner join [Movie] m on m.Id = r.MovieId " +
                    "where m.Id = @id";

                var movieWithUser = await connection.QueryAsync<Movie, User, decimal, string, Movie>(cmd, (m, u, rating, txt) => {
                    m.Users.Add(u);
                    m.Ratings.Add(rating);
                    m.ReviewTexts.Add(txt);
                    return m;
                }, new { id = id }, splitOn: "userId, Rating, ReviewText");

                return movieWithUser;
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
