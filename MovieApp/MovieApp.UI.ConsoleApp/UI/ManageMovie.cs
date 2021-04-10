using MovieApp.Data.Models;
using MovieApp.Services;
using MovieApp.UI.ConsoleApp.Utility.Menus;
using MovieApp.UI.ConsoleApp.Utility.Menus.MenuOptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.UI.ConsoleApp.UI
{
    
    public class ManageMovie
    {
        private readonly MovieService movieService;
        public ManageMovie()
        {
            movieService = new MovieService();
        }

        #region sync
        void AddMovie()
        {
            try
            {
                Movie m = new Movie();

                Console.Write("Enter Title => ");
                m.Title = Console.ReadLine();

                Console.Write("Enter Overview => ");
                m.Overview = Console.ReadLine();

                Console.Write("Enter Tagline => ");
                m.Tagline = Console.ReadLine();

                Console.Write("Enter Runtime (int) => ");
                m.Runtime = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Budget (decimal) => ");
                m.Budget = Convert.ToDecimal(Console.ReadLine());

                Console.Write("Enter Revenue (decimal) => ");
                m.Revenue = Convert.ToDecimal(Console.ReadLine());

                Console.Write("Enter TmdbUrl => ");
                m.TmdbUrl = Console.ReadLine();

                Console.Write("Enter BackdropUrl => ");
                m.BackdropUrl = Console.ReadLine();

                Console.Write("Enter PosterUrl => ");
                m.PosterUrl = Console.ReadLine();

                Console.Write("Enter ImdbUrl => ");
                m.ImdbUrl = Console.ReadLine();

                Console.Write("Enter Original Language => ");
                m.OriginalLanguage = Console.ReadLine();

                Console.Write("Enter ReleaseDate (yyyy-mm-dd) => ");
                m.ReleaseDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);

                int res = movieService.AddMovie(m);

                if (res > 0)
                {
                    Console.WriteLine("Movie added successfully");
                }
                else
                {
                    Console.WriteLine("Some error occurs");
                }
                
            }
            catch (FormatException fe)
            {
                Console.WriteLine("Format not accepted. " + fe.Message);
            }
            catch (OverflowException oe)
            {
                Console.WriteLine("value must be in between 1 to " + int.MaxValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine("some error has been occured. Contact the admin department");
            }
        }


        void DeleteMovie() 
        {
            try
            {
                Console.Write("Enter Id => ");
                int id = Convert.ToInt32(Console.ReadLine());

                int res = movieService.DeleteMovie(id);

                if (res > 0)
                {
                    Console.WriteLine("Movie delete successfully");
                }
                else
                {
                    Console.WriteLine("Some error occurs. Data not found.");
                }
            }
            catch (FormatException fe)
            {
                Console.WriteLine("Only numbers are allowed");
            }
            catch (OverflowException oe)
            {
                Console.WriteLine("value must be in between 1 to " + int.MaxValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine("some error has been occured. Contact the admin department");
            }

        }


        void PrintAll()
        {
            try
            {
                IEnumerable<Movie> movieCollection = movieService.GetAllMovie();
                if (movieCollection != null)
                {
                    string format = "yyyy-MM-dd";
                    foreach (Movie item in movieCollection)
                    {
                        Console.WriteLine(item.Id + " \t " + item.Title + " \t " + item.ReleaseDate.ToString(format));
                    }
                }
                else
                {
                    Console.WriteLine("No data");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        void UpdateMovie()
        {
            try
            {
                Movie m = new Movie();

                Console.Write("Enter Id => ");
                m.Id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Title => ");
                m.Title = Console.ReadLine();

                Console.Write("Enter Overview => ");
                m.Overview = Console.ReadLine();

                Console.Write("Enter Tagline => ");
                m.Tagline = Console.ReadLine();

                Console.Write("Enter Runtime (int) => ");
                m.Runtime = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Budget (decimal) => ");
                m.Budget = Convert.ToDecimal(Console.ReadLine());

                Console.Write("Enter Revenue (decimal) => ");
                m.Revenue = Convert.ToDecimal(Console.ReadLine());

                Console.Write("Enter TmdbUrl => ");
                m.TmdbUrl = Console.ReadLine();

                Console.Write("Enter BackdropUrl => ");
                m.BackdropUrl = Console.ReadLine();

                Console.Write("Enter PosterUrl => ");
                m.PosterUrl = Console.ReadLine();

                Console.Write("Enter ImdbUrl => ");
                m.ImdbUrl = Console.ReadLine();

                Console.Write("Enter Original Language => ");
                m.OriginalLanguage = Console.ReadLine();

                Console.Write("Enter ReleaseDate (yyyy-mm-dd) => ");
                m.ReleaseDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);

                int res = movieService.UpdateMovie(m);

                if (res > 0)
                {
                    Console.WriteLine("Movie update successfully");
                }
                else
                {
                    Console.WriteLine("Some error occurs. Data not found.");
                }

            }
            catch (FormatException fe)
            {
                Console.WriteLine("Format not accepted. " + fe.Message);
            }
            catch (OverflowException oe)
            {
                Console.WriteLine("value must be in between 1 to " + int.MaxValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine("some error has been occured. Contact the admin department");
            }
        }


        void GetMovieByIdWithGenre()
        {
            try
            {
                Console.Write("Enter Id => ");
                int id = Convert.ToInt32(Console.ReadLine());
                IEnumerable<Movie> movies = movieService.GetMovieByIdWithGenre(id);

                if (movies != null)
                {
                    var result = movies.GroupBy(m => m.Id).Select(group =>
                    {
                        var groupedMovie = group.First();
                        groupedMovie.Genres = group.Select(p => p.Genres.Single()).ToList();
                        return groupedMovie;
                    });

                    bool any = false;
                    foreach (var movie in result)
                    {
                        any = true;
                        Console.WriteLine("Movie: " + movie.Title + "\n");
                        Console.WriteLine("Related Genres: ");
                        foreach (var genre in movie.Genres)
                        {
                            Console.WriteLine(genre.Name);
                        }
                        Console.WriteLine();
                    }

                    if (!any)
                    {
                        Console.WriteLine("No data.");
                    }
                }
                else
                {
                    Console.WriteLine("Some error occurs.");
                }
            }
            catch (FormatException fe)
            {
                Console.WriteLine("Only numbers are allowed");
            }
            catch (OverflowException oe)
            {
                Console.WriteLine("value must be in between 1 to " + int.MaxValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine("some error has been occured. Contact the admin department");
            }
        }

        void GetMovieByIdWithCast()
        {
            try
            {
                Console.Write("Enter Id => ");
                int id = Convert.ToInt32(Console.ReadLine());
                IEnumerable<Movie> movies = movieService.GetMovieByIdWithCast(id);

                if (movies != null)
                {
                    var result = movies.GroupBy(m => m.Id).Select(group =>
                    {
                        var groupedMovie = group.First();
                        groupedMovie.Casts = group.Select(p => p.Casts.Single()).ToList();
                        groupedMovie.Characters = group.Select(p => p.Characters.Single()).ToList();
                        return groupedMovie;
                    });

                    bool any = false;
                    foreach (var movie in result)
                    {
                        any = true;
                        Console.WriteLine("Movie: " + movie.Title + "\n");
                        Console.WriteLine("Related Casts and Characters: ");

                        if (movie.Casts.Count == movie.Characters.Count)
                        {
                            int length = movie.Casts.Count;
                            for (int i = 0; i < length; i++)
                            {
                                Console.WriteLine(movie.Casts[i].Name + " \t\t\t " + movie.Characters[i]);
                            }
                        }
                        Console.WriteLine();
                    }
                    if (!any)
                    {
                        Console.WriteLine("No data.");
                    }
                }
                else
                {
                    Console.WriteLine("Some error occurs.");
                }
            }
            catch (FormatException fe)
            {
                Console.WriteLine("Only numbers are allowed");
            }
            catch (OverflowException oe)
            {
                Console.WriteLine("value must be in between 1 to " + int.MaxValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine("some error has been occured. Contact the admin department");
            }
        }

        void GetMovieByIdWithUser()
        {
            try
            {
                Console.Write("Enter Id => ");
                int id = Convert.ToInt32(Console.ReadLine());
                IEnumerable<Movie> movies = movieService.GetMovieByIdWithUser(id);

                if (movies != null)
                {
                    var result = movies.GroupBy(m => m.Id).Select(group =>
                    {
                        var groupedMovie = group.First();
                        groupedMovie.Users = group.Select(p => p.Users.Single()).ToList();
                        groupedMovie.Ratings = group.Select(p => p.Ratings.Single()).ToList();
                        groupedMovie.ReviewTexts = group.Select(p => p.ReviewTexts.Single()).ToList();
                        return groupedMovie;
                    });

                    bool any = false;
                    foreach (var movie in result)
                    {
                        any = true;
                        Console.WriteLine("Movie: " + movie.Title + "\n");
                        Console.WriteLine("Related Users and Reviews: ");

                        if (movie.Users.Count == movie.Ratings.Count && movie.Users.Count == movie.ReviewTexts.Count)
                        {
                            int length = movie.Users.Count;
                            for (int i = 0; i < length; i++)
                            {
                                Console.WriteLine(movie.Users[i].FirstName + " " + movie.Users[i].LastName + " => \t\t\t "
                                    + "Rating: " + movie.Ratings[i] + ", Review: " + movie.ReviewTexts[i]);
                                Console.WriteLine("------------------------------");
                            }
                        }
                        Console.WriteLine();
                    }

                    if (!any)
                    {
                        Console.WriteLine("No data.");
                    }
                }
            }
            catch (FormatException fe)
            {
                Console.WriteLine("Only numbers are allowed");
            }
            catch (OverflowException oe)
            {
                Console.WriteLine("value must be in between 1 to " + int.MaxValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine("some error has been occured. Contact the admin department");
            }
            

        }
        #endregion


        #region async
        async Task AddMovieAsync()
        {
            try
            {
                Movie m = new Movie();

                Console.Write("Enter Title => ");
                m.Title = Console.ReadLine();

                Console.Write("Enter Overview => ");
                m.Overview = Console.ReadLine();

                Console.Write("Enter Tagline => ");
                m.Tagline = Console.ReadLine();

                Console.Write("Enter Runtime (int) => ");
                m.Runtime = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Budget (decimal) => ");
                m.Budget = Convert.ToDecimal(Console.ReadLine());

                Console.Write("Enter Revenue (decimal) => ");
                m.Revenue = Convert.ToDecimal(Console.ReadLine());

                Console.Write("Enter TmdbUrl => ");
                m.TmdbUrl = Console.ReadLine();

                Console.Write("Enter BackdropUrl => ");
                m.BackdropUrl = Console.ReadLine();

                Console.Write("Enter PosterUrl => ");
                m.PosterUrl = Console.ReadLine();

                Console.Write("Enter ImdbUrl => ");
                m.ImdbUrl = Console.ReadLine();

                Console.Write("Enter Original Language => ");
                m.OriginalLanguage = Console.ReadLine();

                Console.Write("Enter ReleaseDate (yyyy-mm-dd) => ");
                m.ReleaseDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);

                int res = await movieService.AddMovieAsync(m);

                if (res > 0)
                {
                    Console.WriteLine("Movie added successfully");
                }
                else
                {
                    Console.WriteLine("Some error occurs");
                }

            }
            catch (FormatException fe)
            {
                Console.WriteLine("Format not accepted. " + fe.Message);
            }
            catch (OverflowException oe)
            {
                Console.WriteLine("value must be in between 1 to " + int.MaxValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine("some error has been occured. Contact the admin department");
            }
        }

        async Task DeleteMovieAsync()
        {
            try
            {
                Console.Write("Enter Id => ");
                int id = Convert.ToInt32(Console.ReadLine());

                int res = await movieService.DeleteMovieAsync(id);

                if (res > 0)
                {
                    Console.WriteLine("Movie delete successfully");
                }
                else
                {
                    Console.WriteLine("Some error occurs. Data not found.");
                }
            }
            catch (FormatException fe)
            {
                Console.WriteLine("Only numbers are allowed");
            }
            catch (OverflowException oe)
            {
                Console.WriteLine("value must be in between 1 to " + int.MaxValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine("some error has been occured. Contact the admin department");
            }

        }

        async Task PrintAllAsync()
        {
            try
            {
                IEnumerable<Movie> movieCollection = await movieService.GetAllMovieAsync();
                if (movieCollection != null)
                {
                    string format = "yyyy-MM-dd";
                    foreach (Movie item in movieCollection)
                    {
                        Console.WriteLine(item.Id + " \t " + item.Title + " \t " + item.ReleaseDate.ToString(format));
                    }
                }
                else
                {
                    Console.WriteLine("No data");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task UpdateMovieAsync()
        {
            try
            {
                Movie m = new Movie();

                Console.Write("Enter Id => ");
                m.Id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Title => ");
                m.Title = Console.ReadLine();

                Console.Write("Enter Overview => ");
                m.Overview = Console.ReadLine();

                Console.Write("Enter Tagline => ");
                m.Tagline = Console.ReadLine();

                Console.Write("Enter Runtime (int) => ");
                m.Runtime = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Budget (decimal) => ");
                m.Budget = Convert.ToDecimal(Console.ReadLine());

                Console.Write("Enter Revenue (decimal) => ");
                m.Revenue = Convert.ToDecimal(Console.ReadLine());

                Console.Write("Enter TmdbUrl => ");
                m.TmdbUrl = Console.ReadLine();

                Console.Write("Enter BackdropUrl => ");
                m.BackdropUrl = Console.ReadLine();

                Console.Write("Enter PosterUrl => ");
                m.PosterUrl = Console.ReadLine();

                Console.Write("Enter ImdbUrl => ");
                m.ImdbUrl = Console.ReadLine();

                Console.Write("Enter Original Language => ");
                m.OriginalLanguage = Console.ReadLine();

                Console.Write("Enter ReleaseDate (yyyy-mm-dd) => ");
                m.ReleaseDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);

                int res = await movieService.UpdateMovieAsync(m);

                if (res > 0)
                {
                    Console.WriteLine("Movie update successfully");
                }
                else
                {
                    Console.WriteLine("Some error occurs. Data not found.");
                }

            }
            catch (FormatException fe)
            {
                Console.WriteLine("Format not accepted. " + fe.Message);
            }
            catch (OverflowException oe)
            {
                Console.WriteLine("value must be in between 1 to " + int.MaxValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine("some error has been occured. Contact the admin department");
            }
        }

        async Task GetMovieByIdWithGenreAsync()
        {
            try
            {
                Console.Write("Enter Id => ");
                int id = Convert.ToInt32(Console.ReadLine());
                IEnumerable<Movie> movies = await movieService.GetMovieByIdWithGenreAsync(id);

                if (movies != null)
                {
                    var result = movies.GroupBy(m => m.Id).Select(group =>
                    {
                        var groupedMovie = group.First();
                        groupedMovie.Genres = group.Select(p => p.Genres.Single()).ToList();
                        return groupedMovie;
                    });

                    bool any = false;
                    foreach (var movie in result)
                    {
                        any = true;
                        Console.WriteLine("Movie: " + movie.Title + "\n");
                        Console.WriteLine("Related Genres: ");
                        foreach (var genre in movie.Genres)
                        {
                            Console.WriteLine(genre.Name);
                        }
                        Console.WriteLine();
                    }

                    if (!any)
                    {
                        Console.WriteLine("No data.");
                    }
                }
                else
                {
                    Console.WriteLine("Some error occurs.");
                }
            }
            catch (FormatException fe)
            {
                Console.WriteLine("Only numbers are allowed");
            }
            catch (OverflowException oe)
            {
                Console.WriteLine("value must be in between 1 to " + int.MaxValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine("some error has been occured. Contact the admin department");
            }
        }

        async Task GetMovieByIdWithCastAsync()
        {
            try
            {
                Console.Write("Enter Id => ");
                int id = Convert.ToInt32(Console.ReadLine());
                IEnumerable<Movie> movies = await movieService.GetMovieByIdWithCastAsync(id);

                if (movies != null)
                {
                    var result = movies.GroupBy(m => m.Id).Select(group =>
                    {
                        var groupedMovie = group.First();
                        groupedMovie.Casts = group.Select(p => p.Casts.Single()).ToList();
                        groupedMovie.Characters = group.Select(p => p.Characters.Single()).ToList();
                        return groupedMovie;
                    });

                    bool any = false;
                    foreach (var movie in result)
                    {
                        any = true;
                        Console.WriteLine("Movie: " + movie.Title + "\n");
                        Console.WriteLine("Related Casts and Characters: ");

                        if (movie.Casts.Count == movie.Characters.Count)
                        {
                            int length = movie.Casts.Count;
                            for (int i = 0; i < length; i++)
                            {
                                Console.WriteLine(movie.Casts[i].Name + " \t\t\t " + movie.Characters[i]);
                            }
                        }
                        Console.WriteLine();
                    }
                    if (!any)
                    {
                        Console.WriteLine("No data.");
                    }
                }
                else
                {
                    Console.WriteLine("Some error occurs.");
                }
            }
            catch (FormatException fe)
            {
                Console.WriteLine("Only numbers are allowed");
            }
            catch (OverflowException oe)
            {
                Console.WriteLine("value must be in between 1 to " + int.MaxValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine("some error has been occured. Contact the admin department");
            }
        }

        async Task GetMovieByIdWithUserAsync()
        {
            try
            {
                Console.Write("Enter Id => ");
                int id = Convert.ToInt32(Console.ReadLine());
                IEnumerable<Movie> movies = await movieService.GetMovieByIdWithUserAsync(id);

                if (movies != null)
                {
                    var result = movies.GroupBy(m => m.Id).Select(group =>
                    {
                        var groupedMovie = group.First();
                        groupedMovie.Users = group.Select(p => p.Users.Single()).ToList();
                        groupedMovie.Ratings = group.Select(p => p.Ratings.Single()).ToList();
                        groupedMovie.ReviewTexts = group.Select(p => p.ReviewTexts.Single()).ToList();
                        return groupedMovie;
                    });

                    bool any = false;
                    foreach (var movie in result)
                    {
                        any = true;
                        Console.WriteLine("Movie: " + movie.Title + "\n");
                        Console.WriteLine("Related Users and Reviews: ");

                        if (movie.Users.Count == movie.Ratings.Count && movie.Users.Count == movie.ReviewTexts.Count)
                        {
                            int length = movie.Users.Count;
                            for (int i = 0; i < length; i++)
                            {
                                Console.WriteLine(movie.Users[i].FirstName + " " + movie.Users[i].LastName + " => \t\t\t "
                                    + "Rating: " + movie.Ratings[i] + ", Review: " + movie.ReviewTexts[i]);
                                Console.WriteLine("------------------------------");
                            }
                        }
                        Console.WriteLine();
                    }

                    if (!any)
                    {
                        Console.WriteLine("No data.");
                    }
                }
            }
            catch (FormatException fe)
            {
                Console.WriteLine("Only numbers are allowed");
            }
            catch (OverflowException oe)
            {
                Console.WriteLine("value must be in between 1 to " + int.MaxValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine("some error has been occured. Contact the admin department");
            }


        }
        #endregion


        public async void Run()
        {
            int choice = 0;

            do
            {

                IMenu movieMenu = new MovieMenu();
                choice = movieMenu.PrintMenu();
                switch (choice)
                {
                    case (int)MovieMenuOptions.Insert:
                        //await AddMovieAsync();
                        AddMovieAsync().Wait();
                        break;
                    case (int)MovieMenuOptions.Delete:
                        //await DeleteMovieAsync();
                        DeleteMovieAsync().Wait();
                        break;
                    case (int)MovieMenuOptions.Print:
                        //await PrintAllAsync();
                        PrintAllAsync().Wait();
                        break;
                    case (int)MovieMenuOptions.Update:
                        //await UpdateMovieAsync();
                        UpdateMovieAsync().Wait();
                        break;
                    case (int)MovieMenuOptions.GetMovieWithGenre:
                        //await GetMovieByIdWithGenreAsync();
                        GetMovieByIdWithGenreAsync().Wait();
                        break;
                    case (int)MovieMenuOptions.GetMovieWithCast:
                        //await GetMovieByIdWithCastAsync();
                        GetMovieByIdWithCastAsync().Wait();
                        break;
                    case (int)MovieMenuOptions.GetMovieWithUser:
                        //await GetMovieByIdWithUserAsync();
                        GetMovieByIdWithUserAsync().Wait();
                        break;
                    case (int)MovieMenuOptions.Exit:
                        Console.WriteLine("Thanks for your visit. Please visit the Movie System again !!!!");
                        break;

                    default:
                        Console.WriteLine("Invalid Option");
                        break;

                }

                if (choice != (int)MovieMenuOptions.Exit)
                {
                    Console.WriteLine("Press Enter to continue.......");
                    Console.ReadLine();
                    Console.Clear();
                }

            } while (choice != (int)MovieMenuOptions.Exit);
        }

    }

    

}
