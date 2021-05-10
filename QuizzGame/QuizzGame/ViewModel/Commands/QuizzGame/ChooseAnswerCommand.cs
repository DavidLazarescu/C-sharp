using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace QuizzGame.ViewModel.Commands.QuizzGame
{
    public class ChooseAnswerCommand : ICommand
    {
        QuizzVM vm;

        public event EventHandler CanExecuteChanged;

        public ChooseAnswerCommand(QuizzVM _vm)
        {
            vm = _vm;
        }


        public bool CanExecute(object parameter)
        {
            /*If the round isn't over, return true*/
            if (vm.RoundOver == false)
                return true;
            return false;
        }

        public void Execute(object parameter)
        {
            /*Check if the answer was wrong or right, call the appropriate function for it*/

            string res = parameter as string;

            if(res == vm.CurrentQuestion.correct_answer)
            {
                vm.rightAnswer();
            }
            else
            {
                vm.wrongAnswer();
            }
        }
    }
}
