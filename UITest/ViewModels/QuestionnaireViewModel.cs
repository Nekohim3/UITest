using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using UITest.Utils;
using UITest.Utils.PageManager;

namespace UITest.ViewModels
{
    public class QuestionnaireViewModel : BasePageViewModel
    {
        #region Properties

        #region Event



        #endregion

        public sealed override bool ThisPageCanModifyEntities { get; set; }

        #endregion

        #region Commands



        #endregion

        #region Ctor

        public QuestionnaireViewModel()
        {
            ThisPageCanModifyEntities = true;
        }

        public void InitViewModel(object selectedItem, bool editMode)
        {
            ThisPageCanModifyEntities = editMode;
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

