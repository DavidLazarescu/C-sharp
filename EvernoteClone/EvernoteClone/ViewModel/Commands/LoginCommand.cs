using EvernoteClone.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class LoginCommand : ICommand
    {

        public LoginVM VM { get; set; }

        public LoginCommand(LoginVM _vm)
        {
            VM = _vm;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            var user = parameter as User;

            if(user == null)
                return false;
            if (string.IsNullOrEmpty(user.Email))
                return false;
            if (string.IsNullOrEmpty(user.Password))
                return false;

            return true;
        }

        public void Execute(object parameter)
        {
            VM.login();
        }
    }
}
