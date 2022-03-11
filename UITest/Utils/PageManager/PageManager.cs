using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.ViewModel;

namespace UITest.Utils.PageManager
{
    public class PageManager : NotificationObject
    {
        #region Properties

        #region Event



        #endregion
        
        private int   _viewCounter = 0;

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

        private ObservableCollection<TNode> _rootLst;

        public ObservableCollection<TNode> RootLst
        {
            get => _rootLst;
            set
            {
                _rootLst = value;
                RaisePropertyChanged(() => RootLst);
            }
        }

        private TNode _selectedNode;

        public TNode SelectedNode
        {
            get => _selectedNode;
            set
            {
                _selectedNode = value;
                if (_selectedNode != null)
                    g.PageManager.Switch(_selectedNode);
                RaisePropertyChanged(() => SelectedNode);
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

        public static object CreateInstance(Type pContext, object[] args = null)
        {
            var argTypes = new List<Type>();
            if (args != null) argTypes.AddRange(args.Select(param => param?.GetType()));

            var ctors = pContext.GetConstructors();
            foreach (var ctor in ctors)
            {
                var paramList = ctor.GetParameters();
                if (argTypes.Count != paramList.Length) continue;
                var areTypesCompatible = true;
                if (args != null)
                    for (var i = 0; i < args.Length; i++)
                    {
                        if (argTypes[i] == null)
                        {
                            if (paramList[i].ParameterType.IsValueType)
                            {
                                args[i]     = CreateInstance(paramList[i].ParameterType, null);
                                argTypes[i] = args[i].GetType();
                            }
                            else
                            {
                                argTypes[i] = paramList[i].ParameterType;
                            }
                        }

                        if (paramList[i].ParameterType.IsAssignableFrom(argTypes[i])) continue;
                        areTypesCompatible = false;
                        break;
                    }

                if (areTypesCompatible)
                    return ctor.Invoke(args);
            }

            try
            {
                return Activator.CreateInstance(pContext);
            }
            catch
            {
                return null;
            }
        }
        public TNode AddAndSwitch<TPage, TVModel>(params object[] p) where TPage : Page where TVModel : BasePageViewModel
        {
            if (p == null)
                p = new object[] { null };
            var   callerVmType = new StackTrace().GetFrame(1).GetMethod().DeclaringType;
            TNode parent       = null;
            if (callerVmType?.FullName != null)
            {
                var type = Type.GetType(callerVmType.FullName);
                parent = GetNodeByViewModelType(type);
            }

            var node = Add<TPage, TVModel>(parent, p);
            return node == null ? null : Switch(node);
        }
        
        public TNode Add<TPage, TVModel>(TNode parent = null, params object[] p) where TPage: Page where TVModel : BasePageViewModel
        {
            if (p == null)
                p = new object[] { null };
            var node = GetNodeByViewModelType<TVModel>();
            if (node == null)
            {
                var f  = (TPage)CreateInstance(typeof(TPage));
                var vm = (TVModel)CreateInstance(typeof(TVModel), p);
                if (vm == null)
                {
                    MessageBox.Show("Накосячил с передаваемыми параметрами (ни 1 конструктор не подходит)");
                    return null;
                }
                node = new TNode(f, vm)
                       {
                           ViewModel =
                           {
                               Window = parent == null ? (CWindow)Application.Current.MainWindow : parent.ViewModel.Window,
                           }
                       };
                if (parent == null)
                {
                    if (Root == null)
                    {
                        Root = node;
                        RootLst  = new ObservableCollection<TNode> { Root };
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
            RaisePropertyChanged(() => RootLst);


            return node;
        }

        public void UpdateAll()
        {
            foreach (var x in GetAllNodes())
                x.Update();

            RaisePropertyChanged(() => Root);
            RaisePropertyChanged(() => RootLst);
        }

        public TNode Switch(TNode node)
        {
            _viewCounter++;
            node.ViewIndex                             = _viewCounter;
            node.ViewModel.WindowViewModel.CurrentNode = node;
            SetSelected(node);
            if (node.ViewModel.Window != Application.Current.Windows.OfType<CWindow>().SingleOrDefault(x => x.IsActive))
                ThreadPool.QueueUserWorkItem(x =>
                                             {
                                                 Thread.Sleep(10);
                                                 g.PageManager.SelectedNode.ViewModel.Window.Dispatcher.Invoke(() =>
                                                                                                               {
                                                                                                                   g.PageManager.SelectedNode.ViewModel.Window.Activate();
                                                                                                               });
                                             });

            return node;
        }

        public bool CloseNode(TNode node)
        {
            var wnds = GetWindowList();
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
                    {
                        var newWnds = GetWindowList();
                        if (wnds.Count == newWnds.Count) return false;
                        foreach (var c in wnds.Where(c => !newWnds.Contains(c)))
                            c.Close();
                    }
                    return false;
                }
            }

            UpdateAll();
            {
                var newWnds = GetWindowList();
                if (wnds.Count == newWnds.Count) return false;
                foreach (var c in wnds.Where(c => !newWnds.Contains(c)))
                    c.Close();
            }

            return true;
        }

        public void Detach(TNode node)
        {
            var oldf  = node.ViewModel.Window;
            var oldvm = node.ViewModel.WindowViewModel;
            
            var f    = new CWindow();
            var vm   = new CWindowViewModel();
            f.DataContext = vm;
            var lst = GetAllNodes(node);
            foreach (var x in lst.Where(x => x.ViewModel.Window == oldf))
                x.ViewModel.Window = f;

            vm.CurrentNode = node;
            oldvm.CurrentNode = node.Parent;
            f.Show();
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
            var lst = new List<CWindow>();
            foreach (var x in GetAllNodes(node))
                if(!lst.Contains(x.ViewModel.Window))
                    lst.Add(x.ViewModel.Window);
            return lst;
        }

        public TNode GetRootByWindow(CWindow wnd) => GetAllNodes().FirstOrDefault(x => x.ViewModel.Window == wnd);

        public TNode GetNodeByViewModelType<TVModel>() where TVModel : BasePageViewModel => GetAllNodes().FirstOrDefault(x => x.ViewModel.GetType() == typeof(TVModel));
        public TNode GetNodeByViewModelType(Type type)  => GetAllNodes().FirstOrDefault(x => x.ViewModel.GetType() == type);

        public void SetSelected(TNode node)
        {
            var lst = GetAllNodes();
            foreach (var x in lst)
                x.SetSelected(false);
            node.SetSelected(true);
            _selectedNode = node;
            RaisePropertyChanged(() => SelectedNode);
        }

        #endregion

        #region Private



        #endregion

        #endregion
    }
}

