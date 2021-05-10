using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class NewNotebookCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;


        public NotesVM VM { get; set; }



        public NewNotebookCommand(NotesVM _vm)
        {
            VM = _vm;
        }



        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            VM.createNotebook();
        }
    }
}
