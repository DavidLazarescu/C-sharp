using QuizzGame.Model;
using QuizzGame.ViewModel.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace QuizzGame.ViewModel.Commands.QuizzGame
{
    public class PlayGameCommand : ICommand
    {
        QuizzVM vm;

        public event EventHandler CanExecuteChanged;


        public PlayGameCommand(QuizzVM _vm)
        {
            vm = _vm;
        }


        public bool CanExecute(object parameter)
        {
            /*If The ComboBoxes for choosing the kind and amount for the questions, isnt empty, return true*/
            if (string.IsNullOrEmpty(vm.KindOfQuestionsComboBoxAnswer) || string.IsNullOrEmpty(vm.AmountOfQuestionsComboBoxAnswer))
                return false;
            return true;
        }

        public async void Execute(object parameter)
        {
            vm.resetAnswerColors();

            /*Get the Questions*/
            var temp = await QuestionsHelper.GetQuestions(vm);

            /*Clear the Questions array to make sure its empty before adding new values to it*/
            vm.Questions.Clear();

            /*Create a temp Oberservable Collection*/
            ObservableCollection<Question> questions = new ObservableCollection<Question>();

            /*Add the new questions to the temp array*/
            foreach (var a in temp)
            {
                questions.Add(a);
            }

            /*Open the game window*/
            vm.showGameWindow();

            /*Sets the temp var to the real Questions List, cant be set directly, cause the INotifyPropChanged wouldnt trigger with
              just adding values to a Observable Collection*/
            vm.Questions = questions;

            /*Calculates the Questions left prop*/
            vm.QuestionsLeftCount = vm.Questions.Count - 1;

            /*Lets game over be false*/
            vm.GameOver = false;
        }
    }
}
