using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class ShowRegisterCommand : ICommand
    {

        public LoginVM VM { get; set; }

        public event EventHandler CanExecuteChanged;



        public ShowRegisterCommand(LoginVM _vm)
        {
            VM = _vm;
        }



        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            VM.switchViews();
        }
    }
}
