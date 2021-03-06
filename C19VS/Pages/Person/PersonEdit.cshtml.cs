using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Person
{
    public class EditModel : PageModel
    {
        public const string TASK_NAME = "Person - EditModel";

        public Models.Person Person { get; private set; }
        
        private DatabaseHelper DatabaseHelper;

        public EditModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public async Task<IActionResult> OnGetAsync(string SSN)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(SSN), SSN);

            DatabaseHelper.ConnectDatabase();

            Person = (Models.Person) await DatabaseHelper.SelectRecordAsync(typeof(Models.Person), dictionary);

            DatabaseHelper.DisconnectDatabase();
            
            if (Person == null)
            {
                return RedirectToPage("/Person/PersonIndex");
            }

            return Page();
        }

        public IActionResult OnPost(Models.Person person)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(person.SSN), person.SSN);

            DatabaseHelper.ConnectDatabase();

            bool updateSuccesful = DatabaseHelper.UpdateRecord(typeof(Models.Person), person, dictionary);

            if (updateSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}] {person.firstName} {person.lastName} updated successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error updating {person.firstName} {person.lastName}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Person/PersonIndex");
        }
    }
}
