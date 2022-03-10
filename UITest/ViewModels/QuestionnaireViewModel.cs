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
        
        private                bool    _editMode;

        public bool EditMode
        {
            get => _editMode;
            set
            {
                _editMode = value;
                RaisePropertyChanged(() => EditMode);
            }
        }

        #endregion

        #region Commands



        #endregion

        #region Ctor

        public QuestionnaireViewModel(bool editMode)
        {
            EditMode = editMode;
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

