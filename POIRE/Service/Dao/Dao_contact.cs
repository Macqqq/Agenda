using ContactsManager;
using POIRE.AgendaMb;
using POIRE.MusicDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace POIRE.Dao
{
    public class DaoContact
    {
        private readonly AgendaMbContext _context;

        public DaoContact(AgendaMbContext context)
        {
            _context = context;
        }

        public IEnumerable<Contactstable> GetAllContacts()
        {
            return _context.Contactstables.ToList();
        }

        public void AddContact(Contactstable contact)
        {
            // Assurez-vous que l'ID n'est pas défini ou qu'il est nouveau / unique
            if (contact.IdContactstable == 0 || !_context.Contactstables.Any(c => c.IdContactstable == contact.IdContactstable))
            {
                _context.Contactstables.Add(contact);
                _context.SaveChanges();
            }
            else
            {
                // Gérer l'erreur ou logguer que l'ID existe déjà
            }
        }



        public void DeleteContact(Contact selectedContact)
        {
            if (selectedContact == null)
            {
                MessageBox.Show("Veuillez sélectionner un contact à supprimer.");
                return;
            }

            var result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer le contact sélectionné ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var contactToDelete = _context.Contactstables.Find(selectedContact.Id);
                    if (contactToDelete != null)
                    {
                        _context.Contactstables.Remove(contactToDelete);
                        _context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Le contact sélectionné est introuvable dans la base de données.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Une erreur est survenue lors de la suppression du contact : {ex.Message}");
                }
            }
        }

        public void ModifyContact(Contact selectedContact)
        {
            if (selectedContact == null)
            {
                MessageBox.Show("Veuillez sélectionner un contact à modifier.");
                return;
            }

            var contactToUpdate = _context.Contactstables.Find(selectedContact.Id);
            if (contactToUpdate != null)
            {
                contactToUpdate.Name = selectedContact.Name;
                contactToUpdate.Prenom = selectedContact.FirstName;
                contactToUpdate.Email = selectedContact.Email;
                contactToUpdate.Phone = Convert.ToInt32(selectedContact.Phone);
                _context.SaveChanges();
                MessageBox.Show("Le contact a été modifié avec succès.");
            }
            else
            {
                MessageBox.Show("Le contact sélectionné est introuvable dans la base de données.");
            }
        }
    }
}
