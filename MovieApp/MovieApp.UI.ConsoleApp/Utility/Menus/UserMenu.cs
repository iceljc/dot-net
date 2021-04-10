using MovieApp.UI.ConsoleApp.Utility.Menus.MenuOptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApp.UI.ConsoleApp.Utility.Menus
{
    public class UserMenu : IMenu
    {
        public int PrintMenu()
        {
            int choice = 0;

            string[] names = Enum.GetNames(typeof(UserMenuOptions));
            int[] values = (int[])Enum.GetValues(typeof(UserMenuOptions));

            int length = names.Length;

            Console.WriteLine("This is User System");
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine("Press {0} for {1}", values[i], names[i]);
            }
            Console.Write("Enter your choice => ");

            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
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

            return choice;
        }
    }
}
