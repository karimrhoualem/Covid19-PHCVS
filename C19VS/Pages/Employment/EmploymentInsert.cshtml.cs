using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Employment
{
    public class InsertModel : PageModel
    {
        public const string TASK_NAME = "Employment - InsertModel";

        public Models.Employment Employment { get; private set; }
        public DatabaseHelper DatabaseHelper { get; private set; }

        public InsertModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper) databaseHelper;
        }

        public IActionResult OnPostAsync(Models.Employment employment)
        {
            DatabaseHelper.ConnectDatabase();

            bool insertSuccessful = DatabaseHelper.InsertRecord(typeof(Models.Employment), employment);

            if (insertSuccessful)
            {
                Console.WriteLine($"[{TASK_NAME}] employment contract {employment.contractNum} inserted successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error inserting employment contract {employment.contractNum}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Employment/EmploymentIndex");
        }
    }
}
