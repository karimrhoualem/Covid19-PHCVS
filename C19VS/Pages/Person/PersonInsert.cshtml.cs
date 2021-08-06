using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Person
{
    public class InsertModel : PageModel
    {
        public const string TASK_NAME = "Person - InsertModel";

        public Models.Person Person { get; private set; }
        public DatabaseHelper DatabaseHelper { get; private set; }

        public InsertModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper) databaseHelper;
        }

        public IActionResult OnPostAsync(Models.Person person)
        {
            DatabaseHelper.ConnectDatabase();

            bool insertSuccessful = DatabaseHelper.InsertRecord(typeof(Models.Person), person);

            if (insertSuccessful)
            {
                Console.WriteLine($"[{TASK_NAME}] {person.firstName} {person.lastName} inserted successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error inserting {person.firstName} {person.lastName}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Person/PersonIndex");
        }
    }
}
