using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.UI.ConsoleApp.UI.Dashboards
{
    public class UserDashboard : IDashboard
    {
        public void ShowDashboard()
        {
            ManageUser manageUser = new ManageUser();
            manageUser.Run();
        }

    }
}
