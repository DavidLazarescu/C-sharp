using DesktopContactApp.Classes;
using SQLite;
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

namespace DesktopContactApp
{
    /// <summary>
    /// Interaction logic for NewContactWindow.xaml
    /// </summary>
    ///

    public partial class NewContactWindow : Window
    {
        private enum CurrentTextBox
        {
            name = 0, email, phone, note
        }
        private CurrentTextBox currentTextBox;

        public NewContactWindow()
        {
            InitializeComponent();

            //Registers the keyboard listener event
            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);

            //Lets the window open in the middle of the main window
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            //Sets automatical keyboard focus
            nameTextBox.Focus();
            currentTextBox = CurrentTextBox.name;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            saveNewContact();

            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void saveNewContact()
        {
            Contact contact = new Contact()
            {
                name = nameTextBox.Text,
                email = emailTextBox.Text,
                phoneNumber = phoneNumberTextBox.Text,
                notes = notesTextBox.Text
            };

            using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.Insert(contact);
            }
        }


        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                currentTextBox = (CurrentTextBox)(int)currentTextBox + 1;

                switch (currentTextBox)
                {
                    case CurrentTextBox.email:
                        emailTextBox.Focus();
                        break;
                    case CurrentTextBox.phone:
                        phoneNumberTextBox.Focus();
                        break;
                    case CurrentTextBox.note:
                        notesTextBox.Focus();
                        break;
                    default:
                        saveNewContact();
                        this.Close();
                        break;
                }
            }
            else if(e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void OnStart()
        {

        }
    }
}
