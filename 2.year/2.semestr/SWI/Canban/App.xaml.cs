using Canban.View.Windows;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Canban
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DbLoad load = new DbLoad();
            load.LoadDB();

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }

}
