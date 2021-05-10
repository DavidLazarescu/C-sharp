using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace EvernoteClone.ViewModel
{
    public class NotesVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler SelectedNoteChanged;


        public ObservableCollection<Notebook> Notebooks { get; set; }
        public ObservableCollection<Note> Notes { get; set; }

        
        private Notebook selectedNotebook;
        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set 
            { 
                selectedNotebook = value;
                OnPropertyChanged("SelectedNotebook");
                getNotes();
            }
        }

        public NewNoteCommand NewNoteCommand { get; set; }

        public NewNotebookCommand NewNotebookCommand { get; set; }

        public RenameCommand RenameCommand { get; set; }

        public EndRenamingCommand EndRenamingCommand { get; set; }

        public DeleteCommand DeleteCommand { get; set; }

        private Note selectedNote;
        public Note SelectedNote
        {
            get { return selectedNote; }
            set 
            {
                selectedNote = value;
                OnPropertyChanged("SelectedNote");
                SelectedNoteChanged?.Invoke(this, new EventArgs());
            }
        }


        private Visibility renameNotebookTextBoxVisibility;
        public Visibility RenameNotebookTextBoxVisibility
        {
            get { return renameNotebookTextBoxVisibility; }
            set 
            {
                renameNotebookTextBoxVisibility = value;

                //close the edeting for the NoteBook if starting to edit a Note
                if (RenameNoteTextBoxVisibility == Visibility.Visible && value == Visibility.Visible)
                {
                    RenameNoteTextBoxVisibility = Visibility.Collapsed;
                }

                OnPropertyChanged("RenameNotebookTextBoxVisibility");
            }
        }


        private Visibility renameNoteTextBoxVisibility;
        public Visibility RenameNoteTextBoxVisibility
        {
            get { return renameNoteTextBoxVisibility; }
            set 
            {
                renameNoteTextBoxVisibility = value;

                //close the edeting for the NoteBook if starting to edit a Note
                if (RenameNotebookTextBoxVisibility == Visibility.Visible && value == Visibility.Visible)
                {
                    RenameNotebookTextBoxVisibility = Visibility.Collapsed;
                }

                OnPropertyChanged("RenameNoteTextBoxVisibility");
            }
        }



        public NotesVM()
        {
            NewNoteCommand = new NewNoteCommand(this);
            NewNotebookCommand = new NewNotebookCommand(this);
            RenameCommand = new RenameCommand(this);
            EndRenamingCommand = new EndRenamingCommand(this);
            DeleteCommand = new DeleteCommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            getNotebooks();

            RenameNotebookTextBoxVisibility = Visibility.Collapsed;
            RenameNoteTextBoxVisibility = Visibility.Collapsed;
        }


        public async void createNote(string _notebookId)
        {
            Note newNote = new Note()
            {
                ParentNotebookId = _notebookId,
                CreateTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                Title = "New note"
            };

            await DatabaseHelper.Insert(newNote);

            getNotes();  //Updates the whole list
        }

        public async void createNotebook()
        {
            Notebook newNotebook = new Notebook()
            {
                Name = "New notebook",
                UserId = App.UserId
            };

            await DatabaseHelper.Insert(newNotebook);

            getNotebooks();    //Updates the whole list
        }

        public async void getNotebooks()
        {
            var notebooks = (await DatabaseHelper.Read<Notebook>()).Where(n => n.UserId == App.UserId).ToList();

            Notebooks.Clear();
            foreach (var o in notebooks)
            {
                Notebooks.Add(o);
            }
        }
        
        public async void getNotes()
        {
            if (SelectedNotebook != null)
            {
                var notes = (await DatabaseHelper.Read<Note>()).Where(n => n.ParentNotebookId == SelectedNotebook.Id).ToList();

                Notes.Clear();

                foreach (var o in notes)
                {
                    Notes.Add(o);
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
        public void startEditing(object parameter)
        {
            if (parameter is Notebook)
                RenameNotebookTextBoxVisibility = Visibility.Visible;
            else if (parameter is Note)
                RenameNoteTextBoxVisibility = Visibility.Visible;
        }

        public async void stopEditingNotebook(object a)
        {
            if(a is Notebook)
            {
                RenameNotebookTextBoxVisibility = Visibility.Collapsed;
                await DatabaseHelper.Update(a as Notebook);
                getNotebooks();
            } 
            else if (a is Note)
            {
                RenameNoteTextBoxVisibility = Visibility.Collapsed;
                await DatabaseHelper.Update(a as Note);
                getNotebooks();
            }
        }

        private void KeyDown()
        {

        }

    }
}
