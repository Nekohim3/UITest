using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UITest.Utils;
using UITest.Utils.PageManager;

namespace UITest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var f  = new CWindow();
            var vm = new CWindowViewModel();
            f.DataContext = vm;
            vm.InitStartPage(f, "MainPage");
            g.PageManager.Switch(g.PageManager.GetRootByWindow(f));
            f.Show();
        }
    }
}
