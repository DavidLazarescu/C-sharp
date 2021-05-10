using EvernoteClone.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class EndRenamingCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public NotesVM VM { get; set; }


        public EndRenamingCommand(NotesVM _vm)
        {
            VM = _vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Notebook notebook = parameter as Notebook;
            if (notebook != null)
            {
                VM.stopEditingNotebook(notebook);
                return;
            }

            Note note = parameter as Note;
            if(note != null)
            {
                VM.stopEditingNotebook(note);
                return;
            }
        }
    }
}
