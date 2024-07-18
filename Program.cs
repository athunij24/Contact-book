using Microsoft.VisualBasic;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Reflection.Metadata;
using System.Xml.Serialization;
namespace ConsoleAppContacts
{
    class Program
    {
        public static string promptUser()
        {
            var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What would you like to do?")
                .PageSize(10)
                .AddChoices(new[] {
                    "Add Contact", "Update Contact", "Delete Contact",
                    "Email Contact", "Quit"
                }));
            return choice;
        }
        static void Main(string[] args)
        {
            AnsiConsole.Write(
            new FigletText("Contact Book")
                .Color(Color.Blue));

            string choice = promptUser();

            while (choice != "Quit")
            {
                switch (choice)
                {
                    case "Add Contact":
                        ContactController.AddContact();
                        break;
                    case "Update Contact":
                        ContactController.UpdateContact();
                        break;
                    case "Delete Contact":
                        ContactController.DeleteContact();
                        break;
                    case "Email Contact":
                        ContactController.EmailContact();
                        break;
                    default:
                        return;
                }
                choice = promptUser();
            }
        }
    }
}