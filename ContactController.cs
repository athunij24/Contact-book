using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
            ReadContacts();
            var contactID = AnsiConsole.Ask<int>("What's the contact you want to remove's id?");
            using (var context = new ContactsContext())
            {
                try
                {
                    var contactToRemove = context.Contacts.Single(c => c.Id == contactID);
                    context.Contacts.Remove(contactToRemove);
                    context.SaveChanges();
                }
                catch(InvalidOperationException)
                {
                    Console.WriteLine("Invalid id entered");
                }
            }
        }


        internal static void UpdateContact()
        {
            ReadContacts();
            var contactID = AnsiConsole.Ask<int>("What's the contact you want to update's id?");
            using (var context = new ContactsContext())
            {
                Contact contactToUpdate = new Contact();
                try
                {
                    var contact = context.Contacts.Single(c => c.Id == contactID);
                    contactToUpdate = contact;

                }
                catch (InvalidOperationException) { Console.WriteLine("Invalid Id entered"); return; }
                var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Which field")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "First Name", "Last Name", "Email",
                        "Phone Number","Quit"
                    }));
                while (choice != "Quit")
                {

                    switch (choice)
                    {
                        case "First Name":
                            var newFname = AnsiConsole.Ask<string>("Enter new [green]first name[/]?");
                            contactToUpdate.FirstName = newFname;
                            break;
                        case "Last Name":
                            var newLname = AnsiConsole.Ask<string>("Enter new [green]last name[/]?");
                            contactToUpdate.LastName = newLname;
                            break;
                        case "Email":
                            var newEmail = AnsiConsole.Ask<string>("Enter new [green]email[/]?");
                            contactToUpdate.Email = newEmail;
                            break;
                        case "Phone Number":
                            var newPhoneNum = AnsiConsole.Ask<string>("Enter new [green]phone number[/]?");
                            contactToUpdate.PhoneNumber = newPhoneNum;
                            break;
                        default:
                            break;
                    }
                    context.SaveChanges();

                    choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Which field")
                        .PageSize(10)
                        .AddChoices(new[] {
                                "First Name", "Last Name", "Email",
                                "Phone Number","Quit"
                        }));
                }
            }

        }
        
    

        internal static void ReadContacts()
        {
            Table table = new Table();
            table.AddColumn("ContactID");
            table.AddColumn("First Name");
            table.AddColumn("Last Name");
            table.AddColumn("Email");
            table.AddColumn("Phone Number");
            using (var context = new ContactsContext())
            {
                var contactList = context.Contacts.ToList();
                foreach(Contact currContact in contactList)
                {
                    table.AddRow(currContact.Id.ToString(),currContact.FirstName, currContact.LastName, currContact.Email, currContact.PhoneNumber);
                }
            }
            AnsiConsole.Write(table);
        }
    }
}
