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
    /// Interaction logic for ContactDetailsWindow.xaml
    /// </summary>
    public partial class ContactDetailsWindow : Window
    {
        Contact contact;
        public ContactDetailsWindow(Contact contact)
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);

            //Lets the window open in the middle of the main window
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;


            this.contact = contact;

            nameTextBox.Text = contact.name;
            emailTextBox.Text = contact.email;
            phoneNumberTextBox.Text = contact.phoneNumber;
            notesNumberTextBox.Text = contact.notes;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            updateContact();
            Close();
        }

        private void updateContact()
        {
            contact.name = nameTextBox.Text;
            contact.email = emailTextBox.Text;
            contact.phoneNumber = phoneNumberTextBox.Text;
            contact.notes = notesNumberTextBox.Text;

            using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.Update(contact);
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.Delete(contact);
            }

            Close();
        }

        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
            else if (e.Key == Key.Enter)
            {
                updateContact();
                this.Close();
            }
        }
    }
}
