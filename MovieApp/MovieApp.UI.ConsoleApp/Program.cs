using MovieApp.UI.ConsoleApp.UI.Dashboards;
using System;
using System.Linq;

namespace MovieApp.UI.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            IDashboard dashboard = new MainDashboard();
            dashboard.ShowDashboard();
        }
    }
}
