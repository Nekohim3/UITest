using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using UITest.Utils;
using UITest.Utils.PageManager;
using UITest.Views;

namespace UITest.ViewModels
{
    public class RefBooksViewModel : BasePageViewModel
    {
        #region Properties

        #region Event



        #endregion
        

        #endregion

        #region Commands

        public DelegateCommand AnswersCmd { get; }

        #endregion

        #region Ctor

        public RefBooksViewModel()
        {
            AnswersCmd                = new DelegateCommand(OnAnswers);
        }

        #endregion

        #region Funcs

        #region Event



        #endregion

        #region Public



        #endregion

        #region Private

        private void OnAnswers()
        {
            g.PageManager.AddAndSwitch<AnswersListRefBook, AnswerListRefBookViewModel>();
        }

        #endregion

        #endregion

    }
}

