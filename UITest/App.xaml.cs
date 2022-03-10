using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UITest.Utils;
using UITest.Utils.PageManager;
using UITest.ViewModels;
using UITest.Views;

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
            vm.InitStartPage(f);
            g.PageManager.Add<MainPage, MainPageViewModel>();
            g.PageManager.Switch(g.PageManager.GetRootByWindow(f));
            f.Show();
        }
    }
}
