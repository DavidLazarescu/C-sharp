using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace QuizzGame.ViewModel.Commands.Login
{
    public class ToResetPasswordWindowCommand : ICommand
    {
        LoginVM vm;

        public event EventHandler CanExecuteChanged;


        public ToResetPasswordWindowCommand(LoginVM _vm)
        {
            vm = _vm;
        }


        public bool CanExecute(object parameter)
        {
            /*Always return true*/
            return true;
        }

        public void Execute(object parameter)
        {
            /*Set the ResetPassword window to visible*/
            vm.RegisterVis = Visibility.Collapsed;
            vm.LoginVis = Visibility.Collapsed;
            vm.ResetPasswordVis = Visibility.Visible;

            /*Sets the fields to empty, so they dont sustain*/
            vm.Email = string.Empty;
            vm.Firstname = string.Empty;
            vm.Lastname = string.Empty;
            vm.Password = string.Empty;
            vm.ConfirmPassword = string.Empty;
        }
    }
}
