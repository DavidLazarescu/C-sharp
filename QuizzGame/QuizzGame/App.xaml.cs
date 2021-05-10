using QuizzGame.ViewModel;
using QuizzGame.ViewModel.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace QuizzGame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string UserId = string.Empty;

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            QuizzVM vm = new QuizzVM();
        }
    }
}
