using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.UI.ConsoleApp.UI.Dashboards
{
    public class GenreDashboard : IDashboard
    {
        public void ShowDashboard()
        {
            ManageGenre manageGenre = new ManageGenre();
            manageGenre.Run();
        }

    }
}
