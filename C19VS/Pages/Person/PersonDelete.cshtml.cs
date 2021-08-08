using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Person
{
    public class DeleteModel : PageModel
    {
        public const string TASK_NAME = "Person - DeleteModel";

        public Models.Person Person { get; private set; }
        
        private DatabaseHelper DatabaseHelper;

        public DeleteModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public async Task<IActionResult> OnGetAsync(string medicare)
        {

            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(medicare), medicare);

            DatabaseHelper.ConnectDatabase();

            Person = (Models.Person) await DatabaseHelper.SelectRecordAsync(typeof(Models.Person), dictionary);

            DatabaseHelper.DisconnectDatabase();

            if (Person == null)
            {
                return RedirectToPage("/Person/PersonIndex");
            }

            return Page();
        }

        public IActionResult OnPostAsync(Models.Person person)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(person.medicare), person.medicare);

            DatabaseHelper.ConnectDatabase();

            bool deleteSuccesful = DatabaseHelper.DeleteRecord(typeof(Models.Person), dictionary);

            if (deleteSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}] {person.firstName} {person.lastName} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error deleting {person.firstName} {person.lastName}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Person/PersonIndex");
        }
    }
}
