using EvernoteClone.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public LoginVM VM { get; set; }


        public RegisterCommand(LoginVM _vm)
        {
            VM = _vm;
        }


        public bool CanExecute(object parameter)
        {
            var user = parameter as User;

            if (user == null)
                return false;
            if (string.IsNullOrEmpty(user.Email))
                return false;
            if (string.IsNullOrEmpty(user.Password))
                return false;
            if (string.IsNullOrEmpty(user.ConfirmPassword))
                return false;
            if (string.IsNullOrEmpty(user.Name))
                return false;
            if (string.IsNullOrEmpty(user.Lastname))
                return false;
            if (user.Password != user.ConfirmPassword)
            {
                VM.PasswordErrorVis = Visibility.Visible;
                return false;
            }

            VM.PasswordErrorVis = Visibility.Hidden;
            return true;
        }

        public void Execute(object parameter)
        {
            VM.register();
        }
    }
}
