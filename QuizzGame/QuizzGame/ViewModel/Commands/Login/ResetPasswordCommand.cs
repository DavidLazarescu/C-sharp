using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace QuizzGame.ViewModel.Commands.Login
{
    public class ResetPasswordCommand : ICommand
    {
        LoginVM vm;
        
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public ResetPasswordCommand(LoginVM _vm)
        {
            vm = _vm;
        }


        public bool CanExecute(object parameter)
        {
            /*return true, if the email is proivided*/
            if (!string.IsNullOrEmpty(vm.User.Email))
                return true;
            
            return false;
        }

        public void Execute(object parameter)
        {
            vm.resetPassword();
        }
    }
}
