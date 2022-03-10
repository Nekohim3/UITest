using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism.ViewModel;
using UITest.ViewModels;
using UITest.Views;

namespace UITest.Utils.PageManager
{
    public class CWindowViewModel : NotificationObject
    {
        #region Properties

        #region Event



        #endregion
        

        public Visibility AttachToMainWindowVisible => g.PageManager.GetRootByWindow(CurrentNode.ViewModel.Window) != g.PageManager.Root ? Visibility.Visible : Visibility.Collapsed;

        #endregion

        #region Commands

        private TNode _currentNode;

        public TNode CurrentNode
        {
            get => _currentNode;
            set
            {
                _currentNode = value;
                if (_currentNode != null)
                    PageLineRefresh();
                RaisePropertyChanged(() => CurrentNode);
            }
        }

        //private ObservableCollection<TNode> _rootForBinding = new ObservableCollection<TNode>();

        //public ObservableCollection<TNode> RootForBinding
        //{
        //    get => _rootForBinding;
        //    set
        //    {
        //        _rootForBinding = value;
        //        RaisePropertyChanged(() => RootForBinding);
        //    }
        //}

        private ObservableCollection<TNode> _pageLine = new ObservableCollection<TNode>();

        public ObservableCollection<TNode> PageLine
        {
            get => _pageLine;
            set
            {
                _pageLine = value;
                RaisePropertyChanged(() => PageLine);
            }
        }

        #endregion

        #region Ctor

        public CWindowViewModel()
        {
            
        }

        public void InitStartPage(CWindow window)
        {
            //var node = g.PageManager.Add<MainPage, MainPageViewModel>(null, window);
        }

        #endregion

        #region Funcs

        #region Event



        #endregion

        #region Public

        public void PageLineRefresh()
        {
            PageLine.Clear();
            var node  = g.PageManager.GetRootByWindow(CurrentNode.ViewModel.Window);
            var cnode = CurrentNode;
            while (cnode != node)
            {
                PageLine.Insert(0, cnode);
                cnode = cnode.Parent;
            }
            PageLine.Insert(0, node);
            cnode = CurrentNode;
            while (cnode != null && cnode.ViewModel.Window == CurrentNode.ViewModel.Window && cnode.Childs.Count(x => x.ViewModel.Window == cnode.ViewModel.Window) != 0)
            {
                if (cnode.Childs.Count == 0)
                    cnode = null;
                else
                {
                    var max = cnode.Childs.Where(x => x.ViewModel.Window == cnode.ViewModel.Window).Max(x => x.ViewIndex);
                    cnode = max == 0 ? null : cnode.Childs.FirstOrDefault(x => x.ViewIndex == max);
                    PageLine.Add(cnode);
                }
            }

            //RootForBinding.Clear();// = new ObservableCollection<TNode>();
            //if (RootForBinding.Count == 0)
            //    RootForBinding.Add(g.PageManager.GetRootByWindow(CurrentNode.ViewModel.Window));
            //else
            //    RootForBinding[0] = g.PageManager.GetRootByWindow(CurrentNode.ViewModel.Window);
        }

        #endregion

        #region Private



        #endregion

        #endregion
    }
}
