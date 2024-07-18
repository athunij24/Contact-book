using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppContacts
{
    internal class ContactController
    {
        internal static void AddContact()
        {
            var fName = AnsiConsole.Ask<string>("What's the contact's first name?");
            var lName = AnsiConsole.Ask<string>("What's the contact's last name?");
            var email = AnsiConsole.Ask<string>("What's the email?");
            var phoneNumber = AnsiConsole.Ask<string>("What's the phone number?");
            Contact contact = new Contact
            {
                FirstName = fName,
                LastName = lName,
                Email = email,
                PhoneNumber = phoneNumber,
            };

            using (var context = new ContactsContext())
            {
                context.Contacts.Add(contact);
                context.SaveChanges();
            }
        }

        internal static void DeleteContact()
        {
            throw new NotImplementedException();
        }

        internal static void EmailContact()
        {
            throw new NotImplementedException();
        }

        internal static void UpdateContact()
        {
            throw new NotImplementedException();
        }

        internal static void ReadContacts()
        {
            Table table = new Table();
            table.AddColumn("First Name");
            table.AddColumn("Last Name");
            table.AddColumn("Email");
            table.AddColumn("Phone Number");
            using (var context = new ContactsContext())
            {
                var contactList = context.Contacts.ToList();
                foreach(Contact currContact in contactList)
                {
                    table.AddRow(currContact.FirstName, currContact.LastName, currContact.Email, currContact.PhoneNumber);
                }
            }
            AnsiConsole.Write(table);
        }
    }
}
