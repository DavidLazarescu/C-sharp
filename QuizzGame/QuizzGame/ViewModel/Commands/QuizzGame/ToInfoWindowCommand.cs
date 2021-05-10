using QuizzGame.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace QuizzGame.ViewModel.Commands.QuizzGame
{
    public class ToInfoWindowCommand : ICommand
    {
        QuizzVM vm;
        public event EventHandler CanExecuteChanged;

        public ToInfoWindowCommand(QuizzVM _vm)
        {
            vm = _vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            UserInfoWindow userInfoWindow = new UserInfoWindow();
            userInfoWindow.ShowDialog();
        }
    }
}
