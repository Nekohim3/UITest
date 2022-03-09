using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        private ObservableCollection<TNode> _lst;

        public ObservableCollection<TNode> Lst
        {
            get => _lst;
            set
            {
                _lst = value;
                RaisePropertyChanged(() => Lst);
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
                    {
                        Root = node;
                        Lst  = new ObservableCollection<TNode>();
                        Lst.Add(Root);
                    }
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

            UpdateAll();
            RaisePropertyChanged(() => Root);
            RaisePropertyChanged(() => Lst);


            return node;
        }

        public void UpdateAll()
        {
            foreach (var x in GetAllNodes())
                x.Update();
        }

        public TNode Switch(TNode node)
        {
            _viewCounter++;
            node.ViewIndex                             = _viewCounter;
            node.ViewModel.WindowViewModel.CurrentNode = node;
            SetSelected(node);
            return node;
        }

        public bool CloseNode(TNode node, bool execWindowClose = true)
        {
            var root = GetRootByWindow(node.ViewModel.Window);

            var lst  = GetAllNodes(node);
            lst.Reverse();
            foreach (var x in lst)
            {
                if (x.ViewModel.Close())
                {
                    var parent = x.Parent;
                    if (parent == null) return true;
                    parent.Childs.Remove(x);
                    if (node.ViewModel.WindowViewModel.CurrentNode.Level > parent.Level)
                        parent.OnSwitch();
                    else
                        node.ViewModel.WindowViewModel.PageLineRefresh();
                }
                else
                {
                    x.OnSwitch();

                    UpdateAll();
                    RaisePropertyChanged(() => Root);
                    RaisePropertyChanged(() => Lst);

                    return false;
                }
            }

            if (root == node && execWindowClose)
            {
                root.ViewModel.Window.Close();
                return true;
            }

            UpdateAll();
            RaisePropertyChanged(() => Root);
            RaisePropertyChanged(() => Lst);

            return true;
        }

        public void Detach(TNode node)
        {
            var oldf  = node.ViewModel.Window;
            var oldvm = node.ViewModel.WindowViewModel;
            //node.ViewModel.WindowViewModel.CurrentNode = node.Parent;
            
            var f    = new CWindow();
            var vm   = new CWindowViewModel();
            f.DataContext = vm;
            var lst = GetAllNodes(node);
            foreach (var x in lst)
                x.ViewModel.Window = f;

            vm.CurrentNode = node;

            oldvm.CurrentNode = node.Parent;


            f.Show();
        }

        public void Attach(TNode node)
        {

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

        public List<CWindow> GetWindowList(TNode node = null)
        {
            var lst                = new List<CWindow>();
            foreach (var x in GetAllNodes(node))
                if(!lst.Contains(x.ViewModel.Window))
                    lst.Add(x.ViewModel.Window);
            return lst;
        }

        public TNode GetRootByWindow(CWindow wnd) => GetAllNodes().FirstOrDefault(x => x.ViewModel.Window == wnd);

        public TNode GetNodeByViewModelType<TVModel>() where TVModel : BasePageViewModel => GetAllNodes().FirstOrDefault(x => x.ViewModel.GetType() == typeof(TVModel));

        public void SetSelected(TNode node)
        {
            var lst = GetAllNodes().Where(x => x.ViewModel.Window == node.ViewModel.Window);
            foreach (var x in lst)
                x.SetSelected(x == node);
        }

        #endregion

        #region Private



        #endregion

        #endregion
    }
}

