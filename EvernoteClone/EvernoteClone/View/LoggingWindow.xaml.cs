using EvernoteClone.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EvernoteClone.View
{
    /// <summary>
    /// Interaction logic for LoggingWindow.xaml
    /// </summary>
    public partial class LoggingWindow : Window
    {
        LoginVM VM;

        public LoggingWindow()
        {
            InitializeComponent();

            VM = Resources["vm"] as LoginVM;

            VM.AuthenticationSucessfull += authenticationSucessfull;
        }


        private void authenticationSucessfull(object sender, EventArgs e)
        {
            NotesWindow notesWindow = new NotesWindow();
            notesWindow.Show();
            Close();
        }
    }
}
