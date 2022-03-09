using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace UITest.Utils.PageManager
{
    public class TNode : NotificationObject
    {
        private Page                        _page;
        private BasePageViewModel           _viewModel;
        private ObservableCollection<TNode> _childs;
        private TNode                       _parent;
        private int                         _viewIndex;

        public Page Page
        {
            get => _page;
            set { _page = value; RaisePropertyChanged(() => Page);}
        }

        public BasePageViewModel ViewModel
        {
            get => _viewModel;
            set { _viewModel = value; RaisePropertyChanged(() => ViewModel);}
        }

        public ObservableCollection<TNode> Childs
        {
            get => _childs;
            set { _childs = value; RaisePropertyChanged(() => Childs); }
        }

        public TNode Parent
        {
            get => _parent;
            set { _parent = value; RaisePropertyChanged(() => Parent); }
        }

        public int ViewIndex
        {
            get => _viewIndex;
            set { _viewIndex = value; RaisePropertyChanged(() => ViewIndex); }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                if (_isSelected)
                    g.PageManager.Switch(this);
                RaisePropertyChanged(() => IsSelected);
            }
        }

        public  int               Level     => Parent?.Level + 1 ?? 0;
        public  bool              IsRoot    => Parent == null;

        public DelegateCommand CloseCmd  { get; }
        public DelegateCommand SwitchCmd { get; }
        public DelegateCommand EscapeCmd { get; }

        public TNode(Page page, BasePageViewModel vm, TNode parent = null)
        {
            CloseCmd         = new DelegateCommand(OnClose);
            SwitchCmd        = new DelegateCommand(OnSwitch);
            EscapeCmd        = new DelegateCommand(OnEscape);
            Page             = page;
            ViewModel        = vm;
            Page.DataContext = ViewModel;
            ViewModel.Node   = this;
            Parent           = parent;
            Childs           = new ObservableCollection<TNode>();
        }

        public void SetSelected(bool s)
        {
            _isSelected = s;
            RaisePropertyChanged(() => IsSelected);
        }

        public void Update()
        {
            RaisePropertyChanged(() => Page);
            RaisePropertyChanged(() => ViewModel);
            RaisePropertyChanged(() => Childs);
            RaisePropertyChanged(() => Parent);
            RaisePropertyChanged(() => ViewIndex);
        }

        public void OnEscape()
        {
            g.PageManager.Detach(this);
        }

        public void OnClose()
        {
            g.PageManager.CloseNode(this);
        }

        public void OnSwitch()
        {
            g.PageManager.Switch(this);
        }

        public override string ToString()
        {
            return ViewModel.Name;
        }
    }
}
