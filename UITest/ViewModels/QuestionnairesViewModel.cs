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
    public class QuestionnairesViewModel : BasePageViewModel
    {
        #region Properties

        #region Event



        #endregion

        public sealed override bool ThisPageCanModifyEntities { get; set; }

        #endregion

        #region Commands

        public DelegateCommand ViewCmd    { get; }
        public DelegateCommand EditCmd    { get; }
        public DelegateCommand AnsCmd     { get; }
        public DelegateCommand HistoryCmd { get; }

        #endregion

        #region Ctor

        public QuestionnairesViewModel()
        {
            ViewCmd    = new DelegateCommand(OnView);
            EditCmd    = new DelegateCommand(OnEdit);
            AnsCmd     = new DelegateCommand(OnAns);
            HistoryCmd = new DelegateCommand(OnHistory);
        }

        #endregion

        #region Funcs

        #region Event



        #endregion

        #region Public



        #endregion

        #region Private

        private void OnView()
        {
            var node = g.PageManager.AddAndSwitch<Questionnaire, QuestionnaireViewModel>("Questionnaire", Node);
            ((QuestionnaireViewModel) node.ViewModel).InitViewModel(null, false);
        }

        private void OnEdit()
        {
            var node = g.PageManager.AddAndSwitch<Questionnaire, QuestionnaireViewModel>("Questionnaire", Node);
            ((QuestionnaireViewModel)node.ViewModel).InitViewModel(null, true);
        }

        private void OnAns()
        {

            var node = g.PageManager.AddAndSwitch<AnswerList, AnswerListViewModel>("AnsList", Node);
            ((AnswerListViewModel)node.ViewModel).InitViewModel(null);
        }

        private void OnHistory()
        {

        }

        #endregion

        #endregion
    }
}

