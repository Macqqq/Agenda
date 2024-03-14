using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using POIRE;
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
                    Phone = dbContact.Phone?.ToString(),
                    Adresse = dbContact.Adresse,
                    CodePostal = dbContact.CodePostal?.ToString(),
                    Ville = dbContact.Ville,
                    DateOfBirth = dbContact.DateOfBirth?.ToString("dd/MM/yyyy") // Supposons que vous utilisez le format jour/mois/année
                });
            }
        }

        private void AddMember_Click(object sender, RoutedEventArgs e)
        {
            var contact = new Contactstable
            {
                Name = NameTextBox.Text.Equals("Nom") ? "" : NameTextBox.Text,
                Prenom = FirstNameTextBox.Text.Equals("Prénom") ? "" : FirstNameTextBox.Text,
                Email = EmailTextBox.Text.Equals("Email") ? "" : EmailTextBox.Text,
                Phone = PhoneTextBox.Text.Equals("Téléphone") ? null : Convert.ToInt32(PhoneTextBox.Text),
                Adresse = AddressTextBox.Text.Equals("Adresse") ? "" : AddressTextBox.Text,
                CodePostal = PostalCodeTextBox.Text.Equals("Code Postal") ? null : Convert.ToInt32(PostalCodeTextBox.Text),
                Ville = CityTextBox.Text.Equals("Ville") ? "" : CityTextBox.Text,
                DateOfBirth = DateOnly.TryParse(BirthDateTextBox.Text, out var dateOfBirth) ? dateOfBirth : (DateOnly?)null
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
            var searchQuery = SearchTextBox.Text.ToLower().Trim();

            // Vérifie si la requête de recherche est vide
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                LoadContacts(); // Recharge tous les contacts si la barre de recherche est vide.
            }
            else
            {
                Contacts.Clear();
                var filteredContacts = _daoContact.GetAllContacts()
                    .Where(c => c.Name.ToLower().Contains(searchQuery) ||
                                c.Prenom.ToLower().Contains(searchQuery) ||
                                (c.Email != null && c.Email.ToLower().Contains(searchQuery)) ||
                                (c.Phone != null && c.Phone.ToString().Contains(searchQuery)) ||
                                (c.Adresse != null && c.Adresse.ToLower().Contains(searchQuery)) ||
                                (c.CodePostal != null && c.CodePostal.ToString().Contains(searchQuery)) ||
                                (c.Ville != null && c.Ville.ToLower().Contains(searchQuery)) ||
                                (c.DateOfBirth != null && c.DateOfBirth.Value.ToString("dd/MM/yyyy").Contains(searchQuery)));

                foreach (var contact in filteredContacts)
                {
                    Contacts.Add(new Contact
                    {
                        Id = contact.IdContactstable,
                        Name = contact.Name,
                        FirstName = contact.Prenom,
                        Email = contact.Email,
                        Phone = contact.Phone?.ToString(),
                        Adresse = contact.Adresse,
                        CodePostal = contact.CodePostal?.ToString(),
                        Ville = contact.Ville,
                        DateOfBirth = contact.DateOfBirth?.ToString("dd/MM/yyyy")
                    });
                }
            }
        }

        private void ModifyMember_Click(object sender, RoutedEventArgs e)
        {
            var selectedContact = ContactsList.SelectedItem as Contact;
            if (selectedContact == null)
            {
                MessageBox.Show("Veuillez sélectionner un contact à modifier.");
                return;
            }

            var modificationWindow = new ContactModificationWindow(selectedContact, _context);
            modificationWindow.ShowDialog();

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
        public string Adresse { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string DateOfBirth { get; set; } // Formaté en chaîne pour l'affichage
    }
}
