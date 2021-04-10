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
    public class ManageCast
    {
        private readonly CastService castService;
        public ManageCast()
        {
            castService = new CastService();
        }

        #region sync
        void AddCast()
        {
            try
            {
                Cast c = new Cast();

                Console.Write("Enter Name => ");
                c.Name = Console.ReadLine();

                Console.Write("Enter Gender => ");
                c.Gender = Console.ReadLine();

                Console.Write("Enter TmdbUrl => ");
                c.TmdbUrl = Console.ReadLine();

                Console.Write("Enter ProfilePath => ");
                c.ProfilePath = Console.ReadLine();

                int res = castService.AddCast(c);

                if (res > 0)
                {
                    Console.WriteLine("Cast added successfully");
                }
                else
                {
                    Console.WriteLine("Some error occurs");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("some error has been occured. Contact the admin department");
            }

        }

        void DeleteCast()
        {
            try
            {
                Console.Write("Enter Id => ");
                int id = Convert.ToInt32(Console.ReadLine());
                int res = castService.DeleteCast(id);

                if (res > 0)
                {
                    Console.WriteLine("Cast delete successfully");
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
                IEnumerable<Cast> castCollection = castService.GetAllCast();
                if (castCollection != null)
                {
                    foreach (Cast item in castCollection)
                    {
                        Console.WriteLine(item.Id + " \t " + item.Name + " \t " + item.Gender);
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

        void UpdateCast()
        {

            try
            {
                Cast c = new Cast();

                Console.Write("Enter Id => ");
                c.Id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Name => ");
                c.Name = Console.ReadLine();

                Console.Write("Enter Gender => ");
                c.Gender = Console.ReadLine();

                Console.Write("Enter TmdbUrl => ");
                c.TmdbUrl = Console.ReadLine();

                Console.Write("Enter ProfilePath => ");
                c.ProfilePath = Console.ReadLine();

                int res = castService.UpdateCast(c);

                if (res > 0)
                {
                    Console.WriteLine("Cast update successfully");
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

        void GetCastByIdWithMovie()
        {

            try
            {
                Console.Write("Enter Id => ");
                int id = Convert.ToInt32(Console.ReadLine());
                IEnumerable<Cast> casts = castService.GetCastByIdWithMovie(id);

                if (casts != null)
                {
                    var result = casts.GroupBy(c => c.Id).Select(group =>
                    {
                        var groupedCast = group.First();
                        groupedCast.Movies = group.Select(p => p.Movies.Single()).ToList();
                        groupedCast.Characters = group.Select(p => p.Characters.Single()).ToList();
                        return groupedCast;
                    });

                    bool any = false;

                    foreach (var cast in result)
                    {
                        any = true;
                        Console.WriteLine("Cast: " + cast.Name + "\n");
                        Console.WriteLine("Related Movies and Characters: ");

                        if (cast.Movies.Count == cast.Characters.Count)
                        {
                            int length = cast.Movies.Count;

                            for (int i = 0; i < length; i++)
                            {
                                Console.WriteLine(cast.Movies[i].Title + " \t\t\t " + cast.Characters[i]);
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

        #endregion




        #region async
        async Task AddCastAsync()
        {
            try
            {
                Cast c = new Cast();

                Console.Write("Enter Name => ");
                c.Name = Console.ReadLine();

                Console.Write("Enter Gender => ");
                c.Gender = Console.ReadLine();

                Console.Write("Enter TmdbUrl => ");
                c.TmdbUrl = Console.ReadLine();

                Console.Write("Enter ProfilePath => ");
                c.ProfilePath = Console.ReadLine();

                int res = await castService.AddCastAsync(c);

                if (res > 0)
                {
                    Console.WriteLine("Cast added successfully");
                }
                else
                {
                    Console.WriteLine("Some error occurs");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("some error has been occured. Contact the admin department");
            }

        }

        async Task DeleteCastAsync()
        {
            try
            {
                Console.Write("Enter Id => ");
                int id = Convert.ToInt32(Console.ReadLine());
                int res = await castService.DeleteCastAsync(id);

                if (res > 0)
                {
                    Console.WriteLine("Cast delete successfully");
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
                IEnumerable<Cast> castCollection = await castService.GetAllCastAsync();
                if (castCollection != null)
                {
                    foreach (Cast item in castCollection)
                    {
                        Console.WriteLine(item.Id + " \t " + item.Name + " \t " + item.Gender);
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

        async Task UpdateCastAsync()
        {

            try
            {
                Cast c = new Cast();

                Console.Write("Enter Id => ");
                c.Id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Name => ");
                c.Name = Console.ReadLine();

                Console.Write("Enter Gender => ");
                c.Gender = Console.ReadLine();

                Console.Write("Enter TmdbUrl => ");
                c.TmdbUrl = Console.ReadLine();

                Console.Write("Enter ProfilePath => ");
                c.ProfilePath = Console.ReadLine();

                int res = await castService.UpdateCastAsync(c);

                if (res > 0)
                {
                    Console.WriteLine("Cast update successfully");
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

        async Task GetCastByIdWithMovieAsync()
        {

            try
            {
                Console.Write("Enter Id => ");
                int id = Convert.ToInt32(Console.ReadLine());
                IEnumerable<Cast> casts = await castService.GetCastByIdWithMovieAsync(id);

                if (casts != null)
                {
                    var result = casts.GroupBy(c => c.Id).Select(group =>
                    {
                        var groupedCast = group.First();
                        groupedCast.Movies = group.Select(p => p.Movies.Single()).ToList();
                        groupedCast.Characters = group.Select(p => p.Characters.Single()).ToList();
                        return groupedCast;
                    });

                    bool any = false;

                    foreach (var cast in result)
                    {
                        any = true;
                        Console.WriteLine("Cast: " + cast.Name + "\n");
                        Console.WriteLine("Related Movies and Characters: ");

                        if (cast.Movies.Count == cast.Characters.Count)
                        {
                            int length = cast.Movies.Count;

                            for (int i = 0; i < length; i++)
                            {
                                Console.WriteLine(cast.Movies[i].Title + " \t\t\t " + cast.Characters[i]);
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
        #endregion


        public async void Run()
        {
            int choice = 0;

            do
            {
                IMenu castMenu = new CastMenu();
                choice = castMenu.PrintMenu();
                switch (choice)
                {
                    case (int)CastMenuOptions.Insert:
                        //await AddCastAsync();
                        AddCastAsync().Wait();
                        break;
                    case (int)CastMenuOptions.Delete:
                        //await DeleteCastAsync();
                        DeleteCastAsync().Wait();
                        break;
                    case (int)CastMenuOptions.Print:
                        //await PrintAllAsync();
                        PrintAllAsync().Wait();
                        break;
                    case (int)CastMenuOptions.Update:
                        //await UpdateCastAsync();
                        UpdateCastAsync().Wait();
                        break;
                    case (int)CastMenuOptions.GetCastWithMovie:
                        //await GetCastByIdWithMovieAsync();
                        GetCastByIdWithMovieAsync().Wait();
                        break;
                    case (int)CastMenuOptions.Exit:
                        Console.WriteLine("Thanks for your visit. Please visit the Cast System again !!!!");
                        break;

                    default:
                        Console.WriteLine("Invalid Option");
                        break;

                }

                if (choice != (int)CastMenuOptions.Exit)
                {
                    Console.WriteLine("Press Enter to continue.......");
                    Console.ReadLine();
                    Console.Clear();
                }
            } while (choice != (int)CastMenuOptions.Exit);
        }
    }
    
}
