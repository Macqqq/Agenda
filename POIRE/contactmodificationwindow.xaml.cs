using ContactsManager;
using POIRE.AgendaMb;
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
using System.Windows.Shapes;
using System.Windows;


    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    namespace POIRE
    {
        public partial class ContactModificationWindow : Window
        {
            private Contact _contact;
            private AgendaMbContext _context;

            public ContactModificationWindow(Contact contact, AgendaMbContext context)
        {
            InitializeComponent();
            _contact = contact;
            _context = context;
            TB_Name.Text = contact.Name;
            FirstNameTextBox.Text = contact.FirstName;
            EmailTextBox.Text = contact.Email;
            PhoneTextBox.Text = contact.Phone;
            PostalCodeTextBox.Text = contact.CodePostal;
            CityTextBox.Text = contact.Ville;
            BirthDateTextBox.Text = contact.DateOfBirth;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var contactToUpdate = _context.Contactstables.Find(_contact.Id);
            if (contactToUpdate != null)
            {
                contactToUpdate.Name = TB_Name.Text;
                contactToUpdate.Prenom = FirstNameTextBox.Text;
                contactToUpdate.Email = EmailTextBox.Text;

                // Conversion sécurisée du code postal en int?.
                if (int.TryParse(PostalCodeTextBox.Text, out var postalCodeResult))
                {
                    contactToUpdate.CodePostal = postalCodeResult;
                }
                else if (string.IsNullOrEmpty(PostalCodeTextBox.Text))
                {
                    contactToUpdate.CodePostal = null; // Assigner null si le champ est vide.
                }
                else
                {
                    MessageBox.Show("Le code postal doit être un nombre entier.");
                    return;
                }

                contactToUpdate.Ville = CityTextBox.Text;

                // Conversion sécurisée de la date de naissance en DateOnly?.
                if (DateOnly.TryParse(BirthDateTextBox.Text, out var dateOfBirthResult))
                {
                    contactToUpdate.DateOfBirth = dateOfBirthResult;
                }
                else if (string.IsNullOrEmpty(BirthDateTextBox.Text))
                {
                    contactToUpdate.DateOfBirth = null; // Assigner null si le champ est vide.
                }
                else
                {
                    MessageBox.Show("La date de naissance doit être dans un format valide (JJ/MM/AAAA).");
                    return;
                }

                _context.SaveChanges();
                MessageBox.Show("Le contact a été modifié avec succès.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Le contact sélectionné est introuvable dans la base de données.");
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
            {
                this.Close();
            }
        }
    }
