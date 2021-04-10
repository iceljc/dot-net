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
    public class ManageUser
    {
        private readonly UserService userService;
        public ManageUser()
        {
            userService = new UserService();
        }

        #region sync
        void AddUser()
        {
            try 
            {
                User u = new User();

                Console.Write("Enter First Name => ");
                u.FirstName = Console.ReadLine();

                Console.Write("Enter Last Name => ");
                u.LastName = Console.ReadLine();

                Console.Write("Enter Date Of Birth (yyyy-mm-dd) => ");
                u.DateOfBirth = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);

                Console.Write("Enter Email => ");
                u.Email = Console.ReadLine();

                Console.Write("Enter Hashed Password => ");
                u.HashedPassword = Console.ReadLine();

                Console.Write("Enter Salt => ");
                u.Salt = Console.ReadLine();

                Console.Write("Enter Phone Number => ");
                u.PhoneNumber = Console.ReadLine();

                u.TwoFactorEnabled = false;
                u.LockoutEndDate = DateTime.Now;
                u.LastLoginDateTime = DateTime.Now;
                u.AccessFailedCount = 0;

                Console.Write("Enter IsLocked (0 or 1) => ");
                u.IsLocked = Console.ReadLine() == "1";


                int res = userService.AddUser(u);

                if (res > 0)
                {
                    Console.WriteLine("User added successfully");
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
            catch (Exception ex)
            {
                Console.WriteLine("some error has been occured. Contact the admin department");
            }
        }


        void UpdateUser()
        {
            try 
            {
                User u = new User();
                Console.Write("Enter Id => ");
                u.Id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter First Name => ");
                u.FirstName = Console.ReadLine();

                Console.Write("Enter Last Name => ");
                u.LastName = Console.ReadLine();

                Console.Write("Enter Date Of Birth (yyyy-mm-dd) => ");
                u.DateOfBirth = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);

                Console.Write("Enter Email => ");
                u.Email = Console.ReadLine();

                Console.Write("Enter Hashed Password => ");
                u.HashedPassword = Console.ReadLine();

                Console.Write("Enter Salt => ");
                u.Salt = Console.ReadLine();

                Console.Write("Enter Phone Number => ");
                u.PhoneNumber = Console.ReadLine();

                Console.Write("Enter IsLocked (0 or 1) => ");
                u.IsLocked = Console.ReadLine() == "1";

                int res = userService.UpdateUser(u);

                if (res > 0)
                {
                    Console.WriteLine("User update successfully");
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


        void DeleteUser()
        {
            try
            {
                Console.Write("Enter Id => ");
                int id = Convert.ToInt32(Console.ReadLine());
                int res = userService.DeleteUser(id);

                if (res > 0)
                {
                    Console.WriteLine("User delete successfully");
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
                IEnumerable<User> userCollection = userService.GetAllUser();
                if (userCollection != null)
                {
                    string format = "yyyy-MM-dd";
                    foreach (User item in userCollection)
                    {
                        Console.WriteLine(item.Id + " \t " + item.FirstName + " \t " + item.LastName + " \t " + item.DateOfBirth.ToString(format));
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


        void GetUserByIdWithMovie()
        {
            try
            {
                Console.Write("Enter Id => ");
                int id = Convert.ToInt32(Console.ReadLine());
                IEnumerable<User> users = userService.GetUserByIdWithMovie(id);

                if (users != null)
                {
                    var result = users.GroupBy(u => u.Id).Select(group =>
                    {
                        var groupedUser = group.First();
                        groupedUser.Movies = group.Select(p => p.Movies.Single()).ToList();
                        groupedUser.Ratings = group.Select(p => p.Ratings.Single()).ToList();
                        groupedUser.ReviewTexts = group.Select(p => p.ReviewTexts.Single()).ToList();
                        return groupedUser;
                    });

                    bool any = false;
                    foreach (var user in result)
                    {
                        any = true;
                        Console.WriteLine("User: " + user.FirstName + " " + user.LastName + "\n");
                        Console.WriteLine("Related Reviews: ");

                        if (user.Movies.Count == user.Ratings.Count && user.Movies.Count == user.ReviewTexts.Count)
                        {
                            int length = user.Movies.Count;
                            for (int i = 0; i < length; i++)
                            {
                                Console.WriteLine(user.Movies[i].Title + " => " + "Rating: " + user.Ratings[i] + ", Review: " + user.ReviewTexts[i]);
                                Console.WriteLine("------------------");
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
        async Task AddUserAsync()
        {
            try
            {
                User u = new User();

                Console.Write("Enter First Name => ");
                u.FirstName = Console.ReadLine();

                Console.Write("Enter Last Name => ");
                u.LastName = Console.ReadLine();

                Console.Write("Enter Date Of Birth (yyyy-mm-dd) => ");
                u.DateOfBirth = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);

                Console.Write("Enter Email => ");
                u.Email = Console.ReadLine();

                Console.Write("Enter Hashed Password => ");
                u.HashedPassword = Console.ReadLine();

                Console.Write("Enter Salt => ");
                u.Salt = Console.ReadLine();

                Console.Write("Enter Phone Number => ");
                u.PhoneNumber = Console.ReadLine();

                u.TwoFactorEnabled = false;
                u.LockoutEndDate = DateTime.Now;
                u.LastLoginDateTime = DateTime.Now;
                u.AccessFailedCount = 0;

                Console.Write("Enter IsLocked (0 or 1) => ");
                u.IsLocked = Console.ReadLine() == "1";


                int res = await userService.AddUserAsync(u);

                if (res > 0)
                {
                    Console.WriteLine("User added successfully");
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
            catch (Exception ex)
            {
                Console.WriteLine("some error has been occured. Contact the admin department");
            }
        }

        async Task UpdateUserAsync()
        {
            try
            {
                User u = new User();
                Console.Write("Enter Id => ");
                u.Id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter First Name => ");
                u.FirstName = Console.ReadLine();

                Console.Write("Enter Last Name => ");
                u.LastName = Console.ReadLine();

                Console.Write("Enter Date Of Birth (yyyy-mm-dd) => ");
                u.DateOfBirth = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);

                Console.Write("Enter Email => ");
                u.Email = Console.ReadLine();

                Console.Write("Enter Hashed Password => ");
                u.HashedPassword = Console.ReadLine();

                Console.Write("Enter Salt => ");
                u.Salt = Console.ReadLine();

                Console.Write("Enter Phone Number => ");
                u.PhoneNumber = Console.ReadLine();

                Console.Write("Enter IsLocked (0 or 1) => ");
                u.IsLocked = Console.ReadLine() == "1";

                int res = await userService.UpdateUserAsync(u);

                if (res > 0)
                {
                    Console.WriteLine("User update successfully");
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

        async Task DeleteUserAsync()
        {
            try
            {
                Console.Write("Enter Id => ");
                int id = Convert.ToInt32(Console.ReadLine());
                int res = await userService.DeleteUserAsync(id);

                if (res > 0)
                {
                    Console.WriteLine("User delete successfully");
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
                IEnumerable<User> userCollection = await userService.GetAllUserAsync();
                if (userCollection != null)
                {
                    string format = "yyyy-MM-dd";
                    foreach (User item in userCollection)
                    {
                        Console.WriteLine(item.Id + " \t " + item.FirstName + " \t " + item.LastName + " \t " + item.DateOfBirth.ToString(format));
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

        async Task GetUserByIdWithMovieAsync()
        {
            try
            {
                Console.Write("Enter Id => ");
                int id = Convert.ToInt32(Console.ReadLine());
                IEnumerable<User> users = await userService.GetUserByIdWithMovieAsync(id);

                if (users != null)
                {
                    var result = users.GroupBy(u => u.Id).Select(group =>
                    {
                        var groupedUser = group.First();
                        groupedUser.Movies = group.Select(p => p.Movies.Single()).ToList();
                        groupedUser.Ratings = group.Select(p => p.Ratings.Single()).ToList();
                        groupedUser.ReviewTexts = group.Select(p => p.ReviewTexts.Single()).ToList();
                        return groupedUser;
                    });

                    bool any = false;
                    foreach (var user in result)
                    {
                        any = true;
                        Console.WriteLine("User: " + user.FirstName + " " + user.LastName + "\n");
                        Console.WriteLine("Related Reviews: ");

                        if (user.Movies.Count == user.Ratings.Count && user.Movies.Count == user.ReviewTexts.Count)
                        {
                            int length = user.Movies.Count;
                            for (int i = 0; i < length; i++)
                            {
                                Console.WriteLine(user.Movies[i].Title + " => " + "Rating: " + user.Ratings[i] + ", Review: " + user.ReviewTexts[i]);
                                Console.WriteLine("------------------");
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
                IMenu userMenu = new UserMenu();
                choice = userMenu.PrintMenu();
                switch (choice)
                {
                    case (int)UserMenuOptions.Insert:
                        //await AddUserAsync();
                        AddUserAsync().Wait();
                        break;
                    case (int)UserMenuOptions.Delete:
                        //await DeleteUserAsync();
                        DeleteUserAsync().Wait();
                        break;
                    case (int)UserMenuOptions.Print:
                        //await PrintAllAsync();
                        PrintAllAsync().Wait();
                        break;
                    case (int)UserMenuOptions.Update:
                        //await UpdateUserAsync();
                        UpdateUserAsync().Wait();
                        break;
                    case (int)UserMenuOptions.GetUserWithMovie:
                        //await GetUserByIdWithMovieAsync();
                        GetUserByIdWithMovieAsync().Wait();
                        break;
                    case (int)UserMenuOptions.Exit:
                        Console.WriteLine("Thanks for your visit. Please visit the User System again !!!!");
                        break;

                    default:
                        Console.WriteLine("Invalid Option");
                        break;

                }

                if (choice != (int)UserMenuOptions.Exit)
                {
                    Console.WriteLine("Press Enter to continue.......");
                    Console.ReadLine();
                    Console.Clear();
                }
            } while (choice != (int)UserMenuOptions.Exit);
        }

    }
}
