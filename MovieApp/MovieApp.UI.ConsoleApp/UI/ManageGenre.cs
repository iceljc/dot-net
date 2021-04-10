using MovieApp.Data.Models;
using MovieApp.Services;
using MovieApp.UI.ConsoleApp.Utility.Menus;
using MovieApp.UI.ConsoleApp.Utility.Menus.MenuOptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;

namespace MovieApp.UI.ConsoleApp.UI
{
    public class ManageGenre
    {
        private readonly GenreService genreService;
        public ManageGenre()
        {
            genreService = new GenreService();
        }

        #region sync
        void AddGenre()
        {
            try
            {
                Genre g = new Genre();

                Console.Write("Enter Name => ");
                g.Name = Console.ReadLine();

                int res = genreService.AddGenre(g);

                if (res > 0)
                {
                    Console.WriteLine("Genre added successfully");
                }
                else
                {
                    Console.WriteLine("Some error occurs");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        void UpdateGenre()
        {
            try
            {
                Genre g = new Genre();
                Console.Write("Enter Id => ");
                g.Id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Name => ");
                g.Name = Console.ReadLine();

                int res = genreService.UpdateGenre(g);

                if (res > 0)
                {
                    Console.WriteLine("Genre update successfully");
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

        void DeleteGenre()
        {
            try
            {
                Console.Write("Enter Id => ");
                int id = Convert.ToInt32(Console.ReadLine());
                int res = genreService.DeleteGenre(id);

                if (res > 0)
                {
                    Console.WriteLine("Genre delete successfully");
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
                IEnumerable<Genre> genreCollection = genreService.GetAllGenre();
                if (genreCollection != null)
                {
                    foreach (Genre item in genreCollection)
                    {
                        Console.WriteLine(item.Id + " \t " + item.Name);
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

        void GetGenreByIdWithMovie()
        {
            try
            {
                Console.Write("Enter Id => ");
                int id = Convert.ToInt32(Console.ReadLine());
                IEnumerable<Genre> genres = genreService.GetGenreByIdWithMovie(id);

                if (genres != null)
                {
                    var result = genres.GroupBy(g => g.Id).Select(group =>
                    {
                        var groupedGenre = group.First();
                        groupedGenre.Movies = group.Select(p => p.Movies.Single()).ToList();
                        return groupedGenre;
                    });

                    bool any = false;
                    foreach (var genre in result)
                    {
                        any = true;
                        Console.WriteLine("Genre: " + genre.Name + "\n");
                        Console.WriteLine("Related Movies: ");
                        foreach (var movie in genre.Movies)
                        {
                            Console.WriteLine(movie.Title);
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
        #endregion



        #region async
        async Task AddGenreAsync()
        {
            try
            {
                Genre g = new Genre();

                Console.Write("Enter Name => ");
                g.Name = Console.ReadLine();

                int res = await genreService.AddGenreAsync(g);

                if (res > 0)
                {
                    Console.WriteLine("Genre added successfully");
                }
                else
                {
                    Console.WriteLine("Some error occurs");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        async Task UpdateGenreAsync()
        {
            try
            {
                Genre g = new Genre();
                Console.Write("Enter Id => ");
                g.Id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Name => ");
                g.Name = Console.ReadLine();

                int res = await genreService.UpdateGenreAsync(g);

                if (res > 0)
                {
                    Console.WriteLine("Genre update successfully");
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

        async Task DeleteGenreAsync()
        {
            try
            {
                Console.Write("Enter Id => ");
                int id = Convert.ToInt32(Console.ReadLine());
                int res = await genreService.DeleteGenreAsync(id);

                if (res > 0)
                {
                    Console.WriteLine("Genre delete successfully");
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
                IEnumerable<Genre> genreCollection = await genreService.GetAllGenreAsync();
                if (genreCollection != null)
                {
                    foreach (Genre item in genreCollection)
                    {
                        Console.WriteLine(item.Id + " \t " + item.Name);
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

        async Task GetGenreByIdWithMovieAsync()
        {
            try
            {
                Console.Write("Enter Id => ");
                int id = Convert.ToInt32(Console.ReadLine());
                IEnumerable<Genre> genres = await genreService.GetGenreByIdWithMovieAsync(id);

                if (genres != null)
                {
                    var result = genres.GroupBy(g => g.Id).Select(group =>
                    {
                        var groupedGenre = group.First();
                        groupedGenre.Movies = group.Select(p => p.Movies.Single()).ToList();
                        return groupedGenre;
                    });

                    bool any = false;
                    foreach (var genre in result)
                    {
                        any = true;
                        Console.WriteLine("Genre: " + genre.Name + "\n");
                        Console.WriteLine("Related Movies: ");
                        foreach (var movie in genre.Movies)
                        {
                            Console.WriteLine(movie.Title);
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

        #endregion






        public async void Run()
        {
            int choice = 0;

            do
            {
                IMenu genreMenu = new GenreMenu();
                choice = genreMenu.PrintMenu();
                switch (choice)
                {
                    case (int)GenreMenuOptions.Insert:
                        AddGenreAsync().Wait();
                        break;
                    case (int)GenreMenuOptions.Delete:
                        DeleteGenreAsync().Wait();
                        break;
                    case (int)GenreMenuOptions.Print:
                        PrintAllAsync().Wait();
                        break;
                    case (int)GenreMenuOptions.Update:
                        UpdateGenreAsync().Wait();
                        break;
                    case (int)GenreMenuOptions.GetGenreWithMovie:
                        GetGenreByIdWithMovieAsync().Wait();
                        break;
                    case (int)GenreMenuOptions.Exit:
                        Console.WriteLine("Thanks for your visit. Please visit the Genre System again !!!!");
                        break;

                    default:
                        Console.WriteLine("Invalid Option");
                        break;

                }

                if (choice != (int)GenreMenuOptions.Exit)
                {
                    //Console.WriteLine("genre " + choice);
                    Console.WriteLine("Press Enter to continue.......");
                    Console.ReadLine();
                    Console.Clear();
                }
            } while (choice != (int) GenreMenuOptions.Exit);
        }

        /*
         * public void Run()
        {
            int choice = 0;

            do
            {
                IMenu genreMenu = new GenreMenu();
                choice = genreMenu.PrintMenu();
                switch (choice)
                {
                    case (int)GenreMenuOptions.Insert:
                        AddGenre();
                        break;
                    case (int)GenreMenuOptions.Delete:
                        DeleteGenre();
                        break;
                    case (int)GenreMenuOptions.Print:
                        PrintAll();
                        break;
                    case (int)GenreMenuOptions.Update:
                        UpdateGenre();
                        break;
                    case (int)GenreMenuOptions.GetGenreWithMovie:
                        GetGenreByIdWithMovie();
                        break;
                    case (int)GenreMenuOptions.Exit:
                        Console.WriteLine("Thanks for your visit. Please visit the Genre System again !!!!");
                        break;

                    default:
                        Console.WriteLine("Invalid Option");
                        break;

                }

                if (choice != (int)GenreMenuOptions.Exit)
                {
                    Console.WriteLine("genre " + choice);
                    Console.WriteLine("Press Enter to continue.......genre");
                    Console.ReadLine();
                    Console.Clear();
                }
            } while (choice != (int)GenreMenuOptions.Exit);
        }
         */

    }
}
