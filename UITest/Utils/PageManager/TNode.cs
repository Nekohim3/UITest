using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;

namespace UITest.Utils.PageManager
{
    public class TNode
    {
        public Page              Page      { get; set; }
        public BasePageViewModel ViewModel { get; set; }
        public List<TNode>       Childs    { get; set; }
        public TNode             Parent    { get; set; }
        public int               ViewIndex { get; set; }
        public int               Level     => Parent?.Level + 1 ?? 0;

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
            Childs           = new List<TNode>();
        }

        public void OnEscape()
        {

        }

        public void OnClose()
        {

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
