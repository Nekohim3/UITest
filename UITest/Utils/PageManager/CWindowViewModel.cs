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

        public void InitStartPage(CWindow window, string name)
        {
            var node = g.PageManager.Add<MainPage, MainPageViewModel>(name, null, window);
        }

        #endregion

        #region Funcs

        #region Event



        #endregion

        #region Public

        public void PageLineRefresh()
        {
            PageLine.Clear();
            var node = g.PageManager.GetRootByWindow(CurrentNode.ViewModel.Window);
            while (node != null)
            {
                PageLine.Add(node);
                if (node.Childs.Count == 0)
                    node = null;
                else
                {
                    var max = node.Childs.Max(x => x.ViewIndex);
                    node = max == 0 ? null : node.Childs.FirstOrDefault(x => x.ViewIndex == max);
                }
            }
        }

        #endregion

        #region Private



        #endregion

        #endregion
    }
}
