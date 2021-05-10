using EvernoteClone.Model;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class DeleteCommand : ICommand
    {
        private NotesVM VM;

        public event EventHandler CanExecuteChanged;


        public DeleteCommand(NotesVM _vm)
        {
            VM = _vm;
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (parameter is Notebook)
            {
                await DatabaseHelper.Delete(parameter as Notebook);
                VM.getNotebooks();
            }
            else if (parameter is Note)
            {
                await DatabaseHelper.Delete(parameter as Note);
                VM.getNotes();
            }
        }
    }
}