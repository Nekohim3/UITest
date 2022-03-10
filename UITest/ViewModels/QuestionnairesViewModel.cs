using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
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
            var node = g.PageManager.AddAndSwitch<Questionnaire, QuestionnaireViewModel>(false);
        }

        private void OnEdit()
        {
            var node = g.PageManager.AddAndSwitch<Questionnaire, QuestionnaireViewModel>(true);
        }

        private void OnAns()
        {

            var node = g.PageManager.AddAndSwitch<AnswerList, AnswerListViewModel>(null);
        }

        private void OnHistory()
        {
            Node.Page.Title = "qwe";
        }

        public override bool Close()
        {
            MessageBox.Show("qs false");
            return false;
        }

        #endregion

        #endregion
    }
}

