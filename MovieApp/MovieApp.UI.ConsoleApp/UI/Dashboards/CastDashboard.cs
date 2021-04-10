using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.UI.ConsoleApp.UI.Dashboards
{
    public class CastDashboard : IDashboard
    {
        public void ShowDashboard()
        {
            ManageCast manageCast = new ManageCast();
            manageCast.Run();
        }

    }
}
