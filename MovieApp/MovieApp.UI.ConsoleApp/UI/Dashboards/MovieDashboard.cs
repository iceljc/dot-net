using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.UI.ConsoleApp.UI.Dashboards
{
    public class MovieDashboard : IDashboard
    {
        public void ShowDashboard()
        {
            ManageMovie manageMovie = new ManageMovie();
            manageMovie.Run();
        }

    }
}
