﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using UITest.Utils.PageManager;

namespace UITest.ViewModels
{
    public class AnswerListRefBookViewModel : BasePageViewModel
    {
        #region Properties

        #region Event



        #endregion

        public sealed override bool ThisPageCanModifyEntities { get; set; }

        #endregion

        #region Commands



        #endregion

        #region Ctor

        public AnswerListRefBookViewModel()
        {
            ThisPageCanModifyEntities = true;
        }

        #endregion

        #region Funcs

        #region Event



        #endregion

        #region Public



        #endregion

        #region Private



        #endregion

        #endregion
    }
}

