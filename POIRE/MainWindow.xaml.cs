using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using POIRE.AgendaMb;
using POIRE.Dao;
using POIRE.MusicDB;

namespace ContactsManager
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Contact> Contacts { get; set; }
        private readonly AgendaMbContext _context = new AgendaMbContext();
        private readonly DaoContact _daoContact;

        public MainWindow()
        {
            InitializeComponent();
            Contacts = new ObservableCollection<Contact>();
            _daoContact = new DaoContact(_context);
            LoadContacts();
            ContactsList.ItemsSource = Contacts;
        }

        private void LoadContacts()
        {
            Contacts.Clear();
            var contactsFromDb = _daoContact.GetAllContacts();
            foreach (var dbContact in contactsFromDb)
            {
                Contacts.Add(new Contact
                {
                    Id = dbContact.IdContactstable,
                    Name = dbContact.Name,
                    FirstName = dbContact.Prenom,
                    Email = dbContact.Email,
                    Phone = dbContact.Phone?.ToString()
                });
            }
        }

        private void AddMember_Click(object sender, RoutedEventArgs e)
        {
            var contact = new Contactstable
            {
                // Ne définissez pas IdContactstable ici, laissez la base de données s'en occuper
                Name = NameTextBox.Text,
                Prenom = FirstNameTextBox.Text,
                Email = EmailTextBox.Text,
                Phone = Convert.ToInt32(PhoneTextBox.Text)  // Assurez-vous que le téléphone est bien géré comme int
            };

            _daoContact.AddContact(contact);
            LoadContacts();
        }

        private void DeleteMember_Click(object sender, RoutedEventArgs e)
        {
            var selectedContact = ContactsList.SelectedItem as Contact;
            _daoContact.DeleteContact(selectedContact);
            LoadContacts();
        }

        private void ResetFields_Click(object sender, RoutedEventArgs e)
        {
            NameTextBox.Text = "Name";
            FirstNameTextBox.Text = "First Name";
            EmailTextBox.Text = "Email";
            PhoneTextBox.Text = "Phone";

            NameTextBox.Foreground = Brushes.Gray;
            FirstNameTextBox.Foreground = Brushes.Gray;
            EmailTextBox.Foreground = Brushes.Gray;
            PhoneTextBox.Foreground = Brushes.Gray;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Foreground == Brushes.Gray)
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Foreground = Brushes.Gray;
                switch (textBox.Name)
                {
                    case "NameTextBox":
                        textBox.Text = "Name";
                        break;
                    case "FirstNameTextBox":
                        textBox.Text = "First Name";
                        break;
                    case "EmailTextBox":
                        textBox.Text = "Email";
                        break;
                    case "PhoneTextBox":
                        textBox.Text = "Phone";
                        break;
                }
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            // Implémentez la logique de recherche ici
        }

        private void ModifyMember_Click(object sender, RoutedEventArgs e)
        {
            var selectedContact = ContactsList.SelectedItem as Contact;
            _daoContact.ModifyContact(selectedContact);
            LoadContacts();
        }
    }

    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
