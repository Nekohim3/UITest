using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism.ViewModel;

namespace UITest.Utils.PageManager
{
    public abstract class BasePageViewModel : NotificationObject
    {
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }

        }

        private CWindow _window;

        public CWindow Window
        {
            get => _window;
            set
            {
                _window = value;
                if (_window != null)
                    WindowViewModel = (CWindowViewModel)_window.DataContext;
                RaisePropertyChanged(() => Window);
            }
        }

        private CWindowViewModel _windowViewModel;

        public CWindowViewModel WindowViewModel
        {
            get => _windowViewModel;
            set
            {
                _windowViewModel = value;
                RaisePropertyChanged(() => WindowViewModel);
            }
        }

        private TNode _node;

        public TNode Node
        {
            get => _node;
            set
            {
                _node = value;
                RaisePropertyChanged(() => Node);
            }
        }

        public abstract bool ThisPageCanModifyEntities { get; set; }

        protected BasePageViewModel()
        {
            
        }

        public virtual bool Close()
        {
            return true;
        }
    }
}
