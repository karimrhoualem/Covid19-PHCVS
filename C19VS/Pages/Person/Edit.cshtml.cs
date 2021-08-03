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

        public async Task<IActionResult> OnGetAsync(string medicare)
        {
            DatabaseHelper.ConnectDatabase();

            Person = (Models.Person) await DatabaseHelper.SelectRecordAsync(typeof(Models.Person), nameof(medicare), medicare);

            DatabaseHelper.DisconnectDatabase();
            
            if (Person == null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public IActionResult OnPost(Models.Person person)
        {
            DatabaseHelper.ConnectDatabase();

            bool updateSuccesful = DatabaseHelper.UpdateRecord(typeof(Models.Person), person, nameof(person.medicare), person.medicare);

            if (updateSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}] {person.firstName} {person.lastName} updated successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error updating {person.firstName} {person.lastName}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Index");
        }
    }
}
