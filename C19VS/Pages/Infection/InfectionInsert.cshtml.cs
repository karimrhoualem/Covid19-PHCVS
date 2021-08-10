using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Infection
{
    public class InsertModel : PageModel
    {
        public const string TASK_NAME = "Infection - InsertModel";

        public Models.Infection Infection { get; private set; }
        public DatabaseHelper DatabaseHelper { get; private set; }

        public InsertModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper) databaseHelper;
        }

        public IActionResult OnPostAsync(Models.Infection infection)
        {
            DatabaseHelper.ConnectDatabase();

            bool insertSuccessful = DatabaseHelper.InsertRecord(typeof(Models.Infection), infection);

            if (insertSuccessful)
            {
                Console.WriteLine($"[{TASK_NAME}] infection of {infection.SSN} inserted successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error inserting infection of {infection.SSN}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Infection/InfectionIndex");
        }
    }
}
