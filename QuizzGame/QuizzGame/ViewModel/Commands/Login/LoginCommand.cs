using System;
using System.Windows.Input;

namespace QuizzGame.ViewModel.Commands.Login
{
    public class LoginCommand : ICommand
    {
        private LoginVM vm;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public LoginCommand(LoginVM _vm)
        {
            vm = _vm;
        }

        public bool CanExecute(object parameter)
        {
            /*Return true, if the password and email textbox are not empty*/
            if (!string.IsNullOrEmpty(vm.Email) && !string.IsNullOrEmpty(vm.Password))
            {
                return true;
            }

            return false;
        }

        public async void Execute(object parameter)
        {
            /*Cant do the login directly from here, because u can only invoke a event from inside its class*/
            vm.login();
        }
    }
}