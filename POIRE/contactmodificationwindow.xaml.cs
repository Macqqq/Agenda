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
            }

            private void SaveButton_Click(object sender, RoutedEventArgs e)
            {
                //// Mettre à jour l'objet contact et la base de données
                var contactToUpdate = _context.Contactstables.Find(_contact.Id);
                if (contactToUpdate != null)
                {
                    contactToUpdate.Name = TB_Name.Text;
                    contactToUpdate.Prenom = FirstNameTextBox.Text;
                    contactToUpdate.Email = EmailTextBox.Text;
                    contactToUpdate.Phone = Convert.ToInt32(PhoneTextBox.Text);
                    _context.SaveChanges();
                    this.Close();
                }
            }

            private void CancelButton_Click(object sender, RoutedEventArgs e)
            {
                this.Close();
            }
        }
    }
