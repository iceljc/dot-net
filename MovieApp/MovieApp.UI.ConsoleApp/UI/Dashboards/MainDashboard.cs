using MovieApp.UI.ConsoleApp.Utility.Menus;
using MovieApp.UI.ConsoleApp.Utility.Menus.MenuOptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.UI.ConsoleApp.UI.Dashboards
{
    public class MainDashboard : IDashboard
    {
        public void ShowDashboard()
        {
            int choice = 0;
            IDashboard dashboard;
            IMenu mainMenu = new MainMenu();

            do
            {
                choice = mainMenu.PrintMenu();

                switch (choice)
                {
                    case (int)TopMenuOptions.MovieSystem:
                        Console.Clear();
                        dashboard = new MovieDashboard();
                        dashboard.ShowDashboard();
                        break;
                    case (int)TopMenuOptions.UserSystem:
                        Console.Clear();
                        dashboard = new UserDashboard();
                        dashboard.ShowDashboard();
                        break;
                    case (int)TopMenuOptions.GenreSystem:
                        Console.Clear();
                        dashboard = new GenreDashboard();
                        dashboard.ShowDashboard();
                        break;
                    case (int)TopMenuOptions.CastSystem:
                        Console.Clear();
                        dashboard = new CastDashboard();
                        dashboard.ShowDashboard();
                        break;
                    case (int)TopMenuOptions.Exit:
                        Console.WriteLine("Thanks for your visit. Please visit again !!!!");
                        break;
                    default:
                        Console.WriteLine("Invalid Option.");
                        break;
                }

                if (choice != (int)TopMenuOptions.Exit)
                {
                    //Console.WriteLine("main " + choice);
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    Console.Clear();
                }

            } while (choice != (int)TopMenuOptions.Exit);
        }

    }
}
