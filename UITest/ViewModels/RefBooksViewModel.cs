using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using UITest.Utils.PageManager;

namespace UITest.ViewModels
{
    public class RefBooksViewModel : BasePageViewModel
    {
        #region Properties

        #region Event



        #endregion
        
        public sealed override bool ThisPageCanModifyEntities { get; set; }

        #endregion

        #region Commands

        public DelegateCommand RefBooksCmd { get; }
        public DelegateCommand STSiKCmd    { get; }

        public DelegateCommand SMSCmd { get; }

        #endregion

        #region Ctor

        public RefBooksViewModel()
        {
            ThisPageCanModifyEntities = false;
        }

        #endregion

        #region Funcs

        #region Event



        #endregion

        #region Public



        #endregion

        #region Private

        private void OnRefBook()
        {

        }

        #endregion

        #endregion

    }
}

