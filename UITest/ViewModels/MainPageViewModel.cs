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
        public sealed override bool ThisPageCanModifyEntities { get; set; }
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
            ThisPageCanModifyEntities = false;
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
            g.PageManager.AddAndSwitch<RefBooks, RefBooksViewModel>("RefBooks", Node);
        }

        private void OnSTSiK()
        {
            g.PageManager.AddAndSwitch<STSiK, STSiKViewModel>("STSiK", Node);
        }

        private void OnSMS()
        {
            g.PageManager.AddAndSwitch<SMS, SMSViewModel>("SMS", Node);
        }

        #endregion

        #endregion

    }
}

