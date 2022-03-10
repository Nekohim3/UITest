﻿using System;
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
    public class STSiKViewModel : BasePageViewModel
    {
        #region Properties

        #region Event



        #endregion
        

        #endregion

        #region Commands

        public DelegateCommand QuestionnairesCmd { get; }

        #endregion

        #region Ctor

        public STSiKViewModel(object selected, bool editMode)
        {
            QuestionnairesCmd = new DelegateCommand(OnQuestionnaires);
        }

        #endregion

        #region Funcs

        #region Event



        #endregion

        #region Public



        #endregion

        #region Private

        private void OnQuestionnaires()
        {
            g.PageManager.AddAndSwitch<Questionnaires, QuestionnairesViewModel>();
        }

        #endregion

        #endregion

    }
}

