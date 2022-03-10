using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UITest.Utils;
using UITest.Utils.PageManager;

namespace UITest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CWindow : Window
    {
        public CWindow()
        {
            InitializeComponent();
        }

        private void CWindow_OnClosing(object sender, CancelEventArgs e)
        {
            var root = g.PageManager.GetRootByWindow(this);
            if (root == null) return;
            if (root != g.PageManager.Root)
            {
                var wins = g.PageManager.GetWindowList(root);
                wins.Reverse();
                foreach (var x in wins.Where(x => x != this))
                    x.Close();
                foreach (var x in g.PageManager.GetAllNodes(root))
                {
                    x.ViewModel.Window          = root.Parent.ViewModel.Window;
                    x.ViewModel.WindowViewModel = root.Parent.ViewModel.WindowViewModel;
                }

                root.Parent.ViewModel.WindowViewModel.PageLineRefresh();
            }
            else
            {
                var wins = g.PageManager.GetWindowList(root);
                wins.Reverse();
                foreach (var x in wins.Where(x => x != this))
                    x.Close();
                foreach (var x in g.PageManager.GetAllNodes(root))
                {
                    x.ViewModel.Window          = root.ViewModel.Window;
                    x.ViewModel.WindowViewModel = root.ViewModel.WindowViewModel;
                }

                root.ViewModel.WindowViewModel.PageLineRefresh();

                if(!g.PageManager.CloseNode(root))
                    e.Cancel = true;
            }
        }
    }
}
