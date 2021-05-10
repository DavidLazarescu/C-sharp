using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace QuizzGame.ViewModel.Commands.QuizzGame
{
    public class ToMenuWindowCommand : ICommand
    {
        private QuizzVM vm;

        public event EventHandler CanExecuteChanged;


        public ToMenuWindowCommand(QuizzVM _vm)
        {
            vm = _vm;
        }


        public bool CanExecute(object parameter)
        {
            /*Always true*/
            return true;
        }

        public void Execute(object parameter)
        {
            /*Navigates you back to the Menu window*/
            vm.showMenuWindow();
            vm.resetAfterRound();
        }
    }
}
