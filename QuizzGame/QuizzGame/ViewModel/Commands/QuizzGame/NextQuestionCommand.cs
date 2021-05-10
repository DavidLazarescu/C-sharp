using QuizzGame.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace QuizzGame.ViewModel.Commands.QuizzGame
{
    public class NextQuestionCommand : ICommand
    {

        private QuizzVM vm;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public NextQuestionCommand(QuizzVM _vm)
        {
            vm = _vm;
        }


        public bool CanExecute(object parameter)
        {
            /*If the game is over, return true*/

            bool gameIsOver = false;
            if (parameter is bool)
                gameIsOver = (bool)parameter;

            if (gameIsOver == true)
                return true;
            else
                return false;
        }

        public void Execute(object parameter)
        {
            /*Increase the Current Question == go to the next question*/
            vm.CurrentQuestion = new Question();
            /*Reset the answer option colors*/
            vm.resetAnswerColors();
            vm.RoundOver = false;
            vm.QuestionsLeftCount--;
            vm.Answer = string.Empty;
        }
    }
}