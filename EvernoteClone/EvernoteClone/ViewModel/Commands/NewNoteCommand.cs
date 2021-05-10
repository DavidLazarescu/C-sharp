using EvernoteClone.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class NewNoteCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }


        public NotesVM VM { get; set; }



        public NewNoteCommand(NotesVM _vM)
        {
            VM = _vM;
        }



        public bool CanExecute(object parameter)
        {
            Notebook selectedNotebook = parameter as Notebook;
            return selectedNotebook != null ? true : false;
        }

        public void Execute(object parameter)
        {
            Notebook selectedNotebook = (parameter as Notebook);
            VM.createNote(selectedNotebook.Id);
        }
    }
}
