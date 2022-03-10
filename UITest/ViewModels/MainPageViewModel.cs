using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using UITest.Utils;
using UITest.Utils.PageManager;
using UITest.Views;

namespace UITest.ViewModels
{
    public class MainPageViewModel : BasePageViewModel
    {
        #region Properties

        #region Event



        #endregion



        #endregion

        #region Commands

        public DelegateCommand RefBooksCmd { get; }
        public DelegateCommand STSiKCmd    { get; }
        public DelegateCommand SMSCmd         { get; }

        #endregion

        #region Ctor

        public MainPageViewModel()
        {
            RefBooksCmd               = new DelegateCommand(OnRefBooks);
            STSiKCmd                  = new DelegateCommand(OnSTSiK);
            SMSCmd                    = new DelegateCommand(OnSMS);
        }

        #endregion

        #region Funcs

        #region Event



        #endregion

        #region Public



        #endregion

        #region Private

        private void OnRefBooks()
        {
            g.PageManager.AddAndSwitch<RefBooks, RefBooksViewModel>();
        }

        private void OnSTSiK()
        {
            g.PageManager.AddAndSwitch<STSiK, STSiKViewModel>(new object(), false);
        }

        private void OnSMS()
        {
            g.PageManager.AddAndSwitch<SMS, SMSViewModel>();
        }

        #endregion

        #endregion

    }
}

