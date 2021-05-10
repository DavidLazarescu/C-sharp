using QuizzGame.ViewModel.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace QuizzGame.ViewModel.Commands.Login
{
    public class RegisterCommand : ICommand
    {
        LoginVM vm;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public RegisterCommand(LoginVM _vm)
        {
            vm = _vm;
        }


        public bool CanExecute(object parameter)
        {
            /*return true, if all TextBoxes are filled out*/
            if(!string.IsNullOrEmpty(vm.Firstname) && !string.IsNullOrEmpty(vm.Lastname) && !string.IsNullOrEmpty(vm.Email) && !string.IsNullOrEmpty(vm.Password) &&
                !string.IsNullOrEmpty(vm.ConfirmPassword) && vm.ConfirmPassword == vm.Password)
            {
                return true;
            }

            return false;
        }

        public async void Execute(object parameter)
        {
            /*calls the register method*/
            vm.register();
        }
    }
}
