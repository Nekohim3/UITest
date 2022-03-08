using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Microsoft.Practices.Prism.ViewModel;

namespace UITest.Utils.PageManager
{
    public class PageManager : NotificationObject
    {
        #region Properties

        #region Event



        #endregion

        private int _viewCounter = 0;

        private TNode _root;

        public TNode Root
        {
            get => _root;
            set
            {
                _root = value;
                RaisePropertyChanged(() => Root);
            }
        }

        #endregion

        #region Commands



        #endregion

        #region Ctor

        public PageManager()
        {

        }

        #endregion

        #region Funcs

        #region Event



        #endregion

        #region Public

        public TNode AddAndSwitch<TPage, TVModel>(string name, TNode parent) where TPage : Page, new() where TVModel : BasePageViewModel, new()
        {
            return Switch(Add<TPage, TVModel>(name, parent));
        }


        public TNode Add<TPage, TVModel>(string name, TNode parent, CWindow wnd = null) where TPage: Page, new() where TVModel : BasePageViewModel, new()
        {
            var node = GetNodeByViewModelType<TVModel>();
            if (node == null)
            {
                node = new TNode(new TPage(), new TVModel())
                       {
                            ViewModel =
                            {
                                Window = wnd ?? parent.ViewModel.Window,
                                Name   = name,
                            }
                       };
                if (parent == null)
                {
                    if (Root == null)
                        Root = node;
                    else
                    {
                        throw new Exception("Parent is null");
                    }
                }
                else
                {
                    node.Parent = parent;
                    parent.Childs.Add(node);
                }
            }

            return node;
        }

        public TNode Switch(TNode node)
        {
            _viewCounter++;
            node.ViewIndex                             = _viewCounter;
            node.ViewModel.WindowViewModel.CurrentNode = node;
            return node;
        }

        public bool CloseNode(TNode node)
        {
            var lst = GetAllNodes(node);
            lst.Reverse();
            foreach (var x in lst)
            {
                if (x.ViewModel.Close())
                {
                    var parent = x.Parent;
                    parent.Childs.Remove(x);
                    if(node.ViewModel.WindowViewModel.CurrentNode.Level > parent.Level)
                        parent.OnSwitch();
                    else
                        node.ViewModel.WindowViewModel.PageLineRefresh();
                }
                else
                {
                    x.OnSwitch();
                    return false;
                }
            }

            return true;
        }

        public List<TNode> GetAllNodes(TNode root = null)
        {
            if (root == null) root = Root;
            if (root == null) return new List<TNode>();
            var lst = new List<TNode> {root};
            GetAllNodesRec(lst, root);
            return lst;
        }

        private void GetAllNodesRec(List<TNode> lst, TNode node)
        {
            foreach (var x in node.Childs)
            {
                lst.Add(x);
                GetAllNodesRec(lst, x);
            }
        }

        public TNode GetRootByWindow(CWindow wnd) => GetAllNodes().FirstOrDefault(x => x.ViewModel.Window == wnd);

        public TNode GetNodeByViewModelType<TVModel>() where TVModel : BasePageViewModel => GetAllNodes().FirstOrDefault(x => x.ViewModel.GetType() == typeof(TVModel));

        #endregion

        #region Private



        #endregion

        #endregion
    }
}

