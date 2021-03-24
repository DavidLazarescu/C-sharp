using DesktopContactApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopContactApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Contact> contacts;
        
        public MainWindow()
        {
            InitializeComponent();
            contacts = new List<Contact>();

            readDatabase();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow = new NewContactWindow();
            
            newContactWindow.ShowDialog();
            
            readDatabase();
        }

        public void readDatabase()
        {

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DatabasePath))
            {
                conn.CreateTable<Contact>();   //Just creates the table if it does not exist
                contacts = conn.Table<Contact>().ToList().OrderBy(c => c.name).ToList();
            }

            //Show contacts
            if(contacts != null)
            {
                contactsListView.ItemsSource = contacts;
            }
        }

        //Searching
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchTextBox = sender as TextBox;
            
            var filteredList = contacts.Where(c => c.name.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();

            contactsListView.ItemsSource = filteredList;
        }

        private void contactsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contact contact = (Contact)contactsListView.SelectedItem;

            if(contact != null)
            {
                ContactDetailsWindow contactDetailsWindow = new ContactDetailsWindow(contact);
                contactDetailsWindow.ShowDialog();
                readDatabase();
            }
        }
    }
}